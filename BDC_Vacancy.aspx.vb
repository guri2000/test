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
            getmastres()
            ' getBlank()
        Else

        End If

    End Sub

    Private Sub getBlank()
        qry = "select * from ams.ADV_Vacancy where 1=0"
        dt = clas.getdata(qry, "QR")
        RadGrid1.DataSource = dt
        RadGrid1.DataBind()
    End Sub

    Private Sub getmastres()
        Try
        
            qry = "select * from ams.ADV_Vacancy"
            qry = "SELECT VACID,  VACNAME as Vacancy, (select name from tt.assforcountry  where transid=LOCID and active='A' ) as Location , TO_CHAR(VACOPNDT, 'DD-Mon-YYYY') as Opening_Date, (select statdescr from ams.ams_statmaster where statid=status) as CStatus, TO_CHAR(VACTTCLS, 'DD-Mon-YYYY') as Closing_Date ,OPENING, 0 as FILLED,  VACACTCLS,    (select agntname from ams.ams_agents where agntid=KEYEDBY) as KEYEDBY,  TO_CHAR(KEYEDON, 'DD-Mon-YYYY') KEYEDON,  KEYSKILLS, REMARKS,status  FROM AMS.ADV_VACANCY"


            ' qry = "select agntname as Agent_Name, (select designation from ranu.desgmaster where desgid=agntdesg) as designation,agnteml as Agent_Email, agnttelno as Agent_Mobile,lgnnam as Login_Name,decode(lgnsts,'A','Active','In-Active') Account_status,decode(lgnroll,'S','Adminstrator','Agent') Account_Acess, to_char(lastlogin,'dd-Mon-yy HH24:mi:ss') as last_login, agntid from ams.ams_agents a, ams.ams_usermaster b where a.agntid=b.lgnagntid"
            dt = clas.getdata(qry, "QR")
            If dt.Rows.Count > 0 Then
                'Dim dccol11 = New DataColumn("Action", GetType(String))
                'dt.Columns.Add(dccol11)
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
    'Protected Sub RadGrid1_ItemCommand(ByVal source As Object, ByVal e As GridCommandEventArgs) Handles RadGrid1.ItemCommand
    '    If e.CommandName = RadGrid.FilterCommandName Then
    '        Dim filterPair As Pair = DirectCast(e.CommandArgument, Pair)
    '        ' gridMessage1 = "Current Filter function: '" + filterPair.First + "' for column '" + filterPair.Second + "'"
    '        Dim filterBox As TextBox = CType((CType(e.Item, GridFilteringItem))(filterPair.Second.ToString()).Controls(0), TextBox)
    '        '   gridMessage1 = "<br> Entered pattern for search: " + filterBox.Text
    '    End If
    'End Sub
    'Private gridMessage1 As String = Nothing
    'Private gridMessage2 As String = Nothing
    'Protected Sub RadGrid1_DataBound(ByVal sender As Object, ByVal e As EventArgs) Handles RadGrid1.DataBound
    '    If Not String.IsNullOrEmpty(gridMessage1) Then
    '        DisplayMessage(gridMessage1)
    '        DisplayMessage(gridMessage2)
    '    End If
    'End Sub
    'Private Sub DisplayMessage(ByVal text As String)
    '    RadGrid1.Controls.Add(New LiteralControl(String.Format("<span style='color:red'>{0}</span>", text)))
    'End Sub
    Protected Sub RadGrid1_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid1.ItemDataBound

      
        'If (TypeOf e.Item Is GridHeaderItem) Then
        '    Dim item As GridHeaderItem = e.Item
        '    item.Cells(10).Width = 0
        'End If
        RadGrid1.MasterTableView.GetColumn("VACID").Visible = False
        RadGrid1.MasterTableView.GetColumn("VACACTCLS").Visible = False
        RadGrid1.MasterTableView.GetColumn("status").Visible = False
        RadGrid1.MasterTableView.GetColumn("OPENING_DATE").HeaderText = "OPEN SINCE"
        RadGrid1.MasterTableView.GetColumn("CSTATUS").HeaderText = "STATUS"
        RadGrid1.MasterTableView.GetColumn("CLOSING_DATE").HeaderText = "CLOSED DATE"



        'RadGrid1.MasterTableView.GetColumn("PWD").ItemStyle.Width = 0
        'RadGrid1.MasterTableView.GetColumn("PWD").HeaderStyle.Width = 0
        If (TypeOf e.Item Is GridPagerItem) Then
            'Dim PageSizeCombo As RadComboBox = DirectCast(e.Item.FindControl("RadComboBox1"), RadComboBox)
            'PageSizeCombo.Items(0).Text = "100"
            'PageSizeCombo.Items(0).Value = "100"
            'PageSizeCombo.Items(1).Text = "100"
            'PageSizeCombo.Items(2).Text = "100"
            ''  PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = True
        End If



           

        If (TypeOf e.Item Is GridDataItem) Then


            Dim item As GridDataItem = e.Item
            Dim btnedit As ImageButton = DirectCast(item.FindControl("btnedit"), ImageButton)
            Dim btnCand As ImageButton = DirectCast(item.FindControl("btnCand"), ImageButton)
            Dim btnget As ImageButton = DirectCast(item.FindControl("btnget"), ImageButton)

			
			
            'If item.Cells(3).Text = "&nbsp;" Then
            '    btnedit.Visible = False
            'Else
            '    btnedit.Visible = True
            'End If

            btnedit.ImageUrl = "Images/edit-ico-small.png"
            btnedit.ToolTip = "Edit Vacancy"
            btnedit.CommandName = "editform"
            btnedit.CommandArgument = item.GetDataKeyValue("vacid").ToString()
			
            btnget.ImageUrl = "Images/publishing_ico.png"
            btnget.ToolTip = "Add Advertisement"
            btnget.CommandName = "AdvDetail"
            btnget.CommandArgument = item.GetDataKeyValue("vacid").ToString()
            'btnget.CommandArgument = item.GetDataKeyValue("advid").ToString()


           
           
            ' btnedit.CommandArgument = item.GetDataKeyValue("advid").ToString()

         

            btnCand.ImageUrl = "Images/add_candates.png"
            btnCand.ToolTip = "Add Candidate"
            btnCand.CommandName = "CandDetail"
            btnCand.CommandArgument = item.GetDataKeyValue("vacid").ToString()


            'If item("ACCOUNT_ACCESS").Text = "Adminstrator" Then
            '  tipimg.Visible = false
            'Else
            '            tipimg.Visible = True

            'End if 
			
			
			
			
            'Dim immcls As String = item.GetDataKeyValue("Immclass").ToString()
            'Dim btnget As ImageButton = DirectCast(item.FindControl("btnget"), ImageButton)
            '
            'If item.Cells(9).Text <> "Not Interested" Then

            '    'Dim a As HtmlAnchor = New HtmlAnchor()
            '    btnedit.ImageUrl = "Images/edit-ico-small.png"
            '    btnedit.ToolTip = "Edit Employer Record"
            '    btnedit.CommandName = "editform"
            '    btnedit.CommandArgument = item.GetDataKeyValue("empid").ToString()
            '    'Dim img As ImageButton = New ImageButton()
            '    If item.Cells(9).Text = "Secured Job Order" Then
            '        btnget.ImageUrl = "Images/secured-job-small.png"
            '        btnget.ToolTip = "Add New Job Order"
            '        btnget.CommandName = "securedjob"
            '        btnget.CommandArgument = item.GetDataKeyValue("empid").ToString()
            '        ''    'a.HRef = "#addnewsecuredjoborder"
            '        ''    'a.Attributes.Add("data-toggle", "modal")
            '        ''    'Ifrmfollowup.Attributes.Add("src", "securedjob_list.aspx?empid=" & item.GetDataKeyValue("empid").ToString())
            '        ''    img.ImageUrl = "Images/magnifier.png"
            '        ''    img.CommandName = "securedjob"
            '        ''    img.CommandArgument = item.GetDataKeyValue("empid").ToString()
            '    ElseIf item.Cells(9).Text = "Wants More Information" Then
            '        btnget.ImageUrl = "Images/followup-ico-small.png"
            '        btnget.ToolTip = "Add New Follow up"
            '        btnget.CommandName = "followup"
            '        btnget.CommandArgument = item.GetDataKeyValue("empid").ToString()
            '        ''    '  a.HRef = "#addnewfollowup"
            '        ''    'img.Attributes.Add("data-toggle", "modal")
            '        ''    'Ifrmfollowup.Attributes.Add("src", "securedjob_list.aspx?empid=" & item.GetDataKeyValue("empid").ToString())
            '        ''    
            '        ''    img.CommandName = "followup"
            '        ''    img.CommandArgument = item.GetDataKeyValue("empid").ToString()
            '        ''    'a.Attributes.Add("onclick", AddressOf )
            '        ''    AddHandler img.Click, AddressOf ImageButton_Click
            '        ''    'a.Attributes.Add("onclick", "onfollow()")
            '    Else
            '        ''    a.HRef = "#"
            '        ''    img.ImageUrl = ""
            '        ''End If
            '        'img.ID = "img1"
            '        'img.ImageUrl = "Images/magnifier.png"
            '        'img.CommandName = "followup"
            '        'img.CommandArgument = item.GetDataKeyValue("empid").ToString()

            '        btnget.Visible = False
            '        'item.Cells(8).Controls.Add(img)
            '    End If
            'Else
            '    btnedit.ImageUrl = "Images/edit-ico-small.png"
            '    btnedit.ToolTip = "Edit Employer Record"
            '    btnedit.CommandName = "editform"
            '    btnedit.CommandArgument = item.GetDataKeyValue("empid").ToString()
            '    btnget.Visible = False
            'End If




        End If
    End Sub
    'Sub ImageButton_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
    '    Ifrmfollowup.Attributes.Add("src", "followup_list.aspx?empid=4")

    '    ClientScript.RegisterClientScriptBlock([GetType](), "none", "<script>$('#addnewfollowup').modal('show');</script>", False)
    'End Sub
    Protected Sub RadGrid1_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.ItemCommand
        'If e.CommandName = "securedjob" Then

        '    Ifrmfollowup.Attributes.Add("src", "securedjob_list.aspx?empid=" & e.CommandArgument.ToString())
        '    ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)
        '    'ClientScript.RegisterClientScriptBlock([GetType](), "none", "openModal();", False)
        '    'ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", True)
        'ElseIf e.CommandName = "followup" Then
        '    Ifrmfollowup.Attributes.Add("src", "followup_list.aspx?empid=" & e.CommandArgument.ToString())
        '    'ClientScript.RegisterStartupScript(Me.GetType(), "none", "<script>$('#addnewfollowup').modal('show');</script>", True)
        '    ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)
        '    'ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", True)
        'Else
        If e.CommandName = "editform" Then
            edtti.InnerText = "Edit Vacancy Information"
            Ifrmfollowup.Attributes.Add("src", "bdc_inv_vacpop.aspx?vacid=" & e.CommandArgument.ToString() & "&mode=edit")
            'ClientScript.RegisterStartupScript(Me.GetType(), "none", "<script>$('#addnewfollowup').modal('show');</script>", True)
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)
            'ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", True)
        ElseIf e.CommandName = "Filter" Then
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            getmastres()
        ElseIf e.CommandName = "AdvDetail" Then
            edtti.InnerText = "Add Advertisement"
            Ifrmfollowup.Attributes.Add("src", "bdc_inv_Advpop.aspx?vacid=" & e.CommandArgument.ToString())
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)

        ElseIf e.CommandName = "CandDetail" Then
            edtti.InnerText = "Add Candidate"
            Ifrmfollowup.Attributes.Add("src", "bdc_inv_Candpop.aspx?vacid=" & e.CommandArgument.ToString())
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)
        ElseIf e.CommandName = "refresh" Then
            For Each column As GridColumn In RadGrid1.MasterTableView.RenderColumns
                column.CurrentFilterFunction = GridKnownFunction.NoFilter
                column.CurrentFilterValue = String.Empty
            Next


            RadGrid1.MasterTableView.FilterExpression = String.Empty
            RadGrid1.Rebind()
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            getmastres()
            'Ifrmfollowup.Attributes.Add("src", "followup_list.aspx?empid=" & e.CommandArgument.ToString())

            'ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)
        ElseIf e.CommandName = "ChangePageSize" Then
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            getmastres()
        ElseIf e.CommandName = "Reset" Then
            edtti.InnerText = "Reset Password"
            Ifrmfollowup.Attributes.Add("src", "ResetPwd.aspx?empid=" & e.CommandArgument.ToString() & "&mode=edit")
            ' Ifrmfollowup.Attributes.Add("src", "ResetPwd.aspx?empid=" & e.CommandArgument.ToString() & "&mode=edit")
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)
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
    Protected Sub RadGrid1_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadGrid1.PreRender
        'RadGrid1.MasterTableView.IsItemInserted = True
        '    RadGrid1.Rebind()
        'If Page.IsPostBack = False Then
        '    RadGrid1.EditIndexes.Add(0)
        '    RadGrid1.Rebind()
        'End If
    End Sub
    Private Sub RadGrid1_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles RadGrid1.PageIndexChanged
        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
        RadGrid1.CurrentPageIndex = e.NewPageIndex

        getmastres()
    End Sub
End Class
