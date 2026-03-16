Public Class ClOutlook

    'Public Function BuscaCatalogoEndereco() As System.Data.DataTable

    '    'Dim dt As New System.Data.DataTable
    '    'dt.Columns.Add("Nome")
    '    'dt.Columns.Add("Tipo de Endereço")
    '    'dt.Columns.Add("Endereço de e-mail")
    '    '' Inicializando o aplicativo Outlook
    '    'Dim outlookApp As New Application()
    '    'Dim namespaceOutlook As [NameSpace] = outlookApp.GetNamespace("MAPI")

    '    '' Acessando o Catálogo de Endereços
    '    'Dim addressList As AddressList = namespaceOutlook.Session.GetGlobalAddressList()

    '    '' Acessando os itens do Catálogo de Endereços (endereços globais)
    '    'Dim addressEntries As AddressEntries = addressList.AddressEntries

    '    '' Percorrendo todos os endereços e adicionando ao Dictionary
    '    'For Each addressEntry As AddressEntry In addressEntries
    '    '    ' Adicionando os dados ao Dictionary: Nome como chave e tipo de endereço como valor
    '    '    dt.Rows.Add(addressEntry.Name, addressEntry.AddressEntryUserType.ToString())
    '    'Next

    '    'Console.ReadLine()

    '    Try
    '        ' Inicializando o aplicativo Outlook
    '        Dim outlookApp As New Application()

    '        ' Inicializando o namespace MAPI
    '        Dim namespaceOutlook As [NameSpace] = outlookApp.GetNamespace("MAPI")

    '        ' Acessando o Catálogo de Endereços
    '        Dim addressList As AddressList = namespaceOutlook.Session.GetGlobalAddressList()

    '        ' Acessando os itens do Catálogo de Endereços (endereços globais)
    '        Dim addressEntries As AddressEntries = addressList.AddressEntries

    '        ' Exibindo os endereços
    '        For Each addressEntry As AddressEntry In addressEntries
    '            MsgBox("Nome: " & addressEntry.Name)
    '            MsgBox("Tipo de Endereço: " & addressEntry.AddressEntryUserType.ToString())
    '            MsgBox("------------------------------------")
    '        Next

    '    Catch ex As system.Exception
    '        ' Exibir a mensagem de erro no console se algo der errado
    '        MsgBox("Erro: " & ex.Message)
    '    Finally
    '        ' Aguardar o usuário pressionar uma tecla para fechar
    '        Console.ReadLine()
    '    End Try

    'End Function

End Class