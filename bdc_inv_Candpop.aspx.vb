Imports System.Data
Imports System.IO
Imports Telerik.Web.UI
Imports System.Text
Imports System.Environment

Partial Class _Default
    Inherits System.Web.UI.Page
    Dim clas As rahulcpm.commonclass = New rahulcpm.commonclass()

    Dim qry As String
    Dim dt As DataTable
    Dim sydate As String
    Dim agntid As Int32
    Dim msg As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            'If Request.QueryString("frm") IsNot Nothing And Not Request.QueryString("frm") = "" Then
            '    Session("mode") = ""
            'End If
            Session("mode1") = ""
            fillPF()
            fillAdv()

            binddates(dt_resdate, "", Date.Today.ToString("dd-MMM-yyyy"), Date.Today.AddMonths(1).ToString("dd-MMM-yyyy"), True)
        Else

        End If

        If Session("lgnagntid") = "0" Or Session("lgnagntid") = "" Then
            'Response.Redirect("login.aspx")
            ImageButton1.Enabled = False
            Button2.Enabled = False
            RadWindowManager1.RadAlert("session is expired, Please login Again", 300, 200, "Session Expired Error", "")
            Response.Redirect("logout.aspx")
        Else
            agntid = Session("lgnagntid")
        End If
        If Convert.ToString(Request.QueryString("pid")) <> "" Then
            If IsDBNull(Request.QueryString("pid")) = False Then
                ddl_PF.Items.FindByValue(Convert.ToString(Request.QueryString("pid"))).Selected = True
                ddl_PF.Enabled = False
            End If
        End If
        If Request.QueryString("mode") = "edit" And Session("mode1") = "" Then
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
    Protected Sub binddates(ByVal cntl As Control, ByVal seldate As String, ByVal mindate As String, ByVal maxdate As String, ByVal enbl As Boolean)

        Dim cnrl1 As RadDatePicker = TryCast(cntl, RadDatePicker)
        If Not seldate.Trim = "" Then
            cnrl1.SelectedDate = seldate
        Else
            '  cnrl1.SelectedDate = Date.Today.ToString("dd-MMM-yyyy")
        End If
        If Not mindate.Trim = "" Then
            cnrl1.MinDate = mindate
        Else
            cnrl1.MinDate = Date.Today.ToString("dd-MMM-yyyy")
        End If
        If Not maxdate.Trim = "" Then
            cnrl1.MaxDate = maxdate
        Else
            cnrl1.MaxDate = Date.Today.ToString("dd-MMM-yyyy")
        End If
        cnrl1.Enabled = enbl
    End Sub

    Private Sub getrcdforedit()
        Try
            qry = "select * from AMS.BDC_LEAD_MASTER where CANDID='" & Request.QueryString("CANDID") & "'"
            dt = clas.getdata(qry, "QR")

            If dt.Rows.Count > 0 Then
                Button1.Text = "Update"
                ' ctr_txtAdvT.Text = Convert.ToString(dt.Rows(0)("ADVTXT"))
                ddl_adv.Enabled = False
                TextBox1.Text = Convert.ToString(dt.Rows(0)("CANDNAME"))
                TextBox2.Text = Convert.ToString(dt.Rows(0)("CANDPHN"))
                TextBox3.Text = Convert.ToString(dt.Rows(0)("CANDEML"))
                txtAdd.Text = Convert.ToString(dt.Rows(0)("CANDADDRESS"))
                TextBox1.Enabled = False

                If IsDBNull(dt.Rows(0)("RESMRECDDAT")) = False Then
                    dt_resdate.MinDate = Date.Now.AddYears(-5)
                    dt_resdate.MaxDate = Date.Now.AddYears(5)
                    dt_resdate.SelectedDate = dt.Rows(0)("RESMRECDDAT")
                    dt_resdate.Enabled = False
                End If
                If IsDBNull(dt.Rows(0)("PLTFORMID")) = False Then
                    ddl_PF.Items.FindByValue(Convert.ToString(dt.Rows(0)("PLTFORMID"))).Selected = True
                End If
                lbl_Fname.Visible = True
                lbl_Fname.Text = Convert.ToString(dt.Rows(0)("resumepath"))
                fupl.Attributes.CssStyle.Add("width", "100px;")

                Session("mode1") = "edit"
            Else
                Button1.Enabled = False

                ImageButton1.Enabled = False
                ImageButton1.ToolTip = " Invalid Record ID for Edit"
            End If
        Catch ex As Exception
            Session("error") = ex.Message.ToString
            Session("mode") = "edit"
            RadWindowManager1.RadAlert(ex.Message, 300, 200, "Exception", "")
            'Dim AlertMessage As String = "alert('" & ex.Message.ToString & "')"
            'ScriptManager.RegisterStartupScript(Page, Page.GetType, "AlertMessage", AlertMessage, True)
        End Try

    End Sub
   
    Private Sub fillPF()
        qry = " SELECT STATID, STATDESCR FROM AMS.AMS_STATMASTER where statstatus='A' and statscp=100 "

        dt = clas.getdata(qry, "QR")
        ddl_PF.DataTextField = "STATDESCR"
        ddl_PF.DataValueField = "STATID"
        ddl_PF.DataSource = dt
        ddl_PF.DataBind()
    End Sub
    Private Sub fillAdv()

        Try
            If Request.QueryString("aid") <> "" Or Request.QueryString("aid") <> Nothing Then
                qry = "SELECT ADVID, SUBSTR(advdesc,1,50) as advdesc,(select vacname from ams.adv_vacancy where vacid=vac_id) as Vac FROM AMS.ADV_MASTER  where advid='" & Request.QueryString("aid") & "'"
            ElseIf Request.QueryString("vacid") <> "" Or Request.QueryString("vacid") <> Nothing Then
                qry = "SELECT ADVID, SUBSTR(advdesc,1,50) as advdesc,(select vacname from ams.adv_vacancy where vacid=vac_id) as Vac FROM AMS.ADV_MASTER where vac_id='" & Request.QueryString("vacid") & "'"
            Else
                qry = "SELECT ADVID, SUBSTR(advdesc,1,50) as advdesc,(select vacname from ams.adv_vacancy where vacid=vac_id) as Vac FROM AMS.ADV_MASTER where advid='" & Session("advid") & "'"
            End If

            dt = clas.getdata(qry, "QR")
            If dt.Rows.Count > 0 Then

                lblVac.Text = "Vacancy :" + Convert.ToString(dt.Rows(0)("Vac"))
                'lblVac.Style.Add("margin-top", "5px")

                ddl_adv.DataTextField = "advdesc"
                ddl_adv.DataValueField = "ADVID"
                ddl_adv.DataSource = dt

                ddl_adv.DataBind()
                ' ddl_adv.Enabled = False
            End If
        Catch ex As Exception
            Session("error") = ex.Message.ToString
            RadWindowManager1.RadAlert(ex.Message, 300, 200, "Exception", "")

        End Try
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click, ImageButton1.Click
        Try
            Dim fileName As String
            Dim flag As Boolean


            If Button1.Text = "Update" Then

                flag = updateCommand()

                If flag = True Then
                    RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Candidate Information Updated Successfully </div></li></ul>", 300, 150, "Validation Success", Nothing)
                End If
            Else
                Dim str As StringBuilder = New StringBuilder

                If fupl.HasFile = True Then


                    fileName = Path.GetFileName(fupl.PostedFile.FileName).ToLower()

                    If fileName = "" Then
                        RadWindowManager1.RadAlert("<div style=color:green;> Please browse your required file. </div>", 300, 150, "Validation Success", Nothing)
                        Exit Sub
                    Else

                        If fileName.Contains("%") Or fileName.Contains("'") Or fileName.Contains("""") Or fileName.Contains("&") Then
                            RadWindowManager1.RadAlert("<div style=color:red;> Invalid File Name. </div>", 300, 150, "Validation Success", Nothing)
                            Exit Sub
                        End If

                        fileName = Replace(fileName.Replace("""", "").Replace("%", "").Replace("&", "").Replace("+", ""), "'", "")

                        Dim actfileName As String = fileName

                        Dim ext As String = Path.GetExtension(fupl.PostedFile.FileName)
                        '  Dim ext As String = Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower()
                        Dim fileSize As Long = fupl.PostedFile.ContentLength
                        If fileSize < 3145728 Then
                            If ext.ToLower.Contains(".pdf") Or ext.ToLower.Contains(".doc") Then
                                fileName = fileName.Replace(" ", "_")

                                Dim FileNM1 As String() = fileName.Split(".")
                                Dim datestr As String = String.Format("{0:ddMMMyyyyHmmss}", DateTime.Now)
                                fileName = FileNM1(0) & datestr & ext

                                Dim Path As String = (Server.MapPath("BDC_Resume/") + fileName)
                                fupl.PostedFile.SaveAs(Path)

                                Dim client_ip As String = Request.UserHostAddress()
                                Dim Browser_Name As String = Request.Browser.Browser.ToString & " " & Request.Browser.Version.ToString

                            Else
                                RadWindowManager1.RadAlert("<div style=color:Red;> Please upload Resume in doc/pdf format. </div>", 300, 150, "Validation Error", Nothing)
                                Exit Sub
                            End If

                        Else
                            RadWindowManager1.RadAlert("<div style=color:Red;> Document should not contain file size more than 3 MB. </div>", 300, 150, "Validation Error", Nothing)
                            Exit Sub
                        End If
                    End If


                Else
                    RadWindowManager1.RadAlert("<div style=color:Red;> Please Choose File..... </div>", 300, 150, "Validation Error", Nothing)
                    Exit Sub
                End If
                str.Append("insert into  AMS.BDC_LEAD_MASTER (CANDID,")
                If ddl_adv.SelectedValue <> "" Then
                    str.Append("ADVID,")
                End If

                If TextBox1.Text <> "" Then
                    str.Append("CANDNAME, ")
                End If

                If TextBox2.Text <> "" Then
                    str.Append("CANDPHN, ")
                End If

                If TextBox3.Text <> "" Then
                    str.Append("CANDEML, ")
                End If

                If Not dt_resdate.SelectedDate Is Nothing Then
                    If (dt_resdate.SelectedDate.ToString.Length >= 1) Then

                        str.Append("RESMRECDDAT,")
                        '  str.Append("'" & CDate(ctr_postDt.SelectedDate).ToString("dd-MMM-yyyy") & "',")
                    End If
                End If

                If fupl.HasFile Then
                    str.Append("RESUMEPATH,")
                End If

                If ddl_PF.SelectedValue <> "" Then
                    str.Append("PLTFormID,")
                End If
                If txtAdd.Text <> "" Then
                    str.Append("CANDADDRESS, ")
                End If
                str.Append(" KEYEDON, KEYEDBY, KEYEDIP) values( AMS.BDC_LEAD_MASTER_seq.nextval,")


                If ddl_adv.SelectedValue <> "" Then
                    str.Append("'" & ddl_adv.SelectedValue & "',")
                End If

                If TextBox1.Text <> "" Then
                    str.Append("'" & StrConv(TextBox1.Text.Trim, VbStrConv.ProperCase) & "',")
                End If

                If TextBox2.Text <> "" Then
                    str.Append("'" & TextBox2.Text.Trim & "',")
                End If

                If TextBox3.Text <> "" Then
                    str.Append("'" & TextBox3.Text.Trim & "',")
                End If

                If Not dt_resdate.SelectedDate Is Nothing Then
                    If (dt_resdate.SelectedDate.ToString.Length >= 1) Then

                        str.Append("'" & CDate(dt_resdate.SelectedDate).ToString("dd-MMM-yyyy") & "',")
                    End If
                End If

                If fupl.HasFile Then
                    str.Append("'" & fileName.Trim & "',")
                End If

                If ddl_PF.SelectedValue <> "" Then
                    str.Append("'" & ddl_PF.SelectedValue & "',")
                End If
                If txtAdd.Text <> "" Then
                    str.Append("'" & txtAdd.Text.Trim & "',")
                End If

                str.Append("sysdate," & agntid & ",'" & HttpContext.Current.Request.UserHostAddress & "' )")



                If str.ToString().Length > 0 Then

                    Dim i = clas.ExecuteNonQuery(str.ToString())
                    If i = 1 Then

                        RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Transaction Successfull </div></li></ul>", 300, 150, "Validation Success", Nothing)
                        clear()
                        Exit Sub
                    Else

                        Dim Err_Msg As String = clas.getexception().Replace(vbCrLf, "")
                        Err_Msg = Err_Msg.Replace(vbCr, "")
                        Err_Msg = Err_Msg.Replace(vbLf, "")
                        RadWindowManager1.RadAlert("<ul><li><div style=color:red;>System found some exception:" & Err_Msg & "</div></li></ul>", 300, 100, "Validation Exception Failure", Nothing)

                        '  RadWindowManager1.RadAlert("<ul><li><div style=color:red;> Transaction not Done </div></li></ul>", 300, 150, "Validation Success", Nothing)
                    End If
                    ' Response.Write(qry)

                Else
                    RadWindowManager1.RadAlert("<ul><li><div style=color:red;>Some Malware Function issue</div></li></ul>", 300, 100, "Validation Exception Failure", Nothing)
                End If


                Exit Sub
            End If

        Catch ex As Exception
            Session("error") = ex.Message.ToString
            RadWindowManager1.RadAlert(ex.Message, 300, 200, "Exception", "")

        End Try


    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        clear()
        ' getmastdesg()
        ' getmastindustry()
    End Sub



    Public Function updateCommand() As Boolean
        Try
            Dim fileName As String
            'Dim transid As Int32 = getMaxID()
            'Dim str2 As String = "insert into tt.ukdata(transid, InDocRecdDt,InDocRecdBy, InDocStat) values(2, to_date(@InDocRecdDt,'dd-MON-yyyy'), @InDocRecdBy, @InDocStat)"
            Dim str As StringBuilder = New StringBuilder
            str.Append("update  AMS.BDC_LEAD_MASTER set ")

            If ddl_PF.SelectedValue <> "" Then
                str.Append("PLTFORMID='" & ddl_PF.SelectedValue & "',")
            End If


            If Not dt_resdate.SelectedDate Is Nothing Then
                If (dt_resdate.SelectedDate.ToString.Length >= 1) Then
                    str.Append("RESMRECDDAT='" & CDate(dt_resdate.SelectedDate).ToString("dd-MMM-yyyy") & "',")
                End If
            End If
            'If ctr_pstBy.SelectedValue <> "" Then
            '    str.Append("POSTBY='" & ctr_pstBy.SelectedValue & "',")
            'End If
            'If ctr_pstStatus.SelectedValue <> "" Then
            '    str.Append("POSTSTS='" & ctr_pstStatus.SelectedValue & "',")
            'End If


            If TextBox1.Text <> "" Then
                str.Append("CANDNAME='" & TextBox1.Text.Trim & "',")

            End If

            If TextBox2.Text <> "" Then
                str.Append("CANDPHN='" & TextBox2.Text.Trim & "',")
            End If

            If TextBox3.Text <> "" Then
                str.Append("CANDEML='" & TextBox3.Text.Trim & "',")
            End If

            If txtAdd.Text <> "" Then
                str.Append("CANDADDRESS='" & txtAdd.Text.Trim & "',")
            End If

            If fupl.HasFile = True Then

                fileName = Path.GetFileName(fupl.PostedFile.FileName).ToLower()


                If fileName = "" Then
                    'RadWindowManager1.RadAlert("<div style=color:green;> Please browse your required file. </div>", 300, 150, "Validation Success", Nothing)
                    'Exit Sub

                Else

                    If fileName.Contains("%") Or fileName.Contains("'") Or fileName.Contains("""") Or fileName.Contains("&") Then
                        RadWindowManager1.RadAlert("<div style=color:red;> Invalid File Name. </div>", 300, 150, "Validation Success", Nothing)
                        Exit Function
                    End If

                    fileName = Replace(fileName.Replace("""", "").Replace("%", "").Replace("&", "").Replace("+", ""), "'", "")

                    Dim actfileName As String = fileName

                    Dim ext As String = Path.GetExtension(fupl.PostedFile.FileName)
                    '  Dim ext As String = Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower()
                    Dim fileSize As Long = fupl.PostedFile.ContentLength
                    If fileSize < 3145728 Then
                        If ext.ToLower.Contains(".PDF") Or ext.ToLower.Contains(".DOC") Then
                            fileName = fileName.Replace(" ", "_")

                            Dim FileNM1 As String() = fileName.Split(".")
                            Dim datestr As String = String.Format("{0:ddMMMyyyyHmmss}", DateTime.Now)
                            fileName = FileNM1(0) & datestr & ext

                            Dim Path As String = (Server.MapPath("BDC_Resume/") + fileName)
                            fupl.PostedFile.SaveAs(Path)

                            Dim client_ip As String = Request.UserHostAddress()
                            Dim Browser_Name As String = Request.Browser.Browser.ToString & " " & Request.Browser.Version.ToString

                        Else
                            RadWindowManager1.RadAlert("<div style=color:Red;> Please upload Advertiement in doc/pdf format. </div>", 300, 150, "Validation Error", Nothing)
                            Exit Function
                        End If

                    Else
                        RadWindowManager1.RadAlert("<div style=color:Red;> Document should not contain file size more than 3 MB. </div>", 300, 150, "Validation Error", Nothing)
                        Exit Function
                    End If
                End If


            Else
                'RadWindowManager1.RadAlert("<div style=color:Red;> Please Choose File..... </div>", 300, 150, "Validation Error", Nothing)
                'Exit Sub
            End If

            If fupl.HasFile Then
                str.Append("RESUMEPATH='" & fileName & "',")
            End If

            Dim temp As String = str.ToString
            str.Clear()
            str.Append(temp.Substring(0, temp.Length - 1))
            str.Append(",LASTUPDON=sysdate,LASTUPDBY='" & agntid & "' where CANDID='" & Request.QueryString("CANDID") & "'")

            If str.ToString().Length > 0 Then

                Dim i = clas.ExecuteNonQuery(str.ToString())
                If i = 1 Then
                    Return True
                End If
            End If


        Catch ex As Exception
            Session("error") = ex.Message.ToString
            RadWindowManager1.RadAlert(ex.Message, 300, 200, "Exception", "")
            'Dim AlertMessage As String = "alert('" & ex.Message.ToString & "')"
            'ScriptManager.RegisterStartupScript(Page, Page.GetType, "AlertMessage", AlertMessage, True)
            Return False
        End Try

    End Function

    Private Sub clear()
        TextBox1.Text = String.Empty
        TextBox2.Text = String.Empty
        TextBox3.Text = String.Empty
        
        ddl_PF.ClearSelection()
        ddl_PF.DataBind()
        dt_resdate.Clear()
        txtAdd.Text = String.Empty
    End Sub
End Class
