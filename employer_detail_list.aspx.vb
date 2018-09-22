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
            ' getmastres()
            getBlank()
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
            If Session("lgnroll") = "S" Then
                qry = "select a.agntid as  agnt_Id, a.agntname as agnt_Name from ams.ams_agents a, ams.ams_usermaster b where b.lgnagntid=a.agntid and b.lgnsts='A' "
            Else
                qry = "select a.agntid as  agnt_Id, a.agntname as agnt_Name from ams.ams_agents a, ams.ams_usermaster b where b.lgnagntid=a.agntid and b.lgnsts='A' and  a.agntid='" & Session("lgnagntid") & "'"
            End If
            dt = clas.getdata(qry, "QR")
            If dt.Rows.Count > 0 Then
                '  Dim ddl_emp As DropDownList = DirectCast(RadGrid1.FindControl("ddl_Employer"), DropDownList)
                ddl_Employer.DataTextField = "agnt_Name"
                ddl_Employer.DataValueField = "agnt_Id"
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


    Private Sub Employer_Filter(ByVal EmpId As String)
        Try
            If Session("lgnroll") = "S" Then
                If EmpId <> "" Then
                    qry = "select ag.agntid, ag.agntname, emp.empfname||' '||emp.empmname||' '||emp.emplname as employer_Name,(select desgname from ams.ams_desgmaster where desgid=empdesg) as designation, emp.empcmpnam as company,emp.empeml as Email, substr(emp.empeml,0,5) || '*******' as Employer_Email, emp.empcmptelno as Company_Telno,to_char(emp.empcontdt,'dd-Mon-yyyy') as Initial_Contact,to_char(emp.lastfolup,'dd-Mon-yyyy') as Last_follup_date,(select statdescr from ams.ams_statmaster where statid=result and statscp=1) as result,(Select count(*) from ams.ams_followup w where w.empid=emp.empId and w.agentid=emp.agentid) as Tot_FollUp,emp.empid,NVL(emp.SCRNRATING,0) SCRNRATING, RESULT scp from ams.ams_employer emp  left outer join ams.ams_agents ag on ag.agntid=emp.Keyedby  where agentid='" & EmpId & "'  order by empcontdt desc"
                    'qry = "select empfname||' '||empmname||' '||emplname as employer_Name, (select desgname from ams.ams_desgmaster where desgid=empdesg) as designation, empcmpnam as company,empeml as Employer_Email, empcmptelno as Company_Telno,to_char(empcontdt,'dd-Mon-yyyy hh24:mi:ss') as Initial_Contact,to_char(lastfolup,'dd-Mon-yyyy hh24:mi:ss') as Last_follup_date,(select statdescr from ams.ams_statmaster where statid=result) as result,empid from ams.ams_employer where agentid='" & EmpId & "'  order by empcontdt desc"
                Else
                    qry = "select ag.agntid,ag.agntname, emp.empfname||' '||emp.empmname||' '||emp.emplname as employer_Name,(select desgname from ams.ams_desgmaster where desgid=empdesg) as designation, emp.empcmpnam as company,emp.empeml as Email, substr(emp.empeml,0,5) || '*******' as Employer_Email, emp.empcmptelno as Company_Telno,to_char(emp.empcontdt,'dd-Mon-yyyy') as Initial_Contact,to_char(emp.lastfolup,'dd-Mon-yyyy') as Last_follup_date,(select statdescr from ams.ams_statmaster where statid=result and statscp=1) as result,(Select count(*) from ams.ams_followup w where w.empid=emp.empId and w.agentid=emp.agentid) as Tot_FollUp,emp.empid,NVL(emp.SCRNRATING,0) SCRNRATING, RESULT scp from ams.ams_employer emp  left outer join ams.ams_agents ag on ag.agntid=emp.Keyedby order by empcontdt desc"
                    'qry = "select empfname||' '||empmname||' '||emplname as employer_Name, (select desgname from ams.ams_desgmaster where desgid=empdesg) as designation, empcmpnam as company,empeml as Employer_Email, empcmptelno as Company_Telno,to_char(empcontdt,'dd-Mon-yyyy hh24:mi:ss') as Initial_Contact,to_char(lastfolup,'dd-Mon-yyyy hh24:mi:ss') as Last_follup_date,(select statdescr from ams.ams_statmaster where statid=result) as result,empid from ams.ams_employer order by empcontdt desc"
                End If

            Else
                If EmpId <> "" Then
                    qry = "select ag.agntid,ag.agntname, emp.empfname||' '||emp.empmname||' '||emp.emplname as employer_Name,(select desgname from ams.ams_desgmaster where desgid=empdesg) as designation, emp.empcmpnam as company,emp.empeml as Email, substr(emp.empeml,0,5) || '*******' as Employer_Email, emp.empcmptelno as Company_Telno,to_char(emp.empcontdt,'dd-Mon-yyyy') as Initial_Contact,to_char(emp.lastfolup,'dd-Mon-yyyy') as Last_follup_date,(select statdescr from ams.ams_statmaster where statid=result and statscp=1) as result,(Select count(*) from ams.ams_followup w where w.empid=emp.empId and w.agentid=emp.agentid) as Tot_FollUp,emp.empid,NVL(emp.SCRNRATING,0) SCRNRATING, RESULT scp from ams.ams_employer emp  left outer join ams.ams_agents ag on ag.agntid=emp.Keyedby where agentid='" & EmpId & "' order by empcontdt desc"
                Else
                    qry = "select ag.agntid,ag.agntname, emp.empfname||' '||emp.empmname||' '||emp.emplname as employer_Name,(select desgname from ams.ams_desgmaster where desgid=empdesg) as designation, emp.empcmpnam as company,emp.empeml as Email, substr(emp.empeml,0,5) || '*******' as Employer_Email, emp.empcmptelno as Company_Telno,to_char(emp.empcontdt,'dd-Mon-yyyy') as Initial_Contact,to_char(emp.lastfolup,'dd-Mon-yyyy') as Last_follup_date,(select statdescr from ams.ams_statmaster where statid=result and statscp=1) as result,(Select count(*) from ams.ams_followup w where w.empid=emp.empId and w.agentid=emp.agentid) as Tot_FollUp,emp.empid,NVL(emp.SCRNRATING,0) SCRNRATING, RESULT scp from ams.ams_employer emp  left outer join ams.ams_agents ag on ag.agntid=emp.Keyedby where agentid='" & Session("lgnagntid") & "' order by empcontdt desc"
                End If



                'qry = "select empfname||' '||empmname||' '||emplname as employer_Name, (select desgname from ams.ams_desgmaster where desgid=empdesg) as designation, empcmpnam as company,empeml as Employer_Email, empcmptelno as Company_Telno,to_char(empcontdt,'dd-Mon-yyyy hh24:mi:ss') as Initial_Contact,to_char(lastfolup,'dd-Mon-yyyy hh24:mi:ss') as Last_follup_date,(select statdescr from ams.ams_statmaster where statid=result) as result,empid from ams.ams_employer where agentid='" & EmpId & "' order by empcontdt desc"
            End If
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

    Private Sub getBlank()
        qry = "select * from ams.ams_EmpData where 2=1"
        dt = clas.getdata(qry, "QR")
                  RadGrid1.DataSource = dt
            RadGrid1.DataBind()
       

    End Sub
    Private Sub getmastres()
        Try
            If Session("lgnroll") = "S" Then
                'qry = "select ag.agntname, emp.empfname||' '||emp.empmname||' '||emp.emplname as employer_Name,(select desgname from ams.ams_desgmaster where desgid=empdesg) as designation, emp.empcmpnam as company,emp.empeml as Email, substr(emp.empeml,0,5) || '*******' as Employer_Email, emp.empcmptelno as Company_Telno,to_char(emp.empcontdt,'dd-Mon-yyyy') as Initial_Contact,to_char(emp.lastfolup,'dd-Mon-yyyy') as Last_follup_date,(select statdescr from ams.ams_statmaster where statid=result and statscp=1) as result,(Select count(*) from ams.ams_followup w where w.empid=emp.empId and w.agentid=emp.agentid) as Tot_FollUp,emp.empid,NVL(emp.SCRNRATING,0) SCRNRATING, RESULT scp from ams.ams_employer emp  left outer join ams.ams_agents ag on ag.agntid=emp.Keyedby order by empcontdt desc"
                qry = "select * from ams.ams_EmpData"
                ' hh24:mi:ss
            Else
                'qry = "select ag.agntname, emp.empfname||' '||emp.empmname||' '||emp.emplname as employer_Name,(select desgname from ams.ams_desgmaster where desgid=empdesg) as designation, emp.empcmpnam as company,emp.empeml as Email, substr(emp.empeml,0,5) || '*******' as Employer_Email, emp.empcmptelno as Company_Telno,to_char(emp.empcontdt,'dd-Mon-yyyy') as Initial_Contact,to_char(emp.lastfolup,'dd-Mon-yyyy') as Last_follup_date,(select statdescr from ams.ams_statmaster where statid=result and statscp=1) as result,(Select count(*) from ams.ams_followup w where w.empid=emp.empId and w.agentid=emp.agentid) as Tot_FollUp,emp.empid,NVL(emp.SCRNRATING,0) SCRNRATING, RESULT scp from ams.ams_employer emp  left outer join ams.ams_agents ag on ag.agntid=emp.Keyedby where agentid='" & agentid & "' order by empcontdt desc"
                qry = "select * from ams.ams_EmpData where agentid='" & agentid & "'"
            End If
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

    'Protected Sub RadGrid1_NeedDataSource(sender As Object, e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGrid1.NeedDataSource
    '    getmastres()
    'End Sub


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
      

        RadGrid1.MasterTableView.GetColumn("agntid").Visible = False

        RadGrid1.MasterTableView.GetColumn("empid").Visible = False
        RadGrid1.MasterTableView.GetColumn("EMAIL").ItemStyle.Width = 0
        RadGrid1.MasterTableView.GetColumn("EMAIL").HeaderStyle.Width = 0
        RadGrid1.MasterTableView.GetColumn("Employer_Email").ItemStyle.Width = 0
        RadGrid1.MasterTableView.GetColumn("Employer_Email").HeaderStyle.Width = 0
        RadGrid1.MasterTableView.GetColumn("SCRNRATING").ItemStyle.Width = 0
        RadGrid1.MasterTableView.GetColumn("SCRNRATING").HeaderStyle.Width = 0
        RadGrid1.MasterTableView.GetColumn("scp").ItemStyle.Width = 0
        RadGrid1.MasterTableView.GetColumn("scp").HeaderStyle.Width = 0
			
        If Session("lgnroll") = "S" Then
         
           RadGrid1.MasterTableView.GetColumnSafe("Screening").Visible = True
        Else
		    RadGrid1.MasterTableView.GetColumnSafe("Screening").Visible = False
         
        End If

        'RadGrid1.MasterTableView.GetColumn("empid").ItemStyle.Width = 0
        'RadGrid1.MasterTableView.GetColumn("empid").HeaderStyle.Width = 0
        If (TypeOf e.Item Is GridDataItem) Then


            Dim item As GridDataItem = e.Item
            'Dim immcls As String = item.GetDataKeyValue("Immclass").ToString()
            Dim btnget As ImageButton = DirectCast(item.FindControl("btnget"), ImageButton)
            Dim btnedit As ImageButton = DirectCast(item.FindControl("btnedit"), ImageButton)
            Dim btnjob As ImageButton = DirectCast(item.FindControl("btnjob"), ImageButton)
            Dim btnint As ImageButton = DirectCast(item.FindControl("btnint"), ImageButton)


            If item("RESULT").Text = "&nbsp;" Then
                btnedit.Visible = False
                btnint.Visible = False
                btnget.Visible = False
                btnjob.Visible = False

            Else
                btnjob.Visible = True
                btnedit.Visible = True
                btnint.Visible = True
                btnget.Visible = True

            End If

            Dim scp As String = item("scp").Text.Replace(" ", "")

            If scp <> "1" Then

                'Dim a As HtmlAnchor = New HtmlAnchor()
                '  btnint.Visible = True
                btnint.ImageUrl = "Images/intersted.png"
                btnint.ToolTip = "Interested"
                btnedit.ImageUrl = "Images/edit-ico-small.png"
                btnedit.ToolTip = "Edit Record"
                btnedit.CommandName = "editform"
                btnedit.CommandArgument = item.GetDataKeyValue("empid").ToString()
                btnget.ImageUrl = "Images/followup-ico-small.png"
                btnget.ToolTip = "New Follow up" & " (" & item.Cells(12).Text & ") "
                btnget.CommandName = "followup"
                btnget.CommandArgument = item.GetDataKeyValue("empid").ToString()



                'Dim img As ImageButton = New ImageButton()
                If scp = "3" Then
                    btnjob.ImageUrl = "Images/secured-job-small.png"
                    btnjob.ToolTip = "My Job Orders"
                    btnjob.CommandName = "securedjob"
                    btnjob.CommandArgument = item.GetDataKeyValue("empid").ToString()


                    ''    'a.HRef = "#addnewsecuredjoborder"
                    ''    'a.Attributes.Add("data-toggle", "modal")
                    ''    'Ifrmfollowup.Attributes.Add("src", "securedjob_list.aspx?empid=" & item.GetDataKeyValue("empid").ToString())
                    ''    img.ImageUrl = "Images/magnifier.png"
                    ''    img.CommandName = "securedjob"
                    ''    img.CommandArgument = item.GetDataKeyValue("empid").ToString()
                ElseIf scp = "2" Then
                    btnget.ImageUrl = "Images/followup-ico-small.png"
                    btnget.ToolTip = "New Follow up" & " (" & item.Cells(12).Text & ") "
                    btnget.CommandName = "followup"
                    btnget.CommandArgument = item.GetDataKeyValue("empid").ToString()
                    btnjob.ImageUrl = "Images/not-secured-job-small.png"
                    btnjob.ImageUrl = "Images/secured-job-small.png"
                    btnjob.ToolTip = "My Job Orders"
                    btnjob.CommandName = "securedjob"
                    btnjob.CommandArgument = item.GetDataKeyValue("empid").ToString()



                ElseIf scp = "1" Then

                    btnint.ImageUrl = "Images/not-intersted.png"
                    btnint.ToolTip = "Not Interested"
                    btnedit.ImageUrl = "Images/edit-ico-small.png"
                    btnedit.ToolTip = "Edit Record"
                    btnedit.CommandName = "editform"
                    btnedit.CommandArgument = item.GetDataKeyValue("empid").ToString()
                    btnget.ImageUrl = "Images/followup-ico-small.png"
                    btnget.ToolTip = "New Follow up" & " (" & item.Cells(12).Text & ") "
                    btnget.CommandName = "followup"
                    btnget.CommandArgument = item.GetDataKeyValue("empid").ToString()
                    btnjob.ImageUrl = "Images/secured-job-small.png"
                    btnjob.ToolTip = "My Job Orders"
                    btnjob.CommandName = "securedjob"
                    btnjob.CommandArgument = item.GetDataKeyValue("empid").ToString()



                    btnint.ImageUrl = "Images/not-intersted.png"
                    btnint.ToolTip = "Not Interested"


                    '    '  a.HRef = "#addnewfollowup"
                    ''    'img.Attributes.Add("data-toggle", "modal")
                    ''    'Ifrmfollowup.Attributes.Add("src", "securedjob_list.aspx?empid=" & item.GetDataKeyValue("empid").ToString())
                    ''    
                    ''    img.CommandName = "followup"
                    ''    img.CommandArgument = item.GetDataKeyValue("empid").ToString()
                    ''    'a.Attributes.Add("onclick", AddressOf )
                    ''    AddHandler img.Click, AddressOf ImageButton_Click
                    ''    'a.Attributes.Add("onclick", "onfollow()")
                Else
                    ''    a.HRef = "#"
                    ''    img.ImageUrl = ""
                    ''End If
                    'img.ID = "img1"
                    'img.ImageUrl = "Images/magnifier.png"
                    'img.CommandName = "followup"
                    'img.CommandArgument = item.GetDataKeyValue("empid").ToString()
                    btnget.ImageUrl = "Images/followup-ico-small.png"
                    btnget.ToolTip = "New Follow up" & " (" & item.Cells(12).Text & ") "
                    btnget.CommandName = "followup"
                    btnget.CommandArgument = item.GetDataKeyValue("empid").ToString()
                    btnjob.ImageUrl = "Images/secured-job-small.png"
                    btnjob.ToolTip = "My Job Orders"
                    btnjob.CommandName = "securedjob"
                    btnjob.CommandArgument = item.GetDataKeyValue("empid").ToString()



                    btnint.ImageUrl = "Images/intersted.png"
                    btnint.ToolTip = "Interested"
                    'item.Cells(8).Controls.Add(img)
                End If
            Else
                btnint.ImageUrl = "Images/not-intersted.png"
                btnint.ToolTip = "Not Interested"
                btnedit.ImageUrl = "Images/edit-ico-small.png"
                btnedit.ToolTip = "Edit Record"
                btnedit.CommandName = "editform"
                btnedit.CommandArgument = item.GetDataKeyValue("empid").ToString()
                btnget.ImageUrl = "Images/followup-ico-small.png"
                btnget.ToolTip = "New Follow up" & " (" & item.Cells(12).Text & ") "
                btnget.CommandName = "followup"
                btnget.CommandArgument = item.GetDataKeyValue("empid").ToString()
                btnjob.ImageUrl = "Images/secured-job-small.png"
                btnjob.ToolTip = "My Job Orders"
                btnjob.CommandName = "securedjob"
                btnjob.CommandArgument = item.GetDataKeyValue("empid").ToString()



                btnint.ImageUrl = "Images/not-intersted.png"
                btnint.ToolTip = "Not Interested"
                'btnjob.ImageUrl = "Images/not-secured-job-small.png"

            End If


          



            'For Each cell As GridColumn In item.Cells
            '    cell.FilterDelay = "200"
            'Next

        End If
    End Sub

    'Sub ImageButton_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
    '    Ifrmfollowup.Attributes.Add("src", "followup_list.aspx?empid=4")

    '    ClientScript.RegisterClientScriptBlock([GetType](), "none", "<script>$('#addnewfollowup').modal('show');</script>", False)
    'End Sub
    Protected Sub RadGrid1_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.ItemCommand
        If e.CommandName = "securedjob" Then
            Label1.Text = "My Job Orders"
            Ifrmfollowup.Attributes.Add("src", "jobsecure_list.aspx?empid=" & e.CommandArgument.ToString())
            '  ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)
            'ClientScript.RegisterClientScriptBlock([GetType](), "none", "openModal();", False)
            'ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", True)
        ElseIf e.CommandName = "followup" Then
            Dim item As GridDataItem = DirectCast(e.Item, GridDataItem)
            Dim aid = item.GetDataKeyValue("agntid").ToString()

            Label1.Text = "Add New Follow up"
            Ifrmfollowup.Attributes.Add("src", "followup_list.aspx?empid=" & e.CommandArgument.ToString() & "&agentid=" & aid.ToString)
            'ClientScript.RegisterStartupScript(Me.GetType(), "none", "<script>$('#addnewfollowup').modal('show');</script>", True)
            '  ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)
            'ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", True)

        ElseIf e.CommandName = "sndeml" Then
            Label1.Text = "Compose Email"
            Ifrmfollowup.Attributes.Add("src", "http://115.249.129.5:5013/commail/sendmail?empno=" & Session("lgnagntid") & "&F_emp=" & Convert.ToString(Session("lgneml")) & "&T_emp=" & Convert.ToString(e.CommandArgument) & "&camsuid=1")
            ' Ifrmfollowup.Attributes.Add("src", "http://115.249.129.5:5013/commail/sendmail?empno=159&F_emp=" & Convert.ToString(Session("lgneml")) & "&T_emp=rahul.gupta273@gmail.com&camsuid=0")
            'ClientScript.RegisterStartupScript(Me.GetType(), "none", "<script>$('#addnewfollowup').modal('show');</script>", True)
            '  ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)
            'ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", True)

        ElseIf e.CommandName = "addscreening" Then
            Label1.Text = "Screening Record"
			
			  If Session("lgnroll") = "S" Then
           Ifrmfollowup.Attributes.Add("src", "employer_det.aspx?empid=" & e.CommandArgument.ToString() & "&mode=edit&s=rating")

        Else
            Ifrmfollowup.Attributes.Add("src", "employer_det.aspx?empid=" & e.CommandArgument.ToString() & "&mode=edit&s=rating&typ=D")
        End If

			
           
            'ClientScript.RegisterStartupScript(Me.GetType(), "none", "<script>$('#addnewfollowup').modal('show');</script>", True)
            '  ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)
            'ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", True)

        ElseIf e.CommandName = "editform" Then
            Label1.Text = "Edit Prospect Record"
            Ifrmfollowup.Attributes.Add("src", "employer_det.aspx?empid=" & e.CommandArgument.ToString() & "&mode=edit")
            'ClientScript.RegisterStartupScript(Me.GetType(), "none", "<script>$('#addnewfollowup').modal('show');</script>", True)
            '  ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)
            'ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", True)
       
        ElseIf e.CommandName = "refresh" Then
            For Each column As GridColumn In RadGrid1.MasterTableView.RenderColumns
                column.CurrentFilterFunction = GridKnownFunction.NoFilter
                column.CurrentFilterValue = String.Empty
            Next


            RadGrid1.MasterTableView.FilterExpression = String.Empty
            RadGrid1.Rebind()
            Label1.Text = ""
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            getmastres()
            'Ifrmfollowup.Attributes.Add("src", "followup_list.aspx?empid=" & e.CommandArgument.ToString())

            'ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)
        ElseIf e.CommandName = "EmployerFilter" Then

           ' Dim ddl_emp As DropDownList = DirectCast(e.Item.FindControl("ddl_Employer"), DropDownList)
         '   Dim lbl_EmpName As Label = DirectCast(e.Item.FindControl("lbl_EmployerFilter"), Label)
            If ddl_Employer IsNot Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
                Employer_Filter(ddl_Employer.SelectedValue)
                  Panel1.Visible = True
                lbl_EmployerFilter.Text = "List of Leads Assigned to user : " & ddl_Employer.SelectedItem.Text
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
    Protected Sub RadGrid1_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadGrid1.PreRender
        If Page.IsPostBack = False Then
            Dim column As GridColumn = TryCast(RadGrid1.MasterTableView.GetColumnSafe("eid"), GridColumn)
            column.OrderIndex = RadGrid1.MasterTableView.RenderColumns.Length - 5
          

            Dim column1 As GridColumn = TryCast(RadGrid1.MasterTableView.GetColumnSafe("Screening"), GridColumn)
            column1.OrderIndex = RadGrid1.MasterTableView.RenderColumns.Length

            RadGrid1.MasterTableView.Rebind()
        End If
    End Sub
    Private Sub RadGrid1_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles RadGrid1.PageIndexChanged
        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
        RadGrid1.CurrentPageIndex = e.NewPageIndex
        Employer_Filter(ddl_Employer.SelectedValue)
    End Sub

    Protected Sub Btnref_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Button2.Click
        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
        RadGrid1.CurrentPageIndex = 0
        Employer_Filter(ddl_Employer.SelectedValue)

    End Sub
End Class
