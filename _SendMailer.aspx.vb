Imports System.Data
Imports System.Data.OracleClient
Imports System.Configuration
Imports System.IO
Imports Telerik.Web.UI
Imports System.Text
Imports System.Net
Imports System.Data.OleDb


Imports System.Net.Mail
Partial Class SendMailer
    Inherits System.Web.UI.Page
    Dim con As String = ConfigurationManager.ConnectionStrings("constr").ConnectionString
    Dim sql1, sql2 As String
    Dim mlrstrhtml As String = ""

    Dim sb As StringBuilder = New StringBuilder()
    Dim dt1 As New DataTable
    Protected Sub mailsend()
       
        Dim sucermsg As String = ""
        ' Dim cmd3 = "Select Distinct Vw.Fileno,Vw.Fileserialno Fsn, Am.Appemail,Am.Appfname||' '||Am.Applname Clntname,To_Char(Retaindate,'dd-Mon-yyyy') Rtdate, Vw.Immclass, Ctr.Name, Zbaname Dlbranch, Vw.Docstatus, Vw.Docrecdon,Vw.Docstatus, Statdescr Docst,(To_Date(Sysdate,'dd-Mon-yyyy')-To_Date(Retaindate,'dd-Mon-yyyy')) As Aging from webbranch.focuslist_slk_view_cmb1 vw, tt.assforcountry ctr, phasezero.applicantmaster am , phasezero.zba_master zb , tt.clientstatmaster st Where Am.Fileno=Vw.Fileno And Vw.Assforcountry=Transid And Dealbranchid=Zbaid  And Deactivate=Statcd And Assforcountry<>2 And Docstatus='DNR' And (Sysdate- Retaindate)>=15  And Deactivate='N' And Vw.Fileno Not In(Select Fileno From Rahul.Dnrdi_Maillog Where Tempid=1 And To_Char(Mailsndon,'dd-Mon-yyyy')= To_Char(Sysdate,'dd-Mon-yyyy')) And Immclass In ('EES', 'EEP','EP1', 'FPP', 'IND', 'MPL','QSK' , 'NSC' ,'SSN') and rownum <=100 "
        'Dim cmd3 = "select a.emailid,a.Template,a.Transid  from meenu.acumenmailertransaction  a left join meenu.acumenmailermaster b on a.Mailerid=b.transid"
        'Dim cmd3 = "select a.emailid,a.Template,a.Transid  from meenu.acumenmailertransaction  a left join meenu.acumenmailermaster b on a.Mailerid=b.transid where a.failurereason is null and rownum<26 order by a.transid"
        Dim cmd3 = "select a.emailid,a.Template,a.Transid  from meenu.acumenmailertransaction  a left join meenu.acumenmailermaster b on a.Mailerid=b.transid where  a.transid>300 order by a.transid"
        Dim adp12 As OracleDataAdapter = New OracleDataAdapter(cmd3, con)
        Dim dt1 As New DataTable()
        adp12.Fill(dt1)
        
        If dt1.Rows.Count > 0 Then
            Dim mlsend As Int32 = 0
            Dim errsend As Int32 = 0
            Dim notneeed As Int32 = 0
            Dim reader As StreamReader
            Dim str As String = String.Empty
            If dt1.Rows(0)(1) = 1 Then
                reader = New StreamReader(Server.MapPath("Templates\") & "index-employers.html")
            Else
                reader = New StreamReader(Server.MapPath("Templates\") & "index-employers.html")
            End If
            For Each row As DataRow In dt1.Rows
                Dim sts As String = ""
                Try
                    'Dim reader As StreamReader
                    'Dim str As String = String.Empty
                    'If dt1.Rows(0)(1) = 1 Then
                    '    reader = New StreamReader(Server.MapPath("Templates\") & "index-employers.html")
                    'Else
                    '    reader = New StreamReader(Server.MapPath("Templates\") & "index-employers.html")
                    'End If

                    str = reader.ReadToEnd


                    Dim toeml As String = Nothing
                    If IsDBNull(row("emailid")) = False Then
                        If validateEmail(row("emailid")) = True Then

                            Dim stringwriter As New StringWriter()
                            Dim htmlwriter As New HtmlTextWriter(stringwriter)
                            'FormView1.RenderControl(htmlwriter)
                            Dim Email_From As String = "vanish@acumen-services.com"
                            Dim Email_To As String = Convert.ToString(row("emailid"))
                            Dim objmail As New MailMessage(Email_From, Email_To)
                            'Dim objmail As New MailMessage(Email_From, "bhat.pawan34@gmail.com")
                            objmail.CC.Add("pawan.bhat@pinnacleinfoedge.com")
                            'objmail.Bcc.Add("akaushalchd2424@gmail.com,anujkaushal@yahoo.com,vanish@acumen-services.com")
                            objmail.Subject = "Acumen Management Services"
                            objmail.IsBodyHtml = True
                            'Dim Mail_Body As String = ""
                            'Dim pagebreak As String = "<br>"
                            'Dim Mail_Str As StringBuilder = New StringBuilder
                            'Mail_Str.Append(pagebreak)
                            mlrstrhtml = str
                            'Mail_Body = Mail_Str.ToString
                            ' objmail.Body = mlrstrhtml
                            ' objmail.Body = Mail_Body
                            objmail.Body = mlrstrhtml
                            Dim objsent As New SmtpClient("121.0.0.219")
                            'Dim objsent As New SmtpClient("smtp.wwicsgroup.com", 587)
                            'Dim objsent As New SmtpClient("smtp.wwicsgroup.com", 587)
                            objsent.UseDefaultCredentials = True
                            ' objsent.Credentials = New System.Net.NetworkCredential("mis_alert@wwicsgroup.com", "MIS")
                            'objsent.Credentials = New System.Net.NetworkCredential("migrate2@wwicsgroup.com", "pmwgJdu#8")
                            objmail.ReplyToList.Add("vanish@acumen-services.com")
                            objsent.Send(objmail)

                            sts = "T"
                            mlsend = mlsend + 1
                            sucermsg = "Sucess"
                        Else
                            Throw New Exception("email id invalid")
                        End If
                    Else
                        Throw New Exception("email id is not found")
                    End If
                Catch ex As Exception
                    sts = "F"
                    'errsend = errsend + 1
                    sucermsg = ex.Message.ToString()
                End Try
                If sts = "T" Or sts = "F" Then
                    Dim con2 As OracleConnection = New OracleConnection(con)
                    If con2.State = ConnectionState.Closed Then
                        con2.Open()
                    End If
                    Try
                        Dim hostName As String = Dns.GetHostName() ' // Retrive the Name of HOST
                        ' // Get the IP
                        Dim myIP As String = Dns.GetHostByName(hostName).AddressList(0).ToString()
                        sql1 = "update  meenu.acumenmailertransaction set sendstatus='" & sts & "',Failurereason='" & sucermsg & "',IP='" & myIP & "',MAILSENDDATE=sysdate where Transid='" & row("Transid") & "'"
                        Dim cmd1 As OracleCommand = New OracleCommand(sql1, con2)
                        cmd1.CommandType = CommandType.Text
                        cmd1.ExecuteNonQuery()

                        cmd1.Dispose()
                    Catch ex As Exception
                        con2.Close()
                        'System.Threading.Thread.Sleep(1000)
                    End Try
                End If

            Next


        Else

        End If
    End Sub
    Public Function validateEmail(ByVal emailAddress As String) As Boolean
        ' Dim email As New Regex("^(?<user>[^@]+)@(?<host>.+)$")
        Dim email As New Regex("([\w-+]+(?:\.[\w-+]+)*@(?:[\w-]+\.)+[a-zA-Z]{2,7})")
        If email.IsMatch(emailAddress) Then
            Return True
        Else
            Return False
        End If
    End Function
    'Protected Function getdata(ByVal qry As String) As DataTable
    '    Dim con As String = ConfigurationManager.ConnectionStrings("constr").ConnectionString
    '    Dim dt1 As New DataTable
    '    Dim str As String = qry
    '    Dim dap As New OracleDataAdapter(str, con)
    '    dap.Fill(dt1)
    '    Return dt1
    'End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        mailsend()
    End Sub


End Class
