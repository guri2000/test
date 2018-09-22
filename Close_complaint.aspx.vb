Imports System.Data
Imports System.IO
Imports Telerik.Web.UI
Imports System.Text
Partial Class Close_complaint
    Inherits System.Web.UI.Page
    Dim clas As rahulcpm.commonclass = New rahulcpm.commonclass()
    Dim qry As String
    Dim dt As DataTable
    Dim sydate As String
    Dim agntid As Int32
    Dim msg As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("lgnagntid") = "0" Or Session("lgnagntid") = "" Then
            'Response.Redirect("login.aspx")
            ImageButton1.Enabled = False
            Button2.Enabled = False
            RadWindowManager1.RadAlert("session is expired, Please login Again", 300, 200, "Session Expired Error", "")
            Response.Redirect("logout.aspx")
        Else
            agntid = Session("lgnagntid")
        End If
        If Page.IsPostBack = False Then
            Bind_Employees()
            Bind_Status()
        End If
        If Request.QueryString("mode") = "edit" And Session("mode") = "" Then

            getrcdforedit()

        End If
    
    End Sub
    
  
    Private Sub getrcdforedit()
        qry = "select  M.MAIL_ID,M.AGENTID,M.STATUS_ID,TO_CHAR(M.CLOSEDON,'DD-MON-YYYY') AS CLOSEDON,M.CLOSEDBY,M.REMARKS from Meenu.AMS_Mails M where M.mail_Id='" & Request.QueryString("Compid") & "'"
        dt = clas.getdata(qry, "QR")
        If dt.Rows.Count > 0 Then
            If IsDBNull(dt.Rows(0)("CLOSEDON")) = False Then
                dtp_date.SelectedDate = dt.Rows(0)("CLOSEDON").ToString
            Else
                dtp_date.SelectedDate = Date.Now
            End If
            If IsDBNull(dt.Rows(0)("REMARKS")) = False Then
                txt_remarks.Text = Convert.ToString(dt.Rows(0)("REMARKS"))
            Else
                txt_remarks.Text = String.Empty
            End If
            If IsDBNull(dt.Rows(0)("STATUS_ID")) = False Then
                ddl_comp_status.SelectedValue = dt.Rows(0)("STATUS_ID")
            Else
                ddl_comp_status.ClearSelection()
            End If
            If IsDBNull(dt.Rows(0)("CLOSEDBY")) = False Then
                ddl_Emp.SelectedValue = dt.Rows(0)("CLOSEDBY")
            Else
                ddl_Emp.ClearSelection()
            End If

            'ddl_comp_status.Items.FindByValue(Convert.ToString(dt.Rows(0)("STATUS_ID"))).Selected = True
            ddl_comp_status.DataBind()
            Button1.Text = "Update"
            Session("mode") = "edit"
        End If

    End Sub

    Private Sub Bind_Status()

        qry = "SELECT CSTATID,CSTAT_DESC FROM Meenu.AMS_Complaint_Status WHERE STATUS='A' order by CSTATID ASC"

        dt = clas.getdata(qry, "QR")
        If dt.Rows.Count > 0 Then
            '  Dim ddl_emp As DropDownList = DirectCast(RadGrid1.FindControl("ddl_Employer"), DropDownList)
            ddl_comp_status.DataTextField = "CSTAT_DESC"
            ddl_comp_status.DataValueField = "CSTATID"
            ddl_comp_status.DataSource = dt
            ddl_comp_status.DataBind()
        End If

    End Sub

    Private Sub Bind_Employees()

        qry = "Select empno,name from tt.employees where deptcode='15' and zbaid='119' and active='T' "
        qry += " and name not like '%Dummy%' and name not like '%System%' order by name asc"

        dt = clas.getdata(qry, "QR")
        If dt.Rows.Count > 0 Then
            '  Dim ddl_emp As DropDownList = DirectCast(RadGrid1.FindControl("ddl_Employer"), DropDownList)
            ddl_Emp.DataTextField = "name"
            ddl_Emp.DataValueField = "empno"
            ddl_Emp.DataSource = dt
            ddl_Emp.DataBind()
        End If

    End Sub


    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click, ImageButton1.Click

        Dim flag As Boolean
        If Button1.Text = "Update" Then
            flag = updateCommand()
        End If

        If flag = True Then
            clear()
            RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Complaint Status Saved Successfully. </div></li></ul>", 300, 150, "Validation Success", Nothing)
        Else
            Session.Remove("mode")
            RadWindowManager1.RadAlert("<ul><li><div style=color:green;>" & Session("error") & " </div></li></ul>", 300, 150, "Validation Error", Nothing)
        End If

    End Sub
    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        clear()
    End Sub
    
    Public Function updateCommand() As Boolean
        'Dim transid As Int32 = getMaxID()
        'Dim str2 As String = "insert into tt.ukdata(transid, InDocRecdDt,InDocRecdBy, InDocStat) values(2, to_date(@InDocRecdDt,'dd-MON-yyyy'), @InDocRecdBy, @InDocStat)"
        Dim str As StringBuilder = New StringBuilder
        str.Append("update meenu.AMS_Mails set ")

        If dtp_date.SelectedDate IsNot Nothing Then
            str.Append("ClosedOn=" & "to_date('" & dtp_date.SelectedDate.Value.ToString("dd-MMM-yyyy HH:mm:ss") & "','dd-MON-yyyy hh24:mi:ss') " & ",")
        End If

        If txt_remarks.Text <> "" Then
            str.Append("Remarks='" & txt_remarks.Text & "',")
        End If

        If ddl_comp_status.SelectedValue <> "" Then
            str.Append("status_id='" & ddl_comp_status.SelectedValue & "',")
        End If

        If ddl_Emp.SelectedValue <> "" Then
            str.Append("ClosedBy='" & ddl_Emp.SelectedValue & "',")
        End If

        Dim temp As String = str.ToString
        str.Clear()
        str.Append(temp.Substring(0, temp.Length - 1))
        str.Append(" where Mail_Id='" & Request.QueryString("Compid") & "'")
        Dim i = clas.ExecuteNonQuery(str.ToString())
        If i = 1 Then



            RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Transaction Done Successfully </div></li></ul>", 300, 150, "Validation Success", Nothing)
            Return True
        Else

            Dim Err_Msg As String = clas.getexception().Replace(vbCrLf, "")
            Err_Msg = Err_Msg.Replace(vbCr, "")
            Err_Msg = Err_Msg.Replace(vbLf, "")
            RadWindowManager1.RadAlert("<ul><li><div style=color:red;>System found some exception:" & Err_Msg & "</div></li></ul>", 300, 100, "Validation Exception Failure", Nothing)

            Return False
        End If

    End Function

    Private Sub clear()
        dtp_date.SelectedDate = Date.Now
        txt_remarks.Text = String.Empty

        ddl_comp_status.ClearSelection()
        ddl_comp_status.DataBind()
        ddl_Emp.ClearSelection()
        ddl_Emp.DataBind()
        Button1.Text = "Update"
        Session.Remove("mode")
    End Sub


End Class
