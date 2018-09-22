Imports System.Data
Imports System.IO
Imports Telerik.Web.UI
Imports System.Text
Partial Class _Default
    Inherits System.Web.UI.Page
    Dim clas As rahulcpm.commonclass = New rahulcpm.commonclass()
    Dim qry As String
    Dim dt As DataTable
    Dim sydate As String
    Dim agntid As Int32
    Dim msg As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
        If Session("lgnagntid") = "0" Or Session("lgnagntid") = "" Then
            'Response.Redirect("login.aspx")
            ImageButton1.Enabled = False
            Button2.Enabled = False
            RadWindowManager1.RadAlert("session is expired, Please login Again", 300, 200, "Session Expired Error", "")
            Response.Redirect("logout.aspx")
        Else
            agntid = Session("lgnagntid")
        End If

      
        If Request.QueryString("mode") = "C" Then
            TextBox2.Enabled = False
            RequiredFieldValidator2.Enabled = False
            sct.Visible = False
        ElseIf Request.QueryString("mode") = "S" Then
            TextBox1.Enabled = False
            sct.Visible = True
        Else
            TextBox2.Enabled = False
            RequiredFieldValidator2.Enabled = False
            sct.Visible = False
        End If

        If Not Request.QueryString("catid") Is Nothing Then
            getrcdforedit(Request.QueryString("catid"))
        End If

    End Sub

    'Private Sub getmastres()
    '    qry = "select * from ams.ams_statmaster where statstatus='A' and statscp=1"
    '    dt = getdata(qry, "QR")
    '    If dt.Rows.Count > 0 Then
    '        DropDownList4.DataTextField = "statdescr"
    '        DropDownList4.DataValueField = "statid"
    '        DropDownList4.DataSource = dt
    '        DropDownList4.DataBind()
    '    End If

    'End Sub
    Private Sub getrcdforedit(ByVal ctid As String)
        Try
            qry = "select * from ams.ams_catmaster where CATID='" & ctid & "'"
            dt = clas.getdata(qry, "QR")
            If dt.Rows.Count > 0 Then
                TextBox1.Text = Convert.ToString(dt.Rows(0)("CATNAM"))

            Else
                ImageButton1.Enabled = False
                RadWindowManager1.RadAlert("Category not Exist", 300, 200, "Exception", "")
            End If
            TextBox1.Enabled = False
            ' Throw New Exception("UnKnown Exception occured , Please Contact IT Department")
        Catch ex As Exception
            RadWindowManager1.RadAlert(ex.Message, 300, 200, "Exception", "")

        End Try
    End Sub

    Private Function Check_DocStatus() As Boolean
        Dim query As String = ""

        If Button1.Text = "Update" Then

            If Request.QueryString("mode") = "S" Then
                query = "Select * from AMS.ams_subcat where LOWER(SUBCATNAM)='" & LCase(TextBox2.Text.Trim) & "' and SUBCATID<>'" & Request.QueryString("subcatid") & "'"

            Else
                query = "Select * from AMS.ams_catmaster where LOWER(CATNAM)='" & LCase(TextBox1.Text.Trim) & "' and catid<>'" & Request.QueryString("catid") & "'"
            End If


        Else
              If Request.QueryString("mode") = "S" Then
                query = "Select * from AMS.ams_subcat where LOWER(SUBCATNAM)='" & LCase(TextBox2.Text.Trim) & "' and catid='" & Request.QueryString("catid") & "'"
            Else
                query = "Select * from AMS.ams_catmaster where LOWER(CATNAM)='" & LCase(TextBox1.Text.Trim) & "'"
            End If

        End If

        Dim _DataTable As DataTable
        _DataTable = clas.getdata(query, "QR")
        If _DataTable.Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click, ImageButton1.Click

        Dim Check_Disposition As Boolean
        Check_Disposition = Check_DocStatus()
        If Check_Disposition = False Then
            If Request.QueryString("mode") = "S" Then
                RadWindowManager1.RadAlert("<ul><li><div style=color:green;> This Sub-Category is already in existance. </div></li></ul>", 300, 150, "Validation Success", Nothing)
            Else
                RadWindowManager1.RadAlert("<ul><li><div style=color:green;> This Category is already in existance. </div></li></ul>", 300, 150, "Validation Success", Nothing)
            End If

            Exit Sub
        End If

        Dim flag As Boolean
        If Button1.Text = "Update" Then
            '   flag = updateCommand()
        Else
           
            flag = InsertCommand()


        End If

        If flag = True Then
            If Request.QueryString("mode") = "S" Then
                clear1()
            Else
                clear()
            End If
            If Button1.Text = "Update" Then
                Button1.Text = "Submit"
                RadWindowManager1.RadAlert("<ul><li><div style=color:green;>  information updated successfully. </div></li></ul>", 300, 150, "Validation Success", Nothing)
            Else
                Button1.Text = "Submit"
                RadWindowManager1.RadAlert("<ul><li><div style=color:green;> information saved successfully. </div></li></ul>", 300, 150, "Validation Success", Nothing)
            End If
           
        Else
            Session.Remove("mode")
            RadWindowManager1.RadAlert("<ul><li><div style=color:green;>" & Session("error") & " </div></li></ul>", 300, 150, "Validation Error", Nothing)


        End If

    End Sub
    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        clear()
        Button1.Text = "Submit"
    End Sub
    Public Function InsertCommand() As Boolean

        Dim str As StringBuilder = New StringBuilder
        If Request.QueryString("mode") = "S" Then
            str.Append("insert into ams.ams_subcat(SUBCATID,CATID,")


            If TextBox2.Text <> "" Then
                str.Append("SUBCATNAM,")
            End If
            If DropDownList2.SelectedValue <> "" Then
                str.Append("SUBCATSTS,")
            End If
            Dim temp As String = str.ToString
            str.Clear()
            str.Append(temp.Substring(0, temp.Length - 1))
            str.Append(") values(AMS.AMS_SUBCAT_SEQ.nextval,'" & Request.QueryString("catid") & "',")

            If TextBox2.Text <> "" Then
                str.Append("'" & TextBox2.Text & "',")
            End If

            If DropDownList2.SelectedValue <> "" Then
                str.Append("'" & DropDownList2.SelectedValue & "',")
            End If
            temp = str.ToString
            str.Clear()
            str.Append(temp.Substring(0, temp.Length - 1))
            str.Append(")")
        Else

            str.Append("insert into ams.ams_catmaster(catid,")


            If TextBox1.Text <> "" Then
                str.Append("catnam,")
            End If
            If DropDownList2.SelectedValue <> "" Then
                str.Append("catsts,")
            End If
            Dim temp As String = str.ToString
            str.Clear()
            str.Append(temp.Substring(0, temp.Length - 1))
            str.Append(") values(AMS.AMS_CAT_SEQ.nextval,")

            If TextBox1.Text <> "" Then
                str.Append("'" & TextBox1.Text & "',")
            End If

            If DropDownList2.SelectedValue <> "" Then
                str.Append("'" & DropDownList2.SelectedValue & "',")
            End If
            temp = str.ToString
            str.Clear()
            str.Append(temp.Substring(0, temp.Length - 1))
            str.Append(")")
        End If


        Dim i = clas.ExecuteNonQuery(str.ToString())
        If i = 1 Then

            Return True

        Else



            Dim Err_Msg As String = clas.getexception().Replace(vbCrLf, "")
            Err_Msg = Err_Msg.Replace(vbCr, "")
            Err_Msg = Err_Msg.Replace(vbLf, "")
            RadWindowManager1.RadAlert("<ul><li><div style=color:red;>System found some exception:" & Err_Msg & "</div></li></ul>", 300, 100, "Validation Exception Failure", Nothing)
            Return False

        End If
        

    End Function

    'Public Function updateCommand() As Boolean
    '    Try
    '        'Dim transid As Int32 = getMaxID()
    '        'Dim str2 As String = "insert into tt.ukdata(transid, InDocRecdDt,InDocRecdBy, InDocStat) values(2, to_date(@InDocRecdDt,'dd-MON-yyyy'), @InDocRecdBy, @InDocStat)"
    '        Dim str As StringBuilder = New StringBuilder
    '        str.Append("update ams.ams_docmaster set ")


    '        If TextBox1.Text <> "" Then
    '            str.Append("doc_description='" & TextBox1.Text & "',")
    '        End If

    '        If DropDownList2.SelectedValue <> "" Then
    '            str.Append("doc_status='" & DropDownList2.SelectedValue & "',")
    '        End If
    '        Dim temp As String = str.ToString
    '        str.Clear()
    '        str.Append(temp.Substring(0, temp.Length - 1))
    '        str.Append(" where doc_id='" & Request.QueryString("empid") & "'")

    '          Dim i = clas.ExecuteNonQuery(str.ToString())
    '        If i = 1 Then

    '            Return True

    '        Else

    '            Dim Err_Msg As String = clas.getexception().Replace(vbCrLf, "")
    '            Err_Msg = Err_Msg.Replace(vbCr, "")
    '            Err_Msg = Err_Msg.Replace(vbLf, "")
    '            RadWindowManager1.RadAlert("<ul><li><div style=color:red;>System found some exception:" & Err_Msg & "</div></li></ul>", 300, 100, "Validation Exception Failure", Nothing)
    '            Return False

    '        End If
    '        Throw New Exception("UnKnown Exception occured , Please Contact IT Department")
    '    Catch ex As Exception
    '        RadWindowManager1.RadAlert(ex.Message, 300, 200, "Exception", "")
    '        Return False
    '    End Try

    'End Function

  

    Private Sub clear()
        TextBox1.Text = String.Empty
        TextBox2.Text = String.Empty
        DropDownList2.ClearSelection()
        DropDownList2.DataBind()
       
        'Button1.Text = "Submit"
        Session.Remove("mode")
        Dim nvc = HttpUtility.ParseQueryString(Request.Url.Query)
        nvc.Remove("mode")
        Dim url As String = Request.Url.AbsolutePath + "?" + nvc.ToString()
    End Sub
    Private Sub clear1()
        TextBox2.Text = String.Empty
        DropDownList2.ClearSelection()
        DropDownList2.DataBind()

        'Button1.Text = "Submit"
        Session.Remove("mode")
        Dim nvc = HttpUtility.ParseQueryString(Request.Url.Query)
        nvc.Remove("mode")
        Dim url As String = Request.Url.AbsolutePath + "?" + nvc.ToString()
    End Sub
  
End Class
