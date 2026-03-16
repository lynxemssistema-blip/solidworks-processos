Option Strict On
Option Infer On

Imports System.Collections.Generic
Imports System.Drawing
Imports System.Globalization
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

' ------------------------------------------------------------
' ChapaFormatarDataGrid
' Utilitários de preparação/estilo/ajustes para DataGridView,
' com foco em performance e render sob demanda.
' ------------------------------------------------------------
Public NotInheritable Class ChapaFormatarDataGrid

    Private Sub New()
    End Sub

    '======================
    ' Configuração base
    '======================
    Public Shared Sub PrepararGrid(g As DataGridView,
                                   Optional fonte As String = "Segoe UI",
                                   Optional tamanhoFonte As Single = 9.0F,
                                   Optional usarDoubleBuffer As Boolean = True)

        ' Comportamento
        g.RowHeadersVisible = False
        g.AllowUserToAddRows = False
        g.AllowUserToDeleteRows = False
        g.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        g.MultiSelect = False

        ' Autosize agressivo = desligado
        g.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None
        g.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None

        ' Estilos (1 instância compartilhada)
        Dim cellStyle As New DataGridViewCellStyle With {
            .Font = New Font(fonte, tamanhoFonte, FontStyle.Regular, GraphicsUnit.Point),
            .Alignment = DataGridViewContentAlignment.MiddleLeft,
            .WrapMode = DataGridViewTriState.[False],
            .NullValue = "",
            .Format = "" ' defina por coluna quando precisar
        }
        g.DefaultCellStyle = cellStyle

        g.ColumnHeadersDefaultCellStyle = New DataGridViewCellStyle(cellStyle) With {
            .Font = New Font(fonte & " Semibold", tamanhoFonte, FontStyle.Regular, GraphicsUnit.Point),
            .Alignment = DataGridViewContentAlignment.MiddleLeft,
            .WrapMode = DataGridViewTriState.[False]
        }

        ' Double buffer para reduzir flicker
        If usarDoubleBuffer Then
            AtivarDoubleBuffer(g, True)
        End If
    End Sub

    '======================
    ' Coluna de imagem
    '======================
    Public Shared Sub GarantirColunaImagem(g As DataGridView, colName As String, header As String, Optional insertAt As Integer = 0, Optional largura As Integer = 28)
        Dim col As DataGridViewImageColumn = TryCast(g.Columns(colName), DataGridViewImageColumn)
        If col Is Nothing Then
            col = New DataGridViewImageColumn() With {
                .Name = colName,
                .HeaderText = header,
                .ImageLayout = DataGridViewImageCellLayout.Zoom,
                .Width = largura,
                .ReadOnly = True
            }
            g.Columns.Insert(Math.Max(0, Math.Min(insertAt, g.Columns.Count)), col)
        End If
    End Sub

    '======================
    ' Visibilidade / Freeze
    '======================
    Public Shared Sub OcultarColunas(g As DataGridView, ParamArray nomes() As String)
        For Each n In nomes
            If g.Columns.Contains(n) Then g.Columns(n).Visible = False
        Next
    End Sub

    Public Shared Sub FixarColuna(g As DataGridView, nome As String, Optional frozen As Boolean = True)
        If g Is Nothing Then Exit Sub
        If g.Columns.Contains(nome) Then
            g.Columns(nome).Frozen = frozen
        End If
    End Sub

    '======================
    ' Autosize uma vez
    '======================
    Public Shared Sub AutoSizeUmaVez(g As DataGridView, Optional apenasVisiveis As Boolean = True)
        ' Aplica DisplayedCells para medir, depois fixa largura (evita custo contínuo).
        For Each c As DataGridViewColumn In g.Columns
            If (Not apenasVisiveis) OrElse c.Visible Then
                Dim old = c.AutoSizeMode
                c.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
                Dim w = c.Width
                c.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                c.Width = w
                c.AutoSizeMode = old ' mantém None, mas devolve se quiser
            End If
        Next
    End Sub

    '======================
    ' DoubleBuffer (reflexão)
    '======================
    Public Shared Sub AtivarDoubleBuffer(g As DataGridView, habilitar As Boolean)
        Dim t = GetType(DataGridView)
        Dim pi = t.GetProperty("DoubleBuffered",
                               Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic)
        pi?.SetValue(g, habilitar, Nothing)
    End Sub

    '======================
    ' Suspender/Retomar desenho (grandes mudanças)
    '======================
    Private Const WM_SETREDRAW As Integer = &HB

    <DllImport("user32.dll")>
    Private Shared Function SendMessage(hWnd As IntPtr, msg As Integer, wParam As Boolean, lParam As Integer) As IntPtr
    End Function

    Public Shared Sub SuspenderDesenho(g As DataGridView, suspender As Boolean)
        SendMessage(g.Handle, WM_SETREDRAW, Not suspender, 0)
        If Not suspender Then g.Refresh()
    End Sub

    '======================
    ' VirtualMode (helper)
    '======================
    Public Shared Sub AtivarVirtualMode(g As DataGridView, rowCount As Integer)
        g.VirtualMode = True
        g.RowCount = rowCount
    End Sub

    '======================
    ' Tratamento de erro de binding
    '======================
    Public Shared Sub LigarTratamentoDataError(ParamArray grids() As DataGridView)
        For Each g In grids
            AddHandler g.DataError,
                Sub(sender As Object, e As DataGridViewDataErrorEventArgs)
                    ' Evita popups; logue conforme sua infra
                    e.Cancel = True
                End Sub
        Next
    End Sub

    '======================
    ' Binder de eventos – leve e reutilizável
    '======================
    Public Shared Function CriarBinder(g As DataGridView) As ChapaGridBinder
        Return New ChapaGridBinder(g)
    End Function

    ' ------------------------------------------------------------
    ' Binder: encapsula handlers com cache leve (por grid)
    ' ------------------------------------------------------------
    Public NotInheritable Class ChapaGridBinder
        Private ReadOnly _g As DataGridView
        Private ReadOnly _existsCache As New Dictionary(Of String, Boolean)(StringComparer.OrdinalIgnoreCase)

        ' Opções de formatação sob demanda
        Public Property ColunaCaminhoArquivo As String = "EnderecoArquivo"

        Public Property ColunaDxf As String = "DGVDXF"
        Public Property ColunaPdf As String = "DGVPDF"

        ' Recursos (injete seus ícones)
        Public Property IconeDxf As Image

        Public Property IconePdf As Image
        Public Property IconeVazio As Image

        ' Cultura padrão para números/datas
        Public Property Cultura As CultureInfo = New CultureInfo("pt-BR")

        Public Sub New(g As DataGridView)
            _g = g
        End Sub

        ' Liga handlers essenciais
        Public Sub Bind(Optional ligarCellFormatting As Boolean = True,
                        Optional ligarDataBindingComplete As Boolean = True)
            If ligarCellFormatting Then
                AddHandler _g.CellFormatting, AddressOf OnCellFormatting
            End If
            If ligarDataBindingComplete Then
                AddHandler _g.DataBindingComplete, AddressOf OnDataBindingComplete
            End If
        End Sub

        ' Desliga (se for descartar o grid)
        Public Sub Unbind()
            RemoveHandler _g.CellFormatting, AddressOf OnCellFormatting
            RemoveHandler _g.DataBindingComplete, AddressOf OnDataBindingComplete
        End Sub

        ' ======= DataBindingComplete: ajustes macro (sem loopar linhas) =======
        Private Sub OnDataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs)
            Dim g = DirectCast(sender, DataGridView)
            If g Is Nothing OrElse g.Columns.Count = 0 Then Exit Sub

            ' Exemplo: aplicar formatação macro e 1 auto-size
            ' > Ajuste headers, números/datas por coluna, visibilidade, freeze…
            ' Evite percorrer g.Rows aqui.
            For Each c As DataGridViewColumn In g.Columns
                ' Datas/números via DefaultCellStyle.Format (melhor que formatar célula a célula)
                If c.ValueType Is GetType(Date) OrElse c.ValueType Is GetType(DateTime) Then
                    c.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm"
                    c.DefaultCellStyle.FormatProvider = Cultura
                ElseIf c.ValueType Is GetType(Decimal) OrElse c.ValueType Is GetType(Double) OrElse c.ValueType Is GetType(Single) Then
                    c.DefaultCellStyle.Format = "N2"
                    c.DefaultCellStyle.FormatProvider = Cultura
                    c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                End If
            Next

            ' Auto-size pontual (uma vez)
            AutoSizeUmaVez(g, apenasVisiveis:=True)
        End Sub

        ' ======= CellFormatting: sob demanda (rápido) =======
        Private Sub OnCellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)
            If e.RowIndex < 0 Then Exit Sub

            Dim g = DirectCast(sender, DataGridView)
            Dim colName = g.Columns(e.ColumnIndex).Name

            ' Exemplo específico: mapear EnderecoArquivo -> ícones DXF/PDF
            If colName = ColunaDxf OrElse colName = ColunaPdf Then
                Dim endereco As String = LerTextoSeguro(g.Rows(e.RowIndex).Cells(ColunaCaminhoArquivo)?.Value)

                If endereco.EndsWith(".SLDPRT", StringComparison.OrdinalIgnoreCase) OrElse
                   endereco.EndsWith(".SLDASM", StringComparison.OrdinalIgnoreCase) Then

                    Dim alvo As String = IO.Path.ChangeExtension(endereco, If(colName = ColunaDxf, ".dxf", ".pdf"))
                    e.Value = If(ExistsCached(alvo),
                                 If(colName = ColunaDxf, IconeDxf, IconePdf),
                                 IconeVazio)
                Else
                    e.Value = IconeVazio
                End If

                e.FormattingApplied = True
            End If
        End Sub

        ' ======= Utilitários internos =======
        Private Function LerTextoSeguro(v As Object) As String
            If v Is Nothing OrElse v Is DBNull.Value Then Return ""
            Return CStr(v)
        End Function

        Private Function ExistsCached(path As String) As Boolean
            If String.IsNullOrWhiteSpace(path) Then Return False
            Dim ok As Boolean
            If _existsCache.TryGetValue(path, ok) Then Return ok
            Try
                ok = IO.File.Exists(path)
            Catch
                ok = False
            End Try
            _existsCache(path) = ok
            Return ok
        End Function

    End Class

