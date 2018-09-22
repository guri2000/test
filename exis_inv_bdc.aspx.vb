Imports System.Data
Imports System.IO
Imports Telerik.Web.UI
Imports System.Text
Imports System.Environment

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
            If Request.QueryString("frm") IsNot Nothing And Not Request.QueryString("frm") = "" Then
                Session("mode") = ""
            End If
            getmastdesg()
         
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

        Ifrm1.Attributes.Add("src", "cat_det.aspx?mode=C")
      
    End Sub
    
    Private Sub getmastres(ByVal ctid As String)
        'qry = "select * from ams.ams_subcat where subcatsts='A' and catid='" & ctid & "'"
        'dt = clas.getdata(qry, "QR")
        'DropDownList3.Items.Clear()
        'DropDownList3.DataBind()
        'DropDownList3.Items.Add(New ListItem("Select Sub-Category", ""))
        'If dt.Rows.Count > 0 Then
        '    DropDownList3.DataTextField = "subcatnam"
        '    DropDownList3.DataValueField = "subcatid"
        '    DropDownList3.DataSource = dt
        '    DropDownList3.DataBind()
        '    DropDownList3.Items.Insert(0, (New ListItem("Select Sub-Category", "")))

        'End If


        'a1.Attributes.Remove("style")
        'a1.Attributes.Add("style", "display:inline")
        'Ifrm_ind.Attributes.Add("src", "cat_det.aspx?mode=S&catid=" & ctid)
    End Sub
    Private Sub getrcdforedit()
        qry = "select * from ams.exist_inv_rec where fsn='" & TextBox1.Text.Trim() & "'"
        dt = clas.getdata(qry, "QR")
        If dt.Rows.Count > 0 Then
            ImageButton1.Enabled = False
            RadWindowManager1.RadAlert("<ul><li><div style=color:red;> This FSN has already keyedin</div></li></ul>", 300, 150, "Validation Error", Nothing)
            Exit Sub

        End If

    End Sub
   
    Private Sub getmastdesg()
        qry = "select lgnagntid, from ams.ams_catmaster where catsts='A'"

        qry = "select agntid,agent_name from ams.ams_agentData where upper(account_access)='AGENT' and upper(ACCOUNT_STATUS)='ACTIVE'"

        dt = clas.getdata(qry, "QR")


        DropDownList1.Items.Clear()
        DropDownList1.DataBind()
        DropDownList1.Items.Add(New ListItem("Select BDC", ""))
        DropDownList1.DataTextField = "agent_name"
        DropDownList1.DataValueField = "agntid"
        DropDownList1.DataSource = dt
        DropDownList1.DataBind()




    End Sub
    'Private Sub getminmax()

    '    dtSubmission.MinDate = Date.Now.AddDays(-1000)
    '    dtSubmission.MaxDate = Date.Now.AddDays(+1)
    '    dtSubmission.SelectedDate = Date.Now


    'End Sub

    Public Function FirstDayOfMonth(ByVal sourceDate As DateTime) As DateTime
        Return New DateTime(sourceDate.Year, sourceDate.Month, 1)
    End Function

    'Get the last day of the month
    Public Function LastDayOfMonth(ByVal sourceDate As DateTime) As DateTime
        Dim lastDay As DateTime = New DateTime(sourceDate.Year, sourceDate.Month, 1)
        Return lastDay.AddMonths(1).AddDays(-1)
    End Function
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click, ImageButton1.Click

        'If TextBox10.Text <> "" Then
        '    TextBox10.Text = TextBox10.Text.Replace("'", "''")
        'End If


        If Button1.Text = "Update" Then

            '   flag = updateCommand()
        Else

                            Dim client_ip As String = Request.UserHostAddress()
                            Dim Browser_Name As String = Request.Browser.Browser.ToString & " " & Request.Browser.Version.ToString

                            Dim str As StringBuilder = New StringBuilder

            If DropDownList1.SelectedValue <> "" Then
                str.Append("update AMS.EXIST_INV_REC set agentid='" & DropDownList1.SelectedValue & "', ASSIGNBY= '" & agntid & "', ASSIGNDT =sysdate where fsn='" & Request.QueryString("fsn") & "'")
            End If                          
            If str.ToString().Length > 0 Then

                Dim i = clas.ExecuteNonQuery(str.ToString())              
                If i = 1 Then
                    RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Transaction Done Successfully </div></li></ul>", 300, 150, "Validation Success", Nothing)
                    Exit Sub
                Else

                    Dim Err_Msg As String = clas.getexception().Replace(vbCrLf, "")
                    Err_Msg = Err_Msg.Replace(vbCr, "")
                    Err_Msg = Err_Msg.Replace(vbLf, "")
                    RadWindowManager1.RadAlert("<ul><li><div style=color:red;>System found some exception:" & Err_Msg & "</div></li></ul>", 300, 100, "Validation Exception Failure", Nothing)

                    '  RadWindowManager1.RadAlert("<ul><li><div style=color:red;> Transaction not Done </div></li></ul>", 300, 150, "Validation Success", Nothing)
                End If
                ' Response.Write(qry)

            Else
                'RadWindowManager1.RadAlert("<ul><li><div style=color:red;>Some Malware Function issue</div></li></ul>", 300, 100, "Validation Exception Failure", Nothing)
            End If
        End If


    End Sub

   


    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        clear()
        ' getmastdesg()
        ' getmastindustry()
    End Sub
   

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
    '        'If TextBox8.Text <> "" Then
    '        str.Append("empcmpfax='" & TextBox8.Text & "',")
    '        'End If
    '        'If TextBox9.Text <> "" Then
    '        str.Append("empcmpweb='" & TextBox9.Text & "',")
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

    '        Dim con As New OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings("constr").ToString)
    '        Dim cmd As New OracleCommand(str.ToString, con)
    '        If con.State = ConnectionState.Closed Then
    '            con.Open()
    '        End If

    '        cmd.ExecuteNonQuery()
    '        con.Close()
    '        'InsertCommand2(transid)

    '        Dim hostname As String = Dns.GetHostName()
    '        Dim ipaddress As String = CType(Dns.GetHostByName(hostname).AddressList.GetValue(0), IPAddress).ToString

    '        Dim Flag As Boolean = Chk_Data_Existance(agntid, Request.QueryString("empid"))
    '        If Flag = True Then
    '            Update_Ques_Ans_JobSheet_Q1(TextBox6.Text, ipaddress, Request.QueryString("empid"))
    '            Update_Ques_Ans_JobSheet_Q2(UppercaseFirstLetter(TextBox1.Text) & " " & UppercaseFirstLetter(TextBox2.Text) & " " & UppercaseFirstLetter(TextBox3.Text) & " , " & UppercaseFirstLetter(DropDownList1.SelectedItem.Text), ipaddress, Request.QueryString("empid"))
    '            Update_Ques_Ans_JobSheet_Q3(TextBox9.Text, ipaddress, Request.QueryString("empid"))
    '            Update_Ques_Ans_JobSheet_Q4(TextBox10.Text & " , Telephone : " & TextBox7.Text, ipaddress, Request.QueryString("empid"))
    '            Update_Ques_Ans_JobSheet_Q6(TextBox8.Text, ipaddress, Request.QueryString("empid"))
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

    Protected Function Chk_Data_Existance(ByVal AgentId As String, ByVal EmpId As String) As Boolean
        Dim Flag As Boolean = False
        Dim Chk_Qry As String = "Select * from Ams.Ams_Emp_Job_Ans where  EMPID='" & EmpId & "'"
        Dim _D2 As DataTable = clas.getdata(Chk_Qry, "QR")
        If _D2.Rows.Count > 0 Then
            Flag = True
        End If
        Return Flag
    End Function




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
        'TextBox1.Text = String.Empty

        DropDownList1.ClearSelection()

 
     
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
    '    getmastres(DropDownList1.SelectedValue)

    'End Sub

    'Protected Sub TextBox11_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox11.TextChanged
    '    If TextBox11.Text <> "" Then
    '        If TextBox11.Text <> "" Then
    '            TextBox11.Text = TextBox11.Text.Replace("'", "")
    '        End If
    '        Dim Query_Str As String
    '        If Button1.Text = "Update" Then
    '            Query_Str = "Select E.EmpId,E.AgentId, E.EMPEML,Ag.AgntName from AMS.AMS_Employer E,ams.AMS_Agents Ag  where Ag.AgntId=E.AgentId and   lower(EMPEML)=lower('" & TextBox11.Text & "') and E.EmpId<>'" & Request.QueryString("empid") & "'"
    '        Else
    '            Query_Str = "Select E.EmpId,E.AgentId, E.EMPEML,Ag.AgntName from AMS.AMS_Employer E,ams.AMS_Agents Ag  where Ag.AgntId=E.AgentId and   lower(EMPEML)=lower('" & TextBox11.Text & "')"
    '        End If

    '        Dim dt_chk As DataTable = getdata(Query_Str, "QR")
    '        If dt_chk.Rows.Count > 0 Then
    '            EmlMsg.Visible = True
    '            EmlMsg.InnerText = "This Email-Id is already registered by BDC: " & dt_chk.Rows(0)("AgntName") & vbCrLf & ". Please Enter some other Email-Id."
    '            'RadWindowManager1.RadAlert("<ul><li><div style=color:green;> This Email-Id is already registered by BDC: " & dt_chk.Rows(0)("AgntName") & vbCrLf & ". Please Enter some other Email-Id.</div></li></ul>", 300, 150, "Validation Success", Nothing)
    '            'TextBox11.Text = String.Empty
    '        Else
    '            EmlMsg.Visible = False
    '        End If
    '    Else
    '        EmlMsg.Visible = False
    '    End If


    'End Sub

    'Protected Sub TextBox9_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox9.TextChanged
    '    If TextBox9.Text <> "" Then
    '        If TextBox9.Text <> "" Then
    '            TextBox9.Text = TextBox9.Text.Replace("'", "")
    '        End If
    '        Dim Query_Str As String
    '        If Button1.Text = "Update" Then
    '            Query_Str = "Select E.EmpId,E.AgentId, E.EMPEML,Ag.AgntName from AMS.AMS_Employer E,ams.AMS_Agents Ag  where Ag.AgntId=E.AgentId and   lower(EMPCMPWEB)=lower('" & TextBox9.Text & "')  and E.EmpId<>'" & Request.QueryString("empid") & "'"
    '        Else
    '            Query_Str = "Select E.EmpId,E.AgentId, E.EMPEML,Ag.AgntName from AMS.AMS_Employer E,ams.AMS_Agents Ag  where Ag.AgntId=E.AgentId and   lower(EMPCMPWEB)=lower('" & TextBox9.Text & "')"
    '        End If

    '        Dim dt_chk As DataTable = getdata(Query_Str, "QR")
    '        If dt_chk.Rows.Count > 0 Then
    '            WebMsg.Visible = True
    '            WebMsg.InnerText = "This Website is already registered by BDC: " & dt_chk.Rows(0)("AgntName") & vbCrLf & ". Please Enter some other Website."
    '            'RadWindowManager1.RadAlert("<ul><li><div style=color:green;> This Email-Id is already registered by BDC: " & dt_chk.Rows(0)("AgntName") & vbCrLf & ". Please Enter some other Email-Id.</div></li></ul>", 300, 150, "Validation Success", Nothing)
    '            'TextBox9.Text = String.Empty
    '        Else
    '            WebMsg.Visible = False
    '        End If
    '    Else
    '        WebMsg.Visible = False
    '    End If


    'End Sub

    'Protected Sub TextBox6_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox6.TextChanged
    '    If TextBox6.Text <> "" Then
    '        If TextBox6.Text <> "" Then
    '            TextBox6.Text = TextBox6.Text.Replace("'", "''")
    '        End If
    '        Dim Query_Str As String
    '        If Button1.Text = "Update" Then
    '            Query_Str = "Select E.EmpId,E.AgentId, E.EMPEML,Ag.AgntName,E.EmpFname || ' ' || E.EmpMName || ' ' || E.EmpLName as Name from AMS.AMS_Employer E,ams.AMS_Agents Ag  where Ag.AgntId=E.AgentId and   lower(E.EMPCMPNAM)=lower('" & TextBox6.Text & "') and E.EmpId<>'" & Request.QueryString("empid") & "' and lower(E.EmpFname)=lower('" & TextBox1.Text.Trim & "') and lower(E.EmpMName)=lower('" & TextBox2.Text.Trim & "') and lower(E.EmpLName)=lower('" & TextBox3.Text.Trim & "')"
    '        Else
    '            Query_Str = "Select E.EmpId,E.AgentId, E.EMPEML,Ag.AgntName,E.EmpFname || ' ' || E.EmpMName || ' ' || E.EmpLName as Name from AMS.AMS_Employer E,ams.AMS_Agents Ag  where Ag.AgntId=E.AgentId and   lower(E.EMPCMPNAM)=lower('" & TextBox6.Text & "') and lower(E.EmpFname)=lower('" & TextBox1.Text.Trim & "') and lower(E.EmpMName)=lower('" & TextBox2.Text.Trim & "') and lower(E.EmpLName)=lower('" & TextBox3.Text.Trim & "')"
    '        End If

    '        Dim dt_chk As DataTable = getdata(Query_Str, "QR")
    '        If dt_chk.Rows.Count > 0 Then
    '            CmpMsg.Visible = True
    '            CmpMsg.InnerText = "This Company Name is already registered by BDC: " & dt_chk.Rows(0)("AgntName") & vbCrLf & " For Employer: " & dt_chk.Rows(0)("Name") & ". Please Enter some other Company Name."
    '            'RadWindowManager1.RadAlert("<ul><li><div style=color:green;> This Email-Id is already registered by BDC: " & dt_chk.Rows(0)("AgntName") & vbCrLf & ". Please Enter some other Email-Id.</div></li></ul>", 300, 150, "Validation Success", Nothing)
    '            'TextBox6.Text = String.Empty
    '        Else
    '            CmpMsg.Visible = False
    '        End If
    '    Else
    '        CmpMsg.Visible = False
    '    End If

    'End Sub

    Protected Sub btnGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGo.Click

        If TextBox1.Text.Trim().Length = 0 Then
            RadWindowManager1.RadAlert("<ul><li><div style=color:red;> Invalid FSN</div></li></ul>", 300, 150, "Validation Error", Nothing)
            Exit Sub
        End If

        dt = clas.getdata("SELECT * FROM RAHUL.APPCLNTVIEW WHERE IMMCLASS IN (SELECT IMMCLASS FROM TT.IMMCLASS_MASTER WHERE JOSCP=1) and fileserialno='" & TextBox1.Text.Trim() & "'", "TX")


        If dt.Rows.Count > 0 Then

            name.InnerText = Convert.ToString(dt.Rows(0)("appname"))
            fno.InnerText = Convert.ToString(dt.Rows(0)("fileno"))
            rtdate.InnerText = Convert.ToString(dt.Rows(0)("retaindate"))
            immclass.InnerText = Convert.ToString(dt.Rows(0)("descr"))
            brcnh.InnerText = Convert.ToString(dt.Rows(0)("DLBRANCH"))
            cnt.InnerText = Convert.ToString(dt.Rows(0)("forcountry"))
            status.InnerText = Convert.ToString(dt.Rows(0)("statdesc"))
            ImageButton1.Enabled = True
            getrcdforedit()

        Else
            RadWindowManager1.RadAlert("<ul><li><div style=color:red;> This FSN does not belongs to the Job Order Category</div></li></ul>", 300, 150, "Validation Error", Nothing)
            Exit Sub
        End If


    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownList1.SelectedIndexChanged

        getmastres(DropDownList1.SelectedValue)

    End Sub
End Class
