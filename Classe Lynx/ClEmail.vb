Imports System.Net
Imports System.Net.Mail
Imports System.Text.RegularExpressions
Imports System.Windows.Forms

Public Class clEmail

    Public Function EnviarEmail(
    ByVal emailEnvio As String,
    ByVal nomeEnvio As String,
    ByVal emailDestino As String,
    ByVal nomeDestino As String,
    ByVal smtp As String,
    ByVal porta As String,
    ByVal senha As String,
    ByVal assunto As String,
    ByVal mensagem As String,
    Optional ByVal enderecoAnexo As String = Nothing
) As Boolean
        ' Verificar parâmetros obrigatórios
        If String.IsNullOrWhiteSpace(emailEnvio) OrElse
       String.IsNullOrWhiteSpace(emailDestino) OrElse
       String.IsNullOrWhiteSpace(smtp) OrElse
       String.IsNullOrWhiteSpace(porta) OrElse
       String.IsNullOrWhiteSpace(senha) Then
            MessageBox.Show("Todos os campos obrigatórios devem ser preenchidos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        Try
            ' Criar objeto MailMessage
            Using objEmail As New MailMessage()
                ' Configurar remetente e destinatário
                objEmail.From = New MailAddress(emailEnvio, nomeEnvio)
                objEmail.To.Add(New MailAddress(emailDestino, nomeDestino))

                ' Configurar assunto e mensagem
                objEmail.Subject = assunto
                objEmail.Body = mensagem
                objEmail.IsBodyHtml = False ' Configurar como texto simples
                objEmail.Priority = MailPriority.High

                ' Adicionar anexo, se houver
                If Not String.IsNullOrWhiteSpace(enderecoAnexo) Then
                    objEmail.Attachments.Add(New Attachment(enderecoAnexo))
                End If

                ' Configurar cliente SMTP
                Using objEnvio As New SmtpClient(smtp, Convert.ToInt32(porta))
                    objEnvio.Credentials = New NetworkCredential(emailEnvio, senha)
                    '  objEnvio.EnableSsl = True 'Alguns servidores de e-mail podem não suportar SSL/TLS na versão que está sendo usada. Teste desativando o SSL:
                    objEnvio.EnableSsl = False
                    objEnvio.Timeout = 100000 ' Ajustável conforme necessário

                    ' objEnvio.UseDefaultCredentials = True '- falta testar

                    ' Enviar o e-mail
                    objEnvio.Send(objEmail)

                    ' System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

                End Using
            End Using

            ' Retornar sucesso
            Return True
        Catch ex As SmtpException
            ' MessageBox.Show($"Erro SMTP: {ex.InnerException }", "Erro de Envio", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As ArgumentException
            ' MessageBox.Show($"Erro nos parâmetros: {ex.InnerException }", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            ' MessageBox.Show($"Erro inesperado: {ex.InnerException }", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return False
    End Function



    Public Function EmailLiberacaoOS() As Boolean

        If My.Settings.BancoDadosAtivo = "alfatec2" Then

            Dim mensagem As String = "Prezados," & vbCrLf & vbCrLf &
                       "Este é um e-mail do controle de Gestão do SINCO/Alfatec." & vbCrLf & vbCrLf &
                       "Por favor, não responda a este e-mail." & vbCrLf & vbCrLf &
                       "Liberaçao de OS 'Ordem de Serviço' numero: " & OrdemServico.IdOrdemServico & vbCrLf &
                       "Data de Liberação: " & Date.Now.Date.ToLongDateString & vbCrLf & vbCrLf &
                       "Projetista Responsavel: " & Usuario.NomeCompleto & vbCrLf & vbCrLf &
                       "Projeto: " & OrdemServico.Projeto & " / Tag: " & OrdemServico.Tag & vbCrLf & vbCrLf &
                       "Atenciosamente," & vbCrLf & vbCrLf & vbCrLf & vbCrLf &
                       "Equipe Alfatec/Engenharia"

            Dim sucesso As Boolean = ClasseEmail.EnviarEmail(
    emailEnvio:="sinco@alfatec.ind.br",
    nomeEnvio:="Sistema de Controle da Enhenharia",
    emailDestino:=Usuario.EnviarEmailLiberacaoOS.ToString, 'VEM DO BANCO DE DADOS
    nomeDestino:="PCP",
    smtp:="mail.alfatec.ind.br",
    porta:="587",
    senha:="v89v9hmbbrz8",
    assunto:="Registro de Liberação de Ordem de Serviço para Fabricação: " & OrdemServico.IdOrdemServico & " Projeto: " & OrdemServico.Projeto & " / Tag: " & OrdemServico.Tag,
    mensagem:=mensagem,
    enderecoAnexo:="")

        ElseIf My.Settings.BancoDadosAtivo = "amceletrica" Then

            Dim mensagem As String = "Prezados," & vbCrLf & vbCrLf &
                      "Este é um e-mail do controle de Gestão do SINCO/AMC." & vbCrLf & vbCrLf &
                      "Por favor, não responda a este e-mail." & vbCrLf & vbCrLf &
                      "Liberaçao de OS 'Ordem de Serviço' numero: " & OrdemServico.IdOrdemServico & vbCrLf &
                      "Data de Liberação: " & Date.Now.Date.ToLongDateString & vbCrLf & vbCrLf &
                      "Projetista Responsavel: " & Usuario.NomeCompleto & vbCrLf & vbCrLf &
                      "Projeto: " & OrdemServico.Projeto & " / Tag: " & OrdemServico.Tag & vbCrLf & vbCrLf &
                      "Atenciosamente," & vbCrLf & vbCrLf & vbCrLf & vbCrLf &
                      "Equipe AMC/Engenharia"

            Dim sucesso As Boolean = ClasseEmail.EnviarEmail(
    emailEnvio:="protheus@amcsolucoes.com.br",
    nomeEnvio:="Sistema de Controle da Enhenharia",
    emailDestino:=Usuario.EnviarEmailLiberacaoOS.ToString,
    nomeDestino:="PCP",
    smtp:="smtp.office365.com", '"mail.amcsolucoes.com.br", 'smtp.office365.com", 'smtp.office365.com -
    porta:="587",
    senha:="1r^Bq6L5j5",
    assunto:="Registro de Liberação de Ordem de Serviço para Fabricação: " & OrdemServico.IdOrdemServico & " Projeto: " & OrdemServico.Projeto & " / Tag: " & OrdemServico.Tag,
    mensagem:=mensagem,
    enderecoAnexo:="")

        End If

    End Function

    Public Function EmailCancelamentoOS() As Boolean

        If My.Settings.BancoDadosAtivo = "alfatec2" Then

            Dim mensagem As String = "Prezados," & vbCrLf & vbCrLf &
                         "Este é um e-mail do controle de Gestão do SINCO/Alfatec." & vbCrLf & vbCrLf &
                         "Por favor, não responda a este e-mail." & vbCrLf & vbCrLf &
                         "Cancelamento de Liberaçao de OS 'Ordem de Serviço' numero: " & OrdemServico.IdOrdemServico & vbCrLf &
                         "Data do Cancelamento: " & Date.Now.Date.ToLongDateString & vbCrLf & vbCrLf &
                         "Projetista Responsavel: " & Usuario.NomeCompleto & vbCrLf & vbCrLf &
                         "Projeto: " & OrdemServico.Projeto & " / Tag: " & OrdemServico.Tag & vbCrLf & vbCrLf &
                         "Endereço da Pasta: " & OrdemServico.EnderecoOrdemServico & vbCrLf &
                         "Atenciosamente," & vbCrLf & vbCrLf & vbCrLf & vbCrLf &
                         "Equipe Alfatec/Engenharia"

            Dim sucesso As Boolean = ClasseEmail.EnviarEmail(
        emailEnvio:="sinco@alfatec.ind.br",
        nomeEnvio:="Sistema de Controle da Enhenharia",
        emailDestino:=Usuario.EnviarEmailLiberacaoOS.ToString,
        nomeDestino:="PCP",
        smtp:="mail.alfatec.ind.br",
        porta:="587",
        senha:="v89v9hmbbrz8",
        assunto:="Registro de Cancelamento de Ordem de Serviço: " & OrdemServico.IdOrdemServico & " Projeto: " & OrdemServico.Projeto & " / Tag: " & OrdemServico.Tag,
        mensagem:=mensagem,
        enderecoAnexo:="")

        ElseIf My.Settings.BancoDadosAtivo = "amceletrica" Then

            Dim mensagem As String = "Prezados," & vbCrLf & vbCrLf &
                      "Este é um e-mail do controle de Gestão do SINCO/AMC." & vbCrLf & vbCrLf &
                      "Por favor, não responda a este e-mail." & vbCrLf & vbCrLf &
                      "Cancelamento de Liberaçao de OS 'Ordem de Serviço' numero: " & OrdemServico.IdOrdemServico & vbCrLf &
                      "Data de Liberação: " & Date.Now.Date.ToLongDateString & vbCrLf & vbCrLf &
                      "Projetista Responsavel: " & Usuario.NomeCompleto & vbCrLf & vbCrLf &
                      "Projeto: " & OrdemServico.Projeto & " / Tag: " & OrdemServico.Tag & vbCrLf & vbCrLf &
                      "Atenciosamente," & vbCrLf & vbCrLf & vbCrLf & vbCrLf &
                      "Equipe AMC/Engenharia"

            Dim sucesso As Boolean = ClasseEmail.EnviarEmail(
    emailEnvio:="protheus@amcsolucoes.com.br",
    nomeEnvio:="Sistema de Controle da Enhenharia",
    emailDestino:=Usuario.EnviarEmailLiberacaoOS.ToString,
    nomeDestino:="PCP",
    smtp:="mail.amcsolucoes.com.br", 'smtp.office365.com", 'smtp.office365.com -
    porta:="587",
    senha:="1r^Bq6L5j5",
    assunto:="Registro de Liberação de Ordem de Serviço para Fabricação: " & OrdemServico.IdOrdemServico & " Projeto: " & OrdemServico.Projeto & " / Tag: " & OrdemServico.Tag,
    mensagem:=mensagem,
    enderecoAnexo:="")

        End If

    End Function

    Public Function EmailNovoUsuario() As Boolean

        If My.Settings.BancoDadosAtivo = "alfatec2" Then

            Dim mensagem As String = "Prezados," & vbCrLf & vbCrLf &
                       "Este é um e-mail do controle de Gestão do SINCO." & vbCrLf & vbCrLf &
                       "Por favor, não responda a este e-mail." & vbCrLf & vbCrLf &
                       "Comunicado de Cadastro de Novo Usuário: " & Usuario.NomeCompleto & vbCrLf &
                       "Data de Liberação: " & Date.Now.Date.ToLongDateString & vbCrLf & vbCrLf &
                       "Senha de Acesso: " & Usuario.Senha & vbCrLf & vbCrLf &
                       "Login: " & Usuario.Login & vbCrLf & vbCrLf &
                       "Atenciosamente," & vbCrLf & vbCrLf & vbCrLf & vbCrLf &
                       "Equipe SINCO"

            Dim sucesso As Boolean = ClasseEmail.EnviarEmail(
    emailEnvio:="sinco@alfatec.ind.br",
    nomeEnvio:="Sistema SINCO",
    emailDestino:=Usuario.NomeCompleto, 'VEM DO BANCO DE DADOS
    nomeDestino:=Usuario.NomeCompleto,
    smtp:="mail.alfatec.ind.br",
    porta:="587",
    senha:="v89v9hmbbrz8",
    assunto:="Confirmação de cadastro - Novo Usuário",
    mensagem:=mensagem,
    enderecoAnexo:="")

        ElseIf My.Settings.BancoDadosAtivo = "amceletrica" Then

            Dim mensagem As String = "Prezados," & vbCrLf & vbCrLf &
                       "Este é um e-mail do controle de Gestão do SINCO." & vbCrLf & vbCrLf &
                       "Por favor, não responda a este e-mail." & vbCrLf & vbCrLf &
                       "Comunicado de Cadastro de Novo Usuário: " & Usuario.NomeCompleto & vbCrLf &
                       "Data de Liberação: " & Date.Now.Date.ToLongDateString & vbCrLf & vbCrLf &
                       "Senha de Acesso: " & Usuario.Senha & vbCrLf & vbCrLf &
                       "Login: " & Usuario.Login & vbCrLf & vbCrLf &
                        "Atenciosamente," & vbCrLf & vbCrLf & vbCrLf & vbCrLf &
                       "Equipe SINCO"

            Dim sucesso As Boolean = ClasseEmail.EnviarEmail(
    emailEnvio:="protheus@amcsolucoes.com.br",
    nomeEnvio:="Sistema de Controle da Enhenharia",
    emailDestino:=Usuario.email, 'VEM DO BANCO DE DADOS
    nomeDestino:=Usuario.NomeCompleto,
    smtp:="smtp.office365.com", 'smtp.office365.com - "mail.amcsolucoes.com.br", '
    porta:="587",
    senha:="1r^Bq6L5j5",
    assunto:="Confirmação de cadastro - Novo Usuário",
    mensagem:=mensagem,
    enderecoAnexo:="")
        Else

            MsgBox("Não há e-mail de suporte ativo, informe o adminstrador do sistema!")

        End If

    End Function

    Public Function EmailAlfatec(ByVal emailCliente As String, ByVal NomeCliente As String, ByVal CorpoMensagem As String, AssuntoMSG As String) As Boolean

        If My.Settings.BancoDadosAtivo = "alfatec2" Then

            Dim sucesso As Boolean = ClasseEmail.EnviarEmail(
    emailEnvio:="sinco@alfatec.ind.br",
    nomeEnvio:="Sistema SINCO",
    emailDestino:=emailCliente, 'VEM DO BANCO DE DADOS
    nomeDestino:=NomeCliente,
    smtp:="mail.alfatec.ind.br",
    porta:="587",
    senha:="v89v9hmbbrz8",
    assunto:=AssuntoMSG,  '"Confirmação de cadastro - Novo Usuário",
    mensagem:=CorpoMensagem,
    enderecoAnexo:="")
        Else

            MsgBox("Não há e-mail de suporte ativo, informe o adminstrador do sistema!")

        End If

    End Function

    Public Function EmailTratamentoErro(ByVal Mensagemex As String) As Boolean

        '' Chamada da função de tratamento de erro de forma assíncrona
        'Task.Run(Sub()
        '             ClasseEmail.EmailTratamentoErro("Erro ao atualizar ou inserir dados no banco: " & ex.Message)
        '         End Sub)

        If My.Settings.BancoDadosAtivo = "alfatec2" Then

            Dim mensagem As String = "Prezados," & vbCrLf & vbCrLf &
                       "Este é um e-mail de tratamento de erro." & vbCrLf & vbCrLf &
                       "Mensagem EX.: " & Mensagemex & vbCrLf & vbCrLf &
                       "Por favor, não responda a este e-mail." & vbCrLf & vbCrLf &
                       "Data de Registro: " & Date.Now.Date.ToLongDateString & vbCrLf & vbCrLf &
                       "Usuario Responsavel: " & Usuario.NomeCompleto & vbCrLf & vbCrLf &
                       "Atenciosamente," & vbCrLf & vbCrLf & vbCrLf & vbCrLf &
                       "Equipe LYNX"

            Dim sucesso As Boolean = ClasseEmail.EnviarEmail(
    emailEnvio:="sinco@alfatec.ind.br",
    nomeEnvio:="Sistema de Controle de Erros",
    emailDestino:="suporte@lynxsolucoesmecanicas.com.br", 'VEM DO BANCO DE DADOS
    nomeDestino:="Sistema",
    smtp:="mail.alfatec.ind.br",
    porta:="587",
    senha:="v89v9hmbbrz8",
    assunto:="Registro de erro Alfatec",
    mensagem:=mensagem,
    enderecoAnexo:="")

        ElseIf My.Settings.BancoDadosAtivo = "amceletrica" Then

            Dim mensagem As String = "Prezados," & vbCrLf & vbCrLf &
                       "Este é um e-mail de tratamento de erro." & vbCrLf & vbCrLf &
                       "Mensagem EX.: " & Mensagemex & vbCrLf & vbCrLf &
                       "Por favor, não responda a este e-mail." & vbCrLf & vbCrLf &
                       "Data de Registro: " & Date.Now.Date.ToLongDateString & vbCrLf & vbCrLf &
                       "Usuario Responsavel: " & Usuario.NomeCompleto & vbCrLf & vbCrLf &
                       "Atenciosamente," & vbCrLf & vbCrLf & vbCrLf & vbCrLf &
                       "Equipe LYNX"

            Dim sucesso As Boolean = ClasseEmail.EnviarEmail(
    emailEnvio:="protheus@amcsolucoes.com.br",
    nomeEnvio:="Sistema de Controle da Enhenharia",
    emailDestino:="suporte@lynxsolucoesmecanicas.com.br", 'VEM DO BANCO DE DADOS
    nomeDestino:="Sistema",
    smtp:="smtp.office365.com",
    porta:="587",
    senha:="1r^Bq6L5j5",
    assunto:="Registro de erro AMC",
    mensagem:=mensagem,
    enderecoAnexo:="")

        End If

    End Function

    Public Function EnviarEmailMultiplo(
    ByVal emailEnvio As String,
    ByVal nomeEnvio As String,
    ByVal emailsDestino As String,
    ByVal nomeDestino As String,
    ByVal smtp As String,
    ByVal porta As String,
    ByVal senha As String,
    ByVal assunto As String,
    ByVal mensagem As String,
    ByVal enderecoAnexo As String
) As Boolean

        ' Verificar parâmetros básicos
        If String.IsNullOrWhiteSpace(emailEnvio) OrElse String.IsNullOrWhiteSpace(emailsDestino) Then
            MessageBox.Show("Os endereços de e-mail de envio e destino são obrigatórios.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        Dim blnRetorno As Boolean = False

        Try
            ' Criar o objeto MailMessage
            Using objEmail As New MailMessage()
                ' Configurar remetente
                objEmail.From = New MailAddress(emailEnvio, nomeEnvio)

                ' Adicionar destinatários (suporta múltiplos e-mails separados por vírgula)
                For Each email In emailsDestino.Split(","c)
                    If Not String.IsNullOrWhiteSpace(email.Trim()) Then
                        objEmail.To.Add(New MailAddress(email.Trim(), nomeDestino))
                    End If
                Next

                ' Configurar assunto e prioridade
                objEmail.Subject = assunto
                objEmail.Priority = MailPriority.High

                ' Adicionar mensagem de texto
                objEmail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(
                                            mensagem, Nothing, Mime.MediaTypeNames.Text.Plain))

                ' Adicionar anexo (se existir)
                If Not String.IsNullOrWhiteSpace(enderecoAnexo) Then
                    objEmail.Attachments.Add(New Attachment(enderecoAnexo))
                End If

                ' Configurar cliente SMTP
                Using objEnvio As New SmtpClient(smtp, Convert.ToInt32(porta))
                    objEnvio.Credentials = New NetworkCredential(emailEnvio, senha)
                    objEnvio.EnableSsl = True
                    objEnvio.Timeout = 100000
                    objEnvio.UseDefaultCredentials = False

                    ' Enviar o e-mail
                    objEnvio.Send(objEmail)
                End Using
            End Using

            blnRetorno = True
        Catch ex As SmtpException
            MessageBox.Show($"Erro SMTP: {ex.Message}", "Erro de Envio", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As ArgumentException
            MessageBox.Show($"Parâmetro inválido: {ex.Message}", "Erro de Parâmetro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show($"Erro inesperado: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return blnRetorno
    End Function

    Public Function NovoEmail(assunto As String, destinatario As String, mensagem As String) As Boolean

        'Dim corpoEmail As String = "Olá " & vCampo_a_Retornar1 & "," & vbCrLf & vbCrLf &
        '    "Aqui estão os detalhes da sua conta:" & vbCrLf &
        '    "Login: " & vCampo_a_Retornar2 & vbCrLf &
        '    "Senha: " & vCampo_a_Retornar3 & vbCrLf & vbCrLf &
        '    "Se você não solicitou essa recuperação de senha, por favor, ignore este e-mail." & vbCrLf &
        '    "Atenciosamente," & vbCrLf &
        '    "Equipe de Suporte"

        'EnviarEmail("Recuperação de Senha", email, corpoEmail)
        'MsgBox("E-mail enviado com sucesso para " & email, MsgBoxStyle.Information, "Recuperação de Senha")

        Try
            ' Configuração do SMTP (Gmail)
            Dim smtpClient As New SmtpClient("smtp.gmail.com", 587) With {
            .Credentials = New Net.NetworkCredential("lynxemssistema@gmail.com", "10207597Eds@$"),
            .EnableSsl = True,
            .DeliveryMethod = SmtpDeliveryMethod.Network
        }

            ' Composição da mensagem
            Dim mailMessage As New MailMessage() With {
            .From = New MailAddress("lynxemssistema@gmail.com", "Sistema de Gestão"),
            .Subject = assunto,
            .Body = mensagem,
            .IsBodyHtml = False
        }

            ' Adiciona destinatário
            mailMessage.To.Add(destinatario)

            ' Envio
            smtpClient.Send(mailMessage)
            Return True
        Catch ex As SmtpException
            MessageBox.Show("Erro SMTP ao enviar e-mail: " & ex.Message, "Erro de envio", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        Catch ex As Exception
            MessageBox.Show("Erro inesperado ao enviar e-mail: " & ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try

    End Function

    Public Function EnviarEmailamc(remetente As String, assunto As String, destinatario As String, mensagem As String) As Boolean

        'Dim corpoEmail As String = "Olá " & vCampo_a_Retornar1 & "," & vbCrLf & vbCrLf &
        '    "Aqui estão os detalhes da sua conta:" & vbCrLf &
        '    "Login: " & vCampo_a_Retornar2 & vbCrLf &
        '    "Senha: " & vCampo_a_Retornar3 & vbCrLf & vbCrLf &
        '    "Se você não solicitou essa recuperação de senha, por favor, ignore este e-mail." & vbCrLf &
        '    "Atenciosamente," & vbCrLf &
        '    "Equipe de Suporte"

        'EnviarEmail("arquivos.engenharia@amcsolucoes.com.br", "Recuperação de Senha", email, corpoEmail)
        'MsgBox("E-mail enviado com sucesso para " & email, MsgBoxStyle.Information, "Recuperação de Senha")

        Try
            Dim smtpClient As New SmtpClient("smtp.office365.com") With {
            .Port = 587,
            .Credentials = New Net.NetworkCredential("arquivos.engenharia@amcsolucoes.com.br", "J$470796977151as"),
            .EnableSsl = True,
            .DeliveryMethod = SmtpDeliveryMethod.Network
        }

            Dim mailMessage As New MailMessage() With {
            .From = New MailAddress(remetente, "Sistema de Gestão"),
            .Subject = assunto,
            .Body = mensagem,
            .IsBodyHtml = False
        }
            mailMessage.To.Add(destinatario)

            smtpClient.Send(mailMessage)
            Return True
        Catch ex As Exception
            Console.WriteLine($"Erro ao enviar e-mail: {ex.Message}")
            Return False
        End Try

    End Function


    Public Function ValidarEmail(ByVal email As String) As Boolean
        ' Remove espaços extras
        email = email.Trim()

        ' Se o e-mail estiver vazio, retorna falso
        If String.IsNullOrWhiteSpace(email) Then
            Return False
        End If

        ' Padrão para validação de e-mail
        Dim padrao As String = "^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$"

        ' Cria o objeto Regex
        Dim regex As New Regex(padrao, RegexOptions.IgnoreCase)

        ' Retorna True se for um e-mail válido
        Return regex.IsMatch(email)
    End Function


End Class