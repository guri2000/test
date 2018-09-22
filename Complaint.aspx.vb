Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data
Imports System.Text
Imports System.IO
Imports System.Net
Imports System.Net.Mail

Partial Class Complaint
    Inherits System.Web.UI.Page
    Dim clas As rahulcpm.commonclass = New rahulcpm.commonclass()
    Dim candid As Int32
    Dim login_emp As String = ""
    Dim mywebReq As WebRequest
    Dim mywebResp As WebResponse
    Dim sr As StreamReader
    Dim strHTML As String
    Dim sw As StreamWriter
    Dim agntid As Int32

   

    Private Sub Bind_Complaint_Type_Status()
        Dim SqlQuery = "SELECT CONCERNID,CON_DESC FROM Meenu.AMS_Complaint_Type WHERE STATUS='A' "
        Dim _DataTable = clas.getdata(SqlQuery, "TX")
        _DataTable.Rows.Add("0", "Select Type")
        Dim dv As New DataView(_DataTable)
        dv.Sort = "CONCERNID ASC"
        '_DataTable.DefaultView.Sort = "STATID"
        '_DataTable = _DataTable.DefaultView.Table
        _DataTable = dv.ToTable
        If _DataTable.Rows.Count > 0 Then
            'ddl_Reason.Items.Add(New ListItem("Select Valid Reason", ""))
            ddl_Concern.ClearSelection()
            ddl_Concern.Items.Clear()
            ddl_Concern.DataTextField = "CON_DESC"
            ddl_Concern.DataValueField = "CONCERNID"
            ddl_Concern.DataSource = _DataTable
            ddl_Concern.DataBind()
        End If
    End Sub

    Private Shared Function GetHtmlPage(strURL As String) As String

        Dim strResult As [String]
        Dim objResponse As WebResponse
        Dim objRequest As WebRequest = HttpWebRequest.Create(strURL)
        objResponse = objRequest.GetResponse()
        Using sr As New StreamReader(objResponse.GetResponseStream())
            strResult = sr.ReadToEnd()
            sr.Close()
        End Using
        Return strResult
    End Function

    Protected Sub SaveHTML(HTML_Text As [String], Html_Name As [String])
        Dim strFileName As [String] = System.IO.Path.GetRandomFileName()
        Dim strFriendlyName As [String] = Html_Name + ".html"
        Using sw As StreamWriter = New System.IO.StreamWriter(Server.MapPath("SavedMails" + "\" + strFriendlyName))
            sw.WriteLine(HTML_Text)
            sw.Close()
        End Using


    End Sub

    Protected Sub Read_Old_tkt_Det()
        Dim Select_Query As String = "Select MAIL_ID,AGENTID,MAILTO,MAILFROM,BCC,CC,SUBJECT,BODY,ATTACHMENT,MAILPATH,CONCERNID,STATUS,to_char(SENDON,'dd-Mon-yyyy') as SENDON,SENDBY,TICKETNO,STATUS_ID,to_char(CLOSEDON,'dd-Mon-yyyy') as CLOSEDON, REMARKS from Meenu.Ams_mails where agentid='" & agntid & "' order by MAIL_ID desc"
        Dim DT As DataTable = clas.getdata(Select_Query, "TX")
        If DT.Rows.Count > 0 Then
            For i As Integer = 0 To DT.Rows.Count - 1
                Dim get_Att_Files As String = "Select DOCID,MAIL_ID,STATE,REMARKS,DOCPATH,KEYEDON,KEYEDBY from Meenu.AMS_DocList Where MAIL_ID='" & DT.Rows(i)("MAIL_ID").ToString & "'"
                Dim DT_att_Files As DataTable = clas.getdata(get_Att_Files, "TX")
                Dim Att_files As StringBuilder = New StringBuilder
                If DT_att_Files.Rows.Count > 0 Then
                    For j As Integer = 0 To DT_att_Files.Rows.Count - 1
                        Dim FN As String = String.Empty
                        FN = Path.GetFileName(DT_att_Files.Rows(j)("DOCPATH"))
                        Dim fip = Request.Url.GetLeftPart(UriPartial.Authority) & Request.ApplicationPath & "/MailAttachment/Files/" & FN
                        Att_files.Append("<a href='" + fip + "' target='_blank'>" + FN + "</a><br>")
                    Next
                End If

                Dim Concern As String = String.Empty
                If DT.Rows(i)("CONCERNID").ToString = "1" Then
                    Concern = "Application Crash"
                ElseIf DT.Rows(i)("CONCERNID").ToString = "2" Then
                    Concern = "Invalid / Irrelevant Data"
                ElseIf DT.Rows(i)("CONCERNID").ToString = "3" Then
                    Concern = "User Interface Issue"
                End If
                Dim CC As String = DT.Rows(i)("CC").ToString
                Dim CCData As String() = CC.Split(";")
                Dim CC1 As String = String.Empty
                Dim CC2 As String = String.Empty
                Dim CC3 As String = String.Empty
                Dim CC4 As String = String.Empty
                If CCData.Length > 0 Then
                    For k As Integer = 0 To CCData.Length - 1
                        If k = 0 Then
                            CC1 = CCData(k).ToString
                        ElseIf k = 1 Then
                            CC2 = CCData(k).ToString
                        ElseIf k = 2 Then
                            CC3 = CCData(k).ToString
                        ElseIf k = 3 Then
                            CC4 = CCData(k).ToString
                        End If
                    Next
                End If

                Dim Inprocess As String = "none"
                Dim Pending As String = "none"
                Dim Closed As String = "none"
                If DT.Rows(i)("STATUS_ID").ToString() = "1" Then
                    Pending = "block"
                ElseIf DT.Rows(i)("STATUS_ID").ToString() = "2" Then
                    Inprocess = "block"
                ElseIf DT.Rows(i)("STATUS_ID").ToString() = "3" Then
                    Closed = "block"
                End If

                'Dim result As String = String.Empty
                'result = Path.GetFileName(DT.Rows(i)("MailPath"))
                'Dim body As String = String.Empty
                'Dim reader As StreamReader = New StreamReader(Server.MapPath("MailAttachment\MailFiles\SendMailFiles\") & result)
                'body = reader.ReadToEnd
                'reader.Close()

                Dim TktNo As String = String.Empty
                TktNo = DT.Rows(i)("MailPath")
                Dim body As String = String.Empty
                Dim reader As StreamReader = New StreamReader(Server.MapPath("MailAttachment/MailFiles/Old_Mail_Format.html"))
                body = reader.ReadToEnd
                reader.Close()

                body = body.Replace("{To}", DT.Rows(i)("MAILTO").ToString)
                body = body.Replace("{CC1}", CC1)
                body = body.Replace("{CC2}", CC2)
                body = body.Replace("{CC3}", CC3)
                body = body.Replace("{CC4}", CC4)
                body = body.Replace("{Concern}", Concern)
                body = body.Replace("{Subject}", DT.Rows(i)("SUBJECT").ToString)
                body = body.Replace("{TktNo}", DT.Rows(i)("TICKETNO").ToString)
                body = body.Replace("{Date}", DT.Rows(i)("SENDON").ToString)
                body = body.Replace("{BODYPART}", DT.Rows(i)("BODY").ToString)
                body = body.Replace("{TRROW}", Att_files.ToString)
                body = body.Replace("{CloseDate}", DT.Rows(i)("CLOSEDON").ToString)
                body = body.Replace("{Remarks}", DT.Rows(i)("REMARKS").ToString)

                body = body.Replace("{Display2}", Pending)
                body = body.Replace("{Display1}", Inprocess)
                body = body.Replace("{Display3}", Closed)

                Dim divNew As New HtmlGenericControl("div")
                'divNew.Attributes.Add("class", "myClass")
                divNew.Attributes.Add("id", TktNo.ToString())
                divNew.InnerHtml = body
                'divNew.Attributes.Add("InnerHtml", body)
                pnl_Old_Tkts.Controls.Add(divNew)

            Next
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session("lgnagntid") <> "0" Or Session("lgnagntid") <> "" Then
            agntid = Session("lgnagntid")
        Else
            Response.Redirect("logout.aspx")
        End If
        sendfrom.Text = "itsupport@acumen-services.com"
        sendfrom.Enabled = False
        Read_Old_tkt_Det()
        If Page.IsPostBack = False Then
            Bind_Complaint_Type_Status()
        End If
    End Sub

    Protected Sub imgbtn_AddAttachment_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_AddAttachment.Click
        Panel2.Visible = True
    End Sub

    Protected Sub btnsave_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnsave.Click
        Dim Ccdata As String = ""
        Try
            Using mm As New MailMessage(sendfrom.Text.ToString.Trim(), TextsendTo.Text.Trim())
                mm.Subject = TextSubject.Text.ToString()
                mm.Body = FTB_Msg.Text.ToString()

                If BCC.Text <> "" Then
                    mm.Bcc.Add(BCC.Text)
                End If


                If CC1.Text <> "" Then
                    mm.CC.Add(CC1.Text)
                    Ccdata = CC1.Text
                End If
                If CC2.Text <> "" Then
                    mm.CC.Add(CC2.Text)
                    Ccdata += ";" + CC2.Text
                End If
                If CC3.Text <> "" Then
                    mm.CC.Add(CC3.Text)
                    Ccdata += ";" + CC3.Text
                End If
                If CC4.Text <> "" Then
                    mm.CC.Add(CC4.Text)
                    Ccdata += ";" + CC4.Text
                End If

                Dim attachfiles As New StringBuilder()

                If DataList1.Items.Count > 0 Then
                    Dim athdata As DataTable = DirectCast(ViewState("grddata"), DataTable)

                    For i As Int32 = 0 To athdata.Rows.Count - 1
                        If athdata.Rows(i)("path") IsNot Nothing Then
                            If System.IO.File.Exists(athdata.Rows(i)("path").ToString()) = True Then
                                Dim att As New Attachment(athdata.Rows(i)("path").ToString())
                                mm.Attachments.Add(att)
                                'Dim fip = "http://121.0.0.79:999/AMS/MailAttachment/Files/" + athdata.Rows(i)("dupnam").ToString()
                                'Dim fip = Server.MapPath(Convert.ToString("MailAttachment\Files\") & athdata.Rows(i)("dupnam").ToString())
                                Dim fip = Request.Url.GetLeftPart(UriPartial.Authority) & Request.ApplicationPath & "/MailAttachment/Files/" & athdata.Rows(i)("dupnam").ToString()
                                attachfiles.Append("<a href='" + fip + "' target='_blank'>" + athdata.Rows(i)("file").ToString() + "</a><br>")
                            End If


                        End If
                    Next
                End If
                mm.IsBodyHtml = True
                Dim smtp As New SmtpClient("121.0.0.219")
                Dim NetworkCred As NetworkCredential = New System.Net.NetworkCredential("itsupport@acumen-services.com", "support")
                smtp.UseDefaultCredentials = True
                smtp.Credentials = NetworkCred
                smtp.Send(mm)
                Dim reader As New StreamReader(Server.MapPath("MailAttachment/MailFiles/MailFormat.html"))
                Dim bodystr As String = reader.ReadToEnd()
                reader.Close()

                bodystr = bodystr.Replace("{FROM}", sendfrom.Text)
                bodystr = bodystr.Replace("{TO}", TextsendTo.Text)
                bodystr = bodystr.Replace("{CC}", Ccdata)
                bodystr = bodystr.Replace("{BCC}", BCC.Text)
                bodystr = bodystr.Replace("{SUBJECT}", TextSubject.Text.ToString())
                bodystr = bodystr.Replace("{DATE}", DateTime.Now.ToString())
                bodystr = bodystr.Replace("{BODYPART}", FTB_Msg.Text)
                bodystr = bodystr.Replace("{TRROW}", attachfiles.ToString())

                Dim transid As Integer = clas.getMaxID("Select nvl(max(Mail_ID),0)+1 as ID from Meenu.AMS_Mails")

                Dim fname As String = transid & "_" & DateTime.Now.ToString("ddmmyyhhmmss")
                Dim file__1 = Path.Combine(Server.MapPath("MailAttachment/MailFiles/SendMailFiles/"), fname + ".html")

                File.WriteAllText(file__1, bodystr)

                Dim atchdata As New DataTable()
                Dim athmode As String = "N"
                If DataList1.Items.Count > 0 Then
                    atchdata = DirectCast(ViewState("grddata"), DataTable)
                End If
                If atchdata.Rows.Count > 0 Then
                    athmode = "Y"
                Else
                    athmode = "N"
                End If


                Dim StrQuery2 As String = ""
                StrQuery2 = "Insert into Meenu.AMS_Mails(MAIL_ID,AGENTID,MAILTO,MAILFROM,BCC,CC,SUBJECT,BODY,ATTACHMENT,MAILPATH,CONCERNID,STATUS,SENDON,SENDBY,TICKETNO) Values"
                StrQuery2 += " ("
                StrQuery2 += " '" & transid & "','" & agntid & "','" & TextsendTo.Text.Trim() & "','" & sendfrom.Text.Trim() & "','" & BCC.Text.Trim() & "','" & Ccdata & "',"
                StrQuery2 += " '" & TextSubject.Text.ToString() & "','" & FTB_Msg.Text & "','" & athmode & "','" & file__1 & "','" & ddl_Concern.SelectedValue & "','A',"
                StrQuery2 += " to_date('" & Date.Now.ToString("dd-MMM-yyyy HH:mm:ss") & "','dd-MON-yyyy hh24:mi:ss'),'" & agntid & "','" & fname & "'"
                StrQuery2 += ")"

                clas.ExecuteNonQuery(StrQuery2)

                If athmode = "Y" Then
                    If atchdata.Rows.Count > 0 Then
                        For i As Int32 = 0 To atchdata.Rows.Count - 1
                            If atchdata.Rows(i)("path") IsNot Nothing Then
                                Dim Doc_id As Integer = clas.getMaxID("SELECT nvl(max(DocId),0)+1 from Meenu.AMS_DocList")

                                StrQuery2 = "Insert Into Meenu.AMS_DocList ( DOCID,MAIL_ID,STATE,REMARKS,DOCPATH,KEYEDON,KEYEDBY) Values "
                                StrQuery2 += "  ("
                                StrQuery2 += " '" & Doc_id & "','" & transid & "','A',"
                                StrQuery2 += " '" & FTB_Msg.Text & "','" & atchdata.Rows(i)("path") & "',to_date('" & Date.Now.ToString("dd-MMM-yyyy HH:mm:ss") & "','dd-MON-yyyy hh24:mi:ss'),'" & agntid & "'"
                                StrQuery2 += " )"

                                  clas.ExecuteNonQuery(StrQuery2)
                            End If
                        Next
                    End If
                End If


                Label1.Text = "Mail Send Successfully on " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss")
                Label1.CssClass = "succls"
                TextSubject.Text = String.Empty
                Label1.ForeColor = System.Drawing.Color.Green

            End Using

            TextsendTo.Text = String.Empty
            CC1.Text = String.Empty
            CC2.Text = String.Empty
            CC3.Text = String.Empty
            CC4.Text = String.Empty
            BCC.Text = String.Empty
            FTB_Msg.Text = String.Empty
            TextSubject.Text = String.Empty
            ddl_Concern.ClearSelection()
            DataList1.DataSource = Nothing
            DataList1.DataBind()
            ViewState.Remove("grddata")
            ViewState("grddata") = ""
            Label7.Visible = False

        Catch ex As Exception
            If True Then
                Label1.Text = "Mail Sending Problem like " + ex.Message.ToString()
                Label1.CssClass = "errcls"
            End If
        End Try


    End Sub

    

    

    Protected Function grddata() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("File", GetType(System.String))
        dt.Columns.Add("path", GetType(System.String))
        dt.Columns.Add("type", GetType(System.String))
        dt.Columns.Add("size", GetType(System.Int32))
        dt.Columns.Add("dupnam", GetType(System.String))
        dt.Columns("size").DefaultValue = "0"
        Return dt
    End Function

    Public Function getimgicn(filetyp As String) As String
        Dim imgname As String = ""
        If filetyp = "application/pdf" Then
            imgname = ""
        ElseIf filetyp = "application/msword" Then

        ElseIf filetyp = "application/vnd.ms-powerpoint" Then

        ElseIf filetyp.Contains("image/") Then

        ElseIf filetyp = "application/zip" Then

        ElseIf filetyp = "unknown/unknown" Then
        Else
        End If
        Return imgname
    End Function

    Protected Sub btncancel_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btncancel.Click
        'Me.ClientScript.RegisterClientScriptBlock(Me.[GetType](), "Close", "window.close()", True)
        TextsendTo.Text = String.Empty
        CC1.Text = String.Empty
        CC2.Text = String.Empty
        CC3.Text = String.Empty
        CC4.Text = String.Empty
        BCC.Text = String.Empty
        FTB_Msg.Text = String.Empty
        TextSubject.Text = String.Empty
        ddl_Concern.ClearSelection()
        DataList1.DataSource = Nothing
        DataList1.DataBind()
        ViewState.Remove("grddata")
        ViewState("grddata") = ""
        Label1.Text = String.Empty
        Label7.Visible = False
    End Sub

    Protected Sub DataList1_ItemCommand(source As Object, e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles DataList1.ItemCommand
        Panel2.Visible = False
        Dim athdata As DataTable = DirectCast(ViewState("grddata"), DataTable)
        Dim rows As DataRow()
        rows = athdata.[Select]("path ='" + e.CommandArgument + "'")
        For Each row As DataRow In rows
            athdata.Rows.Remove(row)
        Next
        ViewState("grddata") = athdata
        DataList1.DataSource = Nothing
        DataList1.DataBind()
        DataList1.DataSource = athdata
        DataList1.DataBind()
        If DataList1.Items.Count > 0 Then
            Label7.Visible = True
        Else
            Label7.Visible = False
        End If
    End Sub

    Protected Sub btndraft_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btndraft.Click
        Dim attachfiles As New StringBuilder()
        If DataList1.Items.Count > 0 Then
            Dim athdata As DataTable = DirectCast(ViewState("grddata"), DataTable)

            For i As Int32 = 0 To athdata.Rows.Count - 1
                If athdata.Rows(i)("dupnam") IsNot Nothing Then
                    If System.IO.File.Exists(athdata.Rows(i)("dupnam").ToString()) = True Then

                        Dim fip = "http://121.0.0.79:999/AMS/MailAttachment/Files/" + athdata.Rows(i)("dupnam").ToString()

                        attachfiles.Append("<a href='" + fip + "' target='_blank'>" + athdata.Rows(i)("file").ToString() + "</a><br>")
                    End If


                End If
            Next
        End If

        Dim Ccdata As String = ""
        If CC1.Text <> "" Then

            Ccdata = CC1.Text
        End If
        If CC2.Text <> "" Then
            Ccdata += "," + CC2.Text
        End If
        If CC3.Text <> "" Then
            Ccdata += "," + CC3.Text
        End If
        If CC4.Text <> "" Then
            Ccdata += "," + CC4.Text
        End If

        Dim reader As New StreamReader(Server.MapPath("MailAttachment/MailFiles/mailformat.html"))
        Dim bodystr As String = reader.ReadToEnd()
        reader.Close()
        bodystr = bodystr.Replace("{FROM}", sendfrom.Text)
        bodystr = bodystr.Replace("{TO}", TextsendTo.Text)
        bodystr = bodystr.Replace("{CC}", Ccdata)
        bodystr = bodystr.Replace("{BCC}", BCC.Text)
        bodystr = bodystr.Replace("{SUBJECT}", TextSubject.Text.ToString())
        bodystr = bodystr.Replace("{DATE}", DateTime.Now.ToString())
        bodystr = bodystr.Replace("{BODYPART}", FTB_Msg.Text)
        bodystr = bodystr.Replace("{TRROW}", attachfiles.ToString())

        Dim fname As String = DateTime.Now.ToString("ddmmyyhhmmss")
        Dim file__1 = Path.Combine(Server.MapPath("MailAttachment/MailFiles/SendMailFiles/"), fname + ".html")
        File.WriteAllText(file__1, bodystr)
        Dim sql3 As [String] = ""



        Dim atchdata As New DataTable()
        Dim athmode As [String] = "N"
        If DataList1.Items.Count > 0 Then
            atchdata = DirectCast(ViewState("grddata"), DataTable)
        End If

        If atchdata.Rows.Count > 0 Then
            athmode = "Y"
        Else
            athmode = "N"
        End If
        Dim id As Long = 0 'getMaxsentid()
        Dim [sub] = TextSubject.Text.ToString()

        sql3 = ""
      
        If True Then
            If atchdata.Rows.Count > 0 Then
                For i As Int32 = 0 To atchdata.Rows.Count - 1
                    If atchdata.Rows(i)("path") IsNot Nothing Then
                        sql3 = ""
                        
                    End If
                Next
            End If
        End If


        Label1.Text = "mail saved in draft on " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss")
        Label1.CssClass = "succls"
    End Sub

    Protected Sub imgbtn_Upload_attachment_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgbtn_Upload_attachment.Click
        Dim athdata As New DataTable()
        If (ViewState("grddata") IsNot Nothing) AndAlso (DataList1.Items.Count <> 0) Then
            athdata = DirectCast(ViewState("grddata"), DataTable)
        Else
            athdata = grddata()
        End If
        If FileUpload1.HasFile = True Then
            Dim fileSize As Long = FileUpload1.PostedFile.ContentLength
            Dim sum As Long = 0
            If athdata.Rows.Count > 0 Then

                sum = Convert.ToInt32(athdata.Compute("SUM(size)", String.Empty))
            End If

            Dim sumafteruplod As Long = sum + fileSize
            If sumafteruplod <= 20728650 Then

                Dim actfilenam As [String] = FileUpload1.PostedFile.FileName
                If (Not actfilenam.Contains(".msi")) AndAlso (Not actfilenam.Contains(".exe")) AndAlso (Not actfilenam.Contains(".dll")) AndAlso (Not actfilenam.Contains(".sln")) AndAlso (Not actfilenam.Contains(".ini")) Then
                    Dim filename As [String] = DateTime.Now.ToString("ddmmyyyyhhmmss") & "_" & Path.GetFileName(FileUpload1.PostedFile.FileName.Replace(" ", "_"))
                    FileUpload1.SaveAs(Server.MapPath("MailAttachment/Files/" + filename))
                    Dim dr As DataRow = athdata.NewRow()
                    dr(0) = actfilenam
                    dr(1) = Server.MapPath("MailAttachment/Files/" + filename)
                    dr(2) = FileUpload1.PostedFile.ContentType
                    dr(3) = fileSize
                    dr(4) = filename
                    athdata.Rows.Add(dr)
                    ViewState("grddata") = athdata
                    DataList1.DataSource = Nothing
                    DataList1.DataBind()
                    DataList1.DataSource = athdata
                    DataList1.DataBind()
                    Panel2.Visible = False
                Else
                    Label6.Text = "Please upload images/documents/pdf/ppt"
                    Label6.CssClass = "errcls"
                End If
            Else
                Label6.Text = "Mail size should not be more than 20 MB"
                Label6.CssClass = "errcls"
            End If
            If DataList1.Items.Count > 0 Then
                Label7.Visible = True
            Else
                Label7.Visible = False
            End If
        End If
    End Sub

   
End Class
