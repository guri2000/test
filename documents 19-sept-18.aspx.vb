Imports System.Data
Imports System.Data.OracleClient
Imports System.IO
Imports Telerik.Web.UI
Imports System.Text
Imports System.Net.Mail

Partial Class Default2
    Inherits System.Web.UI.Page
    Dim clas As rahulcpm.commonclass = New rahulcpm.commonclass()
    Dim qry As String
    Dim dt As DataTable
    Dim agentid As Int32 = 0
    Dim locid As Int32 = 0
    Dim msg As String
    Dim SelectedDocId_Value As String
    Dim agid As String = ""
    Dim leadid As String = ""
    Dim clntid As String = ""
    Dim prtnrid As String = ""
    Dim grpid As String = ""
    Dim docis As String = ""
    Dim srcby As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("lgnagntid") = "0" Or Session("lgnagntid") = "" Then
            Response.Redirect("logout.aspx")
        Else
            agentid = Session("lgnagntid")
        End If
        If Page.IsPostBack = False Then
            'ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            ' getmastres()
            'getBlank()
            'Bind_Employer()
            getmaster1()
            If Not Request.QueryString("fileno") Is Nothing Then
                getmastresofemp(Request.QueryString("fileno").ToString)
            End If

            ' Bind_ViewGrid(agid, leadid, prtnrid, clntid)
        Else

        End If

        ViewState("dec") = 0
    End Sub

    Private Sub getmaster1()
        Try

            qry = "select docid, docname as docname from ams.DOCCHKLISTMASTER where DOCDESCR='A' "


            'If docid <> "" Then
            '    qry &= "and docid='" & docid & "'"

            'End If
            dt = clas.getdata(qry, "QR")
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


    Protected Sub Bind_ViewGrid(Optional ByVal docid As String = "")
        'If Request.QueryString("srcby") = "A" Then
        '    qry = "select  * from cbisi_crm.doclist_base where agid=" & agid
        'ElseIf Request.QueryString("srcby") = "L" Then
        '    qry = "select  * from cbisi_crm.doclist_base where leadid=" & leadid
        'ElseIf Request.QueryString("srcby") = "P" Then
        '    qry = "select  * from cbisi_crm.doclist_base where prtid=" & prtid
        'ElseIf Request.QueryString("srcby") = "C" Then
        '    qry = "select  * from cbisi_crm.doclist_base where leadid=" & clntid
        'End If
        qry = "select  * from ams.doclist_base where docid=" & docid & " and fileno='" & Request.QueryString("fileno") & "'"
        'If docid <> "" Then
        '    qry &= " and docid='" & docid & "'"
        'End If

        Dim dt = clas.getdata(qry, "TX")

        RadGrid2.DataSource = dt
        RadGrid2.DataBind()

        '  End If
    End Sub
    Protected Sub Bind_ViewGrid_all(Optional ByVal type As String = "")
        'If Request.QueryString("srcby") = "A" Then
        '    qry = "select  * from cbisi_crm.doclist_base where agid=" & agid
        'ElseIf Request.QueryString("srcby") = "L" Then
        '    qry = "select  * from cbisi_crm.doclist_base where leadid=" & leadid
        'ElseIf Request.QueryString("srcby") = "P" Then
        '    qry = "select  * from cbisi_crm.doclist_base where prtid=" & prtid
        'ElseIf Request.QueryString("srcby") = "C" Then
        '    qry = "select  * from cbisi_crm.doclist_base where leadid=" & clntid
        'End If
        If type = "A" Then
            qry = "select  * from ams.doclist_base where fileno='" & Request.QueryString("fileno") & "' and status1='A'"
        ElseIf type = "D" Then
            qry = "select  * from ams.doclist_base where fileno='" & Request.QueryString("fileno") & "' and status1='D'"
            ViewState("dec") = 1
        End If

        'If docid <> "" Then
        '    qry &= " and docid='" & docid & "'"
        'End If

        'RadGrid2.Columns(0).Visible = True

        Dim dt = clas.getdata(qry, "TX")

        RadGrid2.DataSource = dt
        RadGrid2.DataBind()

        '  End If
    End Sub

    Protected Sub RadGrid_ViewDocs_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGrid2.ItemCommand
        If e.CommandName = "ViewDoc" Then
            If e.CommandArgument <> "" Then

                Dim row_Index As Integer = Convert.ToInt32(e.CommandArgument)


                If CType(e.Item.FindControl("imgbtn_ViewDoc"), ImageButton).AlternateText <> "" Then
                    Dim filePath As String = CType(e.Item.FindControl("imgbtn_ViewDoc"), ImageButton).AlternateText
                    If filePath = "" Then
                        Dim lbl_FullPath As String = CType(e.Item.FindControl("imgbtn_ViewDoc"), ImageButton).AlternateText
                        filePath = lbl_FullPath
                    End If

                    Dim filename As String = filePath.Split("\").Last



                    Dim words As String()
                    If filePath.Contains("http://121.0.0.79/mis/") Then
                        words = filePath.Split(New Char() {"/"c})
                    ElseIf filePath.Contains("/") Then
                        words = filePath.Split(New Char() {"/"c})
                    Else
                        words = filePath.Split(New Char() {"\"c})
                    End If

                    Dim docname As String = ""
                    Dim strtxt11 As String = ""




                    'strtxt11 = "D:\ZAxis-IIS\misc_app\CBIS\libdocs\" & words(1) & ""
                    strtxt11 = Server.MapPath("~/libdocs/") & words(1) & ""

                    docname = words(1)


                    ' Response.Write(strtxt11 & "----" & filePath & ";")

                    '  Dim FN As String = Path.GetFileName(filePath)

                    ' FN = FN.Replace(" ", "_")
                    'Response.ContentType = ContentType

                    If File.Exists(strtxt11) = True Then
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
                        ElseIf filename.Contains(".html") Then
                            Response.ContentType = "text/html"
                        End If
                        Response.Clear()
                        Dim FN As String = docname
                        Response.AppendHeader("Content-Disposition", ("attachment; filename=" + FN))
                        ''Response.AppendHeader("Content-Disposition", "attachment; filename=" & docname & "")
                        Response.WriteFile(strtxt11)
                        Response.[End]()
                    Else
                        Response.Clear()
                        RadWindowManager1.RadAlert("<ul><li><div style=color:Red>File Not Found</div></li> </ul>", 300, 100, "Download Failure", Nothing)
                    End If

                End If
            End If
        ElseIf e.CommandName = "DelDoc" Then
            If e.CommandArgument <> "" Then

                Dim row_Index As Integer = Convert.ToInt32(e.CommandArgument)
                Dim docid As String = e.CommandArgument
                Delete_Doc(docid)


            End If
        End If
    End Sub

    Protected Sub Delete_Doc(ByVal TransId As String)
        Dim UpdateQuery As StringBuilder = New StringBuilder
        UpdateQuery.Append("update ams.doclib set Status='D'  where id='" & TransId & "'")
        Response.Write(UpdateQuery.ToString())
        clas.ExecuteNonQuery(UpdateQuery.ToString())
        Bind_ViewGrid_all("A")
    End Sub
    Protected Sub RadGrid2_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid2.ItemDataBound
        RadGrid2.MasterTableView.GetColumn("docid").ItemStyle.Width = 0
        RadGrid2.MasterTableView.GetColumn("docid").HeaderStyle.Width = 0
        RadGrid2.MasterTableView.GetColumn("AGID").ItemStyle.Width = 0
        RadGrid2.MasterTableView.GetColumn("AGID").HeaderStyle.Width = 0
        RadGrid2.MasterTableView.GetColumn("ID").ItemStyle.Width = 0
        RadGrid2.MasterTableView.GetColumn("ID").HeaderStyle.Width = 0
        RadGrid2.MasterTableView.GetColumn("CLNTID").ItemStyle.Width = 0
        RadGrid2.MasterTableView.GetColumn("CLNTID").HeaderStyle.Width = 0
        RadGrid2.MasterTableView.GetColumn("PRTID").ItemStyle.Width = 0
        RadGrid2.MasterTableView.GetColumn("PRTID").HeaderStyle.Width = 0
        RadGrid2.MasterTableView.GetColumn("LEADID").ItemStyle.Width = 0
        RadGrid2.MasterTableView.GetColumn("LEADID").HeaderStyle.Width = 0
        RadGrid2.MasterTableView.GetColumn("KEYEDBY").ItemStyle.Width = 0
        RadGrid2.MasterTableView.GetColumn("KEYEDBY").HeaderStyle.Width = 0
        RadGrid2.MasterTableView.GetColumn("CATEGORY").ItemStyle.Width = 0
        RadGrid2.MasterTableView.GetColumn("CATEGORY").HeaderStyle.Width = 0
        RadGrid2.MasterTableView.GetColumn("docpath").ItemStyle.Width = 0
        RadGrid2.MasterTableView.GetColumn("docpath").HeaderStyle.Width = 0

        RadGrid2.MasterTableView.GetColumn("status1").ItemStyle.Width = 0
        RadGrid2.MasterTableView.GetColumn("status1").HeaderStyle.Width = 0
        RadGrid2.MasterTableView.GetColumn("FSN").ItemStyle.Width = 0
        RadGrid2.MasterTableView.GetColumn("FSN").HeaderStyle.Width = 0


        If (TypeOf e.Item Is GridDataItem) Then
            Dim item As GridDataItem = e.Item
            Dim btndel As ImageButton = DirectCast(item.FindControl("imgbtn_DelDoc"), ImageButton)
            If ViewState("dec") = 1 Then
                btndel.Visible = False
            Else
                btndel.Visible = True
            End If
        End If
      

    End Sub
    Private Sub getmastresofemp(Optional ByVal fileno As String = "", Optional ByVal fsn As String = "", Optional ByVal prtid As String = "", Optional ByVal clntid As String = "")
        Try

            'If Request.QueryString("srcby") = "A" Then
            '    qry = "select  * from cbisi_crm.AGENTS_LIST where leadid=" & leadid
            'ElseIf Request.QueryString("srcby") = "L" Then
            '    qry = "select  * from cbisi_crm.BASIC_LEADS_LIST where leadid=" & leadid
            'ElseIf Request.QueryString("srcby") = "P" Then
            '    qry = "select  * from cbisi_crm.Partners_LIST where leadid=" & leadid
            'ElseIf Request.QueryString("srcby") = "C" Then
            '    qry = "select  * from cbisi_crm.BASIC_LEADS_LIST where leadid=" & clntid
            'End If
            qry = "select  * from  SYSTEM.AMS_EXIST_INV_REC_LIST where fileno='" & fileno & "'"

            dt = clas.getdata(qry, "QR")
            If dt.Rows.Count > 0 Then
                'Dim dccol11 = New DataColumn("Action", GetType(String))
                'dt.Columns.Add(dccol11)
                Label3.Text = Convert.ToString(dt.Rows(0)("name"))
                'Label4.Text = Convert.ToString(dt.Rows(0)("category"))
                Label5.Text = Convert.ToString(dt.Rows(0)("category"))
                Label7.Text = Convert.ToString(dt.Rows(0)("fileno"))
                Label4.Text = Convert.ToString(dt.Rows(0)("fsn"))

                'Label6.Text = Convert.ToString(dt.Rows(0)("country"))

                'Label8.Text = Convert.ToString(dt.Rows(0)("STATUS"))
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
    'Private Sub Bind_dogroups(Optional ByVal grpcd As String = "", Optional ByVal docid As String = "")
    '    Try
    '        qry = "select distinct grpcd from CBISI_CRM.DOCCHKLISTMASTER where GRPCD is not null"

    '        dt = clas.getdata(qry, "QR")

    '        '  Dim ddl_emp As DropDownList = DirectCast(RadGrid1.FindControl("ddl_Employer"), DropDownList)
    '        ddl_docsmast.DataTextField = "grpcd"
    '        ddl_docsmast.DataValueField = "grpcd"
    '        ddl_docsmast.DataSource = dt
    '        ddl_docsmast.DataBind()

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


    'Private Sub doc_Filter(Optional ByVal grpcd As String = "SALES", Optional ByVal docid As String = "")
    '    Try
    '        If ddl_docsmast.SelectedValue = "1" Then   'Prosepect
    '            qry = "select docid, docshrtnm as docname from CBISI_CRM.DOCCHKLISTMASTER where DOCDESCR='A'  and GRPCD='SALES'  "
    '        ElseIf ddl_docsmast.SelectedValue = "2" Then   'Agent
    '            qry = "select docid, docshrtnm as docname from CBISI_CRM.DOCCHKLISTMASTER where DOCDESCR='A'  and GRPCD='Agent'  "
    '        ElseIf ddl_docsmast.SelectedValue = "3" Then   'Partner
    '            qry = "select docid, docshrtnm as docname from CBISI_CRM.DOCCHKLISTMASTER where DOCDESCR='A'  and GRPCD='Partner'  "
    '        Else

    '        End If

    '        If docid <> "" Then
    '            qry &= "and docid='" & docid & "'"

    '        End If
    '        dt = clas.getdata(qry, "QR")
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
        '  doc_Filter(grpid, docis)
        getmaster1()
    End Sub

    Protected Sub rad_document_viewer_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadGrid1.SelectedIndexChanged

        Dim dataItem = TryCast(RadGrid1.SelectedItems(0), GridDataItem)
        If dataItem IsNot Nothing Then
            Dim SelectedDocId As String = dataItem("docid").Text
            SelectedDocId_Value = SelectedDocId
            Label2.Text = dataItem("docname").Text
            Panel2.Visible = True
            Bind_ViewGrid(SelectedDocId)
        End If


    End Sub
    Protected Sub RadGrid1_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles RadGrid1.ItemDataBound
        RadGrid1.MasterTableView.GetColumn("docid").ItemStyle.Width = 0
        RadGrid1.MasterTableView.GetColumn("docid").HeaderStyle.Width = 0
        RadGrid1.MasterTableView.GetColumn("docname").HeaderText = "Document"
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
            Label1.Text = "Add New Follow up"
            Ifrmfollowup.Attributes.Add("src", "followup_list.aspx?empid=" & e.CommandArgument.ToString())
            'ClientScript.RegisterStartupScript(Me.GetType(), "none", "<script>$('#addnewfollowup').modal('show');</script>", True)
            '  ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)
            'ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", True)
        ElseIf e.CommandName = "genagragnt" Then
            Label1.Text = "Generate Aggrement For Agent"
            Ifrmfollowup.Attributes.Add("src", "agent_agreement.aspx?empid=" & e.CommandArgument.ToString())
            'ClientScript.RegisterStartupScript(Me.GetType(), "none", "<script>$('#addnewfollowup').modal('show');</script>", True)
            '  ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)
            'ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", True)
        ElseIf e.CommandName = "editform" Then
            Label1.Text = "Edit Lead Record"
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
            'doc_Filter(ddl_docsmast.SelectedValue, docis)
            getmaster1()
            'Ifrmfollowup.Attributes.Add("src", "followup_list.aspx?empid=" & e.CommandArgument.ToString())

            'ClientScript.RegisterStartupScript(Me.GetType(), "alert", "ShowPopup();", True)

        ElseIf e.CommandName = "Filter" Then
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            'doc_Filter(ddl_docsmast.SelectedValue, docis)
            getmaster1()
        ElseIf e.CommandName = "ChangePageSize" Then
            ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            'doc_Filter(ddl_docsmast.SelectedValue, docis)
            getmaster1()
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
    Protected Sub RadGrid2_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadGrid2.PreRender
        'RadGrid1.MasterTableView.IsItemInserted = True
        '    RadGrid1.Rebind()
        'If Page.IsPostBack = False Then
        '    RadGrid1.EditIndexes.Add(0)
        '    RadGrid1.Rebind()
        'End If


        If Page.IsPostBack = False Then
            Dim column As GridColumn = TryCast(RadGrid2.MasterTableView.GetColumnSafe("docdwn"), GridColumn)
            column.OrderIndex = RadGrid2.MasterTableView.RenderColumns.Length

            ''Dim column1 As GridColumn = TryCast(GV_ChkLst.MasterTableView.GetColumnSafe("lock"), GridColumn)
            ''column1.OrderIndex = GV_ChkLst.MasterTableView.RenderColumns.Length - 5

            RadGrid2.MasterTableView.Rebind()
        End If

    End Sub
    Private Sub RadGrid1_PageIndexChanged(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridPageChangedEventArgs) Handles RadGrid1.PageIndexChanged
        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
        RadGrid1.CurrentPageIndex = e.NewPageIndex
        getmaster1()
        'doc_Filter(ddl_docsmast.SelectedValue, docis)
    End Sub
    Protected Sub Btnref_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles Button2.Click
        ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
        getmaster1()
        'doc_Filter(ddl_docsmast.SelectedValue, docis)
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        If RadGrid1.SelectedItems.Count > 0 Then

            If FileUpload1.HasFile Then


                Dim item As GridDataItem = DirectCast(RadGrid1.SelectedItems(0), GridDataItem)
                ' Dim docid As String

                Dim DocumentId As String = item("docid").Text
                Dim extension As String = System.IO.Path.GetExtension(FileUpload1.FileName).ToLower()
                Session("extension") = extension

                If Not extension = ".exe" And Not extension = ".dll" And Not extension = ".bat" And Not extension = ".iso" And Not extension = ".setup" Then

                    Dim fname As String = DocumentId & Date.Now.ToString("ddmmyyyyhhmiss") & extension
                    Dim path1 As String = Server.MapPath("libdocs") & "\" & fname
                    Dim path2 As String = "libdocs\" & fname

                    FileUpload1.SaveAs(path1)

                    Dim sql123 = "insert into ams.DOCLIB(ID,DOCID,AGID,leadid,prtid,clntid,DOCPATH,fileno,fsn,KEYEDON,KEYEDBY,KEYEDIP,REMARKS)values(ams.doclib_SEQ.nextval,'" & DocumentId & "','" & agid & "','" & leadid & "','" & prtnrid & "','" & clntid & "','" & path2 & "','" & Request.QueryString("fileno") & "','" & Request.QueryString("fsn") & "',sysdate,'" & Session("lgnagntid") & "','" & HttpContext.Current.Request.UserHostAddress & "','" & txtRem.Text.Trim & "')"

                    Dim i = clas.ExecuteNonQuery(sql123)

                    If i = 1 Then

                        RadWindowManager1.RadAlert("<ul><li><div style=color:Green>Document Saved Successfully</div></li> </ul>", 300, 100, "Document Upload Success", Nothing)

                    Else
                        RadWindowManager1.RadAlert("<ul><li><div style=color:red>Document not uploaded</div></li> </ul>", 300, 100, "Document Upload Success", Nothing)

                    End If
                    '  ClearControld()


                    item.Selected = True
                    Bind_ViewGrid(DocumentId)
                    Exit Sub
                Else
                    RadWindowManager1.RadAlert("<ul><li><div style=color:Red>Please Upload Valid File</div></li> </ul>", 300, 100, "Document Upload Failure", Nothing)
                    Exit Sub
                End If

            Else
                RadWindowManager1.RadAlert("<ul><li><div style=color:Red>Please Upload the Document</div></li> </ul>", 300, 100, "Document Upload Failure", Nothing)
                Exit Sub
            End If
        Else
            RadWindowManager1.RadAlert("<ul><li><div style=color:Red>Please select Document By Selecting the Row</div></li> </ul>", 300, 100, "Document Selection Failure", Nothing)
            Exit Sub
        End If
    End Sub



    Public Sub Email_thru_MIS(ByVal emailto As String, ByVal cc As String, ByVal emailbody As String)
        Try


            Dim Email_From As String = "mis_alerts@acumen-services.com"
            Dim Email_To As String = "anuj@wwicsgroup.com,cpanke@gpscanada.com,ninder.hayer@acumen-services.com"
            'Dim Email_To As String = "satinder.pal@pinnacleinfoedge.com"
            Email_To += "," + emailto
            'Dim Email_To As String = "meenu@pinnacleinfoedge.com"
            Dim objmail As New MailMessage(Email_From, Email_To)
            objmail.Bcc.Add("itdept@wwicsgroup.com")
            If txtCC.Text.Trim <> "" Then
                objmail.CC.Add(txtCC.Text.Trim)
            End If
            objmail.Subject = "Acumen MIS Document Upload Intimation" & Convert.ToString(DateTime.Now)
            objmail.IsBodyHtml = True

            Dim Mail_Body As String = ""


            Dim Mail_Str As StringBuilder = New StringBuilder
            Mail_Str.Append("<html><head></head><title></title>")
            Mail_Str.Append("<body style='font-size:14px;font-family:Times New Roman;'>")

            Mail_Str.Append("<p><h2><Strong>Acumen MIS  Document Upload Intimation </Strong></h2></p> <hr />")

            Mail_Str.Append("<p><b> Document Uploaded  On: </b>" & DateTime.Now & " </p> <hr /> <br/>")
            Mail_Str.Append("<p>Dear User, </p>")

            Mail_Str.Append("<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Acumen MIS Document Uploaded for Following User.</p>")
            'Mail_Str.Append("<p> <b>Acumen MIS Report</b></p>")



            Mail_Str.Append("<table border='1px' cellpadding='5' cellspacing='0' ")
            Mail_Str.Append("style='border: solid 1px Silver; font-size: 12px;font-family:Times New Roman;'>")

            Mail_Str.Append("<tr align='center' valign='top'>")
            Mail_Str.Append("<td align='left' valign='top'><b>&nbsp;&nbsp;")
            Mail_Str.Append("Username")
            Mail_Str.Append("&nbsp;&nbsp;</b></td>")
            Mail_Str.Append("<td align='left' valign='top'><b>&nbsp;&nbsp;")
            Mail_Str.Append("DateTime")
            Mail_Str.Append("&nbsp;&nbsp;</b></td>")
            Mail_Str.Append("</tr>")

            Mail_Str.Append("<tr align='center' valign='top'>")
            Mail_Str.Append("<td align='left' valign='top'>")
            Mail_Str.Append(Session("lgnagntnam"))
            Mail_Str.Append("</td>")
            Mail_Str.Append("<td align='left' valign='top'>")
            Mail_Str.Append(Convert.ToString(DateTime.Now))
            Mail_Str.Append("</td>")

            Mail_Str.Append("</tr>")

            Mail_Str.Append("</table> <br/>")


            Mail_Str.Append("<table border='1px' cellpadding='5' cellspacing='0' ")
            Mail_Str.Append("style='border: solid 1px Silver; font-size: 12px;font-family:Times New Roman;'>")
            Mail_Str.Append("<tr align='center' valign='top'>")
            Mail_Str.Append("<td align='Center' valign='top'>&nbsp;&nbsp;")
            Mail_Str.Append("Content")
            Mail_Str.Append("&nbsp;&nbsp;</b></td>")
            Mail_Str.Append("</tr>")

            Mail_Str.Append("<tr align='center' valign='top'>")
            Mail_Str.Append("<td align='left' valign='top'>&nbsp;&nbsp;")
            Mail_Str.Append(emailbody)
            Mail_Str.Append("&nbsp;&nbsp;</b></td>")
            Mail_Str.Append("</tr>")
            Mail_Str.Append("</table> <br/>")


            Mail_Str.Append("<p> <b>Regards,</b></p>")
            Mail_Str.Append("<p> MIS (z-aXis) </p>")
            Mail_Str.Append("</body> </html>")

            Mail_Body = Mail_Str.ToString

            objmail.Body = Mail_Body
            Dim objsent As New SmtpClient("121.0.0.219")
            objsent.UseDefaultCredentials = True
            objsent.Credentials = New System.Net.NetworkCredential("mis_alerts@acumen-services.com", "Malerts")
            objsent.Send(objmail)
            ClientScript.RegisterStartupScript([GetType](), "alert", "alert('Email sent to registered email id');window.close();", True)
            txtemail.Text = ""
            TextBox10.Text = ""
            txtCC.Text = ""
        Catch ex As Exception
            ClientScript.RegisterStartupScript([GetType](), "alert", "alert('Some error occur');window.close();", True)
        End Try
    End Sub

    Protected Sub btnsend_Click(sender As Object, e As System.EventArgs) Handles btnsend.Click
        If txtemail.Text.Trim = "" Or TextBox10.Text.Trim = "" Then
            RadWindowManager1.RadAlert("<ul><li><div style=color:Red>Please enter email/body content</div></li> </ul>", 300, 100, "Document Selection Failure", Nothing)
            Exit Sub
        End If


        Email_thru_MIS(txtemail.Text.Trim, txtCC.Text, TextBox10.Text.Trim)
    End Sub


    Protected Sub btnDeactive_Click(sender As Object, e As System.EventArgs) Handles btnDeactive.Click
        
        Bind_ViewGrid_all("D")

    End Sub

    Protected Sub btnAll_Click(sender As Object, e As System.EventArgs) Handles btnAll.Click
        
        Bind_ViewGrid_all("A")
    End Sub


End Class
