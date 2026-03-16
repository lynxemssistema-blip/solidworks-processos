Option Strict On

Imports System.Collections.Generic
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

' ==== Opções de configuração ====
Public Class DgvPerfOptions

    ' Estilo/Comportamento
    Public Property EnableDoubleBuffer As Boolean = True

    Public Property DefaultFont As Font = New Font("Segoe UI", 9.0F)
    Public Property HeaderFont As Font = New Font("Segoe UI Semibold", 9.0F)
    Public Property SelectionMode As DataGridViewSelectionMode = DataGridViewSelectionMode.FullRowSelect
    Public Property StartWithFixedWidths As Boolean = True   ' Desliga autosize agressivo

    ' Ação para sua formatação global (ex.: cl_BancoDados.FormatarDataGridView)
    Public Property OnApplyGlobalFormat As Action(Of DataGridView)

    ' Visibilidade / Freeze (opcional)
    Public Property ColumnsToHide As New List(Of String)

    Public Property ColumnsToFreeze As New List(Of String)
    Public Property ColumnsEnsureVisible As New List(Of String)

    ' Colunas de arquivo (DXF/PDF)
    Public Property FilePathColumnName As String            ' ex.: "EnderecoArquivo"

    Public Property DxfColumnName As String                 ' ex.: "DGVDXF"
    Public Property PdfColumnName As String                 ' ex.: "DGVPDF"
    Public Property AddImageColumnsIfMissing As Boolean = True
    Public Property DxfHeader As String = "DXF"
    Public Property PdfHeader As String = "PDF"
    Public Property DxfColumnIndex As Integer = 0
    Public Property PdfColumnIndex As Integer = 1

    ' Coluna de status (liberação engenharia)
    Public Property StatusColumnName As String              ' ex.: "dgvStatus"

    Public Property LiberadoEngenhariaColumnName As String  ' ex.: "Liberado_Engenharia"

    ' Imagens (ajuste para seus Resources se desejar)
    Public Property ImageOk As Image = My.Resources.verificado1

    Public Property ImageWarn As Image = My.Resources.atencao
    Public Property ImageDxf As Image = My.Resources.arquivo_dxf
    Public Property ImagePdf As Image = My.Resources.ficheiro_pdf
    Public Property ImageNone As Image = My.Resources.Sem_Incone

    ' Autosize “uma vez” pós-bind (opcional)
    Public Property DoOneTimeAutoSize As Boolean = False

End Class

