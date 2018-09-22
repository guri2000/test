Imports System.Data
Imports System.IO
Imports Telerik.Web.UI
Imports System.Text
Imports System.Data.OracleClient
Imports System.Data.OleDb

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
            getmasters()

            Bind_Employer()



            binddates(SRCGENDAT, clas.GetDate().ToString("dd-MMM-yyyy"), clas.GetDate().AddMonths(-1).ToString("dd-MMM-yyyy"), "", True)





            SRCOWN.SelectedValue = Session("lgnagntid")

            If Session("lgnroll") = "ADMIN" Then
                SRCOWN.Enabled = True
            Else
                SRCOWN.Enabled = False
            End If
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

        If Request.QueryString("mode") = "edit" And Session("mode") = "" Then
            '  getrcdforedit()
        End If




        'For Each c As Control In Me.Page.Form.Controls
        '    Response.Write(c.ID & "---" & c.GetType().ToString() & "")

        'Next

    End Sub
    Private Sub getmasters()
        dt = clas.getdata("select * from AMS.ams_statmaster where statstatus='A'", "TX")
        ViewState("qrydata") = dt
        bindfromdt(SRCCAT, "statscp=51", dt, "", "statdescr", "statid", "dropdown", True)


    End Sub


    Protected Sub bindfromdt(ByVal cntl As Control, ByVal filquery As String, ByVal sdt As DataTable, ByVal selval As String, ByVal disptxt As String, ByVal dispval As String, ByVal cntltyp As String, ByVal enbl As Boolean)
        Dim dv As DataView = New DataView()
        dv = sdt.DefaultView
        If filquery.Length > 0 And Not filquery.Trim = "" Then
            dv.RowFilter = filquery
        End If

        If cntltyp = "dropdown" Then
            Dim cnt As DropDownList = TryCast(cntl, DropDownList)
            cnt.AppendDataBoundItems = True
            '  Dim itm As ListItem = New ListItem("Select Value", "")
            '  cnt.Items.Add(itm)
            cnt.DataSource = dv
            cnt.DataTextField = disptxt
            cnt.DataValueField = dispval
            cnt.DataBind()
            If selval.Length > 0 And Not selval.Trim = "" Then
                cnt.Items.FindByValue(selval).Selected = True
            End If
            cnt.Enabled = enbl
            cnt.AppendDataBoundItems = False
        ElseIf cntltyp = "radcombo" Then
            Dim cnt As RadComboBox = TryCast(cntl, RadComboBox)
            cnt.DataSource = dv
            cnt.DataTextField = disptxt
            cnt.DataValueField = dispval
            cnt.DataBind()
            If selval.Length > 0 And Not selval.Trim = "" Then
                cnt.Items.FindItemByValue(selval).Selected = True
            End If
            cnt.Enabled = enbl
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

    Private Sub Bind_Employer()
        Try
            qry = "select AGNTID as  agnt_Id, AGNTNAME as agnt_Name from ams.ams_agents"
            If Session("lgnroll") = "S" Then

            Else
                qry &= " where  AGNTID='" & Session("lgnagntid") & "'"
            End If
            dt = clas.getdata(qry, "QR")

            '  Dim ddl_emp As DropDownList = DirectCast(RadGrid1.FindControl("ddl_Employer"), DropDownList)
            SRCOWN.DataTextField = "agnt_Name"
            SRCOWN.DataValueField = "agnt_Id"
            SRCOWN.DataSource = dt
            SRCOWN.DataBind()

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



    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click, ImageButton2.Click


        Dim inststr As String = ""
        Dim inststr1 As String = ""
        Dim updstr As String = ""
        Dim cntrl As Control = TryCast(Me.Page.Form, Control)