End Class

'' Form_Load
'Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
'    ' 1) Preparar grids (uma vez)
'    ChapaFormatarDataGrid.PrepararGrid(DGVListaMaterialSW)
'    ChapaFormatarDataGrid.PrepararGrid(dgvos)

'    ' 2) (Opcional) definir colunas manualmente antes do DataSource
'    'DGVListaMaterialSW.AutoGenerateColumns = False
'    'DGVListaMaterialSW.Columns.Clear()
'    'DGVListaMaterialSW.Columns.Add(New DataGridViewTextBoxColumn() With {.Name="DescResumo", .HeaderText="Resumo", .Width=220})
'    ChapaFormatarDataGrid.GarantirColunaImagem(DGVListaMaterialSW, "DGVDXF", "DXF", 0)
'    ChapaFormatarDataGrid.GarantirColunaImagem(DGVListaMaterialSW, "DGVPDF", "PDF", 1)

'    ' 3) Ligar tratamento de DataError (evita pop-ups de binding)
'    ChapaFormatarDataGrid.LigarTratamentoDataError(DGVListaMaterialSW, dgvos)

'    ' 4) Binder para formatação sob demanda (ícones etc.)
'    _binder = ChapaFormatarDataGrid.CriarBinder(DGVListaMaterialSW)
'    _binder.ColunaCaminhoArquivo = "EnderecoArquivo"
'    _binder.ColunaDxf = "DGVDXF"
'    _binder.ColunaPdf = "DGVPDF"
'    _binder.IconeDxf = My.Resources.arquivo_dxf        ' injete seus ícones
'    _binder.IconePdf = My.Resources.ficheiro_pdf
'    _binder.IconeVazio = My.Resources.Sem_Incone
'    _binder.Bind()

'    ' 5) Atribuir o DataSource por último
'    'DGVListaMaterialSW.DataSource = suaListaOuBindingSource
'End Sub

'' Campo no Form para manter o binder vivo
'Private _binder As ChapaFormatarDataGrid.ChapaGridBinder