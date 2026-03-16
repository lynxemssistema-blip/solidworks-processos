Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports ClosedXML.Excel

Public Class ClExcel

    Public Function ExportarOrdemServicoPadrao(dgvGrid As DataGridView, ByVal BarraProgresso As ToolStripProgressBar, Endereco As String, ByVal DescricaoOs As String, ByVal dgvprincipal As DataGridView, ByVal dgvMaterial As DataGridView, Optional rp As Boolean = False) As Boolean

        Try

            BarraProgresso.Minimum = 0
            BarraProgresso.Value = 0
            BarraProgresso.Maximum = dgvGrid.RowCount

            '    Validar condições iniciais
            If dgvGrid Is Nothing OrElse dgvGrid.Rows.Count = 0 Then
                MsgBox("Não há itens a serem liberados para fabricação.", vbInformation, "Atenção")
                Return False
            End If

            If String.IsNullOrEmpty(My.Settings.EnderecoTemplateExcel) OrElse Not File.Exists(My.Settings.EnderecoTemplateExcel) Then
                MsgBox("A planilha template não foi encontrada. Por favor, configure o caminho corretamente.", vbCritical, "Erro")
                Return False
            End If

            Dim ObjetoExcel As Microsoft.Office.Interop.Excel.Application = Nothing
            Dim pasta1 As Microsoft.Office.Interop.Excel.Workbook = Nothing
            Dim planilha As Microsoft.Office.Interop.Excel.Worksheet = Nothing
            Dim planilhaMaterial As Microsoft.Office.Interop.Excel.Worksheet = Nothing

            Dim planilhaOmie As Microsoft.Office.Interop.Excel.Worksheet = Nothing

            'Inicializar o Excel
            ObjetoExcel = New Microsoft.Office.Interop.Excel.Application()
            pasta1 = ObjetoExcel.Workbooks.Open(My.Settings.EnderecoTemplateExcel)
            ' planilha = CType(pasta1.ActiveSheet, Microsoft.Office.Interop.Excel.Worksheet)

            planilha = CType(pasta1.Sheets("OrdemServico"), Microsoft.Office.Interop.Excel.Worksheet)
            planilhaMaterial = CType(pasta1.Sheets("Material"), Microsoft.Office.Interop.Excel.Worksheet)

            If My.Settings.ProgramaRM = "#omie" Then

                planilhaOmie = CType(pasta1.Sheets("Omie_Produtos_Estrutura"), Microsoft.Office.Interop.Excel.Worksheet)

            End If

            '            planilha = pasta1.ActiveSheet

            Try
                Dim NovoIdOrdemServicoDB As Integer = Convert.ToInt32(dgvprincipal.CurrentRow.Cells("IdOrdemServico").Value.ToString)

                NovoIdOrdemServico = cl_BancoDados.FormatarPara5Caracteres(NovoIdOrdemServicoDB.ToString())
            Catch ex As Exception

                ' Em caso de erro, atribuir "00001" como valor inicial
                NovoIdOrdemServico = "00001" 'Nothing

            End Try

            NovoIdOrdemServico = cl_BancoDados.FormatarPara5Caracteres(NovoIdOrdemServico.ToString)

            'cabeçalho DA LISTA DE PEÇAS
            planilha.Range("W1").Value = NovoIdOrdemServico.ToString
            planilha.Range("D8").Value = dgvprincipal.CurrentRow.Cells("Projeto").Value.ToString & " - " & dgvprincipal.CurrentRow.Cells("DescEmpresa").Value.ToString
            planilha.Range("D9").Value = dgvprincipal.CurrentRow.Cells("Tag").Value.ToString.Trim.ToUpper
            planilha.Range("T8").Value = dgvprincipal.CurrentRow.Cells("Descricao").Value.ToString.Trim.ToUpper
            planilha.Range("D10").Value = dgvprincipal.CurrentRow.Cells("ENDERECO").Value.ToString.Trim.ToUpper

            planilha.Range("D13").Value = dgvprincipal.CurrentRow.Cells("CriadoPor").Value.ToString.Trim.ToUpper
            planilha.Range("D14").Value = dgvprincipal.CurrentRow.Cells("DataCriacao").Value.ToString.Trim.ToUpper

            'cabeçalho DA LISTA DE MATERIAIS
            planilhaMaterial.Range("P1").Value = NovoIdOrdemServico.ToString
            planilhaMaterial.Range("D8").Value = dgvprincipal.CurrentRow.Cells("Projeto").Value.ToString & " - " & dgvprincipal.CurrentRow.Cells("DescEmpresa").Value.ToString
            planilhaMaterial.Range("D9").Value = dgvprincipal.CurrentRow.Cells("Tag").Value.ToString.Trim.ToUpper
            planilhaMaterial.Range("M8").Value = dgvprincipal.CurrentRow.Cells("Descricao").Value.ToString.Trim.ToUpper
            planilhaMaterial.Range("D10").Value = dgvprincipal.CurrentRow.Cells("ENDERECO").Value.ToString.Trim.ToUpper

            planilhaMaterial.Range("D13").Value = dgvprincipal.CurrentRow.Cells("CriadoPor").Value.ToString.Trim.ToUpper
            planilhaMaterial.Range("D14").Value = dgvprincipal.CurrentRow.Cells("DataCriacao").Value.ToString.Trim.ToUpper

            'Percorre o grid procurando o item selecionado
            For I As Integer = 0 To dgvGrid.Rows.Count - 1

                Try

                    planilha.Range("A18:W18").Copy(planilha.Range("A" & 19 + I & ":W" & 19 + I))
                    planilha.Range("A" & I + 19).Value = dgvGrid.Rows(I).Cells("IDOrdemServicoItem").Value.ToString.Trim.ToUpper
                    planilha.Range("B" & I + 19).Value = dgvGrid.Rows(I).Cells("CodMatFabricante").Value.ToString.Trim.ToUpper
                    planilha.Range("I" & I + 19).Value = dgvGrid.Rows(I).Cells("QtdeTotal").Value.ToString.Trim.ToUpper
                    planilha.Range("J" & I + 19).Value = dgvGrid.Rows(I).Cells("MaterialSW").Value.ToString.Trim.ToUpper
                    planilha.Range("K" & I + 19).Value = dgvGrid.Rows(I).Cells("Unidade").Value.ToString.Trim.ToUpper
                    planilha.Range("L" & I + 19).Value = dgvGrid.Rows(I).Cells("Espessura").Value.ToString.Trim.ToUpper
                    planilha.Range("M" & I + 19).Value = dgvGrid.Rows(I).Cells("Altura").Value.ToString.Trim.ToUpper
                    planilha.Range("N" & I + 19).Value = dgvGrid.Rows(I).Cells("Largura").Value.ToString.Trim.ToUpper
                    planilha.Range("O" & I + 19).Value = dgvGrid.Rows(I).Cells("txtItemEstoque").Value.ToString.Trim.ToUpper
                    planilha.Range("P" & I + 19).Value = dgvGrid.Rows(I).Cells("DescResumo").Value.ToString.Trim.ToUpper
                    planilha.Range("S" & I + 19).Value = dgvGrid.Rows(I).Cells("DescDetal").Value.ToString.Trim.ToUpper
                    planilha.Range("V" & I + 19).Value = dgvGrid.Rows(I).Cells("Acabamento").Value.ToString.Trim.ToUpper
                    planilha.Range("W" & I + 19).Value = dgvGrid.Rows(I).Cells("txtTipoDesenho").Value.ToString.Trim.ToUpper

                    BarraProgresso.Value = I
                Catch ex As Exception

                    Continue For

                End Try

            Next

            planilha.Range("A18:W18").Delete()

            BarraProgresso.Minimum = 0
            BarraProgresso.Value = 0
            BarraProgresso.Maximum = dgvMaterial.RowCount

            If My.Settings.ProgramaRM = "#omie" Or My.Settings.BancoDadosAtivo = "lynxlocal" Then

                '   If rp = True Then
                'Percorre o grid procurando o item selecionado MATERIAL
                For I As Integer = 0 To dgvMaterial.Rows.Count - 1

                    Try

                        planilhaMaterial.Range("A18:P18").Copy(planilhaMaterial.Range("A" & 19 + I & ":P" & 19 + I))
                        planilhaMaterial.Range("A" & I + 19).Value = dgvMaterial.Rows(I).Cells("CodMatFabricante").Value.ToString.Trim.ToUpper
                        planilhaMaterial.Range("D" & I + 19).Value = dgvMaterial.Rows(I).Cells("NumeroRP").Value.ToString.Trim.ToUpper
                        planilhaMaterial.Range("G" & I + 19).Value = dgvMaterial.Rows(I).Cells("DescResumo").Value.ToString.Trim.ToUpper & vbCrLf & dgvMaterial.Rows(I).Cells("DescDetal").Value.ToString.Trim.ToUpper
                        planilhaMaterial.Range("N" & I + 19).Value = dgvMaterial.Rows(I).Cells("QtdeTotal").Value.ToString.Trim.ToUpper
                        planilhaMaterial.Range("O" & I + 19).Value = dgvMaterial.Rows(I).Cells("Unidade").Value.ToString.Trim.ToUpper
                        planilhaMaterial.Range("P" & I + 19).Value = dgvMaterial.Rows(I).Cells("Peso").Value.ToString.Trim.ToUpper

                        BarraProgresso.Value = I

                    Catch ex As Exception

                        Continue For

                    End Try

                Next

                ExportarOMIEOrdemProducao(Endereco, BarraProgresso, dgvMaterial, ProdutoPrincipal)


            End If

            planilhaMaterial.Range("A18:P18").Delete()

            Dim NomeArquivo As String = Endereco & "\OS_" & NovoIdOrdemServico & ".xlsx"

            Dim contador As Integer = 1

            ' Verifica se o arquivo já existe
            While System.IO.File.Exists(NomeArquivo)
                ' Se o arquivo já existir, adiciona um sufixo numérico
                NomeArquivo = Endereco & "\OS_" & NovoIdOrdemServico & "_" & contador & ".xlsx"
                contador += 1
            End While

            pasta1.SaveCopyAs(NomeArquivo)
            pasta1.Close(False)
            ObjetoExcel.Application.Visible = False

            Process.Start("Explorer", Endereco.ToString)
            BarraProgresso.Value = 0

            MsgBox("Dados exportados com Sucesso!!!", vbInformation, "Atenção!!!!")

        Catch ex As Exception

        Finally

        End Try

    End Function

    Public Function ExportarOMIEOrdemProducao(Endereco As String,
                                              BarraProgresso As ToolStripProgressBar,
                                              ByVal dgvMaterial As DataGridView,
                                              ProdutoPrincipal As String) As Boolean

        Try

            BarraProgresso.Minimum = 0
            BarraProgresso.Value = 0
            BarraProgresso.Maximum = dgvMaterial.RowCount

            '    Validar condições iniciais
            If dgvMaterial Is Nothing OrElse dgvMaterial.Rows.Count = 0 Then
                MsgBox("Não há itens a serem liberados para fabricação.", vbInformation, "Atenção")
                Return False
            End If

            If String.IsNullOrEmpty(My.Settings.PlanilhaModeloOmie) OrElse Not File.Exists(My.Settings.PlanilhaModeloOmie) Then
                MsgBox("A planilha template não foi encontrada. Por favor, configure o caminho corretamente.", vbCritical, "Erro")
                Return False
            End If

            Dim ObjetoExcel As Microsoft.Office.Interop.Excel.Application = Nothing
            Dim pasta1 As Microsoft.Office.Interop.Excel.Workbook = Nothing
            Dim planilhaOmie As Microsoft.Office.Interop.Excel.Worksheet = Nothing

            'Inicializar o Excel
            ObjetoExcel = New Microsoft.Office.Interop.Excel.Application()
            pasta1 = ObjetoExcel.Workbooks.Open(My.Settings.PlanilhaModeloOmie)
            planilhaOmie = CType(pasta1.Sheets("Omie_Produtos_Estrutura"), Microsoft.Office.Interop.Excel.Worksheet)

            'Try
            '    Dim NovoIdOrdemServicoDB As Integer = Convert.ToInt32(dgvMaterial.CurrentRow.Cells("IdOrdemServico").Value.ToString)

            '    NovoIdOrdemServico = cl_BancoDados.FormatarPara5Caracteres(NovoIdOrdemServicoDB.ToString())
            'Catch ex As Exception

            '    ' Em caso de erro, atribuir "00001" como valor inicial
            '    NovoIdOrdemServico = "00001" 'Nothing

            'End Try

            'NovoIdOrdemServico = cl_BancoDados.FormatarPara5Caracteres(NovoIdOrdemServico.ToString)

            BarraProgresso.Minimum = 0
            BarraProgresso.Value = 0
            BarraProgresso.Maximum = dgvMaterial.RowCount

            '   If rp = True Then
            'Percorre o grid procurando o item selecionado MATERIAL
            For I As Integer = 0 To dgvMaterial.Rows.Count - 1

                Try

                    planilhaOmie.Range("B" & I + 6).Value = ProdutoPrincipal.ToString
                    planilhaOmie.Range("C" & I + 6).Value = dgvMaterial.Rows(I).Cells("NumeroRP").Value.ToString.Trim.ToUpper
                    planilhaOmie.Range("D" & I + 6).Value = dgvMaterial.Rows(I).Cells("QtdeTotal").Value.ToString.Trim.ToUpper()
                    planilhaOmie.Range("E" & I + 6).Value = "PADRAO - Produção"

                    BarraProgresso.Value = I

                Catch ex As Exception

                    Continue For

                End Try

            Next

            ' End If

            '  End If

            Dim NomeArquivo As String = Endereco & "\OS_" & NovoIdOrdemServico & "_Omie_Produtos_v1_9_5.xlsx"

            Dim contador As Integer = 1

            ' Verifica se o arquivo já existe
            While System.IO.File.Exists(NomeArquivo)
                ' Se o arquivo já existir, adiciona um sufixo numérico
                NomeArquivo = Endereco & "\OS_" & NovoIdOrdemServico & "_Omie_Produtos_v1_9_5_" & contador & ".xlsx"
                contador += 1
            End While

            pasta1.SaveCopyAs(NomeArquivo)
            pasta1.Close(False)
            ObjetoExcel.Application.Visible = False

            Process.Start("Explorer", Endereco.ToString)
            BarraProgresso.Value = 0

            MsgBox("Dados exportados com Sucesso!!!", vbInformation, "Atenção!!!!")

        Catch ex As Exception

        Finally

        End Try

    End Function

    Public Function ExportarListaMaterial(ByVal dgvGrid As DataGridView, ByVal BarraProgresso As ProgressBar, Endereco As String, ByVal DescricaoOs As String, ByVal dgvprincipal As DataGridView, ByVal dgvMaterial As DataGridView) As Boolean

        Try

            BarraProgresso.Minimum = 0
            BarraProgresso.Value = 0
            BarraProgresso.Maximum = dgvGrid.Rows.Count - 1

            Dim ObjetoExcel As Microsoft.Office.Interop.Excel.Application

            Dim pasta1 As Microsoft.Office.Interop.Excel.Workbook

            Dim planilha As Microsoft.Office.Interop.Excel.Worksheet

            'Dim linhaOriginal As Microsoft.Office.Interop.Excel.Range

            'Dim linhaDestino As Microsoft.Office.Interop.Excel.Range

            ObjetoExcel = CreateObject("Excel.application")

            Try
                pasta1 = ObjetoExcel.Workbooks.Open(My.Settings.TemplatAlmoxarifadorev01)
            Catch ex As Exception
                MsgBox("A planilha não foi encontrada, favor indicar o caminho correto nas configurações")
                Exit Function
            End Try

            planilha = pasta1.ActiveSheet

            'cabeçalho
            planilha.Range("C8").Value = Date.Now.ToLongDateString
            '  planilha.Range("C9").Value = ClasseUsuario.NomeCompleto

            'Percorre o grid procurando o item selecionado
            For I As Integer = 0 To dgvGrid.Rows.Count - 1

                Try

                    planilha.Range("A13:N13").Copy(planilha.Range("A" & 14 + I & ":N" & 14 + I))

                    planilha.Range("A" & I + 14).Value = dgvGrid.Rows(I).Cells("OS").Value
                    planilha.Range("B" & I + 19).Value = dgvGrid.Rows(I).Cells("Projeto").Value
                    planilha.Range("C" & I + 19).Value = dgvGrid.Rows(I).Cells("Tag").Value
                    planilha.Range("D" & I + 19).Value = dgvGrid.Rows(I).Cells("DescDetal").Value
                    planilha.Range("K" & I + 19).Value = dgvGrid.Rows(I).Cells("DescResumo").Value
                    planilha.Range("L" & I + 19).Value = dgvGrid.Rows(I).Cells("Codigo").Value
                    planilha.Range("M" & I + 19).Value = dgvGrid.Rows(I).Cells("Unidade").Value
                    planilha.Range("N" & I + 19).Value = dgvGrid.Rows(I).Cells("qtde").Value

                    BarraProgresso.Value = I
                Catch ex As Exception

                    Continue For

                End Try
            Next

            planilha.Range("A13:N13").Delete()

            ObjetoExcel.Application.Visible = True

            Process.Start("Explorer", Endereco.ToString)
            BarraProgresso.Value = 0

            MsgBox("Dados exportados com Sucesso!!!", vbInformation, "Atenção!!!!")
        Catch ex As Exception
        Finally
        End Try

    End Function

