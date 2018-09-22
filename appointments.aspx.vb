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
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("lgnagntid") = "0" Or Session("lgnagntid") = "" Then
            Response.Redirect("logout.aspx")
        Else
            agentid = Session("lgnagntid")
        End If
        If Page.IsPostBack = False Then
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            getempty()
            Bind_Employer()
           
        Else

        End If
        'Dim mstlbl As Label = Page.Master.FindControl("masterpagelabel")
        'mstlbl.Text = "List of Records"
        'Dim abcummb As HtmlAnchor = Page.Master.FindControl("abcummb")
        'Dim mstbcmblbl As Label = Page.Master.FindControl("Lblbcumb")
        'mstbcmblbl.Text = "New Leads"
        'abcummb.Attributes.Add("title", "Prospect Transaction Screen")
    End Sub
    
    Private Sub Bind_Employer()
        Try
            qry = "select   agntId, agntname as agnt_Name from ams.ams_agents"
            If Session("lgnroll") = "S" Then

            Else
                qry &= " where  agntid='" & Session("lgnagntid") & "'"
            End If
            dt = clas.getdata(qry, "QR")

            '  Dim ddl_emp As DropDownList = DirectCast(RadGrid1.FindControl("ddl_Employer"), DropDownList)
            ddl_Category.DataTextField = "agnt_Name"
            ddl_Category.DataValueField = "agntId"
            ddl_Category.DataSource = dt
            ddl_Category.DataBind()

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
    'Private Sub Bind_Employer()
    '    Try
    '        qry = "  Select * from CBISI_CRM.ams_statmaster where statscp=51"

    '        dt = clas.getdata(qry, "QR")

    '        '  Dim ddl_emp As DropDownList = DirectCast(RadGrid1.FindControl("ddl_Employer"), DropDownList)
    '        ddl_Category.DataTextField = "STATDESCR"
    '        ddl_Category.DataValueField = "STATID"
    '        ddl_Category.DataSource = dt
    '        ddl_Category.DataBind()

    '    Catch ex As Exception
    '        If ex.Message.Contains("OutOfMemory") Then
    '            RadWindowManager1.RadAlert("<ul><li><div style=color:Red;>This is a temporary issue; please try to Re-Login after some time.</div></li></ul>", 300, 150, "Page Loading Error", Nothing)
    '        Else
    '            Dim Err_Msg As String = ex.Message.ToString.Replace(vbCrLf, "")
    '            Err_Msg = Err_Msg.Replace(vbCr, "")
    '            Err_Msg = Err_Msg.Replace(vbLf, "")
    '            RadWindowManager1.RadAlert("<ul><li><div style=color:Red;>" & Err_Msg & ".</div></li></ul>", 300, 100, "Page Loading Error", Nothing)
    '        End If
    '    End Try
    'End Sub


    Private Sub Employer_Filter(ByVal EmpId As String)
        Try
            qry = "select * from ams.LEADS_FOL_LIST where nxtdt between '" & CDate(nxtdt.SelectedDate).ToString("dd-MMM-yyyy") & "' and '" & CDate(nxtdate21.SelectedDate).ToString("dd-MMM-yyyy") & "'"

         
            '   Response.Write(qry)
            If Session("lgnroll") = "S" Then
                If ddl_Category.SelectedIndex > 0 Then
                    qry &= " and  AGENTID='" & ddl_Category.SelectedValue & "'"
                End If
            Else
                qry &= " and  AGENTID='" & agentid & "'"
            End If
           
            dt = clas.getdata(qry, "QR")

          

            RadGrid1.DataSource = dt
            RadGrid1.DataBind()




        Catch ex As Exception
            If ex.Message.Contains("OutOfMemory") Then
                RadWindowManager1.RadAlert("<ul><li><div style=color:Red;>This is a temporary issue; please try to Re-Login after some time.</div></li></ul>", 300, 150, "Page Loading Error", Nothing)
            Else
                Dim Err_Msg As String = ex.Message.ToString.Replace(vbCrLf, "")
                Err_Msg = Err_Msg.Replace(vbCr, "")
                Err_Msg = Err_Msg.Replace("""", "")
                Err_Msg = Err_Msg.Replace("'", "")
                Err_Msg = Err_Msg.Replace(vbLf, "")
                RadWindowManager1.RadAlert("<ul><li><div style=color:Red;>" & Err_Msg & ".</div></li></ul>", 300, 100, "Page Loading Error", Nothing)
            End If
        End Try
    End Sub

    Private Sub getempty()
        Try
            qry = "select * from  ams.LEADS_FOL_LIST where 1=2"
            'If Session("lgnroll") = "ADMIN" Then

            'Else
            '    qry &= " where HANDELBY='" & agentid & "'"
            'End If
            dt = clas.getdata(qry, "QR")

            'Dim drnewrow As DataRow = dt.NewRow
            'dt.Rows.Add(drnewrow)

            If dt.Rows.Count = 0 Then
                Dim drnewrow As DataRow = dt.NewRow
                ' dt.Rows.Add(drnewrow)
            End If
            RadGrid1.DataSource = dt
            RadGrid1.DataBind()

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


    'Private Sub getmastres()
    '    Try
    '        qry = "select * from  ams.LEADS_FOL_LIST"
    '        'If Session("lgnroll") = "ADMIN" Then

    '        'Else
    '        '    qry &= " where HANDELBY='" & agentid & "'"
    '        'End If
    '        dt = clas.getdata(qry, "QR")

    '        'Dim drnewrow As DataRow = dt.NewRow
    '        'dt.Rows.Add(drnewrow)

    '        If dt.Rows.Count = 0 Then
    '            Dim drnewrow As DataRow = dt.NewRow
    '            dt.Rows.Add(drnewrow)
    '        End If
    '        RadGrid1.DataSource = dt
    '        RadGrid1.DataBind()

    '    Catch ex As Exception
    '        If ex.Message.Contains("OutOfMemory") Then
    '            RadWindowManager1.RadAlert("<ul><li><div style=color:Red;>This is a temporary issue; please try to Re-Login after some time.</div></li></ul>", 300, 150, "Page Loading Error", Nothing)
    '        Else
    '            Dim Err_Msg As String = ex.Message.ToString.Replace(vbCrLf, "")
    '            Err_Msg = Err_Msg.Replace(vbCr, "")
    '            Err_Msg = Err_Msg.Replace(vbLf, "")
    '            RadWindowManager1.RadAlert("<ul><li><div style=color:Red;>" & Err_Msg & ".</div></li></ul>", 300, 100, "Page Loading Error", Nothing)
    '        End If
    '    End Try
    'End Sub
    Protected Sub RadGrid1_SortCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridSortCommandEventArgs) Handles RadGrid1.SortCommand
        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
        Employer_Filter(ddl_Category.SelectedValue)
    End Sub
  
 
    Protected Sub RadGrid1_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid1.ItemDataBound

      
        'If (TypeOf e.Item Is GridHeaderItem) Then
        '    Dim item As GridHeaderItem = e.Item
        '    item.Cells(10).Width = 0
        'End If

      '  Dim ddl_emp As DropDownList = DirectCast(e.Item.FindControl("ddl_Employer"), DropDownList)
       ' If ddl_emp IsNot Nothing Then
          '  If ddl_emp.Items.Count - 1 = 0 Then

             '   qry = "select empid as  Emp_Id, empfname || '' || empmname || '' || emplname as Emp_Name from ams.ams_EMPLOYER"
              '  dt = getdata(qry, "QR")
              '  If dt.Rows.Count > 0 Then
               '     ddl_emp.DataTextField = "Emp_Name"
               '     ddl_emp.DataValueField = "Emp_Id"
              '      ddl_emp.DataSource = dt
              '      ddl_emp.DataBind()
              '      'ddl_emp.SelectedValue = Session("ddl_employer")
             '   End If
           ' End If
       ' End If
      



        ' RadGrid1.MasterTableView.GetColumn("FOLLOWUPID").Visible = False
          RadGrid1.MasterTableView.GetColumn("REMARKS1").ItemStyle.Width = 0
          RadGrid1.MasterTableView.GetColumn("REMARKS1").HeaderStyle.Width = 0
        '    RadGrid1.MasterTableView.GetColumn("LEADID").ItemStyle.Width = 0
        '   RadGrid1.MasterTableView.GetColumn("LEADID").HeaderStyle.Width = 0
        RadGrid1.MasterTableView.GetColumn("AGENTID").ItemStyle.Width = 0
        RadGrid1.MasterTableView.GetColumn("AGENTID").HeaderStyle.Width = 0
        RadGrid1.MasterTableView.GetColumn("empid").ItemStyle.Width = 0
        RadGrid1.MasterTableView.GetColumn("empid").HeaderStyle.Width = 0
        RadGrid1.MasterTableView.GetColumn("followupid").ItemStyle.Width = 0
        RadGrid1.MasterTableView.GetColumn("followupid").HeaderStyle.Width = 0
         RadGrid1.MasterTableView.GetColumn("nxtdt").ItemStyle.Width = 0
        RadGrid1.MasterTableView.GetColumn("nxtdt").HeaderStyle.Width = 0

        'RadGrid1.MasterTableView.GetColumn("AGNSTAPPSTS").ItemStyle.Width = 0
        'RadGrid1.MasterTableView.GetColumn("AGNSTAPPSTS").HeaderStyle.Width = 0
        'RadGrid1.MasterTableView.GetColumn("nxt_dt").ItemStyle.Width = 0
        'RadGrid1.MasterTableView.GetColumn("nxt_dt").HeaderStyle.Width = 0
       


        If (TypeOf e.Item Is GridDataItem) Then


            Dim item As GridDataItem = e.Item

           

            'Dim chk As CheckBox = DirectCast(item.FindControl("chk"), CheckBox)





            'If (item("Email1").Text.Trim().Replace("&nbsp", "").Length = 0 And item("Mobile1").Text.Trim().Replace("&nbsp", "").Length = 0) Or item("Email1").Text.Trim() = "D" Then
            '    chk.Enabled = False
            'Else
            '    chk.Enabled = True
            'End If

            '    'Dim immcls As String = item.GetDataKeyValue("Immclass").ToString()
            Dim btnget As ImageButton = DirectCast(item.FindControl("btnget"), ImageButton)
            Dim btnedit As ImageButton = DirectCast(item.FindControl("btnedit"), ImageButton)
            Dim btnjob As ImageButton = DirectCast(item.FindControl("btnjob"), ImageButton)
            Dim btnint As ImageButton = DirectCast(item.FindControl("btnint"), ImageButton)

            btnedit.Visible = False
            btnint.Visible = False
            btnjob.Visible = False
            'If Convert.ToString(item.GetDataKeyValue("ACTION_TAKEN_ON")).Replace("&nbsp;", "").Trim().Length > 0 Then
            '    btnget.Visible = False
            'Else
            btnget.Visible = True
            btnget.ImageUrl = "Images/followup-ico-small.png"
            btnget.CommandName = "followup"
            btnget.CommandArgument = Convert.ToString(item.GetDataKeyValue("EMPID"))

            'If item("fileno").Text.Trim().Replace("&nbsp", "").Length = 0 Then
            '    btnget.CommandArgument = Convert.ToString(item.GetDataKeyValue("agentid")) & ";" & Convert.ToString(item.GetDataKeyValue("FOLLOWUPID")) & ";" & Convert.ToString(item.GetDataKeyValue("fileno"))
            'Else
            '    btnget.CommandArgument = Convert.ToString(item.GetDataKeyValue("leadid")) & ";" & Convert.ToString(item.GetDataKeyValue("FOLLOWUPID")) & ";" & Convert.ToString(item.GetDataKeyValue("fileno"))
            'End If




            '        End If

            '    Dim btnagr As Button = DirectCast(item.FindControl("btnagr"), Button)

            '    If item("status").Text = "&nbsp;" Then




            '    Else
            '        btnjob.Visible = True
            '        btnedit.Visible = True
            '        btnint.Visible = True
            '        btnget.Visible = True

            '    End If

            '    If item("scpid").Text.Trim() = "83" Then


            '        btnagr.ToolTip = "Generate Aggreement For: " & item("Scope").Text
            '        btnagr.CommandName = "genagragnt"
            '        btnagr.CommandArgument = item.GetDataKeyValue("leadid").ToString()
            '        btnagr.Visible = True
            '    Else
            '        btnagr.Enabled = False
            '        btnagr.Visible = False
            '    End If


            '    If item("status").Text <> "Not Interested" Then

            '        'Dim a As HtmlAnchor = New HtmlAnchor()
            '        '  btnint.Visible = True
            '        btnint.ImageUrl = "Images/intersted.png"
            '        btnint.ToolTip = "Interested"
            '        btnedit.ImageUrl = "Images/edit-ico-small.png"
            '        btnedit.ToolTip = "Edit Record"
            '        btnedit.CommandName = "editform"
            '        btnedit.CommandArgument = item.GetDataKeyValue("leadid").ToString()
            '   btnget.ImageUrl = "Images/followup-ico-small.png"
            '   btnget.ToolTip = "New Follow up" & " (" & item("leadname").Text & ") "
            '  btnget.CommandName = "followup"
            ' btnget.CommandArgument = item.GetDataKeyValue("leadid").ToString()



            '        'Dim img As ImageButton = New ImageButton()
            '        If item("status").Text = "Secured Job Order" Then
            '            btnjob.ImageUrl = "Images/secured-job-small.png"
            '            btnjob.ToolTip = "My Job Orders"
            '            btnjob.CommandName = "securedjob"
            '            btnjob.CommandArgument = item.GetDataKeyValue("leadid").ToString()


            '            ''    'a.HRef = "#addnewsecuredjoborder"
            '            ''    'a.Attributes.Add("data-toggle", "modal")
            '            ''    'Ifrmfollowup.Attributes.Add("src", "securedjob_list.aspx?empid=" & item.GetDataKeyValue("empid").ToString())
            '            ''    img.ImageUrl = "Images/magnifier.png"
            '            ''    img.CommandName = "securedjob"
            '            ''    img.CommandArgument = item.GetDataKeyValue("empid").ToString()
            '        ElseIf item("status").Text = "Wants More Information" Then
            '            btnget.ImageUrl = "Images/followup-ico-small.png"
            '            btnget.ToolTip = "New Follow up" & " (" & item("leadname").Text & ") "
            '            btnget.CommandName = "followup"
            '            btnget.CommandArgument = item.GetDataKeyValue("leadid").ToString()
            '            btnjob.ImageUrl = "Images/not-secured-job-small.png"
            '            btnjob.ImageUrl = "Images/secured-job-small.png"
            '            btnjob.ToolTip = "My Job Orders"
            '            btnjob.CommandName = "securedjob"
            '            btnjob.CommandArgument = item.GetDataKeyValue("leadid").ToString()



            '            '    '  a.HRef = "#addnewfollowup"
            '            ''    'img.Attributes.Add("data-toggle", "modal")
            '            ''    'Ifrmfollowup.Attributes.Add("src", "securedjob_list.aspx?empid=" & item.GetDataKeyValue("empid").ToString())
            '            ''    
            '            ''    img.CommandName = "followup"
            '            ''    img.CommandArgument = item.GetDataKeyValue("empid").ToString()
            '            ''    'a.Attributes.Add("onclick", AddressOf )
            '            ''    AddHandler img.Click, AddressOf ImageButton_Click
            '            ''    'a.Attributes.Add("onclick", "onfollow()")
            '        Else
            '            ''    a.HRef = "#"
            '            ''    img.ImageUrl = ""
            '            ''End If
            '            'img.ID = "img1"
            '            'img.ImageUrl = "Images/magnifier.png"
            '            'img.CommandName = "followup"
            '            'img.CommandArgument = item.GetDataKeyValue("empid").ToString()
            '            btnget.ImageUrl = "Images/followup-ico-small.png"
            '            btnget.ToolTip = "New Follow up" & " (" & item("leadname").Text & ") "
            '            btnget.CommandName = "followup"
            '            btnget.CommandArgument = item.GetDataKeyValue("leadid").ToString()
            '            btnjob.ImageUrl = "Images/secured-job-small.png"
            '            btnjob.ToolTip = "My Job Orders"
            '            btnjob.CommandName = "securedjob"
            '            btnjob.CommandArgument = item.GetDataKeyValue("leadid").ToString()



            '            btnint.ImageUrl = "Images/intersted.png"
            '            btnint.ToolTip = "Interested"
            '            'item.Cells(8).Controls.Add(img)
            '        End If
            '    Else
            '        btnint.ImageUrl = "Images/not-intersted.png"
            '        btnint.ToolTip = "Not Interested"
            '        btnedit.ImageUrl = "Images/edit-ico-small.png"
            '        btnedit.ToolTip = "Edit Record"
            '        btnedit.CommandName = "editform"
            '        btnedit.CommandArgument = item.GetDataKeyValue("leadid").ToString()

            '        btnget.ToolTip = "New Follow up" & " (" & item("leadname").Text & ") "

            '        btnjob.ImageUrl = "Images/secured-job-small.png"
            '        btnjob.ToolTip = "My Job Orders"
            '        btnjob.CommandName = "securedjob"
            '        btnjob.CommandArgument = item.GetDataKeyValue("leadid").ToString()



            '        btnint.ImageUrl = "Images/not-intersted.png"
            '        btnint.ToolTip = "Not Interested"
            '        'btnjob.ImageUrl = "Images/not-secured-job-small.png"

            '    End If
            '    btnjob.Visible = False

            '    'For Each cell As GridColumn In item.Cells
            '    '    cell.FilterDelay = "200"
            '    'Next


            'If item("AGNSTAPPSTS").Text.Replace("&nbsp;", "").Trim() = "T" Then

            '    item.ForeColor = Drawing.Color.BlueViolet
            '    '   btnget.Visible = False


            'End If

        End If
    End Sub

    'Sub ImageButton_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
    '    Ifrmfollowup.Attributes.Add("src", "followup_list.aspx?empid=4")

    '    ClientScript.RegisterClientScriptBlock([GetType](), "none", "<script>$('#addnewfollowup').modal('show');</script>", False)
    'End Sub
    Protected Sub RadGrid1_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.ItemCommand
        'If e.CommandName = "securedjob" Then
        '    Label1.Text = "My Job Orders"
        '    Ifrmfollowup.Attributes.Add("src", "jobsecure_list.aspx?empid=" & e.CommandArgument.ToString())
        '    '  ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
        '    ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)
        '    'ClientScript.RegisterClientScriptBlock([GetType](), "none", "openModal();", False)
        '    'ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", True)
        'ElseIf e.CommandName = "followup" Then
            '    Label1.Text = "Add New Follow up"
            '    Ifrmfollowup.Attributes.Add("src", "followup_list.aspx?empid=" & e.CommandArgument.ToString())
            '    'ClientScript.RegisterStartupScript(Me.GetType(), "none", "<script>$('#addnewfollowup').modal('show');</script>", True)
            '    '  ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            '    ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)
            '    'ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", True)
            'ElseIf e.CommandName = "genagragnt" Then
            '    Label1.Text = "Generate Aggrement For Agent"
            '    Ifrmfollowup.Attributes.Add("src", "agent_agreement.aspx?empid=" & e.CommandArgument.ToString())
            '    'ClientScript.RegisterStartupScript(Me.GetType(), "none", "<script>$('#addnewfollowup').modal('show');</script>", True)
            '    '  ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            '    ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)
            '    'ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", True)
            'ElseIf e.CommandName = "editform" Then
            '    Label1.Text = "Edit Lead Record"
            '    Ifrmfollowup.Attributes.Add("src", "employer_det.aspx?empid=" & e.CommandArgument.ToString() & "&mode=edit")
            '    'ClientScript.RegisterStartupScript(Me.GetType(), "none", "<script>$('#addnewfollowup').modal('show');</script>", True)
            '    '  ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            '    ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)
            '    'ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", True)

            If e.CommandName = "refresh" Then
                For Each column As GridColumn In RadGrid1.MasterTableView.RenderColumns
                    column.CurrentFilterFunction = GridKnownFunction.NoFilter
                    column.CurrentFilterValue = String.Empty
                Next


                RadGrid1.MasterTableView.FilterExpression = String.Empty
                RadGrid1.Rebind()
                Label1.Text = ""
                ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
                Employer_Filter(ddl_Category.SelectedValue)
                'Ifrmfollowup.Attributes.Add("src", "followup_list.aspx?empid=" & e.CommandArgument.ToString())

                'ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)
            ElseIf e.CommandName = "EmployerFilter" Then

                ' Dim ddl_emp As DropDownList = DirectCast(e.Item.FindControl("ddl_Employer"), DropDownList)
                '   Dim lbl_EmpName As Label = DirectCast(e.Item.FindControl("lbl_EmployerFilter"), Label)
                If ddl_Category IsNot Nothing Then
                    ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
                    Employer_Filter(ddl_Category.SelectedValue)
                    Panel1.Visible = True
                    lbl_EmployerFilter.Text = "List of Leads Assigned to user : " & ddl_Category.SelectedItem.Text
                    'Session("ddl_employer") = ddl_emp.SelectedValue

                    'qry = "select empid as  Emp_Id, empfname || '' || empmname || '' || emplname as Emp_Name from ams.ams_EMPLOYER"
                    'dt = getdata(qry, "QR")
                    'If dt.Rows.Count > 0 Then
                    '    ddl_emp.Items.Clear()
                    '    ddl_emp.Items.Add(New ListItem("Show All", ""))
                    '    ddl_emp.DataTextField = "Emp_Name"
                    '    ddl_emp.DataValueField = "Emp_Id"
                    '    ddl_emp.DataSource = dt
                    '    ddl_emp.DataBind()
                    'End If

                    'ddl_emp.SelectedValue = Session("ddl_employer")
                End If
        ElseIf e.CommandName = "followup" Then

            Label1.Text = "Add New Follow up"
            Ifrmfollowup.Attributes.Add("src", "followup_list.aspx?empid=" & e.CommandArgument.ToString())
            'ClientScript.RegisterStartupScript(Me.GetType(), "none", "<script>$('#addnewfollowup').modal('show');</script>", True)
            '  ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)
            'ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", True



            'ClientScript.RegisterStartupScript(Me.GetType(), "none", "<script>$('#addnewfollowup').modal('show');</script>", True)
            '  ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)
            'ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", True)
            ElseIf e.CommandName = "Filter" Then
                ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
                Employer_Filter(ddl_Category.SelectedValue)
            ElseIf e.CommandName = "ChangePageSize" Then
                ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
                Employer_Filter(ddl_Category.SelectedValue)
            ElseIf e.CommandName = "allowfilter" Then

                Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
                Dim fil As RadButton = TryCast(item.FindControl("BuiltinIconsButton2"), RadButton)



                '  ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
                ' getmastres()
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
    Protected Sub RadGrid1_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadGrid1.PreRender
        
    End Sub
    Private Sub RadGrid1_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles RadGrid1.PageIndexChanged
        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
        RadGrid1.CurrentPageIndex = e.NewPageIndex
        Employer_Filter(ddl_Category.SelectedValue)
    End Sub
    Protected Sub Btnref_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Button2.Click
        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
        Employer_Filter(ddl_Category.SelectedValue)
    End Sub
End Class
