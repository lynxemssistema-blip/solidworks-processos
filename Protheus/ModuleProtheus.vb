Imports System.Data.SqlClient

Module ModuleProtheus

    Public BancoProtheus As New ClBancoProtheus
    Public sqlcn As New SqlConnection  'Variavel do banco de Dados
    Public sqlmyCmd As New SqlCommand  'Variavel da conexão e execução do comando do banco de dados

End Module