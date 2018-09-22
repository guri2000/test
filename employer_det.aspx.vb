Imports System.Data
Imports System.IO
Imports Telerik.Web.UI
Imports System.Text
Imports System.Environment
Imports System.Net

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
            ' If Request.QueryString("frm") IsNot Nothing And Not Request.QueryString("frm") = "" Then
            Session("mode") = ""
            'End If
            getmastres()
            getmastdesg()
            getmastindustry()
            getminmax()
            label2.Text = Session("lgnagntnam")
            label3.Text = Session("lgnagntnam")

        Else

        End If

        'If Not Session("qry")  = "" Then
        '   Response.Write(Session("qry"))
        '  End If

        If Session("lgnagntid") = "0" Or Session("lgnagntid") = "" Then
            'Response.Redirect("login.aspx")
            ImageButton1.Enabled = False
            Button2.Enabled = False
            RadWindowManager1.RadAlert("session is expired, Please login Again", 300, 200, "Session Expired Error", "")
            Response.Redirect("logout.aspx")
        Else
            agntid = Session("lgnagntid")
        End If

        ' If Session("lgnroll") = "S" Then
        ' quest.Visible = True
        '  If Page.IsPostBack = False Then FILLCTR()
        ' FILLCTR()
        'Else
        'quest.Visible = False
        ' End If

        FILLCTR()

        If Request.QueryString("mode") = "edit" And Session("mode") = "" Then
            TextBox1.Enabled = False
            TextBox2.Enabled = False
            TextBox3.Enabled = False
            DropDownList4.Enabled = False
            getrcdforedit()
            'If Session("lgnroll") = "S" Then
            '    chkrating()
            'End If
            chkrating()
            ' fillCTr_edit()
        Else

        End If


        If Request.QueryString("s") = "rating" Then
            'If Session("lgnroll") = "S" Then
            '    chkrating()
            'End If
            chkrating()
            binddates(dtSubmission2, Date.Today.ToString("dd-MMM-yyyy"), "", "", False)
            Pnl_Rating.Visible = True
            If Page.IsPostBack = False Then getmasters1()
        End If


        If Request.QueryString("typ") = "D" Then

            Button1.Enabled = False
            ImageButton1.Enabled = False
            ImageButton1.ToolTip = "User Mode Screening"
        End If


        Ifrm1.Attributes.Add("src", "SetDisposition.aspx?Disposition=" & "Designation")

        Ifrm_ind.Attributes.Add("src", "SetDisposition.aspx?Disposition=" & "Industry")

    End Sub

    Protected Sub getmasters1()
        qry = "select (select AGNTNAME from AMS.AMS_AGENTS where AGNTID=SCRNRMKSBY) as Screening_By,To_char(SCRNRMKSDATE, 'DD-MON-YYYY') as Scr_Date,SCRNRATING as Rating1,SCRNRMKS as Remarks,SCRNRMKSBY from ams.AMS_SCREENING scr where scr.empid='" & Request.QueryString("empid") & "'"

        dt = clas.getdata(qry, "QR")

        RadGrid1.DataSource = dt
        RadGrid1.DataBind()

        If dt.Rows.Count > 0 Then
            Dim dv As DataView = dt.DefaultView

            dv.RowFilter = "SCRNRMKSBY='" & Convert.ToString(Session("lgnagntid")) & "'"
            Dim dt2 As DataTable = dv.ToTable()
            If dt2.Rows.Count > 0 Then
                If IsDBNull(dt2.Rows(0)("Rating1")) = False Then
                    RadRating1.Value = Convert.ToString(dt2.Rows(0)("Rating1"))
                End If
                If IsDBNull(dt2.Rows(0)("Remarks")) = False Then
                    txtRatRem.Text = Convert.ToString(dt2.Rows(0)("Remarks"))
                End If
                If IsDBNull(dt2.Rows(0)("Screening_By")) = False Then
                    label3.Text = Convert.ToString(dt2.Rows(0)("Screening_By"))
                End If
                If IsDBNull(dt2.Rows(0)("Scr_Date")) = False Then
                    dtSubmission2.MinDate = Date.Now.AddYears(-10)
                    dtSubmission2.MaxDate = Date.Now.AddDays(1)
                    dtSubmission2.SelectedDate = Convert.ToString(dt2.Rows(0)("Scr_Date"))

                End If
            End If
        Else

        End If
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
        qry = "select * from ams.ams_statmaster where statstatus='A' and statscp=1"
        dt = clas.getdata(qry, "QR")
        If dt.Rows.Count > 0 Then
            DropDownList4.DataTextField = "statdescr"
            DropDownList4.DataValueField = "statid"
            DropDownList4.DataSource = dt
            DropDownList4.DataBind()
        End If

    End Sub
    Private Sub getrcdforedit()
        qry = "select a.*,b.agntname, c.agntname srnnma from ams.ams_EMPLOYER a, ams.ams_agents b, ams.ams_agents c where  a.agentid=b.agntid and  a.SCRNRMKSBY=c.agntid(+) and a.empid='" & Request.QueryString("empid") & "'"
        dt = clas.getdata(qry, "QR")
        If dt.Rows.Count > 0 Then
            TextBox1.Text = Convert.ToString(dt.Rows(0)("empfname"))
            TextBox2.Text = Convert.ToString(dt.Rows(0)("empmname"))
            TextBox3.Text = Convert.ToString(dt.Rows(0)("emplname"))
            TextBox4.Text = Convert.ToString(dt.Rows(0)("emptel"))
            TextBox5.Text = Convert.ToString(dt.Rows(0)("empmbl"))
            TextBox6.Text = Convert.ToString(dt.Rows(0)("empcmpnam"))
            TextBox7.Text = Convert.ToString(dt.Rows(0)("empcmptelno"))
            TextBox8.Text = Convert.ToString(dt.Rows(0)("empcmpfax"))
            TextBox9.Text = Convert.ToString(dt.Rows(0)("empcmpweb"))
            TextBox10.Text = Convert.ToString(dt.Rows(0)("empcmpadd"))
            TextBox11.Text = Convert.ToString(dt.Rows(0)("empeml"))
            If IsDBNull(dt.Rows(0)("empdesg")) = False Then
                DropDownList1.Items.FindByValue(Convert.ToString(dt.Rows(0)("empdesg"))).Selected = True
                '  DropDownList1.DataBind()
            End If
            If IsDBNull(dt.Rows(0)("empcmpind")) = False Then
                DropDownList2.Items.FindByValue(Convert.ToString(dt.Rows(0)("empcmpind"))).Selected = True
                ' DropDownList2.DataBind()
            End If
            If IsDBNull(dt.Rows(0)("result")) = False Then
                DropDownList4.Items.FindByValue(Convert.ToString(dt.Rows(0)("result").ToString.Trim)).Selected = True
                ' DropDownList4.DataBind()
                'DropDownList4.Enabled = False     
            End If

            If IsDBNull(dt.Rows(0)("Leadscope")) = False Then
                DropDownList3.Items.FindByValue(Convert.ToString(dt.Rows(0)("Leadscope").ToString.Trim)).Selected = True
                ' DropDownList4.DataBind()
                'DropDownList4.Enabled = False     
            End If

            Button1.Text = "Update"

            If IsDBNull(dt.Rows(0)("empcontdt")) = False Then
                dtSubmission.MinDate = Date.Now.AddYears(-10)
                dtSubmission.MaxDate = Date.Now.AddDays(1)
                dtSubmission.SelectedDate = dt.Rows(0)("empcontdt")
            End If

            '    If IsDBNull(dt.Rows(0)("scrnrmksdate")) = False Then
            '    dtSubmission2.MinDate = Date.Now.AddYears(-10)
            '    dtSubmission2.MaxDate = Date.Now.AddDays(1)
            '     dtSubmission2.SelectedDate = dt.Rows(0)("scrnrmksdate")
            '    End If

            dtSubmission.Enabled = False
            dtSubmission2.Enabled = False
            label2.Text = Convert.ToString(dt.Rows(0)("agntname"))
            'If IsDBNull(dt.Rows(0)("SCRNRATING")) = False Then
            '    RadRating1.Value = Convert.ToString(dt.Rows(0)("SCRNRATING"))

            'End If
            'If IsDBNull(dt.Rows(0)("SCRNRMKS")) = False Then
            '    txtRatRem.Text = Convert.ToString(dt.Rows(0)("SCRNRMKS"))
            'End If
            'If IsDBNull(dt.Rows(0)("srnnma")) = False Then
            '    label3.Text = Convert.ToString(dt.Rows(0)("srnnma"))
            'End If
            Session("mode") = "edit"

        Else
            Button1.Enabled = False

            ImageButton1.Enabled = False
            ImageButton1.ToolTip = " Invalid Record ID for Edit"
        End If

    End Sub
    Private Sub getmastindustry()
        qry = "select * from ams.ams_indmaster where indsts='A'"
        dt = clas.getdata(qry, "QR")
        If dt.Rows.Count > 0 Then
            DropDownList2.Items.Clear()
            DropDownList2.DataBind()
            DropDownList2.DataTextField = "indname"
            DropDownList2.DataValueField = "indid"
            DropDownList2.DataSource = dt
            DropDownList2.DataBind()
        End If

    End Sub
    Private Sub getmastdesg()
        qry = "select * from ams.ams_desgmaster where desgsts='A'"
        dt = clas.getdata(qry, "QR")
        If dt.Rows.Count > 0 Then
            DropDownList1.Items.Clear()
            DropDownList1.DataBind()
            DropDownList1.DataTextField = "desgname"
            DropDownList1.DataValueField = "desgid"
            DropDownList1.DataSource = dt
            DropDownList1.DataBind()
        End If

    End Sub
    Private Sub getminmax()

        dtSubmission.MinDate = Date.Now.AddYears(-10)
        dtSubmission.MaxDate = Date.Now.AddDays(+1)
        dtSubmission.SelectedDate = Date.Now


    End Sub

    Public Function FirstDayOfMonth(ByVal sourceDate As DateTime) As DateTime
        Return New DateTime(sourceDate.Year, sourceDate.Month, 1)
    End Function

    'Get the last day of the month
    Public Function LastDayOfMonth(ByVal sourceDate As DateTime) As DateTime
        Dim lastDay As DateTime = New DateTime(sourceDate.Year, sourceDate.Month, 1)
        Return lastDay.AddMonths(1).AddDays(-1)
    End Function
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click, ImageButton1.Click

        If TextBox10.Text <> "" Then
            TextBox10.Text = TextBox10.Text.Replace("'", "''")
        End If

        Dim flag As Boolean
        If Button1.Text = "Update" Then

            remove_SingleCot()

            Dim Query_Str2 As String = "Select E.EmpId,E.AgentId, E.EMPEML,Ag.AgntName,E.EmpFname || ' ' || E.EmpMName || ' ' || E.EmpLName as Name from AMS.AMS_Employer E,ams.AMS_Agents Ag  where Ag.AgntId=E.AgentId and   lower(E.EMPCMPNAM)=lower('" & TextBox6.Text.Trim & "') and E.EmpId<>'" & Request.QueryString("empid") & "' and lower(E.EmpFname)=lower('" & TextBox1.Text.Trim & "') and lower(E.EmpMName)=lower('" & TextBox2.Text.Trim & "') and lower(E.EmpLName)=lower('" & TextBox3.Text.Trim & "')"
            Dim dt_chk2 As DataTable = clas.getdata(Query_Str2, "QR")
            If dt_chk2.Rows.Count > 0 Then
                CmpMsg.Visible = True
                CmpMsg.InnerText = "This Company Name is already registered by BDC: " & dt_chk2.Rows(0)("AgntName") & vbCrLf & " for Employer : " & dt_chk2.Rows(0)("Name") & ". Please Enter some other Company Name."
                Exit Sub
            End If

            Dim Query_Str As String = "Select * from AMS.Act_EMPLOYER_DATA Where  lower(EMPEML)=lower('" & TextBox11.Text.Trim & "') and EmpId<>'" & Request.QueryString("empid") & "'"
            Dim dt_chk As DataTable = clas.getdata(Query_Str, "QR")
            If dt_chk.Rows.Count > 0 Then
                EmlMsg.Visible = True
                EmlMsg.InnerText = "This Email-Id is already registered by BDC: " & dt_chk.Rows(0)("AgntName") & vbCrLf & ". Please Enter some other Email-Id."
                a1.Attributes.Add("style", "display:inline")
                Ifrmfollowup.Attributes.Add("src", "followup_list.aspx?empid=" & dt_chk.Rows(0)("EmpId") & "&mod=D")
                'RadWindowManager1.RadAlert("<ul><li><div style=color:green;> This Email-Id is already registered by BDC: " & dt_chk.Rows(0)("AgntName") & vbCrLf & ". Please Enter some other Email-Id.</div></li></ul>", 300, 150, "Validation Success", Nothing)
                Exit Sub
            Else
                a1.Attributes.Add("style", "display:none")
                Ifrmfollowup.Attributes.Remove("src")
            End If

            Dim Query_Str1 As String = "Select E.EmpId,E.AgentId, E.EMPEML,Ag.AgntName from AMS.AMS_Employer E,ams.AMS_Agents Ag  where Ag.AgntId=E.AgentId and   lower(EMPCMPWEB)=lower('" & TextBox9.Text.Trim & "') and E.EmpId<>'" & Request.QueryString("empid") & "'"
            Dim dt_chk1 As DataTable = clas.getdata(Query_Str1, "QR")
            If dt_chk1.Rows.Count > 0 Then
                WebMsg.Visible = True
                WebMsg.InnerText = "This Website is already registered by BDC: " & dt_chk1.Rows(0)("AgntName") & vbCrLf & ". Please Enter some other Website."
                Exit Sub
            End If
            If Request.QueryString("s") = "rating" Then
                'If txtRatRem.Text.Trim = "" Then
                '    RadWindowManager1.RadAlert("<ul><li><div style=color:red;>Please Enter Rating Remarks.  </div></li></ul>", 300, 100, "Validation Failure", Nothing)
                '    txtRatRem.Focus()
                'End If

                If RadRating1.Value = 0 Then
                    RadWindowManager1.RadAlert("<ul><li><div style=color:red;>Please Choose Rating  </div></li></ul>", 300, 100, "Validation Failure", Nothing)
                    ' RadRating1.Focus()
                    Exit Sub
                End If

            End If
            flag = updateCommand()
        Else

            remove_SingleCot()

            Dim Query_Str2 As String = "Select E.EmpId,E.AgentId, E.EMPEML,Ag.AgntName from AMS.AMS_Employer E,ams.AMS_Agents Ag  where Ag.AgntId=E.AgentId and   lower(E.EMPCMPNAM)=lower('" & TextBox6.Text.Trim & "') and lower(E.EmpFname)=lower('" & TextBox1.Text.Trim & "') and lower(E.EmpMName)=lower('" & TextBox2.Text.Trim & "') and lower(E.EmpLName)=lower('" & TextBox3.Text.Trim & "')"
            Dim dt_chk2 As DataTable = clas.getdata(Query_Str2, "QR")
            If dt_chk2.Rows.Count > 0 Then
                CmpMsg.Visible = True
                CmpMsg.InnerText = "This Company Name is already registered by BDC: " & dt_chk2.Rows(0)("AgntName") & vbCrLf & ". Please Enter some other Company Name."
                Exit Sub
            End If

            Dim Query_Str As String = "Select * from AMS.Act_EMPLOYER_DATA Where lower(EMPEML)=lower('" & TextBox11.Text.Trim & "')"
            Dim dt_chk As DataTable = clas.getdata(Query_Str, "QR")
            If dt_chk.Rows.Count > 0 Then
                EmlMsg.Visible = True
                EmlMsg.InnerText = "This Email-Id is already registered by BDC: " & dt_chk.Rows(0)("AgntName") & vbCrLf & ". Please Enter some other Email-Id."
                a1.Attributes.Add("style", "display:inline")
                Ifrmfollowup.Attributes.Add("src", "followup_list.aspx?empid=" & dt_chk.Rows(0)("EmpId") & "&mod=D")
                'RadWindowManager1.RadAlert("<ul><li><div style=color:green;> This Email-Id is already registered by BDC: " & dt_chk.Rows(0)("AgntName") & vbCrLf & ". Please Enter some other Email-Id.</div></li></ul>", 300, 150, "Validation Success", Nothing)
                Exit Sub
            Else
                a1.Attributes.Add("style", "display:none")
                Ifrmfollowup.Attributes.Remove("src")
            End If

            Dim Query_Str1 As String = "Select E.EmpId,E.AgentId, E.EMPEML,Ag.AgntName from AMS.AMS_Employer E,ams.AMS_Agents Ag  where Ag.AgntId=E.AgentId and   lower(EMPCMPWEB)=lower('" & TextBox9.Text.Trim & "')"
            Dim dt_chk1 As DataTable = clas.getdata(Query_Str1, "QR")
            If dt_chk1.Rows.Count > 0 Then
                WebMsg.Visible = True
                WebMsg.InnerText = "This Website is already registered by BDC: " & dt_chk1.Rows(0)("AgntName") & vbCrLf & ". Please Enter some other Website."
                Exit Sub
            End If

            flag = InsertCommand()
        End If

        If flag = True Then
            If Button1.Text = "Update" Then
                RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Prospect Information Updated Successfully </div></li></ul>", 300, 150, "Validation Success", Nothing)
            Else
                clear()
                RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Prospect Information Saved Successfully </div></li></ul>", 300, 150, "Validation Success", Nothing)
            End If


        Else
            Session.Remove("mode")
            Dim Err_Msg As String = Convert.ToString(Session("error")).Replace(vbCrLf, "")
            Err_Msg = Err_Msg.Replace(vbCr, "")
            Err_Msg = Err_Msg.Replace(vbLf, "")
            RadWindowManager1.RadAlert("<ul><li><div style=color:green;>" & Err_Msg & "</div></li></ul>", 300, 150, "Validation Unsuccessful", Nothing)
            'Dim message As String = "alert('" & Session("error").ToString & "')"
            'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
        End If
    End Sub

    Protected Sub remove_SingleCot()
        If TextBox1.Text <> "" Then
            TextBox1.Text = TextBox1.Text.Replace("'", "")
        End If
        If TextBox2.Text <> "" Then
            TextBox2.Text = TextBox2.Text.Replace("'", "")
        End If
        If TextBox3.Text <> "" Then
            TextBox3.Text = TextBox3.Text.Replace("'", "")
        End If

        If TextBox4.Text <> "" Then
            TextBox4.Text = TextBox4.Text.Replace("'", "")
        End If
        If TextBox5.Text <> "" Then
            TextBox5.Text = TextBox5.Text.Replace("'", "")
        End If
        If TextBox11.Text <> "" Then
            TextBox11.Text = TextBox11.Text.Replace("'", "")
        End If

        If TextBox6.Text <> "" Then
            TextBox6.Text = TextBox6.Text.Replace("'", "")
        End If
        If TextBox7.Text <> "" Then
            TextBox7.Text = TextBox7.Text.Replace("'", "")
        End If
        If TextBox8.Text <> "" Then
            TextBox8.Text = TextBox8.Text.Replace("'", "")
        End If
        If TextBox9.Text <> "" Then
            TextBox9.Text = TextBox9.Text.Replace("'", "")
        End If
        If TextBox10.Text <> "" Then
            TextBox10.Text = TextBox10.Text.Replace("'", "")
        End If
        If txtRatRem.Text <> "" Then
            txtRatRem.Text = txtRatRem.Text.Replace("'", "")
        End If


    End Sub


    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        clear()
        ' getmastdesg()
        ' getmastindustry()
    End Sub
    Public Function InsertCommand() As Boolean
        Try
            'Dim transid As Int32 = getMaxID()
            'Dim str2 As String = "insert into tt.ukdata(transid, InDocRecdDt,InDocRecdBy, InDocStat) values(2, to_date(@InDocRecdDt,'dd-MON-yyyy'), @InDocRecdBy, @InDocStat)"
            Dim str As StringBuilder = New StringBuilder
            str.Append("insert into ams.ams_EMPLOYER(EMPID,")


            If TextBox1.Text <> "" Then
                str.Append("empfname,")
            End If
            If TextBox2.Text <> "" Then
                str.Append("empmname,")
            End If
            If TextBox3.Text <> "" Then
                str.Append("emplname,")
            End If
            If DropDownList1.SelectedValue <> "" Then
                str.Append("empdesg,")
            End If
            If TextBox4.Text <> "" Then
                str.Append("emptel,")
            End If
            If TextBox5.Text <> "" Then
                str.Append("empmbl,")
            End If
            If DropDownList2.SelectedValue <> "" Then
                str.Append("empcmpind,")
            End If
            If TextBox6.Text <> "" Then
                str.Append("empcmpnam,")
            End If
            If TextBox7.Text <> "" Then
                str.Append("empcmptelno,")
            End If
            If TextBox8.Text <> "" Then
                str.Append("empcmpfax,")
            End If
            If TextBox9.Text <> "" Then
                str.Append("empcmpweb,")
            End If
            If TextBox10.Text <> "" Then
                str.Append("empcmpadd,")
            End If
            If dtSubmission.SelectedDate IsNot Nothing Then
                str.Append("empcontdt,")
            End If
            If TextBox11.Text <> "" Then
                str.Append("empeml,")
            End If
            If DropDownList4.SelectedValue <> "" Then
                str.Append("result,")
            End If
            If DropDownList3.SelectedValue <> "" Then
                str.Append("LEADSCOPE,")
            End If
            'SCRNRATING,SCRNRMKS,SCRNRMKSBY,SCRNRMKSDATE

            Dim temp As String = str.ToString
            str.Clear()
            str.Append(temp.Substring(0, temp.Length - 1))


            str.Append(",  KEYEDON,  KEYEDBY,ip,agentid) values(ams.ams_employer_seq.nextval,")

            If TextBox1.Text <> "" Then
                str.Append("'" & UppercaseFirstLetter(TextBox1.Text) & "',")
            End If
            If TextBox2.Text <> "" Then
                str.Append("'" & UppercaseFirstLetter(TextBox2.Text) & "',")
            End If
            If TextBox3.Text <> "" Then
                str.Append("'" & UppercaseFirstLetter(TextBox3.Text) & "',")
            End If
            If DropDownList1.SelectedValue <> "" Then
                str.Append("'" & DropDownList1.SelectedValue & "',")
            End If
            If TextBox4.Text <> "" Then
                str.Append("'" & TextBox4.Text & "',")
            End If
            If TextBox5.Text <> "" Then
                str.Append("'" & TextBox5.Text & "',")
            End If
            If DropDownList2.SelectedValue <> "" Then
                str.Append("'" & DropDownList2.SelectedValue & "',")
            End If
            If TextBox6.Text <> "" Then
                str.Append("'" & TextBox6.Text & "',")
            End If
            If TextBox7.Text <> "" Then
                str.Append("'" & TextBox7.Text & "',")
            End If
            If TextBox8.Text <> "" Then
                str.Append("'" & TextBox8.Text & "',")
            End If
            If TextBox9.Text <> "" Then
                str.Append("'" & TextBox9.Text & "',")
            End If
            If TextBox10.Text <> "" Then
                str.Append("'" & TextBox10.Text & "',")
            End If
            If dtSubmission.SelectedDate IsNot Nothing Then
                str.Append("to_date('" & dtSubmission.SelectedDate.Value.ToString("dd-MMM-yyyy HH:mm:ss") & "','dd-MON-yyyy hh24:mi:ss') ,")
                'str.Append("' & CDate(dtSubmission.SelectedDate).ToString("dd-MMM-yyyy") & "',")
            End If

            If TextBox11.Text <> "" Then
                str.Append("'" & TextBox11.Text & "',")
            End If
            If DropDownList4.SelectedValue <> 0 Then
                str.Append("'" & DropDownList4.SelectedValue & "',")
            End If
            If DropDownList3.SelectedValue <> "" Then
                str.Append("'" & DropDownList3.SelectedValue & "',")
            End If


            'HttpContext.Current.Request.UserHostAddress
            str.Append("sysdate," & agntid & ",'" & HttpContext.Current.Request.UserHostAddress & "','" & agntid & "',")

            temp = str.ToString
            str.Clear()
            str.Append(temp.Substring(0, temp.Length - 1))
            'SCRNRATING,SCRNRMKS,SCRNRMKSBY,SCRNRMKSDATE



            str.Append(")")
            ' Try


            'Response.Write(str.ToString())

            Dim i = clas.ExecuteNonQuery(str.ToString())
            If i = 1 Then
                If quest.Visible Then
                    Dim empid = clas.getMaxID("Select ams.ams_employer_seq.currval from dual")
                    saveQue(empid)
                End If
                Return True
            Else

                Dim Err_Msg As String = clas.getexception().Replace(vbCrLf, "")
                Err_Msg = Err_Msg.Replace(vbCr, "")
                Err_Msg = Err_Msg.Replace(vbLf, "")
                Err_Msg = Err_Msg.Replace("'", "")
                Session("error") = Err_Msg
                RadWindowManager1.RadAlert("<ul><li><div style=color:red;>System found some exception:" & Err_Msg & "</div></li></ul>", 300, 100, "Validation Exception Failure", Nothing)
                Return False
                '  RadWindowManager1.RadAlert("<ul><li><div style=color:red;> Transaction not Done </div></li></ul>", 300, 150, "Validation Success", Nothing)
            End If

            'InsertCommand2(transid)

            'Catch ex As Exception
            '    Session("error") = ex.Message
            '    RadWindowManager1.RadAlert("<ul><li><div style=color:Red;>" & ex.Message & "</div></li></ul>", 300, 150, "Validation Error", Nothing)
            '    Return False
            '    Exit Function
            '  End Try
            Throw New Exception("UnKnown Exception occured , Please Contact IT Department")
        Catch ex As Exception
            Session("error") = ex.Message.ToString
            RadWindowManager1.RadAlert(ex.Message, 300, 200, "Exception", "")
            Return False
        End Try
    End Function

    Public Function updateCommand() As Boolean
        Try
            'Dim transid As Int32 = getMaxID()
            'Dim str2 As String = "insert into tt.ukdata(transid, InDocRecdDt,InDocRecdBy, InDocStat) values(2, to_date(@InDocRecdDt,'dd-MON-yyyy'), @InDocRecdBy, @InDocStat)"
            Dim str As StringBuilder = New StringBuilder
            str.Append("update ams.ams_EMPLOYER set ")


            'If TextBox1.Text <> "" Then
            str.Append("empfname='" & UppercaseFirstLetter(TextBox1.Text) & "',")
            'End If
            'If TextBox2.Text <> "" Then
            str.Append("empmname='" & UppercaseFirstLetter(TextBox2.Text) & "',")
            'End If
            'If TextBox3.Text <> "" Then
            str.Append("emplname='" & UppercaseFirstLetter(TextBox3.Text) & "',")
            'End If
            If DropDownList1.SelectedValue <> "" Then
                str.Append("empdesg='" & DropDownList1.SelectedValue & "',")
            End If
            If TextBox4.Text <> "" Then
                str.Append("emptel='" & TextBox4.Text & "',")
            End If
            'If TextBox5.Text <> "" Then
            str.Append("empmbl='" & TextBox5.Text & "',")
            'End If
            If DropDownList2.SelectedValue <> "" Then
                str.Append("empcmpind='" & DropDownList2.SelectedValue & "',")
            End If
            'If TextBox6.Text <> "" Then
            str.Append("empcmpnam='" & TextBox6.Text & "',")
            'End If
            'If TextBox7.Text <> "" Then
            str.Append("empcmptelno='" & TextBox7.Text & "',")
            'End If
            'If TextBox8.Text <> "" Then
            str.Append("empcmpfax='" & TextBox8.Text & "',")
            'End If
            'If TextBox9.Text <> "" Then
            str.Append("empcmpweb='" & TextBox9.Text & "',")
            'End If
            'If TextBox10.Text <> "" Then
            str.Append("empcmpadd='" & TextBox10.Text & "',")
            'End If
            'If TextBox11.Text <> "" Then
            str.Append("empeml='" & TextBox11.Text & "',")
            'End If
            If DropDownList4.SelectedValue <> "" Then
                str.Append("result='" & DropDownList4.SelectedValue & "',")
            End If
            If DropDownList3.SelectedValue <> "" Then
                str.Append("LEADSCOPE='" & DropDownList3.SelectedValue & "',")
            End If




            'If Request.QueryString("s") = "rating" Then


            '    If RadRating1.Value <> 0 Then
            '        str.Append("SCRNRATING='" & RadRating1.Value & "',")
            '    End If
            '    If txtRatRem.Text.Trim <> "" Then
            '        str.Append("SCRNRMKS='" & txtRatRem.Text & "',")
            '    End If

            '    str.Append("SCRNRMKSBY='" & Session("lgnagntid") & "',")
            '    str.Append("SCRNRMKSDATE='" & CDate(dtSubmission2.SelectedDate).ToString("dd-MMM-yyyy").Trim() & "',")

            'End If




            Dim temp As String = str.ToString
            str.Clear()
            str.Append(temp.Substring(0, temp.Length - 1))
            str.Append(",lastupdateon=sysdate,lastupdateby='" & agntid & "' where empid='" & Request.QueryString("empid") & "'")


            'temp = str.ToString
            'str.Clear()
            'str.Append(temp.Substring(0, temp.Length - 1))
            'str.Append(")")
            'Try

            Dim i = clas.ExecuteNonQuery(str.ToString())
            If i = 1 Then
                
                If quest.Visible Then
                    UpdateQue()
                End If
                Dim ipaddress As String = Request.UserHostAddress

                If Request.QueryString("s") = "rating" Then

                    Dim qry As New StringBuilder
                    Dim txt As String
                    txt = " select * from AMS.AMS_SCREENING where SCRNRMKSBY='" & Session("lgnagntid") & "' and empid='" & Request.QueryString("Empid") & "'"
                    Dim _dt As DataTable
                    _dt = clas.getdata(txt, "txt")

                    If _dt.Rows.Count > 0 Then

                        qry.Append("update AMS.AMS_SCREENING  set  ")
                        If RadRating1.Value <> 0 Then
                            qry.Append("SCRNRATING='" & RadRating1.Value & "',")
                        End If
                        If txtRatRem.Text.Trim <> "" Then
                            qry.Append("SCRNRMKS='" & txtRatRem.Text & "',")
                        End If
                        qry.Append("SCRNRMKSBY='" & Session("lgnagntid") & "',")
                        qry.Append("SCRNRMKSDATE='" & CDate(dtSubmission2.SelectedDate).ToString("dd-MMM-yyyy").Trim() & "',")
                        ' qry.Append("SCRNIP='" & ipaddress & "'" & " where SCRNRMKSBY='" & Session("lgnagntid") & "' and empid='" & Request.QueryString("Empid") & "'")
                        qry.Append("SCRNIP='" & ipaddress & "'" & ",LASTUPDON=sysdate,LASTUPDBY='" & agntid & "' where SCRNRMKSBY='" & Session("lgnagntid") & "' and empid='" & Request.QueryString("Empid") & "'")

                        Dim j = clas.ExecuteNonQuery(qry.ToString())

                        If j = 0 Then

                            Dim Err_Msg As String = clas.getexception().Replace(vbCrLf, "").Replace("'", "")
                            Err_Msg = Err_Msg.Replace(vbCr, "")
                            Err_Msg = Err_Msg.Replace(vbLf, "")
                            Session("error") = Err_Msg

                            '	Response.Write(Err_Msg)

                            RadWindowManager1.RadAlert("<ul><li><div style=color:red;>System found some exception:" & Err_Msg & "</div></li></ul>", 300, 100, "Validation Exception Failure", Nothing)

                        End If


                    Else
                        ' Update question is screening is not donw


                        qry.Append("Insert into AMS.AMS_SCREENING (ID,EMPID,SCRNRATING, SCRNRMKS,SCRNRMKSBY, SCRNRMKSDATE,SCRNIP,keyedon) values ( ")
                        qry.Append("AMS.AMS_SCREENING_SEQ.nextval,")
                        qry.Append("'" & Request.QueryString("empid") & "',")
                        qry.Append("'" & RadRating1.Value & "',")
                        qry.Append("'" & txtRatRem.Text & "',")
                        qry.Append("'" & Session("lgnagntid") & "',")
                        qry.Append("'" & CDate(dtSubmission2.SelectedDate).ToString("dd-MMM-yyyy").Trim() & "',")
                        qry.Append("'" & ipaddress & "',sysdate)")
                        Dim j = clas.ExecuteNonQuery(qry.ToString())

                        If j = 0 Then

                            Dim Err_Msg As String = clas.getexception().Replace(vbCrLf, "").Replace("'", "")
                            Err_Msg = Err_Msg.Replace(vbCr, "")
                            Err_Msg = Err_Msg.Replace(vbLf, "")
                            Session("error") = Err_Msg
                            '	Response.Write(Err_Msg)
                            RadWindowManager1.RadAlert("<ul><li><div style=color:red;>System found some exception:" & Err_Msg & "</div></li></ul>", 300, 100, "Validation Exception Failure", Nothing)

                        End If

                    End If

                    getmasters1()
                    chkrating()
                End If



                Dim Flag As Boolean = Chk_Data_Existance(agntid, Request.QueryString("empid"))
                If Flag = True Then
                    Update_Ques_Ans_JobSheet_Q1(TextBox6.Text, ipaddress, Request.QueryString("empid"))
                    Update_Ques_Ans_JobSheet_Q2(UppercaseFirstLetter(TextBox1.Text) & " " & UppercaseFirstLetter(TextBox2.Text) & " " & UppercaseFirstLetter(TextBox3.Text) & " , " & UppercaseFirstLetter(DropDownList1.SelectedItem.Text), ipaddress, Request.QueryString("empid"))
                    Update_Ques_Ans_JobSheet_Q3(TextBox9.Text, ipaddress, Request.QueryString("empid"))
                    Update_Ques_Ans_JobSheet_Q4(TextBox10.Text & " , Telephone : " & TextBox7.Text, ipaddress, Request.QueryString("empid"))
                    Update_Ques_Ans_JobSheet_Q6(TextBox8.Text, ipaddress, Request.QueryString("empid"))
                    Update_Ques_Ans_JobSheet_Q7(TextBox11.Text, ipaddress, Request.QueryString("empid"))
                    Update_Ques_Ans_JobSheet_Q8(TextBox4.Text & " , " & TextBox5.Text, ipaddress, Request.QueryString("empid"))
                End If

                Return True
            Else

                Dim Err_Msg As String = clas.getexception().Replace(vbCrLf, "")
                Err_Msg = Err_Msg.Replace(vbCr, "")
                Err_Msg = Err_Msg.Replace(vbLf, "")
                Session("error") = Err_Msg
                RadWindowManager1.RadAlert("<ul><li><div style=color:red;>System found some exception:" & Err_Msg & "</div></li></ul>", 300, 100, "Validation Exception Failure", Nothing)
                Return False
                '  RadWindowManager1.RadAlert("<ul><li><div style=color:red;> Transaction not Done </div></li></ul>", 300, 150, "Validation Success", Nothing)
            End If
            'InsertCommand2(transid)





            Return True
            'Catch ex As Exception
            '    Session("error") = ex.Message
            '    RadWindowManager1.RadAlert("<ul><li><div style=color:Red;>" & ex.Message & "</div></li></ul>", 300, 150, "Validation Error", Nothing)
            '    Return False
            '    Exit Function
            'End Try
            Throw New Exception("UnKnown Exception occured , Please Contact IT Department")
        Catch ex As Exception
            Session("error") = ex.Message.ToString
            RadWindowManager1.RadAlert(ex.Message, 300, 200, "Exception", "")
            'Dim AlertMessage As String = "alert('" & ex.Message.ToString & "')"
            'ScriptManager.RegisterStartupScript(Page, Page.GetType, "AlertMessage", AlertMessage, True)
            Return False
        End Try

    End Function

    Protected Function Chk_Data_Existance(ByVal AgentId As String, ByVal EmpId As String) As Boolean
        Dim Flag As Boolean = False
        Dim Chk_Qry As String = "Select * from Ams.Ams_Emp_Job_Ans where  EMPID='" & EmpId & "'"
        Dim _D2 As DataTable = clas.getdata(Chk_Qry, "QR")
        If _D2.Rows.Count > 0 Then
            Flag = True
        End If
        Return Flag
    End Function

    Protected Sub Update_Ques_Ans_JobSheet_Q1(ByVal Ans As String, ByVal IP As String, ByVal EmpId As String)
        Dim Update_str As StringBuilder = New StringBuilder
        Update_str.Append(" Update Ams.Ams_Emp_Job_Ans Set Ans_Desc='" & Ans & "',KeyedOn=sysdate,IP='" & IP & "' where Qid='1' and EmpId='" & EmpId & "' ")
        clas.ExecuteNonQuery(Update_str.ToString())
        'KeyedBy='" & agntid & "',
    End Sub

    Protected Sub Update_Ques_Ans_JobSheet_Q2(ByVal Ans As String, ByVal IP As String, ByVal EmpId As String)
        Dim Update_str As StringBuilder = New StringBuilder
        Update_str.Append(" Update Ams.Ams_Emp_Job_Ans Set Ans_Desc='" & Ans & "',KeyedOn=sysdate,IP='" & IP & "' where Qid='2' and EmpId='" & EmpId & "' ")
        clas.ExecuteNonQuery(Update_str.ToString())
    End Sub

    Protected Sub Update_Ques_Ans_JobSheet_Q3(ByVal Ans As String, ByVal IP As String, ByVal EmpId As String)
        Dim Update_str As StringBuilder = New StringBuilder
        Update_str.Append(" Update Ams.Ams_Emp_Job_Ans Set Ans_Desc='" & Ans & "',KeyedOn=sysdate,IP='" & IP & "' where Qid='3' and EmpId='" & EmpId & "' ")
        clas.ExecuteNonQuery(Update_str.ToString())
    End Sub

    Protected Sub Update_Ques_Ans_JobSheet_Q4(ByVal Ans As String, ByVal IP As String, ByVal EmpId As String)
        Dim Update_str As StringBuilder = New StringBuilder
        Update_str.Append(" Update Ams.Ams_Emp_Job_Ans Set Ans_Desc='" & Ans & "',KeyedOn=sysdate,IP='" & IP & "' where Qid='4' and EmpId='" & EmpId & "' ")
        clas.ExecuteNonQuery(Update_str.ToString())
    End Sub

    Protected Sub Update_Ques_Ans_JobSheet_Q6(ByVal Ans As String, ByVal IP As String, ByVal EmpId As String)
        Dim Update_str As StringBuilder = New StringBuilder
        Update_str.Append(" Update Ams.Ams_Emp_Job_Ans Set Ans_Desc='" & Ans & "',KeyedOn=sysdate,IP='" & IP & "' where Qid='6' and EmpId='" & EmpId & "' ")
        clas.ExecuteNonQuery(Update_str.ToString())
    End Sub

    Protected Sub Update_Ques_Ans_JobSheet_Q7(ByVal Ans As String, ByVal IP As String, ByVal EmpId As String)
        Dim Update_str As StringBuilder = New StringBuilder
        Update_str.Append(" Update Ams.Ams_Emp_Job_Ans Set Ans_Desc='" & Ans & "',KeyedOn=sysdate,IP='" & IP & "' where Qid='7' and EmpId='" & EmpId & "' ")
        clas.ExecuteNonQuery(Update_str.ToString())
    End Sub

    Protected Sub Update_Ques_Ans_JobSheet_Q8(ByVal Ans As String, ByVal IP As String, ByVal EmpId As String)
        Dim Update_str As StringBuilder = New StringBuilder
        Update_str.Append(" Update Ams.Ams_Emp_Job_Ans Set Ans_Desc='" & Ans & "',KeyedOn=sysdate,IP='" & IP & "' where Qid='8' and EmpId='" & EmpId & "' ")
        clas.ExecuteNonQuery(Update_str.ToString())
    End Sub


    'Public Sub InsertCommand2(ByVal empid As Int32)
    '    Dim transid As Int32 = getMaxempresID()
    '     Dim str As StringBuilder = New StringBuilder
    '    str.Append("insert into ams.ams_EMPLOYERRESULT(transid,EMPID,")



    '    If DropDownList4.SelectedValue <> "" Then
    '        str.Append("resultid,")
    '    End If
    '    Dim temp As String = str.ToString
    '    str.Clear()
    '    str.Append(temp.Substring(0, temp.Length - 1))
    '    str.Append(",  KEYEDON, KEYEDBY,ip) values(" & transid & "," & empid & ",")


    '    If DropDownList4.SelectedValue <> "" Then
    '        str.Append("'" & DropDownList4.SelectedValue & "',")
    '    End If


    '    'HttpContext.Current.Request.UserHostAddress
    '    str.Append("sysdate," & empno & ",'" & HttpContext.Current.Request.UserHostAddress & "',")


    '    temp = str.ToString
    '    str.Clear()
    '    str.Append(temp.Substring(0, temp.Length - 1))
    '    str.Append(")")

    '    Dim con As New OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings("constr").ToString)
    '    Dim cmd As New OracleCommand(str.ToString, con)
    '    If con.State = ConnectionState.Closed Then
    '        con.Open()
    '    End If

    '    cmd.ExecuteNonQuery()
    '    con.Close()
    '    clear()


    'End Sub

    'Public Shared Function upDateCommand() As String
    '    ' Dim transid As Int32 = getMaxID()
    '    Dim f As Boolean = False
    '    Dim str As StringBuilder = New StringBuilder
    '    str.Append("update rahul.BRITISHCOLDATANEW set ")
    '    'If usr.InDocDt IsNot Nothing Then
    '    '    str.Append("InDocDt='" & [String].Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(usr.InDocDt)) & "',")
    '    'End If
    '    'If usr.InDocRecdBy IsNot Nothing Then
    '    '    str.Append("InDocRecdBy='" & usr.InDocRecdBy & "',")
    '    'End If

    '    If usr.TRVDOCRECDDT IsNot Nothing AndAlso f = False Then
    '        str.Append("TRVDOCRECDDT='" & Format("dd-MON-yyyy", usr.TRVDOCRECDDT) & "',")
    '    End If
    '    If usr.TRVDOCRECDBY IsNot Nothing AndAlso f = False Then
    '        str.Append("TRVDOCRECDBY='" & usr.TRVDOCRECDBY & "',")
    '    End If
    '    If usr.TRVDOCSTATUSID IsNot Nothing AndAlso f = False Then
    '        str.Append("TRVDOCSTATUSID='" & usr.TRVDOCSTATUSID & "',")
    '        'If usr.TRVDOCSTATUSID <> 1 Then
    '        '    f = True
    '        'End If
    '    End If
    '    If usr.EXPDOCSCHDDT IsNot Nothing AndAlso f = False Then
    '        str.Append("EXPDOCSCHDDT='" & Format("dd-MON-yyyy", usr.EXPDOCSCHDDT) & "',")
    '    End If
    '    If usr.EXPDOCRSTATUSID IsNot Nothing AndAlso f = False Then
    '        str.Append("EXPDOCRSTATUSID='" & usr.EXPDOCRSTATUSID & "',")
    '    End If
    '    If usr.EOIDOCRECDDT IsNot Nothing AndAlso f = False Then
    '        str.Append("EOIDOCRECDDT='" & Format("dd-MON-yyyy", usr.EOIDOCRECDDT) & "',")
    '    End If
    '    If usr.EOIDOCRECDBY IsNot Nothing AndAlso f = False Then
    '        str.Append("EOIDOCRECDBY='" & usr.EOIDOCRECDBY & "',")
    '    End If

    '    If usr.EOIDOCSTATUSID IsNot Nothing AndAlso f = False Then
    '        str.Append("EOIDOCSTATUSID='" & usr.EOIDOCSTATUSID & "',")
    '        '    If usr.EOIDOCSTATUSID <> 1 Then
    '        '        f = True
    '        '    End If
    '    End If
    '    If usr.EOIDOCFILEDON IsNot Nothing AndAlso f = False Then
    '        str.Append("EOIDOCFILEDON='" & Format("dd-MON-yyyy", usr.EOIDOCFILEDON) & "',")
    '    End If
    '    If usr.EOIDOCFILEDBY IsNot Nothing AndAlso f = False Then
    '        str.Append("EOIDOCFILEDBY='" & usr.EOIDOCFILEDBY & "',")
    '    End If
    '    If usr.EOIDOCRSTATUSID IsNot Nothing AndAlso f = False Then
    '        str.Append("EOIDOCRSTATUSID='" & usr.EOIDOCRSTATUSID & "',")

    '    End If

    '    If usr.ITASTATUSID IsNot Nothing AndAlso f = False Then
    '        str.Append("ITASTATUSID='" & usr.ITASTATUSID & "',")

    '    End If
    '    If usr.ITALETTERDT IsNot Nothing AndAlso f = False Then
    '        str.Append("ITALETTERDT='" & Format("dd-MON-yyyy", usr.ITALETTERDT) & "',")
    '    End If

    '    If usr.ITARECDDT IsNot Nothing AndAlso f = False Then
    '        str.Append("ITARECDDT='" & Format("dd-MON-yyyy", usr.ITARECDDT) & "',")
    '    End If
    '    If usr.ITAREASONID IsNot Nothing AndAlso f = False Then
    '        str.Append("ITAREASONID='" & usr.ITAREASONID & "',")

    '    End If
    '    If usr.ITANUMBER IsNot Nothing AndAlso f = False Then
    '        str.Append("ITANUMBER='" & usr.ITANUMBER & "',")

    '    End If
    '    If usr.CFNOC IsNot Nothing AndAlso f = False Then
    '        str.Append("CFNOC='" & usr.CFNOC & "',")

    '    End If
    '    If usr.ITACLIENTINTON IsNot Nothing AndAlso f = False Then
    '        str.Append("ITACLIENTINTON='" & Format("dd-MON-yyyy", usr.ITACLIENTINTON) & "',")
    '    End If
    '    If usr.TPDOCRECDON IsNot Nothing AndAlso f = False Then
    '        str.Append("TPDOCRECDON='" & Format("dd-MON-yyyy", usr.TPDOCRECDON) & "',")
    '    End If
    '    If usr.TPDOCRECDBY IsNot Nothing AndAlso f = False Then
    '        str.Append("TPDOCRECDBY='" & usr.TPDOCRECDBY & "',")
    '    End If
    '    If usr.TPDOCSTATUSID IsNot Nothing AndAlso f = False Then
    '        str.Append("TPDOCSTATUSID='" & usr.TPDOCSTATUSID & "',")
    '        'If usr.TPDOCSTATUSID <> 1 Then
    '        '    f = True
    '        'End If
    '    End If
    '    If usr.TPSANTDT IsNot Nothing AndAlso f = False Then
    '        str.Append("TPSANTDT='" & Format("dd-MON-yyyy", usr.TPSANTDT) & "',")
    '    End If
    '    If usr.TPREPRES IsNot Nothing AndAlso f = False Then
    '        str.Append("TPREPRES='" & usr.TPREPRES & "',")

    '    End If
    '    If usr.TPLETTERDT IsNot Nothing AndAlso f = False Then
    '        str.Append("TPLETTERDT='" & Format("dd-MON-yyyy", usr.TPLETTERDT) & "',")
    '    End If
    '    If usr.CFDOCRECDON IsNot Nothing AndAlso f = False Then
    '        str.Append("CFDOCRECDON='" & Format("dd-MON-yyyy", usr.CFDOCRECDON) & "',")
    '    End If
    '    If usr.CFDOCRECDBY IsNot Nothing AndAlso f = False Then
    '        str.Append("CFDOCRECDBY='" & usr.CFDOCRECDBY & "',")
    '    End If
    '    If usr.CFFILEDON IsNot Nothing AndAlso f = False Then
    '        str.Append("CFFILEDON='" & Format("dd-MON-yyyy", usr.CFFILEDON) & "',")
    '    End If
    '    If usr.CFDOCSTATUSID IsNot Nothing AndAlso f = False Then
    '        str.Append("CFDOCSTATUSID='" & usr.CFDOCSTATUSID & "',")
    '        'If usr.CFDOCSTATUSID <> 1 Then
    '        '    f = True
    '        'End If
    '    End If
    '    If usr.CFFILEDBY IsNot Nothing AndAlso f = False Then
    '        str.Append("CFFILEDBY='" & usr.CFFILEDBY & "',")
    '    End If
    '    If usr.CFRSTATUS IsNot Nothing AndAlso f = False Then
    '        str.Append("CFRSTATUS='" & usr.CFRSTATUS & "',")

    '    End If

    '    If usr.INTRECDON IsNot Nothing AndAlso f = False Then
    '        str.Append("INTRECDON='" & Format("dd-MON-yyyy", usr.INTRECDON) & "',")
    '    End If
    '    If usr.INTRECDBY IsNot Nothing AndAlso f = False Then
    '        str.Append("INTRECDBY='" & usr.INTRECDBY & "',")
    '    End If
    '    If usr.INTLETTERDT IsNot Nothing AndAlso f = False Then
    '        str.Append("INTLETTERDT='" & Format("dd-MON-yyyy", usr.INTLETTERDT) & "',")
    '    End If
    '    If usr.INTSCHDON IsNot Nothing AndAlso f = False Then
    '        str.Append("INTSCHDON='" & Format("dd-MON-yyyy", usr.INTSCHDON) & "',")
    '    End If
    '    If usr.LSRECDON IsNot Nothing AndAlso f = False Then
    '        str.Append("LSRECDON='" & Format("dd-MON-yyyy", usr.LSRECDON) & "',")
    '    End If
    '    If usr.LSSCHDON IsNot Nothing AndAlso f = False Then
    '        str.Append("LSSCHDON='" & Format("dd-MON-yyyy", usr.LSSCHDON) & "',")
    '    End If
    '    If usr.INTRESULT IsNot Nothing AndAlso f = False Then
    '        str.Append("INTRESULT='" & usr.INTRESULT & "',")
    '    End If
    '    If usr.PAGRSINGON IsNot Nothing AndAlso f = False Then
    '        str.Append("PAGRSINGON='" & Format("dd-MON-yyyy", usr.PAGRSINGON) & "',")
    '    End If
    '    If usr.PAGRSTATUS IsNot Nothing AndAlso f = False Then
    '        str.Append("PAGRSTATUS='" & usr.PAGRSTATUS & "',")
    '    End If
    '    If usr.WPDOCRECDON IsNot Nothing AndAlso f = False Then
    '        str.Append("WPDOCRECDON='" & Format("dd-MON-yyyy", usr.WPDOCRECDON) & "',")
    '    End If
    '    If usr.WPDOCRECDBY IsNot Nothing AndAlso f = False Then
    '        str.Append("WPDOCRECDBY='" & usr.WPDOCRECDBY & "',")
    '    End If

    '    If usr.WPDOCSTATUS IsNot Nothing AndAlso f = False Then
    '        str.Append("WPDOCSTATUS='" & usr.WPDOCSTATUS & "',")
    '    End If
    '    If usr.WPDOCRESULT IsNot Nothing AndAlso f = False Then
    '        str.Append("WPDOCRESULT='" & usr.WPDOCRESULT & "',")
    '    End If


    '    If usr.TRVFILEDON IsNot Nothing AndAlso f = False Then
    '        str.Append("TRVFILEDON='" & Format("dd-MON-yyyy", usr.TRVFILEDON) & "',")
    '    End If
    '    If usr.TRVFILEDBY IsNot Nothing AndAlso f = False Then
    '        str.Append("TRVFILEDBY='" & usr.TRVFILEDBY & "',")
    '    End If
    '    If usr.TRVFILSTATUS IsNot Nothing AndAlso f = False Then
    '        str.Append("TRVFILSTATUS='" & usr.TRVFILSTATUS & "',")

    '    End If
    '    'If usr.IntPlace IsNot Nothing Then
    '    '    str.Append("IntPlace='" & usr.IntPlace & "',")
    '    'End If












    '    Dim temp As String = str.ToString
    '    str.Clear()
    '    str.Append(temp.Substring(0, temp.Length - 1))
    '    str.Append(",LASTUPDATE=sysdate,LASTUPDATEBY=" & HttpContext.Current.Session("Sp_empid") & " where FSN='" & usr.FSN & "' ")

    '    Dim con As New OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings("constr").ToString)
    '    Dim cmd As New OracleCommand(str.ToString, con)
    '    con.Open()
    '    cmd.ExecuteNonQuery()
    '    con.Close()
    'End Function
    'Public Shared Function getMaxID() As Int32
    '    Dim con As New OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings("constr").ToString)
    '    Dim str As String = "select nvl(max(empid),0)+1 from ams.ams_EMPLOYER"
    '    Dim mytable As New DataTable
    '    Dim da As New OracleDataAdapter(str, con)
    '    da.Fill(mytable)
    '    If mytable IsNot Nothing AndAlso mytable.Rows.Count > 0 Then
    '        Return mytable.Rows(0)(0)
    '    End If
    'End Function
    'Public Shared Function getMaxempresID() As Int32
    '    Dim con As New OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings("constr").ToString)
    '    Dim str As String = "select nvl(max(transid),0)+1 from ams.ams_EMPLOYERRESULT"
    '    Dim mytable As New DataTable
    '    Dim da As New OracleDataAdapter(str, con)
    '    da.Fill(mytable)
    '    If mytable IsNot Nothing AndAlso mytable.Rows.Count > 0 Then
    '        Return mytable.Rows(0)(0)
    '    End If
    'End Function

    Private Sub clear()
        TextBox1.Text = String.Empty
        TextBox2.Text = String.Empty
        TextBox3.Text = String.Empty
        TextBox4.Text = String.Empty
        TextBox5.Text = String.Empty
        TextBox6.Text = String.Empty
        TextBox7.Text = String.Empty
        TextBox8.Text = String.Empty
        TextBox9.Text = String.Empty
        TextBox10.Text = String.Empty
        TextBox11.Text = String.Empty
        DropDownList1.ClearSelection()
        DropDownList1.DataBind()
        DropDownList2.ClearSelection()
        DropDownList2.DataBind()
        DropDownList4.ClearSelection()
        DropDownList4.DataBind()
        dtSubmission.SelectedDate = Date.Today
        label2.Text = Session("lgnagntnam")
        Button1.Text = "Submit"
        dtSubmission.Enabled = True
        DropDownList4.Enabled = True
        Session.Remove("mode")
    End Sub

    'Public Shared Function checkData(ByVal FSN As String) As Boolean
    '    Dim con As New OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings("constr").ToString)
    '    Dim str As String = "select count(*) from rahul.BRITISHCOLDATANEW where FSN=" & fsn & " "
    '    Dim mytable As New DataTable
    '    Dim da As New OracleDataAdapter(str, con)
    '    da.Fill(mytable)
    '    If mytable IsNot Nothing AndAlso mytable.Rows.Count > 0 Then
    '        If mytable.Rows(0)(0) = 0 Then
    '            Return True
    '        Else
    '            Return False
    '        End If
    '    End If
    'End Function
    Function UppercaseFirstLetter(ByVal val As String) As String
        ' Test for nothing or empty.
        If String.IsNullOrEmpty(val) Then
            Return val
        End If

        ' Convert to character array.
        Dim array() As Char = val.ToCharArray

        ' Uppercase first character.
        array(0) = Char.ToUpper(array(0))

        ' Return new string.
        Return New String(array)
    End Function

    '  Protected Sub imgbtn_AddDesignation_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_AddDesignation.Click
    '  Label1.Text = "Designation"
    '   Ifrm1.Attributes.Add("src", "SetDisposition.aspx?Disposition=" & "Designation")
    '  ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)
    '   getmastdesg()
    '   End Sub

    'Protected Sub imgbtn_AddIndustry_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_AddIndustry.Click
    '    ' Label1.Text = "Industry"
    '    '   Ifrm1.Attributes.Add("src", "SetDisposition.aspx?Disposition=" & "Industry")
    '    '  ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)
    '    ' getmastindustry()
    'End Sub
    Protected Sub Btnref_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Btnref.Click, Btnref2.Click
        getmastdesg()
        getmastindustry()
        If Request.QueryString("mode") = "edit" Then
            getrcdforedit()
        End If
    End Sub

    Protected Sub TextBox11_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox11.TextChanged
        If TextBox11.Text <> "" Then
            If TextBox11.Text <> "" Then
                TextBox11.Text = TextBox11.Text.Replace("'", "")
            End If
            Dim Query_Str As String
            If Button1.Text = "Update" Then
                Query_Str = "Select * from AMS.Act_EMPLOYER_DATA where  lower(EMPEML)=lower('" & TextBox11.Text & "') and EmpId<>'" & Request.QueryString("empid") & "'"
            Else
                Query_Str = "Select * from AMS.Act_EMPLOYER_DATA Where lower(EMPEML)=lower('" & TextBox11.Text.Trim & "')"
            End If

            Dim dt_chk As DataTable = clas.getdata(Query_Str, "QR")
            If dt_chk.Rows.Count > 0 Then
                EmlMsg.Visible = True
                EmlMsg.InnerText = "This Email-Id is already registered by BDC: " & dt_chk.Rows(0)("AgntName") & vbCrLf & ". Please Enter some other Email-Id."
                'RadWindowManager1.RadAlert("<ul><li><div style=color:green;> This Email-Id is already registered by BDC: " & dt_chk.Rows(0)("AgntName") & vbCrLf & ". Please Enter some other Email-Id.</div></li></ul>", 300, 150, "Validation Success", Nothing)
                'TextBox11.Text = String.Empty
                a1.Attributes.Add("style", "display:inline")
                Ifrmfollowup.Attributes.Add("src", "followup_list.aspx?empid=" & dt_chk.Rows(0)("EmpId") & "&mod=D")

            Else
                EmlMsg.Visible = False
                a1.Attributes.Add("style", "display:none")
                Ifrmfollowup.Attributes.Remove("src")

            End If
        Else
            EmlMsg.Visible = False
        End If



    End Sub

    Protected Sub TextBox9_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox9.TextChanged
        If TextBox9.Text <> "" Then
            If TextBox9.Text <> "" Then
                TextBox9.Text = TextBox9.Text.Replace("'", "")
            End If
            Dim Query_Str As String
            If Button1.Text = "Update" Then
                Query_Str = "Select E.EmpId,E.AgentId, E.EMPEML,Ag.AgntName from AMS.AMS_Employer E,ams.AMS_Agents Ag  where Ag.AgntId=E.AgentId and   lower(EMPCMPWEB)=lower('" & TextBox9.Text & "')  and E.EmpId<>'" & Request.QueryString("empid") & "'"
            Else
                Query_Str = "Select E.EmpId,E.AgentId, E.EMPEML,Ag.AgntName from AMS.AMS_Employer E,ams.AMS_Agents Ag  where Ag.AgntId=E.AgentId and   lower(EMPCMPWEB)=lower('" & TextBox9.Text & "')"
            End If

            Dim dt_chk As DataTable = clas.getdata(Query_Str, "QR")
            If dt_chk.Rows.Count > 0 Then
                WebMsg.Visible = True
                WebMsg.InnerText = "This Website is already registered by BDC: " & dt_chk.Rows(0)("AgntName") & vbCrLf & ". Please Enter some other Website."
                'RadWindowManager1.RadAlert("<ul><li><div style=color:green;> This Email-Id is already registered by BDC: " & dt_chk.Rows(0)("AgntName") & vbCrLf & ". Please Enter some other Email-Id.</div></li></ul>", 300, 150, "Validation Success", Nothing)
                'TextBox9.Text = String.Empty
            Else
                WebMsg.Visible = False
            End If
        Else
            WebMsg.Visible = False
        End If


    End Sub

    Protected Sub TextBox6_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox6.TextChanged
        If TextBox6.Text <> "" Then
            If TextBox6.Text <> "" Then
                TextBox6.Text = TextBox6.Text.Replace("'", "''")
            End If
            Dim Query_Str As String
            If Button1.Text = "Update" Then
                Query_Str = "Select E.EmpId,E.AgentId, E.EMPEML,Ag.AgntName,E.EmpFname || ' ' || E.EmpMName || ' ' || E.EmpLName as Name from AMS.AMS_Employer E,ams.AMS_Agents Ag  where Ag.AgntId=E.AgentId and   lower(E.EMPCMPNAM)=lower('" & TextBox6.Text & "') and E.EmpId<>'" & Request.QueryString("empid") & "' and lower(E.EmpFname)=lower('" & TextBox1.Text.Trim & "') and lower(E.EmpMName)=lower('" & TextBox2.Text.Trim & "') and lower(E.EmpLName)=lower('" & TextBox3.Text.Trim & "')"
            Else
                Query_Str = "Select E.EmpId,E.AgentId, E.EMPEML,Ag.AgntName,E.EmpFname || ' ' || E.EmpMName || ' ' || E.EmpLName as Name from AMS.AMS_Employer E,ams.AMS_Agents Ag  where Ag.AgntId=E.AgentId and   lower(E.EMPCMPNAM)=lower('" & TextBox6.Text & "') and lower(E.EmpFname)=lower('" & TextBox1.Text.Trim & "') and lower(E.EmpMName)=lower('" & TextBox2.Text.Trim & "') and lower(E.EmpLName)=lower('" & TextBox3.Text.Trim & "')"
            End If

            Dim dt_chk As DataTable = clas.getdata(Query_Str, "QR")
            If dt_chk.Rows.Count > 0 Then
                CmpMsg.Visible = True
                CmpMsg.InnerText = "This Company Name is already registered by BDC: " & dt_chk.Rows(0)("AgntName") & vbCrLf & " For Employer: " & dt_chk.Rows(0)("Name") & ". Please Enter some other Company Name."
                'RadWindowManager1.RadAlert("<ul><li><div style=color:green;> This Email-Id is already registered by BDC: " & dt_chk.Rows(0)("AgntName") & vbCrLf & ". Please Enter some other Email-Id.</div></li></ul>", 300, 150, "Validation Success", Nothing)
                'TextBox6.Text = String.Empty
            Else
                CmpMsg.Visible = False
            End If
        Else
            CmpMsg.Visible = False
        End If

    End Sub

    Protected Sub RadGrid1_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadGrid1.PreRender
        If Page.IsPostBack = False Then
            'Dim column As GridColumn = TryCast(RadGrid1.MasterTableView.GetColumnSafe("eid"), GridColumn)
            'column.OrderIndex = RadGrid1.MasterTableView.RenderColumns.Length - 5


            'Dim column1 As GridColumn = TryCast(RadGrid1.MasterTableView.GetColumnSafe("Screening"), GridColumn)
            'column1.OrderIndex = RadGrid1.MasterTableView.RenderColumns.Length

            'RadGrid1.MasterTableView.Rebind()
        End If
    End Sub
    Private Sub RadGrid1_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles RadGrid1.PageIndexChanged
        RadGrid1.CurrentPageIndex = e.NewPageIndex
        getmasters1()
    End Sub
    Protected Sub RadGrid1_SortCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridSortCommandEventArgs) Handles RadGrid1.SortCommand
        getmasters1()
    End Sub
    Protected Sub RadGrid1_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid1.ItemDataBound
        RadGrid1.MasterTableView.GetColumn("SCRNRMKSBY").Visible = False
        RadGrid1.MasterTableView.GetColumn("RATING1").Visible = False
        RadGrid1.MasterTableView.GetColumn("scr_date").HeaderText = "DATE"
        RadGrid1.MasterTableView.GetColumn("Screening_By").ItemStyle.Width = 80
        RadGrid1.MasterTableView.GetColumn("Screening_By").HeaderStyle.Width = 80
        RadGrid1.MasterTableView.GetColumn("Screening_By").HeaderText = "SCREENING BY"

        RadGrid1.MasterTableView.GetColumn("REMARKS").ItemStyle.Width = 180
        RadGrid1.MasterTableView.GetColumn("REMARKS").HeaderStyle.Width = 180
    End Sub
    Protected Sub RadGrid1_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.ItemCommand
        If e.CommandName = "Filter" Then
            getmasters1()
        ElseIf e.CommandName = "ChangePageSize" Then
            getmasters1()
        End If
    End Sub

    Private Sub FILLCTR()

        Dim qry As String
        Dim _dt As DataTable
        Dim ctrlType As String
        Dim ctrpanel As String
        Dim chk1 = 0
        Dim kid = 0
        'Panel_ques.Visible = True
        Try
            If Request.QueryString("mode") Is Nothing Then
                '     qry = "select * from AMS.CONTROLS_QUESTION where module_id= '" & Request.QueryString("Modid") & "' order by qid "
                qry = "select * from AMS.CONTROLS_QUESTION where module_id=2 and ques_Stat='A' order by qid  "
            Else
                qry = "select a.*,b.crt_value as ans from ( select * from  AMS.CONTROLS_QUESTION where module_id=2 and ques_Stat='A' ) a left outer join ams.CONTROLS_QUESTION_result b on a.qid=b.qid and  b.empid='" & Request.QueryString("empid") & "'  order by a.qid"
                chk1 = 1
            End If
            _dt = clas.getdata(qry, "QR")
            Dim j = 1
            ' Dim k As Int32 = 1
            If _dt.Rows.Count > 0 Then
                Dim i As Integer = 1

                For Each row As DataRow In _dt.Rows
                    ctrlType = row("Control")
                    ctrpanel = row("panelid")


                    Dim panelRow As New Panel
                    Dim panelAns2 As New Panel
                    If (ctrpanel = "1") Then
                        ' panelAns = panel_s1a
                        'step1.Visible = True
                        ' panelQue = Panel_S1

                    End If


                    Dim lblque As New Label()

                    lblque.Text = row("CRT_QUES")    
                    Dim pnlque As New Panel
                    pnlque.CssClass = "divTableCell"
                    pnlque.Controls.Add(lblque)
                    ' panel_s1a.Controls.Add(pnlque)
                    panelRow.CssClass = "divTableRow"
                    panelRow.Attributes.Add("Runat", "server")
                    panelRow.Controls.Add(pnlque)


                    'Add Answer with control in Panel
                    Dim _dt2 As DataTable

                    If row("CRT_Values").ToString.Length > 0 Then
                        _dt2 = clas.getdata(row("CRT_Values").ToString, "QR")
                    Else
                        _dt2 = clas.getdata("select ctr_id,ctr_values from AMS.CONTROL_ANSWER where qid=" & row("qid"), "QR")
                    End If

                    ' TXTAREA  TXT DDL RDB CHK controls
                    Dim pnlCtr As New Panel
                    pnlCtr.CssClass = "span6"
                    pnlCtr.Attributes.Add("Runat", "server")
                    If ctrlType.ToUpper = "TXT" Then

                        Dim txt As New TextBox()
                        txt.ID = "txt" & i
                        txt.Attributes.Add("data_id", row("qid"))
                        txt.Attributes.Add("class", "que_txtbox")
                        txt.Attributes.Add("runat", "server")
                        'txt.Attributes.Add("id", "txt-" & kid)
                        txt.MaxLength = 50
                        If chk1 = 1 Then
                            If Not IsDBNull(row("ans")) Then
                                txt.Text = row("ans")
                            End If
                        End If
                        'pnl1.Controls.Add(txt)
                        pnlCtr.Controls.Add(txt)
                        ' panelAns.Controls.Add(pnl)
                        'pnl1.Controls.Add(pnl1)
                        'Page.Controls.Add(tx);

                        Page.Form.Controls.Add(txt)
                        pnlCtr.Controls.Add(txt)
                        pnlCtr.CssClass = "divTableCell"
                        panelRow.Controls.Add(pnlCtr)
                    ElseIf ctrlType.ToUpper = "DDL" Then
                        Dim ddl As New DropDownList

                        ddl.ID = "ddl" & i
                        ddl.Attributes.Add("data_id", row("qid"))
                        ddl.Attributes.Add("class", "que_drop")
                        ddl.Attributes.Add("runat", "server")
                        '  ddl.Attributes.Add("id", "txt-" & kid)
                        ' ddl.AutoPostBack = True
                        ' Page.Form.Controls.Add(pnl1)

                        'Ading values
                        ' Dim parts As String() = row("CRT_Values").Split(";")
                        ' Dim part As String       


                        If _dt2 Is Nothing Then Continue For
                       
                            ddl.Items.Add("--Choose a Value--")
                            For Each rw As DataRow In _dt2.Rows
                                'ddl.Items.Add(rw("ctr_values"))
                            ddl.Items.Add(New ListItem(rw(1), rw(0)))
                            Next


                        If chk1 = 1 Then
                            If Not IsDBNull(row("ans")) Then
                                'ddl.SelectedItem.Value = row("ans")
                                ddl.Items.FindByValue(row("ans")).Selected = True
                            End If
                        End If
                    
                        Page.Form.Controls.Add(ddl)
                        pnlCtr.Controls.Add(ddl)
                        pnlCtr.CssClass = "divTableCell"
                        panelRow.Controls.Add(pnlCtr)
                    ElseIf ctrlType.ToUpper = "TXTAREA" Then
                        Dim txtarea As New TextBox()

                        txtarea.TextMode = TextBoxMode.MultiLine
                        txtarea.Attributes.Add("data_id", row("qid"))
                        txtarea.ID = "txtA" & i
                        txtarea.MaxLength = 1000
    ' pnl1.Controls.Add(txtarea)

    ' pnlCtr.Controls.Add(txtarea)
                        pnlCtr.CssClass = "divTableCell"
                        If chk1 = 1 Then
                            If Not IsDBNull(row("ans")) Then
    'ddl.SelectedItem.Value = row("ans")
                                txtarea.Text = row("ans")
                            End If
                        End If
                        Page.Form.Controls.Add(txtarea)
                        panelRow.Controls.Add(txtarea)
                    ElseIf ctrlType.ToUpper = "RDB" Then
                        If _dt2 Is Nothing Then Continue For
                        For Each rw As DataRow In _dt2.Rows
                            Dim rdb As New RadioButton
                            rdb.Attributes.Add("data_id", row("qid"))
                            rdb.ID = "rdb" & j
                            rdb.GroupName = "Group" & i

    ' pnl1.Controls.Add(txtarea)
                            pnlCtr.CssClass = "divTableCell"
    'rdb.Text = rw("ctr_values")
                            rdb.Text = rw(1)
    ' pnlCtr.Controls.Add(rdb)
                            pnlCtr.Controls.Add(rdb)
                            Page.Form.Controls.Add(rdb)
                            panelRow.Controls.Add(pnlCtr)
                            j += 1
                        Next
                    ElseIf ctrlType.ToUpper = "CHK" Then
                        If _dt2 Is Nothing Then Continue For
                        For Each rw As DataRow In _dt2.Rows

                            Dim chk As New CheckBox
                            chk.Attributes.Add("data_id", row("qid"))
                            chk.ID = "chk" & j

                            chk.Text = rw(1)
    'pnlCtr.Controls.Add(chk)
                            pnlCtr.CssClass = "divTableCell"
                            pnlCtr.Controls.Add(chk)
                            Page.Form.Controls.Add(chk)
                            panelRow.Controls.Add(pnlCtr)
                            j += 1
                        Next
                    End If
                    i += 1
                    ' kid += 1
                    ' panelAns2.Controls.Add(panelAns)
                    pnlTable.Controls.Add(panelRow)
                Next
                ' Page.Controls.Add(pnlTable)
            End If

        Catch ex As Exception
            RadWindowManager1.RadAlert(ex.Message, 300, 200, "Exception", "")
        End Try
    End Sub

    Protected Sub saveQue(empid2 As String)
        'SAVE


        Dim updstr As String = ""
        Dim i = 0
        Dim cntrl As Control = TryCast(Me.Page.Form, Control)
        Try
            Dim testid = "A-" & clas.getMaxID("Select AMS.TESTID_SEQ.nextval from dual")

            For Each c1 As Control In pnlTable.Controls

                For Each c2 As Control In c1.Controls

                    For Each c As Control In c2.Controls
                        If TypeOf c Is TextBox Or TypeOf c Is DropDownList Or TypeOf c Is RadComboBox Or TypeOf c Is RadioButton Or TypeOf c Is RadDatePicker Or TypeOf c Is CheckBox Then
                            Dim inststr1 As String = ""
                            If TypeOf c Is TextBox Then

                                Dim tb As TextBox = TryCast(c, TextBox)

                                If tb.Text.Trim().Length > 0 Then
                                    inststr1 &= "," & tb.Attributes("data_id") & ",'" & tb.Text.Trim() & "'"
                                Else
                                    inststr1 &= "," & tb.Attributes("data_id") & ",'" & DBNull.Value & "'"
                                End If

                            ElseIf TypeOf c Is RadDatePicker Then
                                Dim tb As RadDatePicker = TryCast(c, RadDatePicker)
                                If Not tb.SelectedDate Is Nothing Then
                                    If (tb.SelectedDate.ToString.Length >= 1) Then
                                        inststr1 &= "," & tb.Attributes("data_id") & ",'" & CDate(tb.SelectedDate).ToString("dd-MMM-yyyy") & "'"
                                    End If
                                Else
                                    inststr1 &= "," & tb.Attributes("data_id") & ",'" & DBNull.Value & "'"
                                End If
                            ElseIf TypeOf c Is DropDownList Then
                                Dim tb As DropDownList = TryCast(c, DropDownList)
                                If Not tb.SelectedValue Is Nothing Then

                                    If (tb.SelectedIndex >= 1) Then
                                        inststr1 &= "," & tb.Attributes("data_id") & ",'" & tb.SelectedItem.Value & "'"
                                    Else

                                        inststr1 &= "," & tb.Attributes("data_id") & ",'" & DBNull.Value & "'"

                                    End If
                                Else
                                    inststr1 &= "," & tb.Attributes("data_id") & ",'" & DBNull.Value & "'"
                                End If
                            ElseIf TypeOf c Is CheckBox Then
                                Dim tb As CheckBox = TryCast(c, CheckBox)
                                If tb.Checked = True Then
                                    inststr1 &= "," & tb.Attributes("data_id") & ",'" & tb.Text & "'"
                                Else
                                    inststr1 &= "," & tb.Attributes("data_id") & ",'" & DBNull.Value & "'"

                                End If

                            ElseIf TypeOf c Is RadioButton Then
                                Dim tb As RadioButton = TryCast(c, RadioButton)
                                If tb.Checked = True Then
                                    inststr1 &= "," & tb.Attributes("data_id") & ",'" & tb.Text & "'"
                                Else
                                    inststr1 &= "," & tb.Attributes("data_id") & ",'" & DBNull.Value & "'"
                                End If

                            End If

                            If (inststr1.Length > 0) Then

                                If Session("updQu") = 1 Then
                                    qry = "insert into AMS.controls_question_Result(id,qid,crt_value,keyedon,keyedby,keyedmip, module_id,agentid,empid) values ( AMS.controls_question_Result_Seq.nextval " & inststr1 & ",sysdate," & "'" & Session("lgnagntid") & "', '" & HttpContext.Current.Request.UserHostAddress & "',2 , " & Session("lgnagntid") & " ,'" & Request.QueryString("empid") & "' ) "
                                Else
                                    qry = "insert into AMS.controls_question_Result(id,qid,crt_value,keyedon,keyedby,keyedmip, module_id,agentid,empid) values (AMS.controls_question_Result_Seq.nextval " & inststr1 & ",sysdate," & "'" & Session("lgnagntid") & "', '" & HttpContext.Current.Request.UserHostAddress & "',2, " & Session("lgnagntid") & " ,'" & empid2 & "') "
                                End If

                                If qry.Length > 0 Then
                                    i = clas.ExecuteNonQuery(qry)
                                End If



                            End If

                        Else

                            'If TypeOf c Is Panel Then


                            '    Dim pnl As Panel = TryCast(c.FindControl("pnl"), Panel)

                            '    If Not pnl Is Nothing Then
                            '        cntrl = pnl
                            '        GoTo Line1

                            '    End If
                            'End If
                        End If
                    Next

                Next
            Next

            'Next



            If i = 1 Then
                'RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Questionary Submitted Successfully </div></li></ul>", 300, 150, "Validation Success", Nothing)
            Else

                'RadWindowManager1.RadAlert("<ul><li><div style=color:red;> Some Malfunction Occured, please Contact IT..</div></li></ul>", 300, 150, "Validation Success", Nothing)
            End If


        Catch ex As Exception
            RadWindowManager1.RadAlert(ex.Message, 300, 200, "Exception", "")
        End Try


    End Sub

    Protected Sub UpdateQue()
        'Update

        Dim _dt1 As New DataTable
        Session("updQu") = 0
        _dt1 = clas.getdata("select * from AMS.CONTROLS_QUESTION_RESULT where empid= '" & Request.QueryString("empid") & "'", "txt")
        If _dt1.Rows.Count <= 0 Then
            Session("updQu") = 1
            saveQue("0")
            Exit Sub
        End If

        Dim i = 0
        Dim cntrl As Control = TryCast(Me.Page.Form, Control)
        Try
            For Each c1 As Control In pnlTable.Controls

                For Each c2 As Control In c1.Controls

                    For Each c As Control In c2.Controls
                        If TypeOf c Is TextBox Or TypeOf c Is DropDownList Or TypeOf c Is RadComboBox Or TypeOf c Is RadioButton Or TypeOf c Is RadDatePicker Or TypeOf c Is CheckBox Then
                            Dim updstr As String = ""
                            If TypeOf c Is TextBox Then

                                Dim tb As TextBox = TryCast(c, TextBox)

                                If tb.Text.Trim().Length > 0 Then
                                    updstr = "crt_value='" & tb.Text.Trim() & "' where qid='" & tb.Attributes("data_id") & "'"
                                End If

                            ElseIf TypeOf c Is RadDatePicker Then
                                Dim tb As RadDatePicker = TryCast(c, RadDatePicker)
                                If Not tb.SelectedDate Is Nothing Then
                                    If (tb.SelectedDate.ToString.Length >= 1) Then
                                        updstr = "crt_value='" & CDate(tb.SelectedDate).ToString("dd-MMM-yyyy") & "' where qid='" & tb.Attributes("data_id") & "'"
                                    End If

                                End If
                            ElseIf TypeOf c Is DropDownList Then
                                Dim tb As DropDownList = TryCast(c, DropDownList)
                                If Not tb.SelectedValue Is Nothing Then

                                    If (tb.SelectedIndex >= 1) Then
                                        ' inststr1 &= "," & tb.Attributes("data_id") & ",'" & tb.SelectedValue & "'"
                                        updstr = "crt_value='" & tb.SelectedItem.Value & "' where qid='" & tb.Attributes("data_id") & "'"
                                    End If

                                End If
                            ElseIf TypeOf c Is CheckBox Then
                                Dim tb As CheckBox = TryCast(c, CheckBox)
                                If tb.Checked = True Then
                                    'inststr1 &= "," & tb.Attributes("data_id") & ",'" & tb.Text & "'"
                                    updstr = "crt_value='" & tb.Text & "' where qid='" & tb.Attributes("data_id") & "'"
                                End If

                            ElseIf TypeOf c Is RadioButton Then
                                Dim tb As RadioButton = TryCast(c, RadioButton)
                                If tb.Checked = True Then
                                    'inststr1 &= "," & tb.Attributes("data_id") & ",'" & tb.Text & "'"
                                    updstr = "crt_value='" & tb.Text & "' where qid='" & tb.Attributes("data_id") & "'"
                                End If

                            End If

                            If (updstr.Length > 0) Then
                                qry = "update AMS.controls_question_Result  set " & updstr & ""
                                If qry.Length > 0 Then
                                    i = clas.ExecuteNonQuery(qry)
                                End If
                            End If

                        Else

                            'If TypeOf c Is Panel Then


                            '    Dim pnl As Panel = TryCast(c.FindControl("pnl"), Panel)

                            '    If Not pnl Is Nothing Then
                            '        cntrl = pnl
                            '        GoTo Line1

                            '    End If
                            'End If
                        End If
                    Next

                Next
            Next

            'Next



            If i = 1 Then
                ' RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Questionary Submitted Successfully </div></li></ul>", 300, 150, "Validation Success", Nothing)
            Else

                'RadWindowManager1.RadAlert("<ul><li><div style=color:red;> Some Malfunction Occured, please Contact IT..</div></li></ul>", 300, 150, "Validation Success", Nothing)
            End If


        Catch ex As Exception
            RadWindowManager1.RadAlert(ex.Message, 300, 200, "Exception", "")
        End Try


    End Sub

    Protected Sub disableQue()
        Dim i = 0
        Dim cntrl As Control = TryCast(Me.Page.Form, Control)
        Try
            ' Dim testid = "A-" & clas.getMaxID("Select AMS.TESTID_SEQ.nextval from dual")
            'Dim mypanel() As String = {"panel_s1a"}

            'For Each p As String In mypanel

            '    Dim mpnl As Panel = TryCast(Page.Form.FindControl("Content1"), Panel)
            '    Dim np2 As Panel = mpnl.FindControl("panel_s1a")
            '    ' Dim cpnl As Panel = TryCast(mpnl.FindControl("panel_s1a"), Panel)



            For Each c1 As Control In pnlTable.Controls

                For Each c2 As Control In c1.Controls

                    For Each c As Control In c2.Controls
                        If TypeOf c Is TextBox Or TypeOf c Is DropDownList Or TypeOf c Is RadComboBox Or TypeOf c Is RadioButton Or TypeOf c Is RadDatePicker Or TypeOf c Is CheckBox Then
                            Dim updstr As String = ""
                            If TypeOf c Is TextBox Then

                                Dim tb As TextBox = TryCast(c, TextBox)

                                tb.Enabled = False

                            ElseIf TypeOf c Is RadDatePicker Then
                                Dim tb As RadDatePicker = TryCast(c, RadDatePicker)
                                tb.Enabled = False


                            ElseIf TypeOf c Is DropDownList Then
                                Dim tb As DropDownList = TryCast(c, DropDownList)
                                tb.Enabled = False


                            ElseIf TypeOf c Is CheckBox Then
                                Dim tb As CheckBox = TryCast(c, CheckBox)
                                tb.Enabled = False

                            ElseIf TypeOf c Is RadioButton Then
                                Dim tb As RadioButton = TryCast(c, RadioButton)
                                tb.Enabled = False

                            End If

                        End If
                    Next

                Next
            Next

            'Next



        Catch ex As Exception
            RadWindowManager1.RadAlert(ex.Message, 300, 200, "Exception", "")
        End Try

    End Sub

    Protected Sub chkrating()
     
        qry = "select To_char(SCRNRMKSDATE, 'DD-MON-YYYY') as Scr_Date,SCRNRATING as Rating1,SCRNRMKS as Remarks,SCRNRMKSBY from ams.AMS_SCREENING scr where scr.empid='" & Request.QueryString("empid") & "'"

        dt = clas.getdata(qry, "QR")


        If dt.Rows.Count > 0 Then
            disableQue()
        End If
    End Sub



    'Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
    '    Dim keys As List(Of String) = Request.Form.AllKeys.Where(Function(key) key.Contains("txtDynamic")).ToList()
    '    Dim i As Integer = 1
    '    For Each key As String In keys
    '        Me.CreateTextBox("txtDynamic" & i)
    '        i += 1
    '    Next
    'End Sub



End Class
