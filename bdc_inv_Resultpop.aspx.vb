Imports System.Data
Imports System.IO
Imports Telerik.Web.UI
Imports System.Text
Imports System.Environment

Imports System.Net.Mail
Imports System.Net.Mime

Partial Class _Default
    Inherits System.Web.UI.Page
    Dim clas As rahulcpm.commonclass = New rahulcpm.commonclass()

    Dim qry As String
    Dim dt As DataTable
    Dim sydate As String
    Dim agntid As Int32
    Dim msg As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Page.IsPostBack = False Then
            'If Request.QueryString("frm") IsNot Nothing And Not Request.QueryString("frm") = "" Then
            '    Session("mode") = ""
            'End If
            'Session("mode") = ""
            fillcand()
            fillRound()
            getmastres()
            fillRoundby()

            binddates(dt_schedule, Date.Today.ToString("dd-MMM-yyyy"), Date.Today.ToString("dd-MMM-yyyy"), Date.Today.AddDays(15).ToString("dd-MMM-yyyy"), True)
            binddates(dt_schedule2, "", Date.Today.ToString("dd-MMM-yyyy"), Date.Today.AddDays(20).ToString("dd-MMM-yyyy"), True)


        Else

        End If

        If Session("lgnagntid") = "0" Or Session("lgnagntid") = "" Then
            'Response.Redirect("login.aspx")
            ImageButton1.Enabled = False
            Button2.Enabled = False
            RadWindowManager1.RadAlert("session is expired, Please login Again", 300, 200, "Session Expired Error", "")
            Response.Redirect("logout.aspx")
        Else
            agntid = Session("lgnagntid")
        End If

        'If Request.QueryString("mode") = "edit" And Session("mode") = "" Then
        '    getrcdforedit()
        'Button2.Enabled = False
        'Else

        'End If
        CheckParam()
    
        ' Ifrm1.Attributes.Add("src", "cat_det.aspx?mode=C")

    End Sub

    Private Sub disable()
        ddl_Round.Enabled = False
        DropDownList1.Enabled = False
        TextBox1.Enabled = False
        ImageButton1.Enabled = False
        Button2.Enabled = False
        ddlConduct.Enabled = False
        ddlReason.Enabled = False
    End Sub

    Private Sub checkparam()
        Try
            If Request.QueryString("res") = "106" Then
                disable()
            End If

            If Request.QueryString("logid") <> "" Then

                qry = "select * FROM AMS.BDC_CAND_RND_LOG a where logid='" & Request.QueryString("logid") & "'"

                dt = clas.getdata(qry, "QR")
                If dt.Rows.Count > 0 Then
                    ddl_Round.Items.FindByValue(Convert.ToString(dt.Rows(0)("CURROUND") + 1)).Selected = True
                    ddlConduct.Items.FindByValue(Convert.ToString(dt.Rows(0)("NXTROUNDBY"))).Selected = True
                    ddl_Round.Enabled = False
                    ddlConduct.Enabled = False
                End If
            End If
        Catch
            ddl_Round.Enabled = False
            ddlConduct.Enabled = False
        End Try


    End Sub
    Protected Sub binddates(ByVal cntl As Control, ByVal seldate As String, ByVal mindate As String, ByVal maxdate As String, ByVal enbl As Boolean)

        Dim cnrl1 As RadDatePicker = TryCast(cntl, RadDatePicker)
        If Not seldate.Trim = "" Then
            cnrl1.SelectedDate = seldate
        Else
            '  cnrl1.SelectedDate = Date.Today.ToString("dd-MMM-yyyy")
        End If
        If Not mindate.Trim = "" Then
            cnrl1.MinDate = mindate
        Else
            cnrl1.MinDate = Date.Today.ToString("dd-MMM-yyyy")
        End If
        If Not maxdate.Trim = "" Then
            cnrl1.MaxDate = maxdate
        Else
            cnrl1.MaxDate = Date.Today.ToString("dd-MMM-yyyy")
        End If
        cnrl1.Enabled = enbl
    End Sub

    Private Sub getmastres()
        Try
            If Request.QueryString("candid") = "" Then Exit Sub

            'qry = "select advid,LOGID,candid,(select candname from ams.bdc_lead_master b where b.candid=a.candid) as Name ,(select roundname from ams.bdc_rnd_mst where roundid=prvround) as PreviousRound,(select roundname from ams.bdc_rnd_mst where roundid=CURROUND) as CurrentRound,(select statdescr from ams.ams_statmaster where statid =STATUS) as Status,CANDFDBCK as Feedback,(select name from tt.employees where empno=conductedby) as RoundBy,to_char(ROUNDDAT,'dd-Mon-yyyy') as ScheduleOn FROM AMS.BDC_CAND_RND_LOG a where candid='" & Request.QueryString("candid") & "' order by ROUNDDAT"
            qry = "select advid,LOGID,candid ,(select roundname from ams.bdc_rnd_mst where roundid=prvround) as PreviousRound,(select roundname from ams.bdc_rnd_mst where roundid=CURROUND) as CurrentRound,(select statdescr from ams.ams_statmaster where statid =STATUS) as Status,CANDFDBCK as Feedback,(select name from tt.employees where empno=conductedby) as RoundBy,to_char(ROUNDDAT,'dd-Mon-yyyy') as ScheduleOn FROM AMS.BDC_CAND_RND_LOG a where candid='" & Request.QueryString("candid") & "' order by ROUNDDAT"
            dt = clas.getdata(qry, "QR")
            If dt.Rows.Count > 0 Then
                RadGrid1.DataSource = dt
                RadGrid1.DataBind()
            Else
                'Dim drnewrow As DataRow = dt.NewRow
                'dt.Rows.Add(drnewrow)
                RadGrid1.DataSource = dt
                RadGrid1.DataBind()
            End If
        Catch ex As Exception
            If ex.Message.Contains("OutOfMemory") Then
                RadWindowManager1.RadAlert("<ul><li><div style=color:Red;>This is a temporary issue; please try to Re-Login after some time.</div></li></ul>", 300, 150, "Page Loading Error", Nothing)
            Else
                Dim Err_Msg As String = ex.Message.ToString.Replace(vbCrLf, "")
                Err_Msg = Err_Msg.Replace(vbCr, "")
                Err_Msg = Err_Msg.Replace(vbLf, "")
                RadWindowManager1.RadAlert("<ul><li><div style=color:Red;>" & Err_Msg & ".</div></li></ul>", 300, 100, "Page Loading Error", Nothing)
            End If
        End Try
    End Sub
    Private Sub getrcdforedit()
        'qry = "select * from AMS.BDC_LEAD_MASTER where CANDID='" & Request.QueryString("CANDID") & "'"
        'dt = clas.getdata(qry, "QR")

        'If dt.Rows.Count > 0 Then
        '    Button1.Text = "Update"
        '    ' ctr_txtAdvT.Text = Convert.ToString(dt.Rows(0)("ADVTXT"))

        '    TextBox1.Text = Convert.ToString(dt.Rows(0)("CANDNAME"))
        '    TextBox2.Text = Convert.ToString(dt.Rows(0)("CANDPHN"))
        '    TextBox3.Text = Convert.ToString(dt.Rows(0)("CANDEML"))


        '    If IsDBNull(dt.Rows(0)("RESMRECDDAT")) = False Then
        '        dt_resdate.MinDate = Date.Now.AddYears(-10)
        '        dt_resdate.MaxDate = Date.Now.AddDays(1)
        '        dt_resdate.SelectedDate = dt.Rows(0)("RESMRECDDAT")
        '    End If
        '    If IsDBNull(dt.Rows(0)("PLTFORMID")) = False Then
        '        ddl_PF.Items.FindByValue(Convert.ToString(dt.Rows(0)("PLTFORMID"))).Selected = True
        '    End If

        '    If IsDBNull(dt.Rows(0)("ADVID")) = False Then
        '        '   ddl_adv.Items.FindByValue(Convert.ToString(dt.Rows(0)("ADVID"))).Selected = True
        '    End If


        '    Session("mode") = "edit"
        'Else
        '    Button1.Enabled = False

        '    ImageButton1.Enabled = False
        '    ImageButton1.ToolTip = " Invalid Record ID for Edit"
        'End If


    End Sub



    Private Sub fillcand()
        Try
            '            qry = " SELECT CANDNAME,CANDPHN, (select substr(ADVDESC,1,20) from AMS.ADV_MASTER a where a.advid=b.ADVID) as adv FROM AMS.BDC_LEAD_MASTER b where candid='" & Request.QueryString("candid") & "'"
            qry = "SELECT CANDNAME,CANDPHN,candeml, (select vacname from ams.adv_vacancy where vacid='" & Request.QueryString("vacid") & "' ) vacancy,(select substr(ADVDESC,1,20) from AMS.ADV_MASTER a where a.advid=b.ADVID) as adv FROM AMS.BDC_LEAD_MASTER b where candid='" & Request.QueryString("candid") & "'"
            '
            ' (SELECT VACNAME FROM AMS.ADV_VACANCY WHERE VACID=(SELECT VAC_ID FROM AMS.ADV_MASTER t1 WHERE t1.ADVID=advid and rownum=1))
            dt = clas.getdata(qry, "tx")
            If dt.Rows.Count > 0 Then
                lblName.Text = dt.Rows(0)("CANDNAME")
                lblemail.Text = dt.Rows(0)("candeml")
                Label2.Text = dt.Rows(0)("CANDPHN")
                lblVac.Text = dt.Rows(0)("vacancy")
                'Label3.Text = dt.Rows(0)("adv")
            End If
        Catch ex As Exception
            Session("error") = ex.Message.ToString
            RadWindowManager1.RadAlert(ex.Message, 300, 200, "Exception", "")

        End Try
    End Sub

    Private Sub fillRoundby()
        Try
            ' qry = "select Empno,Name from TT.EMPLOYEES where desgid in(select desgid from ranu.desgmaster where desiglevel=2 or desiglevel=3 or desiglevel=4 ) and active='T'"
            qry = " select Empno,Name || ','|| empdesignation || '('|| Department || ')' AS name  from TT.EMPLOYEES where desgid in(select desgid from ranu.desgmaster where desiglevel=2 or desiglevel=3 or desiglevel=4 ) and active='T' ORDER BY DEPARTMENT, name"

            dt = clas.getdata(qry, "tx")
            If dt.Rows.Count > 0 Then
                ddlConduct.DataTextField = "Name"
                ddlConduct.DataValueField = "Empno"
                ddlConduct.DataSource = dt
                ddlConduct.DataBind()
                ddl_roundby2.DataTextField = "Name"
                ddl_roundby2.DataValueField = "Empno"
                ddl_roundby2.DataSource = dt
                ddl_roundby2.DataBind()

            End If
        Catch ex As Exception
            Session("error") = ex.Message.ToString
            RadWindowManager1.RadAlert(ex.Message, 300, 200, "Exception", "")

        End Try
    End Sub


    Private Sub fillRound()
        Try
            qry = "SELECT ROUNDID, ROUNDNAME  FROM AMS.BDC_RND_MST  where roundsts='A'"
            dt = clas.getdata(qry, "QR")
            ddl_Round.DataTextField = "ROUNDNAME"
            ddl_Round.DataValueField = "ROUNDID"
            ddl_Round.DataSource = dt
            ddl_Round.DataBind()

            qry = "SELECT STATID, STATDESCR  FROM AMS.AMS_STATMASTER where  statscp=101 and statstatus='A'"
            dt = clas.getdata(qry, "QR")
            DropDownList1.DataTextField = "STATDESCR"
            DropDownList1.DataValueField = "STATID"
            DropDownList1.DataSource = dt
            DropDownList1.DataBind()

        Catch ex As Exception
            Session("error") = ex.Message.ToString
            RadWindowManager1.RadAlert(ex.Message, 300, 200, "Exception", "")
        End Try
    End Sub





    Protected Sub RadGrid1_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid1.ItemDataBound
        'RadGrid1.MasterTableView.GetColumn("advimage").Visible = False
        RadGrid1.MasterTableView.GetColumn("ADVID").Visible = False
        RadGrid1.MasterTableView.GetColumn("LOGID").Visible = False
        RadGrid1.MasterTableView.GetColumn("CANDID").Visible = False
        If (TypeOf e.Item Is GridDataItem) Then
            'Dim item As GridDataItem = e.Item
            'Dim btnedit As ImageButton = DirectCast(item.FindControl("btnedit"), ImageButton)
            'Dim btnget As ImageButton = DirectCast(item.FindControl("btnget"), ImageButton)
            'Dim btnCand As ImageButton = DirectCast(item.FindControl("btnCand"), ImageButton)
            'Dim lnkBtn As LinkButton = TryCast(item.FindControl("lnkshowpost"), LinkButton)

            'btnedit.ImageUrl = "Images/add_rec.png"
            'btnedit.ToolTip = "Edit Record"
            'btnedit.CommandName = "editform"
            'btnedit.CommandArgument = item.GetDataKeyValue("advid").ToString()

            'btnget.ImageUrl = "Images/publishing_ico.png"
            'btnget.ToolTip = "Add Publish Details"
            'btnget.CommandName = "PostDetail"
            'btnget.CommandArgument = item.GetDataKeyValue("advid").ToString()

            'btnCand.ImageUrl = "Images/add_candates.png"
            'btnCand.ToolTip = "Add Candidate Details"
            'btnCand.CommandName = "CandDetail"
            'btnCand.CommandArgument = item.GetDataKeyValue("advid").ToString()
        End If
    End Sub


    Protected Sub RadGrid1_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.ItemCommand
        If e.CommandName = "securedjob" Then
            Label1.Text = "My Job Orders"
            'Ifrmfollowup.Attributes.Add("src", "jobsecure_list.aspx?empid=" & e.CommandArgument.ToString())
            '  ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            'ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)
            'ClientScript.RegisterClientScriptBlock([GetType](), "none", "openModal();", False)
            'ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", True)

        ElseIf e.CommandName = "refresh" Then
            For Each column As GridColumn In RadGrid1.MasterTableView.RenderColumns
                column.CurrentFilterFunction = GridKnownFunction.NoFilter
                column.CurrentFilterValue = String.Empty
            Next
            RadGrid1.MasterTableView.FilterExpression = String.Empty
            RadGrid1.Rebind()
            Label1.Text = ""
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            getmastres()
            'Ifrmfollowup.Attributes.Add("src", "followup_list.aspx?empid=" & e.CommandArgument.ToString())
            'ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)
        ElseIf e.CommandName = "Filter" Then
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            getmastres()
        ElseIf e.CommandName = "ChangePageSize" Then
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            getmastres()
        End If
    End Sub

    Protected Sub RadGrid1_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadGrid1.PreRender
        If Page.IsPostBack = False Then
            'Dim column As GridColumn = TryCast(RadGrid1.MasterTableView.GetColumnSafe("rpst1"), GridColumn)
            'column.OrderIndex = RadGrid1.MasterTableView.RenderColumns.Length - 1
            'RadGrid1.MasterTableView.Rebind()
        End If

    End Sub
    Protected Sub RadGrid1_SortCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridSortCommandEventArgs) Handles RadGrid1.SortCommand
        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
        getmastres()
    End Sub


    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click, ImageButton1.Click
        Try
            ' Dim fileName As String
            Dim flag As Boolean


            If Button1.Text = "Update" Then

                flag = updateCommand()

                If flag = True Then
                    RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Information Updated Successfully </div></li></ul>", 300, 150, "Validation Success", Nothing)
                End If
            Else

                Dim str As StringBuilder = New StringBuilder

                str.Append("insert into   AMS.BDC_CAND_RND_LOG (LOGID,CANDID,ADVID,PRVROUND,STATUS,")


                If ddl_Round.SelectedValue <> "" Then
                    str.Append("CURROUND,")
                End If

                If TextBox1.Text.Trim <> "" Then
                    str.Append("CANDFDBCK, ")
                End If

                If ddlConduct.SelectedValue <> "" Then
                    str.Append("CONDUCTEDBY,")
                End If

                If ddlReason.SelectedValue <> "" Then
                    str.Append("REASON,")
                End If
                If Not dt_schedule.SelectedDate Is Nothing Then
                    If (dt_schedule.SelectedDate.ToString.Length >= 1) Then
                        str.Append("ROUNDDAT,")
                    End If
                End If
                'dt_schedule
                If Not dt_schedule2.SelectedDate Is Nothing Then
                    If (dt_schedule.SelectedDate.ToString.Length >= 1) Then
                        str.Append("NXTROUNDDAT,")
                    End If
                End If
                If ddl_roundby2.SelectedValue <> "" Then
                    str.Append("NXTROUNDBY,")
                End If
                str.Append("vac_id,")
                str.Append("ROUNDBY, KEYEDON, KEYEDBY, KEYEDIP) values( AMS.AMS_BRC_CAND_LOG_SEQ.nextval,")




                qry = "select CURROUND from AMS.BDC_CAND_RND_LOG where candid='" & Request.QueryString("candid") & "'"
                dt = clas.getdata(qry, "txt")
                Dim preRnd

                If (dt.Rows.Count > 0) Then
                    preRnd = dt.Rows(0)("CURROUND").ToString()
                Else
                    preRnd = "0"
                End If

                str.Append("'" & Request.QueryString("candid") & "','" & Request.QueryString("aid") & "','" & preRnd & "',")

                If DropDownList1.SelectedValue <> "" Then
                    str.Append("'" & DropDownList1.SelectedValue & "',")
                End If


                If ddl_Round.SelectedValue <> "" Then

                    str.Append("'" & ddl_Round.SelectedValue & "',")
                End If

                If TextBox1.Text.Trim <> "" Then
                    str.Append("'" & TextBox1.Text.Trim & "',")
                End If

                If ddlConduct.SelectedValue <> "" Then
                    str.Append("'" & ddlConduct.SelectedValue & "',")

                End If

                If ddlReason.SelectedValue <> "" Then
                    str.Append("'" & ddlReason.SelectedValue & "',")
                End If


                If Not dt_schedule.SelectedDate Is Nothing Then
                    If (dt_schedule.SelectedDate.ToString.Length >= 1) Then
                        str.Append("'" & CDate(dt_schedule.SelectedDate).ToString("dd-MMM-yyyy") & "',")
                    End If
                End If

                If Not dt_schedule2.SelectedDate Is Nothing Then
                    If (dt_schedule2.SelectedDate.ToString.Length >= 1) Then
                        str.Append("'" & CDate(dt_schedule2.SelectedDate).ToString("dd-MMM-yyyy") & "',")
                    End If
                End If
                If ddl_roundby2.SelectedValue <> "" Then             
                    str.Append("'" & ddl_roundby2.SelectedValue & "',")
                End If
                str.Append("'" & Request.QueryString("vacid") & "',")
                str.Append(agntid & ",SYSDATE," & agntid & ",'" & HttpContext.Current.Request.UserHostAddress & "' )")
                If str.ToString().Length > 0 Then                 
                    Dim i = clas.ExecuteNonQuery(str.ToString())
                    If i = 1 Then
                        If Request.QueryString("logid") <> "" Then
                            'set reson=1 if round details updated
                            qry = "update  ams.bdc_cand_rnd_log  set reason=1 where logid='" & Request.QueryString("logid") & "'"
                            Dim k = clas.ExecuteNonQuery(qry)
                        End If
                        RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Transaction Successfull </div></li></ul>", 300, 150, "Validation Success", Nothing)
                        Email_thru_MIS("")
                        If ddl_Round.SelectedValue <> "" Then
                            'qry = "update  AMS.BDC_LEAD_MASTER set CANDRND='" & ddl_Round.SelectedValue & "' where candid='" & Request.QueryString("candid") & "'"
                            qry = "update  AMS.BDC_LEAD_MASTER set CANDRND='" & ddl_Round.SelectedValue & "' , CANDRESULT='" & DropDownList1.SelectedValue & "' where candid='" & Request.QueryString("candid") & "'"
                            Dim k = clas.ExecuteNonQuery(qry)
                        End If
                        getmastres()
                        clear()
                        Exit Sub
                    Else

                        Dim Err_Msg As String = clas.getexception().Replace(vbCrLf, "")
                        Err_Msg = Err_Msg.Replace(vbCr, "")
                        Err_Msg = Err_Msg.Replace(vbLf, "")
                        RadWindowManager1.RadAlert("<ul><li><div style=color:red;>System found some exception:" & Err_Msg & "</div></li></ul>", 300, 100, "Validation Exception Failure", Nothing)
                        '  RadWindowManager1.RadAlert("<ul><li><div style=color:red;> Transaction not Done </div></li></ul>", 300, 150, "Validation Success", Nothing)
                    End If

                Else
                    RadWindowManager1.RadAlert("<ul><li><div style=color:red;>Some Malware Function issue</div></li></ul>", 300, 100, "Validation Exception Failure", Nothing)
                End If
                Exit Sub
            End If
        Catch ex As Exception
            Session("error") = ex.Message.ToString
            RadWindowManager1.RadAlert(ex.Message, 300, 200, "Exception", "")
        End Try

    End Sub


    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        clear()
        ' getmastdesg()
        ' getmastindustry()
    End Sub


    Public Sub Email_thru_MIS(ByVal RptName As String)
        If ddl_roundby2.SelectedValue = "" Or dt_schedule2.SelectedDate = "" Then
            Exit Sub
        End If
        Dim Email_From As String = "mis_alerts@acumen-services.com"
        Dim Email_To As String = "anuj@wwicsgroup.com,cpanke@gpscanada.com,ninder.hayer@acumen-services.com"
        'Dim Email_To As String = "satinder.pal@pinnacleinfoedge.com"
        'Dim Email_To As String = "meenu@pinnacleinfoedge.com"
        Dim objmail As New MailMessage(Email_From, Email_To)
        objmail.Bcc.Add("itdept@wwicsgroup.com")

        'objmail.Bcc.Add("anuj@wwicsgroup.com")
        objmail.Subject = "Acumen MIS Interview Schedule" & Convert.ToString(DateTime.Now)
        objmail.IsBodyHtml = True

        Dim Mail_Body As String = ""


        Dim Mail_Str As StringBuilder = New StringBuilder
        Mail_Str.Append("<html><head></head><title></title>")
        Mail_Str.Append("<body style='font-size:14px;font-family:Times New Roman;'>")

        Mail_Str.Append("<p><h2><Strong>Acumen MIS Interview Schedule </Strong></h2></p> <hr />")

        Mail_Str.Append("<p><b> Generated On: </b>" & DateTime.Now & " </p> <hr /> <br/>")
        Mail_Str.Append("<p>Dear User, </p>")

        Mail_Str.Append("<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Following Interview Schedule from Acumen MIS..</p>")
        'Mail_Str.Append("<p> <b>Acumen MIS Report</b></p>")



        Mail_Str.Append("<table border='1px' cellpadding='5' cellspacing='0' ")
        Mail_Str.Append("style='border: solid 1px Silver; font-size: 12px;font-family:Times New Roman;'>")

        Mail_Str.Append("<tr align='center' valign='top'>")
        Mail_Str.Append("<td align='left' valign='top'><b>&nbsp;&nbsp;")
        Mail_Str.Append("Candidate Name")
        Mail_Str.Append("&nbsp;&nbsp;</b></td>")
        Mail_Str.Append("<td align='left' valign='top'><b>&nbsp;&nbsp;")
        Mail_Str.Append("Schedule on")
        Mail_Str.Append("&nbsp;&nbsp;</b></td>")
        Mail_Str.Append("<td align='left' valign='top'><b>&nbsp;&nbsp;")
        Mail_Str.Append("Condcted by")
        Mail_Str.Append("&nbsp;&nbsp;</b></td>")
        Mail_Str.Append("<td align='left' valign='top'><b>&nbsp;&nbsp;")
        Mail_Str.Append("Round")
        Mail_Str.Append("&nbsp;&nbsp;</b></td>")
        Mail_Str.Append("</tr>")

        Mail_Str.Append("<tr align='center' valign='top'>")
        Mail_Str.Append("<td align='left' valign='top'>")
        Mail_Str.Append(lblName.Text.ToUpper)
        Mail_Str.Append("</td>")
        Mail_Str.Append("<td align='left' valign='top'>")
        Mail_Str.Append(dt_schedule2.SelectedDate)
        Mail_Str.Append("</td>")
        Mail_Str.Append("<td align='left' valign='top'>")
        Mail_Str.Append(ddl_roundby2.SelectedItem.Text)
        Mail_Str.Append("</td>")
        Mail_Str.Append("<td align='left' valign='top'>")
        Mail_Str.Append(ddl_Round.SelectedItem.Text)
        Mail_Str.Append("</td>")
        Mail_Str.Append("</tr>")


        Mail_Str.Append("</table> <br/>")
        Mail_Str.Append("<p> <b>Regards,</b></p>")
        Mail_Str.Append("<p> MIS (z-axis) </p>")
        Mail_Str.Append("</body> </html>")

        Mail_Body = Mail_Str.ToString

        objmail.Body = Mail_Body
        Dim objsent As New SmtpClient("121.0.0.219")
        objsent.UseDefaultCredentials = True
        objsent.Credentials = New System.Net.NetworkCredential("mis_alerts@acumen-services.com", "Malerts")
        objsent.Send(objmail)
        'ClientScript.RegisterStartupScript([GetType](), "alert", "alert('Email sent to registered email id');window.close();", True)
    End Sub

    Public Function updateCommand() As Boolean
        Try
            'Dim fileName As String
            ''Dim transid As Int32 = getMaxID()
            ''Dim str2 As String = "insert into tt.ukdata(transid, InDocRecdDt,InDocRecdBy, InDocStat) values(2, to_date(@InDocRecdDt,'dd-MON-yyyy'), @InDocRecdBy, @InDocStat)"
            'Dim str As StringBuilder = New StringBuilder
            'str.Append("update  AMS.BDC_LEAD_MASTER set ")

            'If ddl_PF.SelectedValue <> "" Then
            '    str.Append("PLTFORMID='" & ddl_PF.SelectedValue & "',")
            'End If

            ''If ddl_adv.SelectedValue <> "" Then
            ''    str.Append("ADVID='" & ddl_adv.SelectedValue & "',")
            ''End If


            'If Not dt_resdate.SelectedDate Is Nothing Then
            '    If (dt_resdate.SelectedDate.ToString.Length >= 1) Then
            '        str.Append("RESMRECDDAT='" & CDate(dt_resdate.SelectedDate).ToString("dd-MMM-yyyy") & "',")
            '    End If
            'End If


            ''If ctr_pstBy.SelectedValue <> "" Then
            ''    str.Append("POSTBY='" & ctr_pstBy.SelectedValue & "',")
            ''End If
            ''If ctr_pstStatus.SelectedValue <> "" Then
            ''    str.Append("POSTSTS='" & ctr_pstStatus.SelectedValue & "',")
            ''End If


            'If TextBox1.Text <> "" Then
            '    str.Append("CANDNAME='" & TextBox1.Text.Trim & "',")
            'End If

            'If TextBox2.Text <> "" Then
            '    str.Append("CANDPHN='" & TextBox2.Text.Trim & "',")
            'End If

            'If TextBox3.Text <> "" Then
            '    str.Append("CANDEML='" & TextBox3.Text.Trim & "',")
            'End If



            'If fupl.HasFile = True Then

            '    fileName = Path.GetFileName(fupl.PostedFile.FileName).ToLower()


            '    If fileName = "" Then
            '        'RadWindowManager1.RadAlert("<div style=color:green;> Please browse your required file. </div>", 300, 150, "Validation Success", Nothing)
            '        'Exit Sub

            '    Else

            '        If fileName.Contains("%") Or fileName.Contains("'") Or fileName.Contains("""") Or fileName.Contains("&") Then
            '            RadWindowManager1.RadAlert("<div style=color:red;> Invalid File Name. </div>", 300, 150, "Validation Success", Nothing)
            '            Exit Function
            '        End If

            '        fileName = Replace(fileName.Replace("""", "").Replace("%", "").Replace("&", "").Replace("+", ""), "'", "")

            '        Dim actfileName As String = fileName

            '        Dim ext As String = Path.GetExtension(fupl.PostedFile.FileName)
            '        '  Dim ext As String = Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower()
            '        Dim fileSize As Long = fupl.PostedFile.ContentLength
            '        If fileSize < 3145728 Then
            '            If ext.ToLower.Contains(".PDF") Or ext.ToLower.Contains(".DOC") Then
            '                fileName = fileName.Replace(" ", "_")

            '                Dim FileNM1 As String() = fileName.Split(".")
            '                Dim datestr As String = String.Format("{0:ddMMMyyyyHmmss}", DateTime.Now)
            '                fileName = FileNM1(0) & datestr & ext

            '                Dim Path As String = (Server.MapPath("BDC_Resume/") + fileName)
            '                fupl.PostedFile.SaveAs(Path)

            '                Dim client_ip As String = Request.UserHostAddress()
            '                Dim Browser_Name As String = Request.Browser.Browser.ToString & " " & Request.Browser.Version.ToString

            '            Else
            '                RadWindowManager1.RadAlert("<div style=color:Red;> Please upload Advertiement in doc/pdf format. </div>", 300, 150, "Validation Error", Nothing)
            '                Exit Function
            '            End If

            '        Else
            '            RadWindowManager1.RadAlert("<div style=color:Red;> Document should not contain file size more than 3 MB. </div>", 300, 150, "Validation Error", Nothing)
            '            Exit Function
            '        End If
            '    End If


            'Else
            '    'RadWindowManager1.RadAlert("<div style=color:Red;> Please Choose File..... </div>", 300, 150, "Validation Error", Nothing)
            '    'Exit Sub
            'End If

            'If fupl.HasFile Then
            '    str.Append("RESUMEPATH='" & fileName & "',")
            'End If

            'Dim temp As String = str.ToString
            'str.Clear()
            'str.Append(temp.Substring(0, temp.Length - 1))
            'str.Append(",LASTUPDON=sysdate,LASTUPDBY='" & agntid & "' where CANDID='" & Request.QueryString("CANDID") & "'")

            'If str.ToString().Length > 0 Then

            '    Dim i = clas.ExecuteNonQuery(str.ToString())
            '    If i = 1 Then
            '        Return True
            '    End If
            'End If


        Catch ex As Exception
            Session("error") = ex.Message.ToString
            RadWindowManager1.RadAlert(ex.Message, 300, 200, "Exception", "")
            'Dim AlertMessage As String = "alert('" & ex.Message.ToString & "')"
            'ScriptManager.RegisterStartupScript(Page, Page.GetType, "AlertMessage", AlertMessage, True)
            Return False
        End Try

    End Function


    Private Sub clear()
        TextBox1.Text = String.Empty
        ddl_Round.ClearSelection()
        DropDownList1.ClearSelection()
        ddlConduct.ClearSelection()
        ddlReason.ClearSelection()
        dt_schedule.Clear()
        ddl_roundby2.ClearSelection()
        dt_schedule2.Clear()
    End Sub


    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DropDownList1.SelectedIndexChanged
        Try

            If (DropDownList1.SelectedValue = 104 Or DropDownList1.SelectedValue = 107) Then  'Reject/Discard/ put on hold 
                pnlReason.Visible = True
                fillreason()
                pnl1.Visible = False
                pnl2.Visible = False
            ElseIf DropDownList1.SelectedValue = 106 Then 'Shortlist
                pnlReason.Visible = False
                pnl1.Visible = False
                pnl2.Visible = False
            ElseIf DropDownList1.SelectedValue = 110 Then 'Move to Candidate Pool
                pnlReason.Visible = False
                pnl1.Visible = False
                pnl2.Visible = False          
            Else
                pnlReason.Visible = False
                pnl1.Visible = True
                pnl2.Visible = True
            End If
        Catch ex As Exception
            Session("error") = ex.Message.ToString
            RadWindowManager1.RadAlert(ex.Message, 300, 200, "Exception", "")
            'Dim AlertMessage As String = "alert('" & ex.Message.ToString & "')"
            'ScriptManager.RegisterStartupScript(Page, Page.GetType, "AlertMessage", AlertMessage, True)

        End Try

    End Sub
    Private Sub fillreason()

        If DropDownList1.SelectedValue = 104 Then
            qry = "SELECT statid, statdescr  FROM AMS.ams_statmaster  where statstatus='A' and statscp=300"
            dt = clas.getdata(qry, "QR")
            ddlReason.DataTextField = "statdescr"
            ddlReason.DataValueField = "statid"
            ddlReason.DataSource = dt
            ddlReason.DataBind()

        ElseIf DropDownList1.SelectedValue = 107 Then
            qry = "SELECT statid, statdescr  FROM AMS.ams_statmaster  where statstatus='A' and statscp=325"
            dt = clas.getdata(qry, "QR")
            ddlReason.DataTextField = "statdescr"
            ddlReason.DataValueField = "statid"
            ddlReason.DataSource = dt
            ddlReason.DataBind()
        End If
    End Sub



End Class