' ==== Helper principal ====
Public Class DgvPerfHelper
    Implements IDisposable

    Private ReadOnly _grid As DataGridView
    Private ReadOnly _opt As DgvPerfOptions
    Private _configured As Boolean = False

    ' Cache File.Exists
    Private ReadOnly _existsCache As New Dictionary(Of String, Boolean)(StringComparer.OrdinalIgnoreCase)

    ' Win32: suspender desenho em grandes mudanças
    Private Const WM_SETREDRAW As Integer = &HB

    <DllImport("user32.dll")>
    Private Shared Function SendMessage(hWnd As IntPtr, msg As Integer, wParam As Boolean, lParam As Integer) As IntPtr
    End Function

    Public Sub New(grid As DataGridView, options As DgvPerfOptions)
        If grid Is Nothing Then Throw New ArgumentNullException(NameOf(grid))
        If options Is Nothing Then Throw New ArgumentNullException(NameOf(options))
        _grid = grid
        _opt = options

        ConfigureGridOnce()
        ' Assina eventos no grid informado
        AddHandler _grid.DataBindingComplete, AddressOf Grid_DataBindingComplete
        AddHandler _grid.CellFormatting, AddressOf Grid_CellFormatting
        AddHandler _grid.DataError, AddressOf Grid_DataError
    End Sub

    ' ===== API pública complementar =====

    Public Sub HideColumns(ParamArray names() As String)
        _opt.ColumnsToHide.AddRange(names)
    End Sub

    Public Sub FreezeColumns(ParamArray names() As String)
        _opt.ColumnsToFreeze.AddRange(names)
    End Sub

    Public Sub EnsureVisible(ParamArray names() As String)
        _opt.ColumnsEnsureVisible.AddRange(names)
    End Sub

    Public Sub ResetExistsCache()
        _existsCache.Clear()
    End Sub

    Public Sub SetVirtualMode(rowCount As Integer, onCellValueNeeded As Func(Of Integer, Integer, Object))
        _grid.VirtualMode = True
        _grid.RowCount = rowCount
        AddHandler _grid.CellValueNeeded,
            Sub(s, e)
                If onCellValueNeeded IsNot Nothing Then
                    e.Value = onCellValueNeeded(e.RowIndex, e.ColumnIndex)
                End If
            End Sub
    End Sub

    Public Sub SuspendPainting()
        SendMessage(_grid.Handle, WM_SETREDRAW, False, 0)
    End Sub

    Public Sub ResumePainting()
        SendMessage(_grid.Handle, WM_SETREDRAW, True, 0)
        _grid.Refresh()
    End Sub

    ' ===== Núcleo de configuração =====

    Private Sub ConfigureGridOnce()
        If _configured Then Return

        ' Comportamento base
        _grid.RowHeadersVisible = False
        _grid.AllowUserToAddRows = False
        _grid.AllowUserToDeleteRows = False
        _grid.SelectionMode = _opt.SelectionMode

        If _opt.StartWithFixedWidths Then
            _grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None
            _grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
        End If

        ' Estilos
        Dim baseCell As New DataGridViewCellStyle With {
            .Font = _opt.DefaultFont,
            .Alignment = DataGridViewContentAlignment.MiddleLeft,
            .WrapMode = DataGridViewTriState.False,
            .NullValue = "",
            .Format = ""
        }
        _grid.DefaultCellStyle = baseCell
        _grid.ColumnHeadersDefaultCellStyle = New DataGridViewCellStyle(baseCell) With {
            .Font = _opt.HeaderFont,
            .Alignment = DataGridViewContentAlignment.MiddleLeft,
            .WrapMode = DataGridViewTriState.False
        }

        ' DoubleBuffered via reflexão (não pública)
        If _opt.EnableDoubleBuffer Then
            Dim t = GetType(DataGridView)
            Dim pi = t.GetProperty("DoubleBuffered", Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic)
            pi?.SetValue(_grid, True, Nothing)
        End If

        _configured = True
    End Sub

    Private Sub EnsureImageColumn(colName As String, header As String, insertAt As Integer)
        If String.IsNullOrWhiteSpace(colName) Then Return
        Dim col As DataGridViewImageColumn = TryCast(_grid.Columns(colName), DataGridViewImageColumn)
        If col Is Nothing AndAlso _opt.AddImageColumnsIfMissing Then
            col = New DataGridViewImageColumn() With {
                .Name = colName,
                .HeaderText = header,
                .ImageLayout = DataGridViewImageCellLayout.Zoom,
                .Width = 28,
                .ReadOnly = True
            }
            Dim idx = Math.Max(0, Math.Min(insertAt, _grid.Columns.Count))
            _grid.Columns.Insert(idx, col)
        End If
    End Sub

    ' ===== Eventos =====

    Private Sub Grid_DataBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs)
        If _grid.Rows Is Nothing OrElse _grid.Rows.Count = 0 Then
            ' Ainda assim, garanta colunas de ícone caso esteja usando AutoGenerateColumns depois
            EnsureIconColumnsIfNeeded()
            Return
        End If

        ' 1) Garante colunas de imagem (DXF/PDF/Status) se configurado
        EnsureIconColumnsIfNeeded()

        ' 2) Formatação global custom (sua rotina externa)
        _opt.OnApplyGlobalFormat?.Invoke(_grid)

        ' 3) Visibilidade / Freeze
        For Each n In _opt.ColumnsToHide
            If _grid.Columns.Contains(n) Then _grid.Columns(n).Visible = False
        Next
        For Each n In _opt.ColumnsEnsureVisible
            If _grid.Columns.Contains(n) Then _grid.Columns(n).Visible = True
        Next
        For Each n In _opt.ColumnsToFreeze
            If _grid.Columns.Contains(n) Then _grid.Columns(n).Frozen = True
        Next

        ' 4) Autosize “uma vez”, com cuidado
        If _opt.DoOneTimeAutoSize Then AutoSizeOnce(_grid)
    End Sub

    Private Sub EnsureIconColumnsIfNeeded()
        If Not String.IsNullOrWhiteSpace(_opt.DxfColumnName) Then
            EnsureImageColumn(_opt.DxfColumnName, _opt.DxfHeader, _opt.DxfColumnIndex)
        End If
        If Not String.IsNullOrWhiteSpace(_opt.PdfColumnName) Then
            EnsureImageColumn(_opt.PdfColumnName, _opt.PdfHeader, _opt.PdfColumnIndex)
        End If
        If Not String.IsNullOrWhiteSpace(_opt.StatusColumnName) Then
            EnsureImageColumn(_opt.StatusColumnName, "Status", 0)
        End If
    End Sub

    Private Sub Grid_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs)
        If e.RowIndex < 0 Then Return
        Dim colName = _grid.Columns(e.ColumnIndex).Name

        ' --- Ícone de Status (Liberado_Engenharia) ---
        If Not String.IsNullOrWhiteSpace(_opt.StatusColumnName) AndAlso
           colName.Equals(_opt.StatusColumnName, StringComparison.OrdinalIgnoreCase) AndAlso
           Not String.IsNullOrWhiteSpace(_opt.LiberadoEngenhariaColumnName) AndAlso
           _grid.Columns.Contains(_opt.LiberadoEngenhariaColumnName) Then

            Dim v = _grid.Rows(e.RowIndex).Cells(_opt.LiberadoEngenhariaColumnName).Value
            Dim isOk As Boolean = False
            If TypeOf v Is Boolean Then
                isOk = CBool(v)
            Else
                Dim s = If(v Is Nothing OrElse v Is DBNull.Value, "", Convert.ToString(v))
                isOk = s.Equals("S", StringComparison.OrdinalIgnoreCase) OrElse s.Equals("SIM", StringComparison.OrdinalIgnoreCase) OrElse s.Equals("1")
            End If

            e.Value = If(isOk, _opt.ImageOk, _opt.ImageWarn)
            e.FormattingApplied = True
            Return
        End If

        ' --- Colunas DXF/PDF baseadas em EnderecoArquivo ---
        If Not String.IsNullOrWhiteSpace(_opt.FilePathColumnName) AndAlso
           _grid.Columns.Contains(_opt.FilePathColumnName) Then

            Dim enderecoObj = _grid.Rows(e.RowIndex).Cells(_opt.FilePathColumnName).Value
            Dim endereco = If(enderecoObj Is Nothing OrElse enderecoObj Is DBNull.Value, "", Convert.ToString(enderecoObj))

            If Not String.IsNullOrWhiteSpace(_opt.DxfColumnName) AndAlso
               colName.Equals(_opt.DxfColumnName, StringComparison.OrdinalIgnoreCase) Then

                e.Value = BuildFileIcon(endereco, ".dxf", _opt.ImageDxf, _opt.ImageNone)
                e.FormattingApplied = True
                Return
            End If

            If Not String.IsNullOrWhiteSpace(_opt.PdfColumnName) AndAlso
               colName.Equals(_opt.PdfColumnName, StringComparison.OrdinalIgnoreCase) Then

                e.Value = BuildFileIcon(endereco, ".pdf", _opt.ImagePdf, _opt.ImageNone)
                e.FormattingApplied = True
                Return
            End If
        End If
    End Sub

    Private Function BuildFileIcon(originalPath As String, newExt As String, okImg As Image, noneImg As Image) As Image
        If String.IsNullOrWhiteSpace(originalPath) Then Return noneImg
        Dim isSw = originalPath.EndsWith(".SLDPRT", StringComparison.OrdinalIgnoreCase) OrElse
                   originalPath.EndsWith(".SLDASM", StringComparison.OrdinalIgnoreCase)
        If Not isSw Then Return noneImg
        Dim candidate As String
        Try
            candidate = IO.Path.ChangeExtension(originalPath, newExt)
        Catch
            Return noneImg
        End Try
        Return If(ExistsCached(candidate), okImg, noneImg)
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

    Private Sub Grid_DataError(sender As Object, e As DataGridViewDataErrorEventArgs)
        ' Evita popups e mantém UI suave (logar se necessário)
        e.Cancel = True
    End Sub

    Private Sub AutoSizeOnce(g As DataGridView)
        ' Ajuste leve: mede e fixa as larguras atuais
        For Each c As DataGridViewColumn In g.Columns
            If Not c.Visible Then Continue For
            Dim current = c.AutoSizeMode
            c.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells
            Dim w = c.Width
            c.AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            c.Width = w
            c.AutoSizeMode = current
        Next
    End Sub

    ' ===== IDisposable =====
    Private _disposed As Boolean = False

    Public Sub Dispose() Implements IDisposable.Dispose
        If _disposed Then Return
        RemoveHandler _grid.DataBindingComplete, AddressOf Grid_DataBindingComplete
        RemoveHandler _grid.CellFormatting, AddressOf Grid_CellFormatting
        RemoveHandler _grid.DataError, AddressOf Grid_DataError
        _disposed = True
    End Sub

