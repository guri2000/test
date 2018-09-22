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
            Session("mode2") = ""
            fillby()
            fillpro()
            fillcur()
            fillVac()
            'fillstatus()
            'fillPF()
            binddates(ctr_dtadv, Date.Today.ToString("dd-MMM-yyyy"), Date.Today.ToString("dd-MMM-yyyy"), Date.Today.AddMonths(2).ToString("dd-MMM-yyyy"), True)
            binddates(ctr_appDt, "", Date.Today.AddDays(-15).ToString("dd-MMM-yyyy"), Date.Today.AddDays(-1).ToString("dd-MMM-yyyy"), True)
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

        If Request.QueryString("mode") = "edit" And Session("mode2") = "" Then
            getrcdforedit()
            Button2.Enabled = False
        Else

        End If
        If Request.QueryString("vacid") <> "" Then
            ChkVac()
        End If


        ' Ifrm1.Attributes.Add("src", "cat_det.aspx?mode=C")

    End Sub
    Private Sub ChkVac()
        Try
            If Convert.ToString(Request.QueryString("vacid")).Trim <> "" Then
                ddl_Vacancy.Items.FindByValue(Convert.ToString(Request.QueryString("vacid"))).Selected = True
                ddl_Vacancy.Enabled = False
            End If
        Catch ex As Exception
            Session("error") = ex.Message.ToString
            RadWindowManager1.RadAlert(ex.Message, 300, 200, "Exception", "")
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
        
        qry = "select * from AMS.ADV_MASTER where advid='" & Request.QueryString("aid") & "'"
            dt = clas.getdata(qry, "QR")

        If dt.Rows.Count > 0 Then
            Button1.Text = "Update"
            ctr_txtexpense.Text = Convert.ToString(dt.Rows(0)("ADVEXPENSE"))
            TextBox10.Text = Convert.ToString(dt.Rows(0)("ADVDESC"))


            'TextBox8.Text = Convert.ToString(dt.Rows(0)("empcmpfax"))
            'TextBox9.Text = Convert.ToString(dt.Rows(0)("empcmpweb"))
            'TextBox10.Text = Convert.ToString(dt.Rows(0)("empcmpadd"))
            'TextBox11.Text = Convert.ToString(dt.Rows(0)("empeml"))

            If IsDBNull(dt.Rows(0)("ADVAPPBY")) = False Then
                ctr_dtAppBy.Items.FindByValue(Convert.ToString(dt.Rows(0)("ADVAPPBY"))).Selected = True

            End If
            If IsDBNull(dt.Rows(0)("ADVPRVID")) = False Then
                ctr_dpProv.Items.FindByValue(Convert.ToString(dt.Rows(0)("ADVPRVID"))).Selected = True
            End If




                If IsDBNull(dt.Rows(0)("ADVAPPDDT")) = False Then

                    ctr_appDt.MinDate = Date.Now.AddYears(-5)
                    ctr_appDt.MaxDate = Date.Now.AddYears(5)

                    ctr_appDt.SelectedDate = dt.Rows(0)("ADVAPPDDT")
                    ctr_appDt.Enabled = False
                End If

            If IsDBNull(dt.Rows(0)("advdate")) = False Then
                    ctr_dtadv.MinDate = Date.Now.AddYears(-5)
                    ctr_dtadv.MaxDate = Date.Now.AddYears(5)
                    ctr_dtadv.SelectedDate = dt.Rows(0)("advdate")
                    ctr_dtadv.Enabled = False
                End If
                'ctr_dtadv.Enabled = False

                If IsDBNull(dt.Rows(0)("CURRCODE")) = False Then
                    ddl_Curr.Items.FindByValue(Convert.ToString(dt.Rows(0)("CURRCODE"))).Selected = True
                End If

                If IsDBNull(dt.Rows(0)("VAC_ID")) = False Then
                    ddl_Vacancy.Items.FindByValue(Convert.ToString(dt.Rows(0)("VAC_ID"))).Selected = True
                    ddl_Vacancy.Enabled = False
                End If


                lbl_Fname.Visible = True
                lbl_Fname.Text = Convert.ToString(dt.Rows(0)("advimage"))
                'fupl.Attributes.CssStyle.Add("color", "transparent;")
                fupl.Attributes.CssStyle.Add("width", "120px;")

            'RadWindowManager1.RadAlert("<ul><li><div style=color:red;> This FSN has already keyedin</div></li></ul>", 300, 150, "Validation Error", Nothing)
            'Exit Sub
                Session("mode2") = "edit"
        Else
            Button1.Enabled = False

            ImageButton1.Enabled = False
            ImageButton1.ToolTip = " Invalid Record ID for Edit"
            End If
        Catch ex As Exception
            Session("error") = ex.Message.ToString
            RadWindowManager1.RadAlert(ex.Message, 300, 200, "Exception", "")
            Session("mode") = "edit"
            'Dim AlertMessage As String = "alert('" & ex.Message.ToString & "')"
            'ScriptManager.RegisterStartupScript(Page, Page.GetType, "AlertMessage", AlertMessage, True)

        End Try

    End Sub
   


    Private Sub fillby()
        Try
            'qry = "select agntname,agntid from ams.ams_agents a left join ams.ams_usermaster b on a.agntid=b.id and b.lgnroll='S'"
            qry = "select Empno,Name from TT.EMPLOYEES where desgid=(select desgid from ranu.desgmaster where desiglevel=1) and active='T' "

            dt = clas.getdata(qry, "QR")
            ctr_dtAppBy.DataTextField = "Name"
            ctr_dtAppBy.DataValueField = "Empno"

            ctr_dtAppBy.DataSource = dt
            ctr_dtAppBy.DataBind()

            ctr_dtAppBy.Items.Insert(0, New ListItem("Choose Approved by", ""))

        Catch ex As Exception
            Session("error") = ex.Message.ToString
            RadWindowManager1.RadAlert(ex.Message, 300, 200, "Exception", "")
            'Dim AlertMessage As String = "alert('" & ex.Message.ToString & "')"
            'ScriptManager.RegisterStartupScript(Page, Page.GetType, "AlertMessage", AlertMessage, True)
        End Try

    End Sub


    Private Sub fillVac()
        Try
            'qry = "select agntname,agntid from ams.ams_agents a left join ams.ams_usermaster b on a.agntid=b.id and b.lgnroll='S'"
            qry = "select vacid,vacName from AMS.ADV_Vacancy where status=60"

            dt = clas.getdata(qry, "QR")
            ddl_Vacancy.DataTextField = "vacName"
            ddl_Vacancy.DataValueField = "vacid"

            ddl_Vacancy.DataSource = dt
            ddl_Vacancy.DataBind()

            ddl_Vacancy.Items.Insert(0, New ListItem("Choose Vacancy", ""))

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
            ddl_Curr.DataTextField = "descr"
            ddl_Curr.DataValueField = "currcode"

            ddl_Curr.DataSource = dt
            ddl_Curr.DataBind()

            ddl_Curr.Items.Insert(0, New ListItem("Choose Currency", ""))

        Catch ex As Exception
            Session("error") = ex.Message.ToString
            RadWindowManager1.RadAlert(ex.Message, 300, 200, "Exception", "")
            'Dim AlertMessage As String = "alert('" & ex.Message.ToString & "')"
            'ScriptManager.RegisterStartupScript(Page, Page.GetType, "AlertMessage", AlertMessage, True)
        End Try

    End Sub

    Private Sub fillpro()
        Try
            qry = "SELECT TRANSID, NAME FROM TT.ASSFORCOUNTRY  where JoSCP in (1,0) And Active='A' Order By Name Asc"

            dt = clas.getdata(qry, "QR")
            ctr_dpProv.DataTextField = "NAME"
            ctr_dpProv.DataValueField = "TRANSID"
            ctr_dpProv.DataSource = dt
            ctr_dpProv.DataBind()
            ctr_dpProv.Items.Insert(0, New ListItem("Choose Province", ""))
        Catch ex As Exception
            Session("error") = ex.Message.ToString
            RadWindowManager1.RadAlert(ex.Message, 300, 200, "Exception", "")
            'Dim AlertMessage As String = "alert('" & ex.Message.ToString & "')"
            'ScriptManager.RegisterStartupScript(Page, Page.GetType, "AlertMessage", AlertMessage, True)
        End Try
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
        Try
            Dim fileName As String
        Dim flag As Boolean

        If Button1.Text = "Update" Then

            flag = updateCommand()
            If flag = True Then
                RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Advertisement Information Updated Successfully </div></li></ul>", 300, 150, "Validation Success", Nothing)
            End If

        Else


            Dim str As StringBuilder = New StringBuilder

            If fupl.HasFile = True Then
                fileName = Path.GetFileName(fupl.PostedFile.FileName).ToLower()
                If fileName = "" Then
                    ' RadWindowManager1.RadAlert("<div style=color:green;> Please browse your required file. </div>", 300, 150, "Validation Success", Nothing)
                    'Exit Sub

                Else

                    If fileName.Contains("%") Or fileName.Contains("'") Or fileName.Contains("""") Or fileName.Contains("&") Then
                        RadWindowManager1.RadAlert("<div style=color:red;> Invalid File Name. </div>", 300, 150, "Validation Success", Nothing)
                        Exit Sub
                    End If

                    fileName = Replace(fileName.Replace("""", "").Replace("%", "").Replace("&", "").Replace("+", ""), "'", "")

                    Dim actfileName As String = fileName

                    Dim ext As String = Path.GetExtension(fupl.PostedFile.FileName)
                    '  Dim ext As String = Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower()
                    Dim fileSize As Long = fupl.PostedFile.ContentLength
                    If fileSize < 3145728 Then
                        If ext.ToLower.Contains(".jpg") Or ext.ToLower.Contains(".jpeg") Or ext.ToLower.Contains(".png") Then
                            fileName = fileName.Replace(" ", "_")

                            Dim FileNM1 As String() = fileName.Split(".")
                            Dim datestr As String = String.Format("{0:ddMMMyyyyHmmss}", DateTime.Now)
                            fileName = FileNM1(0) & datestr & ext

                            Dim Path As String = (Server.MapPath("Advertisement/") + fileName)
                            fupl.PostedFile.SaveAs(Path)

                            Dim client_ip As String = Request.UserHostAddress()
                            Dim Browser_Name As String = Request.Browser.Browser.ToString & " " & Request.Browser.Version.ToString

                        Else
                            RadWindowManager1.RadAlert("<div style=color:Red;> Please upload Advertiement in jpg/png format. </div>", 300, 150, "Validation Error", Nothing)
                            Exit Sub
                        End If

                    Else
                        RadWindowManager1.RadAlert("<div style=color:Red;> Document should not contain file size more than 3 MB. </div>", 300, 150, "Validation Error", Nothing)
                        Exit Sub
                    End If
                End If

            Else
                'RadWindowManager1.RadAlert("<div style=color:Red;> Please Choose File..... </div>", 300, 150, "Validation Error", Nothing)
                'Exit Sub
            End If


            str.Append("insert into AMS.ADV_MASTER(ADVID,")

            If Not ctr_dtadv.SelectedDate Is Nothing Then
                If (ctr_dtadv.SelectedDate.ToString.Length >= 1) Then
                    str.Append("ADVDATE,")
                End If
            End If
            If TextBox10.Text <> "" Then
                str.Append("ADVDESC,")
            End If
            If Not ctr_appDt.SelectedDate Is Nothing Then
                If (ctr_appDt.SelectedDate.ToString.Length >= 1) Then
                    str.Append("ADVAPPDDT,")
                End If
            End If
            If ctr_dtAppBy.SelectedValue <> "" Then
                str.Append("ADVAPPBY,")
            End If
            If ctr_dpProv.SelectedValue <> "" Then
                str.Append("ADVPRVID,")
            End If
            If ctr_txtexpense.Text.Trim <> "" Then
                str.Append("ADVEXPENSE,")
            End If
            If fupl.HasFile Then
                str.Append("ADVIMAGE,")
            End If
                If ddl_Curr.SelectedValue <> "" Then
                    str.Append("CURRCODE,")
                End If
                If ddl_Vacancy.SelectedValue <> "" Then
                    str.Append("VAC_ID,")
                End If

            str.Append("KEYEDON, KEYEDBY, KEYEDIP) ")

            str.Append(" values ( AMS.ADV_MASTER_seq.nextval,")


            If Not ctr_dtadv.SelectedDate Is Nothing Then
                If (ctr_dtadv.SelectedDate.ToString.Length >= 1) Then
                    str.Append("'" & CDate(ctr_dtadv.SelectedDate).ToString("dd-MMM-yyyy") & "',")
                End If
            End If
                If TextBox10.Text <> "" Then
                    str.Append("'" & TextBox10.Text.Trim & "',")
                End If
            If Not ctr_appDt.SelectedDate Is Nothing Then
                If (ctr_appDt.SelectedDate.ToString.Length >= 1) Then
                    str.Append("'" & CDate(ctr_appDt.SelectedDate).ToString("dd-MMM-yyyy") & "',")
                End If
            End If
            If ctr_dtAppBy.SelectedValue <> "" Then
                str.Append("'" & ctr_dtAppBy.SelectedValue & "',")
            End If
            If ctr_dpProv.SelectedValue <> "" Then
                str.Append("'" & ctr_dpProv.SelectedValue & "',")
            End If
            'If ctr_txtAdvT.Text.Trim <> "" Then
            '    str.Append("'" & ctr_txtAdvT.Text & "',")
            'End If
            If ctr_txtexpense.Text.Trim <> "" Then
                str.Append("'" & ctr_txtexpense.Text.Trim & "',")
            End If
            If fupl.HasFile Then
                str.Append("'" & fileName.Trim & "',")
                End If
                If ddl_Curr.SelectedValue <> "" Then
                    str.Append("'" & ddl_Curr.SelectedValue & "',")                   
                End If
                If ddl_Vacancy.SelectedValue <> "" Then
                    str.Append("'" & ddl_Vacancy.SelectedValue & "',")
                End If

            str.Append("sysdate," & agntid & ",'" & HttpContext.Current.Request.UserHostAddress & "' )")

            If str.ToString().Length > 0 Then
                Dim i = clas.ExecuteNonQuery(str.ToString())
                If i = 1 Then
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
                ' Response.Write(qry)

            Else
                RadWindowManager1.RadAlert("<ul><li><div style=color:red;>Some Malware Function issue</div></li></ul>", 300, 100, "Validation Exception Failure", Nothing)
            End If


            Exit Sub
        End If

        Catch ex As Exception
            Session("error") = ex.Message.ToString
            RadWindowManager1.RadAlert(ex.Message, 300, 200, "Exception", "")
            'Dim AlertMessage As String = "alert('" & ex.Message.ToString & "')"
            'ScriptManager.RegisterStartupScript(Page, Page.GetType, "AlertMessage", AlertMessage, True)

        End Try


    End Sub




    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        clear()
        ' getmastdesg()
        ' getmastindustry()
    End Sub


    Public Function updateCommand() As Boolean
        Try
            'Dim transid As Int32 = getMaxID()
            'Dim str2 As String = "insert into tt.ukdata(transid, InDocRecdDt,InDocRecdBy, InDocStat) values(2, to_date(@InDocRecdDt,'dd-MON-yyyy'), @InDocRecdBy, @InDocStat)"
            Dim filename As String
            Dim str As StringBuilder = New StringBuilder
            str.Append("update  AMS.ADV_MASTER set ")


            If Not ctr_dtadv.SelectedDate Is Nothing Then
                If (ctr_dtadv.SelectedDate.ToString.Length >= 1) Then
                    str.Append("ADVDATE='" & CDate(ctr_dtadv.SelectedDate).ToString("dd-MMM-yyyy") & "',")
                End If
            End If

            If TextBox10.Text <> "" Then
                str.Append("ADVDESC='" & TextBox10.Text.Trim & "',")
            End If

            If Not ctr_appDt.SelectedDate Is Nothing Then
                If (ctr_appDt.SelectedDate.ToString.Length >= 1) Then
                    str.Append("ADVAPPDDT='" & CDate(ctr_appDt.SelectedDate).ToString("dd-MMM-yyyy") & "',")
                End If
            End If
            If ctr_dtAppBy.SelectedValue <> "" Then
                str.Append("ADVAPPBY='" & ctr_dtAppBy.SelectedValue & "',")
            End If
            If ctr_dpProv.SelectedValue <> "" Then
                str.Append("ADVPRVID='" & ctr_dpProv.SelectedValue & "',")
            End If

            If ctr_txtexpense.Text.Trim <> "" Then
                str.Append("ADVEXPENSE='" & ctr_txtexpense.Text.Trim & "',")
            End If


            If fupl.HasFile = True Then

                fileName = Path.GetFileName(fupl.PostedFile.FileName).ToLower()


                If fileName = "" Then
                    ' RadWindowManager1.RadAlert("<div style=color:green;> Please browse your required file. </div>", 300, 150, "Validation Success", Nothing)

                    ' Exit Function

                Else

                    If fileName.Contains("%") Or fileName.Contains("'") Or fileName.Contains("""") Or fileName.Contains("&") Then
                        RadWindowManager1.RadAlert("<div style=color:red;> Invalid File Name. </div>", 300, 150, "Validation Success", Nothing)
                        Exit Function
                    End If

                    fileName = Replace(fileName.Replace("""", "").Replace("%", "").Replace("&", "").Replace("+", ""), "'", "")

                    Dim actfileName As String = fileName

                    Dim ext As String = Path.GetExtension(fupl.PostedFile.FileName)
                    '  Dim ext As String = Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower()
                    Dim fileSize As Long = fupl.PostedFile.ContentLength
                    If fileSize < 3145728 Then
                        If ext.ToLower.Contains(".jpg") Or ext.ToLower.Contains(".jpeg") Or ext.ToLower.Contains(".png") Then
                            fileName = fileName.Replace(" ", "_")

                            Dim FileNM1 As String() = fileName.Split(".")
                            Dim datestr As String = String.Format("{0:ddMMMyyyyHmmss}", DateTime.Now)
                            fileName = FileNM1(0) & datestr & ext

                            Dim Path As String = (Server.MapPath("Advertisement/") + fileName)
                            fupl.PostedFile.SaveAs(Path)

                            Dim client_ip As String = Request.UserHostAddress()
                            Dim Browser_Name As String = Request.Browser.Browser.ToString & " " & Request.Browser.Version.ToString

                        Else
                            RadWindowManager1.RadAlert("<div style=color:Red;> Please upload Advertiement in jpg/png format. </div>", 300, 150, "Validation Error", Nothing)
                            Exit Function
                        End If

                    Else
                        RadWindowManager1.RadAlert("<div style=color:Red;> Document should not contain file size more than 3 MB. </div>", 300, 150, "Validation Error", Nothing)
                        Exit Function
                    End If
                End If

            Else
                'RadWindowManager1.RadAlert("<div style=color:Red;> Please Choose File..... </div>", 300, 150, "Validation Error", Nothing)
                'Exit Function
            End If

            If fupl.HasFile Then
                str.Append("ADVIMage='" & filename & "',")
            End If
            If ddl_Curr.SelectedValue <> "" Then
                str.Append("CURRCODE='" & ddl_Curr.SelectedValue & "',")                
            End If
            If ddl_Vacancy.SelectedValue <> "" Then
                str.Append("VAC_ID='" & ddl_Vacancy.SelectedValue & "',")
            End If

            Dim temp As String = str.ToString
            str.Clear()
            str.Append(temp.Substring(0, temp.Length - 1))
            str.Append(",LASTUPDON=sysdate,LASTUPDBY='" & agntid & "' where ADVID='" & Request.QueryString("aid") & "'")

            If str.ToString().Length > 0 Then

                Dim i = clas.ExecuteNonQuery(str.ToString())
                If i = 1 Then
                    Return True
                End If
            End If


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
        ctr_dpProv.ClearSelection()
        ctr_txtexpense.Text = String.Empty
        ctr_dtAppBy.ClearSelection()
        ddl_Curr.ClearSelection()
        ddl_Vacancy.ClearSelection()
        ctr_dtadv.Clear()
        ctr_appDt.Clear()
        binddates(ctr_dtadv, Date.Today.ToString("dd-MMM-yyyy"), Date.Today.ToString("dd-MMM-yyyy"), Date.Today.AddMonths(1).ToString("dd-MMM-yyyy"), True)
        binddates(ctr_appDt, "", Date.Today.AddMonths(-3).ToString("dd-MMM-yyyy"), Date.Today.AddDays(-1).ToString("dd-MMM-yyyy"), True)
        '  TextBox1.Text = String.Empty
    End Sub

End Class
