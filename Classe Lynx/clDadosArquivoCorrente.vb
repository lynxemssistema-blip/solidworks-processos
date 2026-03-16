Imports System.Data.SqlClient
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports iText.Kernel.Pdf
Imports MySql.Data.MySqlClient
Imports SolidWorks.Interop.sldworks
Imports SolidWorks.Interop.swconst

Public Class ClDadosArquivoCorrente

    Public IdMaterial As Integer
    Public NomeArquivoComExtensao As String
    Public NomeArquivoSemExtensao As String
    Public EnderecoArquivo As String

    Public EnderecoArquivoAterior As String

    Public Extencao As String

    Public DataCriacaDesenho As String
    Public DataUltimoSalvamento As String
    Public SalvoUltimaVezPor As String

    Public Titulo As String
    Public AssuntoSubiTitulo As String
    Public Comentarios As String
    Public Author As String
    Public PalavraChave As String

    'Processo
    Public soldagem As String

    Public Acabamento As String
    Public TipoDesenho As String
    Public Corte As String
    Public Dobra As String
    Public Solda As String
    Public Pintura As String
    Public Montagem As String
    Public ItemEstoque As String
    Public rnc As String

    'Public Sobra_Fabrica As String
    Public qtde As String

    Public ArquivoPdf As String
    Public ArquivoDxf As String
    Public ArquivoDft As String
    Public ArquivoLXDS As String

    'Caixa Delimitadora
    Public Profundidadeaixadelimitadora As String

    Public Larguracaixadelimitadora As String
    Public Alturacaixadelimitadora As String

    'Lista de Corte
    Public ComprimentoBlank As String = Nothing

    Public LarguraBlank As String = Nothing
    Public Espessura As String = Nothing
    Public PerimetroCorteExterno As String
    Public PerimetroCorteInterno As String
    Public NumeroDobras As String
    Public Massa As String
    Public material As String
    Public AreaPintura As String

    Public propertyName As String
    Public propertyValue As String
    Public propertyNames As Object = Nothing
    Public propertyValues As Object = Nothing
    Public propertyTypes As Object = Nothing
    Public wasResolvedLC As Object = Nothing
    Public NomePropriedadeListCut As String
    Public DescricaoPendencia As String
    Public Sobra_Fabrica As String

    Public EnderecoFichaTecnica As String
    Public EnderecoIsometrico As String

    Public Bloqueado As String
    Public Aprovado As String
    Public Verificado As String

    Public EnderecoImagem As String

    Public Function ArquivoCorrente(ByVal swModel As ModelDoc2, ByVal chkBoxProcesso As CheckedListBox) As Boolean

        MyTaskPanelHost.LimparTelaVariaveis()

        IntanciaSolidWorks.ConectarSolidWorks()
        ' swApparq = CreateObject("SldWorks.Application")

        swModel = swapp.ActiveDoc
        'swModel = swApparq.ActiveDoc

        If swModel Is Nothing Then

            Exit Function
        Else

            ' Obtém o tipo do documento atual
            Dim docType As Integer
            docType = swModel.GetType()

            EnderecoArquivoAterior = swModel.GetPathName.ToUpper

            ' Declara uma variável para armazenar o manipulador do documento
            Dim docHandler As Object

            If DescarregarLynx = False Then Exit Function

            ' Usa Select Case para diferenciar o tipo do documento
            Select Case docType
                Case swDocumentTypes_e.swDocPART

                    docHandler = New PartEventHandler()
                    ' Adicione o código específico para manipular documentos de peça aqui
                    LendoDadosComunsPartAssembly(swModel, chkBoxProcesso)

                Case swDocumentTypes_e.swDocASSEMBLY

                    docHandler = New AssemblyEventHandler()
                    ' Adicione o código específico para manipular documentos de montagem aqui
                    LendoDadosComunsPartAssembly(swModel, chkBoxProcesso)

                Case swDocumentTypes_e.swDocDRAWING

                    docHandler = New DrawingEventHandler()
                    ' Adicione o código específico para manipular documentos de desenho aqui

                Case Else

                    ' Tipo de documento não reconhecido
                    Return False

            End Select

            ' Se chegou aqui, o tipo de documento foi reconhecido

            Return True

        End If

    End Function

    Public Function LendoDadosComunsPartAssembly(ByRef swModel As ModelDoc2, ByVal chkBoxProcesso As CheckedListBox) As Boolean

        Try

            ''''''''''''''''''''''''''''''''''''''''''''''''''''''     IntanciaSolidWorks.ConectarSolidWorks()
            swModel = swapp.ActiveDoc

            If swModel Is Nothing Then

                Exit Function
                Return False
            End If

            If swModel.GetType() = swDocumentTypes_e.swDocPART Or swModel.GetType() = swDocumentTypes_e.swDocASSEMBLY Then
                swModel.Visible = True
                swModelDocExt = swModel.Extension
                MyMassProp = swModelDocExt.CreateMassProperty

                EnderecoArquivo = swModel.GetPathName().ToUpper()
                NomeArquivoComExtensao = Path.GetFileName(EnderecoArquivo).ToUpper()
                NomeArquivoSemExtensao = Path.GetFileNameWithoutExtension(EnderecoArquivo)

                ' Manipulação de arquivos
                ArquivoDxf = Replace(EnderecoArquivo, ".SLDASM", ".DXF").Replace(".SLDPRT", ".DXF")
                ArquivoPdf = Replace(EnderecoArquivo, ".SLDASM", ".PDF").Replace(".SLDPRT", ".PDF")
                ArquivoDft = Replace(EnderecoArquivo, ".SLDASM", ".DFT").Replace(".SLDPRT", ".DFT")

                ' Obtenção de informações
                Titulo = swModel.SummaryInfo(swSummInfoField_e.swSumInfoTitle)
                AssuntoSubiTitulo = swModel.SummaryInfo(swSummInfoField_e.swSumInfoSubject)
                Comentarios = swModel.SummaryInfo(swSummInfoField_e.swSumInfoComment)
                Author = swModel.SummaryInfo(swSummInfoField_e.swSumInfoAuthor)
                PalavraChave = swModel.SummaryInfo(swSummInfoField_e.swSumInfoKeywords)
                DataCriacaDesenho = swModel.SummaryInfo(swSummInfoField_e.swSumInfoCreateDate).ToString()
                DataUltimoSalvamento = swModel.SummaryInfo(swSummInfoField_e.swSumInfoSaveDate).ToString()
                SalvoUltimaVezPor = swModel.SummaryInfo(swSummInfoField_e.swSumInfoSavedBy).ToUpper()

                ' Cálculo da área de pintura
                AreaPintura = If(MyMassProp IsNot Nothing, MyMassProp.SurfaceArea.ToString("F2", Globalization.CultureInfo.InvariantCulture), "0.00")
                ' AreaPintura = If((MyMassProp IsNot Nothing, MyMassProp.SurfaceArea).ToString("F2"), 0.0)

                'MyMassProp.SetResultOptions(kg:=True)
                ''''''''''''''''''''''''''''''''''''''''''Massa = If(MyMassProp IsNot Nothing, MyMassProp.Mass.ToString("F2", Globalization.CultureInfo.InvariantCulture), "0.00")

                ' DadosArquivoCorrente.AreaPintura

                ' EnderecoFichaTecnica = cl_BancoDados.RetornaCampoDaPesquisa("Select EnderecoFichaTecnica from  " & ComplementoTipoBanco & "material where CodMatFabricante = '" & NomeArquivoSemExtensao & "'", "EnderecoFichaTecnica")
                ' EnderecoIsometrico = cl_BancoDados.RetornaCampoDaPesquisa("Select EnderecoIsometrico from  " & ComplementoTipoBanco & "material where CodMatFabricante = '" & NomeArquivoSemExtensao & "'", "EnderecoIsometrico")

                ' Obtendo propriedades personalizadas
                If swModel IsNot Nothing Then
                    swCustProp = swModelDocExt.CustomPropertyManager("")
                    ' Try

                    soldagem = GetCustomProperty(swCustProp, "txtsoldagem")
                    Acabamento = GetCustomProperty(swCustProp, "txtacabamento")
                    TipoDesenho = GetCustomProperty(swCustProp, "txtTipoDesenho")
                    Corte = GetCustomProperty(swCustProp, "txtcorte")
                    Dobra = GetCustomProperty(swCustProp, "txtdobra")
                    Solda = GetCustomProperty(swCustProp, "txtsolda")
                    Pintura = GetCustomProperty(swCustProp, "txtpintura")
                    Montagem = GetCustomProperty(swCustProp, "txtmontagem")
                    ItemEstoque = GetCustomProperty(swCustProp, "txtitemestoque")
                    Bloqueado = GetCustomProperty(swCustProp, "Bloqueado")

                    Aprovado = GetCustomProperty(swCustProp, "Aprovado")
                    Verificado = GetCustomProperty(swCustProp, "Verificado")

                    LerPropriedadesPersonalizadas(swModel, chkBoxProcesso)

                End If

            End If

            Return False

            ' PercorrerPropriedadesDaListaDeCorte(swModel)
        Catch ex As Exception

            '  MsgBox(ex.Message & " Erro ao abrir")
        Finally

        End Try

        '  MyTaskPanelHost.TimerMontaPeca.Enabled = True

    End Function

    Public Function SalvarPrintDoModelo(caminhoDestino As String, swModel As ModelDoc2)
        Try
            If swModel Is Nothing Then
                MsgBox("Nenhum modelo ativo encontrado.", vbExclamation)
                Exit Function
            End If

            ' Define parâmetros para salvar como imagem
            Dim errors As Integer = 0
            Dim warnings As Integer = 0

            ' O caminho deve ter a extensão da imagem, ex: .png, .jpg
            Dim sucesso As Boolean = swModel.Extension.SaveAs(
            caminhoDestino,
            swSaveAsVersion_e.swSaveAsCurrentVersion,
            swSaveAsOptions_e.swSaveAsOptions_Silent,
            Nothing, errors, warnings
        )

            'If sucesso Then
            '    MsgBox("Imagem salva com sucesso em:" & vbCrLf & caminhoDestino, vbInformation)
            'Else
            '    MsgBox("Falha ao salvar imagem. Erros: " & errors & " | Warnings: " & warnings, vbCritical)
            'End If
        Catch ex As Exception
        Finally
            'MsgBox("Erro ao capturar imagem: " & ex.Message, vbCritical)
        End Try
    End Function

    Sub LerPropriedadesPersonalizadas(swModel As ModelDoc2, ByVal chkBoxProcesso As CheckedListBox)

        If swModel Is Nothing Then
            Exit Sub
        End If

        'desmarca todos os itens
        For i As Integer = 0 To chkBoxProcesso.Items.Count - 1

            chkBoxProcesso.SetItemChecked(i, False)

        Next

        ' Obtém a extensão do documento
        Dim swModelDocExt As ModelDocExtension = swModel.Extension

        ' Obtém o gerenciador de propriedades personalizadas
        Dim swCustPropMgr As CustomPropertyManager = swModelDocExt.CustomPropertyManager("")

        If swCustPropMgr Is Nothing Then
            Exit Sub
        End If

        ' Obtém todas as propriedades personalizadas do documento
        Dim propNames As Object = swCustPropMgr.GetNames()

        If propNames IsNot Nothing Then
            Dim propArray() As String = CType(propNames, String())

            For Each propName As String In propArray
                Dim propValue As String = ""
                Dim resolvedValue As String = ""

                ' Obtém o valor da propriedade
                swCustPropMgr.Get4(propName, False, propValue, resolvedValue)

                ' Exibe no console ou armazena como necessário
                '  MsgBox($"Propriedade: {propName}, Valor: {resolvedValue}")

                propName = Replace(propName.ToString(), " ", "").ToLower

                For i As Integer = 0 To chkBoxProcesso.Items.Count - 1

                    ' Obtém o nome do processo sem espaços
                    Dim Processo As String = "txt" & Replace(chkBoxProcesso.Items(i).ToString(), " ", "").ToLower

                    If propName = Processo AndAlso resolvedValue = "1" Then
                        ' Verifica se o item está marcado corretamente
                        ' If chkBoxProcesso.GetItemChecked(i) Then

                        chkBoxProcesso.SetItemChecked(i, True)

                    End If

                Next

            Next
        Else

            Console.WriteLine("Nenhuma propriedade personalizada encontrada.")

        End If

        If DadosArquivoCorrente.Bloqueado = "S" Then

            MyTaskPanelHost.chkAtualizacao.Checked = True

        ElseIf DadosArquivoCorrente.Bloqueado = "" Or DadosArquivoCorrente.Bloqueado = Nothing Or DadosArquivoCorrente.Bloqueado = "N" Then

            MyTaskPanelHost.chkAtualizacao.Checked = False

        End If

    End Sub

    Private Sub LimparPropriedadesListaDeCorte()
        ' Reseta todas as propriedades para seus valores padrões
        ComprimentoBlank = "0"
        LarguraBlank = "0"
        Espessura = "0"
        PerimetroCorteExterno = "0"
        PerimetroCorteInterno = "0"
        NumeroDobras = "0"
        'Massa = ""
        material = ""
    End Sub

    Public Function PercorrerPropriedadesDaListaDeCorte(ByVal swModel As ModelDoc2) As Boolean

        Try

            IntanciaSolidWorks.ConectarSolidWorks()
            ' swApparq = CreateObject("SldWorks.Application")

            swModel = swapp.ActiveDoc
            'swModel = swApparq.ActiveDoc



            ' Verifica se o modelo foi aberto corretamente
            If swModel Is Nothing Then
                ' Retorna falso se o modelo não for válido
                Return False
            End If

            Dim swMassProp As MassProperty = swModel.Extension.CreateMassProperty()

            ' Atualiza os dados da massa (importante para garantir precisão)
            'swMassProp.Update()

            ' Força o uso do sistema de unidades do documento
            swMassProp.UseSystemUnits = True

            ' Verifica se o documento é do tipo peça
            If swModel.GetType() <> swDocumentTypes_e.swDocPART Then
                ' Caso não seja uma peça, limpa as propriedades e retorna falso
                LimparPropriedadesListaDeCorte()

                ''''''Massa = GetCustomProperty(swCustProp, "Peso")
                ''''''' material = GetCustomProperty(swCustProp, "material")

                ''''''Try

                ''''''    If IsNumeric(Massa) = False Then

                ''''''        Massa = "0"

                ''''''    End If

                ''''''Catch ex As Exception
                ''''''    Massa = "0"
                ''''''Finally

                ''''''End Try

                ''''''DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "Peso", Massa, Massa)

                ''''''Exit Function
                '''
                Try
                    ' Verifica se o modelo não é nulo
                    ' If swModel Is Nothing Then Return Massa

                    ' Objeto para propriedades físicas
                    'Dim swMassProp As MassProperty = swModel.Extension.CreateMassProperty()

                    '' Atualiza os dados da massa (importante para garantir precisão)
                    ''swMassProp.Update()

                    '' Força o uso do sistema de unidades do documento
                    'swMassProp.UseSystemUnits = True

                    ' Pega a massa em quilogramas
                    Dim massaKg As Double = swMassProp.Mass

                    ' Converte para string com duas casas decimais, usa ponto como separador decimal
                    Massa = massaKg.ToString("0.00", Globalization.CultureInfo.InvariantCulture)

                    ' Garante a propriedade personalizada "Peso"
                    DadosArquivoCorrente.GarantirOuCriarPropriedade(swModel, "Peso", Massa, Massa)
                Catch ex As Exception
                    ' Em caso de erro, define massa como zero
                    Massa = "0"
                End Try
            Else

                Try

                    material = GetCustomProperty(swCustProp, "material")

                    ' O nome do material é o primeiro valor no array de propriedades
                    ' material = materialProperties(0) ' O nome do material é armazenado no índice 0
                Catch ex As Exception

                    material = ""

                End Try

                ' MsgBox(material)

                Try
                    ' Converte o documento em um PartDoc para acessar as funcionalidades da peça
                    Dim part As PartDoc = CType(swModel, PartDoc)

                    ' Força a reconstrução para garantir que a lista de corte esteja atualizada
                    ' swModel.ForceRebuild3(False) ' Força a reconstrução
                    swModel.ForceRebuild3(True)

                    Dim cutListFeature As Feature = part.FirstFeature()

                    Dim feat As Feature = swModel.FirstFeature()
                    Dim cutListFolder As Object

                    Espessura = "0"

                    While cutListFeature IsNot Nothing

                        If cutListFeature.GetTypeName() = "CutListFolder" Or
                            cutListFeature.GetTypeName() = "Lista de corte(1)" Or
                             cutListFeature.GetTypeName().Contains("Item da lista de") Then

                            cutListFolder = cutListFeature.GetSpecificFeature2()

                            cutListFolder.UpdateCutList() ' Força a atualização da lista de corte
                            ' cutListFolder.SetAutomaticUpdate(True) ' Força a atualização automática da lista de corte
                            ' cutListFolder.SetAutomaticUpdate(True) ' Força a atualização automática da lista de corte
                            'cutListFolder.UpdateCutList(True)

                            'edson 23/04/2025
                            'cutListFolder.SetAutomaticUpdate(False)
                            ' Obtém as propriedades da lista de corte
                            Dim cutListProperties As CustomPropertyManager = cutListFeature.CustomPropertyManager()

                            ' Captura todas as propriedades da lista de corte
                            cutListProperties.GetAll3(propertyNames, propertyValues, propertyTypes, wasResolvedLC, False)

                            ' Percorre as propriedades
                            For i As Integer = 0 To propertyNames.Length - 1
                                propertyName = CType(propertyNames(i), String)
                                propertyValue = CType(propertyValues(i), String)
                                NomePropriedadeListCut = CType(propertyNames(i), String)

                                If String.Equals(NomePropriedadeListCut.ToString(), "Comprimento da Caixa delimitadora", StringComparison.OrdinalIgnoreCase) Or
   String.Equals(NomePropriedadeListCut.ToString(), "Bounding Box Length", StringComparison.OrdinalIgnoreCase) Then

                                    Try
                                        ' Garante que o valor inicial seja tratado como String e evita nulos.
                                        ComprimentoBlank = If(propertyTypes(i)?.ToString(), "0")

                                        ' Verifica se é numérico; caso contrário, atribui "0".
                                        If Not IsNumeric(ComprimentoBlank) Then
                                            ComprimentoBlank = "0"
                                        Else
                                            ' Converte para Decimal e formata com 2 casas decimais usando ponto como separador.
                                            Dim comprimentoNumerico As Decimal = Decimal.Parse(ComprimentoBlank, Globalization.CultureInfo.InvariantCulture)
                                            ComprimentoBlank = comprimentoNumerico.ToString("F2", Globalization.CultureInfo.InvariantCulture)
                                        End If
                                    Catch ex As Exception
                                        ' Em caso de erro, atribui o valor padrão "0.00".
                                        ComprimentoBlank = "0.00"
                                    End Try

                                ElseIf String.Equals(NomePropriedadeListCut.ToString(), "Largura da Caixa delimitadora", StringComparison.OrdinalIgnoreCase) Or
       String.Equals(NomePropriedadeListCut.ToString(), "Bounding Box Width", StringComparison.OrdinalIgnoreCase) Then

                                    Try
                                        ' Garante que o valor inicial seja tratado como String e evita nulos.
                                        LarguraBlank = If(propertyTypes(i)?.ToString(), "0")

                                        ' Verifica se é numérico; caso contrário, atribui "0".
                                        If Not IsNumeric(LarguraBlank) Then
                                            LarguraBlank = "0"
                                        Else
                                            ' Converte para Decimal e formata com 2 casas decimais usando ponto como separador.
                                            Dim larguraNumerica As Decimal = Decimal.Parse(LarguraBlank, Globalization.CultureInfo.InvariantCulture)
                                            LarguraBlank = larguraNumerica.ToString("F2", Globalization.CultureInfo.InvariantCulture)
                                        End If
                                    Catch ex As Exception
                                        ' Em caso de erro, atribui o valor padrão "0.00".
                                        LarguraBlank = "0.00"
                                    End Try

                                ElseIf String.Equals(NomePropriedadeListCut.ToString(), "Espessura da Chapa metálica", StringComparison.OrdinalIgnoreCase) Or
       String.Equals(NomePropriedadeListCut.ToString(), "Sheet Metal Thickness", StringComparison.OrdinalIgnoreCase) Then

                                    Try
                                        ' Garante que o valor inicial seja tratado como String e evita nulos.
                                        Espessura = If(propertyTypes(i)?.ToString(), "0")

                                        ' Se não estiver vazio, formata como número com ponto como separador decimal.
                                        If Not String.IsNullOrWhiteSpace(Espessura) Then
                                            Dim espessuraNumerica As Decimal = Decimal.Parse(Espessura, Globalization.CultureInfo.InvariantCulture)
                                            Espessura = espessuraNumerica.ToString("F2", Globalization.CultureInfo.InvariantCulture)
                                        Else
                                            Espessura = "0.00" ' Formato padrão com ponto.
                                        End If
                                    Catch ex As Exception
                                        ' Caso ocorra erro, retorna valor padrão formatado.
                                        Espessura = "0.00"
                                    End Try
                                ElseIf String.Equals(NomePropriedadeListCut.ToString(), "Perímetro de corte-Externo", StringComparison.OrdinalIgnoreCase) Or
       String.Equals(NomePropriedadeListCut.ToString(), "Cutting Length-Outer", StringComparison.OrdinalIgnoreCase) Then

                                    Try
                                        ' Garante que o valor inicial seja tratado como String e evita nulos.
                                        PerimetroCorteExterno = If(propertyTypes(i)?.ToString(), "0")

                                        ' Verifica se o valor é numérico.
                                        If Not IsNumeric(PerimetroCorteExterno) Then
                                            PerimetroCorteExterno = "0"
                                        Else
                                            ' Converte para Decimal e formata com 2 casas decimais usando ponto como separador.
                                            Dim perimetroNumerico As Decimal = Decimal.Parse(PerimetroCorteExterno, Globalization.CultureInfo.InvariantCulture)
                                            PerimetroCorteExterno = perimetroNumerico.ToString("F2", Globalization.CultureInfo.InvariantCulture)
                                        End If
                                    Catch ex As Exception
                                        ' Em caso de erro, atribui o valor padrão "0.00".
                                        PerimetroCorteExterno = "0.00"
                                    End Try

                                ElseIf String.Equals(NomePropriedadeListCut.ToString(), "Perímetro interno de corte", StringComparison.OrdinalIgnoreCase) Or
       String.Equals(NomePropriedadeListCut.ToString(), "Cutting Length-Inner", StringComparison.OrdinalIgnoreCase) Then

                                    Try
                                        ' Garante que o valor inicial seja tratado como String e evita nulos.
                                        PerimetroCorteInterno = If(propertyTypes(i)?.ToString(), "0")

                                        ' Verifica se o valor é numérico.
                                        If Not IsNumeric(PerimetroCorteInterno) Then
                                            PerimetroCorteInterno = "0"
                                        Else
                                            ' Converte para Decimal e formata com 2 casas decimais usando ponto como separador.
                                            Dim perimetroNumerico As Decimal = Decimal.Parse(PerimetroCorteInterno, Globalization.CultureInfo.InvariantCulture)
                                            PerimetroCorteInterno = perimetroNumerico.ToString("F2", Globalization.CultureInfo.InvariantCulture)
                                        End If
                                    Catch ex As Exception
                                        ' Em caso de erro, atribui o valor padrão "0.00".
                                        PerimetroCorteInterno = "0.00"
                                    End Try

                                ElseIf String.Equals(NomePropriedadeListCut.ToString(), "Dobras", StringComparison.OrdinalIgnoreCase) Or
        String.Equals(NomePropriedadeListCut.ToString(), "Bends", StringComparison.OrdinalIgnoreCase) Then

                                    Try
                                        ' Garante que o valor inicial seja tratado como String e evita nulos.
                                        NumeroDobras = If(propertyTypes(i)?.ToString(), "0")

                                        ' Verifica se o valor é numérico.
                                        If Not IsNumeric(NumeroDobras) Then
                                            NumeroDobras = "0"
                                        Else
                                            ' Converte para Integer e garante que seja um número inteiro.
                                            Dim numeroDobrasInt As Integer = Convert.ToInt32(Decimal.Parse(NumeroDobras, Globalization.CultureInfo.InvariantCulture))
                                            NumeroDobras = numeroDobrasInt.ToString()
                                        End If
                                    Catch ex As Exception
                                        ' Em caso de erro, atribui o valor padrão "0".
                                        NumeroDobras = "0"
                                    End Try

                                ElseIf String.Equals(NomePropriedadeListCut.ToString(), "Massa", StringComparison.OrdinalIgnoreCase) Or
       String.Equals(NomePropriedadeListCut.ToString(), "Mass", StringComparison.OrdinalIgnoreCase) Then

                                    '''''''Try
                                    '''''''    ' Garante que o valor inicial seja tratado como String e evita nulos.
                                    '''''''    Massa = If(propertyTypes(i)?.ToString(), "0")

                                    '''''''    ' Verifica se o valor é numérico.
                                    '''''''    If Not IsNumeric(Massa) Then
                                    '''''''        Massa = "0"
                                    '''''''    Else
                                    '''''''        ' Converte para Decimal e formata com 2 casas decimais usando ponto como separador.
                                    '''''''        Dim massaNumerica As Decimal = Decimal.Parse(Massa, Globalization.CultureInfo.InvariantCulture)
                                    '''''''        Massa = massaNumerica.ToString("F2", Globalization.CultureInfo.InvariantCulture)
                                    '''''''    End If

                                    '''''''Catch ex As Exception
                                    '''''''    ' Em caso de erro, atribui o valor padrão "0.00".
                                    '''''''    Massa = "0.00"
                                    '''''''End Try

                                    Try
                                        Massa = If(propertyTypes(i)?.ToString(), "0")

                                        If Not IsNumeric(Massa) Then
                                            Massa = "0.00"
                                        Else
                                            Dim massaNumerica As Decimal = Decimal.Parse(Massa, Globalization.CultureInfo.InvariantCulture)

                                            '    Forçar conversão por tentativa de detecção (simples)
                                            If massaNumerica > 200 Then ' Provavelmente está em gramas
                                                massaNumerica /= 1000D
                                            ElseIf massaNumerica < 5 Then ' Pode ser libra (ex: 2.2 lb = 1 kg)
                                                massaNumerica *= 0.453592D
                                            End If

                                            Massa = massaNumerica.ToString("F2", Globalization.CultureInfo.InvariantCulture)
                                        End If
                                    Catch ex As Exception
                                        Massa = "0.00"
                                    End Try

                                ElseIf String.Equals(NomePropriedadeListCut.ToString(), "material", StringComparison.OrdinalIgnoreCase) Then

                                    ' Garante que o valor inicial seja tratado como String e evita nulos.
                                    material = If(propertyTypes(i)?.ToString(), String.Empty)

                                    ' Se o valor for "True", redefine como uma string vazia.
                                    If String.Equals(material, "True", StringComparison.OrdinalIgnoreCase) Then
                                        material = String.Empty
                                    End If

                                End If

                            Next

                            'cutListFolder.UpdateCutList() ' Força a atualização da lista de corte
                            cutListFolder.SetAutomaticUpdate(False) ' Força a atualização automática da lista de corte

                        End If

                        cutListFeature = cutListFeature.GetNextFeature()

                        '  Exit While

                    End While

                    If Espessura.ToString = "" Or Espessura.ToString = "0" Or Espessura = Nothing Then

                        ComprimentoBlank = "0"
                        LarguraBlank = "0"
                        Espessura = "0"
                        PerimetroCorteExterno = "0"
                        PerimetroCorteInterno = "0"
                        NumeroDobras = "0"
                        Massa = "0"
                        material = ""

                    End If
                Catch ex As Exception
                    ComprimentoBlank = "0"
                    LarguraBlank = "0"
                    Espessura = "0"
                    PerimetroCorteExterno = "0"
                    PerimetroCorteInterno = "0"
                    NumeroDobras = "0"
                    Massa = "0"
                    material = ""
                Finally
                End Try

            End If
        Catch ex As Exception
        Finally
        End Try

    End Function

    Public Sub LerDadosCaixaDelimitadora(ByVal swModel As ModelDoc2)

        Try


            IntanciaSolidWorks.ConectarSolidWorks()
            ' swApparq = CreateObject("SldWorks.Application")

            If My.Settings.CaixaDelimitadora = "SIM" Then

                'So se aplica caixa delimitadora para Metta
                ' If My.Settings.BancoDadosAtivo = "mettapaineis" Then

                '  Dim swApp As Object
                '  Dim swModel As Object
                ' Dim swModelDocExt As SolidWorks.Interop.sldworks.ModelDocExtension
                Dim swConfMgr As SolidWorks.Interop.sldworks.ConfigurationManager
                Dim swConf As SolidWorks.Interop.sldworks.Configuration
                Dim swCustPropMgr As SolidWorks.Interop.sldworks.CustomPropertyManager
                ' Dim bool As Boolean
                Dim valout As String = String.Empty
                Dim success As Boolean = False

                'swModel.Rebuild(2)

                ' Conectar ao SolidWorks
                'IntanciaSolidWorks.ConectarSolidWorks()

                ' Obter o documento ativo
                swModel = swapp.ActiveDoc

                Try

                    ' Verifique se o swModel foi aberto com sucesso
                    If Not swModel Is Nothing Then

                        ' Usa Select Case para diferenciar o tipo do documento
                        If swModel.GetType() = swDocumentTypes_e.swDocPART Or swModel.GetType() = swDocumentTypes_e.swDocASSEMBLY Then

                            ' Obtém o gerenciador de recursos da peça
                            Dim swFeatMgr As FeatureManager = swModel.FeatureManager
                            If swFeatMgr Is Nothing Then
                                MessageBox.Show("Erro ao obter o gerenciador de recursos da peça.")
                                ' Return
                            End If

                            ' Cria a definição da caixa delimitadora
                            Dim swFeatData As Object = swFeatMgr.CreateDefinition(swFeatureNameID_e.swFmBoundingBox)
                            If swFeatData Is Nothing Then
                                MessageBox.Show("Erro ao criar a definição da caixa delimitadora.")
                                ' Return
                            End If

                            ' Configura as propriedades da caixa delimitadora
                            swFeatData.IncludeHiddenBodies = False
                            swFeatData.IncludeSurfaces = False
                            swFeatData.ReferenceFaceOrPlane = 1

                            ' Cria a caixa delimitadora
                            Dim swFeat As Feature = swFeatMgr.CreateFeature(swFeatData)
                            If swFeat Is Nothing Then
                                ' MessageBox.Show("Erro ao criar a caixa delimitadora.")
                                ' Return
                            End If

                            ' Limpa a seleção
                            swModel.ClearSelection2(True)

                            swConfMgr = swModel.ConfigurationManager

                            ' Obtém a configuração ativa
                            swConf = swConfMgr.ActiveConfiguration

                            ' Obtém o gerenciador de propriedades personalizadas da configuração ativa
                            swCustPropMgr = swModelDocExt.CustomPropertyManager(swConf.Name)

                            '' Verifica se o gerenciador de propriedades personalizadas foi obtido com sucesso
                            'If swCustPropMgr Is Nothing Then
                            '    Return "Erro ao obter o gerenciador de propriedades personalizadas da configuração ativa."
                            'End If

                            ' Tenta obter a propriedade "Comprimento total da caixa delimitadora"
                            ''''success = swCustPropMgr.Get4("Comprimento total da caixa delimitadora", False, valout, valout)

                            ''''' Atribui o valor de valout a Alturacaixadelimitadora com formatação de 2 casas decimais, se for numérico
                            ''''Alturacaixadelimitadora = If(success AndAlso IsNumeric(valout), Convert.ToDecimal(valout).ToString("F2", Globalization.CultureInfo.InvariantCulture), "")

                            ''''' Tenta obter a propriedade "Largura total da caixa delimitadora"
                            ''''success = swCustPropMgr.Get4("Largura total da caixa delimitadora", False, valout, valout)

                            ''''' Atribui o valor de valout a Larguracaixadelimitadora com formatação de 2 casas decimais, se for numérico
                            ''''Larguracaixadelimitadora = If(success AndAlso IsNumeric(valout), Convert.ToDecimal(valout).ToString("F2", Globalization.CultureInfo.InvariantCulture), "")

                            ''''' Tenta obter a propriedade "Espessura total da caixa delimitadora"
                            ''''success = swCustPropMgr.Get4("Espessura total da caixa delimitadora", False, valout, valout)

                            ''''' Atribui o valor de valout a Profundidadeaixadelimitadora com formatação de 2 casas decimais, se for numérico
                            ''''Profundidadeaixadelimitadora = If(success AndAlso IsNumeric(valout), Convert.ToDecimal(valout).ToString("F2", Globalization.CultureInfo.InvariantCulture), "")
                            '''

                            success = swCustPropMgr.Get4("Comprimento total da caixa delimitadora", False, valout, valout)

                            ' Atribui o valor de valout a Alturacaixadelimitadora com formatação de 2 casas decimais, se for numérico
                            Alturacaixadelimitadora = If(success AndAlso IsNumeric(valout), valout, "")

                            ' Tenta obter a propriedade "Largura total da caixa delimitadora"
                            success = swCustPropMgr.Get4("Largura total da caixa delimitadora", False, valout, valout)

                            ' Atribui o valor de valout a Larguracaixadelimitadora com formatação de 2 casas decimais, se for numérico
                            Larguracaixadelimitadora = If(success AndAlso IsNumeric(valout), valout, "")

                            ' Tenta obter a propriedade "Espessura total da caixa delimitadora"
                            success = swCustPropMgr.Get4("Espessura total da caixa delimitadora", False, valout, valout)

                            ' Atribui o valor de valout a Profundidadeaixadelimitadora com formatação de 2 casas decimais, se for numérico
                            Profundidadeaixadelimitadora = If(success AndAlso IsNumeric(valout), valout, "")

                        End If
                    End If

                    '  ExcluirCaixaDelimitadora(swModel)
                Catch ex As Exception
                    '  Return "Ocorreu um problema: " & ex.Message
                Finally

                End Try

            ElseIf My.Settings.CaixaDelimitadora = "NÃO" Then

                ExcluirCaixaDelimitadora(swModel)

            End If
        Catch ex As Exception
        Finally

        End Try
    End Sub

    Public Sub ExcluirCaixaDelimitadora(ByVal swModel As ModelDoc)

        If My.Settings.CaixaDelimitadora = "SIM" Then


            IntanciaSolidWorks.ConectarSolidWorks()
            ' swApparq = CreateObject("SldWorks.Application")


            Try

                ' Conectar ao SolidWorks
                'IntanciaSolidWorks.ConectarSolidWorks()

                ' Obter o documento ativo
                swModel = swapp.ActiveDoc

                Try

                    ' Verifique se o swModel foi aberto com sucesso
                    If Not swModel Is Nothing Then

                        ' Usa Select Case para diferenciar o tipo do documento
                        If swModel.GetType() = swDocumentTypes_e.swDocPART Or swModel.GetType() = swDocumentTypes_e.swDocASSEMBLY Then

                            ' Selecionar a feature da caixa delimitadora
                            Dim boolstatus As Boolean
                            boolstatus = swModel.Extension.SelectByID2("Caixa delimitadora", "BBOXSKETCH", 0, 0, 0, False, 0, Nothing, 0)

                            If Not boolstatus Then
                                ' MessageBox.Show("Não foi possível selecionar a caixa delimitadora.")
                                ' Return
                            End If

                            ' Excluir a feature selecionada
                            swModel.EditDelete()

                            ' Ocultar o esboço selecionado
                            swModel.BlankSketch()

                            ' Limpar a seleção
                            swModel.ClearSelection2(True)

                            '  MessageBox.Show("Caixa delimitadora excluída com sucesso.")

                        End If
                    End If
                Catch ex As Exception
                    ' Finally
                End Try
            Catch ex As Exception
            Finally
            End Try

        End If

    End Sub

    Public Function GetCustomProperty(swModelDocExt As CustomPropertyManager, propertyName As String) As String
        Try
            Dim bool As Boolean = False
            Dim valout As String = ""

            bool = swModelDocExt.Get4(propertyName, False, valout, valout)
            If bool Then
                Return valout
            Else
                Return ""
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Sub GarantirOuCriarPropriedade(ByVal swModel As ModelDoc2, ByVal nomePropriedade As String, ByVal valorPadrao As String, ByRef VariavelRecebeValor As String)

        Try

            ' Obter o documento ativo
            swModel = swapp.ActiveDoc
            ' Verifique se o modelo é válido
            If swModel IsNot Nothing Then
                ' Obter o tipo do documento
                Dim modelType As Integer = swModel.GetType()

                ' Verifique se é uma peça ou uma montagem
                If modelType = swDocumentTypes_e.swDocPART OrElse modelType = swDocumentTypes_e.swDocASSEMBLY Then

                    ' Obtenha o gerenciador de propriedades personalizadas
                    Dim swCustPropMgr As CustomPropertyManager = swModel.Extension.CustomPropertyManager("")

                    ' Verifique se a propriedade já existe
                    Dim valorAtual As String = ""
                    Dim foiResolvido As Boolean = swCustPropMgr.Get4(nomePropriedade, False, valorAtual, valorAtual)

                    ' Atualizar ou criar a propriedade com o valor fornecido pelo usuário
                    swCustPropMgr.Add3(nomePropriedade, swCustomInfoType_e.swCustomInfoText, valorPadrao, swCustomPropertyAddOption_e.swCustomPropertyReplaceValue)

                    ' Define a variável com o valor informado pelo usuário
                    VariavelRecebeValor = valorPadrao
                Else
                    Throw New Exception("O modelo não é uma peça nem uma montagem.")
                End If
                'Else
                '    Throw New Exception("O modelo fornecido é inválido (Nothing).")
            End If
        Catch ex As Exception
            ' Relatar o erro (se necessário, pode incluir logs aqui)
            Console.WriteLine($"Erro ao processar propriedade: {ex.Message}")
            VariavelRecebeValor = ""
        End Try

    End Sub

    Public Function VerificarProcessodaPecaCorrente(ByVal swModel As ModelDoc2, ByVal msg As Boolean) As Boolean

        VerificarProcessodaPecaCorrente = False

        ' Verifique se o modelo é válido
        If swModel IsNot Nothing Then
            ' Obter o tipo do documento
            Dim modelType As Integer = swModel.GetType()

            ' Verifique se é uma peça ou uma montagem
            If modelType = swDocumentTypes_e.swDocPART OrElse modelType = swDocumentTypes_e.swDocASSEMBLY Then

                Dim MensagemErros As String = "Lista de Ações Pendentes no Arquivo: " & DadosArquivoCorrente.NomeArquivoSemExtensao & vbCrLf & vbCrLf & vbCrLf

                If DadosArquivoCorrente.TipoDesenho = "" Then

                    MensagemErros = MensagemErros & "Erro 02 - O tipo de desenho não foi informado, favor informar!" & vbCrLf

                    VerificarProcessodaPecaCorrente = True

                End If

                ' Busca direta no banco
                cl_BancoDados.RetornaCampoDaPesquisa(
        "SELECT rnc FROM " & ComplementoTipoBanco & "material WHERE CodMatFabricante = '" & NomeArquivoSemExtensao & "'",
        "rnc"
    )

                DadosArquivoCorrente.rnc = VCampo0.ToString

                If DadosArquivoCorrente.rnc.ToString <> "" Then
                    MensagemErros &= "Erro 03 - O arquivo não possui RNC, favor verificar!" & vbCrLf
                    VerificarProcessodaPecaCorrente = True
                End If

                If DadosArquivoCorrente.Corte.ToString = "" And
                     DadosArquivoCorrente.Dobra.ToString = "" And
                     DadosArquivoCorrente.Solda.ToString = "" And
                     DadosArquivoCorrente.Pintura.ToString = "" And
                     DadosArquivoCorrente.Montagem.ToString = "" Then

                    MensagemErros = MensagemErros & "Erro 10 - Os arquivos deve conter pelomenos algum processo!" & vbCrLf

                    VerificarProcessodaPecaCorrente = True

                End If

            End If

        End If

    End Function

    '    Public Function AtualizaDesenho(ByVal swModel As ModelDoc2, Optional EntradaDireta As Boolean = True) As Boolean

    '        Try

    '            Dim query As String

    '            query = "INSERT INTO " & ComplementoTipoBanco & "material (DescResumo, DescDetal, PecaManuFat, Autor, Palavrachave, Notas, Espessura, AreaPintura, NumeroDobras, Peso, " &
    '    "Unidade, Altura, Largura, Profundidade, CodMatFabricante, DtCad, UsuarioCriacao, UsuarioAlteracao, DtAlteracao, CodigoJuridicoMat, " &
    '    "StatusMat, MaterialSW, EnderecoArquivo, Acabamento, txtSoldagem, txtTipoDesenho, txtCorte, txtDobra, txtSolda, txtPintura, " &
    '    "txtMontagem, Comprimentocaixadelimitadora, Larguracaixadelimitadora, Espessuracaixadelimitadora, txtItemEstoque, D_E_L_E_T_E,EnderecoImagem) " &
    '    "VALUES (@DescResumo, @DescDetal, @PecaManuFat, @Autor, @Palavrachave, @Notas, @Espessura, @AreaPintura, @NumeroDobras, @Peso, " &
    '    "@Unidade, @Altura, @Largura, @Profundidade, @CodMatFabricante, @DtCad, @UsuarioCriacao, @UsuarioAlteracao, @DtAlteracao, @CodigoJuridicoMat, " &
    '    "@StatusMat, @MaterialSW, @EnderecoArquivo, @Acabamento, @txtSoldagem, @txtTipoDesenho, @txtCorte, @txtDobra, @txtSolda, @txtPintura, " &
    '    "@txtMontagem, @Compcx, @Largcx, @Espcx, @txtItemEstoque, @D_E_L_E_T_E,@EnderecoImagem) " &
    '    "ON DUPLICATE KEY UPDATE " &
    '    "DescResumo = VALUES(DescResumo), DescDetal = VALUES(DescDetal), PecaManuFat = VALUES(PecaManuFat), Autor = VALUES(Autor), " &
    '    "Palavrachave = VALUES(Palavrachave), Notas = VALUES(Notas), Espessura = VALUES(Espessura), AreaPintura = VALUES(AreaPintura), " &
    '    "NumeroDobras = VALUES(NumeroDobras), Peso = VALUES(Peso), Unidade = VALUES(Unidade), Altura = VALUES(Altura), Largura = VALUES(Largura), " &
    '    "Profundidade = VALUES(Profundidade), UsuarioAlteracao = VALUES(UsuarioAlteracao), DtAlteracao = VALUES(DtAlteracao), " &
    '    "CodigoJuridicoMat = VALUES(CodigoJuridicoMat), StatusMat = VALUES(StatusMat), MaterialSW = VALUES(MaterialSW), " &
    '    "EnderecoArquivo = VALUES(EnderecoArquivo), Acabamento = VALUES(Acabamento), txtSoldagem = VALUES(txtSoldagem), " &
    '    "txtTipoDesenho = VALUES(txtTipoDesenho), txtCorte = VALUES(txtCorte), txtDobra = VALUES(txtDobra), txtSolda = VALUES(txtSolda), " &
    '    "txtPintura = VALUES(txtPintura), txtMontagem = VALUES(txtMontagem), Comprimentocaixadelimitadora = VALUES(Comprimentocaixadelimitadora), " &
    '    "Larguracaixadelimitadora = VALUES(Larguracaixadelimitadora), Espessuracaixadelimitadora = VALUES(Espessuracaixadelimitadora), " &
    '    "txtItemEstoque = VALUES(txtItemEstoque), D_E_L_E_T_E = VALUES(D_E_L_E_T_E), EnderecoImagem = VALUES(EnderecoImagem)"

    '            If EntradaDireta = True Then

    '                Try

    '                    If DadosArquivoCorrente.PalavraChave.ToString = "" Then
    '                        DadosArquivoCorrente.PalavraChave = DadosArquivoCorrente.EnderecoArquivo.ToString
    '                        swModel.SummaryInfo(swSummInfoField_e.swSumInfoKeywords) = DadosArquivoCorrente.PalavraChave.ToString

    '                    End If

    '                    If DadosArquivoCorrente.AssuntoSubiTitulo.ToString = "" Then
    '                        DadosArquivoCorrente.AssuntoSubiTitulo = DadosArquivoCorrente.NomeArquivoSemExtensao.ToString
    '                        swModel.SummaryInfo(swSummInfoField_e.swSumInfoSubject) = DadosArquivoCorrente.AssuntoSubiTitulo
    '                    End If

    '                    If DadosArquivoCorrente.NumeroDobras <> "" Then
    '                        DadosArquivoCorrente.Corte = "1"
    '                    End If

    '                Catch ex As Exception
    '                Finally
    '                End Try

    '            End If

    '            If My.Settings.TipoConexao = "MYSQL" Then

    '                Using cmd As New MySqlCommand(query, myconect)

    '                    Try

    '                        ' Adicione os parâmetros ao comando
    '                        AddTextParameterMysql(cmd, "@DescResumo", UCase(DadosArquivoCorrente.Titulo))
    '                        AddTextParameterMysql(cmd, "@DescDetal", UCase(DadosArquivoCorrente.AssuntoSubiTitulo))
    '                        AddTextParameterMysql(cmd, "@PecaManuFat", "S")
    '                        AddTextParameterMysql(cmd, "@Autor", UCase(DadosArquivoCorrente.Author))
    '                        AddTextParameterMysql(cmd, "@Palavrachave", UCase(DadosArquivoCorrente.PalavraChave))
    '                        AddTextParameterMysql(cmd, "@Notas", UCase(DadosArquivoCorrente.Comentarios))
    '                        AddTextParameterMysql(cmd, "@Espessura", DadosArquivoCorrente.Espessura)
    '                        AddTextParameterMysql(cmd, "@AreaPintura", DadosArquivoCorrente.AreaPintura)
    '                        AddTextParameterMysql(cmd, "@NumeroDobras", DadosArquivoCorrente.NumeroDobras)
    '                        AddTextParameterMysql(cmd, "@Peso", DadosArquivoCorrente.Massa)
    '                        AddTextParameterMysql(cmd, "@Unidade", "PC")
    '                        AddTextParameterMysql(cmd, "@Altura", UCase(DadosArquivoCorrente.ComprimentoBlank))
    '                        AddTextParameterMysql(cmd, "@Largura", UCase(DadosArquivoCorrente.LarguraBlank))
    '                        AddTextParameterMysql(cmd, "@Profundidade", String.Empty)
    '                        AddTextParameterMysql(cmd, "@CodMatFabricante", UCase(DadosArquivoCorrente.NomeArquivoSemExtensao))
    '                        'cmd.Parameters.AddWithValue("@DtCad", DateTime.Now.Date) ' Formato ISO
    '                        AddTextParameterMysql(cmd, "@DtCad", Date.Now.ToString("dd/MM/yyyy"))
    '                        AddTextParameterMysql(cmd, "@UsuarioCriacao", Usuario.NomeCompleto.ToString.ToUpper)
    '                        AddTextParameterMysql(cmd, "@UsuarioAlteracao", UCase(DadosArquivoCorrente.SalvoUltimaVezPor))
    '                        AddTextParameterMysql(cmd, "@DtAlteracao", DadosArquivoCorrente.DataUltimoSalvamento.ToString) ' Formato ISO
    '                        AddTextParameterMysql(cmd, "@CodigoJuridicoMat", "")
    '                        AddTextParameterMysql(cmd, "@StatusMat", "A")
    '                        AddTextParameterMysql(cmd, "@MaterialSW", UCase(DadosArquivoCorrente.material))
    '                        AddTextParameterMysql(cmd, "@EnderecoArquivo", UCase(DadosArquivoCorrente.EnderecoArquivo))
    '                        AddTextParameterMysql(cmd, "@Acabamento", UCase(DadosArquivoCorrente.Acabamento))
    '                        AddTextParameterMysql(cmd, "@txtSoldagem", UCase(DadosArquivoCorrente.soldagem))
    '                        AddTextParameterMysql(cmd, "@txtTipoDesenho", UCase(DadosArquivoCorrente.TipoDesenho))

    '                        AddTextParameterMysql(cmd, "@txtCorte", UCase(DadosArquivoCorrente.Corte))
    '                        AddTextParameterMysql(cmd, "@txtDobra", UCase(DadosArquivoCorrente.Dobra))
    '                        AddTextParameterMysql(cmd, "@txtSolda", UCase(DadosArquivoCorrente.Solda))
    '                        AddTextParameterMysql(cmd, "@txtPintura", UCase(DadosArquivoCorrente.Pintura))
    '                        AddTextParameterMysql(cmd, "@txtMontagem", UCase(DadosArquivoCorrente.Montagem))
    '                        AddTextParameterMysql(cmd, "@Compcx", DadosArquivoCorrente.Alturacaixadelimitadora)
    '                        AddTextParameterMysql(cmd, "@Largcx", DadosArquivoCorrente.Larguracaixadelimitadora)
    '                        AddTextParameterMysql(cmd, "@Espcx", DadosArquivoCorrente.Profundidadeaixadelimitadora)
    '                        AddTextParameterMysql(cmd, "@txtItemEstoque", DadosArquivoCorrente.ItemEstoque)
    '                        AddTextParameterMysql(cmd, "@D_E_L_E_T_E", "")
    '                        AddTextParameterMysql(cmd, "@EnderecoImagem", DadosArquivoCorrente.EnderecoImagem)

    '                        Dim maxTentativas As Integer = 3 ' Quantidade máxima de tentativas
    '                        Dim tentativaAtual As Integer = 0
    '                        Dim sucesso As Boolean = False

    '                        Do While Not sucesso And tentativaAtual < maxTentativas
    '                            Try
    '                                cmd.ExecuteNonQuery()

    '                                sucesso = True ' Se chegou aqui, a execução foi bem-sucedida
    '                                ' Threading.Thread.Sleep(CInt(My.Settings.TempoRespostaServidor))
    '                            Catch ex As Exception
    '                                tentativaAtual += 1
    '                                If tentativaAtual < maxTentativas Then

    '                                    cl_BancoDados.AbrirBanco()

    '                                Else
    '                                    ' Lançar exceção após atingir o limite de tentativas
    '                                    Throw New Exception($"Falha ao executar o comando após {maxTentativas} tentativas.", ex)

    '                                    cl_BancoDados.AbrirBanco()

    '                                End If

    '                            End Try

    '                        Loop

    '                    Catch ex As Exception

    '                        Task.Run(Sub()
    '                                     ClasseEmail.EmailTratamentoErro("Erro ao atualizar ou inserir dados no banco: " & ex.Message)
    '                                 End Sub)

    '                    Finally

    '                    End Try

    '                End Using

    '            ElseIf My.Settings.TipoConexao = "SQL" Then

    '                Using cmdSQL As New SqlCommand(query, myconectSQL)

    '                    Try

    '                        ' Adicione os parâmetros ao comando
    '                        AddTextParametersql(cmdSQL, "@DescResumo", UCase(DadosArquivoCorrente.Titulo))
    '                        AddTextParametersql(cmdSQL, "@DescDetal", UCase(DadosArquivoCorrente.AssuntoSubiTitulo))
    '                        AddTextParametersql(cmdSQL, "@PecaManuFat", "S")
    '                        AddTextParametersql(cmdSQL, "@Autor", UCase(DadosArquivoCorrente.Author))
    '                        AddTextParametersql(cmdSQL, "@Palavrachave", UCase(DadosArquivoCorrente.PalavraChave))
    '                        AddTextParametersql(cmdSQL, "@Notas", UCase(DadosArquivoCorrente.Comentarios))
    '                        AddTextParametersql(cmdSQL, "@Espessura", UCase(DadosArquivoCorrente.Espessura))
    '                        AddTextParametersql(cmdSQL, "@AreaPintura", UCase(DadosArquivoCorrente.AreaPintura))
    '                        AddTextParametersql(cmdSQL, "@NumeroDobras", UCase(DadosArquivoCorrente.NumeroDobras))
    '                        AddTextParametersql(cmdSQL, "@Peso", UCase(DadosArquivoCorrente.Massa))
    '                        AddTextParametersql(cmdSQL, "@Unidade", "PC")
    '                        AddTextParametersql(cmdSQL, "@Altura", UCase(DadosArquivoCorrente.ComprimentoBlank))
    '                        AddTextParametersql(cmdSQL, "@Largura", UCase(DadosArquivoCorrente.LarguraBlank))
    '                        AddTextParametersql(cmdSQL, "@Profundidade", String.Empty)
    '                        AddTextParametersql(cmdSQL, "@CodMatFabricante", UCase(DadosArquivoCorrente.NomeArquivoSemExtensao))
    '                        AddTextParametersql(cmdSQL, "@DtCad", Date.Now.ToString("dd/MM/yyyy"))
    '                        AddTextParametersql(cmdSQL, "@UsuarioCriacao", Usuario.NomeCompleto.ToString.ToUpper)
    '                        AddTextParametersql(cmdSQL, "@UsuarioAlteracao", UCase(DadosArquivoCorrente.SalvoUltimaVezPor))
    '                        AddTextParametersql(cmdSQL, "@DtAlteracao", DadosArquivoCorrente.DataUltimoSalvamento.ToString) ' Formato ISO
    '                        AddTextParametersql(cmdSQL, "@CodigoJuridicoMat", "")
    '                        AddTextParametersql(cmdSQL, "@StatusMat", "A")
    '                        AddTextParametersql(cmdSQL, "@MaterialSW", UCase(DadosArquivoCorrente.material))
    '                        AddTextParametersql(cmdSQL, "@EnderecoArquivo", UCase(DadosArquivoCorrente.EnderecoArquivo))
    '                        AddTextParametersql(cmdSQL, "@Acabamento", UCase(DadosArquivoCorrente.Acabamento))
    '                        AddTextParametersql(cmdSQL, "@txtSoldagem", UCase(DadosArquivoCorrente.soldagem))
    '                        AddTextParametersql(cmdSQL, "@txtTipoDesenho", UCase(DadosArquivoCorrente.TipoDesenho))

    '                        AddTextParametersql(cmdSQL, "@txtCorte", UCase(DadosArquivoCorrente.Corte))
    '                        AddTextParametersql(cmdSQL, "@txtDobra", UCase(DadosArquivoCorrente.Dobra))
    '                        AddTextParametersql(cmdSQL, "@txtSolda", UCase(DadosArquivoCorrente.Solda))
    '                        AddTextParametersql(cmdSQL, "@txtPintura", UCase(DadosArquivoCorrente.Pintura))
    '                        AddTextParametersql(cmdSQL, "@txtMontagem", UCase(DadosArquivoCorrente.Montagem))
    '                        AddTextParametersql(cmdSQL, "@Compcx", DadosArquivoCorrente.Alturacaixadelimitadora)
    '                        AddTextParametersql(cmdSQL, "@Largcx", DadosArquivoCorrente.Larguracaixadelimitadora)
    '                        AddTextParametersql(cmdSQL, "@Espcx", DadosArquivoCorrente.Profundidadeaixadelimitadora)
    '                        AddTextParametersql(cmdSQL, "@txtItemEstoque", DadosArquivoCorrente.ItemEstoque)
    '                        AddTextParametersql(cmdSQL, "@D_E_L_E_T_E", "")
    '                        AddTextParametersql(cmdSQL, "@EnderecoImagem", DadosArquivoCorrente.EnderecoImagem)

    '                        ' cmd.ExecuteNonQuery()

    '                        Dim maxTentativas As Integer = 3 ' Quantidade máxima de tentativas
    '                        Dim tentativaAtual As Integer = 0
    '                        Dim sucesso As Boolean = False

    '                        Do While Not sucesso And tentativaAtual < maxTentativas
    '                            Try
    '                                cmdSQL.ExecuteNonQuery()
    '                                sucesso = True ' Se chegou aqui, a execução foi bem-sucedida
    '                                Threading.Thread.Sleep(CInt(My.Settings.TempoRespostaServidor))
    '                            Catch ex As Exception
    '                                tentativaAtual += 1
    '                                If tentativaAtual < maxTentativas Then
    '                                    '  MsgBox($"Erro na execução. Tentando novamente em 30 segundos... ({tentativaAtual}/{maxTentativas})")
    '                                    Threading.Thread.Sleep(CInt(My.Settings.TempoRespostaServidor))

    '                                    cl_BancoDados.AbrirBanco()

    '                                Else
    '                                    ' Lançar exceção após atingir o limite de tentativas
    '                                    Throw New Exception($"Falha ao executar o comando após {maxTentativas} tentativas.", ex)

    '                                    cl_BancoDados.AbrirBanco()

    '                                End If

    '                            End Try

    '                        Loop

    '                    Catch ex As Exception

    '                        ' Exibir mensagem de erro
    '                        '    MessageBox.Show("Erro ao atualizar ou inserir dados no banco: " & ex.Message)

    '                        'edson 23/04/2025 ClasseEmail.EmailTratamentoErro("Erro ao atualizar ou inserir dados no banco: " & ex.Message)

    '                        ' Chamada da função de tratamento de erro de forma assíncrona
    '                        Task.Run(Sub()
    '                                     ClasseEmail.EmailTratamentoErro("Erro ao atualizar ou inserir dados no banco: " & ex.Message)
    '                                 End Sub)

    '                    Finally

    '                    End Try

    '                End Using

    '            End If

    '            AtualizaDesenho = True

    '        Catch ex As Exception

    '            Return AtualizaDesenho
    '        Finally

    '        End Try

    '        Try

    '            'Verifica se não há pendencias em aberto do desenho, se não houver , limpa o campo RNC do material

    '            cl_BancoDados.RetornaCampoDaPesquisa("SELECT count(idordemservicoitempendencia) as rnc FROM ordemservicoitempendencia
    'where (estatus <> 'FINALIZADA') and (D_E_L_E_T_E = '' OR D_E_L_E_T_E IS NULL) AND
    'codmatfabricante = '" & UCase(DadosArquivoCorrente.NomeArquivoSemExtensao) & "'", "rnc")

    '            If VCampo0 <= 0 Then
    '                cl_BancoDados.AlteracaoEspecifica("material", "RNC", "", "CodMatFabricante", UCase(DadosArquivoCorrente.NomeArquivoSemExtensao))
    '            End If

    '        Catch ex As Exception
    '        Finally

    '        End Try

    '    End Function

    Public Function AtualizaDesenho(ByVal swModel As ModelDoc2, Optional EntradaDireta As Boolean = True) As Boolean
        AtualizaDesenho = False

        Try
            ' --------- NORMALIZA A CHAVE ---------
            Dim cod As String = UCase(Trim(DadosArquivoCorrente.NomeArquivoSemExtensao))
            If String.IsNullOrEmpty(cod) Then
                ' Sem chave -> não insere nem atualiza
                Return False
            End If
            DadosArquivoCorrente.NomeArquivoSemExtensao = cod

            ' Opcional: normalize também campos de apoio aqui…
            If EntradaDireta Then
                Try
                    If String.IsNullOrWhiteSpace(DadosArquivoCorrente.PalavraChave) Then
                        DadosArquivoCorrente.PalavraChave = DadosArquivoCorrente.EnderecoArquivo
                        swModel.SummaryInfo(swSummInfoField_e.swSumInfoKeywords) = DadosArquivoCorrente.PalavraChave
                    End If
                    If String.IsNullOrWhiteSpace(DadosArquivoCorrente.AssuntoSubiTitulo) Then
                        DadosArquivoCorrente.AssuntoSubiTitulo = DadosArquivoCorrente.NomeArquivoSemExtensao
                        swModel.SummaryInfo(swSummInfoField_e.swSumInfoSubject) = DadosArquivoCorrente.AssuntoSubiTitulo
                    End If
                    If Not String.IsNullOrWhiteSpace(DadosArquivoCorrente.NumeroDobras) Then
                        DadosArquivoCorrente.Corte = "1"
                    End If
                Catch
                    ' segue fluxo
                End Try
            End If

            ' --------- MONTAGEM DA QUERY POR SGBD ---------
            Dim isMySql As Boolean = (My.Settings.TipoConexao = "MYSQL")

            Dim cols As String =
"DescResumo, DescDetal, PecaManuFat, Autor, Palavrachave, Notas, Espessura, AreaPintura, NumeroDobras, Peso, " &
"Unidade, Altura, Largura, Profundidade, CodMatFabricante, DtCad, UsuarioCriacao, UsuarioAlteracao, DtAlteracao, CodigoJuridicoMat, " &
"StatusMat, MaterialSW, EnderecoArquivo, Acabamento, txtSoldagem, txtTipoDesenho, txtCorte, txtDobra, txtSolda, txtPintura, " &
"txtMontagem, Comprimentocaixadelimitadora, Larguracaixadelimitadora, Espessuracaixadelimitadora, txtItemEstoque, D_E_L_E_T_E, EnderecoImagem"

            Dim vals As String =
"@DescResumo, @DescDetal, @PecaManuFat, @Autor, @Palavrachave, @Notas, @Espessura, @AreaPintura, @NumeroDobras, @Peso, " &
"@Unidade, @Altura, @Largura, @Profundidade, @CodMatFabricante, @DtCad, @UsuarioCriacao, @UsuarioAlteracao, @DtAlteracao, @CodigoJuridicoMat, " &
"@StatusMat, @MaterialSW, @EnderecoArquivo, @Acabamento, @txtSoldagem, @txtTipoDesenho, @txtCorte, @txtDobra, @txtSolda, @txtPintura, " &
"@txtMontagem, @Compcx, @Largcx, @Espcx, @txtItemEstoque, @D_E_L_E_T_E, @EnderecoImagem"

            Dim setUpd As String =
"DescResumo = VALUES(DescResumo), DescDetal = VALUES(DescDetal), PecaManuFat = VALUES(PecaManuFat), Autor = VALUES(Autor), " &
"Palavrachave = VALUES(Palavrachave), Notas = VALUES(Notas), Espessura = VALUES(Espessura), AreaPintura = VALUES(AreaPintura), " &
"NumeroDobras = VALUES(NumeroDobras), Peso = VALUES(Peso), Unidade = VALUES(Unidade), Altura = VALUES(Altura), Largura = VALUES(Largura), " &
"Profundidade = VALUES(Profundidade), UsuarioAlteracao = VALUES(UsuarioAlteracao), DtAlteracao = VALUES(DtAlteracao), " &
"CodigoJuridicoMat = VALUES(CodigoJuridicoMat), StatusMat = VALUES(StatusMat), MaterialSW = VALUES(MaterialSW), " &
"EnderecoArquivo = VALUES(EnderecoArquivo), Acabamento = VALUES(Acabamento), txtSoldagem = VALUES(txtSoldagem), " &
"txtTipoDesenho = VALUES(txtTipoDesenho), txtCorte = VALUES(txtCorte), txtDobra = VALUES(txtDobra), txtSolda = VALUES(txtSolda), " &
"txtPintura = VALUES(txtPintura), txtMontagem = VALUES(txtMontagem), Comprimentocaixadelimitadora = VALUES(Comprimentocaixadelimitadora), " &
"Larguracaixadelimitadora = VALUES(Larguracaixadelimitadora), Espessuracaixadelimitadora = VALUES(Espessuracaixadelimitadora), " &
"txtItemEstoque = VALUES(txtItemEstoque), D_E_L_E_T_E = VALUES(D_E_L_E_T_E), EnderecoImagem = VALUES(EnderecoImagem)"

            Dim queryMySql As String =
$"INSERT INTO {ComplementoTipoBanco}material ({cols})
VALUES ({vals})
ON DUPLICATE KEY UPDATE
{setUpd};"

            ' MERGE para SQL Server (Upsert com chave em CodMatFabricante)
            ' Observação: se você quiser **ignorar** linhas marcadas como deletadas logicamente no UPDATE,
            ' adicione AND (t.D_E_L_E_T_E IS NULL OR t.D_E_L_E_T_E = '') na cláusula WHEN MATCHED.
            Dim querySqlServer As String =
$"MERGE {ComplementoTipoBanco}material AS t
USING (SELECT
    @DescResumo AS DescResumo, @DescDetal AS DescDetal, @PecaManuFat AS PecaManuFat, @Autor AS Autor,
    @Palavrachave AS Palavrachave, @Notas AS Notas, @Espessura AS Espessura, @AreaPintura AS AreaPintura,
    @NumeroDobras AS NumeroDobras, @Peso AS Peso, @Unidade AS Unidade, @Altura AS Altura, @Largura AS Largura,
    @Profundidade AS Profundidade, @CodMatFabricante AS CodMatFabricante, @DtCad AS DtCad, @UsuarioCriacao AS UsuarioCriacao,
    @UsuarioAlteracao AS UsuarioAlteracao, @DtAlteracao AS DtAlteracao, @CodigoJuridicoMat AS CodigoJuridicoMat,
    @StatusMat AS StatusMat, @MaterialSW AS MaterialSW, @EnderecoArquivo AS EnderecoArquivo, @Acabamento AS Acabamento,
    @txtSoldagem AS txtSoldagem, @txtTipoDesenho AS txtTipoDesenho, @txtCorte AS txtCorte, @txtDobra AS txtDobra,
    @txtSolda AS txtSolda, @txtPintura AS txtPintura, @txtMontagem AS txtMontagem,
    @Compcx AS Comprimentocaixadelimitadora, @Largcx AS Larguracaixadelimitadora, @Espcx AS Espessuracaixadelimitadora,
    @txtItemEstoque AS txtItemEstoque, @D_E_L_E_T_E AS D_E_L_E_T_E, @EnderecoImagem AS EnderecoImagem
) AS s
ON (t.CodMatFabricante = s.CodMatFabricante)
WHEN MATCHED THEN UPDATE SET
    t.DescResumo = s.DescResumo,
    t.DescDetal = s.DescDetal,
    t.PecaManuFat = s.PecaManuFat,
    t.Autor = s.Autor,
    t.Palavrachave = s.Palavrachave,
    t.Notas = s.Notas,
    t.Espessura = s.Espessura,
    t.AreaPintura = s.AreaPintura,
    t.NumeroDobras = s.NumeroDobras,
    t.Peso = s.Peso,
    t.Unidade = s.Unidade,
    t.Altura = s.Altura,
    t.Largura = s.Largura,
    t.Profundidade = s.Profundidade,
    t.UsuarioAlteracao = s.UsuarioAlteracao,
    t.DtAlteracao = s.DtAlteracao,
    t.CodigoJuridicoMat = s.CodigoJuridicoMat,
    t.StatusMat = s.StatusMat,
    t.MaterialSW = s.MaterialSW,
    t.EnderecoArquivo = s.EnderecoArquivo,
    t.Acabamento = s.Acabamento,
    t.txtSoldagem = s.txtSoldagem,
    t.txtTipoDesenho = s.txtTipoDesenho,
    t.txtCorte = s.txtCorte,
    t.txtDobra = s.txtDobra,
    t.txtSolda = s.txtSolda,
    t.txtPintura = s.txtPintura,
    t.txtMontagem = s.txtMontagem,
    t.Comprimentocaixadelimitadora = s.Comprimentocaixadelimitadora,
    t.Larguracaixadelimitadora = s.Larguracaixadelimitadora,
    t.Espessuracaixadelimitadora = s.Espessuracaixadelimitadora,
    t.txtItemEstoque = s.txtItemEstoque,
    t.D_E_L_E_T_E = s.D_E_L_E_T_E,
    t.EnderecoImagem = s.EnderecoImagem
WHEN NOT MATCHED THEN
INSERT ({cols})
VALUES ({vals});"

            ' --------- EXECUÇÃO ---------

            cl_BancoDados.AbrirBanco()

            If isMySql Then
                Using cmd As New MySqlCommand(queryMySql, myconect)
                    PreencheParametrosMySql(cmd)
                    ExecutaComRetryMySql(cmd)
                End Using
            Else
                Using cmdSQL As New SqlCommand(querySqlServer, myconectSQL)
                    PreencheParametrosSqlServer(cmdSQL)
                    ExecutaComRetrySqlServer(cmdSQL)
                End Using
            End If

            cl_BancoDados.FecharBanco()

            ' Limpa RNC se não houver pendências
            Try
                cl_BancoDados.RetornaCampoDaPesquisa(
                "SELECT count(idordemservicoitempendencia) as rnc FROM ordemservicoitempendencia " &
                "WHERE (estatus <> 'FINALIZADA') AND (D_E_L_E_T_E = '' OR D_E_L_E_T_E IS NULL) AND " &
                "codmatfabricante = '" & cod & "'", "rnc")
                If VCampo0.ToString = "" Then
                    cl_BancoDados.AlteracaoEspecifica("material", "RNC", "", "CodMatFabricante", cod)
                End If
            Catch
            End Try

            AtualizaDesenho = True
            Return True
        Catch
            Return False
        End Try
    End Function

    Private Sub PreencheParametrosMySql(cmd As MySqlCommand)
        AddTextParameterMysql(cmd, "@DescResumo", UCase(DadosArquivoCorrente.Titulo))
        AddTextParameterMysql(cmd, "@DescDetal", UCase(DadosArquivoCorrente.AssuntoSubiTitulo))
        AddTextParameterMysql(cmd, "@PecaManuFat", "S")
        AddTextParameterMysql(cmd, "@Autor", UCase(DadosArquivoCorrente.Author))
        AddTextParameterMysql(cmd, "@Palavrachave", UCase(DadosArquivoCorrente.PalavraChave))
        AddTextParameterMysql(cmd, "@Notas", UCase(DadosArquivoCorrente.Comentarios))
        AddTextParameterMysql(cmd, "@Espessura", DadosArquivoCorrente.Espessura)
        AddTextParameterMysql(cmd, "@AreaPintura", DadosArquivoCorrente.AreaPintura)
        AddTextParameterMysql(cmd, "@NumeroDobras", DadosArquivoCorrente.NumeroDobras)
        AddTextParameterMysql(cmd, "@Peso", DadosArquivoCorrente.Massa)
        AddTextParameterMysql(cmd, "@Unidade", "PC")
        AddTextParameterMysql(cmd, "@Altura", UCase(DadosArquivoCorrente.ComprimentoBlank))
        AddTextParameterMysql(cmd, "@Largura", UCase(DadosArquivoCorrente.LarguraBlank))
        AddTextParameterMysql(cmd, "@Profundidade", String.Empty)
        AddTextParameterMysql(cmd, "@CodMatFabricante", UCase(Trim(DadosArquivoCorrente.NomeArquivoSemExtensao)))
        AddTextParameterMysql(cmd, "@DtCad", Date.Now.ToString("yyyy-MM-dd"))
        AddTextParameterMysql(cmd, "@UsuarioCriacao", Usuario.NomeCompleto.ToString().ToUpper())
        AddTextParameterMysql(cmd, "@UsuarioAlteracao", UCase(DadosArquivoCorrente.SalvoUltimaVezPor))
        AddTextParameterMysql(cmd, "@DtAlteracao", Date.Now.ToString("yyyy-MM-dd"))
        AddTextParameterMysql(cmd, "@CodigoJuridicoMat", "")
        AddTextParameterMysql(cmd, "@StatusMat", "A")
        AddTextParameterMysql(cmd, "@MaterialSW", UCase(DadosArquivoCorrente.material))
        AddTextParameterMysql(cmd, "@EnderecoArquivo", UCase(DadosArquivoCorrente.EnderecoArquivo))
        AddTextParameterMysql(cmd, "@Acabamento", UCase(DadosArquivoCorrente.Acabamento))
        AddTextParameterMysql(cmd, "@txtSoldagem", UCase(DadosArquivoCorrente.soldagem))
        AddTextParameterMysql(cmd, "@txtTipoDesenho", UCase(DadosArquivoCorrente.TipoDesenho))
        AddTextParameterMysql(cmd, "@txtCorte", UCase(DadosArquivoCorrente.Corte))
        AddTextParameterMysql(cmd, "@txtDobra", UCase(DadosArquivoCorrente.Dobra))
        AddTextParameterMysql(cmd, "@txtSolda", UCase(DadosArquivoCorrente.Solda))
        AddTextParameterMysql(cmd, "@txtPintura", UCase(DadosArquivoCorrente.Pintura))
        AddTextParameterMysql(cmd, "@txtMontagem", UCase(DadosArquivoCorrente.Montagem))
        AddTextParameterMysql(cmd, "@Compcx", DadosArquivoCorrente.Alturacaixadelimitadora)
        AddTextParameterMysql(cmd, "@Largcx", DadosArquivoCorrente.Larguracaixadelimitadora)
        AddTextParameterMysql(cmd, "@Espcx", DadosArquivoCorrente.Profundidadeaixadelimitadora)
        AddTextParameterMysql(cmd, "@txtItemEstoque", DadosArquivoCorrente.ItemEstoque)
        AddTextParameterMysql(cmd, "@D_E_L_E_T_E", "")
        AddTextParameterMysql(cmd, "@EnderecoImagem", DadosArquivoCorrente.EnderecoImagem)
    End Sub

    Private Sub PreencheParametrosSqlServer(cmd As SqlCommand)
        AddTextParametersql(cmd, "@DescResumo", UCase(DadosArquivoCorrente.Titulo))
        AddTextParametersql(cmd, "@DescDetal", UCase(DadosArquivoCorrente.AssuntoSubiTitulo))
        AddTextParametersql(cmd, "@PecaManuFat", "S")
        AddTextParametersql(cmd, "@Autor", UCase(DadosArquivoCorrente.Author))
        AddTextParametersql(cmd, "@Palavrachave", UCase(DadosArquivoCorrente.PalavraChave))
        AddTextParametersql(cmd, "@Notas", UCase(DadosArquivoCorrente.Comentarios))
        AddTextParametersql(cmd, "@Espessura", UCase(DadosArquivoCorrente.Espessura))
        AddTextParametersql(cmd, "@AreaPintura", UCase(DadosArquivoCorrente.AreaPintura))
        AddTextParametersql(cmd, "@NumeroDobras", UCase(DadosArquivoCorrente.NumeroDobras))
        AddTextParametersql(cmd, "@Peso", UCase(DadosArquivoCorrente.Massa))
        AddTextParametersql(cmd, "@Unidade", "PC")
        AddTextParametersql(cmd, "@Altura", UCase(DadosArquivoCorrente.ComprimentoBlank))
        AddTextParametersql(cmd, "@Largura", UCase(DadosArquivoCorrente.LarguraBlank))
        AddTextParametersql(cmd, "@Profundidade", String.Empty)
        AddTextParametersql(cmd, "@CodMatFabricante", UCase(Trim(DadosArquivoCorrente.NomeArquivoSemExtensao)))
        AddTextParametersql(cmd, "@DtCad", Date.Now.ToString("yyyy-MM-dd"))
        AddTextParametersql(cmd, "@UsuarioCriacao", Usuario.NomeCompleto.ToString().ToUpper())
        AddTextParametersql(cmd, "@UsuarioAlteracao", UCase(DadosArquivoCorrente.SalvoUltimaVezPor))
        AddTextParametersql(cmd, "@DtAlteracao", DadosArquivoCorrente.DataUltimoSalvamento.ToString("yyyy-MM-dd HH:mm:ss"))
        AddTextParametersql(cmd, "@CodigoJuridicoMat", "")
        AddTextParametersql(cmd, "@StatusMat", "A")
        AddTextParametersql(cmd, "@MaterialSW", UCase(DadosArquivoCorrente.material))
        AddTextParametersql(cmd, "@EnderecoArquivo", UCase(DadosArquivoCorrente.EnderecoArquivo))
        AddTextParametersql(cmd, "@Acabamento", UCase(DadosArquivoCorrente.Acabamento))
        AddTextParametersql(cmd, "@txtSoldagem", UCase(DadosArquivoCorrente.soldagem))
        AddTextParametersql(cmd, "@txtTipoDesenho", UCase(DadosArquivoCorrente.TipoDesenho))
        AddTextParametersql(cmd, "@txtCorte", UCase(DadosArquivoCorrente.Corte))
        AddTextParametersql(cmd, "@txtDobra", UCase(DadosArquivoCorrente.Dobra))
        AddTextParametersql(cmd, "@txtSolda", UCase(DadosArquivoCorrente.Solda))
        AddTextParametersql(cmd, "@txtPintura", UCase(DadosArquivoCorrente.Pintura))
        AddTextParametersql(cmd, "@txtMontagem", UCase(DadosArquivoCorrente.Montagem))
        AddTextParametersql(cmd, "@Compcx", DadosArquivoCorrente.Alturacaixadelimitadora)
        AddTextParametersql(cmd, "@Largcx", DadosArquivoCorrente.Larguracaixadelimitadora)
        AddTextParametersql(cmd, "@Espcx", DadosArquivoCorrente.Profundidadeaixadelimitadora)
        AddTextParametersql(cmd, "@txtItemEstoque", DadosArquivoCorrente.ItemEstoque)
        AddTextParametersql(cmd, "@D_E_L_E_T_E", "")
        AddTextParametersql(cmd, "@EnderecoImagem", DadosArquivoCorrente.EnderecoImagem)
    End Sub

    Private Sub ExecutaComRetryMySql(cmd As MySqlCommand)
        Dim maxTentativas = 3, tentativa = 0
        Dim sucesso = False
        Do While Not sucesso AndAlso tentativa < maxTentativas
            Try
                cmd.ExecuteNonQuery()
                sucesso = True
            Catch
                tentativa += 1
                If tentativa >= maxTentativas Then Throw
                cl_BancoDados.AbrirBanco()
            End Try
        Loop
    End Sub

    Private Sub ExecutaComRetrySqlServer(cmd As SqlCommand)
        Dim maxTentativas = 3, tentativa = 0
        Dim sucesso = False
        Do While Not sucesso AndAlso tentativa < maxTentativas
            Try
                cmd.ExecuteNonQuery()
                sucesso = True
            Catch
                tentativa += 1
                If tentativa >= maxTentativas Then Throw
                cl_BancoDados.AbrirBanco()
            End Try
        Loop
    End Sub

    Public Function AddTextParameterMysql(ByRef cmd As MySqlCommand, ByVal paramName As String, ByVal value As Object) As Boolean

        Try

            cmd.Parameters.AddWithValue(paramName, If(value Is Nothing, String.Empty, value.ToString().Trim()))
            Return True
        Catch ex As Exception
            Return False

        End Try

    End Function

    ' Função para adicionar parâmetros como texto
    Public Function AddTextParametersql(ByRef cmd As SqlCommand, ByVal paramName As String, ByVal value As Object) As Boolean

        Try

            cmd.Parameters.AddWithValue(paramName, If(value Is Nothing, String.Empty, value.ToString().Trim()))
            Return True
        Catch ex As Exception
            Return False

        End Try

    End Function

    Public Function ExportDXF2(ByVal swModel As ModelDoc2, ByVal ManterAberto As Boolean, ByVal ExcluirLxds As Boolean) As Boolean

        Try
            ExportDXF2 = False
            ExcluirLxds = False

            If swModel Is Nothing Then
                Throw New ArgumentNullException("swModel", "O modelo passado para ExportDXF é nulo.")
            End If

            ' Verifica se o modelo é uma peça (PART)
            If swModel.GetType() <> swDocumentTypes_e.swDocPART Then
                Throw New InvalidOperationException("O documento não é uma peça de chapa metálica.")
            End If

            Dim swPart As PartDoc = swModel
            Dim sModelName As String = swModel.GetPathName()
            Dim sPathName As String = Left(sModelName, Len(sModelName) - 6) & "dxf"

            ' Verifica se a peça contém chapa metálica
            If Not IsSheetMetalPart(swModel) Then
                Throw New InvalidOperationException("A peça não contém uma feature de chapa metálica.")
            End If

            ' Acessa a feature de Flat-Pattern diretamente
            Dim swFlatPatternFeature As Feature = swPart.FeatureByName("Flat-Pattern")

            ' Se a feature de Flat-Pattern não existir ou não estiver visível, forçar a planificação
            If swFlatPatternFeature Is Nothing OrElse swFlatPatternFeature.IsSuppressed() Then
                Try
                    swPart.EditUnsuppressFeature(swFlatPatternFeature)
                Catch ex As Exception
                    Debug.Print("Erro ao dessuprimir a Flat-Pattern: " & ex.Message)
                End Try
            End If

            '' Exclui arquivos anteriores (DXF e LXDS)
            'Try
            '    If File.Exists(sPathName) Then File.Delete(sPathName)
            '    If ExcluirLxds Then
            '        Dim lxdsPath As String = Left(sModelName, Len(sModelName) - 6) & "lxds"
            '        If File.Exists(lxdsPath) Then File.Delete(lxdsPath)
            '    End If
            'Catch ex As Exception
            '    Debug.Print("Erro ao excluir arquivos anteriores: " & ex.Message)
            'End Try

            If File.Exists(sPathName) = False Then

                File.Delete(sPathName)
                ' If ExcluirLxds Then
                Dim lxdsPath As String = Left(sModelName, Len(sModelName) - 6) & "lxds"
                If File.Exists(lxdsPath) Then File.Delete(lxdsPath)
                ' End If

            ElseIf File.Exists(sPathName) = True And BloqueaArquivoExistente = False Then

                File.Delete(sPathName)
                ' Apaga o LXDS se solicitado
                ' If ExcluirLxds Then
                Dim lxdsPath As String = Left(sModelName, Len(sModelName) - 6) & "lxds"
                If File.Exists(lxdsPath) Then File.Delete(lxdsPath)
                ' End If

            ElseIf File.Exists(sPathName) = True And BloqueaArquivoExistente = True Then

                Exit Function

            End If

            ' Exportação DXF
            Try
                Dim varAlignment As Object = {0.0#, 0.0#, 0.0#, 1.0#, 0.0#, 0.0#, 0.0#, 1.0#, 0.0#, 0.0#, 0.0#, 1.0#}
                Dim varViews As Object = {"*Current", "*Front"}
                Dim options As Integer = 1 ' Configuração de exportação

                ' Exporta DXF
                Dim exportSuccess As Boolean = swPart.ExportToDWG2(sPathName, sModelName, swExportToDWG_e.swExportToDWG_ExportSheetMetal, True, varAlignment, False, False, options, Nothing)
                If Not exportSuccess Then
                    Throw New Exception("Falha ao exportar DXF.")
                End If
            Catch ex As Exception
                Debug.Print("Erro ao exportar DXF: " & ex.Message)
                ClasseEmail.EmailTratamentoErro("Erro na exportação DXF: " & ex.Message)
            End Try

            ' Fecha o documento se necessário
            If Not ManterAberto Then
                Try
                    swapp.CloseDoc(sModelName)
                Catch ex As Exception
                    Debug.Print("Erro ao fechar o documento: " & ex.Message)
                End Try
            End If

            ExportDXF2 = True
        Catch ex As ArgumentNullException
            ' MsgBox("Erro: " & ex.Message, MsgBoxStyle.Exclamation, "Erro de Parâmetro")
        Catch ex As InvalidOperationException
            '  MsgBox("Erro: " & ex.Message, MsgBoxStyle.Exclamation, "Erro de Operação")
        Catch ex As IOException
            ' MsgBox("Erro ao acessar arquivos: " & ex.Message, MsgBoxStyle.Exclamation, "Erro de Arquivo")
        Catch ex As Exception
            ' Registra erro sem fechar o SolidWorks
            '  MsgBox("Erro inesperado: " & ex.Message, MsgBoxStyle.Critical, "Erro")
            '  Debug.Print("Erro inesperado: " & ex.Message)
            ClasseEmail.EmailTratamentoErro("Erro inesperado: " & ex.Message)
        End Try

        ' Return ExportDXF2
    End Function

    Public Function ExportDXF(ByVal swModel As ModelDoc2, ByVal ManterAberto As Boolean, ByVal ExcluirLxds As Boolean) As Boolean

        ExportDXF = False

        Try
            If swModel Is Nothing Then Throw New Exception("Modelo SW está nulo.")
            If swModel.GetType() <> swDocumentTypes_e.swDocPART Then Throw New Exception("Documento não é uma peça.")

            Dim sModelName As String = swModel.GetPathName()
            If String.IsNullOrWhiteSpace(sModelName) Then Throw New Exception("O caminho do arquivo está vazio. Salve o arquivo primeiro.")

            Dim sPathName As String = Path.ChangeExtension(sModelName, "dxf")
            Dim sPathLxds As String = Path.ChangeExtension(sModelName, "lxds")

            ' Exclusão de arquivos anteriores, se permitido
            If File.Exists(sPathName) Then
                If BloqueaArquivoExistente Then Exit Function
                File.Delete(sPathName)
            End If

            If ExcluirLxds AndAlso File.Exists(sPathLxds) Then
                File.Delete(sPathLxds)
            End If

            ' Prepara alinhamento e vistas
            Dim varAlignment As Object = {
            0.0, 0.0, 0.0,
            1.0, 0.0, 0.0,
            0.0, 1.0, 0.0,
            0.0, 0.0, 1.0
        }

            Dim varViews As Object = {"*Current", "*Front"}

            Dim swPart As PartDoc = CType(swModel, PartDoc)
            Dim options As Integer = 1 ' Flat pattern

            ' Exportação
            Dim exportou As Boolean = swPart.ExportToDWG2(
            sPathName,
            sModelName,
            swExportToDWG_e.swExportToDWG_ExportSheetMetal,
            True,
            varAlignment,
            False,
            False,
            options,
            Nothing
        )

            If Not exportou Then Throw New Exception("Falha ao exportar o DXF. Verifique se a peça é chapa metálica e está com flat pattern gerado.")

            ' Fecha o modelo se necessário
            If Not ManterAberto Then
                Dim swApp As SldWorks = CType(swModel.GetSwApp(), SldWorks)
                If swApp IsNot Nothing Then
                    swApp.CloseDoc(swModel.GetTitle())
                End If
            End If

            ExportDXF = True
        Catch ex As Exception
            ' ClasseEmail.EmailTratamentoErro("Erro ao exportar DXF: " & ex.Message)
        Finally
        End Try

    End Function

    Public Function ExportDXFFerramentaConformacao(ByVal swModel As ModelDoc2, ByVal ManterAberto As Boolean, Optional ByVal FerramentaConformacao As Boolean = False) As Boolean

        '' Exportação padrão (sem ferramenta de conformação):
        'ExportDXF(swModel, False, True)

        '' Exportação como ferramenta de conformação:
        'ExportDXF(swModel, False, True, True)

        ExportDXFFerramentaConformacao = False

        Try
            If swModel Is Nothing Then Throw New Exception("Modelo SW está nulo.")
            If swModel.GetType() <> swDocumentTypes_e.swDocPART Then Throw New Exception("Documento não é uma peça.")

            Dim sModelName As String = swModel.GetPathName()
            If String.IsNullOrWhiteSpace(sModelName) Then Throw New Exception("O caminho do arquivo está vazio. Salve o arquivo primeiro.")

            Dim sPathName As String = Path.ChangeExtension(sModelName, "dxf")
            Dim sPathLxds As String = Path.ChangeExtension(sModelName, "lxds")

            If DadosArquivoCorrente.Bloqueado = "S" Then

                Exit Function

            End If

            ' Exclusão de arquivos anteriores, se permitido
            If File.Exists(sPathName) = True Then 'Verifica DXF
                If BloqueaArquivoExistente = False Then
                    File.Delete(sPathName)
                    File.Delete(sPathLxds)

                    ExportDXFFerramentaConformacao = True
                Else

                    Exit Function

                End If

            End If

            'If ExcluirLxds AndAlso File.Exists(sPathLxds) Then
            '    File.Delete(sPathLxds)
            'End If

            ' Prepara alinhamento e vistas
            Dim varAlignment As Object = {
            0.0, 0.0, 0.0,
            1.0, 0.0, 0.0,
            0.0, 1.0, 0.0,
            0.0, 0.0, 1.0
        }

            Dim varViews As Object = {"*Current", "*Front"}

            Dim swPart As PartDoc = CType(swModel, PartDoc)

            ' Define opção de exportação: 1 = Flat, 2 = Ferramenta de Conformação
            Dim options As Integer = If(FerramentaConformacao, 2, 1)

            ' Exportação
            Dim exportou As Boolean = swPart.ExportToDWG2(
            sPathName,
            sModelName,
            swExportToDWG_e.swExportToDWG_ExportSheetMetal,
            True,
            varAlignment,
            False,
            False,
            options,
            Nothing
        )

            If Not exportou Then Throw New Exception("Falha ao exportar o DXF. Verifique se a peça é chapa metálica e está com flat pattern gerado.")

            ' Fecha o modelo se necessário
            If Not ManterAberto Then
                Dim swApp As SldWorks = CType(swModel.GetSwApp(), SldWorks)
                If swApp IsNot Nothing Then
                    swApp.CloseDoc(swModel.GetTitle())
                End If
            End If

            ExportDXFFerramentaConformacao = True
        Catch ex As Exception
            ' ClasseEmail.EmailTratamentoErro("Erro ao exportar DXF: " & ex.Message)
        End Try

    End Function

    Private Function IsSheetMetalPart(ByVal swModel As ModelDoc2) As Boolean
        Dim swFeature As Feature = swModel.FirstFeature

        ' Itera pelas features da peça
        While Not swFeature Is Nothing
            ' Verifica se a feature é uma feature de chapa metálica
            If swFeature.GetTypeName2() = "SheetMetal" Then
                Return True ' Encontrou uma feature de chapa metálica
            End If
            swFeature = swFeature.GetNextFeature()
        End While

        ' Retorna False se nenhuma feature de chapa metálica for encontrada
        Return False
    End Function

    Public Function ExportToPDF(ByVal swModel As ModelDoc2, ByVal filePath As String, ByVal ManterAberto As Boolean) As Boolean

        IntanciaSolidWorks.ConectarSolidWorks()

        swModel = swapp.ActiveDoc

        Try

            If Not swModel Is Nothing Then
                ' Exporta o arquivo para PDF
                Dim swModelDocExt As ModelDocExtension = swModel.Extension
                Dim pdfFilePath As String = Path.ChangeExtension(filePath, ".pdf")

                If DadosArquivoCorrente.Bloqueado = "S" Then

                    Exit Function

                End If

                ' Exclusão de arquivos anteriores, se permitido
                If File.Exists(pdfFilePath) = True Then 'Verifica pdf
                    If BloqueaArquivoExistente = False Then
                        File.Delete(pdfFilePath)
                    Else

                        Exit Function

                    End If

                End If

                Dim pdfExportData As ExportPdfData = swapp.GetExportFileData(swExportDataFileType_e.swExportPdfData)
                Dim v = pdfExportData.SetSheets(1, True)

                ' Exportar o arquivo para PDF sem abrir o leitor de PDF
                swModelDocExt.SaveAs(pdfFilePath,
                                 swSaveAsVersion_e.swSaveAsCurrentVersion,
                                 swSaveAsOptions_e.swSaveAsOptions_Silent, ' Salvamento silencioso
                                 Nothing, 0, 0)

                Threading.Thread.Sleep(CInt(My.Settings.TempoRespostaServidor))

                '  swModel.SaveAs(pdfFilePath)

                If ManterAberto = False Then

                    swapp.CloseDoc(filePath)

                End If

                ExportToPDF = True

            End If
        Catch ex As Exception
        Finally
        End Try

    End Function

    Public Function TrocarFormatoA3(ByVal swModel As ModelDoc2) As Boolean

        If BloqueaArquivoExistente = True Then
            Exit Function
        End If

        If File.Exists(My.Settings.EnderecoNovoFormatoA3) = False Then
            MsgBox("O Arquivo padrão deve ser selecionado antes de executar a operação!", vbCritical, "Atenção")
        Else
            Try
                Dim swModelDocExt As ModelDocExtension
                Dim swDrawing As DrawingDoc
                Dim fileName As String
                Dim swSheet As Sheet
                Dim swSheetNames As Object
                Dim i As Integer
                Dim swSheetName As String

                IntanciaSolidWorks.ConectarSolidWorks()

                swModel = swapp.ActiveDoc
                swModel.Visible = True
                swModelDocExt = swModel.Extension
                swDrawing = CType(swModel, DrawingDoc)

                ' Obtem o caminho atual do desenho
                fileName = swModel.GetPathName.ToString

                ' Reabre o documento como Drawing
                swModel = swapp.OpenDoc6(fileName, swDocumentTypes_e.swDocDRAWING, swOpenDocOptions_e.swOpenDocOptions_LoadModel, "", 0, 0)
                swModelDocExt = swModel.Extension
                swDrawing = CType(swModel, DrawingDoc)

                ' Pega todos os nomes de folhas
                swSheetNames = swDrawing.GetSheetNames

                'For i = 0 To UBound(swSheetNames)
                '    swSheetName = swSheetNames(i)

                '    ' Seleciona a folha atual
                '    swDrawing.ActivateSheet(swSheetName)

                '    ' Aplica o novo formato
                '    swDrawing.SetupSheet6(swSheetName,
                '                  swDwgPaperSizes_e.swDwgPapersUserDefined,
                '                  swDwgTemplates_e.swDwgTemplateCustom,
                '                  0, 0,
                '                  True,
                '                  My.Settings.EnderecoNovoFormatoA3.ToString,
                '                  0.385, 0.277,
                '                  "Default",
                '                  True,
                '                  0, 0, 0, 0, 0, 0)
                'Next

                For i = 0 To UBound(swSheetNames)
                    swSheetName = swSheetNames(i)

                    ' Seleciona a folha atual
                    swDrawing.ActivateSheet(swSheetName)

                    ' Obtém os dados da folha atual
                    Dim sheet As Sheet = swDrawing.GetCurrentSheet()
                    Dim currentTemplate As String = sheet.GetTemplateName()
                    Dim sheetWidth As Double, sheetHeight As Double
                    sheet.GetSize(sheetWidth, sheetHeight)

                    ' Verifica se o template e tamanho já correspondem ao novo formato
                    Dim isAlreadyFormatted As Boolean =
        currentTemplate.ToLower().Contains(My.Settings.EnderecoNovoFormatoA3.ToString.ToLower()) AndAlso
        Math.Abs(sheetWidth - 0.385) < 0.001 AndAlso
        Math.Abs(sheetHeight - 0.277) < 0.001

                    If Not isAlreadyFormatted Then
                        ' Aplica o novo formato
                        swDrawing.SetupSheet6(swSheetName,
                              swDwgPaperSizes_e.swDwgPapersUserDefined,
                              swDwgTemplates_e.swDwgTemplateCustom,
                              0, 0,
                              True,
                              My.Settings.EnderecoNovoFormatoA3.ToString,
                              0.385, 0.277,
                              "Default",
                              True,
                              0, 0, 0, 0, 0, 0)
                    End If
                Next

                swModel.ForceRebuild3(True)
                swModel.ViewZoomtofit2()
                swModel.GraphicsRedraw2()
                swModel.Save3(CInt(swSaveAsOptions_e.swSaveAsOptions_SaveReferenced), 0, 0)
                swModel.Save()
            Catch ex As Exception
                '   MsgBox("Erro: " & ex.Message)
            Finally
                ' Qualquer limpeza aqui
            End Try
        End If

    End Function

    Public Sub SalvarMaterialDesenho(ByVal CodMatFabricante As String,
                                     ByVal TipoPeca As String,
                                     ByVal IdMaterial As String,
                                     ByVal PecaQtde As String,
                                     ByVal IdMaterialPeca As String,
                                     ByVal Peso As String,
                                     ByVal Valor As String,
                                     ByVal UsuarioCriacao As String,
                                     ByVal DataCriacao As String,
                                     ByVal vICMSCalculado As String,
                                     ByVal vIPICalculado As String,
                                     ByVal PercIPICalculado As String,
                                     ByVal PercICMSCalculado As String,
                                     ByVal Unidade As String)
        Dim query As String

        cl_BancoDados.AbrirBanco()

        If My.Settings.TipoConexao = "MYSQL" Then

            Try

                Using cmd As New MySqlCommand("insert into  " & ComplementoTipoBanco & "montapeca
                                             (CodMatFabricante,
                                              TipoPeca,
                                              IdMaterial,
                                              PecaQtde,
                                              IdMaterialPeca,
                                              Peso,
                                              Valor,
                                              UsuarioCriacao,
                                              DataCriacao,
                                              vICMSCalculado,
                                              vIPICalculado,
                                              PercIPICalculado,
                                              PercICMSCalculado,
                                              Unidade)
                                            values
                                              (@CodMatFabricante,
                                              @TipoPeca,
                                              @IdMaterial,
                                              @PecaQtde,
                                              @IdMaterialPeca,
                                              @Peso,
                                              @Valor,
                                              @UsuarioCriacao,
                                              @DataCriacao,
                                              @vICMSCalculado,
                                              @vIPICalculado,
                                              @PercIPICalculado,
                                              @PercICMSCalculado,
                                              @Unidade)", myconect)

                    cmd.Parameters.AddWithValue("@CodMatFabricante", CodMatFabricante)
                    cmd.Parameters.AddWithValue("@TipoPeca", TipoPeca)
                    cmd.Parameters.AddWithValue("@IdMaterial", IdMaterial)
                    cmd.Parameters.AddWithValue("@PecaQtde", PecaQtde)
                    cmd.Parameters.AddWithValue("@IdMaterialPeca", IdMaterialPeca)
                    cmd.Parameters.AddWithValue("@Peso", Peso)
                    cmd.Parameters.AddWithValue("@Valor", Valor)
                    cmd.Parameters.AddWithValue("@UsuarioCriacao", UsuarioCriacao)
                    cmd.Parameters.AddWithValue("@DataCriacao", DataCriacao)
                    cmd.Parameters.AddWithValue("@vICMSCalculado", vICMSCalculado)
                    cmd.Parameters.AddWithValue("@vIPICalculado", vIPICalculado)
                    cmd.Parameters.AddWithValue("@PercIPICalculado", PercIPICalculado)
                    cmd.Parameters.AddWithValue("@PercICMSCalculado", PercICMSCalculado)
                    cmd.Parameters.AddWithValue("@Unidade", Unidade)

                    cmd.ExecuteNonQuery()

                End Using
            Catch ex As Exception

                '   MsgBox(ex.Message)
            Finally

            End Try

        ElseIf My.Settings.TipoConexao = "SQL" Then

            Using cmd As New SqlCommand("insert into  " & ComplementoTipoBanco & "montapeca
                                             (CodMatFabricante,TipoPeca,IdMaterial, PecaQtde, IdMaterialPeca, Peso, Valor, UsuarioCriacao, DataCriacao)
                                              values
                                             (@CodMatFabricante,@TipoPeca,@IdMaterial, @PecaQtde, @IdMaterialPeca, @Peso, @Valor, @UsuarioCriacao, @DataCriacao)", myconectSQL)

                cmd.Parameters.Add("@CodMatFabricante", CodMatFabricante)
                cmd.Parameters.Add("@TipoPeca", TipoPeca)
                cmd.Parameters.Add("@IdMaterial", IdMaterial)
                cmd.Parameters.Add("@PecaQtde", PecaQtde)
                cmd.Parameters.Add("@IdMaterialPeca", IdMaterialPeca)
                cmd.Parameters.Add("@Peso", Peso)
                cmd.Parameters.Add("@Valor", Valor)
                cmd.Parameters.Add("@UsuarioCriacao", UsuarioCriacao)
                cmd.Parameters.Add("@DataCriacao", DataCriacao)

                cmd.ExecuteNonQuery()
            End Using

        End If

        cl_BancoDados.FecharBanco()

    End Sub

    Public Sub TrocarFormatoA3EmTodosOsDesenhos(ByVal caminhoPasta As String, ByVal swModel As ModelDoc2)

        ' Agora busca arquivos em todos os subdiretórios
        Dim arquivos As String() = Directory.GetFiles(caminhoPasta, "*.slddrw", SearchOption.AllDirectories)

        Dim swDrawing As DrawingDoc = Nothing
        Dim swSheetNames As Object
        Dim i As Integer

        swapp.Visible = True

        For Each arquivo As String In arquivos
            Try
                Dim erros As Integer = 0
                Dim avisos As Integer = 0

                swModel = swapp.OpenDoc6(arquivo,
                                         swDocumentTypes_e.swDocDRAWING,
                                         swOpenDocOptions_e.swOpenDocOptions_Silent,
                                         "",
                                         erros,
                                         avisos)

                If swModel IsNot Nothing Then
                    swDrawing = CType(swModel, DrawingDoc)
                    swSheetNames = swDrawing.GetSheetNames

                    For i = 0 To UBound(swSheetNames)
                        Dim nomeFolha As String = swSheetNames(i)

                        swDrawing.ActivateSheet(nomeFolha)

                        swDrawing.SetupSheet6(nomeFolha,
                                              swDwgPaperSizes_e.swDwgPapersUserDefined,
                                              swDwgTemplates_e.swDwgTemplateCustom,
                                              0, 0,
                                              True,
                                              My.Settings.EnderecoNovoFormatoA3,
                                              0.385, 0.277,
                                              "Default",
                                              True,
                                              0, 0, 0, 0, 0, 0)
                    Next

                    swModel.ForceRebuild3(True)
                    swModel.ViewZoomtofit2()
                    swModel.GraphicsRedraw2()
                    swModel.Save3(CInt(swSaveAsOptions_e.swSaveAsOptions_SaveReferenced), 0, 0)

                    DadosArquivoCorrente.ExportToPDF(swModel, arquivo, False)

                    swModel.Save()

                    swapp.CloseDoc(swModel.GetTitle)
                End If
            Catch ex As Exception
                '  MsgBox("Erro ao processar: " & arquivo & vbCrLf & ex.Message)
            End Try
        Next

        MsgBox("Processo concluído com sucesso!", vbInformation)
    End Sub

End Class

Public Class ClSolidWorks

    Public Function ConectarSolidWorks() As Boolean
        Try
            ' Tenta obter uma instância ativa do SolidWorks
            swapp = TryCast(Marshal.GetActiveObject("SldWorks.Application"), SldWorks)

            ' Se não conseguir obter uma instância ativa, tenta criar uma nova
            If swapp Is Nothing Then
                swapp = TryCast(CreateObject("SldWorks.Application"), SldWorks)
            End If

            ' Verifica se obteve uma instância válida
            If swapp Is Nothing Then
                Debug.WriteLine("Erro: Não foi possível obter ou criar uma instância do SolidWorks.")
                Return False
            End If

            Return True
        Catch ex As Exception
            Debug.WriteLine("Erro ao conectar ao SolidWorks: " & ex.Message)
            Return False
        Finally
            ' Libera o objeto COM (importante!)
            'If Not swapp Is Nothing Then
            '    Marshal.ReleaseComObject(swapp) ' Não libera aqui, pois swapp é usado fora da função
            'End If
        End Try
    End Function

    ' Função para liberar os objetos COM do SolidWorks
    Public Function LiberarRecurso(ByVal SwModel As ModelDoc2) As Boolean
        If SwModel IsNot Nothing Then
            Marshal.ReleaseComObject(SwModel)
        End If
        SwModel = Nothing
    End Function

End Class

Public Class clPdf

    Public Sub EscreverPdf(EnderecoCompleto As String, caminhoArquivoDestino As String, Parametro As String)
        Try
            ' Substitui extensões ".sldprt" ou ".sldasm" por ".pdf"
            Dim inputPdf As String = System.Text.RegularExpressions.Regex.Replace(EnderecoCompleto, ".sldprt$|.sldasm$", ".pdf", System.Text.RegularExpressions.RegexOptions.IgnoreCase)

            ' Verifica se o arquivo de entrada existe
            If Not System.IO.File.Exists(inputPdf) Then
                MsgBox("O arquivo PDF de entrada não existe: " & inputPdf)
                Exit Sub
            End If

            ' Extrai o nome do arquivo de entrada
            Dim fileName As String = System.IO.Path.GetFileName(inputPdf)

            ' Monta o caminho do arquivo de saída corretamente
            Dim outputPdf As String = System.IO.Path.Combine(caminhoArquivoDestino, "qtde_" & fileName)

            ' Verifica ou cria o diretório de saída
            Dim outputDirectory As String = System.IO.Path.GetDirectoryName(outputPdf)
            If Not System.IO.Directory.Exists(outputDirectory) Then
                System.IO.Directory.CreateDirectory(outputDirectory)
            End If

            ' Inicializa o PdfReader
            Dim pdfReader As iText.Kernel.Pdf.PdfReader = Nothing
            Try
                pdfReader = New iText.Kernel.Pdf.PdfReader(inputPdf)
            Catch ex As Exception
                '  MsgBox("Erro ao abrir o arquivo PDF de entrada: " & ex.Message)
                Exit Sub
            End Try

            ' Inicializa o PdfWriter e PdfDocument
            Dim pdfWriter As New iText.Kernel.Pdf.PdfWriter(outputPdf)
            Dim pdfDocument As New iText.Kernel.Pdf.PdfDocument(pdfReader, pdfWriter)

            ' Obtém a primeira página do PDF
            Dim page As iText.Kernel.Pdf.PdfPage = pdfDocument.GetFirstPage()
            Dim canvas As New iText.Kernel.Pdf.Canvas.PdfCanvas(page)

            ' Configura o texto a ser inserido
            Dim font As iText.Kernel.Font.PdfFont = iText.Kernel.Font.PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA)
            canvas.BeginText()
            canvas.SetFontAndSize(font, 12)
            canvas.MoveText(50, 800) ' Define a posição do texto
            canvas.ShowText($"OS: {Parametro} - Data de Emissão do Desenho: {DateTime.Now:dd/MM/yyyy}")
            canvas.EndText()

            ' Fecha o documento PDF
            pdfDocument.Close()

            MsgBox("Texto inserido com sucesso no arquivo: " & outputPdf)
        Catch ex As Exception
            '  MsgBox("Erro ao processar o PDF: " & ex.Message & vbCrLf & ex.StackTrace)
            If ex.InnerException IsNot Nothing Then
                MsgBox("Erro interno: " & ex.InnerException.Message)
            End If
        End Try
    End Sub

End Class