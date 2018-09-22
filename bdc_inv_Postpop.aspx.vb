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
            'If Request.QueryString("frm") IsNot Nothing And Not Request.QueryString("frm") = "" Then
            'Session("mode") = ""
            'End If
            Session("mode") = ""
            fillBy()
            fillstatus()
            fillPF()
            filldt()
            getlbl()
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
    Private Sub getlbl()
        Try

        
        qry = "select (select vacname from ams.adv_vacancy where vacid=vac_id) vacancy ,substr(advdesc,1,30) adv from ams.adv_master where advid='" & Request.QueryString("aid") & "'"
        dt = clas.getdata(qry, "QR")

        If dt.Rows.Count > 0 Then
            lblVac.Text = dt(0)("vacancy")
            lblAdv.Text = dt(0)("adv")
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub getrcdforedit()
        Try
            qry = "select * from AMS.ADV_DETAILS where TRANSID='" & Request.QueryString("transid") & "'"
            dt = clas.getdata(qry, "QR")
            lbl1.Visible = False
            lbl2.Visible = False
            If dt.Rows.Count > 0 Then
                Button1.Text = "Update"
                ctr_txtAdvT.Text = Convert.ToString(dt.Rows(0)("ADVTXT"))

                ' TextBox10.Text = Convert.ToString(dt.Rows(0)("ADVDESC"))


                'TextBox8.Text = Convert.ToString(dt.Rows(0)("empcmpfax"))
                'TextBox9.Text = Convert.ToString(dt.Rows(0)("empcmpweb"))
                'TextBox10.Text = Convert.ToString(dt.Rows(0)("empcmpadd"))
                'TextBox11.Text = Convert.ToString(dt.Rows(0)("empeml"))

                If IsDBNull(dt.Rows(0)("PLTFRMID")) = False Then
                    ctr_Pf.Items.FindByValue(Convert.ToString(dt.Rows(0)("PLTFRMID"))).Selected = True

                End If
                ctr_Pf.Enabled = False
                If IsDBNull(dt.Rows(0)("POSTBY")) = False Then
                    ctr_pstBy.Items.FindByValue(Convert.ToString(dt.Rows(0)("POSTBY"))).Selected = True
                End If
                If IsDBNull(dt.Rows(0)("POSTSTS")) = False Then
                    ctr_pstStatus.Items.FindByValue(Convert.ToString(dt.Rows(0)("POSTSTS"))).Selected = True
                End If



                If IsDBNull(dt.Rows(0)("POSTDT")) = False Then
                    ctr_postDt.MinDate = Date.Now.AddYears(-5)
                    ctr_postDt.MaxDate = Date.Now.AddYears(5)
                    ctr_postDt.SelectedDate = dt.Rows(0)("POSTDT")
                    ctr_postDt.Enabled = False
                End If
                lbl_Fname.Visible = True
                lbl_Fname.Text = Convert.ToString(dt.Rows(0)("advimg"))
                fupl.Attributes.CssStyle.Add("width", "90px;")

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
            Session("mode") = "edit"
            RadWindowManager1.RadAlert(ex.Message, 300, 200, "Exception", "")
            'Dim AlertMessage As String = "alert('" & ex.Message.ToString & "')"
            'ScriptManager.RegisterStartupScript(Page, Page.GetType, "AlertMessage", AlertMessage, True)

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

    Private Sub filldt()
        qry = "select ADVDATE,advid from AMS.ADV_MASTER where advid='" & Request.QueryString("aid") & "'"
        dt = clas.getdata(qry, "QR")
        If dt.Rows.Count > 0 Then
            binddates(ctr_postDt, "", Convert.ToDateTime(dt.Rows(0)("ADVDATE")).ToString("dd-MMM-yyyy"), Date.Now.AddMonths(2).ToString("dd-MMM-yyyy"), True)
        Else
            binddates(ctr_postDt, "", "", "", False)
        End If
    End Sub

    Private Sub fillby()
        'qry = "select agntname,agntid from ams.ams_agents a left join ams.ams_usermaster b on a.agntid=b.id and b.lgnroll='S'"
        'qry = "select empno,name from TT.EMPLOYEES  where deptcode in (5,69) and Active='T'"
        qry = "select empno,name || ','|| empdesignation || '('|| Department || ')' as name from TT.EMPLOYEES  where deptcode in (5,69) and Active='T' order by Department,name"
        dt = clas.getdata(qry, "QR")
        ctr_pstBy.DataTextField = "name"
        ctr_pstBy.DataValueField = "empno"
        ctr_pstBy.DataSource = dt
        ctr_pstBy.DataBind()

    End Sub
    Private Sub fillstatus()
        qry = "SELECT STATID, STATDESCR FROM AMS.AMS_STATMASTER where statstatus='A' and statscp=3"
        dt = clas.getdata(qry, "QR")
        ctr_pstStatus.DataTextField = "STATDESCR"
        ctr_pstStatus.DataValueField = "STATID"
        ctr_pstStatus.DataSource = dt
        ctr_pstStatus.DataBind()
    End Sub

    Private Sub fillPF()
        qry = " SELECT STATID, STATDESCR FROM AMS.AMS_STATMASTER where statstatus='A' and statscp=100 "
        dt = clas.getdata(qry, "QR")
        ctr_Pf.DataTextField = "STATDESCR"
        ctr_Pf.DataValueField = "STATID"
        ctr_Pf.DataSource = dt
        ctr_Pf.DataBind()
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
                    RadWindowManager1.RadAlert("<div style=color:green;> Please browse your required file. </div>", 300, 150, "Validation Success", Nothing)
                    Exit Sub

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
                RadWindowManager1.RadAlert("<div style=color:Red;> Please Choose File..... </div>", 300, 150, "Validation Error", Nothing)
                Exit Sub
            End If


            str.Append("insert into  AMS.ADV_DETAILS(TRANSID,ADVID,")

            'AMS.ADV_DETAILS_SEQ.Nextval, AMS.ADV_MASTER_seq.currval


            If ctr_Pf.SelectedValue <> "" Then
                str.Append("PLTFRMID,")
            End If

            If Not ctr_postDt.SelectedDate Is Nothing Then
                If (ctr_postDt.SelectedDate.ToString.Length >= 1) Then

                    str.Append("POSTDT,")
                    '  str.Append("'" & CDate(ctr_postDt.SelectedDate).ToString("dd-MMM-yyyy") & "',")
                End If
            End If
            If ctr_pstBy.SelectedValue <> "" Then
                str.Append("POSTBY,")
            End If
            If ctr_pstStatus.SelectedValue <> "" Then
                str.Append("POSTSTS,")
            End If

            If fupl.HasFile Then
                str.Append("ADVIMG,")
            End If

            If ctr_txtAdvT.Text <> "" Then
                str.Append("ADVTXT, ")
            End If
            str.Append("KEYEDON, KEYEDBY, KEYEDIP) values ( ams.ADV_MASTER_SEQ.nextval,")

            str.Append("'" & Request.QueryString("aid").ToString & "',")

            If ctr_Pf.SelectedValue <> "" Then
                str.Append("'" & ctr_Pf.SelectedValue & "' ,")
            End If


            If Not ctr_postDt.SelectedDate Is Nothing Then
                If (ctr_postDt.SelectedDate.ToString.Length >= 1) Then
                    str.Append("'" & CDate(ctr_postDt.SelectedDate).ToString("dd-MMM-yyyy") & "',")                   
                End If
            End If
            If ctr_pstBy.SelectedValue <> "" Then
                str.Append("'" & ctr_pstBy.SelectedValue & "',")
            End If
            If ctr_pstStatus.SelectedValue <> "" Then
                str.Append("'" & ctr_pstStatus.SelectedValue & "',")
            End If

            If fupl.HasFile Then
                str.Append("'" & fileName.Trim & "',")
            End If

            If ctr_txtAdvT.Text <> "" Then
                str.Append("'" & ctr_txtAdvT.Text & "',")
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




    End Sub

   


    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        clear()
        ' getmastdesg()
        ' getmastindustry()
    End Sub
   

    

    Protected Function Chk_Data_Existance(ByVal AgentId As String, ByVal EmpId As String) As Boolean
        Dim Flag As Boolean = False
        Dim Chk_Qry As String = "Select * from Ams.Ams_Emp_Job_Ans where  EMPID='" & EmpId & "'"
        Dim _D2 As DataTable = clas.getdata(Chk_Qry, "QR")
        If _D2.Rows.Count > 0 Then
            Flag = True
        End If
        Return Flag
    End Function




    Public Function updateCommand() As Boolean
        Try
            Dim fileName As String
            'Dim transid As Int32 = getMaxID()
            'Dim str2 As String = "insert into tt.ukdata(transid, InDocRecdDt,InDocRecdBy, InDocStat) values(2, to_date(@InDocRecdDt,'dd-MON-yyyy'), @InDocRecdBy, @InDocStat)"
            Dim str As StringBuilder = New StringBuilder
            str.Append("update  AMS.ADV_DETAILS set ")

            If ctr_Pf.SelectedValue <> "" Then
                str.Append("PLTFRMID='" & ctr_Pf.SelectedValue & "',")
            End If


            If Not ctr_postDt.SelectedDate Is Nothing Then
                If (ctr_postDt.SelectedDate.ToString.Length >= 1) Then
                    str.Append("POSTDT='" & CDate(ctr_postDt.SelectedDate).ToString("dd-MMM-yyyy") & "',")
                End If
            End If

            If ctr_pstBy.SelectedValue <> "" Then
                str.Append("POSTBY='" & ctr_pstBy.SelectedValue & "',")
            End If
            If ctr_pstStatus.SelectedValue <> "" Then
                str.Append("POSTSTS='" & ctr_pstStatus.SelectedValue & "',")
            End If


            If ctr_txtAdvT.Text <> "" Then
                str.Append("ADVTXT='" & ctr_txtAdvT.Text.Trim & "',")
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
                str.Append("ADVIMG='" & fileName & "',")
            End If

            Dim temp As String = str.ToString
            str.Clear()
            str.Append(temp.Substring(0, temp.Length - 1))
            str.Append(",LASTUPDON=sysdate,LASTUPDBY='" & agntid & "' where TRANSID='" & Request.QueryString("transid") & "'")

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


   

    Private Sub clear()
        ctr_Pf.ClearSelection()
        ctr_pstBy.ClearSelection()
        ctr_txtAdvT.Text = String.Empty
        ctr_pstStatus.ClearSelection()
        ctr_postDt.Clear()
        '  TextBox1.Text = String.Empty

        'TextBox4.Text = String.Empty

        'TextBox10.Text = String.Empty

        ''DropDownList1.ClearSelection()
        ''DropDownList1.DataBind()
        'DropDownList3.ClearSelection()
        'DropDownList3.DataBind()

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

    'Protected Sub btnGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGo.Click

    'If TextBox1.Text.Trim().Length = 0 Then
    '    RadWindowManager1.RadAlert("<ul><li><div style=color:red;> Invalid FSN</div></li></ul>", 300, 150, "Validation Error", Nothing)
    '    Exit Sub
    'End If

    'dt = clas.getdata("SELECT * FROM RAHUL.APPCLNTVIEW WHERE IMMCLASS IN (SELECT IMMCLASS FROM TT.IMMCLASS_MASTER WHERE JOSCP=1) and fileserialno='" & TextBox1.Text.Trim() & "'", "TX")


    'If dt.Rows.Count > 0 Then

    '    name.InnerText = Convert.ToString(dt.Rows(0)("appname"))
    '    fno.InnerText = Convert.ToString(dt.Rows(0)("fileno"))
    '    rtdate.InnerText = Convert.ToString(dt.Rows(0)("retaindate"))
    '    immclass.InnerText = Convert.ToString(dt.Rows(0)("descr"))
    '    brcnh.InnerText = Convert.ToString(dt.Rows(0)("DLBRANCH"))
    '    cnt.InnerText = Convert.ToString(dt.Rows(0)("forcountry"))
    '    status.InnerText = Convert.ToString(dt.Rows(0)("statdesc"))
    '    ImageButton1.Enabled = True
    '    getrcdforedit()

    'Else
    '    RadWindowManager1.RadAlert("<ul><li><div style=color:red;> This FSN does not belongs to the Job Order Category</div></li></ul>", 300, 150, "Validation Error", Nothing)
    '    Exit Sub
    'End If


    ' End Sub

    'Protected Sub DropDownList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownList1.SelectedIndexChanged

    '    ' getmastres(DropDownList1.SelectedValue)

    'End Sub
End Class