End Class

'''''''Como usar(no seu Form)
'''''''1) DGVListaMaterialSW (ícones DXF/PDF baseados em EnderecoArquivo)
'''''''' Campos no Form:
'''''''Private helperMaterial As DgvPerfHelper

'''''''Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
'''''''    Dim optMaterial As New DgvPerfOptions With {
'''''''        .OnApplyGlobalFormat = Sub(g) cl_BancoDados.FormatarDataGridView(g, "SIM"),
'''''''        .FilePathColumnName = "EnderecoArquivo",
'''''''        .DxfColumnName = "DGVDXF",
'''''''        .PdfColumnName = "DGVPDF",
'''''''        .DxfHeader = "DXF",
'''''''        .PdfHeader = "PDF",
'''''''        .DxfColumnIndex = 0,
'''''''        .PdfColumnIndex = 1,
'''''''        .DoOneTimeAutoSize = False
'''''''    }

'''''''    ' Exemplo de visibilidade
'''''''    optMaterial.ColumnsToHide.AddRange({
'''''''        "EnderecoArquivoItemOrdemServico", "EnderecoArquivo", "IdOrdemServico", "IdOrdemServicoItem",
'''''''        "Tag", "IdProjeto", "IdTag", "Liberado_Engenharia", "ProdutoPrincipal",
'''''''        "Data_Liberacao_Engenharia", "descempresa"
'''''''    })
'''''''    optMaterial.ColumnsEnsureVisible.AddRange({"DescResumo", "DescDetal", "QtdeTotal", "Acabamento", "Espessura", "CodMatFabricante", "Fator", "qtde"})

