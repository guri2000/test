Imports System.Data

Imports System.IO
Imports Telerik.Web.UI
Imports System.Text
Partial Class Default2
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
            getmastres()
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
                qry = "Select a.EmpId, a.EmpFName || ' ' || a.EmpMName || ' ' || a.EmpLName as Emp_Name from AMS.AMS_Employer a"
                qry += " , ams.ams_usermaster b where b.lgnagntid=a.AgentId and b.lgnsts='A' "
                qry += " AND  a.EmpFName is not null and a.EmpFName not like '%Xxx%'  "
                qry += " and a.EmpFName not like '%Dummy%'  Order BY a.EmpFName ASC,a.EmpMName ASC,a.EmpLName ASC"
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
                    qry = "Select JS.JobSecuredId,JS.agentid,JS.empid,JS.jobpositionid AS Position,JS.totalvacancies,"
                    qry += "  (Select count(*) from AMS.AMS_Candidate CD where CD.JobId=JS.JobSecuredId) as Cand_Applied"
                    qry += " ,to_char(ValidUpto,'dd-Mon-YYYY') as Valid_UpTo,ExperienceReq as Experience_Req, "
                    qry += " EducationReq as Education_Req,AC.Name as Province,IC.DESCR as Category_"
                    qry += "  from AMS.AMS_JobSecured JS "
                    qry += "  Left Outer Join TT.ASSForCountry AC ON AC.TransId=JS.province"
                    qry += "  Left Outer Join TT.IMMCLASS_MASTER IC ON IC.IMMCLASS=JS.Category"
                    qry += "  where JS.vacancystatusid<>-1"
                    qry += "  and  JS.EMPID='" & ddl_Employer.SelectedValue & "' and JS.vacancystatusid='" & ddl_JO_Status.SelectedValue & "'"
                ElseIf ddl_Employer.SelectedValue = "" And ddl_JO_Status.SelectedValue <> "" Then
                    qry = "Select JS.JobSecuredId,JS.agentid,JS.empid,JS.jobpositionid AS Position,JS.totalvacancies,"
                    qry += "  (Select count(*) from AMS.AMS_Candidate CD where CD.JobId=JS.JobSecuredId) as Cand_Applied"
                    qry += " ,to_char(ValidUpto,'dd-Mon-YYYY') as Valid_UpTo,ExperienceReq as Experience_Req, "
                    qry += " EducationReq as Education_Req,AC.Name as Province,IC.DESCR as Category_"
                    qry += "  from AMS.AMS_JobSecured JS "
                    qry += "  Left Outer Join TT.ASSForCountry AC ON AC.TransId=JS.province"
                    qry += "  Left Outer Join TT.IMMCLASS_MASTER IC ON IC.IMMCLASS=JS.Category"
                    qry += "  where JS.vacancystatusid<>-1"
                    qry += "  and JS.vacancystatusid='" & ddl_JO_Status.SelectedValue & "'"
                ElseIf ddl_Employer.SelectedValue <> "" And ddl_JO_Status.SelectedValue = "" Then
                    qry = "Select JS.JobSecuredId,JS.agentid,JS.empid,JS.jobpositionid AS Position,JS.totalvacancies,"
                    qry += "  (Select count(*) from AMS.AMS_Candidate CD where CD.JobId=JS.JobSecuredId) as Cand_Applied"
                    qry += " ,to_char(ValidUpto,'dd-Mon-YYYY') as Valid_UpTo,ExperienceReq as Experience_Req, "
                    qry += " EducationReq as Education_Req,AC.Name as Province,IC.DESCR as Category_"
                    qry += "  from AMS.AMS_JobSecured JS "
                    qry += "  Left Outer Join TT.ASSForCountry AC ON AC.TransId=JS.province"
                    qry += "  Left Outer Join TT.IMMCLASS_MASTER IC ON IC.IMMCLASS=JS.Category"
                    qry += "  where JS.vacancystatusid<>-1"
                    qry += "  and JS.EMPID='" & ddl_Employer.SelectedValue & "'"
                ElseIf ddl_Employer.SelectedValue = "" And ddl_JO_Status.SelectedValue = "" Then
                    qry = "Select JS.JobSecuredId,JS.agentid,JS.empid,JS.jobpositionid AS Position,JS.totalvacancies,"
                    qry += "  (Select count(*) from AMS.AMS_Candidate CD where CD.JobId=JS.JobSecuredId) as Cand_Applied"
                    qry += " ,to_char(ValidUpto,'dd-Mon-YYYY') as Valid_UpTo,ExperienceReq as Experience_Req, "
                    qry += " EducationReq as Education_Req,AC.Name as Province,IC.DESCR as Category_"
                    qry += "  from AMS.AMS_JobSecured JS "
                    qry += "  Left Outer Join TT.ASSForCountry AC ON AC.TransId=JS.province"
                    qry += "  Left Outer Join TT.IMMCLASS_MASTER IC ON IC.IMMCLASS=JS.Category"
                    qry += " where JS.vacancystatusid<>-1"
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

    Private Sub getmastres()
        Try
            If Session("lgnroll") = "S" Then
                qry = "Select JS.JobSecuredId,JS.agentid,JS.empid,JS.jobpositionid AS Position,JS.totalvacancies,"
                qry += "  (Select count(*) from AMS.AMS_Candidate CD where CD.JobId=JS.JobSecuredId) as Cand_Applied"
                qry += " ,to_char(ValidUpto,'dd-Mon-YYYY') as Valid_UpTo,ExperienceReq as Experience_Req, "
                qry += " EducationReq as Education_Req,AC.Name as Province,IC.DESCR as Category_"
                qry += "  from AMS.AMS_JobSecured JS "
                qry += "  Left Outer Join TT.ASSForCountry AC ON AC.TransId=JS.province"
                qry += "  Left Outer Join TT.IMMCLASS_MASTER IC ON IC.IMMCLASS=JS.Category"
                qry += " where JS.vacancystatusid<>-1 "
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

    Protected Sub RadGrid1_SortCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridSortCommandEventArgs) Handles RadGrid1.SortCommand
        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
        getmastres()
    End Sub
   
    Protected Sub RadGrid1_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid1.ItemDataBound

      
      
      '  Dim ddl_emp As DropDownList = DirectCast(e.Item.FindControl("ddl_Employer"), DropDownList)
       ' If ddl_emp IsNot Nothing Then
          '  If ddl_emp.Items.Count - 1 = 0 Then

            '    qry = "select AGNTID, AGNTNAME from ams.ams_agents "
              '  dt = getdata(qry, "QR")
             '   If dt.Rows.Count > 0 Then
              '      ddl_emp.DataTextField = "AGNTNAME"
               '     ddl_emp.DataValueField = "AGNTID"
               '     ddl_emp.DataSource = dt
                '    ddl_emp.DataBind()
               '     'ddl_emp.SelectedValue = Session("ddl_employer")
           '   '  End If
           ' End If
        'End If
     
        RadGrid1.MasterTableView.GetColumn("JobSecuredId").Visible = False
        RadGrid1.MasterTableView.GetColumn("empid").Visible = False
        RadGrid1.MasterTableView.GetColumn("agentid").Visible = False

        If (TypeOf e.Item Is GridDataItem) Then


            Dim item As GridDataItem = e.Item

            Dim btnView As ImageButton = DirectCast(item.FindControl("btnView"), ImageButton)
            Dim btn_AddCandidate As ImageButton = DirectCast(item.FindControl("btn_AddCandidate"), ImageButton)
            If item.Cells(6).Text = "&nbsp;" Then
                btnView.Visible = False
                btn_AddCandidate.Visible = False
            Else
                btnView.Visible = True
                btn_AddCandidate.Visible = True
            End If

            btnView.ImageUrl = "Images/View_icon.png"
            btnView.ToolTip = "View Candidate List"
            btnView.CommandName = "View_Cand"
            btnView.CommandArgument = item.GetDataKeyValue("jobsecuredid").ToString()


            btn_AddCandidate.ImageUrl = "Images/Add_Candidate.png"
            btn_AddCandidate.ToolTip = "Add Candidate"
            btn_AddCandidate.CommandName = "Add_Cand"
            btn_AddCandidate.CommandArgument = item.GetDataKeyValue("jobsecuredid").ToString()

        End If
    End Sub
  
    Protected Sub RadGrid1_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.ItemCommand

        If e.CommandName = "View_Cand" Then
            Label1.Text = "View Candidate List"
            Ifrmfollowup.Attributes.Add("src", "View_Cand_List.aspx?Jid=" & e.CommandArgument.ToString())

            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)

        ElseIf e.CommandName = "Add_Cand" Then
            Label1.Text = "Add Candidate"
            Ifrmfollowup.Attributes.Add("src", "Add_Candidate.aspx?Jid=" & e.CommandArgument.ToString() & "&File=ACand")
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)

            'ElseIf e.CommandName = "IntShdul" Then

            '    Label1.Text = "Interview Schedule"
            '    Ifrmfollowup.Attributes.Add("src", "Int_Shdul_list.aspx?Jobid=" & e.CommandArgument.ToString())
            '    ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)

            'ElseIf e.CommandName = "refresh" Then
            '    For Each column As GridColumn In RadGrid1.MasterTableView.RenderColumns
            '        column.CurrentFilterFunction = GridKnownFunction.NoFilter
            '        column.CurrentFilterValue = String.Empty
            '    Next

            '    RadGrid1.MasterTableView.FilterExpression = String.Empty
            '    RadGrid1.Rebind()
            '    Label1.Text = ""
            '    ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            '    getmastres()

            'ElseIf e.CommandName = "EmployerFilter" Then
            Dim ddl_emp As DropDownList = DirectCast(e.Item.FindControl("ddl_Employer"), DropDownList)
            'Dim lbl_EmpName As Label = DirectCast(e.Item.FindControl("lbl_EmployerFilter"), Label)
            If ddl_emp IsNot Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
                Employer_Filter()
                ' lbl_EmpName.Text = "List of Job Orders Assigned to user : " & ddl_emp.SelectedItem.Text
                Panel1.Visible = True
                lbl_EmployerFilter.Text = "List of Leads Assigned to user : " & ddl_emp.SelectedItem.Text
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
            Else
                ddl_BDC.ClearSelection()
            End If
        Else
            ddl_BDC.ClearSelection()
        End If
    End Sub
End Class
