Imports System.Data
Imports System.IO
Imports Telerik.Web.UI
Imports System.Text
Partial Class Candidate_List
    Inherits System.Web.UI.Page
    Dim clas As rahulcpm.commonclass = New rahulcpm.commonclass()
    Dim qry As String
    Dim dt As DataTable
    Dim sydate As String
    Dim agentid As Int32 = 0
    Dim locid As Int32 = 0
    Dim msg As String
    Dim empid As Int32 = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("lgnagntid") = "0" Or Session("lgnagntid") = "" Then
            Response.Redirect("logout.aspx")
        Else
            agentid = Session("lgnagntid")
            'empid = Session("lgnagntid")
        End If
        If Page.IsPostBack = False Then
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            Get_Status()
            Bind_Agent()
            Bind_Employer()
            ' getmastres()
            getBlank()
            'addnewfrm.Attributes.Add("src", "JobSecured_Fin.aspx")
        Else

        End If

    End Sub

    

    Private Sub Bind_Agent()
        Try
            If Session("lgnroll") = "S" Then
                qry = "select a.agntid as  agnt_Id, a.agntname as agnt_Name from ams.ams_agents a, ams.ams_usermaster b where b.lgnagntid=a.agntid and b.lgnsts='A'"
                'Else
                '    qry = "select a.agntid as  agnt_Id, a.agntname as agnt_Name from ams.ams_agents a, ams.ams_usermaster b where b.lgnagntid=a.agntid and b.lgnsts='A' and a.agntid='" & Session("lgnagntid") & "'"
            End If
            dt = clas.getdata(qry, "QR")
            If dt.Rows.Count > 0 Then
                '  Dim ddl_BDC As DropDownList = DirectCast(RadGrid1.FindControl("ddl_BDC"), DropDownList)
                ddl_BDC.DataTextField = "agnt_Name"
                ddl_BDC.DataValueField = "agnt_Id"
                ddl_BDC.DataSource = dt
                ddl_BDC.DataBind()
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

    Private Sub Get_Status()
        Try
            Dim SqlQuery As String = "SELECT STATID,STATDESCR FROM ams.ams_STATMASTER WHERE STATSTATUS='A' and statscp='3'"
            Dim _DataTable As DataTable = clas.getdata(SqlQuery, "QR")
            If _DataTable.Rows.Count > 0 Then
                ddl_JO_Status.DataTextField = "STATDESCR"
                ddl_JO_Status.DataValueField = "STATID"
                ddl_JO_Status.DataSource = _DataTable
                ddl_JO_Status.DataBind()
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

    Private Sub Bind_Employer()
        Try
            If Session("lgnroll") = "S" Then
                qry = "Select a.EmpId,  a.empcmpnam ||  ' - ' || a.EmpFName || ' ' || a.EmpMName || ' ' || a.EmpLName as Emp_Name from AMS.AMS_Employer a"
                qry += " , ams.ams_usermaster b where b.lgnagntid=a.AgentId and b.lgnsts='A' "
                qry += " Order BY a.EmpFName ASC,a.EmpMName ASC,a.EmpLName ASC"
                'qry += " AND  a.EmpFName is not null and a.EmpFName not like '%Xxx%'  "
                'qry += " and a.EmpFName not like '%Dummy%'  Order BY a.EmpFName ASC,a.EmpMName ASC,a.EmpLName ASC"
            End If
            dt = clas.getdata(qry, "QR")
            If dt.Rows.Count > 0 Then
                '  Dim ddl_BDC As DropDownList = DirectCast(RadGrid1.FindControl("ddl_Employer"), DropDownList)
                ddl_Employer.DataTextField = "Emp_Name"
                ddl_Employer.DataValueField = "EmpId"
                ddl_Employer.DataSource = dt
                ddl_Employer.DataBind()
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

    Private Sub Employer_Filter()
        Try
            If Session("lgnroll") = "S" Then
                If ddl_Employer.SelectedValue <> "" And ddl_JO_Status.SelectedValue <> "" Then
                    qry = "   Select CD.TransId,JS.EmpId,JS.AgentId,JS.vacancyStatusId,CD.Jobid,CD.FSN,InitCap(CV.Name) as Candidate_Name,"
                    qry += "   to_char(CV.AppDOB,'DD-Mon-YYYY') as APP_DOB,   CV.ImmClass,cd.experience, "
                    qry += "   cd.education,JS.JobPositionId as Job_Position,cd.resume_path,ins.description as Status    from AMS.AMS_Candidate CD,"
                    qry += "   tt.ClientView CV,AMS.AMS_IntStatus INS ,AMS.AMS_JobSecured JS  where CD.JobId=JS.JobSecuredId "
                    qry += "   and CD.FSN = CV.fileserialno And cd.status = INS.TransId and CD.Cand_Status='A' AND JS.EmpId='" & ddl_Employer.SelectedValue & "' AND JS.vacancyStatusId='" & ddl_JO_Status.SelectedValue & "'"
                ElseIf ddl_Employer.SelectedValue = "" And ddl_JO_Status.SelectedValue <> "" Then
                    qry = "   Select CD.TransId,JS.EmpId,JS.AgentId,JS.vacancyStatusId,CD.Jobid,CD.FSN,InitCap(CV.Name) as Candidate_Name,"
                    qry += "   to_char(CV.AppDOB,'DD-Mon-YYYY') as APP_DOB,   CV.ImmClass,cd.experience, "
                    qry += "   cd.education,JS.JobPositionId as Job_Position,cd.resume_path,ins.description as Status    from AMS.AMS_Candidate CD,"
                    qry += "   tt.ClientView CV,AMS.AMS_IntStatus INS ,AMS.AMS_JobSecured JS  where CD.JobId=JS.JobSecuredId "
                    qry += "   and CD.FSN = CV.fileserialno And cd.status = INS.TransId and CD.Cand_Status='A' AND JS.vacancyStatusId='" & ddl_JO_Status.SelectedValue & "'"
                ElseIf ddl_Employer.SelectedValue <> "" And ddl_JO_Status.SelectedValue = "" Then
                    qry = "   Select CD.TransId,JS.EmpId,JS.AgentId,JS.vacancyStatusId,CD.Jobid,CD.FSN,InitCap(CV.Name) as Candidate_Name,"
                    qry += "   to_char(CV.AppDOB,'DD-Mon-YYYY') as APP_DOB,   CV.ImmClass,cd.experience, "
                    qry += "   cd.education,JS.JobPositionId as Job_Position,cd.resume_path,ins.description as Status    from AMS.AMS_Candidate CD,"
                    qry += "   tt.ClientView CV,AMS.AMS_IntStatus INS ,AMS.AMS_JobSecured JS  where CD.JobId=JS.JobSecuredId "
                    qry += "   and CD.FSN = CV.fileserialno And cd.status = INS.TransId and CD.Cand_Status='A' AND JS.EmpId='" & ddl_Employer.SelectedValue & "'"
                ElseIf ddl_Employer.SelectedValue = "" And ddl_JO_Status.SelectedValue = "" Then
                    qry = "   Select CD.TransId,JS.EmpId,JS.AgentId,JS.vacancyStatusId,CD.Jobid,CD.FSN,InitCap(CV.Name) as Candidate_Name,"
                    qry += "   to_char(CV.AppDOB,'DD-Mon-YYYY') as APP_DOB,   CV.ImmClass,cd.experience, "
                    qry += "   cd.education,JS.JobPositionId as Job_Position,cd.resume_path,ins.description as Status    from AMS.AMS_Candidate CD,"
                    qry += "   tt.ClientView CV,AMS.AMS_IntStatus INS ,AMS.AMS_JobSecured JS  where CD.JobId=JS.JobSecuredId "
                    qry += "   and CD.FSN = CV.fileserialno And cd.status = INS.TransId and CD.Cand_Status='A'"
                End If
            End If


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

    Private Sub getBlank()
        qry = "select * from tt.ams_candidateList where 1=0"
        dt = clas.getdata(qry, "QR")       
            RadGrid1.DataSource = dt
            RadGrid1.DataBind()
    End Sub

    Private Sub getmastres()
        Try
            If Session("lgnroll") = "S" Then
                qry = "   Select CD.TransId,JS.EmpId,JS.AgentId,JS.vacancyStatusId,CD.Jobid,CD.FSN,InitCap(CV.Name) as Candidate_Name,"
                qry += "   to_char(CV.AppDOB,'DD-Mon-YYYY') as APP_DOB,   CV.ImmClass,cd.experience, "
                qry += "   cd.education,JS.JobPositionId as Job_Position,cd.resume_path,ins.description as Status    from AMS.AMS_Candidate CD,"
                qry += "   tt.ClientView CV,AMS.AMS_IntStatus INS ,AMS.AMS_JobSecured JS  where CD.JobId=JS.JobSecuredId "
                qry += "   and CD.FSN = CV.fileserialno And cd.status = INS.TransId and CD.Cand_Status='A'"
            End If


            'qry = "Select Empfname||' '||Empmname||' '||Emplname As Empnam, Jobpositionid As Job_Position,Totalvacancies As Tot_Vacancy,(select statdescr from ams.ams_statmaster where statid=vacancystatusid ) status,To_Char(Validupto,'dd-Mon-yyyy hh24:mi:ss') As Valid_Upto,To_Char(Cndapprdate,'dd-Mon-yyyy hh24:mi:ss') As Canada_Appdate,to_char(indapprdate,'dd-Mon-yyyy hh24:mi:ss') as Ind_AppDate,to_char(Keyedon,'dd-Mon-yyyy hh24:mi:ss') as Keyedon,jobsecuredid  from ams.ams_employer a,ams.ams_jobsecured b where a.empid=b.empid  order by keyeddate desc"
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

    Protected Sub RadGrid1_ItemCreated(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid1.ItemCreated
        If TypeOf e.Item Is GridFilteringItem Then
            Dim gridFilteringItem As GridFilteringItem = TryCast(e.Item, GridFilteringItem)
            'Dim FSN As TextBox = TryCast(gridFilteringItem("FSN").Controls(0), TextBox)
            'FSN.Width = Unit.Pixel(10)

        End If
    End Sub

    Protected Sub RadGrid1_SortCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridSortCommandEventArgs) Handles RadGrid1.SortCommand
        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
        getmastres()
    End Sub

    Protected Sub RadGrid1_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid1.ItemDataBound


        RadGrid1.MasterTableView.GetColumn("TransId").Visible = False
        RadGrid1.MasterTableView.GetColumn("Jobid").Visible = False
        RadGrid1.MasterTableView.GetColumn("resume_path").Visible = False

        RadGrid1.MasterTableView.GetColumn("EmpId").Visible = False
        RadGrid1.MasterTableView.GetColumn("AgentId").Visible = False
        RadGrid1.MasterTableView.GetColumn("vacancyStatusId").Visible = False

        If (TypeOf e.Item Is GridDataItem) Then


            Dim item As GridDataItem = e.Item

            Dim btnedit As ImageButton = DirectCast(item.FindControl("imgbtnedit"), ImageButton)
            Dim btn_JobSwitch As ImageButton = DirectCast(item.FindControl("btn_JobSwitch"), ImageButton)
            Dim btn_ViewResume As ImageButton = DirectCast(item.FindControl("btn_ViewResume"), ImageButton)
            If item.Cells(8).Text = "&nbsp;" Then
                btnedit.Visible = False
                btn_JobSwitch.Visible = False
                btn_ViewResume.Visible = False
            Else
                btnedit.Visible = True
                btn_JobSwitch.Visible = True
                btn_ViewResume.Visible = True
            End If

          
            btnedit.CommandName = "Edit_now"
            btnedit.CommandArgument = item.GetDataKeyValue("TransId").ToString()

            btn_JobSwitch.CommandName = "Switch"
            btn_JobSwitch.CommandArgument = item.GetDataKeyValue("TransId").ToString()

            btn_ViewResume.CommandName = "View"
            btn_ViewResume.CommandArgument = item.GetDataKeyValue("TransId").ToString()

        End If
    End Sub

    Protected Sub RadGrid1_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.ItemCommand

        If e.CommandName = "Edit_now" Then
            lbl_tag.Text = "Edit Candidate Record"
            IfrEditcand.Attributes.Add("src", "Add_Candidate.aspx?tid=" & e.CommandArgument.ToString() & "&mode=edit" & "&File=ACand")
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup4();", True)

        ElseIf e.CommandName = "Switch" Then
            lbl_tag.Text = "Switch Candidate Record"
            IfrEditcand.Attributes.Add("src", "Add_Candidate.aspx?tid=" & e.CommandArgument.ToString() & "&mode=edit" & "&File=Switch")
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup4();", True)

        ElseIf e.CommandName = "View" Then
            Dim btn_ViewResume As ImageButton = DirectCast(e.Item.FindControl("btn_ViewResume"), ImageButton)
            If btn_ViewResume.AlternateText <> "" Then
                Dim filePath As String = btn_ViewResume.AlternateText
                View_Resume(filePath)
            Else
                RadWindowManager1.RadAlert("<ul><li><div style=color:green;> System didn't find Resume to view. </div></li></ul>", 300, 150, "Validation Success", Nothing)
            End If

        ElseIf e.CommandName = "Filter" Then
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            getmastres()
        ElseIf e.CommandName = "ChangePageSize" Then
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            getmastres()
        End If
    End Sub

    Protected Sub RadButton1_ToggleStateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.ButtonToggleStateChangedEventArgs)
        Dim btn As RadButton = TryCast(sender, RadButton)
        If e.SelectedToggleState.Value = "T" Then
            RadGrid1.AllowFilteringByColumn = True
        ElseIf e.SelectedToggleState.Value = "F" Then
            RadGrid1.AllowFilteringByColumn = False
        End If
    End Sub

    Private Sub RadGrid1_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles RadGrid1.PageIndexChanged
        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
        RadGrid1.CurrentPageIndex = e.NewPageIndex
        getmastres()
    End Sub

    Protected Sub Btnref_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Button2.Click
        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
        Employer_Filter()
    End Sub

    Protected Sub ddl_Employer_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_Employer.SelectedIndexChanged
        If ddl_Employer.SelectedValue <> "" Then
            Dim Query As String = " Select A.Agentid from AMS.AMS_Employer A where A.EmpId='" & ddl_Employer.SelectedValue & "'"
            Dim _DT1 As DataTable = New DataTable
            _DT1 = clas.getdata(Query, "QR")
            If _DT1.Rows.Count > 0 Then
                ddl_BDC.SelectedValue = _DT1.Rows(0)("Agentid")
                ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
                Employer_Filter()
            Else
                ddl_BDC.ClearSelection()
            End If
        Else
            ddl_BDC.ClearSelection()
        End If
    End Sub

    Protected Sub View_Resume(ByVal filePath As String)
        Try
            Dim filename As String = filePath.Split("\").Last
            If filename.Contains(".doc") Then
                Response.ContentType = "application/vnd.ms-word"
            ElseIf filename.Contains(".pdf") Then
                Response.ContentType = "application/pdf"
            ElseIf filename.Contains(".txt") Then
                Response.ContentType = "text/plain"
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
            FN = FN.Replace(" ", "_")
            'Response.ContentType = ContentType
            Response.AppendHeader("Content-Disposition", ("attachment; filename=" + FN))
            Response.WriteFile(filePath)
            Response.End()

            Throw New Exception("UnKnown Exception occured , Please Contact IT Department")
        Catch ex As Exception
            RadWindowManager1.RadAlert("<ul><li><div style=color:green;>" & ex.Message.ToString & "</div></li></ul>", 300, 200, "Exception", Nothing)

        End Try
        

    End Sub

    Protected Sub ddl_JO_Status_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_JO_Status.SelectedIndexChanged
        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
        Employer_Filter()
    End Sub
End Class
