Imports System.Data.SqlClient

Public Class ClBancoProtheus

    Public Function AbriBanco() As Boolean

        Try

            sqlcn = New SqlConnection()

            'cn.ConnectionString = "Persist Security Info = False;User ID = engenharia;Password=Enge003498;MultipleActiveResultSets=true;Initial Catalog=MP12OFICIAL;Data Source=192.168.1.103;"

            sqlcn.ConnectionString = "Persist Security Info = False;User ID = engenharia;Password=Engenhari@003498;MultipleActiveResultSets=true;Initial Catalog=MP12OFICIAL;Data Source=192.168.163.22;"

            ' cn.ConnectionString = "Data Source=192.168.1.103;Initial Catalog=MP12OFICIAL;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"

            sqlmyCmd.Connection = sqlcn
            sqlcn.Open()
            Return True
        Catch ex As Exception
            Return False
        Finally
        End Try

    End Function

End Class