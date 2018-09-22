Imports System.Data

Imports System.Web.UI.WebControls
Imports System.IO
Imports Telerik.Web.UI
Imports System.Text

Imports System.Environment


Partial Class default2
    Inherits System.Web.UI.Page
    Dim clas As rahulcpm.commonclass = New rahulcpm.commonclass()
    Dim Flag As Boolean = False
    Dim Flag_Done As Boolean = False
    Dim SqlQuery As String
    Dim _DataTable As DataTable
    Dim EID As Integer = 0
    Dim JID As Integer = 0
    Dim AID As Integer = 0

  

    Private Sub Bind_Agent()
        Try
            Dim qry_str As String
            'If Session("lgnroll") = "S" Then
            qry_str = "select a.agntid as  agnt_Id, a.agntname as agnt_Name from ams.ams_agents a, ams.ams_usermaster b where b.lgnagntid=a.agntid " 'and b.lgnsts='A'
            'End If
            Dim dt_2 As DataTable = clas.getdata(qry_str, "TX")
            If dt_2.Rows.Count > 0 Then
                '  Dim ddl_BDC As DropDownList = DirectCast(RadGrid1.FindControl("ddl_BDC"), DropDownList)
                ddl_Agent.DataTextField = "agnt_Name"
                ddl_Agent.DataValueField = "agnt_Id"
                ddl_Agent.DataSource = dt_2
                ddl_Agent.DataBind()
            End If
            ddl_Agent.Items.Insert(0, New ListItem("Show All", ""))
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

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session("lgnagntid") = "0" Or Session("lgnagntid") = "" Then
            RadWindowManager1.RadAlert("session is expired, Please login Again", 300, 200, "Session Expired Error", "")
            Response.Redirect("logout.aspx")
        End If
        If Request.QueryString("mode") = "edit" Then
            If Request.QueryString("tid") IsNot Nothing And Not Request.QueryString("tid") = "" Then
                JID = Get_JobId(Request.QueryString("tid"))
            Else
                JID = 0
            End If
            Dim Ids As String() = Get_EmpId()
            EID = Ids(0)
            AID = Ids(1)
        Else
            If Request.QueryString("Jid") IsNot Nothing And Not Request.QueryString("Jid") = "" Then
                JID = Request.QueryString("Jid")
                Dim Ids As String() = Get_EmpId()
                EID = Ids(0)
                AID = Ids(1)
            End If
        End If

        If Page.IsPostBack = False Then
            Int_Date_Field(JID)
            Bind_Agent()

            If Request.QueryString("File") = "ACand" Then
                ddl_Agent.SelectedValue = AID
                Bind_Employer(AID)
                ddl_Employer.SelectedValue = EID
                Bind_JobPosition(EID)
                ddl_Position.SelectedValue = JID
                ddl_Agent.Enabled = False
                ddl_Employer.Enabled = False
                ddl_Position.Enabled = False

            ElseIf Request.QueryString("File") = "Switch" Then
                ddl_Agent.SelectedValue = AID
                Bind_Employer(AID)
                ddl_Employer.SelectedValue = EID
                Bind_JobPosition(EID)
                ddl_Position.SelectedValue = JID
                ddl_Agent.Enabled = True
                ddl_Employer.Enabled = True
                ddl_Position.Enabled = True

            Else
                ddl_Agent.SelectedValue = AID
                Bind_Employer(AID)
                ddl_Employer.SelectedValue = EID
                Bind_JobPosition(EID)
                ddl_Position.SelectedValue = JID
                ddl_Agent.Enabled = True
                ddl_Employer.Enabled = True
                ddl_Position.Enabled = True

            End If

            If Request.QueryString("mode") = "edit" Then
                If Request.QueryString("tid") IsNot Nothing And Not Request.QueryString("tid") = "" Then
                    Bind_Existing_Page(Request.QueryString("tid"))
                    If Request.QueryString("File") = "Switch" Then
                        ImgBtn_Save.AlternateText = "Switch"
                    End If
                End If
            End If
        End If

    End Sub

    Protected Sub Bind_Existing_Page(ByVal Transid As String)
        Try
            Dim Query_str1 As String = ""
            Query_str1 = " Select TRANSID,FSN,JOBID,ALOT_DT,INT_DT,INT_RESULT,FEEDBACK,EXPERIENCE,EDUCATION,"
            Query_str1 += " RESUME_PATH,STATUS,REASON,KEYEDON,KEYEDBY,PREV_KEYEDON,PREV_KEYEDBY,SWITCH_JOB,replace (RESUME_PATH, regexp_substr (RESUME_PATH, '.*\\')) file_name ,"
            Query_str1 += " Next_TransId,SWITCH_DT,SWITCH_BY,CAND_STATUS from ams.ams_candidate "
            Query_str1 += " where transid='" & Transid & "'"
            Dim DT_3 As DataTable = New DataTable
            DT_3 = clas.getdata(Query_str1, "TX")
            If DT_3.Rows.Count > 0 Then
                txt_FSN.Text = DT_3.Rows(0)("FSN").ToString
                Fill_FSNDet(DT_3.Rows(0)("FSN").ToString)
                txt_EDU.Text = DT_3.Rows(0)("EDUCATION").ToString
                txt_Exp.Text = DT_3.Rows(0)("EXPERIENCE").ToString
                txt_Feedback.Text = DT_3.Rows(0)("FEEDBACK").ToString

                lbl_ResumeFileName.Text = DT_3.Rows(0)("file_name").ToString
                lbl_FullPath.Text = (Server.MapPath("~/Resume/") + DT_3.Rows(0)("file_name").ToString)
                lbl_ResumeFileName.Visible = True
                ImgBtn_Save.AlternateText = "Update"
                txt_FSN.Enabled = False
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

    Protected Sub Int_Date_Field(ByVal JobId As String)
        Try
            Dim Str_qty As String = "Select Intid,to_char(to_date(int_date,'dd-Mon-RRRR'),'dd-Mon-YYYY') as Int_Dt from AMS.ams_int_shdul where intid=(Select max(Intid) from  AMS.ams_int_shdul where jobid='" & JobId & "')"
            Dim _DT_6 As DataTable = New DataTable
            _DT_6 = clas.getdata(Str_qty, "TX")
            If _DT_6.Rows.Count > 0 Then
                If IsDBNull(_DT_6.Rows(0)("Int_Dt")) = False Then
                    Interview_Date.SelectedDate = DateTime.Parse(_DT_6.Rows(0)("Int_Dt").ToString)
                Else
                    Interview_Date.Clear()
                    'RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Please schedule interview date before moving for cadidate selection process against this Job Order. </div></li></ul>", 300, 150, "Validation Success", Nothing)
                End If
            Else
                Interview_Date.Clear()
                'RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Please schedule interview date before moving for cadidate selection process against this Job Order. </div></li></ul>", 300, 150, "Validation Success", Nothing)
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

    Protected Function Get_EmpId() As String()
        Dim Ids(1) As String
        Dim QRY_STR As String = "Select * from AMS.AMS_JobSecured where JobSecuredId='" & JID & "'"
        Dim dt2 As DataTable = New DataTable
        dt2 = clas.getdata(QRY_STR, "TX")
        Ids(0) = dt2.Rows(0)("EmpId")
        Ids(1) = dt2.Rows(0)("AgentId")
        Return Ids
    End Function

    Protected Function Get_JobId(ByVal TransId As String) As String
        Dim Id As String
        Dim QRY_STR As String = "Select * from AMS.AMS_Candidate where TransId='" & TransId & "'"
        Dim dt2 As DataTable = New DataTable
        dt2 = clas.getdata(QRY_STR, "TX")
        Id = dt2.Rows(0)("Jobid")
        Return Id
    End Function

    Protected Sub ddl_Agent_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_Agent.SelectedIndexChanged
        If ddl_Agent.SelectedValue <> "" Then
            Bind_Employer(ddl_Agent.SelectedValue)
            Bind_JobPosition("")
        End If
    End Sub

    Private Sub Bind_Employer(ByVal Agentid As String)
        Try
            Dim qry_str As String
            'If Session("lgnroll") = "S" Then
            qry_str = "Select a.EmpId, a.EmpFName || ' ' || a.EmpMName || ' ' || a.EmpLName as Emp_Name from AMS.AMS_Employer a"
            qry_str += " , ams.ams_usermaster b where b.lgnagntid=a.AgentId " 'and b.lgnsts='A' 
            qry_str += " AND  a.EmpFName is not null and a.EmpFName not like '%Xxx%'  "
            qry_str += " and a.EmpFName not like '%Dummy%' and a.AgentId='" & Agentid & "'  Order BY a.EmpFName ASC,a.EmpMName ASC,a.EmpLName ASC"
            'End If
            Dim dt_3 As DataTable = clas.getdata(qry_str, "TX")
            If dt_3.Rows.Count > 0 Then
                '  Dim ddl_BDC As DropDownList = DirectCast(RadGrid1.FindControl("ddl_Employer"), DropDownList)
                ddl_Employer.DataTextField = "Emp_Name"
                ddl_Employer.DataValueField = "EmpId"
                ddl_Employer.DataSource = dt_3
                ddl_Employer.DataBind()
            Else
                ddl_Employer.DataTextField = "Emp_Name"
                ddl_Employer.DataValueField = "EmpId"
                ddl_Employer.DataSource = dt_3
                ddl_Employer.DataBind()
            End If
            ddl_Employer.Items.Insert(0, New ListItem("Show All", ""))
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

    Protected Sub ddl_Employer_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_Employer.SelectedIndexChanged
        If ddl_Employer.SelectedValue <> "" Then
            Bind_JobPosition(ddl_Employer.SelectedValue)
            If ddl_Position.SelectedValue <> "" Then
                Int_Date_Field(ddl_Position.SelectedValue)
            End If
        End If
    End Sub

    Private Sub Bind_JobPosition(ByVal EmpId As String)
        Try
            Dim qry_str As String
            qry_str = "Select JobSecuredId as JId ,JobPositionID as Descr from AMS.AMS_JobSecured where VacancyStatusId<>-1 and EmpId='" & EmpId & "'"
            Dim dt_4 As DataTable = clas.getdata(qry_str, "TX")
            If dt_4.Rows.Count > 0 Then
                '  Dim ddl_BDC As DropDownList = DirectCast(RadGrid1.FindControl("ddl_Employer"), DropDownList)
                ddl_Position.DataTextField = "Descr"
                ddl_Position.DataValueField = "JId"
                ddl_Position.DataSource = dt_4
                ddl_Position.DataBind()
            Else
                ddl_Position.DataTextField = "Descr"
                ddl_Position.DataValueField = "JId"
                ddl_Position.DataSource = dt_4
                ddl_Position.DataBind()
            End If
            ddl_Position.Items.Insert(0, New ListItem("Show All", ""))
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

    Protected Sub ddl_Position_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_Position.SelectedIndexChanged
        If ddl_Position.SelectedValue <> "" Then
            Int_Date_Field(ddl_Position.SelectedValue)
        End If
    End Sub

    Protected Sub Imgbtn_Get_Cand_Data_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles Imgbtn_Get_Cand_Data.Click


        If txt_FSN.Text <> "" Then
            Dim Chk_flag As Boolean = Chk_FSN_Existance_4_Revoked(txt_FSN.Text)
            If Chk_flag = False Then
                Fill_FSNDet(txt_FSN.Text)
            Else
                Clear_FSNDet()
                RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Candidate bearing FSN No : " & txt_FSN.Text & " already got revoked. </div></li></ul>", 300, 150, "Validation Success", Nothing)
            End If
        Else
            Clear_FSNDet()
            RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Please enter FSN to make search for candidate. </div></li></ul>", 300, 150, "Validation Success", Nothing)
        End If

    End Sub

    Protected Sub Fill_FSNDet(ByVal FSN As String)
        Dim Qry_str1 As String = String.Empty
        Qry_str1 = "Select cv.FileNo,cv.FileSerialNo,cv.Name,to_char(cv.AppDOB,'DD-Mon-YYYY') as AppDob,cv.IMMClass,ac.Name as Country"
        Qry_str1 += " from tt.ClientView cv ,tt.ASSForCountry ac where cv.ASSforCountry=ac.TransId and FileSerialNo='" & FSN & "'"
        Dim dt_5 As DataTable = clas.getdata(Qry_str1, "TX")
        If dt_5.Rows.Count > 0 Then
            lbl_Name.Text = dt_5.Rows(0)("Name")
            lbl_Category.Text = dt_5.Rows(0)("IMMClass")
            lbl_DOB.Text = dt_5.Rows(0)("AppDob")
            lbl_ImmClass.Text = dt_5.Rows(0)("IMMClass")
            lbl_Location.Text = dt_5.Rows(0)("Country")

        Else
            Clear_FSNDet()
            RadWindowManager1.RadAlert("<ul><li><div style=color:green;> This FSN does not exist in Z-Axis DB. </div></li></ul>", 300, 150, "Validation Success", Nothing)
        End If
    End Sub

    Protected Function Chk_FSN_Existance_4_Revoked(ByVal FSN As String) As Boolean
        Dim Chk_Flag As Boolean = False
        Dim Qry_str1 As String = String.Empty
        Qry_str1 = "Select * from ams.ams_candidate where FSN='" & FSN & "' and Int_result='5'"
        Dim dt_7 As DataTable = clas.getdata(Qry_str1, "TX")
        If dt_7.Rows.Count > 0 Then
            Chk_Flag = True
        End If
        Return Chk_Flag
    End Function

    Protected Sub Clear_FSNDet()
        lbl_Name.Text = String.Empty
        lbl_Category.Text = String.Empty
        lbl_DOB.Text = String.Empty
        lbl_ImmClass.Text = String.Empty
        lbl_Location.Text = String.Empty
    End Sub

    Protected Sub imgbtn_Upload_Resume_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_Upload_Resume.Click
        Upload_Resume()
    End Sub
    Protected Sub Upload_Resume()
        'Dim athdata As New DataTable()
        If Resume_Upload.HasFile = True Then
            Dim fileSize As Long = Resume_Upload.PostedFile.ContentLength
            Dim sum As Long = 0
            'If athdata.Rows.Count > 0 Then
            '    sum = Convert.ToInt32(athdata.Compute("SUM(size)", String.Empty))
            'End If

            Dim sumafteruplod As Long = sum + fileSize
            If sumafteruplod <= 1999000 Then



                Dim actfilenam As [String] = Resume_Upload.PostedFile.FileName

                If ((actfilenam.Contains(".doc")) Or (actfilenam.Contains(".pdf")) Or (actfilenam.Contains(".htm")) Or (actfilenam.Contains(".ppt")) Or (actfilenam.Contains(".txt"))) Then
                    Dim filename As [String] = DateTime.Now.ToString("ddmmyyyyhhmm") & "_" & Path.GetFileName(Resume_Upload.PostedFile.FileName.Replace(" ", "_"))
                    filename = filename.Replace("'", "''")
                    Resume_Upload.SaveAs(Server.MapPath("~/Resume/" + filename))
                    lbl_ResumeFileName.Text = filename
                    lbl_FullPath.Text = (Server.MapPath("~/Resume/") + filename)
                Else
                    RadWindowManager1.RadAlert("<ul><li><div style=color:green;> User has to upload resume (in FileFormat .doc/.pdf/.ppt/.htm/.txt). </div></li></ul>", 300, 150, "Validation Success", Nothing)
                End If

                'If (Not actfilenam.Contains(".msi")) AndAlso (Not actfilenam.Contains(".exe")) AndAlso (Not actfilenam.Contains(".dll")) AndAlso (Not actfilenam.Contains(".sln")) AndAlso (Not actfilenam.Contains(".ini")) AndAlso (Not actfilenam.Contains(".aspx")) AndAlso (Not actfilenam.Contains(".html")) AndAlso (Not actfilenam.Contains(".xlsx")) AndAlso (Not actfilenam.Contains(".xls")) Then
                '    Dim filename As [String] = DateTime.Now.ToString("ddmmyyyyhhmm") & "_" & Path.GetFileName(Resume_Upload.PostedFile.FileName.Replace(" ", "_"))
                '    Resume_Upload.SaveAs(Server.MapPath("~/Resume/" + filename))
                '    'Dim dr As DataRow = athdata.NewRow()
                '    'dr(0) = actfilenam
                '    'dr(1) = Server.MapPath("Resume/" + filename)
                '    'dr(2) = Resume_Upload.PostedFile.ContentType
                '    'dr(3) = fileSize
                '    'dr(4) = filename
                '    lbl_ResumeFileName.Text = filename
                '    lbl_FullPath.Text = (Server.MapPath("~/Resume/") + filename)
                '    'athdata.Rows.Add(dr)
                '    'ViewState("grddata") = athdata
                'Else
                '    RadWindowManager1.RadAlert("<ul><li><div style=color:green;> User has to upload resume (in FileFormat images/.doc/.pdf/.ppt). </div></li></ul>", 300, 150, "Validation Success", Nothing)
                'End If
            Else
                '    Label6.Text = "Mail size should not be more than 20 MB"
                '    Label6.CssClass = "errcls"
                'End If
                RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Resume should not contain file size more than 10 MB. </div></li></ul>", 300, 150, "Validation Success", Nothing)
            End If
        Else
            RadWindowManager1.RadAlert("<ul><li><div style=color:green;> User has to Browse resume (in FileFormat .doc/.pdf/.ppt/.htm/.txt) to upload. </div></li></ul>", 300, 150, "Validation Success", Nothing)
        End If
    End Sub

    Protected Sub View_Resume_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles View_Resume.Click
        If lbl_FullPath.Text <> "" Then
            Dim filePath As String = lbl_FullPath.Text

            Dim filename As String = filePath.Split("\").Last
            If filename.Contains(".doc") Then
                Response.ContentType = "application/vnd.ms-word"
            ElseIf filename.Contains(".pdf") Then
                Response.ContentType = "application/pdf"
            ElseIf filename.Contains(".ppt") Then
                Response.ContentType = "application/x-mspowerpoint"
            ElseIf filename.Contains(".txt") Then
                Response.ContentType = "text/plain"
            ElseIf filename.Contains(".htm") Then
                Response.ContentType = "text/HTML"
            ElseIf filename.Contains(".bmp") Then
                Response.ContentType = "image/bmp"
            ElseIf filename.Contains(".gif") Then
                Response.ContentType = "image/gif"
            ElseIf filename.Contains(".jpeg") Or filename.Contains(".jpg") Then
                Response.ContentType = "image/jpeg"
            ElseIf filename.Contains(".png") Then
                Response.ContentType = "image/png"
            ElseIf filename.Contains(".tif") Or filename.Contains(".tiff") Then
                Response.ContentType = "image/tiff"
            End If
            Dim FN As String = Path.GetFileName(filePath)
            If FN = "" Then
                RadWindowManager1.RadAlert("<ul><li><div style=color:green;> System didn't find Resume to view. </div></li></ul>", 300, 150, "Validation Success", Nothing)
                Exit Sub
            End If
            FN = FN.Replace(" ", "_")

            'Response.ContentType = ContentType
            Response.AppendHeader("Content-Disposition", ("attachment; filename=" + FN))
            If File.Exists(filePath) Then
                Response.WriteFile(filePath)
                Response.End()
            Else
                RadWindowManager1.RadAlert("<ul><li><div style=color:green;> System didn't find Resume to view. </div></li></ul>", 300, 150, "Validation Success", Nothing)
            End If


        Else
            RadWindowManager1.RadAlert("<ul><li><div style=color:green;> System didn't find Resume to view. </div></li></ul>", 300, 150, "Validation Success", Nothing)
        End If
    End Sub

    Protected Sub imgbtn_Delete_Resume_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_Delete_Resume.Click

        Dim TRANSID As String = Request.QueryString("tid")


        If TRANSID <> "" Then
            If lbl_ResumeFileName.Text <> "" Then
                Dim confirmValue As String = Request.Form("confirm_value")
                If confirmValue = "Yes" Then
                    Dim Flag As Boolean = False
                    Flag = DeleteResume_Command()
                    If Flag = True Then
                        lbl_ResumeFileName.Text = String.Empty
                        lbl_FullPath.Text = String.Empty
                        RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Existing Resume get deleted. </div></li></ul>", 300, 150, "Validation Success", Nothing)
                        'Else
                        '    RadWindowManager1.RadAlert("<ul><li><div style=color:green;> UnKnown Exception occured , Please Contact IT Department. </div></li></ul>", 300, 150, "Validation Success", Nothing)
                    End If
                    'Else
                    'RadWindowManager1.RadAlert("<ul><li><div style=color:green;> System didn't find Resume to delete. </div></li></ul>", 300, 150, "Validation Success", Nothing)
                End If
              
            End If
        Else
            lbl_ResumeFileName.Text = String.Empty
            lbl_FullPath.Text = String.Empty
            'RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Existing Resume get deleted. </div></li></ul>", 300, 150, "Validation Success", Nothing)
        End If


    End Sub

    Protected Function Check_FSN_Exist_For_SameJID() As Boolean
        Dim Chk_Result As Boolean = False
        Dim Str_query As String = "Select * from AMS.AMS_Candidate Where FSN='" & txt_FSN.Text.Trim & "' and JobId='" & JID & "' and Switch_Dt is null"
        Dim DT_Chk As DataTable = clas.getdata(Str_query, "TX")
        If DT_Chk.Rows.Count > 0 Then
            Chk_Result = True
        End If
        Return Chk_Result
    End Function

    Protected Sub ImgBtn_Save_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImgBtn_Save.Click

        Dim Chk_flag As Boolean = Chk_FSN_Existance_4_Revoked(txt_FSN.Text)
        If Chk_flag = True Then
            RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Candidate bearing FSN No : " & txt_FSN.Text & " already got revoked. </div></li></ul>", 300, 150, "Validation Success", Nothing)
            Exit Sub
        End If

        'If Interview_Date.SelectedDate IsNot Nothing Then
        Dim flag As Boolean = False
        If ImgBtn_Save.AlternateText = "Save" Then
            If lbl_Name.Text = "" Then  'And lbl_ImmClass.Text = ""
                RadWindowManager1.RadAlert("<ul><li><div style=color:green;> This FSN does not exist in Z-Axis DB. </div></li></ul>", 300, 150, "Validation Success", Nothing)
                Exit Sub
            End If
            Dim Chk_Existance_result As Boolean = Check_FSN_Exist_For_SameJID()

            If Chk_Existance_result = False Then
                flag = Insert_Command()
                If flag = True Then
                    Clear()
                    RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Candidate Record Saved Successfully. </div></li></ul>", 300, 150, "Validation Success", Nothing)
                Else
                    Exit Sub
                End If
            Else
                RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Candidate Record already existing for same JO. </div></li></ul>", 300, 150, "Validation Success", Nothing)
                Exit Sub
            End If
           
        ElseIf ImgBtn_Save.AlternateText = "Update" Then
            flag = Update_Command()
            If flag = True Then
                Clear()
                RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Candidate Record Updated Successfully. </div></li></ul>", 300, 150, "Validation Success", Nothing)
            Else
                Exit Sub
            End If
        ElseIf ImgBtn_Save.AlternateText = "Switch" Then
            flag = Switch_Command()
            If flag = True Then
                Clear()
                RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Candidate Record switched Successfully. </div></li></ul>", 300, 150, "Validation Success", Nothing)
            Else
                Exit Sub
            End If
        End If
        'Else
        '    RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Please schedule interview date before moving for cadidate selection process against this Job Order. </div></li></ul>", 300, 150, "Validation Success", Nothing)
        'End If
    End Sub

    Protected Function Insert_Command() As Boolean
        Dim Flag As Boolean = False
        Try

            Dim TransId As Integer = clas.getMaxID("select AMS.AMS_CANDIDATE_SEQ.nextval from dual")
            Dim Insert_Query As StringBuilder = New StringBuilder
            Insert_Query.Append("INSERT INTO AMS.AMS_Candidate(TRANSID,FSN, JOBID, ALOT_DT, INT_DT,INT_RESULT,")
            Insert_Query.Append(" FEEDBACK, EXPERIENCE, EDUCATION, RESUME_PATH, ")
            Insert_Query.Append(" STATUS, KEYEDON, KEYEDBY) ")
            Insert_Query.Append(" Values")
            Insert_Query.Append(" (")
            Insert_Query.Append(" '" & TransId & "', '" & txt_FSN.Text.Trim & "','" & JID & "',sysdate,")
            If Interview_Date.SelectedDate IsNot Nothing Then
                Insert_Query.Append(" to_date('" & Interview_Date.SelectedDate.Value.ToString("dd-MMM-yyyy") & "','dd-MON-yyyy'),")
            Else
                Insert_Query.Append(" null,")
            End If

            Insert_Query.Append(" '4',")
            Insert_Query.Append(" '" & txt_Feedback.Text.Trim & "','" & txt_Exp.Text.Trim & "','" & txt_EDU.Text.Trim & "','" & lbl_FullPath.Text.Trim & "',")
            Insert_Query.Append(" '1',sysdate,'" & Session("lgnagntid") & "'")
            Insert_Query.Append(" )")

            Dim i = clas.ExecuteNonQuery(Insert_Query.ToString())
            If i = 1 Then

                Flag = True
                Return Flag

               
            Else

                Dim Err_Msg As String = clas.getexception().Replace(vbCrLf, "")
                Err_Msg = Err_Msg.Replace(vbCr, "")
                Err_Msg = Err_Msg.Replace(vbLf, "")
                RadWindowManager1.RadAlert("<ul><li><div style=color:red;>System found some exception:" & Err_Msg & "</div></li></ul>", 300, 100, "Validation Exception Failure", Nothing)

                '  RadWindowManager1.RadAlert("<ul><li><div style=color:red;> Transaction not Done </div></li></ul>", 300, 150, "Validation Success", Nothing)
                Flag = False
            End If

            
            Throw New Exception("UnKnown Exception occured , Please Contact IT Department")
        Catch ex As Exception
            RadWindowManager1.RadAlert("<ul><li><div style=color:green;>" & ex.Message.ToString & "</div></li></ul>", 300, 200, "Exception", Nothing)
            Flag = False
        End Try
        Return Flag
    End Function

    

    Protected Function Update_Command() As Boolean
        Dim Flag As Boolean = False
        Dim TRANSID As String = Request.QueryString("tid")
        Try
            Dim Update_Query As StringBuilder = New StringBuilder
            Update_Query.Append("Update AMS.AMS_Candidate Set ")
            Update_Query.Append(" FEEDBACK='" & txt_Feedback.Text.Trim & "', ")
            Update_Query.Append(" EXPERIENCE='" & txt_Exp.Text.Trim & "',")
            Update_Query.Append(" EDUCATION='" & txt_EDU.Text.Trim & "',")
            Update_Query.Append(" RESUME_PATH='" & lbl_FullPath.Text.Trim & "',")
            Update_Query.Append(" PREV_KEYEDON=KEYEDON,PREV_KEYEDBY=KEYEDBY,KEYEDON=sysdate,KEYEDBY='" & Session("lgnagntid") & "'")
            Update_Query.Append(" WHERE TRANSID='" & TRANSID & "'")
            Dim i = clas.ExecuteNonQuery(Update_Query.ToString())
            If i = 1 Then

                Flag = True
                Return Flag


            Else

                Dim Err_Msg As String = clas.getexception().Replace(vbCrLf, "")
                Err_Msg = Err_Msg.Replace(vbCr, "")
                Err_Msg = Err_Msg.Replace(vbLf, "")
                RadWindowManager1.RadAlert("<ul><li><div style=color:red;>System found some exception:" & Err_Msg & "</div></li></ul>", 300, 100, "Validation Exception Failure", Nothing)

                '  RadWindowManager1.RadAlert("<ul><li><div style=color:red;> Transaction not Done </div></li></ul>", 300, 150, "Validation Success", Nothing)
                Flag = False
            End If

            Throw New Exception("UnKnown Exception occured , Please Contact IT Department")
        Catch ex As Exception
            RadWindowManager1.RadAlert("<ul><li><div style=color:green;>" & ex.Message.ToString & "</div></li></ul>", 300, 200, "Exception", Nothing)
            Flag = False
        End Try
        Return Flag
    End Function


    Protected Function Switch_Command() As Boolean
        Dim Flag As Boolean = False
        Try
            Dim TRANSID As String = Request.QueryString("tid")
            Dim Next_TransId As Integer = clas.getMaxID("select AMS.AMS_CANDIDATE_SEQ.nextval from dual")

            Dim Update_Query As StringBuilder = New StringBuilder
            Update_Query.Append("Update AMS.AMS_Candidate Set ")
            Update_Query.Append(" Switch_Job='Y', ")
            Update_Query.Append(" Next_TransId='" & Next_TransId & "',")
            Update_Query.Append(" Switch_dt=sysdate,")
            Update_Query.Append(" Switch_By='" & Session("lgnagntid") & "',")
            Update_Query.Append(" Cand_Status='F',")
            Update_Query.Append(" PREV_KEYEDON=KEYEDON,PREV_KEYEDBY=KEYEDBY,KEYEDON=sysdate,KEYEDBY='" & Session("lgnagntid") & "'")
            Update_Query.Append(" WHERE TRANSID='" & TRANSID & "'")
            Dim i = clas.ExecuteNonQuery(Update_Query.ToString())
            If i = 1 Then


                Dim Insert_Query As StringBuilder = New StringBuilder
                Insert_Query.Append("INSERT INTO AMS.AMS_Candidate(TRANSID,FSN, JOBID, ALOT_DT, INT_DT,INT_RESULT,")
                Insert_Query.Append(" FEEDBACK, EXPERIENCE, EDUCATION, RESUME_PATH, ")
                Insert_Query.Append(" STATUS, KEYEDON, KEYEDBY) ")
                Insert_Query.Append(" Values")
                Insert_Query.Append(" (")
                Insert_Query.Append(" '" & Next_TransId & "', '" & txt_FSN.Text.Trim & "','" & ddl_Position.SelectedValue & "',sysdate,")
                If Interview_Date.SelectedDate IsNot Nothing Then
                    Insert_Query.Append(" to_date('" & Interview_Date.SelectedDate.Value.ToString("dd-MMM-yyyy") & "','dd-MON-yyyy'),")
                Else
                    Insert_Query.Append(" null,")
                End If
                'to_date('" & Interview_Date.SelectedDate.Value.ToString("dd-MMM-yyyy") & "','dd-MON-yyyy'),
                Insert_Query.Append("  '4',")
                Insert_Query.Append(" '" & txt_Feedback.Text.Trim & "','" & txt_Exp.Text.Trim & "','" & txt_EDU.Text.Trim & "','" & lbl_FullPath.Text.Trim & "',")
                Insert_Query.Append(" '1',sysdate,'" & Session("lgnagntid") & "'")
                Insert_Query.Append(" )")


                Dim j = clas.ExecuteNonQuery(Update_Query.ToString())
                If j = 1 Then

                    Flag = True
                    Return Flag
                Else

                    Dim Err_Msg As String = clas.getexception().Replace(vbCrLf, "")
                    Err_Msg = Err_Msg.Replace(vbCr, "")
                    Err_Msg = Err_Msg.Replace(vbLf, "")
                    RadWindowManager1.RadAlert("<ul><li><div style=color:red;>System found some exception:" & Err_Msg & "</div></li></ul>", 300, 100, "Validation Exception Failure", Nothing)

                    '  RadWindowManager1.RadAlert("<ul><li><div style=color:red;> Transaction not Done </div></li></ul>", 300, 150, "Validation Success", Nothing)
                    Flag = False
                End If

            Else

                Dim Err_Msg As String = clas.getexception().Replace(vbCrLf, "")
                Err_Msg = Err_Msg.Replace(vbCr, "")
                Err_Msg = Err_Msg.Replace(vbLf, "")
                RadWindowManager1.RadAlert("<ul><li><div style=color:red;>System found some exception:" & Err_Msg & "</div></li></ul>", 300, 100, "Validation Exception Failure", Nothing)

                '  RadWindowManager1.RadAlert("<ul><li><div style=color:red;> Transaction not Done </div></li></ul>", 300, 150, "Validation Success", Nothing)
                Flag = False
            End If



                Throw New Exception("UnKnown Exception occured , Please Contact IT Department")
        Catch ex As Exception
            RadWindowManager1.RadAlert("<ul><li><div style=color:green;>" & ex.Message.ToString & "</div></li></ul>", 300, 200, "Exception", Nothing)
            Flag = False
        End Try
        Return Flag
    End Function

    Protected Function DeleteResume_Command() As Boolean
        Dim Flag As Boolean = False
        Dim TRANSID As String = Request.QueryString("tid")
        Try
            Dim Update_Query As StringBuilder = New StringBuilder
            Update_Query.Append("Update AMS.AMS_Candidate Set ")
            Update_Query.Append(" RESUME_PATH=null,")
            Update_Query.Append(" PREV_KEYEDON=KEYEDON,PREV_KEYEDBY=KEYEDBY,KEYEDON=sysdate,KEYEDBY='" & Session("lgnagntid") & "'")
            Update_Query.Append(" WHERE TRANSID='" & TRANSID & "'")
            Dim i = clas.ExecuteNonQuery(Update_Query.ToString())
            If i = 1 Then

                Flag = True
                Return Flag


            Else

                Dim Err_Msg As String = clas.getexception().Replace(vbCrLf, "")
                Err_Msg = Err_Msg.Replace(vbCr, "")
                Err_Msg = Err_Msg.Replace(vbLf, "")
                RadWindowManager1.RadAlert("<ul><li><div style=color:red;>System found some exception:" & Err_Msg & "</div></li></ul>", 300, 100, "Validation Exception Failure", Nothing)

                '  RadWindowManager1.RadAlert("<ul><li><div style=color:red;> Transaction not Done </div></li></ul>", 300, 150, "Validation Success", Nothing)
                Flag = False
            End If

            Throw New Exception("UnKnown Exception occured , Please Contact IT Department")
        Catch ex As Exception
            RadWindowManager1.RadAlert("<ul><li><div style=color:green;>" & ex.Message.ToString & "</div></li></ul>", 300, 200, "Exception", Nothing)
            Flag = False
        End Try
        Return Flag
    End Function


    Protected Sub ImgBtn_Refresh_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImgBtn_Refresh.Click
        Clear()
    End Sub

    Protected Sub Clear()
        Clear_FSNDet()
        txt_FSN.Enabled = True
        txt_FSN.Text = String.Empty
        txt_Exp.Text = String.Empty
        txt_EDU.Text = String.Empty
        txt_Feedback.Text = String.Empty
        lbl_ResumeFileName.Text = String.Empty
        lbl_FullPath.Text = String.Empty
    End Sub

End Class
