Imports System.Data
Imports System.IO
Imports Telerik.Web.UI
Imports System.Text
Imports System.Windows.Forms

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

        Else

        End If

    End Sub


    Private Sub getmastres()
        Try
            qry = "SELECT CANDID,CANDNAME as Name,CANDPHN as Phone,CANDEML as Email,CANDADDRESS as Address, to_char(RESMRECDDAT,'dd-Mon-yyyy') ReceivedOn, RESUMEPATH,(select substr(advdesc,0,50) from ams.adv_master t1 where t1.advid=a.ADVID) Advertisement, (SELECT STATDESCR FROM AMS.AMS_STATMASTER where statstatus='A' and STATID=PLTFORMID ) PLATFORM,(SELECT ROUNDNAME FROM AMS.BDC_RND_MST WHERE ROUNDID=CANDRND) AS ROUND,  to_char(KEYEDON,'dd-Mon-yyyy') keyedon, (select agntname from ams.ams_agents where agntid=KEYEDBY) as KEYEDBY     FROM AMS.BDC_LEAD_MASTER a"

            dt = clas.getdata(qry, "QR")
            If dt.Rows.Count > 0 Then
                RadGrid1.DataSource = dt
                RadGrid1.DataBind()
            Else
                Dim drnewrow As DataRow = dt.NewRow
                dt.Rows.Add(drnewrow)
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

    Private Sub Employer_Filter(ByVal EmpId As String)
        Try
            If Session("lgnroll") = "S" Then
                If EmpId = "1" Then
                    qry = "SELECT CANDID,CANDNAME as Name,CANDPHN as Phone,CANDEML as Email,CANDADDRESS as Address, to_char(RESMRECDDAT,'dd-Mon-yyyy') ReceivedOn, RESUMEPATH,(select substr(advdesc,0,50) from ams.adv_master t1 where t1.advid=a.ADVID) Advertisement, (SELECT STATDESCR FROM AMS.AMS_STATMASTER where statstatus='A' and STATID=PLTFORMID ) PLATFORM,(SELECT ROUNDNAME FROM AMS.BDC_RND_MST WHERE ROUNDID=CANDRND) AS ROUND,  to_char(KEYEDON,'dd-Mon-yyyy') keyedon, (select agntname from ams.ams_agents where agntid=KEYEDBY) as KEYEDBY     FROM AMS.BDC_LEAD_MASTER a where candresult=110"
                ElseIf EmpId = "2" Then
                    qry = "SELECT CANDID,CANDNAME as Name,CANDPHN as Phone,CANDEML as Email,CANDADDRESS as Address, to_char(RESMRECDDAT,'dd-Mon-yyyy') ReceivedOn, RESUMEPATH,(select substr(advdesc,0,50) from ams.adv_master t1 where t1.advid=a.ADVID) Advertisement, (SELECT STATDESCR FROM AMS.AMS_STATMASTER where statstatus='A' and STATID=PLTFORMID ) PLATFORM,(SELECT ROUNDNAME FROM AMS.BDC_RND_MST WHERE ROUNDID=CANDRND) AS ROUND,  to_char(KEYEDON,'dd-Mon-yyyy') keyedon, (select agntname from ams.ams_agents where agntid=KEYEDBY) as KEYEDBY     FROM AMS.BDC_LEAD_MASTER a where candresult=106"
                Else
                    qry = "SELECT CANDID,CANDNAME as Name,CANDPHN as Phone,CANDEML as Email,CANDADDRESS as Address, to_char(RESMRECDDAT,'dd-Mon-yyyy') ReceivedOn, RESUMEPATH,(select substr(advdesc,0,50) from ams.adv_master t1 where t1.advid=a.ADVID) Advertisement, (SELECT STATDESCR FROM AMS.AMS_STATMASTER where statstatus='A' and STATID=PLTFORMID ) PLATFORM,(SELECT ROUNDNAME FROM AMS.BDC_RND_MST WHERE ROUNDID=CANDRND) AS ROUND,  to_char(KEYEDON,'dd-Mon-yyyy') keyedon, (select agntname from ams.ams_agents where agntid=KEYEDBY) as KEYEDBY     FROM AMS.BDC_LEAD_MASTER a "
                    'qry = "select empfname||' '||empmname||' '||emplname as employer_Name, (select desgname from ams.ams_desgmaster where desgid=empdesg) as designation, empcmpnam as company,empeml as Employer_Email, empcmptelno as Company_Telno,to_char(empcontdt,'dd-Mon-yyyy hh24:mi:ss') as Initial_Contact,to_char(lastfolup,'dd-Mon-yyyy hh24:mi:ss') as Last_follup_date,(select statdescr from ams.ams_statmaster where statid=result) as result,empid from ams.ams_employer order by empcontdt desc"
                End If

            Else
                'If EmpId <> "" Then
                '    qry = "select ag.agntname, emp.empfname||' '||emp.empmname||' '||emp.emplname as employer_Name,(select desgname from ams.ams_desgmaster where desgid=empdesg) as designation, emp.empcmpnam as company,emp.empeml as Email, substr(emp.empeml,0,5) || '*******' as Employer_Email, emp.empcmptelno as Company_Telno,to_char(emp.empcontdt,'dd-Mon-yyyy') as Initial_Contact,to_char(emp.lastfolup,'dd-Mon-yyyy') as Last_follup_date,(select statdescr from ams.ams_statmaster where statid=result and statscp=1) as result,(Select count(*) from ams.ams_followup w where w.empid=emp.empId and w.agentid=emp.agentid) as Tot_FollUp,emp.empid,NVL(emp.SCRNRATING,0) SCRNRATING, RESULT scp from ams.ams_employer emp  left outer join ams.ams_agents ag on ag.agntid=emp.Keyedby where agentid='" & EmpId & "' order by empcontdt desc"
                'Else
                '    qry = "select ag.agntname, emp.empfname||' '||emp.empmname||' '||emp.emplname as employer_Name,(select desgname from ams.ams_desgmaster where desgid=empdesg) as designation, emp.empcmpnam as company,emp.empeml as Email, substr(emp.empeml,0,5) || '*******' as Employer_Email, emp.empcmptelno as Company_Telno,to_char(emp.empcontdt,'dd-Mon-yyyy') as Initial_Contact,to_char(emp.lastfolup,'dd-Mon-yyyy') as Last_follup_date,(select statdescr from ams.ams_statmaster where statid=result and statscp=1) as result,(Select count(*) from ams.ams_followup w where w.empid=emp.empId and w.agentid=emp.agentid) as Tot_FollUp,emp.empid,NVL(emp.SCRNRATING,0) SCRNRATING, RESULT scp from ams.ams_employer emp  left outer join ams.ams_agents ag on ag.agntid=emp.Keyedby where agentid='" & Session("lgnagntid") & "' order by empcontdt desc"
                'End If



                'qry = "select empfname||' '||empmname||' '||emplname as employer_Name, (select desgname from ams.ams_desgmaster where desgid=empdesg) as designation, empcmpnam as company,empeml as Employer_Email, empcmptelno as Company_Telno,to_char(empcontdt,'dd-Mon-yyyy hh24:mi:ss') as Initial_Contact,to_char(lastfolup,'dd-Mon-yyyy hh24:mi:ss') as Last_follup_date,(select statdescr from ams.ams_statmaster where statid=result) as result,empid from ams.ams_employer where agentid='" & EmpId & "' order by empcontdt desc"
            End If
            dt = clas.getdata(qry, "QR")
            If dt.Rows.Count > 0 Then
                'Dim dccol11 = New DataColumn("Action", GetType(String))
                'dt.Columns.Add(dccol11)
                RadGrid1.DataSource = dt
                RadGrid1.DataBind()
            Else
                Dim drnewrow As DataRow = dt.NewRow
                dt.Rows.Add(drnewrow)
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
        RadGrid1.MasterTableView.GetColumn("CANDID").Visible = False
        RadGrid1.MasterTableView.GetColumn("RESUMEPATH").Visible = False

        'RadGrid1.MasterTableView.GetColumn("status").Visible = False

        'RadGrid1.MasterTableView.GetColumn("PWD").ItemStyle.Width = 0
        'RadGrid1.MasterTableView.GetColumn("PWD").HeaderStyle.Width = 0
        'If (TypeOf e.Item Is GridPagerItem) Then
        '    'Dim PageSizeCombo As RadComboBox = DirectCast(e.Item.FindControl("RadComboBox1"), RadComboBox)
        '    'PageSizeCombo.Items(0).Text = "100"
        '    'PageSizeCombo.Items(0).Value = "100"
        '    'PageSizeCombo.Items(1).Text = "100"
        '    'PageSizeCombo.Items(2).Text = "100"
        '    ''  PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = True
        'End If





        If (TypeOf e.Item Is GridDataItem) Then


            Dim item As GridDataItem = e.Item
            Dim btnget As LinkButton = DirectCast(item.FindControl("lnkshowResume"), LinkButton)

            btnget.Text = "Download Resume"
            btnget.Enabled = True
            btnget.CommandName = "dwnldresume"

            'If item.Cells(3).Text = "&nbsp;" Then
            '    btnedit.Visible = False
            'Else
            '    btnedit.Visible = True
            'End If

            'btnedit.ImageUrl = "Images/edit-ico-small.png"
            'btnedit.ToolTip = "Edit Agent Record"
            'btnedit.CommandName = "editform"
            'btnedit.CommandArgument = item.GetDataKeyValue("vacid").ToString()


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

        ElseIf e.CommandName = "dwnldresume" Then
            If TypeOf e.Item Is GridDataItem Then
                Dim item As GridDataItem = DirectCast(e.Item, GridDataItem)


                Dim id As String = item.GetDataKeyValue("CANDID").ToString()
                'Dim authdat As String = item("AUTHDAT").Text              
                '    Dim resauthstat As String = item("RESAUTHSTAT").Text

                Dim fname As String = clas.ExecuteScalar("select RESUMEPATH from  AMS.BDC_LEAD_MASTER  where candid = '" & id & "'")
                'Response.Redirect("~/Resume/" + fname)

                If fname.Trim().Length > 0 Then

                    If File.Exists(Server.MapPath("~/BDC_Resume/" & fname)) Then
                        Response.ContentType = "application/octet-stream"
                        Response.AppendHeader("Content-Disposition", "attachment;filename=" & fname)
                        Response.TransmitFile(Server.MapPath("~/BDC_Resume/" & fname))
                        Response.End()
                    Else
                        RadWindowManager1.RadAlert("<ul><li><div style=color:red;>System is unable to found any document for download...</div></li></ul>", 300, 100, "Validation Exception Failure", Nothing)
                        Exit Sub
                    End If

                Else
                    RadWindowManager1.RadAlert("<ul><li><div style=color:red;>System is unable to found any document for download.</div></li></ul>", 300, 100, "Validation Exception Failure", Nothing)
                    Exit Sub
                End If


            End If

        End If

    End Sub
    'Protected Sub RadButton1_ToggleStateChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.ButtonToggleStateChangedEventArgs)

    '    Dim btn As RadButton = TryCast(sender, RadButton)

    '    If e.SelectedToggleState.Value = "T" Then
    '        RadGrid1.AllowFilteringByColumn = True
    '    ElseIf e.SelectedToggleState.Value = "F" Then
    '        RadGrid1.AllowFilteringByColumn = False
    '        End If


    'End Sub
    Protected Sub RadGrid1_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadGrid1.PreRender
        If Page.IsPostBack = False Then
            Dim column As GridColumn = TryCast(RadGrid1.MasterTableView.GetColumnSafe("res"), GridColumn)
            column.OrderIndex = RadGrid1.MasterTableView.RenderColumns.Length
            RadGrid1.MasterTableView.Rebind()
        End If
    End Sub
    Private Sub RadGrid1_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles RadGrid1.PageIndexChanged
        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
        RadGrid1.CurrentPageIndex = e.NewPageIndex

        getmastres()
    End Sub
    Protected Sub Btnref_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Button2.Click
        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
        RadGrid1.CurrentPageIndex = 0
        Employer_Filter(ddl_Employer.SelectedValue)

    End Sub
End Class
