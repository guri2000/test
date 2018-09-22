Imports System.Data
Imports System.IO
Imports Telerik.Web.UI
Imports System.Text
Imports System.Environment

Imports System.Threading
Imports word = Microsoft.Office.Interop.Word
Imports Syncfusion.DocIO.DLS
Imports Syncfusion.DocToPDFConverter
Imports Syncfusion.Pdf
Imports Syncfusion.DocIO


Partial Class _Default
    Inherits System.Web.UI.Page
    Dim clas As rahulcpm.commonclass = New rahulcpm.commonclass()

    Dim qry As String
    Dim dt As DataTable
    Dim sydate As String
    Dim agntid As Int32
    Dim msg As String
    Dim agno As Int64

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            'If Request.QueryString("frm") IsNot Nothing And Not Request.QueryString("frm") = "" Then
            '    Session("mode") = ""
            'End If
            Session("mode") = ""
            'fillcand()
            fillREPORT_TO()
        Else

        End If
        'ClientScript.RegisterStartupScript(Me.GetType, "loads", "ShowProgress();")

        ' ScriptManager.RegisterClientScriptBlock(Me.GetType, Me.GetType, "script", "HideProgress();", True)


        If Session("lgnagntid") = "0" Or Session("lgnagntid") = "" Then
            'Response.Redirect("login.aspx")
            ImageButton1.Enabled = False
            Button2.Enabled = False
            RadWindowManager1.RadAlert("session is expired, Please login Again", 300, 200, "Session Expired Error", "")
            Response.Redirect("logout.aspx")
        Else
            agntid = Session("lgnagntid")
        End If

        If Request.QueryString("mode") = "edit" And Session("mode") = "" Then
            getrcdforedit()
            Button2.Enabled = False

        Else

        End If
        ' Ifrm1.Attributes.Add("src", "cat_det.aspx?mode=C")

    End Sub

    Private Sub getmastres(ByVal ctid As String)
        'qry = "select * from ams.ams_subcat where subcatsts='A' and catid='" & ctid & "'"
        'dt = clas.getdata(qry, "QR")
        'DropDownList3.Items.Clear()
        'DropDownList3.DataBind()
        'DropDownList3.Items.Add(New ListItem("Select Sub-Category", ""))
        'If dt.Rows.Count > 0 Then
        '    DropDownList3.DataTextField = "subcatnam"
        '    DropDownList3.DataValueField = "subcatid"
        '    DropDownList3.DataSource = dt
        '    DropDownList3.DataBind()
        '    DropDownList3.Items.Insert(0, (New ListItem("Select Sub-Category", "")))

        'End If


        'a1.Attributes.Remove("style")
        'a1.Attributes.Add("style", "display:inline")
        'Ifrm_ind.Attributes.Add("src", "cat_det.aspx?mode=S&catid=" & ctid)
    End Sub
    Private Sub getrcdforedit()
        Try
            qry = "select CANDNAME,REPTTO,CANDADDRESS from AMS.BDC_LEAD_MASTER where CANDID='" & Request.QueryString("CANDID") & "'"
            dt = clas.getdata(qry, "QR")

            If dt.Rows.Count > 0 Then
                Button1.Text = "Update"
                ' ctr_txtAdvT.Text = Convert.ToString(dt.Rows(0)("ADVTXT"))

                txtBname.Text = Convert.ToString(dt.Rows(0)("CANDNAME"))
                txtBAdd.Text = Convert.ToString(dt.Rows(0)("CANDADDRESS"))

                ' TextBox3.Text = Convert.ToString(dt.Rows(0)("CANDEML"))

                If IsDBNull(dt.Rows(0)("REPTTO")) = False Then
                    ddl_Report.Items.FindByValue(Convert.ToString(dt.Rows(0)("REPTTO"))).Selected = True
                End If

                Session("mode") = "edit"

                qry = "select WNAME,  WADDRESS,  AGR_CITY,  AGR_PROV,  AGRNO,  KEYEDON,  KEYEDBY,  KEYEDIP from AMS.BDC_WITNESS  where agrno='" & Request.QueryString("AGRNO") & "'"
                dt = clas.getdata(qry, "QR")

                If dt.Rows.Count > 0 Then
                    txtWname.Text = Convert.ToString(dt.Rows(0)("WNAME"))
                    txtWAdd.Text = Convert.ToString(dt.Rows(0)("WADDRESS"))
                    txtAcity.Text = Convert.ToString(dt.Rows(0)("AGR_CITY"))

                    'If IsDBNull(dt.Rows(0)("AGR_CITY")) = False Then

                    '    DropDownList1.Items.FindByValue(Convert.ToString(dt.Rows(0)("AGR_CITY"))).Selected = True
                    'End If
                    If IsDBNull(dt.Rows(0)("AGR_PROV")) = False Then
                        DropDownList2.Items.FindByValue(Convert.ToString(dt.Rows(0)("AGR_PROV"))).Selected = True
                    End If

                End If
            Else
                Button1.Enabled = False

                ImageButton1.Enabled = False
                ImageButton1.ToolTip = " Invalid Record ID for Edit"
            End If
            Session("mode") = "edit"
        Catch ex As Exception
            Session("error") = ex.Message.ToString
            RadWindowManager1.RadAlert(ex.Message, 300, 200, "Exception", "")
            'Dim AlertMessage As String = "alert('" & ex.Message.ToString & "')"
            'ScriptManager.RegisterStartupScript(Page, Page.GetType, "AlertMessage", AlertMessage, True)
            Session("mode") = "edit"
        End Try
    End Sub

    Private Sub fillcand()
        qry = " SELECT CANDNAME,CANDPHN, (select substr(ADVDESC,1,20) from AMS.ADV_MASTER a where a.advid=b.ADVID) as adv FROM AMS.BDC_LEAD_MASTER b where candid='" & Request.QueryString("candid") & "'"
        dt = clas.getdata(qry, "tx")
        If dt.Rows.Count > 0 Then
            'lblName.Text = " Name: " & dt.Rows(0)("CANDNAME")
            'Label2.Text = "Phone: " & dt.Rows(0)("CANDPHN")
            'Label3.Text = "Advertisement: " & dt.Rows(0)("adv")
        End If
    End Sub
    Private Sub fillREPORT_TO()
        'select lgnagntid,(select agntname from ams.ams_agents where agntid=lgnagntid) from ams.ams_usermaster where lgnsts='A'
        Try

            qry = "select lgnagntid,(select agntname from ams.ams_agents where agntid=lgnagntid) AS NAME from ams.ams_usermaster where lgnsts='A'  AND LGNROLL='S'"
            dt = clas.getdata(qry, "QR")
            ddl_Report.DataTextField = "NAME"
            ddl_Report.DataValueField = "lgnagntid"
            ddl_Report.DataSource = dt
            ddl_Report.DataBind()


            '    qry = "SELECT TRANSID, NAME FROM TT.ASSFORCOUNTRY  where active='A'"
            qry = "SELECT TRANSID, NAME FROM TT.ASSFORCOUNTRY  where JoSCP in (1,0) And Active='A' Order By Name Asc"
            dt = clas.getdata(qry, "QR")
            'DropDownList1.DataTextField = "NAME"
            'DropDownList1.DataValueField = "TRANSID"
            'DropDownList1.DataSource = dt
            'DropDownList1.DataBind()

            DropDownList2.DataTextField = "NAME"
            DropDownList2.DataValueField = "TRANSID"
            DropDownList2.DataSource = dt
            DropDownList2.DataBind()
        Catch ex As Exception
            Session("error") = ex.Message.ToString
            RadWindowManager1.RadAlert(ex.Message, 300, 200, "Exception", "")
        End Try
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click, ImageButton1.Click
        Try

      
        Dim fileName As String
        Dim flag As Boolean
        'print(Request.QueryString("CANDID").ToString, Request.QueryString("AGRNO").ToString)

        If Button1.Text = "Update" Then

            flag = updateCommand()

            If flag = True Then
                'RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Information Updated Successfully </div></li></ul>", 300, 150, "Validation Success", Nothing)
            End If
        Else

        End If

        Catch ex As Exception
            Session("error") = ex.Message.ToString
            RadWindowManager1.RadAlert(ex.Message, 300, 200, "Exception", "")
            'Dim AlertMessage As String = "alert('" & ex.Message.ToString & "')"
            'ScriptManager.RegisterStartupScript(Page, Page.GetType, "AlertMessage", AlertMessage, True)
        End Try

    End Sub


    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        clear()
        ' getmastdesg()
        ' getmastindustry()
    End Sub


    Public Function updateCommand() As Boolean
        Try
            'Dim fileName As String
            ''Dim transid As Int32 = getMaxID()
            qry = "select AGRNO from AMS.BDC_LEAD_MASTER where candid='" & Request.QueryString("CANDID").ToString.Trim & "'"

            dt = clas.getdata(qry, "tx")
            If dt.Rows.Count > 0 Then
                If Convert.ToString(dt.Rows(0)("AGRNO")).Length > 1 Then
                    agno = Convert.ToString(dt.Rows(0)("AGRNO"))
                End If
            End If

            If agno.ToString.Length > 1 Then

            Else
                agno = getAgrno()

            End If

            Dim str1 As New StringBuilder
            str1.Append("UPDATE  AMS.BDC_LEAD_MASTER SET ")

            If txtBname.Text <> "" Then
                str1.Append("CANDNAME='" & txtBname.Text.Trim & "',")
            End If

            If txtBAdd.Text <> "" Then
                str1.Append("CANDADDRESS='" & txtBAdd.Text.Trim & "',")
            End If

            If ddl_Report.SelectedValue <> "" Then
                str1.Append("REPTTO='" & ddl_Report.SelectedValue & "',")
            End If
            str1.Append("AGRNO='" & agno & "'")


            str1.Append(" where CANDID='" & Request.QueryString("CANDID") & "'")

            If str1.ToString().Length > 0 Then

                Dim i = clas.ExecuteNonQuery(str1.ToString())
                If i = 1 Then

                End If
            End If


            'qry = "SELECT COUNT(*) FROM AMS.BDC_WITNESS WHERE AGRNO='" & agno & "'"
            'dt = clas.getdata(qry, "TX")

            If Request.QueryString("AGRNO").ToString.Trim.Length > 0 Then
                update_witness()
            Else
                insert_witness()
            End If

            print(Request.QueryString("CANDID").ToString, agno)
            Return True
        Catch ex As Exception
            Session("error") = ex.Message.ToString
            RadWindowManager1.RadAlert(ex.Message, 300, 200, "Exception", "")
            'Dim AlertMessage As String = "alert('" & ex.Message.ToString & "')"
            'ScriptManager.RegisterStartupScript(Page, Page.GetType, "AlertMessage", AlertMessage, True)
            Return False
        End Try

    End Function

    Private Sub insert_witness()
        Try
            Dim str As New StringBuilder

            str.Append("insert into  AMS.BDC_WITNESS (ID,")
            ' "insert into AMS.BDC_WITNESS(ID,WNAME,WADDRESS,  AGR_CITY,  AGR_PROV,  AGRNO,  KEYEDON,  KEYEDBY) values(BDC_WITNESS_SEQ.NEXTVAL,"

            If txtWname.Text.Trim <> "" Then
                str.Append("WNAME, ")
            End If

            If txtWAdd.Text.Trim <> "" Then
                str.Append("WADDRESS, ")
            End If
            'If DropDownList1.SelectedValue <> "" Then
            '    str.Append("AGR_CITY, ")

            'End If
            If txtAcity.Text.Trim <> "" Then
                str.Append("AGR_CITY, ")
            End If


            If DropDownList2.SelectedValue <> "" Then
                str.Append("AGR_PROV, ")
            End If

            str.Append("AGRNO, ")

            str.Append(" KEYEDON, KEYEDBY, KEYEDIP) values( AMS.BDC_WITNESS_SEQ.NEXTVAL,")


            If txtWname.Text.Trim <> "" Then
                str.Append("'" & txtWname.Text.Trim & "',")
            End If

            If txtWAdd.Text.Trim <> "" Then
                str.Append("'" & txtWAdd.Text.Trim & "',")
            End If

            'If DropDownList1.SelectedValue <> "" Then
            '    str.Append("'" & DropDownList1.SelectedValue & "',")
            'End If
            If txtAcity.Text.Trim <> "" Then
                str.Append("'" & txtAcity.Text.Trim & "',")
            End If


            If DropDownList2.SelectedValue <> "" Then
                str.Append("'" & DropDownList2.SelectedValue & "',")
            End If
            str.Append("'" & agno & "',")


            str.Append("SYSDATE," & agntid & ",'" & HttpContext.Current.Request.UserHostAddress & "' )")

            If str.ToString().Length > 0 Then

                Dim i = clas.ExecuteNonQuery(str.ToString())
                If i = 1 Then

                End If
            End If
        Catch ex As Exception
            Session("error") = ex.Message.ToString
            RadWindowManager1.RadAlert(ex.Message, 300, 200, "Exception", "")
            'Dim AlertMessage As String = "alert('" & ex.Message.ToString & "')"
            'ScriptManager.RegisterStartupScript(Page, Page.GetType, "AlertMessage", AlertMessage, True)

        End Try
    End Sub


    Private Sub update_witness()
        Try
            Dim str As New StringBuilder

            str.Append("update AMS.BDC_WITNESS set ")

            If txtWname.Text.Trim <> "" Then
                str.Append("WNAME='" & txtWname.Text.Trim & "',")
            End If

            If txtWAdd.Text <> "" Then
                str.Append("WADDRESS='" & txtWAdd.Text.Trim & "',")
            End If

            'If DropDownList1.SelectedValue <> "" Then
            '    str.Append("AGR_CITY='" & DropDownList1.SelectedValue & "'")
            'End If
            If txtAcity.Text.Trim <> "" Then
                str.Append("AGR_CITY='" & txtAcity.Text.Trim & "',")
            End If

            If DropDownList2.SelectedValue <> "" Then
                str.Append("AGR_PROV='" & DropDownList2.SelectedValue & "'")
            End If

            str.Append(" where agrno='" & Request.QueryString("AGRNO") & "'")

            If str.ToString().Length > 0 Then

                Dim i = clas.ExecuteNonQuery(str.ToString())
                If i = 1 Then

                End If
            End If
        Catch ex As Exception
            Session("error") = ex.Message.ToString
            RadWindowManager1.RadAlert(ex.Message, 300, 200, "Exception", "")
            'Dim AlertMessage As String = "alert('" & ex.Message.ToString & "')"
            'ScriptManager.RegisterStartupScript(Page, Page.GetType, "AlertMessage", AlertMessage, True)

        End Try

    End Sub
    Private Sub clear()
        '  TextBox1.Text = String.Empty
        ddl_Report.ClearSelection()
        'DropDownList1.ClearSelection()
        'ddl_Round.DataBind()       
    End Sub


    Public Function getAgrno() As String
        Try
            Dim immid As String = clas.getMaxID("select nvl(max(agrno)+1,1000) as id from ams.bdc_lead_master where to_char(keyedon,'yyyy')=to_char(sysdate,'yyyy')")
            Dim curyear = clas.getMaxID("SELECT to_char(sysdate,'yy') id  from dual")

            Dim agrno As String = ""
            If immid.Length = "1" Then
                agrno = "000" & immid & ""
            ElseIf immid.Length = "2" Then
                agrno = "00" & immid & ""
            ElseIf immid.Length = "3" Then
                agrno = "0" & immid & ""
            Else
                agrno = "" & immid & ""

            End If
            Dim str As String = agrno
            Return str
        Catch ex As Exception
            Session("error") = ex.Message.ToString
            RadWindowManager1.RadAlert(ex.Message, 300, 200, "Exception", "")
            'Dim AlertMessage As String = "alert('" & ex.Message.ToString & "')"
            'ScriptManager.RegisterStartupScript(Page, Page.GetType, "AlertMessage", AlertMessage, True)
            Exit Function
        End Try
    End Function

    Private Sub print(candid As String, agrno As String)
        Try


            Dim path As String = ""
            Dim outpu As Boolean = False
            Dim outpu1 As Boolean = False
       


            Dim curyear = clas.getMaxID("SELECT to_char(sysdate,'yy') id  from dual")
            ' Dim str As String = 40 & curyear & agrno & ""
            Dim filename As String = 40 & curyear & agrno & ".pdf"
            Dim filename2 As String = 40 & curyear & agrno
            ' Dim filename2 As String = agrno & ".doc"
            'path = Server.MapPath("agntform") & "\temp\agent_form.doc"
            path = Server.MapPath("aggrements") & "\temp\bdc_form.doc"
            Dim FilePath_pdf As String = Server.MapPath("aggrements") & "\gen\pdf\" & filename

            ' Dim PDFPath As String = Server.MapPath("invcrecpt") & "\gen\pdf\" & formno & ".pdf"
            'Dim FilePath_pdf As String = "D:\ZAxis-IIS\CBIS\invcrecpt\gen\pdf\" & filename
            'Dim FilePath_doc As String = "D:\ZAxis-IIS\CBIS\invcrecpt\gen\doc\" & filename2
            'path = Server.MapPath("invcrecpt") &

            'If File.Exists(FilePath_pdf) Then
            '    Dim qry = "insert into AMS.BDC_PRINTEDFORM  (PID,FORMNO,EMPID,PRTDATE,PRTIP) values "
            '    qry += "(ams.BDC_PRINTEDFORM_seq.nextval, '" & agrno & "' , " & Session("lgnagntid") & ", sysdate,'" & HttpContext.Current.Request.UserHostAddress & "')"
            '    Dim i = clas.ExecuteNonQuery(qry)

            '    fileDownload(filename, FilePath_pdf)
            'Else
            If File.Exists(path) Then
                outpu1 = GenerateAsspdf(path, filename2, "R")

                'qry = "update AMS.BDC_LEAD_MASTER set AGRNO='" & agrno & "' where CANDID='" & candid & "'"
                'clas.ExecuteNonQuery(qry)

                qry = "insert into AMS.BDC_PRINTEDFORM  (PID,FORMNO,EMPID,PRTDATE,PRTIP) values "
                qry += "(ams.BDC_PRINTEDFORM_seq.nextval, '" & agrno & "' , " & Session("lgnagntid") & ", sysdate,'" & HttpContext.Current.Request.UserHostAddress & "')"
                Dim i = clas.ExecuteNonQuery(qry)


                If outpu1 = True Then
                    '  Dim FilePath_pdf2 As String = Server.MapPath("aggrements") & "\gen\pdf\"
                    fileDownload(filename, FilePath_pdf)
                End If
            End If
            ' End If


            'path = Server.MapPath("bdcform") & "\temp\agent_form.doc"
            'outpu1 = GenerateAsspdf(path, fno, "R")
            If outpu1 = True Then

                ' Dim FilePath As String = "D:\18-jul-18\CBIS\libdocs\" & filename
                'Dim btnprint As ImageButton = DirectCast(e.Item.FindControl("btnprint"), ImageButton)
                'btnprint.Visible = False
                ' Dim filepath As String = "D:\latest server 280717\CBIS\agntform\gen\pdf\" & filename
                'RadGrid2.Rebind()
                'Session("SENDFILE") = "YES"
                'Session("Filname") = filename
                'Session("Filepath") = FilePath
                ' fillgrid()
                ' RefreshPage()
                'fileDownload(filename, FilePath)

                'ScriptManager.RegisterClientScriptBlock(Me.GetType, Me.GetType, "script", "HideProgress();", True)
                ' RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Transaction Done Successfully </div></li></ul>", 300, 150, "Validation Success", Nothing)

            Else
                RadWindowManager1.RadAlert("<ul><li><div style=color:red;> System encounter some unknwon exception, please Contact IT..</div></li></ul>", 300, 150, "Validation Success", Nothing)
            End If
        Catch ex As Exception
            msg = ex.Message.ToString()
        End Try


    End Sub


    Private Function GenerateAsspdf2(ByVal docPath As String, ByVal FilePath_pdf As String, ByVal formno As String, ByVal pdftyp As String) As Boolean
        Try

            Dim PDFPath As String = Server.MapPath("invcrecpt") & "\gen\pdf\" & formno & ".pdf"
            Dim converter As DocToPDFConverter = New DocToPDFConverter()
            Dim pdfDoc As PdfDocument = converter.ConvertToPDF(docPath)
            pdfDoc.Save(FilePath_pdf)
            Return True

        Catch ex As Exception
            msg = ex.Message.ToString()
            Return False
        End Try

    End Function


    Public Function GenerateAsspdf(ByVal docPath As String, ByVal agno As String, ByVal pdftyp As String) As Boolean
        Try

            Dim bzon As Int32 = 0
            Dim bzid As Int32 = 0

            Dim document As WordDocument = New WordDocument()

            Dim frmnolABEL As String = ""

            If File.Exists(docPath) Then
                document.Open(docPath, FormatType.Doc)
            Else
                msg = "Sorry, Format not Found"
                Throw New System.Exception(msg)
            End If

            frmnolABEL = agno
            '  Dim curdat = applicantdetl.Rows(0)(1)
            '   End If
            '  Dim recptDate As String = CDate(dtpReceiptDate.SelectedDate).ToString("dd-MMM-yyyy").Trim

            document.Replace("<<ZXSYSDATE>>", Now.ToString("dd-MMM-yyyy"), True, True)
            document.Replace("<<ZXSYSDATE1>>", Now.AddYears(1).ToString("dd-MMM-yyyy"), True, True)

            '    document.Replace("<<ZxPrnDt>>", Now.ToString("dd-MMM-yyyy"), True, True)
            document.Replace("<<ZXBdcName>>", txtBname.Text.Trim, True, True)
            document.Replace("<<ZXBdcAdd>>", txtBAdd.Text.Trim, True, True)
            document.Replace("<<ZXProvenance>>", DropDownList2.SelectedItem.Text, True, True)
            document.Replace("<<ZXCity>>", txtAcity.Text.Trim, True, True)
            document.Replace("<<ZXWName>>", txtWname.Text.Trim, True, True)
            document.Replace("<<ZXWAddress>>", txtWAdd.Text.Trim, True, True)
            document.Replace("<<ZXAGRNO>>", agno, True, True)


            Dim DocumentPath As String = Server.MapPath("aggrements") & "\gen\doc\" & frmnolABEL & ".doc"
            Dim PDFPath As String = Server.MapPath("aggrements") & "\gen\pdf\" & frmnolABEL & ".pdf"
            'Dim PDFPath2 As String = Server.MapPath("libdocs") & "\" & frmnolABEL & ".pdf"
            'Dim path1 As String = "libdocs\" & frmnolABEL & ".pdf"

            If File.Exists(DocumentPath) = True Then
                File.Delete(DocumentPath)
            End If
            If File.Exists(PDFPath) = True Then
                File.Delete(PDFPath)
            End If

            document.Save(DocumentPath)
            Dim wordDoc As WordDocument = New WordDocument(DocumentPath)

            Dim converter As DocToPDFConverter = New DocToPDFConverter()
            Dim pdfDoc As PdfDocument = converter.ConvertToPDF(wordDoc)
            pdfDoc.Save(PDFPath)

            

            'File.Copy(PDFPath, PDFPath2)

            document.Close()
            wordDoc.Close()
            '   pdfDoc.Close()


            Return True
        Catch ex As Exception
            msg = ex.Message.ToString()
            RadWindowManager1.RadAlert(msg, 300, 150, "Validation Success", Nothing)
            Return False
        End Try

    End Function


