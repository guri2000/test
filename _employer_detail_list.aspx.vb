Imports System.Data
Imports System.Data.OracleClient
Imports System.Configuration

Imports System.IO
Imports Telerik.Web.UI
Imports System.Text
Partial Class _Default
    Inherits System.Web.UI.Page
    Dim con As String = ConfigurationManager.ConnectionStrings("constr").ConnectionString
    Dim qry As String
    Dim dt As DataTable
    Dim sydate As String
    Dim empno As Int32 = 0
    Dim locid As Int32 = 0
    Dim msg As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        empno = "4798"
        If Page.IsPostBack = False Then

            getmastres()
          
        Else

        End If
       
    End Sub
    Protected Function getdata(ByVal qry As String, ByVal type As String) As DataTable
        Dim adp As New OracleDataAdapter(qry, con)
        If type = "sp" Then
            adp.SelectCommand.CommandType = CommandType.StoredProcedure
            adp.SelectCommand.Parameters.Add("cur_employees", OracleType.Cursor).Direction = ParameterDirection.Output
            adp.SelectCommand.Parameters.Add("p_comm", OracleType.Number).Value = 0
            adp.SelectCommand.Parameters.Add("p_empno", OracleType.Number).Value = empno
            adp.SelectCommand.Parameters.Add("p_date1", OracleType.DateTime).Value = Date.Now
            adp.SelectCommand.Parameters.Add("p_date2", OracleType.DateTime).Value = Date.Now
            adp.SelectCommand.Parameters.Add("p_par1", OracleType.VarChar, 50).Value = "null"
            adp.SelectCommand.Parameters.Add("p_par2", OracleType.VarChar, 50).Value = "null"
            adp.SelectCommand.Parameters.Add("zid", OracleType.Number).Value = 0

        Else
            adp.SelectCommand.CommandType = CommandType.Text
        End If

        Dim dt1 As New DataTable
        adp.Fill(dt1)
        Return dt1

    End Function
    Private Sub getmastres()
        qry = "select empfname||' '||empmname||' '||emplname as empname, empcmpnam ,empcmptelno,to_char(empcontdt,'dd-Mon-yyyy') as empcontdt,(select resdescr from rahul.ams_resultmaster where resid in (select resultid from rahul.ams_employerresult where rahul.ams_employerresult.empid=rahul.ams_employer.empid)) as result from rahul.ams_employer"
        dt = getdata(qry, "QR")
        If dt.Rows.Count > 0 Then
            RadGrid1.DataSource = dt
            RadGrid1.DataBind()
        End If

    End Sub
    Protected Sub RadGrid1_SortCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridSortCommandEventArgs) Handles RadGrid1.SortCommand
        getmastres()
    End Sub
    Protected Sub RadGrid1_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs) Handles RadGrid1.ItemCommand
        If e.CommandName = RadGrid.FilterCommandName Then
            Dim filterPair As Pair = DirectCast(e.CommandArgument, Pair)
            ' gridMessage1 = "Current Filter function: '" + filterPair.First + "' for column '" + filterPair.Second + "'"
            Dim filterBox As TextBox = CType((CType(e.Item, GridFilteringItem))(filterPair.Second.ToString()).Controls(0), TextBox)
            '   gridMessage1 = "<br> Entered pattern for search: " + filterBox.Text
        End If
    End Sub
    'Private gridMessage1 As String = Nothing
    'Private gridMessage2 As String = Nothing
    'Protected Sub RadGrid1_DataBound(ByVal sender As Object, ByVal e As EventArgs) Handles RadGrid1.DataBound
    '    If Not String.IsNullOrEmpty(gridMessage1) Then
    '        DisplayMessage(gridMessage1)
    '        DisplayMessage(gridMessage2)
    '    End If
    'End Sub
    'Private Sub DisplayMessage(ByVal text As String)
    '    RadGrid1.Controls.Add(New LiteralControl(String.Format("<span style='color:red'>{0}</span>", text)))
    'End Sub
End Class
