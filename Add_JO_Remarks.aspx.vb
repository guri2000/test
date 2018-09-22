Imports System.Data

Imports System.IO
Imports Telerik.Web.UI
Imports System.Text

Imports System.Environment
Imports System.Net


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

        Button1.Text = "Update"
        Session("mode") = "edit"

        If Request.QueryString("mode") = "edit" And Session("mode") = "" Then
            'getrcdforedit()
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

    'Private Sub getrcdforedit()
    '    qry = "select * from ams.ams_indmaster where indid='" & Request.QueryString("empid") & "'"
    '    dt = getdata(qry, "QR")
    '    If dt.Rows.Count > 0 Then
    '        TextBox1.Text = Convert.ToString(dt.Rows(0)("indname"))
    '        TextBox10.Text = Convert.ToString(dt.Rows(0)("inddesc"))
    '        DropDownList2.Items.FindByValue(Convert.ToString(dt.Rows(0)("indsts"))).Selected = True
    '        DropDownList2.DataBind()
    '        Button1.Text = "Update"
    '        Session("mode") = "edit"
    '    End If

    'End Sub



    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click, ImageButton1.Click
        Dim flag As Boolean
        If Button1.Text = "Update" Then
            If TextBox10.Text <> "" Then
                flag = updateCommand()
            End If

            'Else
            '    flag = InsertCommand()
        End If

        If flag = True Then
            clear()
            RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Remarks Saved Successfully </div></li></ul>", 300, 150, "Validation Success", Nothing)
        Else
            Session.Remove("mode")
            RadWindowManager1.RadAlert("<ul><li><div style=color:green;>" & Session("error") & " </div></li></ul>", 300, 150, "Validation Error", Nothing)


        End If

    End Sub
    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        clear()
    End Sub

    'Public Function InsertCommand() As Boolean
    '    Dim transid As Int32 = getMaxID()
    '    'Dim str2 As String = "insert into tt.ukdata(transid, InDocRecdDt,InDocRecdBy, InDocStat) values(2, to_date(@InDocRecdDt,'dd-MON-yyyy'), @InDocRecdBy, @InDocStat)"
    '    Dim str As StringBuilder = New StringBuilder
    '    str.Append("insert into ams.ams_indmaster(indid,")


    '    If TextBox1.Text <> "" Then
    '        str.Append("indname,")
    '    End If

    '    If TextBox10.Text <> "" Then
    '        str.Append("inddesc,")
    '    End If

    '    If DropDownList2.SelectedValue <> "" Then
    '        str.Append("indsts,")
    '    End If
    '    Dim temp As String = str.ToString
    '    str.Clear()
    '    str.Append(temp.Substring(0, temp.Length - 1))
    '    str.Append(") values('" & transid & "',")

    '    If TextBox1.Text <> "" Then
    '        str.Append("'" & TextBox1.Text & "',")
    '    End If

    '    If TextBox10.Text <> "" Then
    '        str.Append("'" & TextBox10.Text & "',")
    '    End If

    '    If DropDownList2.SelectedValue <> "" Then
    '        str.Append("'" & DropDownList2.SelectedValue & "',")
    '    End If




    '    temp = str.ToString
    '    str.Clear()
    '    str.Append(temp.Substring(0, temp.Length - 1))
    '    str.Append(")")
    '    'Try

    '    Dim con As New OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings("constr").ToString)
    '    Dim cmd As New OracleCommand(str.ToString, con)
    '    If con.State = ConnectionState.Closed Then
    '        con.Open()
    '    End If

    '    cmd.ExecuteNonQuery()
    '    con.Close()
    '    Return True
    '    'Catch ex As Exception
    '    '    Session("error") = ex.Message
    '    '    RadWindowManager1.RadAlert("<ul><li><div style=color:Red;>" & ex.Message & "</div></li></ul>", 300, 150, "Validation Error", Nothing)
    '    '    Return False
    '    '    Exit Function
    '    'End Try

    'End Function

    Public Function updateCommand() As Boolean


        Dim ipaddress As String = Request.UserHostAddress

        'Dim transid As Int32 = getMaxID()
        'Dim str2 As String = "insert into tt.ukdata(transid, InDocRecdDt,InDocRecdBy, InDocStat) values(2, to_date(@InDocRecdDt,'dd-MON-yyyy'), @InDocRecdBy, @InDocStat)"
        Dim str As StringBuilder = New StringBuilder
        str.Append("update ams.ams_JOBSECURED set ")

        If TextBox10.Text <> "" Then
            str.Append("REMARKS= REMARKS || ' ' || '<ns>" & Date.Now.ToString("dd-MMM-yyyy") & " : " & Session("lgnagntnam") & " - " & TextBox10.Text & "',")
        End If
        str.Append(" KEYEDDATE=sysdate,KEYEDBY='" & agntid & "',KEYEDIP='" & ipaddress & "' where jobsecuredid='" & Request.QueryString("JobId") & "'")
       




        Dim i = clas.ExecuteNonQuery(str.ToString())
        If i = 1 Then

            Return True

        Else

            Dim Err_Msg As String = clas.getexception().Replace(vbCrLf, "")
            Err_Msg = Err_Msg.Replace(vbCr, "")
            Err_Msg = Err_Msg.Replace(vbLf, "")
            RadWindowManager1.RadAlert("<ul><li><div style=color:red;>System found some exception:" & Err_Msg & "</div></li></ul>", 300, 100, "Validation Exception Failure", Nothing)
            Return False
            '  RadWindowManager1.RadAlert("<ul><li><div style=color:red;> Transaction not Done </div></li></ul>", 300, 150, "Validation Success", Nothing)
        End If

      

    End Function

    Private Sub clear()
        TextBox10.Text = String.Empty
        Button1.Text = "Submit"
        Session.Remove("mode")
    End Sub


End Class
