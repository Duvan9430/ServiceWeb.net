Imports System.Net
Imports Newtonsoft.Json

Public Class Form1
    Dim Servicio As New ServicioDatos.ConexionAgendaSoapClient

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        DataGridView1.DataSource = Servicio.GetUsers(txtBuscar.Text).Tables(0)
    End Sub

    Private Sub cboDestino_GotFocus(sender As Object, e As EventArgs) Handles cboDestino.GotFocus
        Dim dt As DataTable
        dt = New DataTable("Tabla")

        dt.Columns.Add("Moneda")

        Dim dr As DataRow

        Dim Currency = New String() {"USD", "DOP", "EUR"}

        For Each element As String In Currency
            dr = dt.NewRow()
            dr("Moneda") = element.ToString
            dt.Rows.Add(dr)

        Next

        cboDestino.DataSource = dt
        cboDestino.ValueMember = "Moneda"
        cboDestino.DisplayMember = "Moneda"


    End Sub

    Private Sub cboOrigen_GotFocus(sender As Object, e As EventArgs) Handles cboOrigen.GotFocus
        Dim dt As DataTable
        dt = New DataTable("Tabla")

        dt.Columns.Add("Moneda")

        Dim dr As DataRow

        Dim Currency = New String() {"USD", "DOP", "EUR"}

        For Each element As String In Currency
            dr = dt.NewRow()
            dr("Moneda") = element.ToString
            dt.Rows.Add(dr)

        Next

        cboOrigen.DataSource = dt
        cboOrigen.ValueMember = "Moneda"
        cboOrigen.DisplayMember = "Moneda"

    End Sub

    Private Sub btnConvertir_Click(sender As Object, e As EventArgs) Handles btnConvertir.Click
        Dim n As WebClient = New WebClient()
        Dim json = n.DownloadString("https://openexchangerates.org/api/latest.json?app_id=571446aa01ce49d39f4079a54e3d5364")
        Dim valueOriginal As String = Convert.ToString(json)
        Console.WriteLine(json)

        Dim MonedaOrigen As Double
        Dim MonedaDestino As Double

        Dim result = JsonConvert.DeserializeObject(valueOriginal)

        MonedaOrigen = (result("rates")(cboOrigen.SelectedValue))
        MonedaDestino = (result("rates")(cboDestino.SelectedValue))


        txtValor.Text = (CDbl(txtCantidad.Text) / CDbl(MonedaOrigen)) * MonedaDestino
    End Sub
End Class
