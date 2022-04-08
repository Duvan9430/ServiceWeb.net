Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports Servidor.Conexion
Imports MySql.Data.MySqlClient

<System.Web.Services.WebService(Namespace:="http://localhost/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class ConexionAgenda
    Inherits System.Web.Services.WebService

    Private Shared _FECHA As Date

    Dim NombreCompleto As String
    Public Validado As Boolean = False
    Public Shared dst As DataSet


    Public Shared Property FECHA As Date
        Get
            Return _FECHA
        End Get
        Set(value As Date)
            _FECHA = value
        End Set
    End Property

    <WebMethod()> Private Function BuscarPersona(ByVal Cadena As String)
        Acceso.AbrirConexion()
        Acceso.ObtenerFechaServidor()

        comando = New MySqlCommand(Cadena, ConexionMySQL)
        comando.Parameters.AddWithValue("?FECHA", MySqlDbType.Date).Value = FECHA
        adaptador = New MySqlDataAdapter(comando)

        ds = New DataSet
        adaptador.Fill(ds)

        If ds.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If

        ConexionMySQL.Close()

    End Function

    <WebMethod()> Public Function GetUsers(ByVal Nombre As String) As DataSet
        Dim DataGrid As New DataGrid

        If BuscarPersona("select * from datos where nombre like'%" & Nombre & "%' or telefono like '%" & Nombre & "%'") = True Then
            DataGrid.DataSource = ds.Tables(0)

        End If

        Return ds

    End Function




End Class