Line1:


        For Each c As Control In cntrl.Controls

            If TypeOf c Is TextBox Or TypeOf c Is DropDownList Or TypeOf c Is RadComboBox Or TypeOf c Is RadioButtonList Or TypeOf c Is RadDatePicker Then



                If TypeOf c Is TextBox Then

                    Dim tb As TextBox = TryCast(c, TextBox)

                    If tb.Text.Trim().Length > 0 Then
                        inststr &= tb.ID & ","
                        inststr1 &= "'" & tb.Text.Trim() & "',"
                        updstr &= tb.ID & "='" & tb.Text.Trim() & "', "
                    End If

                ElseIf TypeOf c Is RadDatePicker Then
                    Dim tb As RadDatePicker = TryCast(c, RadDatePicker)
                    If Not tb.SelectedDate Is Nothing Then
                        inststr &= tb.ID & ","
                        inststr1 &= "'" & CDate(tb.SelectedDate).ToString("dd-MMM-yyyy") & "',"
                        updstr &= tb.ID & "='" & CDate(tb.SelectedDate).ToString("dd-MMM-yyyy") & "', "
                    End If
                ElseIf TypeOf c Is DropDownList Then
                    Dim tb As DropDownList = TryCast(c, DropDownList)
                    If Not tb.SelectedValue Is Nothing Then
                        inststr &= tb.ID & ","
                        inststr1 &= "'" & tb.SelectedValue & "',"
                        updstr &= tb.ID & "='" & tb.SelectedValue & "', "
                    End If
                ElseIf TypeOf c Is RadioButtonList Then
                    Dim tb As RadioButtonList = TryCast(c, RadioButtonList)
                    If tb.SelectedValue <> "" Then
                        inststr &= tb.ID & ","
                        inststr1 &= "'" & tb.SelectedValue & "',"
                        updstr &= tb.ID & "='" & tb.SelectedValue & "', "
                    End If

                End If

            Else

            End If




        Next

        If Request.QueryString("mode") <> "edit" And Request.QueryString("empid") Is Nothing Then
            Dim Query_Str2 As String = "select * from AMS.dbcollect where lower(SRCEML1)='" & SRCEML1.Text.ToLower() & "'"
            Dim dt_chk2 As DataTable = clas.getdata(Query_Str2, "")
            If dt_chk2.Rows.Count = 0 Then

                qry = "insert into AMS.dbcollect(TRANSID," & inststr & " KEYEDON,KEYEDBY,IP,LASTUPDATEBY,LASTUPDATEON) values (AMS.DBCOLLECTS_SEQ.nextval," & inststr1 & " sysdate,' " & Session("lgnagntid") & "', '" & HttpContext.Current.Request.UserHostAddress & "','" & Session("lgnagntid") & "',sysdate) "

            Else
                Dim leadid = Convert.ToString(dt_chk2.Rows(0)("TRANSID"))
                qry = "update AMS.dbcollect set " & updstr & " lastupdateby='" & Session("lgnagntid") & "', lastupdateon=sysdate where transid='" & leadid & "'"



            End If

        Else


        End If




        If qry.Length > 0 Then


            Dim i = clas.ExecuteNonQuery(qry)
            If i = 1 Then
                clear()
                RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Transaction Done Successfully </div></li></ul>", 300, 150, "Validation Success", Nothing)
                Exit Sub
            Else
                Dim Err_Msg As String = clas.getexception().ToString.Replace(vbCrLf, "")
                Err_Msg = Err_Msg.Replace(vbCr, "")
                Err_Msg = Err_Msg.Replace(vbLf, "")
                RadWindowManager1.RadAlert("<ul><li><div style=color:red;>Transaction not Done due to " & Err_Msg & "</div></li></ul>", 300, 150, "Validation Unsuccessful", Nothing)
                ' RadWindowManager1.RadAlert("<ul><li><div style=color:red;> Transaction not Done </div></li></ul>", 300, 150, "Validation Success", Nothing)
            End If
            ' Response.Write(qry)

        End If




        'If TextBox10.Text <> "" Then
        '    TextBox10.Text = TextBox10.Text.Replace("'", "''")
        'End If

        'Dim flag As Boolean
        'If Button1.Text = "Update" Then

        '    remove_SingleCot()

        '    Dim Query_Str2 As String = "Select E.EmpId,E.AgentId, E.EMPEML,Ag.AgntName,E.EmpFname || ' ' || E.EmpMName || ' ' || E.EmpLName as Name from AMS.AMS_Employer E,ams.AMS_Agents Ag  where Ag.AgntId=E.AgentId and   lower(E.EMPCMPNAM)=lower('" & TextBox6.Text.Trim & "') and E.EmpId<>'" & Request.QueryString("empid") & "' and lower(E.EmpFname)=lower('" & TextBox1.Text.Trim & "') and lower(E.EmpMName)=lower('" & TextBox2.Text.Trim & "') and lower(E.EmpLName)=lower('" & TextBox3.Text.Trim & "')"
        '    Dim dt_chk2 As DataTable = clas.getdata(Query_Str2, "QR")
        '    If dt_chk2.Rows.Count > 0 Then
        '        CmpMsg.Visible = True
        '        CmpMsg.InnerText = "This Company Name is already registered by BDC: " & dt_chk2.Rows(0)("AgntName") & vbCrLf & " for Employer : " & dt_chk2.Rows(0)("Name") & ". Please Enter some other Company Name."
        '        Exit Sub
        '    End If

        '    Dim Query_Str As String = "Select E.EmpId,E.AgentId, E.EMPEML,Ag.AgntName from AMS.AMS_Employer E left outer join ams.AMS_Agents Ag  on  Ag.AgntId=E.AgentId Where   lower(EMPEML)=lower('" & TextBox11.Text.Trim & "') and E.EmpId<>'" & Request.QueryString("empid") & "'"
        '    Dim dt_chk As DataTable = clas.getdata(Query_Str, "QR")
        '    If dt_chk.Rows.Count > 0 Then
        '        EmlMsg.Visible = True
        '        EmlMsg.InnerText = "This Email-Id is already registered by BDC: " & dt_chk.Rows(0)("AgntName") & vbCrLf & ". Please Enter some other Email-Id."
        '        'RadWindowManager1.RadAlert("<ul><li><div style=color:green;> This Email-Id is already registered by BDC: " & dt_chk.Rows(0)("AgntName") & vbCrLf & ". Please Enter some other Email-Id.</div></li></ul>", 300, 150, "Validation Success", Nothing)
        '        Exit Sub
        '    End If

        '    flag = updateCommand()
        'Else

        '    remove_SingleCot()

        '    Dim Query_Str2 As String = "Select E.EmpId,E.AgentId, E.EMPEML,Ag.AgntName from AMS.AMS_Employer E,ams.AMS_Agents Ag  where Ag.AgntId=E.AgentId and   lower(E.EMPCMPNAM)=lower('" & TextBox6.Text.Trim & "') and lower(E.EmpFname)=lower('" & TextBox1.Text.Trim & "') and lower(E.EmpMName)=lower('" & TextBox2.Text.Trim & "') and lower(E.EmpLName)=lower('" & TextBox3.Text.Trim & "')"
        '    Dim dt_chk2 As DataTable = clas.getdata(Query_Str2, "QR")
        '    If dt_chk2.Rows.Count > 0 Then
        '        CmpMsg.Visible = True
        '        CmpMsg.InnerText = "This Company Name is already registered by BDC: " & dt_chk2.Rows(0)("AgntName") & vbCrLf & ". Please Enter some other Company Name."
        '        Exit Sub
        '    End If

        '    Dim Query_Str As String = "Select E.EmpId,E.AgentId, E.EMPEML,Ag.AgntName from AMS.AMS_Employer E left outer join ams.AMS_Agents Ag  on  Ag.AgntId=E.AgentId Where   lower(EMPEML)=lower('" & TextBox11.Text.Trim & "')"
        '    Dim dt_chk As DataTable = clas.getdata(Query_Str, "QR")
        '    If dt_chk.Rows.Count > 0 Then
        '        EmlMsg.Visible = True
        '        EmlMsg.InnerText = "This Email-Id is already registered by BDC: " & dt_chk.Rows(0)("AgntName") & vbCrLf & ". Please Enter some other Email-Id."
        '        'RadWindowManager1.RadAlert("<ul><li><div style=color:green;> This Email-Id is already registered by BDC: " & dt_chk.Rows(0)("AgntName") & vbCrLf & ". Please Enter some other Email-Id.</div></li></ul>", 300, 150, "Validation Success", Nothing)
        '        Exit Sub
        '    End If



        '    flag = InsertCommand()
        'End If

        'If flag = True Then
        '    If Button1.Text = "Update" Then
        '        RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Prospect Information Updated Successfully </div></li></ul>", 300, 150, "Validation Success", Nothing)
        '    Else
        '        RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Prospect Information Saved Successfully </div></li></ul>", 300, 150, "Validation Success", Nothing)
        '    End If

        '    clear()
        'Else
        '    Session.Remove("mode")
        '    Dim Err_Msg As String = Session("error").ToString.Replace(vbCrLf, "")
        '    Err_Msg = Err_Msg.Replace(vbCr, "")
        '    Err_Msg = Err_Msg.Replace(vbLf, "")
        '    RadWindowManager1.RadAlert("<ul><li><div style=color:green;>" & Err_Msg & "</div></li></ul>", 300, 150, "Validation Unsuccessful", Nothing)
        '    'Dim message As String = "alert('" & Session("error").ToString & "')"
        '    'ScriptManager.RegisterClientScriptBlock(TryCast(sender, Control), Me.GetType(), "alert", message, True)
        'End If
    End Sub




    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        clear()
        ' getmastdesg()
        ' getmastindustry()
    End Sub
    'Public Function InsertCommand() As Boolean
    '    Try
    '        'Dim transid As Int32 = getMaxID()
    '        'Dim str2 As String = "insert into tt.ukdata(transid, InDocRecdDt,InDocRecdBy, InDocStat) values(2, to_date(@InDocRecdDt,'dd-MON-yyyy'), @InDocRecdBy, @InDocStat)"
    '        Dim str As StringBuilder = New StringBuilder
    '        str.Append("insert into ams.ams_EMPLOYER(EMPID,")


    '        If TextBox1.Text <> "" Then
    '            str.Append("empfname,")
    '        End If
    '        If TextBox2.Text <> "" Then
    '            str.Append("empmname,")
    '        End If
    '        If TextBox3.Text <> "" Then
    '            str.Append("emplname,")
    '        End If
    '        If DropDownList1.SelectedValue <> "" Then
    '            str.Append("empdesg,")
    '        End If
    '        If TextBox4.Text <> "" Then
    '            str.Append("emptel,")
    '        End If
    '        If TextBox5.Text <> "" Then
    '            str.Append("empmbl,")
    '        End If
    '        If DropDownList2.SelectedValue <> "" Then
    '            str.Append("empcmpind,")
    '        End If
    '        If TextBox6.Text <> "" Then
    '            str.Append("empcmpnam,")
    '        End If
    '        If TextBox7.Text <> "" Then
    '            str.Append("empcmptelno,")
    '        End If

    '        If TextBox10.Text <> "" Then
    '            str.Append("empcmpadd,")
    '        End If
    '        If dtSubmission.SelectedDate IsNot Nothing Then
    '            str.Append("empcontdt,")
    '        End If
    '        If TextBox11.Text <> "" Then
    '            str.Append("empeml,")
    '        End If
    '        If DropDownList4.SelectedValue <> "" Then
    '            str.Append("result,")
    '        End If
    '        Dim temp As String = str.ToString
    '        str.Clear()
    '        str.Append(temp.Substring(0, temp.Length - 1))
    '        str.Append(",  KEYEDON,  KEYEDBY,ip,agentid) values(ams.ams_employer_seq.nextval,")

    '        If TextBox1.Text <> "" Then
    '            str.Append("'" & UppercaseFirstLetter(TextBox1.Text) & "',")
    '        End If
    '        If TextBox2.Text <> "" Then
    '            str.Append("'" & UppercaseFirstLetter(TextBox2.Text) & "',")
    '        End If
    '        If TextBox3.Text <> "" Then
    '            str.Append("'" & UppercaseFirstLetter(TextBox3.Text) & "',")
    '        End If
    '        If DropDownList1.SelectedValue <> "" Then
    '            str.Append("'" & DropDownList1.SelectedValue & "',")
    '        End If
    '        If TextBox4.Text <> "" Then
    '            str.Append("'" & TextBox4.Text & "',")
    '        End If
    '        If TextBox5.Text <> "" Then
    '            str.Append("'" & TextBox5.Text & "',")
    '        End If
    '        If DropDownList2.SelectedValue <> "" Then
    '            str.Append("'" & DropDownList2.SelectedValue & "',")
    '        End If
    '        If TextBox6.Text <> "" Then
    '            str.Append("'" & TextBox6.Text & "',")
    '        End If
    '        If TextBox7.Text <> "" Then
    '            str.Append("'" & TextBox7.Text & "',")
    '        End If

    '        If TextBox10.Text <> "" Then
    '            str.Append("'" & TextBox10.Text & "',")
    '        End If
    '        If dtSubmission.SelectedDate IsNot Nothing Then
    '            str.Append("to_date('" & dtSubmission.SelectedDate.Value.ToString("dd-MMM-yyyy HH:mm:ss") & "','dd-MON-yyyy hh24:mi:ss') ,")
    '            'str.Append("' & CDate(dtSubmission.SelectedDate).ToString("dd-MMM-yyyy") & "',")
    '        End If

    '        If TextBox11.Text <> "" Then
    '            str.Append("'" & TextBox11.Text & "',")
    '        End If
    '        If DropDownList4.SelectedValue <> "" Then
    '            str.Append("'" & DropDownList4.SelectedValue & "',")
    '        End If

    '        'HttpContext.Current.Request.UserHostAddress
    '        str.Append("to_date('" & Date.Now.ToString("dd-MMM-yyyy HH:mm:ss") & "','dd-MON-yyyy hh24:mi:ss')," & agntid & ",'" & HttpContext.Current.Request.UserHostAddress & "','" & agntid & "',")

    '        temp = str.ToString
    '        str.Clear()
    '        str.Append(temp.Substring(0, temp.Length - 1))
    '        str.Append(")")
    '        ' Try

    '        clas.ExecuteNonQuery(str.ToString)

    '        'InsertCommand2(transid)
    '        Return True
    '        'Catch ex As Exception
    '        '    Session("error") = ex.Message
    '        '    RadWindowManager1.RadAlert("<ul><li><div style=color:Red;>" & ex.Message & "</div></li></ul>", 300, 150, "Validation Error", Nothing)
    '        '    Return False
    '        '    Exit Function
    '        '  End Try
    '        Throw New Exception("UnKnown Exception occured , Please Contact IT Department")
    '    Catch ex As Exception
    '        Session("error") = ex.Message.ToString
    '        RadWindowManager1.RadAlert(ex.Message, 300, 200, "Exception", "")
    '        Return False
    '    End Try
    'End Function

    'Public Function updateCommand() As Boolean
    '    Try
    '        'Dim transid As Int32 = getMaxID()
    '        'Dim str2 As String = "insert into tt.ukdata(transid, InDocRecdDt,InDocRecdBy, InDocStat) values(2, to_date(@InDocRecdDt,'dd-MON-yyyy'), @InDocRecdBy, @InDocStat)"
    '        Dim str As StringBuilder = New StringBuilder
    '        str.Append("update ams.ams_EMPLOYER set ")


    '        'If TextBox1.Text <> "" Then
    '        str.Append("empfname='" & UppercaseFirstLetter(TextBox1.Text) & "',")
    '        'End If
    '        'If TextBox2.Text <> "" Then
    '        str.Append("empmname='" & UppercaseFirstLetter(TextBox2.Text) & "',")
    '        'End If
    '        'If TextBox3.Text <> "" Then
    '        str.Append("emplname='" & UppercaseFirstLetter(TextBox3.Text) & "',")
    '        'End If
    '        If DropDownList1.SelectedValue <> "" Then
    '            str.Append("empdesg='" & DropDownList1.SelectedValue & "',")
    '        End If
    '        If TextBox4.Text <> "" Then
    '            str.Append("emptel='" & TextBox4.Text & "',")
    '        End If
    '        'If TextBox5.Text <> "" Then
    '        str.Append("empmbl='" & TextBox5.Text & "',")
    '        'End If
    '        If DropDownList2.SelectedValue <> "" Then
    '            str.Append("empcmpind='" & DropDownList2.SelectedValue & "',")
    '        End If
    '        'If TextBox6.Text <> "" Then
    '        str.Append("empcmpnam='" & TextBox6.Text & "',")
    '        'End If
    '        'If TextBox7.Text <> "" Then
    '        str.Append("empcmptelno='" & TextBox7.Text & "',")
    '        'End If

    '        'If TextBox10.Text <> "" Then
    '        str.Append("empcmpadd='" & TextBox10.Text & "',")
    '        'End If
    '        'If TextBox11.Text <> "" Then
    '        str.Append("empeml='" & TextBox11.Text & "',")
    '        'End If
    '        If DropDownList4.SelectedValue <> "" Then
    '            str.Append("result='" & DropDownList4.SelectedValue & "',")
    '        End If

    '        Dim temp As String = str.ToString
    '        str.Clear()
    '        str.Append(temp.Substring(0, temp.Length - 1))
    '        str.Append(",lastupdateon=to_date('" & Date.Now.ToString("dd-MMM-yyyy HH:mm:ss") & "','dd-MON-yyyy hh24:mi:ss'),lastupdateby='" & agntid & "' where empid='" & Request.QueryString("empid") & "'")


    '        'temp = str.ToString
    '        'str.Clear()
    '        'str.Append(temp.Substring(0, temp.Length - 1))
    '        'str.Append(")")
    '        'Try

    '        clas.ExecuteNonQuery(str.ToString)
    '        'InsertCommand2(transid)

    '        Dim hostname As String = Dns.GetHostName()
    '        Dim ipaddress As String = CType(Dns.GetHostByName(hostname).AddressList.GetValue(0), IPAddress).ToString

    '        Dim Flag As Boolean = Chk_Data_Existance(agntid, Request.QueryString("empid"))
    '        If Flag = True Then
    '            Update_Ques_Ans_JobSheet_Q1(TextBox6.Text, ipaddress, Request.QueryString("empid"))
    '            Update_Ques_Ans_JobSheet_Q2(UppercaseFirstLetter(TextBox1.Text) & " " & UppercaseFirstLetter(TextBox2.Text) & " " & UppercaseFirstLetter(TextBox3.Text) & " , " & UppercaseFirstLetter(DropDownList1.SelectedItem.Text), ipaddress, Request.QueryString("empid"))

    '            Update_Ques_Ans_JobSheet_Q4(TextBox10.Text & " , Telephone : " & TextBox7.Text, ipaddress, Request.QueryString("empid"))

    '            Update_Ques_Ans_JobSheet_Q7(TextBox11.Text, ipaddress, Request.QueryString("empid"))
    '            Update_Ques_Ans_JobSheet_Q8(TextBox4.Text & " , " & TextBox5.Text, ipaddress, Request.QueryString("empid"))
    '        End If


    '        Return True
    '        'Catch ex As Exception
    '        '    Session("error") = ex.Message
    '        '    RadWindowManager1.RadAlert("<ul><li><div style=color:Red;>" & ex.Message & "</div></li></ul>", 300, 150, "Validation Error", Nothing)
    '        '    Return False
    '        '    Exit Function
    '        'End Try
    '        Throw New Exception("UnKnown Exception occured , Please Contact IT Department")
    '    Catch ex As Exception
    '        Session("error") = ex.Message.ToString
    '        RadWindowManager1.RadAlert(ex.Message, 300, 200, "Exception", "")
    '        'Dim AlertMessage As String = "alert('" & ex.Message.ToString & "')"
    '        'ScriptManager.RegisterStartupScript(Page, Page.GetType, "AlertMessage", AlertMessage, True)
    '        Return False
    '    End Try

    'End Function

    'Protected Sub Update_Ques_Ans_JobSheet_Q1(ByVal Ans As String, ByVal IP As String, ByVal EmpId As String)
    '    Dim Update_str As StringBuilder = New StringBuilder
    '    Update_str.Append(" Update Ams.Ams_Emp_Job_Ans Set Ans_Desc='" & Ans & "',KeyedOn=sysdate,IP='" & IP & "' where Qid='1' and EmpId='" & EmpId & "' ")
    '    Dim con As New OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings("constr").ToString)
    '    Dim cmd As New OracleCommand(Update_str.ToString, con)
    '    If con.State = ConnectionState.Closed Then
    '        con.Open()
    '    End If
    '    cmd.ExecuteNonQuery()
    '    con.Close()
    '    'KeyedBy='" & agntid & "',
    'End Sub

    'Protected Sub Update_Ques_Ans_JobSheet_Q2(ByVal Ans As String, ByVal IP As String, ByVal EmpId As String)
    '    Dim Update_str As StringBuilder = New StringBuilder
    '    Update_str.Append(" Update Ams.Ams_Emp_Job_Ans Set Ans_Desc='" & Ans & "',KeyedOn=sysdate,IP='" & IP & "' where Qid='2' and EmpId='" & EmpId & "' ")
    '    Dim con As New OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings("constr").ToString)
    '    Dim cmd As New OracleCommand(Update_str.ToString, con)
    '    If con.State = ConnectionState.Closed Then
    '        con.Open()
    '    End If
    '    cmd.ExecuteNonQuery()
    '    con.Close()
    'End Sub

    'Protected Sub Update_Ques_Ans_JobSheet_Q3(ByVal Ans As String, ByVal IP As String, ByVal EmpId As String)
    '    Dim Update_str As StringBuilder = New StringBuilder
    '    Update_str.Append(" Update Ams.Ams_Emp_Job_Ans Set Ans_Desc='" & Ans & "',KeyedOn=sysdate,IP='" & IP & "' where Qid='3' and EmpId='" & EmpId & "' ")
    '    Dim con As New OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings("constr").ToString)
    '    Dim cmd As New OracleCommand(Update_str.ToString, con)
    '    If con.State = ConnectionState.Closed Then
    '        con.Open()
    '    End If
    '    cmd.ExecuteNonQuery()
    '    con.Close()
    'End Sub

    'Protected Sub Update_Ques_Ans_JobSheet_Q4(ByVal Ans As String, ByVal IP As String, ByVal EmpId As String)
    '    Dim Update_str As StringBuilder = New StringBuilder
    '    Update_str.Append(" Update Ams.Ams_Emp_Job_Ans Set Ans_Desc='" & Ans & "',KeyedOn=sysdate,IP='" & IP & "' where Qid='4' and EmpId='" & EmpId & "' ")
    '    Dim con As New OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings("constr").ToString)
    '    Dim cmd As New OracleCommand(Update_str.ToString, con)
    '    If con.State = ConnectionState.Closed Then
    '        con.Open()
    '    End If
    '    cmd.ExecuteNonQuery()
    '    con.Close()
    'End Sub

    'Protected Sub Update_Ques_Ans_JobSheet_Q6(ByVal Ans As String, ByVal IP As String, ByVal EmpId As String)
    '    Dim Update_str As StringBuilder = New StringBuilder
    '    Update_str.Append(" Update Ams.Ams_Emp_Job_Ans Set Ans_Desc='" & Ans & "',KeyedOn=sysdate,IP='" & IP & "' where Qid='6' and EmpId='" & EmpId & "' ")
    '    Dim con As New OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings("constr").ToString)
    '    Dim cmd As New OracleCommand(Update_str.ToString, con)
    '    If con.State = ConnectionState.Closed Then
    '        con.Open()
    '    End If
    '    cmd.ExecuteNonQuery()
    '    con.Close()
    'End Sub

    'Protected Sub Update_Ques_Ans_JobSheet_Q7(ByVal Ans As String, ByVal IP As String, ByVal EmpId As String)
    '    Dim Update_str As StringBuilder = New StringBuilder
    '    Update_str.Append(" Update Ams.Ams_Emp_Job_Ans Set Ans_Desc='" & Ans & "',KeyedOn=sysdate,IP='" & IP & "' where Qid='7' and EmpId='" & EmpId & "' ")
    '    Dim con As New OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings("constr").ToString)
    '    Dim cmd As New OracleCommand(Update_str.ToString, con)
    '    If con.State = ConnectionState.Closed Then
    '        con.Open()
    '    End If
    '    cmd.ExecuteNonQuery()
    '    con.Close()
    'End Sub

    'Protected Sub Update_Ques_Ans_JobSheet_Q8(ByVal Ans As String, ByVal IP As String, ByVal EmpId As String)
    '    Dim Update_str As StringBuilder = New StringBuilder
    '    Update_str.Append(" Update Ams.Ams_Emp_Job_Ans Set Ans_Desc='" & Ans & "',KeyedOn=sysdate,IP='" & IP & "' where Qid='8' and EmpId='" & EmpId & "' ")
    '    Dim con As New OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings("constr").ToString)
    '    Dim cmd As New OracleCommand(Update_str.ToString, con)
    '    If con.State = ConnectionState.Closed Then
    '        con.Open()
    '    End If
    '    cmd.ExecuteNonQuery()
    '    con.Close()
    'End Sub


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

        Dim cntrl As Control = TryCast(Me.Page.Form, Control)

Line1:


        For Each c As Control In cntrl.Controls



            If TypeOf c Is TextBox Or TypeOf c Is DropDownList Or TypeOf c Is RadComboBox Or TypeOf c Is CheckBoxList Or TypeOf c Is RadDatePicker Then

                If TypeOf c Is TextBox Then

                    Dim tb As TextBox = TryCast(c, TextBox)

                    tb.Text = String.Empty

                ElseIf TypeOf c Is RadDatePicker Then
                    Dim tb As RadDatePicker = TryCast(c, RadDatePicker)

                    tb.SelectedDate = Nothing

                ElseIf TypeOf c Is DropDownList Then
                    Dim tb As DropDownList = TryCast(c, DropDownList)
                    tb.ClearSelection()
                    tb.DataBind()
                ElseIf TypeOf c Is CheckBoxList Then
                    Dim tb As CheckBoxList = TryCast(c, CheckBoxList)
                    tb.SelectedIndex = -1
                    tb.ClearSelection()
                    tb.DataBind()

                End If

            Else
                If TypeOf c Is UpdatePanel Then

                    Dim pnl As Panel = TryCast(c.FindControl("pnl"), Panel)

                    If Not pnl Is Nothing Then
                        cntrl = pnl
                        GoTo Line1

                    End If





                End If

            End If



        Next




        Button1.Text = "Submit"

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
    'Protected Sub Btnref_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Btnref.Click, Btnref2.Click
    '    getmastdesg()
    '    getmastindustry()
    '    If Request.QueryString("mode") = "edit" Then
    '        getrcdforedit()
    '    End If
    'End Sub

    'Protected Sub TextBox11_TextChanged(sender As Object, e As System.EventArgs) Handles TextBox11.TextChanged
    'If TextBox11.Text <> "" Then
    '    If TextBox11.Text <> "" Then
    '        TextBox11.Text = TextBox11.Text.Replace("'", "")
    '    End If
    '    Dim Query_Str As String
    '    If Button1.Text = "Update" Then
    '        Query_Str = "Select E.EmpId,E.AgentId, E.EMPEML,Ag.AgntName from AMS.AMS_Employer E,ams.AMS_Agents Ag  where Ag.AgntId=E.AgentId and   lower(EMPEML)=lower('" & TextBox11.Text & "') and E.EmpId<>'" & Request.QueryString("empid") & "'"
    '    Else
    '        Query_Str = "Select E.EmpId,E.AgentId, E.EMPEML,Ag.AgntName from AMS.AMS_Employer E,ams.AMS_Agents Ag  where Ag.AgntId=E.AgentId and   lower(EMPEML)=lower('" & TextBox11.Text & "')"
    '    End If

    '    Dim dt_chk As DataTable = clas.getdata(Query_Str, "QR")
    '    If dt_chk.Rows.Count > 0 Then
    '        EmlMsg.Visible = True
    '        EmlMsg.InnerText = "This Email-Id is already registered by BDC: " & dt_chk.Rows(0)("AgntName") & vbCrLf & ". Please Enter some other Email-Id."
    '        'RadWindowManager1.RadAlert("<ul><li><div style=color:green;> This Email-Id is already registered by BDC: " & dt_chk.Rows(0)("AgntName") & vbCrLf & ". Please Enter some other Email-Id.</div></li></ul>", 300, 150, "Validation Success", Nothing)
    '        'TextBox11.Text = String.Empty
    '    Else
    '        EmlMsg.Visible = False
    '    End If
    'Else
    '    EmlMsg.Visible = False
    'End If


    '  End Sub

    ' Protected Sub TextBox6_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox6.TextChanged
    'If TextBox6.Text <> "" Then
    '    If TextBox6.Text <> "" Then
    '        TextBox6.Text = TextBox6.Text.Replace("'", "''")
    '    End If
    '    Dim Query_Str As String
    '    If Button1.Text = "Update" Then
    '        Query_Str = "Select E.EmpId,E.AgentId, E.EMPEML,Ag.AgntName,E.EmpFname || ' ' || E.EmpMName || ' ' || E.EmpLName as Name from AMS.AMS_Employer E,ams.AMS_Agents Ag  where Ag.AgntId=E.AgentId and   lower(E.EMPCMPNAM)=lower('" & TextBox6.Text & "') and E.EmpId<>'" & Request.QueryString("empid") & "' and lower(E.EmpFname)=lower('" & TextBox1.Text.Trim & "') and lower(E.EmpMName)=lower('" & TextBox2.Text.Trim & "') and lower(E.EmpLName)=lower('" & TextBox3.Text.Trim & "')"
    '    Else
    '        Query_Str = "Select E.EmpId,E.AgentId, E.EMPEML,Ag.AgntName,E.EmpFname || ' ' || E.EmpMName || ' ' || E.EmpLName as Name from AMS.AMS_Employer E,ams.AMS_Agents Ag  where Ag.AgntId=E.AgentId and   lower(E.EMPCMPNAM)=lower('" & TextBox6.Text & "') and lower(E.EmpFname)=lower('" & TextBox1.Text.Trim & "') and lower(E.EmpMName)=lower('" & TextBox2.Text.Trim & "') and lower(E.EmpLName)=lower('" & TextBox3.Text.Trim & "')"
    '    End If

    '    Dim dt_chk As DataTable = clas.getdata(Query_Str, "QR")
    '    If dt_chk.Rows.Count > 0 Then
    '        CmpMsg.Visible = True
    '        CmpMsg.InnerText = "This Company Name is already registered by BDC: " & dt_chk.Rows(0)("AgntName") & vbCrLf & " For Employer: " & dt_chk.Rows(0)("Name") & ". Please Enter some other Company Name."
    '        'RadWindowManager1.RadAlert("<ul><li><div style=color:green;> This Email-Id is already registered by BDC: " & dt_chk.Rows(0)("AgntName") & vbCrLf & ". Please Enter some other Email-Id.</div></li></ul>", 300, 150, "Validation Success", Nothing)
    '        'TextBox6.Text = String.Empty
    '    Else
    '        CmpMsg.Visible = False
    '    End If
    'Else
    '    CmpMsg.Visible = False
    'End If

    ' End Sub



End Class
