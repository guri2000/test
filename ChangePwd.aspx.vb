Imports System.Data
Imports System.IO
Imports Telerik.Web.UI
Imports System.Text
Imports System.Environment
Imports System.Net
Imports System.Net.Mail
Imports System.Net.Mime

Partial Class ChangePwd
    Inherits System.Web.UI.Page

    Dim clas As rahulcpm.commonclass = New rahulcpm.commonclass()
    Dim agntid As Int32

    Protected Sub ValidateCaptcha(sender As Object, e As ServerValidateEventArgs)
        Captcha1.ValidateCaptcha(txtCaptcha.Text.Trim())
        e.IsValid = Captcha1.UserValidated
        If e.IsValid Then
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "alert('Valid Captcha!');", True)
        End If
    End Sub

   

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Session("lgnagntid") <> "0" Or Session("lgnagntid") <> "" Then
            agntid = Session("lgnagntid")
        Else
            Response.Redirect("logout.aspx")
        End If

        If Page.IsPostBack = False Then
            Clear()
            get_Agent_Data()
        End If
    End Sub

    Protected Sub get_Agent_Data()
        Dim Query As String = ""
        Query = "select a.agntid,a.agntname,a.agnteml,b.lgnnam,b.lgnpwd,b.lgnroll from ams.ams_agents a, ams.ams_usermaster b where b.lgnagntid=a.agntid and b.lgnsts='A' and a.agntid='" & agntid & "' "
        Dim DT As DataTable = clas.getdata(Query, "TX")
        If DT.Rows.Count > 0 Then
            txt_OldExistingPwd.Text = DT.Rows(0)("lgnpwd").ToString.Trim()
            txt_LoginName.Text = DT.Rows(0)("lgnnam").ToString
            agntmail.Text = DT.Rows(0)("agnteml").ToString
        End If
    End Sub


    Public Sub Email_thru_MIS()

        Dim Email_From As String = "mis_alert@wwicsgroup.com"
        Dim Email_To As String = agntmail.Text.Trim
        'Dim Email_To As String = "meenu@pinnacleinfoedge.com"
        Dim objmail As New MailMessage(Email_From, Email_To)
      

        'objmail.Bcc.Add("anuj@wwicsgroup.com")
        objmail.Subject = "Intimation Mail – Account password changed " & Convert.ToString(DateTime.Now)
        objmail.IsBodyHtml = True

        Dim Mail_Body As String = ""


        Dim Mail_Str As StringBuilder = New StringBuilder
        Mail_Str.Append("<html><head></head><title></title>")
        Mail_Str.Append("<body style='font-size:14px;font-family:Times New Roman;'>")

        Mail_Str.Append("<p><h3><Strong> Account password changed </Strong></h3></p> <hr />")


        Mail_Str.Append("<p>Dear User, </p>")

        Mail_Str.Append("<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Account password changed by " & Session("lgnagntnam") & ".</p>")

        Mail_Str.Append("<p> <b>Regards,</b></p>")
         Mail_Str.Append("<p> MIS (z-aXis) </p>")
        Mail_Str.Append("</body> </html>")

        Mail_Body = Mail_Str.ToString

        objmail.Body = Mail_Body
        Dim objsent As New SmtpClient("121.0.0.219")
        objsent.UseDefaultCredentials = True
        objsent.Credentials = New System.Net.NetworkCredential("mis_alert@wwicsgroup.com", "MIS")
        'objsent.Send(objmail)
        'ClientScript.RegisterStartupScript([GetType](), "alert", "alert('Email sent to registered email id');window.close();", True)
    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Captcha1.ValidateCaptcha(txtCaptcha.Text.Trim())
        Dim Captcha As String = Captcha1.UserValidated
        If Captcha = "True" Then
            Dim Falg As Boolean = updateCommand()
            If Falg = True Then
                Maintain_Logs()
                ' Email_thru_MIS()
                Clear()
                RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Account password changed Successfully. </div></li></ul>", 300, 150, "Validation Success", Nothing)
            Else
            End If
        Else
            RadWindowManager1.RadAlert("<ul><li><div style=color:Red;> Captcha Invalid. Please try again.. </div></li></ul>", 300, 150, "Validation Success", Nothing)
        End If
       
    End Sub

    Public Function updateCommand() As Boolean
        Try
            Dim str As StringBuilder = New StringBuilder
            str.Append("update ams.ams_usermaster set lgnpwd='" & txt_NewPwd.Text.Trim() & "' where lgnagntid='" & agntid & "'")
            clas.ExecuteNonQuery(str.ToString())
            Return True
            Throw New Exception("UnKnown Exception occured , Please Contact IT Department")
        Catch ex As Exception
            RadWindowManager1.RadAlert(ex.Message, 300, 200, "Exception", "")
            Return False
        End Try
    End Function

    Protected Sub Maintain_Logs()

        Dim ipaddress As String = Request.UserHostAddress

        Dim TransId As Integer = clas.getMaxID("SELECT nvl(max(Transid),0)+1 from ams.AMS_Chng_Pwd_Logs")
        Dim str As StringBuilder = New StringBuilder
        str.Append("Insert Into AMS.AMS_Chng_Pwd_Logs (TRANSID,AGENTID,OLDPWD,NEWPWD,KEYEDON,KEYEDBY,IP) Values ")
        str.Append(" ('" & TransId & "','" & agntid & "','" & txt_OldPwd.Text & "'")
        str.Append(",'" & txt_NewPwd.Text & "',sysdate,'" & agntid & "','" & ipaddress & "' )")
        clas.ExecuteNonQuery(str.ToString())
    End Sub

   

    Protected Sub Clear()
        agntmail.Text = String.Empty
        txt_LoginName.Text = String.Empty
        txt_OldPwd.Text = String.Empty
        txt_OldExistingPwd.Text = String.Empty
        txt_NewPwd.Text = String.Empty
        txt_ConfmPwd.Text = String.Empty
        txtCaptcha.Text = String.Empty
        get_Agent_Data()
    End Sub

    Protected Sub Button2_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles Button2.Click
        Clear()
    End Sub
End Class