End Class

Public Class clPadraoMetta

    Public Function ExportarOrdemServicoPadraoMettaAntigo(ByVal dgvGrid As DataGridView, ByVal BarraProgresso As ToolStripProgressBar, Endereco As String, ByVal DescricaoOs As String, ByVal dgvPrincipal As DataGridView, ByVal TipoExcel As Boolean, ByVal dgvMaterial As DataGridView) As Boolean

        Try

            BarraProgresso.Minimum = 0
            BarraProgresso.Value = 0
            BarraProgresso.Maximum = dgvGrid.RowCount + dgvMaterial.RowCount

            '    Validar condições iniciais
            If dgvGrid Is Nothing OrElse dgvGrid.Rows.Count = 0 Then
                MsgBox("Não há itens a serem liberados para fabricação.", vbInformation, "Atenção")
                Return False
            End If

            If String.IsNullOrEmpty(My.Settings.EnderecoTemplateExcel) OrElse Not File.Exists(My.Settings.EnderecoTemplateExcel) Then
                MsgBox("A planilha template não foi encontrada. Por favor, configure o caminho corretamente.", vbCritical, "Erro")
                Return False
            End If

            Dim ObjetoExcel As Microsoft.Office.Interop.Excel.Application = Nothing
            Dim pasta1 As Microsoft.Office.Interop.Excel.Workbook = Nothing
            Dim planilha As Microsoft.Office.Interop.Excel.Worksheet = Nothing

            'Inicializar o Excel
            ObjetoExcel = New Microsoft.Office.Interop.Excel.Application()
            pasta1 = ObjetoExcel.Workbooks.Open(My.Settings.EnderecoTemplateExcel)
            planilha = CType(pasta1.ActiveSheet, Microsoft.Office.Interop.Excel.Worksheet)

            planilha = pasta1.ActiveSheet

            Try
                Dim NovoIdOrdemServicoDB As Integer = Convert.ToInt32(dgvPrincipal.CurrentRow.Cells("IdOrdemServico").Value.ToString)

                NovoIdOrdemServico = cl_BancoDados.FormatarPara5Caracteres(NovoIdOrdemServicoDB.ToString())
            Catch ex As Exception

                ' Em caso de erro, atribuir "00001" como valor inicial
                NovoIdOrdemServico = "00001" ' Nothing

            End Try

            'cabeçalho
            planilha.Range("R2").Value = NovoIdOrdemServico.ToString
            planilha.Range("D10").Value = dgvPrincipal.CurrentRow.Cells("Projeto").Value.ToString & " - " & dgvPrincipal.CurrentRow.Cells("DescEmpresa").Value.ToString
            planilha.Range("D11").Value = dgvPrincipal.CurrentRow.Cells("Tag").Value.ToString
            planilha.Range("N10").Value = dgvPrincipal.CurrentRow.Cells("Descricao").Value.ToString
            planilha.Range("D12").Value = dgvPrincipal.CurrentRow.Cells("ENDERECO").Value.ToString

            planilha.Range("D16").Value = dgvPrincipal.CurrentRow.Cells("CriadoPor").Value.ToString
            planilha.Range("D17").Value = dgvPrincipal.CurrentRow.Cells("DataCriacao").Value.ToString

            If TipoExcel = True Then

                planilha.Range("N16").Value = dgvPrincipal.CurrentRow.Cells("Data_Liberacao_Engenharia").Value.ToString

            End If

            planilha.Range("Q16").Value = My.Computer.Name.ToString.ToUpper
            planilha.Range("Q17").Value = Date.Now.ToShortDateString

            'Percorre o grid procurando o item selecionado
            For I As Integer = 0 To dgvGrid.Rows.Count - 1

                Try

                    planilha.Range("A22:U22").Copy(planilha.Range("A" & 23 + I & ":U" & 23 + I))

                    planilha.Range("A" & I + 23).Value = dgvGrid.Rows(I).Cells("IDOrdemServicoItem").Value
                    planilha.Range("B" & I + 23).Value = dgvGrid.Rows(I).Cells("DescResumo").Value
                    planilha.Range("I" & I + 23).Value = dgvGrid.Rows(I).Cells("DescDetal").Value
                    planilha.Range("L" & I + 23).Value = dgvGrid.Rows(I).Cells("CodMatFabricante").Value
                    planilha.Range("N" & I + 23).Value = dgvGrid.Rows(I).Cells("QtdeTotal").Value
                    planilha.Range("O" & I + 23).Value = dgvGrid.Rows(I).Cells("MaterialSW").Value
                    planilha.Range("P" & I + 23).Value = dgvGrid.Rows(I).Cells("Acabamento").Value
                    planilha.Range("Q" & I + 23).Value = dgvGrid.Rows(I).Cells("txtTipoDesenho").Value
                    planilha.Range("R" & I + 23).Value = dgvGrid.Rows(I).Cells("Espessura").Value
                    planilha.Range("S" & I + 23).Value = dgvGrid.Rows(I).Cells("Altura").Value
                    planilha.Range("T" & I + 23).Value = dgvGrid.Rows(I).Cells("Largura").Value
                    planilha.Range("U" & I + 23).Value = dgvGrid.Rows(I).Cells("txtItemEstoque").Value

                    BarraProgresso.Value = I
                Catch ex As Exception

                    Continue For

                End Try

            Next

            'AJUSTE NA LISTA DE MATERIAL TIRANDO A DUPLICIDADE
            ''''Dim InicioMaterial As Integer = dgvGrid.RowCount + 1

            ''''For m As Integer = 0 To dgvMaterial.RowCount - 1

            ''''    Try

            ''''        If dgvMaterial.Rows(m).Cells("txtTipoDesenho").Value = "MATERIAL" Then

            ''''            planilha.Range("A22:u22").Copy(planilha.Range("A" & 23 + InicioMaterial + m & ":U" & 23 + InicioMaterial + m))

            ''''            'planilha.Range("A" & InicioMaterial + m + 23).Value = dgvMaterial.Rows(m).Cells("IDOrdemServicoItem").Value
            ''''            planilha.Range("B" & InicioMaterial + m + 23).Value = dgvMaterial.Rows(m).Cells("DescResumo").Value
            ''''            planilha.Range("I" & InicioMaterial + m + 23).Value = dgvMaterial.Rows(m).Cells("DescDetal").Value
            ''''            planilha.Range("L" & InicioMaterial + m + 23).Value = dgvMaterial.Rows(m).Cells("CodMatFabricante").Value
            ''''            planilha.Range("N" & InicioMaterial + m + 23).Value = dgvMaterial.Rows(m).Cells("QtdeTotal").Value
            ''''            'planilha.Range("O" & InicioMaterial + m + 23).Value = dgvMaterial.Rows(m).Cells("MaterialSW").Value
            ''''            planilha.Range("P" & InicioMaterial + m + 23).Value = dgvMaterial.Rows(m).Cells("Unidade").Value
            ''''            planilha.Range("Q" & InicioMaterial + m + 23).Value = "material" 'dgvMaterial.Rows(m).Cells("TipoDesenho").Value
            ''''            'planilha.Range("R" & InicioMaterial + m + 23).Value = dgvMaterial.Rows(m).Cells("Espessura").Value
            ''''            'planilha.Range("S" & InicioMaterial + m + 23).Value = dgvMaterial.Rows(m).Cells("Altura").Value
            ''''            'planilha.Range("T" & InicioMaterial + m + 23).Value = dgvMaterial.Rows(m).Cells("Largura").Value
            ''''            'planilha.Range("U" & InicioMaterial + m + 23).Value = dgvMaterial.Rows(m).Cells("ItemEstoque").Value

            ''''        End If

            ''''        BarraProgresso.Value = m
            ''''    Catch ex As Exception

            ''''        Continue For

            ''''    End Try

            ''''Next

            ' Deleta a linha A18:S18
            planilha.Range("A22:U22").Delete()

            pasta1.SaveCopyAs(Endereco & "\OS_" & NovoIdOrdemServico & ".xlsx")
            pasta1.Close(False)
            ObjetoExcel.Application.Visible = False

            'ObjetoExcel.Application.Visible = True

            Process.Start("Explorer", Endereco.ToString)
            BarraProgresso.Value = 0

            MsgBox("Dados exportados com Sucesso!!!", vbInformation, "Atenção!!!!")
        Catch ex As Exception

            Try

                Dim excelFilePath As String = My.Settings.EnderecoTemplateExcel

                ' Abrindo o arquivo Excel
                Using wb As New XLWorkbook(excelFilePath)
                    Dim ws As IXLWorksheet = wb.Worksheet(1)

                    '    ' Escrever na célula A1
                    '    ws.Cell("A1").Value = "Novo valor"

                    '' Salvar o arquivo
                    'wb.Save()

                    BarraProgresso.Minimum = 0
                    BarraProgresso.Value = 0
                    BarraProgresso.Maximum = dgvGrid.RowCount + dgvMaterial.RowCount

                    '    Validar condições iniciais
                    If dgvGrid Is Nothing OrElse dgvGrid.Rows.Count = 0 Then
                        MsgBox("Não há itens a serem liberados para fabricação.", vbInformation, "Atenção")
                        Return False
                    End If

                    If String.IsNullOrEmpty(My.Settings.EnderecoTemplateExcel) OrElse Not File.Exists(My.Settings.EnderecoTemplateExcel) Then
                        MsgBox("A planilha template não foi encontrada. Por favor, configure o caminho corretamente.", vbCritical, "Erro")
                        Return False
                    End If

                    Try
                        Dim NovoIdOrdemServicoDB As Integer = Convert.ToInt32(dgvPrincipal.CurrentRow.Cells("IdOrdemServico").Value.ToString)

                        NovoIdOrdemServico = cl_BancoDados.FormatarPara5Caracteres(NovoIdOrdemServicoDB.ToString())
                    Catch ex1 As Exception

                        ' Em caso de erro, atribuir "00001" como valor inicial
                        NovoIdOrdemServico = "00001" ' Nothing

                    End Try

                    'cabeçalho
                    wb.Range("R2").Value = NovoIdOrdemServico.ToString
                    wb.Range("D10").Value = dgvPrincipal.CurrentRow.Cells("Projeto").Value.ToString & " - " & dgvPrincipal.CurrentRow.Cells("DescEmpresa").Value.ToString
                    wb.Range("D11").Value = dgvPrincipal.CurrentRow.Cells("Tag").Value.ToString
                    wb.Range("N10").Value = dgvPrincipal.CurrentRow.Cells("Descricao").Value.ToString
                    wb.Range("D12").Value = dgvPrincipal.CurrentRow.Cells("ENDERECO").Value.ToString

                    wb.Range("D16").Value = dgvPrincipal.CurrentRow.Cells("CriadoPor").Value.ToString
                    wb.Range("D17").Value = dgvPrincipal.CurrentRow.Cells("DataCriacao").Value.ToString

                    If TipoExcel = True Then

                        wb.Range("N16").Value = dgvPrincipal.CurrentRow.Cells("Data_Liberacao_Engenharia").Value.ToString

                    End If

                    wb.Range("Q16").Value = My.Computer.Name.ToString.ToUpper
                    wb.Range("Q17").Value = Date.Now.ToShortDateString

                    'Percorre o grid procurando o item selecionado
                    For I As Integer = 0 To dgvGrid.Rows.Count - 1

                        Try

                            wb.Range("A22:U22").CopyTo(wb.Range("A" & 23 + I & ":U" & 23 + I))

                            wb.Range("A" & I + 23).Value = dgvGrid.Rows(I).Cells("IDOrdemServicoItem").Value
                            wb.Range("B" & I + 23).Value = dgvGrid.Rows(I).Cells("DescResumo").Value
                            wb.Range("I" & I + 23).Value = dgvGrid.Rows(I).Cells("DescDetal").Value
                            wb.Range("L" & I + 23).Value = dgvGrid.Rows(I).Cells("CodMatFabricante").Value
                            wb.Range("N" & I + 23).Value = dgvGrid.Rows(I).Cells("QtdeTotal").Value
                            wb.Range("O" & I + 23).Value = dgvGrid.Rows(I).Cells("MaterialSW").Value
                            wb.Range("P" & I + 23).Value = dgvGrid.Rows(I).Cells("Acabamento").Value
                            wb.Range("Q" & I + 23).Value = dgvGrid.Rows(I).Cells("txtTipoDesenho").Value
                            wb.Range("R" & I + 23).Value = dgvGrid.Rows(I).Cells("Espessura").Value
                            wb.Range("S" & I + 23).Value = dgvGrid.Rows(I).Cells("Altura").Value
                            wb.Range("T" & I + 23).Value = dgvGrid.Rows(I).Cells("Largura").Value
                            wb.Range("U" & I + 23).Value = dgvGrid.Rows(I).Cells("txtItemEstoque").Value

                            BarraProgresso.Value = I
                        Catch ex2 As Exception

                            Continue For

                        End Try

                    Next

                    Dim InicioMaterial As Integer = dgvGrid.RowCount + 1

                    For m As Integer = 0 To dgvMaterial.RowCount - 1

                        Try

                            wb.Range("A22:u22").CopyTo(wb.Range("A" & 23 + InicioMaterial + m & ":U" & 23 + InicioMaterial + m))

                            'planilha.Range("A" & InicioMaterial + m + 23).Value = dgvMaterial.Rows(m).Cells("IDOrdemServicoItem").Value
                            wb.Range("B" & InicioMaterial + m + 23).Value = dgvMaterial.Rows(m).Cells("DescResumo").Value
                            wb.Range("I" & InicioMaterial + m + 23).Value = dgvMaterial.Rows(m).Cells("DescDetal").Value
                            wb.Range("L" & InicioMaterial + m + 23).Value = dgvMaterial.Rows(m).Cells("CodMatFabricante").Value
                            wb.Range("N" & InicioMaterial + m + 23).Value = dgvMaterial.Rows(m).Cells("QtdeTotal").Value
                            'planilha.Range("O" & InicioMaterial + m + 23).Value = dgvMaterial.Rows(m).Cells("MaterialSW").Value
                            wb.Range("P" & InicioMaterial + m + 23).Value = dgvMaterial.Rows(m).Cells("Unidade").Value
                            wb.Range("Q" & InicioMaterial + m + 23).Value = "material" 'dgvMaterial.Rows(m).Cells("TipoDesenho").Value
                            'planilha.Range("R" & InicioMaterial + m + 23).Value = dgvMaterial.Rows(m).Cells("Espessura").Value
                            'planilha.Range("S" & InicioMaterial + m + 23).Value = dgvMaterial.Rows(m).Cells("Altura").Value
                            'planilha.Range("T" & InicioMaterial + m + 23).Value = dgvMaterial.Rows(m).Cells("Largura").Value
                            'planilha.Range("U" & InicioMaterial + m + 23).Value = dgvMaterial.Rows(m).Cells("ItemEstoque").Value

                            BarraProgresso.Value = m
                        Catch ex3 As Exception

                            Continue For

                        End Try

                    Next

                    ' Deleta a linha A18:S18
                    wb.Range("A22:U22").Delete("A22:U22")

                    wb.SaveAs(Endereco & "\OS_" & NovoIdOrdemServico & ".xlsx")
                    wb.Dispose() ' .Close(False)
                    ' ObjetoExcel.Application.Visible = False

                    'ObjetoExcel.Application.Visible = True

                    Process.Start("Explorer", Endereco.ToString)
                    BarraProgresso.Value = 0

                    MsgBox("Dados exportados com Sucesso!!!", vbInformation, "Atenção!!!!")

                End Using
            Catch ex5 As Exception

                MsgBox(ex5.Message)

            End Try
        Finally

        End Try

    End Function

    Public Function ExportarOrdemServicoPadraoMetta(
    ByVal dgvGrid As DataGridView,
    ByVal BarraProgresso As ProgressBar,
    ByVal Endereco As String,
    ByVal DescricaoOs As String,
    ByVal dgvPrincipal As DataGridView,
    ByVal TipoExcel As Boolean,
    ByVal dgvMaterial As DataGridView
) As Boolean

        ' Validar condições iniciais
        If Not ValidarEntrada(dgvGrid, dgvMaterial) Then
            Return False
        End If

        Dim ObjetoExcel As Microsoft.Office.Interop.Excel.Application = Nothing
        Dim pasta1 As Microsoft.Office.Interop.Excel.Workbook = Nothing
        Dim planilha As Microsoft.Office.Interop.Excel.Worksheet = Nothing

        Try
            ' Inicializar Excel e abrir template
            ObjetoExcel = New Microsoft.Office.Interop.Excel.Application()
            pasta1 = ObjetoExcel.Workbooks.Open(My.Settings.EnderecoTemplateExcel)
            planilha = CType(pasta1.ActiveSheet, Microsoft.Office.Interop.Excel.Worksheet)

            ' Configurar barra de progresso
            ConfigurarBarraProgresso(BarraProgresso, dgvGrid.Rows.Count, dgvMaterial.Rows.Count)

            ' Obter ID da Ordem de Serviço
            Dim NovoIdOrdemServico As String = ObterIdOrdemServico(dgvGrid)

            ' Preencher cabeçalho
            PreencherCabecalho(planilha, dgvPrincipal, NovoIdOrdemServico, TipoExcel)

            ' Preencher dados do Grid
            PreencherGrid(planilha, dgvGrid, 23, BarraProgresso)

            ' Preencher dados de material
            Dim linhaInicialMaterial = dgvGrid.Rows.Count + 23
            PreencherGrid(planilha, dgvMaterial, linhaInicialMaterial, BarraProgresso, "material")

            ' Remover linha modelo
            planilha.Range("A22:U22").Delete()

            ' Salvar planilha
            Dim caminhoArquivo = Path.Combine(Endereco, $"OS_{NovoIdOrdemServico}.xlsx")
            pasta1.SaveCopyAs(caminhoArquivo)
            pasta1.Close(False)

            ' Abrir pasta destino
            Process.Start("Explorer", Endereco)

            ' Resetar progresso e mostrar mensagem de sucesso
            BarraProgresso.Value = 0
            MsgBox("Dados exportados com sucesso!", vbInformation, "Sucesso")

            Return True
        Catch ex As Exception
            MsgBox("Erro durante a exportação: " & ex.Message, vbCritical, "Erro")
            Return False
        Finally
            ' Liberar recursos COM
            If pasta1 IsNot Nothing Then Marshal.ReleaseComObject(pasta1)
            If planilha IsNot Nothing Then Marshal.ReleaseComObject(planilha)
            If ObjetoExcel IsNot Nothing Then
                ObjetoExcel.Quit()
                Marshal.ReleaseComObject(ObjetoExcel)
            End If

            GC.Collect()
            GC.WaitForPendingFinalizers()
        End Try
    End Function

    ' Método para validar entrada
    Private Function ValidarEntrada(dgvGrid As DataGridView, dgvMaterial As DataGridView) As Boolean
        If dgvGrid Is Nothing OrElse dgvGrid.Rows.Count = 0 Then
            MsgBox("Não há itens a serem exportados.", vbInformation, "Atenção")
            Return False
        End If
        If String.IsNullOrEmpty(My.Settings.EnderecoTemplateExcel) OrElse Not File.Exists(My.Settings.EnderecoTemplateExcel) Then
            MsgBox("Template Excel não encontrado. Verifique as configurações.", vbCritical, "Erro")
            Return False
        End If
        Return True
    End Function

    ' Método para configurar a barra de progresso
    Private Sub ConfigurarBarraProgresso(BarraProgresso As ProgressBar, totalGrid As Integer, totalMaterial As Integer)
        BarraProgresso.Minimum = 0
        BarraProgresso.Value = 0
        BarraProgresso.Maximum = totalGrid + totalMaterial - 1
    End Sub

    ' Método para obter o ID da Ordem de Serviço
    Private Function ObterIdOrdemServico(dgvGrid As DataGridView) As String
        Try
            Dim id As Integer = Convert.ToInt32(dgvGrid.CurrentRow.Cells("IdOrdemServico").Value)
            Return cl_BancoDados.FormatarPara5Caracteres(id.ToString())
        Catch
            Return cl_BancoDados.FormatarPara5Caracteres("1")
        End Try
    End Function

    ' Método para preencher cabeçalho
    Private Sub PreencherCabecalho(
    planilha As Microsoft.Office.Interop.Excel.Worksheet,
    dgvPrincipal As DataGridView,
    ordemServico As String,
    TipoExcel As Boolean
)
        planilha.Range("R2").Value = ordemServico
        planilha.Range("D10").Value = $"{dgvPrincipal.CurrentRow.Cells("Projeto").Value} - {dgvPrincipal.CurrentRow.Cells("DescEmpresa").Value}"
        planilha.Range("D11").Value = dgvPrincipal.CurrentRow.Cells("Tag").Value
        planilha.Range("N10").Value = dgvPrincipal.CurrentRow.Cells("Descricao").Value
        planilha.Range("D12").Value = dgvPrincipal.CurrentRow.Cells("ENDERECO").Value
        planilha.Range("D16").Value = dgvPrincipal.CurrentRow.Cells("USUARIO").Value
        planilha.Range("D17").Value = dgvPrincipal.CurrentRow.Cells("Data").Value
        If TipoExcel Then
            planilha.Range("N16").Value = dgvPrincipal.CurrentRow.Cells("Data_Liberacao_Engenharia").Value
        End If
        planilha.Range("Q16").Value = My.Computer.Name.ToUpper()
        planilha.Range("Q17").Value = Date.Now.ToShortDateString
    End Sub

    ' Método para preencher dados do grid
    Private Sub PreencherGrid(
    planilha As Microsoft.Office.Interop.Excel.Worksheet,
    dgv As DataGridView,
    linhaInicial As Integer,
    BarraProgresso As ProgressBar,
    Optional tipoMaterial As String = ""
)
        For i As Integer = 0 To dgv.Rows.Count - 1
            Dim linhaAtual = linhaInicial + i
            Try
                planilha.Range("A22:U22").Copy(planilha.Range("A" & linhaAtual & ":U" & linhaAtual))

                Dim row = dgv.Rows(i)
                planilha.Range("A" & linhaAtual).Value = row.Cells("IDOrdemServicoItem").Value
                planilha.Range("B" & linhaAtual).Value = row.Cells("DescResumo").Value
                planilha.Range("I" & linhaAtual).Value = row.Cells("DescDetal").Value
                planilha.Range("L" & linhaAtual).Value = row.Cells("CodMatFabricante").Value
                planilha.Range("N" & linhaAtual).Value = row.Cells("QtdeTotal").Value
                planilha.Range("P" & linhaAtual).Value = If(String.IsNullOrEmpty(tipoMaterial), row.Cells("Unidade").Value, tipoMaterial)
                BarraProgresso.Value += 1
            Catch
                Continue For
            End Try
        Next
    End Sub

End Class