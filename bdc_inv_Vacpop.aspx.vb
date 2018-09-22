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
            Session("mode") = ""
            fillby()
            fillpro()
            fillcur()
            fillStat()
            'fillstatus()
            'fillPF()
            binddates(dt_open, Date.Today.ToString("dd-MMM-yyyy"), Date.Today.ToString("dd-MMM-yyyy"), Date.Today.AddMonths(1).ToString("dd-MMM-yyyy"), True)
            binddates(dt_close, "", Date.Today.AddDays(7).ToString("dd-MMM-yyyy"), Date.Today.AddMonths(3).ToString("dd-MMM-yyyy"), True)
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

        If Request.QueryString("mode") = "edit" And Session("mode") = "" Then           
            getrcdforedit()
            Button2.Enabled = False
        Else

        End If

        ' Ifrm1.Attributes.Add("src", "cat_det.aspx?mode=C")
      
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
        Try
        
            qry = "select * from AMS.ADV_VACANCY where vacid='" & Request.QueryString("vacid") & "'"
        dt = clas.getdata(qry, "QR")
        If dt.Rows.Count > 0 Then
                Button1.Text = "Update"
                binddates(dt_open, "", Date.Today.AddYears(-5).ToString("dd-MMM-yyyy"), Date.Today.AddYears(5).ToString("dd-MMM-yyyy"), True)
                binddates(dt_close, "", Date.Today.AddYears(-5).ToString("dd-MMM-yyyy"), Date.Today.AddYears(5).ToString("dd-MMM-yyyy"), True)

                ' ctr_txtexpense.Text = Convert.ToString(dt.Rows(0)("ADVEXPENSE"))
                TextBox10.Text = Convert.ToString(dt.Rows(0)("REMARKS"))
                txtVname.Text = Convert.ToString(dt.Rows(0)("VACNAME"))
                txtkskill.Text = Convert.ToString(dt.Rows(0)("KEYSKILLS"))
                txtOpening.Text = Convert.ToString(dt.Rows(0)("OPENING"))
                txtVname.Enabled = False


                If IsDBNull(dt.Rows(0)("LOCID")) = False Then
                    DDLOC.Items.FindByValue(Convert.ToString(dt.Rows(0)("LOCID"))).Selected = True
                End If
                If IsDBNull(dt.Rows(0)("status")) = False Then
                    ddl_scope.Items.FindByValue(Convert.ToString(dt.Rows(0)("status"))).Selected = True
                End If


                If IsDBNull(dt.Rows(0)("VACOPNDT")) = False Then
                    'dt_open.MinDate = Date.Now.AddMonths(-1)
                    'dt_open.MaxDate = Date.Now.AddMonths(1)
                    dt_open.SelectedDate = dt.Rows(0)("VACOPNDT")
                    dt_open.Enabled = False
                End If

                If IsDBNull(dt.Rows(0)("VACTTCLS")) = False Then
                    'dt_close.MinDate = Date.Now.AddMonths(-1)
                    'dt_close.MaxDate = Date.Now.AddMonths(2)
                    dt_close.SelectedDate = dt.Rows(0)("VACTTCLS")
                    dt_close.Enabled = False
                End If

            'RadWindowManager1.RadAlert("<ul><li><div style=color:red;> This FSN has already keyedin</div></li></ul>", 300, 150, "Validation Error", Nothing)
            'Exit Sub
            Session("mode") = "edit"
        Else
            Button1.Enabled = False

            ImageButton1.Enabled = False
            ImageButton1.ToolTip = " Invalid Record ID for Edit"
            End If
        Catch ex As Exception
            Session("error") = ex.Message.ToString
            RadWindowManager1.RadAlert(ex.Message, 300, 200, "Exception", "")
            'Dim AlertMessage As String = "alert('" & ex.Message.ToString & "')"
            'ScriptManager.RegisterStartupScript(Page, Page.GetType, "AlertMessage", AlertMessage, True)
            Session("mode") = "edit"
        End Try

    End Sub
   
    Private Sub fillStat()
        Try
            'qry = "select agntname,agntid from ams.ams_agents a left join ams.ams_usermaster b on a.agntid=b.id and b.lgnroll='S'"
            qry = "select statid,statdescr from AMS.AMS_Statmaster where statstatus='A' and statSCP=60"

            dt = clas.getdata(qry, "QR")
            ddl_scope.DataTextField = "statdescr"
            ddl_scope.DataValueField = "statid"

            ddl_scope.DataSource = dt
            ddl_scope.DataBind()

            ddl_scope.Items.Insert(0, New ListItem("Choose Scope", ""))

        Catch ex As Exception
            Session("error") = ex.Message.ToString
            RadWindowManager1.RadAlert(ex.Message, 300, 200, "Exception", "")
            'Dim AlertMessage As String = "alert('" & ex.Message.ToString & "')"
            'ScriptManager.RegisterStartupScript(Page, Page.GetType, "AlertMessage", AlertMessage, True)
        End Try

    End Sub

    Private Sub fillby()
        Try
            'qry = "select agntname,agntid from ams.ams_agents a left join ams.ams_usermaster b on a.agntid=b.id and b.lgnroll='S'"
            qry = "select Empno,Name from TT.EMPLOYEES where desgid=(select desgid from ranu.desgmaster where desiglevel=1) and active='T' "

            dt = clas.getdata(qry, "QR")
            'ctr_dtAppBy.DataTextField = "Name"
            'ctr_dtAppBy.DataValueField = "Empno"

            'ctr_dtAppBy.DataSource = dt
            'ctr_dtAppBy.DataBind()

            'ctr_dtAppBy.Items.Insert(0, New ListItem("Choose Sub Category", ""))

        Catch ex As Exception
            Session("error") = ex.Message.ToString
            RadWindowManager1.RadAlert(ex.Message, 300, 200, "Exception", "")
            'Dim AlertMessage As String = "alert('" & ex.Message.ToString & "')"
            'ScriptManager.RegisterStartupScript(Page, Page.GetType, "AlertMessage", AlertMessage, True)
        End Try

    End Sub

    Private Sub fillcur()
        Try
            'qry = "select agntname,agntid from ams.ams_agents a left join ams.ams_usermaster b on a.agntid=b.id and b.lgnroll='S'"
            qry = "select currcode,descr from tt.CURRENCYMASTER "

            dt = clas.getdata(qry, "QR")
            'ddl_Curr.DataTextField = "descr"
            'ddl_Curr.DataValueField = "currcode"

            'ddl_Curr.DataSource = dt
            'ddl_Curr.DataBind()

            'ddl_Curr.Items.Insert(0, New ListItem("Choose Sub Category", ""))

        Catch ex As Exception
            Session("error") = ex.Message.ToString
            RadWindowManager1.RadAlert(ex.Message, 300, 200, "Exception", "")
            'Dim AlertMessage As String = "alert('" & ex.Message.ToString & "')"
            'ScriptManager.RegisterStartupScript(Page, Page.GetType, "AlertMessage", AlertMessage, True)
        End Try

    End Sub

    Private Sub fillpro()
        qry = "SELECT TRANSID, NAME FROM TT.ASSFORCOUNTRY  where JoSCP in (1,0) And Active='A' Order By Name Asc"

        dt = clas.getdata(qry, "QR")
        DDLOC.DataTextField = "NAME"
        DDLOC.DataValueField = "TRANSID"
        DDLOC.DataSource = dt
        DDLOC.DataBind()
        DDLOC.Items.Insert(0, New ListItem("Choose Location", ""))

    End Sub



   
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click, ImageButton1.Click
        Try

            Dim flag As Boolean = False

        If Button1.Text = "Update" Then

            flag = updateCommand()
            If flag = True Then
                    RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Vacancy Information Updated Successfully </div></li></ul>", 300, 150, "Validation Success", Nothing)
                    Exit Sub
                Else

                    Dim Err_Msg As String = clas.getexception().Replace(vbCrLf, "")
                    Err_Msg = Err_Msg.Replace(vbCr, "")
                    Err_Msg = Err_Msg.Replace(vbLf, "")
                    RadWindowManager1.RadAlert("<ul><li><div style=color:red;>System found some exception:" & Err_Msg & "</div></li></ul>", 300, 100, "Validation Exception Failure", Nothing)

                    '  RadWindowManager1.RadAlert("<ul><li><div style=color:red;> Transaction not Done </div></li></ul>", 300, 150, "Validation Success", Nothing)
                End If


            Else

                flag = insercommand()


                If flag = True Then

                    RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Transaction Successfull </div></li></ul>", 300, 150, "Validation Success", Nothing)
                    clear()
                    Exit Sub
                Else

                    Dim Err_Msg As String = clas.getexception().Replace(vbCrLf, "")
                    Err_Msg = Err_Msg.Replace(vbCr, "")
                    Err_Msg = Err_Msg.Replace(vbLf, "")
                    RadWindowManager1.RadAlert("<ul><li><div style=color:red;>System found some exception:" & Err_Msg & "</div></li></ul>", 300, 100, "Validation Exception Failure", Nothing)

                    '  RadWindowManager1.RadAlert("<ul><li><div style=color:red;> Transaction not Done </div></li></ul>", 300, 150, "Validation Success", Nothing)
                End If

            End If

        Catch

        End Try


    End Sub




    Public Function insercommand() As Boolean
        Try
            Dim str As StringBuilder = New StringBuilder

            '          INSERT INTO AMS.ADV_VACANCY
            '( VACID, VACNAME,LOCID,VACOPNDT,VACTTCLS,VACACTCLS,REMARKS,KEYSKILLS,OPENING,STATUS,KEYEDBY,KEYEDON,KEYEDMIP,LASTUPDATEON,LASTUPDATEBY  )
            '          VALUES()                    

            str.Append("INSERT INTO AMS.ADV_VACANCY(VACID,")


            If txtVname.Text.Trim <> "" Then
                str.Append("VACNAME,")
            End If
            If DDLOC.SelectedValue <> "" Then
                str.Append("LOCID,")
            End If

            If Not dt_open.SelectedDate Is Nothing Then
                If (dt_open.SelectedDate.ToString.Length >= 1) Then
                    str.Append("VACOPNDT,")
                End If
            End If
            If Not dt_close.SelectedDate Is Nothing Then
                If (dt_close.SelectedDate.ToString.Length >= 1) Then
                    str.Append("VACTTCLS,")
                End If
            End If
            If TextBox10.Text <> "" Then
                str.Append("REMARKS,")
            End If
            If txtkskill.Text <> "" Then
                str.Append("KEYSKILLS,")
            End If
            If txtOpening.Text <> "" Then
                str.Append("OPENING,")
            End If
            If ddl_scope.SelectedValue <> "" Then
                str.Append("STATUS,")
            End If

            str.Append("KEYEDON, KEYEDBY, KEYEDMIP) ")

            str.Append(" values ( AMS.ADV_VACANCY_SEQ.nextval,")


            If txtVname.Text.Trim <> "" Then
                str.Append("'" & StrConv(txtVname.Text.Trim, VbStrConv.ProperCase) & "',")
            End If
            If DDLOC.SelectedValue <> "" Then
                str.Append("'" & DDLOC.SelectedValue & "',")
            End If

            If Not dt_open.SelectedDate Is Nothing Then
                If (dt_open.SelectedDate.ToString.Length >= 1) Then
                    str.Append("'" & CDate(dt_open.SelectedDate).ToString("dd-MMM-yyyy") & "',")
                End If
            End If
            If Not dt_close.SelectedDate Is Nothing Then
                If (dt_close.SelectedDate.ToString.Length >= 1) Then
                    str.Append("'" & CDate(dt_close.SelectedDate).ToString("dd-MMM-yyyy") & "',")
                End If
            End If
            If TextBox10.Text <> "" Then
                str.Append("'" & TextBox10.Text.Trim & "',")
            End If
            If txtkskill.Text <> "" Then
                str.Append("'" & txtkskill.Text.Trim & "',")
            End If
            If txtOpening.Text <> "" Then
                str.Append("'" & txtOpening.Text.Trim & "',")
            End If
            If ddl_scope.SelectedValue <> "" Then
                str.Append("'" & ddl_scope.SelectedValue & "',")
            End If
            str.Append("sysdate," & agntid & ",'" & HttpContext.Current.Request.UserHostAddress & "' )")

            If str.ToString().Length > 0 Then

                Dim i = clas.ExecuteNonQuery(str.ToString())
                If i = 1 Then
                    Return True
                    Exit Function
                End If
                'Response.Write(str.ToString)
            Else

                'RadWindowManager1.RadAlert("<ul><li><div style=color:red;>Some Malware Function issue</div></li></ul>", 300, 100, "Validation Exception Failure", Nothing)
                Return False
                Exit Function
            End If


        Catch ex As Exception
            Session("error") = ex.Message.ToString
            ' Response.Write(qry)
            RadWindowManager1.RadAlert(ex.Message, 300, 200, "Exception", "")
            'Dim AlertMessage As String = "alert('" & ex.Message.ToString & "')"
            'ScriptManager.RegisterStartupScript(Page, Page.GetType, "AlertMessage", AlertMessage, True)
            Return False
        End Try
    End Function


    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        clear()
        ' getmastdesg()
        ' getmastindustry()
    End Sub


    Public Function updateCommand() As Boolean
        Try       
            Dim str As StringBuilder = New StringBuilder
            str.Append("update AMS.ADV_VACANCY set ")

            If txtVname.Text.Trim <> "" Then          
                str.Append("VACNAME='" & txtVname.Text.Trim & "',")
            End If

            If DDLOC.SelectedValue <> "" Then

                str.Append("LOCID='" & DDLOC.SelectedValue & "',")
            End If

            If Not dt_open.SelectedDate Is Nothing Then           
                If (dt_open.SelectedDate.ToString.Length >= 1) Then
                    str.Append("VACOPNDT='" & CDate(dt_open.SelectedDate).ToString("dd-MMM-yyyy") & "',")
                End If
            End If
            If Not dt_close.SelectedDate Is Nothing Then          
                If (dt_open.SelectedDate.ToString.Length >= 1) Then
                    str.Append("VACTTCLS='" & CDate(dt_open.SelectedDate).ToString("dd-MMM-yyyy") & "',")
                End If
            End If

            If TextBox10.Text <> "" Then
                str.Append("REMARKS='" & TextBox10.Text.Trim & "',")
            End If
            If txtkskill.Text <> "" Then            
                str.Append("KEYSKILLS='" & txtkskill.Text.Trim & "',")
            End If
            If txtOpening.Text <> "" Then         
                str.Append("OPENING='" & txtOpening.Text.Trim & "',")
            End If
            If ddl_scope.SelectedValue <> "" Then

                str.Append("Status='" & ddl_scope.SelectedValue & "',")
            End If


            Dim temp As String = str.ToString
            str.Clear()
            str.Append(temp.Substring(0, temp.Length - 1))
            str.Append(",LASTUPDATEON =sysdate,LASTUPDATEBY='" & agntid & "' where VACID='" & Request.QueryString("vacid") & "'")


            If str.ToString().Length > 0 Then

                Dim i = clas.ExecuteNonQuery(str.ToString())
                If i = 1 Then
                    Return True
                    Exit Function
                Else
                    Return False
                    Exit Function
                End If
            End If

            Return False
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

    Private Sub clear()
        TextBox10.Text = String.Empty
        DDLOC.ClearSelection()
        'ctr_txtexpense.Text = String.Empty
        'ctr_dtAppBy.ClearSelection()
        dt_open.Clear()
        dt_close.Clear()
        txtkskill.Text = String.Empty
        txtOpening.Text = String.Empty
        txtVname.Text = String.Empty
        TextBox10.Text = String.Empty
        DDLOC.ClearSelection()
        ddl_scope.ClearSelection()
        'binddates(dt_open, Date.Today.ToString("dd-MMM-yyyy"), Date.Today.ToString("dd-MMM-yyyy"), Date.Today.AddMonths(1).ToString("dd-MMM-yyyy"), True)
        'binddates(dt_close, "", Date.Today.AddMonths(-3).ToString("dd-MMM-yyyy"), Date.Today.AddDays(-1).ToString("dd-MMM-yyyy"), True)
        ''  TextBox1.Text = String.Empty
    End Sub

End Class