'''''''    helperMaterial = New DgvPerfHelper(DGVListaMaterialSW, optMaterial)

'''''''    ' Depois disso, atribua o DataSource normalmente
'''''''    ' DGVListaMaterialSW.DataSource = ...
'''''''End Sub

'''''''2) dgvos (coluna de status por “Liberado_Engenharia”)
'''''''Private helperOs As DgvPerfHelper

'''''''Private Sub Form1_Load_2(sender As Object, e As EventArgs) Handles MyBase.Load
'''''''    Dim optOs As New DgvPerfOptions With {
'''''''        .OnApplyGlobalFormat = Sub(g) cl_BancoDados.FormatarDataGridView(g, "SIM"),
'''''''        .StatusColumnName = "dgvStatus",
'''''''        .LiberadoEngenhariaColumnName = "Liberado_Engenharia",
'''''''        .DoOneTimeAutoSize = False
'''''''    }

'''''''    optOs.ColumnsToHide.AddRange({
'''''''        "ENDERECO", "idTag", "idProjeto", "Estatus", "DataPrevisao",
'''''''        "Data_Liberacao_Engenharia", "Liberado_Engenharia", "ProdutoPadrao"
'''''''    })
'''''''    optOs.ColumnsToFreeze.Add("idProjeto")

'''''''    helperOs = New DgvPerfHelper(dgvos, optOs)

'''''''    ' DataSource do dgvos aqui...
'''''''End Sub

'''''''Dica: faça o Dispose() dos helpers no FormClosed

'''''''Private Sub Form1_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
'''''''    helperMaterial?.Dispose()
'''''''    helperOs?.Dispose()
'''''''End Sub

'''''''Extras(se quiser usar VirtualMode)
'''''''' Suponha que você tenha um repositório/lista:
'''''''Private repo As IList(Of MeuRegistro)

'''''''Private Sub HabilitarVirtualNoMaterial()
'''''''    helperMaterial.SetVirtualMode(
'''''''        rowCount:=repo.Count,
'''''''        onCellValueNeeded:=Function(r, c)
'''''''                               Dim colName = DGVListaMaterialSW.Columns(c).Name
'''''''                               Dim item = repo(r)
'''''''                               ' retorne o valor certo para a coluna:
'''''''                               Select Case colName
'''''''                                   Case "DescResumo" : Return item.DescResumo
'''''''                                   Case "EnderecoArquivo" : Return item.EnderecoArquivo
'''''''                                       ' etc...
'''''''                               End Select
'''''''                               Return Nothing
'''''''                           End Function
'''''''    )
'''''''End Sub

'''''''Por que essa classe resolve

'''''''Desacoplada: não referencia DGVListaMaterialSW/dgvos por nome; você passa o grid no construtor.

'''''''Reutilizável: uma instância por grid, com DgvPerfOptions moldando o comportamento.

'''''''Performática: usa CellFormatting(on-demand), cacheia File.Exists, evita autosize agressivo, liga DoubleBuffered.

'''''''Organizada: tudo centralizado, com API para esconder/”congelar” colunas, autosize uma vez, suspender desenho.

'''''''Segura: trata DataError para evitar pop-ups e travamentos durante render.

'''''''Se quiser, adapto As imagens padrão das opções para bater 100% com os seus My.Resources atuais ou coloco fallbacks caso alguma não exista.