#Region "File Download"
    Private Sub fileDownload(ByVal fileName As String, ByVal fileUrl As String)
        Try
            Page.Response.Clear()
            Dim success As Boolean = ResponseFile(Page.Request, Page.Response, fileName, fileUrl, 1024000)

            If Not success Then
                Page.Response.[End]()
                RadWindowManager1.RadAlert("<div style=color:Red;> System unable to download Agent Personal Information Sheet </div>", 320, 150, "Information", Nothing)
            End If


            Page.Response.[End]()
        Catch ex As Exception
            msg = ex.Message.ToString()
        End Try

    End Sub

    Public Shared Function ResponseFile(ByVal _Request As HttpRequest, ByVal _Response As HttpResponse, ByVal _fileName As String, ByVal _fullPath As String, ByVal _speed As Long) As Boolean
        Try
            Dim myFile As New FileStream(_fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
            Dim br As New BinaryReader(myFile)
            Try
                _Response.AddHeader("Accept-Ranges", "bytes")
                _Response.Buffer = False
                Dim fileLength As Long = myFile.Length
                Dim startBytes As Long = 0

                Dim pack As Integer = 10240
                '10K bytes
                Dim sleep As Integer = CInt(Math.Floor(CDbl(1000 * pack \ _speed))) + 1
                If _Request.Headers("Range") IsNot Nothing Then
                    _Response.StatusCode = 206
                    Dim range As String() = _Request.Headers("Range").Split(New Char() {"="c, "-"c})
                    startBytes = Convert.ToInt64(range(1))
                End If
                _Response.AddHeader("Content-Length", (fileLength - startBytes).ToString())
                If startBytes <> 0 Then
                    _Response.AddHeader("Content-Range", String.Format(" bytes {0}-{1}/{2}", startBytes, fileLength - 1, fileLength))
                End If
                _Response.AddHeader("Connection", "Keep-Alive")
                _Response.ContentType = "application/octet-stream"
                _Response.AddHeader("Content-Disposition", "attachment;filename=" & HttpUtility.UrlEncode(_fileName, System.Text.Encoding.UTF8))

                br.BaseStream.Seek(startBytes, SeekOrigin.Begin)
                Dim maxCount As Integer = CInt(Math.Floor(CDbl((fileLength - startBytes) \ pack))) + 1

                For i As Integer = 0 To maxCount - 1
                    If _Response.IsClientConnected Then
                        _Response.BinaryWrite(br.ReadBytes(pack))
                        Thread.Sleep(sleep)
                    Else
                        i = maxCount
                    End If
                Next
            Catch ex As Exception
                '  ex.Message.ToString()
                Return False
            Finally
                br.Close()
                myFile.Close()
            End Try
        Catch ex As Exception
            '  ex.Message.ToString()
            Return False
        End Try
        Return True
    End Function
#End Region

End Class
