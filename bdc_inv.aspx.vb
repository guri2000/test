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
            getmastres4()
            fillblank()
            RadTabStrip2.SelectedIndex = 0
        End If

    End Sub

    Private Sub Bind_Employer()
        Try
            qry = "select * from AMS.AMS_CATMASTER where catsts='A'"
            'If Session("lgnroll") = "S" Then
            'Else
            '    qry &= " and  a.agntid='" & Session("lgnagntid") & "'"
            'End If
            dt = clas.getdata(qry, "QR")
            If dt.Rows.Count > 0 Then

                ddl_Employer.DataTextField = "catnam"
                ddl_Employer.DataValueField = "catid"
                ddl_Employer.DataSource = dt
                ddl_Employer.DataBind()
            End If
        Catch ex As Exception
            If ex.Message.Contains("OutOfMemory") Then
                RadWindowManager1.RadAlert("<ul><li><div style=color:Red;>This is a temporary issue; please try to Re-Login after some time.</div></li></ul>", 300, 150, "Page Loading Error", Nothing)
                Exit Sub
            Else
                Dim Err_Msg As String = ex.Message.ToString.Replace(vbCrLf, "")
                Err_Msg = Err_Msg.Replace(vbCr, "")
                Err_Msg = Err_Msg.Replace(vbLf, "")
                RadWindowManager1.RadAlert("<ul><li><div style=color:Red;>" & Err_Msg & ".</div></li></ul>", 300, 100, "Page Loading Error", Nothing)
                Exit Sub
            End If
        End Try
    End Sub


    Private Sub Employer_Filter(ByVal EmpId As String)
        Try

            Dim qry = "select * from system.AMS_EXIST_INV_REC_LIST"

            If EmpId <> "0" Then
                qry &= " where CATID='" & EmpId & "'"
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
    Private Sub fillblank2()
        Try


            If Page.IsPostBack = False Then
                'qry = "SELECT TRANSID,ADVID, (SELECT STATDESCR FROM AMS.AMS_STATMASTER where statstatus='A' and STATID=PLTFRMID ) as Platform ,  POSTDT,   (select agntname from ams.ams_agents where agntid=POSTBY) as  POSTBY,  (SELECt STATDESCR FROM AMS.AMS_STATMASTER where statstatus='A' and STATID=poststs) as STATUS,  ADVTXT,  KEYEDON,  (select agntname from ams.ams_agents where agntid=KEYEDBY)  as KEYEDBY FROM AMS.ADV_DETAILS where 1=0 "
                'qry = "SELECT TRANSID,ADVID,PLTFRMID,PLATFORM,POSTDT,POSTBY,STATUS, RESCOUNT, SHORLISTED,OLG, KEYEDON, KEYEDBY FROM tt.AMS_ADVPLATFORM where 1=0"
                qry = "SELECT TRANSID,ADVID,PLTFRMID,PLATFORM,POSTDT,POSTBY,STATUS, RESCOUNT, SHORLISTED,OLG, KEYEDON, KEYEDBY FROM tt.AMS_ADVPLATFORM"
                dt = clas.getdata(qry, "QR")
                ' If Not dt Is Nothing Then
                ViewState("ADVPlatform") = dt
                'End If
                Dim _dttemp As DataTable
                Dim dv As DataView
                _dttemp = ViewState("ADVPlatform")

                dv = _dttemp.DefaultView
                dv.RowFilter = "TRANSID<0"
                RadGrid2.DataSource = dv
                RadGrid2.DataBind()

                ' qry = "select CANDID,CANDNAME as Name,CANDPHN as Phone,CANDEML as Email, TO_CHAR(RESMRECDDAT, 'DD-Mon-YYYY')  as Rec_Date,b.advid,(SELECT  STATDESCR FROM AMS.AMS_STATMASTER where STATID=PLTFORMID) as Platform,nvl((select roundname from ams.bdc_rnd_mst where roundid=CANDRND),'') as CRound, TO_CHAR(KEYEDON, 'DD-Mon-YYYY')  KEYEDON, (select agntname from ams.ams_agents where agntid=KEYEDBY)  as KEYEDBY  FROM AMS.BDC_LEAD_MASTER b where 1=0"

                qry = "select CANDID,CANDNAME as Name,CANDPHN as Phone,CANDEML as Email, TO_CHAR(RESMRECDDAT, 'DD-Mon-YYYY')  as Rec_Date,b.advid,(SELECT  STATDESCR FROM AMS.AMS_STATMASTER where STATID=PLTFORMID) as Platform,nvl((select roundname from ams.bdc_rnd_mst where roundid=CANDRND),'') as CRound,nvl((SELECT  STATDESCR FROM AMS.AMS_STATMASTER WHERE STATID=CANDRESULT),'') as Result,CANDRESULT, TO_CHAR(KEYEDON, 'DD-Mon-YYYY')  KEYEDON, (select agntname from ams.ams_agents where agntid=KEYEDBY)  as KEYEDBY,AGRNO  FROM AMS.BDC_LEAD_MASTER b"
                dt = clas.getdata(qry, "QR")


                ViewState("ADVCandidate") = dt

                _dttemp = ViewState("ADVCandidate")

                dv = _dttemp.DefaultView
                dv.RowFilter = "candid<0"
                RadGrid3.DataSource = dv
                RadGrid3.DataBind()

                'qry = "select CANDID,CANDNAME as Name,CANDPHN as Phone,CANDEML as Email, RESMRECDDAT as Rec_Date, (select substr(advdesc,1,40) from ams.adv_master a where a.ADVID=b.advid) as Adv_Desc, b.advid,(SELECT  STATDESCR FROM AMS.AMS_STATMASTER where STATID=PLTFORMID) as Platform,CANDRND as Round, KEYEDON,   (select agntname from ams.ams_agents where agntid=KEYEDBY)  as KEYEDBY  FROM AMS.BDC_LEAD_MASTER b where 1=0"
                'dt = clas.getdata(qry, "QR")
                'RadGrid3.DataSource = dt
                'RadGrid3.DataBind()
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

    Private Sub fillblank()
        Try


            If Page.IsPostBack = False Then
                Session("advid") = ""
                Session("vacid") = ""

                'qry = "SELECT TRANSID,ADVID, (SELECT STATDESCR FROM AMS.AMS_STATMASTER where statstatus='A' and STATID=PLTFRMID ) as Platform ,  POSTDT,   (select agntname from ams.ams_agents where agntid=POSTBY) as  POSTBY,  (SELECt STATDESCR FROM AMS.AMS_STATMASTER where statstatus='A' and STATID=poststs) as STATUS,  ADVTXT,  KEYEDON,  (select agntname from ams.ams_agents where agntid=KEYEDBY)  as KEYEDBY FROM AMS.ADV_DETAILS where 1=0 "
                'qry = "SELECT TRANSID,ADVID,PLTFRMID,PLATFORM,POSTDT,POSTBY,STATUS, RESCOUNT, SHORLISTED,OLG, KEYEDON, KEYEDBY FROM tt.AMS_ADVPLATFORM where 1=0"
                qry = "SELECT TRANSID,ADVID,PLTFRMID,PLATFORM,POSTDT,POSTBY,STATUS, RESCOUNT, SHORLISTED,OLG, KEYEDON, KEYEDBY FROM tt.AMS_ADVPLATFORM where 1=0"
                dt = clas.getdata(qry, "QR")
                RadGrid2.DataSource = dt
                RadGrid2.DataBind()

                ' qry = "select CANDID,CANDNAME as Name,CANDPHN as Phone,CANDEML as Email, TO_CHAR(RESMRECDDAT, 'DD-Mon-YYYY')  as Rec_Date,b.advid,(SELECT  STATDESCR FROM AMS.AMS_STATMASTER where STATID=PLTFORMID) as Platform,nvl((select roundname from ams.bdc_rnd_mst where roundid=CANDRND),'') as CRound, TO_CHAR(KEYEDON, 'DD-Mon-YYYY')  KEYEDON, (select agntname from ams.ams_agents where agntid=KEYEDBY)  as KEYEDBY  FROM AMS.BDC_LEAD_MASTER b where 1=0"

                qry = "select CANDID,CANDNAME as Name,CANDPHN as Phone,CANDEML as Email, TO_CHAR(RESMRECDDAT, 'DD-Mon-YYYY')  as Rec_Date,b.advid,(SELECT  STATDESCR FROM AMS.AMS_STATMASTER where STATID=PLTFORMID) as Platform,nvl((select roundname from ams.bdc_rnd_mst where roundid=CANDRND),'') as CRound,nvl((SELECT  STATDESCR FROM AMS.AMS_STATMASTER WHERE STATID=CANDRESULT),'') as Result,CANDRESULT, TO_CHAR(KEYEDON, 'DD-Mon-YYYY')  KEYEDON, (select agntname from ams.ams_agents where agntid=KEYEDBY)  as KEYEDBY,AGRNO  FROM AMS.BDC_LEAD_MASTER b where 1=0"
                dt = clas.getdata(qry, "QR")
                RadGrid3.DataSource = dt
                RadGrid3.DataBind()

                'qry = "select CANDID,CANDNAME as Name,CANDPHN as Phone,CANDEML as Email, RESMRECDDAT as Rec_Date, (select substr(advdesc,1,40) from ams.adv_master a where a.ADVID=b.advid) as Adv_Desc, b.advid,(SELECT  STATDESCR FROM AMS.AMS_STATMASTER where STATID=PLTFORMID) as Platform,CANDRND as Round, KEYEDON,   (select agntname from ams.ams_agents where agntid=KEYEDBY)  as KEYEDBY  FROM AMS.BDC_LEAD_MASTER b where 1=0"
                'dt = clas.getdata(qry, "QR")
                'RadGrid3.DataSource = dt
                'RadGrid3.DataBind(
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
            'Dim qry = "SELECT ADVID, TO_CHAR(ADVDATE, 'DD-Mon-YYYY') as AdvDate ,  ADVDESC as Description,  TO_CHAR(ADVAPPDDT, 'DD-MON-YYYY') as ApproveDt,  (select agntname from ams.ams_agents where agntid=ADVAPPBY) as  ApprovedBy,  ADVPRVID,  ADVEXPENSE as Expense,   KEYEDON,  (select agntname from ams.ams_agents where agntid=KEYEDBY) as KEYEDBY   FROM AMS.ADV_MASTER"
            qry = "SELECT ADVID,vac_id,Vacancy,  ADVDATE,  DESCRIPTION,  APPROVEDT,  APPROVEDBY,  Province,  EXPENSE,  RESCOUNT,  SHORLISTED,  OLG,  KEYEDON,  KEYEDBY FROM TT.AMS_ADVERTISEMENT order by  to_date(keyedon,'dd-Mon-yyyy') desc"
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

    Private Sub getmastres2()
        Try

            qry = "SELECT TRANSID,ADVID,PLTFRMID,PLATFORM,POSTDT,POSTBY,STATUS, RESCOUNT, SHORLISTED,OLG, KEYEDON, KEYEDBY FROM TT.AMS_ADVPLATFORM where advid='" & Session("advid") & "'"

            dt = clas.getdata(qry, "QR")

            ' dt = ViewState("ADVPlatform")

            If dt.Rows.Count > 0 Then

                RadGrid2.DataSource = dt
                RadGrid2.DataBind()
            Else
                'Dim drnewrow As DataRow = dt.NewRow
                'dt.Rows.Add(drnewrow)
                RadGrid2.DataSource = dt
                RadGrid2.DataBind()
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

    Private Sub getmastres3()
        Try
            qry = "select CANDID,CANDNAME as Name,CANDPHN as Phone,CANDEML as Email, TO_CHAR(RESMRECDDAT, 'DD-Mon-YYYY')  as Rec_Date,b.advid,(SELECT  STATDESCR FROM AMS.AMS_STATMASTER where STATID=PLTFORMID) as Platform,nvl((select roundname from ams.bdc_rnd_mst where roundid=CANDRND),'') as CRound,nvl((SELECT  STATDESCR FROM AMS.AMS_STATMASTER WHERE STATID=CANDRESULT),'') as Result,CANDRESULT, TO_CHAR(KEYEDON, 'DD-Mon-YYYY')  KEYEDON, (select agntname from ams.ams_agents where agntid=KEYEDBY)  as KEYEDBY, AGRNO  FROM AMS.BDC_LEAD_MASTER b where advid='" & Session("advid") & "'"
            dt = clas.getdata(qry, "QR")
            'dt = ViewState("ADVCandidate")
            If dt.Rows.Count > 0 Then
                RadGrid3.DataSource = dt
                RadGrid3.DataBind()
            Else
                RadGrid3.DataSource = dt
                RadGrid3.DataBind()
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

    Protected Sub RadGrid2_PreRender(sender As Object, e As System.EventArgs) Handles RadGrid2.PreRender
        If Page.IsPostBack = False Then
            Dim column As GridColumn = TryCast(RadGrid2.MasterTableView.GetColumnSafe("rpst"), GridColumn)
            column.OrderIndex = RadGrid2.MasterTableView.RenderColumns.Length - 1
            RadGrid2.MasterTableView.Rebind()
        End If
    End Sub

    Protected Sub RadGrid2_SortCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridSortCommandEventArgs) Handles RadGrid2.SortCommand
        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
        getmastres2()
    End Sub

    Protected Sub RadGrid2_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid2.ItemDataBound
        RadGrid2.MasterTableView.GetColumn("advid").Visible = False
        RadGrid2.MasterTableView.GetColumn("TRANSID").Visible = False
        RadGrid2.MasterTableView.GetColumn("PLTFRMID").Visible = False
        RadGrid2.MasterTableView.GetColumn("POSTDT").HeaderText = "PUBLISH DATE"
        RadGrid2.MasterTableView.GetColumn("POSTBY").HeaderText = "PUBLISH BY"
        RadGrid2.MasterTableView.GetColumn("RESCOUNT").HeaderText = "CV RECEVIED"
        RadGrid2.MasterTableView.GetColumn("OLG").HeaderText = "OL GIVEN"

        If (TypeOf e.Item Is GridDataItem) Then
            Dim item As GridDataItem = e.Item
            Dim btnedit As ImageButton = DirectCast(item.FindControl("btnedit"), ImageButton)
            Dim btnCand As ImageButton = DirectCast(item.FindControl("btnCand2"), ImageButton)

            btnedit.ImageUrl = "Images/add_rec.png"
            btnedit.ToolTip = "Edit Record"
            btnedit.CommandName = "editform"
            btnedit.CommandArgument = item.GetDataKeyValue("TRANSID").ToString()

            btnCand.ImageUrl = "Images/add_candates.png"
            btnCand.ToolTip = "Add Candidate"
            btnCand.CommandName = "CandDetail"
            btnCand.CommandArgument = item.GetDataKeyValue("ADVID").ToString()

        End If
    End Sub
    Protected Sub RadGrid2_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid2.ItemCommand
        If e.CommandName = "editform" Then
            Label1.Text = "Edit Publishing Details"
            Ifrmfollowup.Attributes.Add("src", "bdc_inv_Postpop.aspx?transid=" & e.CommandArgument.ToString() & "&mode=edit")
            '  ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)
            'ClientScript.RegisterClientScriptBlock([GetType](), "none", "openModal();", False)
            'ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", True)
        ElseIf e.CommandName = "viewpost" Then
            If TypeOf e.Item Is GridDataItem Then
                Dim item As GridDataItem = DirectCast(e.Item, GridDataItem)
                Dim id As String = item.GetDataKeyValue("TRANSID").ToString()
                'Dim authdat As String = item("AUTHDAT").Text              
                '    Dim resauthstat As String = item("RESAUTHSTAT").Text

                Dim fname As String = clas.ExecuteScalar("select ADVIMG from  AMS.ADV_DETAILS  where TRANSID = '" & id & "'")
                'Response.Redirect("~/Resume/" + fname)

                If fname.Trim().Length > 0 Then

                    If File.Exists(Server.MapPath("~/Advertisement/" & fname)) Then
                        Response.ContentType = "application/octet-stream"
                        Response.AppendHeader("Content-Disposition", "attachment;filename=" & fname)
                        Response.TransmitFile(Server.MapPath("~/Advertisement/" & fname))
                        Response.End()
                    Else
                        RadWindowManager1.RadAlert("<ul><li><div style=color:red;>System is unable to found any document for download...</div></li></ul>", 300, 100, "Validation Exception Failure", Nothing)
                        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
                        Exit Sub
                    End If

                Else
                    RadWindowManager1.RadAlert("<ul><li><div style=color:red;>System is unable to found any document for download.</div></li></ul>", 300, 100, "Validation Exception Failure", Nothing)
                    Exit Sub
                End If
            End If

        ElseIf e.CommandName = "CandDetail" Then
            Label1.Text = "Add Candidate"
            Dim item As GridDataItem = DirectCast(e.Item, GridDataItem)

            Dim pid As String = item.GetDataKeyValue("PLTFRMID").ToString()
            Ifrmfollowup.Attributes.Add("src", "bdc_inv_Candpop.aspx?aid=" & e.CommandArgument.ToString() & "&pid=" + pid)
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)
            'ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", True)
        ElseIf e.CommandName = "refresh" Then
            For Each column As GridColumn In RadGrid2.MasterTableView.RenderColumns
                column.CurrentFilterFunction = GridKnownFunction.NoFilter
                column.CurrentFilterValue = String.Empty
            Next
            RadGrid2.MasterTableView.FilterExpression = String.Empty
            RadGrid2.Rebind()
            Label1.Text = ""
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            getmastres2()
        ElseIf e.CommandName = "EmployerFilter" Then
            If ddl_Employer IsNot Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
                Employer_Filter(ddl_Employer.SelectedValue)
            End If

        ElseIf e.CommandName = "Filter" Then
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            getmastres2()
        ElseIf e.CommandName = "ChangePageSize" Then
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            getmastres2()
        End If
    End Sub


    Protected Sub RadGrid1_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid1.ItemDataBound
        'RadGrid1.MasterTableView.GetColumn("advimage").Visible = False
        RadGrid1.MasterTableView.GetColumn("advid").Visible = False
        RadGrid1.MasterTableView.GetColumn("vac_id").Visible = False

        RadGrid1.MasterTableView.GetColumn("ADVDATE").HeaderText = "AD. DATE"
        RadGrid1.MasterTableView.GetColumn("APPROVEDT").HeaderText = "APPROVAL ON"
        RadGrid1.MasterTableView.GetColumn("APPROVEDBY").HeaderText = "APPROVAL BY"
        RadGrid1.MasterTableView.GetColumn("RESCOUNT").HeaderText = "CV RECEIVED"
        RadGrid1.MasterTableView.GetColumn("OLG").HeaderText = "OL GIVEN"
        RadGrid1.MasterTableView.GetColumn("KEYEDBY").HeaderText = "RECEIVED BY"

        If (TypeOf e.Item Is GridDataItem) Then
            Dim item As GridDataItem = e.Item
            Dim btnedit As ImageButton = DirectCast(item.FindControl("btnedit"), ImageButton)
            Dim btnget As ImageButton = DirectCast(item.FindControl("btnget"), ImageButton)
            Dim btnCand As ImageButton = DirectCast(item.FindControl("btnCand"), ImageButton)
            Dim lnkBtn As LinkButton = TryCast(item.FindControl("lnkshowpost"), LinkButton)

            btnedit.ImageUrl = "Images/add_rec.png"
            btnedit.ToolTip = "Edit Record"
            btnedit.CommandName = "editform"
            btnedit.CommandArgument = item.GetDataKeyValue("advid").ToString()

            btnget.ImageUrl = "Images/publishing_ico.png"
            btnget.ToolTip = "Add Publish Details"
            btnget.CommandName = "PostDetail"
            btnget.CommandArgument = item.GetDataKeyValue("advid").ToString()

            btnCand.ImageUrl = "Images/add_candates.png"
            btnCand.ToolTip = "Add Candidate"
            btnCand.CommandName = "CandDetail"
            btnCand.CommandArgument = item.GetDataKeyValue("advid").ToString()
        End If
    End Sub


    Protected Sub RadGrid1_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid1.ItemCommand
        If e.CommandName = "securedjob" Then
            Label1.Text = "My Job Orders"
            Ifrmfollowup.Attributes.Add("src", "jobsecure_list.aspx?empid=" & e.CommandArgument.ToString())
            '  ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)
            'ClientScript.RegisterClientScriptBlock([GetType](), "none", "openModal();", False)
            'ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", True)
        ElseIf e.CommandName = "viewadv" Then
            If TypeOf e.Item Is GridDataItem Then
                Dim item As GridDataItem = DirectCast(e.Item, GridDataItem)
                Dim id As String = item.GetDataKeyValue("advid").ToString()
                'Dim authdat As String = item("AUTHDAT").Text              
                '    Dim resauthstat As String = item("RESAUTHSTAT").Text

                Dim fname As String = clas.ExecuteScalar("select ADVIMAGE from  AMS.ADV_Master  where ADVID = '" & id & "'")
                'Response.Redirect("~/Resume/" + fname)

                If fname.Trim().Length > 0 Then

                    If File.Exists(Server.MapPath("~/Advertisement/" & fname)) Then
                        Response.ContentType = "application/octet-stream"
                        Response.AppendHeader("Content-Disposition", "attachment;filename=" & fname)
                        Response.TransmitFile(Server.MapPath("~/Advertisement/" & fname))
                        Response.End()
                    Else
                        RadWindowManager1.RadAlert("<ul><li><div style=color:red;>System is unable to found any document for download...</div></li></ul>", 300, 100, "Validation Exception Failure", Nothing)
                        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
                        Exit Sub
                    End If

                Else
                    RadWindowManager1.RadAlert("<ul><li><div style=color:red;>System is unable to found any document for download.</div></li></ul>", 300, 100, "Validation Exception Failure", Nothing)
                    Exit Sub
                End If
            End If
        ElseIf e.CommandName = "PostDetail" Then
            Label1.Text = "Add Publishing "
            Ifrmfollowup.Attributes.Add("src", "bdc_inv_Postpop.aspx?aid=" & e.CommandArgument.ToString())
            'ClientScript.RegisterStartupScript(Me.GetType(), "none", "<script>$('#addnewfollowup').modal('show');</script>", True)
            '  ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)
            'ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", True)
        ElseIf e.CommandName = "CandDetail" Then
            Label1.Text = "Add Candidate"
            Ifrmfollowup.Attributes.Add("src", "bdc_inv_Candpop.aspx?aid=" & e.CommandArgument.ToString())
            'ClientScript.RegisterStartupScript(Me.GetType(), "none", "<script>$('#addnewfollowup').modal('show');</script>", True)
            '  ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)
            'ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", True)
        ElseIf e.CommandName = "editform" Then
            Label1.Text = "Edit Advertisement"
            Ifrmfollowup.Attributes.Add("src", "bdc_inv_Advpop.aspx?aid=" & e.CommandArgument.ToString() & "&mode=edit")
            'ClientScript.RegisterStartupScript(Me.GetType(), "none", "<script>$('#addnewfollowup').modal('show');</script>", True)
            '  ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)
            'ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", True)
        ElseIf e.CommandName = "viewaddresume" Then
            'If TypeOf e.Item Is GridDataItem Then
            '    Dim item As GridDataItem = DirectCast(e.Item, GridDataItem)
            '    Label1.Text = "View Resume"
            '    Dim fno As String = item.GetDataKeyValue("fileno").ToString()
            '    Dim fsn As String = item.GetDataKeyValue("fsn").ToString()
            '    Dim id As String = item.GetDataKeyValue("ID").ToString()
            '    'Dim authdat As String = item("AUTHDAT").Text              
            '    Dim resauthstat As String = item("RESAUTHSTAT").Text
            '    Ifrmfollowup.Attributes.Add("src", "exis_invc_upload_resume.aspx?fileno=" & fno & "&fsn=" + fsn + "&resauthstat=" + resauthstat + "&id=" + id + "")
            '    ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)
            'End If
        ElseIf e.CommandName = "RowClick" Then
            If TypeOf e.Item Is GridDataItem Then
                Dim item As GridDataItem = DirectCast(e.Item, GridDataItem)
                Session("advid") = item.GetDataKeyValue("advid")
                Session("vacid") = item.GetDataKeyValue("vac_id")
                getmastres2()
                getmastres3()
                'ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            End If
        ElseIf e.CommandName = "dwnldresume" Then
            If TypeOf e.Item Is GridDataItem Then
                Dim item As GridDataItem = DirectCast(e.Item, GridDataItem)

                Dim fno As String = item.GetDataKeyValue("fileno").ToString()
                Dim fsn As String = item.GetDataKeyValue("fsn").ToString()
                Dim id As String = item.GetDataKeyValue("ID").ToString()
                'Dim authdat As String = item("AUTHDAT").Text              
                Dim resauthstat As String = item("RESAUTHSTAT").Text

                Dim fname As String = clas.ExecuteScalar("select RSUME from AMS.EXIST_INV_REC where id = '" & id & "'")
                'Response.Redirect("~/Resume/" + fname)

                If fname.Trim().Length > 0 Then

                    If File.Exists(Server.MapPath("~/Resume/" & fname)) Then
                        Response.ContentType = "application/octet-stream"
                        Response.AppendHeader("Content-Disposition", "attachment;filename=" & fname)
                        Response.TransmitFile(Server.MapPath("~/Resume/" & fname))
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
            If ddl_Employer IsNot Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
                Employer_Filter(ddl_Employer.SelectedValue)
            End If

        ElseIf e.CommandName = "Filter" Then
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            getmastres()
        ElseIf e.CommandName = "ChangePageSize" Then
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            getmastres()
        End If
    End Sub

    Protected Sub RadGrid1_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadGrid1.PreRender
        If Page.IsPostBack = False Then
            Dim column As GridColumn = TryCast(RadGrid1.MasterTableView.GetColumnSafe("rpst1"), GridColumn)
            'Dim column As GridColumn = TryCast(RadGrid1.MasterTableView.GetColumnSafe("rpst1"), GridColumn)
            column.OrderIndex = RadGrid1.MasterTableView.RenderColumns.Length - 1
            RadGrid1.MasterTableView.Rebind()
        End If

    End Sub
    Protected Sub RadGrid1_SortCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridSortCommandEventArgs) Handles RadGrid1.SortCommand
        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
        getmastres()
    End Sub

    Protected Sub RadGrid3_PreRender(sender As Object, e As System.EventArgs) Handles RadGrid3.PreRender
        If Page.IsPostBack = False Then
            Dim column As GridColumn = TryCast(RadGrid3.MasterTableView.GetColumnSafe("res"), GridColumn)
            Dim column2 As GridColumn = TryCast(RadGrid3.MasterTableView.GetColumnSafe("eid"), GridColumn)

            column.OrderIndex = RadGrid3.MasterTableView.RenderColumns.Length - 1
            column2.OrderIndex = RadGrid3.MasterTableView.RenderColumns.Length - 8
            RadGrid3.MasterTableView.Rebind()
        End If

    End Sub

    Protected Sub RadGrid3_SortCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridSortCommandEventArgs) Handles RadGrid3.SortCommand
        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
        getmastres3()
    End Sub
    Protected Sub RadGrid3_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid3.ItemDataBound
        RadGrid3.MasterTableView.GetColumn("advid").Visible = False
        RadGrid3.MasterTableView.GetColumn("CANDID").Visible = False
        RadGrid3.MasterTableView.GetColumn("CANDRESULT").Visible = False

        RadGrid3.MasterTableView.GetColumn("Email").Visible = False
        RadGrid3.MasterTableView.GetColumn("REC_DATE").HeaderText = "RECEIVED"
        RadGrid3.MasterTableView.GetColumn("AGRNO").Visible = False
        RadGrid3.MasterTableView.GetColumn("CRound").HeaderText = "ROUND"




        If (TypeOf e.Item Is GridDataItem) Then


            Dim item As GridDataItem = e.Item
            Dim btnedit As ImageButton = DirectCast(item.FindControl("btnedit"), ImageButton)
            Dim btnint As ImageButton = DirectCast(item.FindControl("btnint"), ImageButton)
            Dim btnget As LinkButton = DirectCast(item.FindControl("lnkshowResume"), LinkButton)
            Dim btnPrint As ImageButton = DirectCast(item.FindControl("btnjob"), ImageButton)

            btnedit.ImageUrl = "Images/add_rec.png"
            btnedit.ToolTip = "Edit Record"
            btnedit.CommandName = "editform"
            btnedit.CommandArgument = item.GetDataKeyValue("CANDID").ToString()

            btnget.Text = "Download Resume"
            btnget.Enabled = True
            btnget.CommandName = "dwnldresume"

            btnint.ImageUrl = "Images/add_round.png"
            btnint.ToolTip = "Add Round Details"
            btnint.CommandName = "Round"
            btnint.CommandArgument() = item.GetDataKeyValue("CANDID").ToString()

            btnPrint.ImageUrl = "Images/gen_ico.png"
            btnPrint.ToolTip = "Generate Agreement"
            btnPrint.CommandName = "generate"

            btnPrint.CommandArgument = item.GetDataKeyValue("CANDID").ToString()

            If item.Cells(12).Text = "Shortlist" Then
                btnPrint.Visible = True
                'btnPrint.Enabled = False
            Else
                btnPrint.Visible = False
            End If
            '  btnjob.Visible = "False"
        End If
    End Sub
    Protected Sub RadGrid3_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid3.ItemCommand
        If e.CommandName = "Round" Then
            Dim item As GridDataItem = DirectCast(e.Item, GridDataItem)
            Dim res = item.GetDataKeyValue("CANDRESULT").ToString()
            Label1.Text = "Interview Rounds"
            Ifrmfollowup.Attributes.Add("src", "bdc_inv_Resultpop.aspx?candid=" & e.CommandArgument().ToString() & "&aid=" & Session("advid") & "&res=" & res & "&vacid=" & Session("vacid"))
            '  ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)
            'ClientScript.RegisterClientScriptBlock([GetType](), "none", "openModal();", False)
            'ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", True)

        ElseIf e.CommandName = "editform" Then
            Label1.Text = "Edit Candidate"
            Ifrmfollowup.Attributes.Add("src", "bdc_inv_CandPop.aspx?CANDID=" & e.CommandArgument.ToString() & "&mode=edit")
            'ClientScript.RegisterStartupScript(Me.GetType(), "none", "<script>$('#addnewfollowup').modal('show');</script>", True)
            '  ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)
            'ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", True)
        ElseIf e.CommandName = "generate" Then
            Dim filname As String
            If TypeOf e.Item Is GridDataItem Then
                Dim item As GridDataItem = DirectCast(e.Item, GridDataItem)
                '    Dim item As GridDataItem = DirectCast(e.Item, GridDataItem)
                '    Label1.Text = "View Resume"
                filname = item.GetDataKeyValue("AGRNO").ToString()
                ' Print(item.GetDataKeyValue("CANDID").ToString(), filname)
                ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)
            End If

            Label1.Text = "Generate Agreement"
            Ifrmfollowup.Attributes.Add("src", "bdc_inv_AgrPop.aspx?CANDID=" & e.CommandArgument.ToString() & "&AGRNO=" & filname & "&mode=edit")
            'ClientScript.RegisterStartupScript(Me.GetType(), "none", "<script>$('#addnewfollowup').modal('show');</script>", True)
            '  ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)


        ElseIf e.CommandName = "sndeml" Then
            Label1.Text = "Compose Email"
            Ifrmfollowup.Attributes.Add("src", "http://115.249.129.5:5013/commail/sendmail?empno=" & Session("lgnagntid") & "&F_emp=" & Convert.ToString(Session("lgneml")) & "&T_emp=" & Convert.ToString(e.CommandArgument) & "&camsuid=1")
            ' Ifrmfollowup.Attributes.Add("src", "http://115.249.129.5:5013/commail/sendmail?empno=159&F_emp=" & Convert.ToString(Session("lgneml")) & "&T_emp=rahul.gupta273@gmail.com&camsuid=0")
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)
            'ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", True)

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
        ElseIf e.CommandName = "refresh" Then
            For Each column As GridColumn In RadGrid1.MasterTableView.RenderColumns
                column.CurrentFilterFunction = GridKnownFunction.NoFilter
                column.CurrentFilterValue = String.Empty
            Next


            RadGrid3.MasterTableView.FilterExpression = String.Empty
            RadGrid3.Rebind()
            Label1.Text = ""
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            getmastres3()
            'Ifrmfollowup.Attributes.Add("src", "followup_list.aspx?empid=" & e.CommandArgument.ToString())

            'ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)
        ElseIf e.CommandName = "EmployerFilter" Then


            If ddl_Employer IsNot Nothing Then
                ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
                Employer_Filter(ddl_Employer.SelectedValue)
            End If

        ElseIf e.CommandName = "Filter" Then
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            getmastres3()
        ElseIf e.CommandName = "ChangePageSize" Then
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            getmastres3()
        End If
    End Sub

    Private Sub RadGrid1_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles RadGrid1.PageIndexChanged
        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
        RadGrid1.CurrentPageIndex = e.NewPageIndex
        getmastres()
    End Sub

    Private Sub RadGrid2_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles RadGrid2.PageIndexChanged
        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
        RadGrid2.CurrentPageIndex = e.NewPageIndex
        getmastres2()

    End Sub
    Private Sub RadGrid3_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles RadGrid3.PageIndexChanged
        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
        RadGrid2.CurrentPageIndex = e.NewPageIndex
        getmastres3()
    End Sub



    Protected Sub Btnref_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Button2.Click
        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
        getmastres()
    End Sub
    Protected Sub Btnref3_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Button3.Click
        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
        getmastres3()
    End Sub

    Protected Sub Btnref2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
        getmastres2()
    End Sub
    Protected Sub Btnref1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton3.Click
        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
        getmastres()
    End Sub
    Protected Sub btn_ref4_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton5.Click
        ' ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "tab2click();", True)
        getmastres4()
        '$(document).on("click", '#<%= RadTabStrip2.ClientID 
        'tabCand.Visible = False
        ' RadTabStrip2.SelectedIndex = 1


    End Sub



    Protected Sub AddAdv_Click(sender As Object, e As System.EventArgs) Handles AddAdv.Click
        Label1.Text = "Add Advertisement"
        Ifrmfollowup.Attributes.Add("src", "bdc_inv_Advpop.aspx")
        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)
    End Sub

    Protected Sub ADDVAC_Click(sender As Object, e As System.EventArgs) Handles ADDVAC.Click
        Label1.Text = "Add Vacancy"
        Ifrmfollowup.Attributes.Add("src", "bdc_inv_Vacpop.aspx")
        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)
    End Sub

    Protected Sub AddSch_Click(sender As Object, e As System.EventArgs) Handles AddSch.Click
        Label1.Text = "Interview Schedule"
        Ifrmfollowup.Attributes.Add("src", "bdc_inv_Schedpop.aspx")
        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)
    End Sub


    Private Sub getmastres4()
        Try
            '        qry = "select advid,LOGID,candid ,(select roundname from ams.bdc_rnd_mst where roundid=prvround) as PreviousRound,(select roundname from ams.bdc_rnd_mst where roundid=CURROUND) as CurrentRound,(select statdescr from ams.ams_statmaster where statid =STATUS) as Status,CANDFDBCK as Feedback,(select name from tt.employees where empno=conductedby) as RoundBy,to_char(ROUNDDAT,'dd-Mon-yyyy') as ScheduleOn FROM AMS.BDC_CAND_RND_LOG a order by ROUNDDAT"

            qry = "select logid,advid,vac_id,candid,to_char(nxtrounddat,'dd-Mon-yyyy') ScheduleOn,(select name from tt.employees where empno=CONDUCTEDBY) ScheduleBy,(select candname from ams.bdc_lead_master a  where a.candid=t1.candid) Candidate,(select vacname from ams.adv_vacancy where vacid=vac_id) Vacancy from ams.bdc_cand_rnd_log t1 where nxtroundby= (select emp_no from ams.ams_usermaster where lgnagntid='" & Session("lgnagntid") & "') and reason is null and status=105"
            dt = clas.getdata(qry, "QR")

            If dt.Rows.Count > 0 Then
                RadGrid4.DataSource = dt
                RadGrid4.DataBind()
            Else
                'Dim drnewrow As DataRow = dt.NewRow
                'dt.Rows.Add(drnewrow)
                RadGrid4.DataSource = dt
                RadGrid4.DataBind()
                '  RadWindowManager1.RadAlert("No Interview Schedule", 300, 200, "AMS", "")
            End If


        Catch ex As Exception
            Session("error") = ex.Message.ToString
            RadWindowManager1.RadAlert("No Interview Schedule", 300, 200, "AMS", "")
            'Dim AlertMessage As String = "alert('" & ex.Message.ToString & "')"
            'ScriptManager.RegisterStartupScript(Page, Page.GetType, "AlertMessage", AlertMessage, True)
        End Try
    End Sub



    Protected Sub RadGrid4_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid4.ItemCommand
        If e.CommandName = "pround" Then
            Dim item As GridDataItem = DirectCast(e.Item, GridDataItem)
            Label1.Text = "Previous Round"

            Ifrmfollowup.Attributes.Add("src", "bdc_inv_logpop.aspx?aid=" & e.CommandArgument.ToString() & "&candid=" & item.GetDataKeyValue("CANDID").ToString())
            '  ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)
            'ClientScript.RegisterClientScriptBlock([GetType](), "none", "openModal();", False)
            'ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", True)

        ElseIf e.CommandName = "nround" Then
            Dim item As GridDataItem = DirectCast(e.Item, GridDataItem)
            Label1.Text = "Interview Feedback"
            Ifrmfollowup.Attributes.Add("src", "bdc_inv_Resultpop.aspx?aid=" & e.CommandArgument.ToString() & "&candid=" & item.GetDataKeyValue("CANDID").ToString() & "&logid=" & item.GetDataKeyValue("logid").ToString())
            'ClientScript.RegisterStartupScript(Me.GetType(), "none", "<script>$('#addnewfollowup').modal('show');</script>", True)
            '  ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)
            'ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", True)

            'ElseIf e.CommandName = "editform" Then
            '    Label1.Text = "Edit Advertisement"
            '    Ifrmfollowup.Attributes.Add("src", "bdc_inv_Advpop.aspx?aid=" & e.CommandArgument.ToString() & "&mode=edit")
            '    'ClientScript.RegisterStartupScript(Me.GetType(), "none", "<script>$('#addnewfollowup').modal('show');</script>", True)
            '    '  ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            '    ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)
            '    'ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", True)


        ElseIf e.CommandName = "refresh" Then
            For Each column As GridColumn In RadGrid4.MasterTableView.RenderColumns
                column.CurrentFilterFunction = GridKnownFunction.NoFilter
                column.CurrentFilterValue = String.Empty
            Next

            RadGrid4.MasterTableView.FilterExpression = String.Empty
            RadGrid4.Rebind()
            Label1.Text = ""
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            getmastres4()
            'Ifrmfollowup.Attributes.Add("src", "followup_list.aspx?empid=" & e.CommandArgument.ToString())
            'ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)


        ElseIf e.CommandName = "Filter" Then
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            getmastres4()
        ElseIf e.CommandName = "ChangePageSize" Then
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            getmastres4()
        End If
    End Sub


    Protected Sub RadGrid4_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid4.ItemDataBound
        'RadGrid1.MasterTableView.GetColumn("advimage").Visible = False
        RadGrid4.MasterTableView.GetColumn("ADVID").Visible = False
        RadGrid4.MasterTableView.GetColumn("LOGID").Visible = False
        RadGrid4.MasterTableView.GetColumn("CANDID").Visible = False
        RadGrid4.MasterTableView.GetColumn("vac_id").Visible = False
        If (TypeOf e.Item Is GridDataItem) Then
            Dim item As GridDataItem = e.Item
            Dim btnPrvRnd As ImageButton = DirectCast(item.FindControl("btnPrvRnd"), ImageButton)
            Dim btnNxtRnd As ImageButton = DirectCast(item.FindControl("btnNxtRnd"), ImageButton)
            'Dim btnCand As ImageButton = DirectCast(item.FindControl("btnCand"), ImageButton)
            'Dim lnkBtn As LinkButton = TryCast(item.FindControl("lnkshowpost"), LinkButton)

            btnPrvRnd.ImageUrl = "Images/publishing_ico.png"
            btnPrvRnd.ToolTip = "Previous Round"
            btnPrvRnd.CommandName = "pround"
            btnPrvRnd.CommandArgument = item.GetDataKeyValue("advid").ToString()

            btnNxtRnd.ImageUrl = "Images/add_round.png"
            btnNxtRnd.ToolTip = "Add Feedback"
            btnNxtRnd.CommandName = "nround"
            btnNxtRnd.CommandArgument = item.GetDataKeyValue("advid").ToString()

            'btnCand.ImageUrl = "Images/add_candates.png"
            'btnCand.ToolTip = "Add Candidate Details"
            'btnCand.CommandName = "CandDetail"
            'btnCa
        End If
    End Sub


    Protected Sub RadGrid4_PageIndexChanged(sender As Object, e As Telerik.Web.UI.GridPageChangedEventArgs) Handles RadGrid4.PageIndexChanged
        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
        getmastres4()
    End Sub



    Protected Sub RadGrid4_SortCommand(sender As Object, e As Telerik.Web.UI.GridSortCommandEventArgs) Handles RadGrid4.SortCommand
        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
        getmastres4()

    End Sub


    'Protected Sub RadTabStrip2_TabClick(sender As Object, e As Telerik.Web.UI.RadTabStripEventArgs) Handles RadTabStrip2.TabClick

    '    Dim TabClicked As Telerik.Web.UI.RadTab = e.Tab
    '    If TabClicked.Value = "2" Then
    '        RadTabStrip1.Visible = False
    '        RadMultiPage1.Visible = False
    '        getmastres4()
    '    Else
    '        RadTabStrip1.Visible = True
    '        RadMultiPage1.Visible = True
    '    End If

    'End Sub

End Class
