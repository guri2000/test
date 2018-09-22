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
        If Page.IsPostBack = False Then

            'getmastres()
            'getmastdesg()
            'getmastindustry()
            'getminmax()
            'label2.Text = Session("lgnagntnam")
        Else

        End If
        If Session("lgnagntid") = "0" Or Session("lgnagntid") = "" Then
            ' Response.Redirect("login.aspx")
            ImageButton1.Enabled = False
            Button2.Enabled = False
            RadWindowManager1.RadAlert("session is expired, Please login Again", 300, 200, "Session Expired Error", "")
            Response.Redirect("logout.aspx")
        Else
            agntid = Session("lgnagntid")
        End If

        If Request.QueryString("mode") = "edit" And Session("mode") = "" Then
           
            getrcdforedit()


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
    Private Sub getrcdforedit()
        Try

      
        qry = "select * from ams.ams_usermaster a, ams.ams_agents b where  a.lgnagntid=b.agntid and b.agntid='" & Request.QueryString("empid") & "'"
            dt = clas.getdata(qry, "QR")
        If dt.Rows.Count > 0 Then
            TextBox1.Text = Convert.ToString(dt.Rows(0)("agntname"))
            'TextBox2.Text = Convert.ToString(dt.Rows(0)("empmname"))
            'TextBox3.Text = Convert.ToString(dt.Rows(0)("emplname"))
            'TextBox4.Text = Convert.ToString(dt.Rows(0)("emptel"))
            TextBox5.Text = Convert.ToString(dt.Rows(0)("agnttelno"))
            TextBox6.Text = Convert.ToString(dt.Rows(0)("lgnnam"))
            ' TextBox7.Text = Convert.ToString(dt.Rows(0)("lgnpwd"))
            ' TextBox8.Text = Convert.ToString(dt.Rows(0)("lgnpwd"))
            
            TextBox10.Text = Convert.ToString(dt.Rows(0)("agntadd"))
            TextBox11.Text = Convert.ToString(dt.Rows(0)("agnteml"))
            'DropDownList1.Items.FindByValue(Convert.ToString(dt.Rows(0)("agntdesg"))).Selected = True
            'DropDownList1.DataBind()
            DropDownList2.Items.FindByValue(Convert.ToString(dt.Rows(0)("lgnsts"))).Selected = True
            DropDownList2.DataBind()
            DropDownList3.Items.FindByValue(Convert.ToString(dt.Rows(0)("lgnroll"))).Selected = True
            DropDownList3.DataBind()
            'DropDownList4.Items.FindByValue(Convert.ToString(dt.Rows(0)("result"))).Selected = True
            'DropDownList4.DataBind()
            'DropDownList4.Enabled = False
            Button1.Text = "Update"
            'dtSubmission.SelectedDate = dt.Rows(0)("empcontdt")
            'dtSubmission.Enabled = False
                'label2.Text = Convert.ToString(dt.Rows(0)("agntname"))
                TextBox6.Enabled = False
                TextBox7.Attributes.Add("Value", Convert.ToString(dt.Rows(0)("lgnpwd")))
                TextBox8.Attributes.Add("Value", Convert.ToString(dt.Rows(0)("lgnpwd")))
				 TextBox7.Enabled = False
                TextBox8.Enabled = False

                RequiredFieldValidator6.Enabled = False
                RequiredFieldValidator7.Enabled = False
                RequiredFieldValidator10.Enabled = False

				if  Convert.ToString(Session("lgnagntid")) <>   Convert.ToString(Request.QueryString("empid")) Then
				DropDownList3.Enabled = true
				DropDownList2.Enabled = true
				
				Else
				
				
				 DropDownList3.Enabled = False
				  DropDownList2.Enabled = False
				End IF 
				
               
            Session("mode") = "edit"
            End If
            '  Throw New Exception()
        Catch ex As Exception
            RadWindowManager1.RadAlert(ex.Message, 300, 200, "Exception", "")
        End Try
    End Sub
    'Private Sub getmastindustry()
    '    qry = "select * from ams.ams_indmaster"
    '    dt = getdata(qry, "QR")
    '    If dt.Rows.Count > 0 Then
    '        DropDownList2.DataTextField = "indname"
    '        DropDownList2.DataValueField = "indid"
    '        DropDownList2.DataSource = dt
    '        DropDownList2.DataBind()
    '    End If

    'End Sub
    'Private Sub getmastdesg()
    '    qry = "select * from ranu.desgmaster"
    '    dt = getdata(qry, "QR")
    '    If dt.Rows.Count > 0 Then
    '        DropDownList1.DataTextField = "designation"
    '        DropDownList1.DataValueField = "desgid"
    '        DropDownList1.DataSource = dt
    '        DropDownList1.DataBind()
    '    End If

    'End Sub
    'Private Sub getminmax()

    '    dtSubmission.MinDate = "30-Dec-2015"
    '    dtSubmission.MaxDate = Date.Today
    '    dtSubmission.SelectedDate = Date.Today


    'End Sub

    Public Function FirstDayOfMonth(ByVal sourceDate As DateTime) As DateTime
        Return New DateTime(sourceDate.Year, sourceDate.Month, 1)
    End Function

    'Get the last day of the month
    Public Function LastDayOfMonth(ByVal sourceDate As DateTime) As DateTime
        Dim lastDay As DateTime = New DateTime(sourceDate.Year, sourceDate.Month, 1)
        Return lastDay.AddMonths(1).AddDays(-1)
    End Function

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click, ImageButton1.Click
        'Dim match As Match = Regex.Match(TextBox6.Text, "admin", RegexOptions.IgnoreCase)
        'Dim match1 As Match = Regex.Match(TextBox6.Text, "adminstrator", RegexOptions.IgnoreCase)

		
		If Convert.ToString(Request.QueryString("empid")) <> "1" Then
		
        If (String.Compare(TextBox6.Text, "admin", StringComparison.OrdinalIgnoreCase) = 0) Or (String.Compare(TextBox6.Text, "adminstrator", StringComparison.OrdinalIgnoreCase) = 0) Then
            'If Match.Success And match1.Success Then
            RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Access Denied to Login Name, Please Choose other </div></li></ul>", 300, 150, "Validation Success", Nothing)
            Exit Sub
        Else
            If (TextBox6.Text.IndexOf("admin", 0, StringComparison.CurrentCultureIgnoreCase) > -1) Or (TextBox6.Text.IndexOf("adminstrator", 0, StringComparison.CurrentCultureIgnoreCase) > -1) Then
                RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Access Denied to Login Name, Please Choose other </div></li></ul>", 300, 150, "Validation Success", Nothing)
                Exit Sub
            End If
       End If
     End If

            Dim flag As Boolean
            If Button1.Text = "Update" Then
                qry = "select * from ams.ams_usermaster where lgnnam='" & TextBox6.Text & "' and lgnagntid <> '" & Request.QueryString("empid") & "'"

				
                dt = clas.getdata(qry, "QR")
                If dt.Rows.count > 0 Then
                    RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Login Name already exists, Please Choose other </div></li></ul>", 300, 150, "Validation Success", Nothing)
                    Exit Sub
                Else
                 
                    Dim Query_Str1 As String = "Select AGNTNAME,AGNTDESG,AGNTADD,AGNTEML,AGNTTELNO,LASTLOGIN,KEYEDON,AGNTID from AMS.ams_agents where lower(AGNTEML)=lower('" & TextBox11.Text.Trim & "') and AGNTID<>'" & Request.QueryString("empid") & "'"
					
						'Response.Write(Query_Str1)
                    Dim dt_chk1 As DataTable = clas.getdata(Query_Str1, "QR")
                    If dt_chk1.Rows.Count > 0 Then
                        EmlMsg.Visible = True
                        EmlMsg.InnerText = "This Email-Id is already registered for user: " & dt_chk1.Rows(0)("AGNTNAME") & vbCrLf & ". Please Enter some other Email-Id."
                        Exit Sub
                    End If

                    Dim Query_Str As String = "Select AGNTNAME,AGNTDESG,AGNTADD,AGNTEML,AGNTTELNO,LASTLOGIN,KEYEDON,AGNTID from AMS.ams_agents where AGNTTELNO='" & TextBox5.Text.Trim & "' and AGNTID<>'" & Request.QueryString("empid") & "'"
				'	Response.Write(Query_Str)
                    Dim dt_chk As DataTable = clas.getdata(Query_Str, "QR")
                    If dt_chk.Rows.Count > 0 Then
                        MobMsg.Visible = True
                        MobMsg.InnerText = "This Mobile Number is already registered for user: " & dt_chk.Rows(0)("AGNTNAME") & vbCrLf & ". Please Enter some other Mobile No."

                        Exit Sub

                    End If
                    remove_SingleCot()
                    flag = updateCommand()
                    'End If
                End If
        Else

            If DropDownList3.SelectedValue = "S" Then
                If Convert.ToString(Session("lgnagntid")) <> "1" Then
                    RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Access Denied, you have no righs to create Administrator User</div></li></ul>", 300, 150, "Validation Success", Nothing)
                    Exit Sub
                End If
            End If
            

            If TextBox7.Text = TextBox8.Text Then
                qry = "select count(*) from ams.ams_usermaster where lgnnam='" & TextBox6.Text & "'"
                dt = clas.getdata(qry, "QR")
                If dt.Rows(0)(0) > 0 Then
                    RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Login Name already exists, Please Choose other </div></li></ul>", 300, 150, "Validation Success", Nothing)
                    Exit Sub
                Else

                    Dim Query_Str1 As String = "Select AGNTNAME,AGNTDESG,AGNTADD,AGNTEML,AGNTTELNO,LASTLOGIN,KEYEDON,AGNTID from AMS.ams_agents where lower(AGNTEML)=lower('" & TextBox11.Text.Trim & "')"
                    Dim dt_chk1 As DataTable = clas.getdata(Query_Str1, "QR")
                    If dt_chk1.Rows.Count > 0 Then
                        EmlMsg.Visible = True
                        EmlMsg.InnerText = "This Email-Id is already registered for user: " & dt_chk1.Rows(0)("AGNTNAME") & vbCrLf & ". Please Enter some other Email-Id."
                        Exit Sub
                    End If

                    Dim Query_Str As String = "Select AGNTNAME,AGNTDESG,AGNTADD,AGNTEML,AGNTTELNO,LASTLOGIN,KEYEDON,AGNTID from AMS.ams_agents where AGNTTELNO='" & TextBox5.Text.Trim & "'"
                    Dim dt_chk As DataTable = clas.getdata(Query_Str, "QR")
                    If dt_chk.Rows.Count > 0 Then
                        MobMsg.Visible = True
                        MobMsg.InnerText = "This Mobile Number is already registered for user: " & dt_chk.Rows(0)("AGNTNAME") & vbCrLf & ". Please Enter some other Mobile No."

                        Exit Sub
                    End If


                    remove_SingleCot()
                    flag = InsertCommand()
                    'End If
                End If
            Else
                RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Password does not match. </div></li></ul>", 300, 150, "Validation Success", Nothing)
                Exit Sub
            End If
   
        End If
        If flag = True Then

            '	Response.Write("in")
            If Button1.Text = "Update" Then
                Button1.Text = "Submit"
                RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Agent Information updated successfully. </div></li></ul>", 300, 150, "Validation Success", Nothing)
            Else
                RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Agent Information saved successfully. </div></li></ul>", 300, 150, "Validation Success", Nothing)
            End If

        Else
            Session.Remove("mode")


            RadWindowManager1.RadAlert("<ul><li><div style=color:green;>" & Session("error") & " </div></li></ul>", 300, 150, "Validation Success", Nothing)


        End If

    End Sub

    Protected Sub remove_SingleCot()
        If TextBox1.Text <> "" Then
            TextBox1.Text = TextBox1.Text.Replace("'", "''")
        End If
        If TextBox5.Text <> "" Then
            TextBox5.Text = TextBox5.Text.Replace("'", "")
        End If
        If TextBox6.Text <> "" Then
            TextBox6.Text = TextBox6.Text.Replace("'", "")
        End If
        If TextBox7.Text <> "" Then
            TextBox7.Text = TextBox7.Text.Replace("'", "")
        End If
        If TextBox8.Text <> "" Then
            TextBox8.Text = TextBox8.Text.Replace("'", "")
        End If
        If TextBox10.Text <> "" Then
            TextBox10.Text = TextBox10.Text.Replace("'", "''")
        End If
        If TextBox11.Text <> "" Then
            TextBox11.Text = TextBox11.Text.Replace("'", "''")
        End If
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        clear()
        Button1.Text = "Submit"
    End Sub
    Public Function InsertCommand() As Boolean
        Try

      
            Dim transid As Int32 = clas.getMaxID("select ams.ams_agents_seq.nextval  from dual")
        'Dim str2 As String = "insert into tt.ukdata(transid, InDocRecdDt,InDocRecdBy, InDocStat) values(2, to_date(@InDocRecdDt,'dd-MON-yyyy'), @InDocRecdBy, @InDocStat)"
        Dim str As StringBuilder = New StringBuilder
        str.Append("insert into ams.ams_agents(agntID,")


        If TextBox1.Text <> "" Then
            str.Append("agntname,")
        End If
        'If TextBox2.Text <> "" Then
        '    str.Append("empmname,")
        'End If
        'If TextBox3.Text <> "" Then
        '    str.Append("emplname,")
        'End If
        'If DropDownList1.SelectedValue <> "" Then
        '    str.Append("agntdesg,")
        'End If
        'If TextBox4.Text <> "" Then
        '    str.Append("emptel,")
        'End If
        If TextBox5.Text <> "" Then
            str.Append("agnttelno,")
        End If
        'If DropDownList2.SelectedValue <> "" Then
        '    str.Append("empcmpind,")
        'End If
        'If TextBox6.Text <> "" Then
        '    str.Append("empcmpnam,")
        'End If
        'If TextBox7.Text <> "" Then
        '    str.Append("empcmptelno,")
        'End If
        'If TextBox8.Text <> "" Then
        '    str.Append("empcmpfax,")
        'End If
       
        If TextBox10.Text <> "" Then
            str.Append("agntadd,")
        End If
        'If dtSubmission.SelectedDate IsNot Nothing Then
        '    str.Append("empcontdt,")
        'End If
        If TextBox11.Text <> "" Then
            str.Append("agnteml,")
        End If
        'If DropDownList4.SelectedValue <> "" Then
        '    str.Append("result,")
        'End If
        Dim temp As String = str.ToString
        str.Clear()
        str.Append(temp.Substring(0, temp.Length - 1))
        str.Append(",agntdesg) values('" & transid & "',")

        If TextBox1.Text <> "" Then
            str.Append("'" & TextBox1.Text & "',")
        End If
        'If TextBox2.Text <> "" Then
        '    str.Append("'" & TextBox2.Text & "',")
        'End If
        'If TextBox3.Text <> "" Then
        '    str.Append("'" & TextBox3.Text & "',")
        'End If
        'If DropDownList1.SelectedValue <> "" Then
        '    str.Append("'" & DropDownList1.SelectedValue & "',")
        'End If
        'If TextBox4.Text <> "" Then
        '    str.Append("'" & TextBox4.Text & "',")
        'End If
        If TextBox5.Text <> "" Then
            str.Append("'" & TextBox5.Text & "',")
        End If
        'If DropDownList2.SelectedValue <> "" Then
        '    str.Append("'" & DropDownList2.SelectedValue & "',")
        'End If
        'If TextBox6.Text <> "" Then
        '    str.Append("'" & TextBox6.Text & "',")
        'End If
        'If TextBox7.Text <> "" Then
        '    str.Append("'" & TextBox7.Text & "',")
        'End If
        'If TextBox8.Text <> "" Then
        '    str.Append("'" & TextBox8.Text & "',")
        'End If
        'If TextBox9.Text <> "" Then
        '    str.Append("'" & TextBox9.Text & "',")
        'End If
        If TextBox10.Text <> "" Then
            str.Append("'" & TextBox10.Text & "',")
        End If
        'If dtSubmission.SelectedDate IsNot Nothing Then
        '    str.Append("'" & Format("dd-MON-yyyy", dtSubmission.SelectedDate) & "',")
        'End If
        If TextBox11.Text <> "" Then
            str.Append("'" & TextBox11.Text & "','Agent',")
        End If
        'If DropDownList4.SelectedValue <> "" Then
        '    str.Append("'" & DropDownList4.SelectedValue & "',")
        'End If

        'HttpContext.Current.Request.UserHostAddress

       
        temp = str.ToString
        str.Clear()
        str.Append(temp.Substring(0, temp.Length - 1))
        str.Append(")")
        'Try

            clas.ExecuteNonQuery(str.ToString)

       
        InsertCommand2(transid)
        Return True
        'Catch ex As Exception
        '    Session("error") = ex.Message
        '    RadWindowManager1.RadAlert("<ul><li><div style=color:Red;>" & ex.Message & "</div></li></ul>", 300, 150, "Validation Error", Nothing)
        '    Return False
        '    Exit Function
            'End Try
            '  Throw New Exception("UnKnown Exception occured , Please Contact IT Department")
        Catch ex As Exception
		
            RadWindowManager1.RadAlert(ex.Message, 300, 200, "Exception", "")
            Return False
        End Try
    End Function

    Public Function updateCommand() As Boolean
        Try
            'Dim transid As Int32 = getMaxID()
            'Dim str2 As String = "insert into tt.ukdata(transid, InDocRecdDt,InDocRecdBy, InDocStat) values(2, to_date(@InDocRecdDt,'dd-MON-yyyy'), @InDocRecdBy, @InDocStat)"
            Dim str As StringBuilder = New StringBuilder
            str.Append("update ams.ams_agents set ")


            If TextBox1.Text <> "" Then
                str.Append("agntname='" & TextBox1.Text & "',")
            End If
            'If TextBox2.Text <> "" Then
            '    str.Append("empmname='" & TextBox2.Text & "',")
            'End If
            'If TextBox3.Text <> "" Then
            '    str.Append("emplname='" & TextBox3.Text & "',")
            'End If
            'If DropDownList1.SelectedValue <> "" Then
            '    str.Append("agntdesg='" & DropDownList1.SelectedValue & "',")
            'End If
            'If TextBox4.Text <> "" Then

            '    str.Append("emptel='" & TextBox4.Text & "',")
            'End If
            If TextBox5.Text <> "" Then

                str.Append("agnttelno='" & TextBox5.Text & "',")
            End If
            'If DropDownList2.SelectedValue <> "" Then

            '    str.Append("empcmpind='" & DropDownList2.SelectedValue & "',")
            'End If
            'If TextBox6.Text <> "" Then
            '    str.Append("empcmpnam='" & TextBox6.Text & "',")
            'End If
            'If TextBox7.Text <> "" Then

            '    str.Append("empcmptelno='" & TextBox7.Text & "',")
            'End If
            'If TextBox8.Text <> "" Then

            '    str.Append("empcmpfax='" & TextBox8.Text & "',")
            'End If
            'If TextBox9.Text <> "" Then

            '    str.Append("empcmpweb='" & TextBox9.Text & "',")
            'End If
            If TextBox10.Text <> "" Then

                str.Append("agntadd='" & TextBox10.Text & "',")
            End If
            If TextBox11.Text <> "" Then

                str.Append("agnteml='" & TextBox11.Text & "',")
            End If
            Dim temp As String = str.ToString
            str.Clear()
            str.Append(temp.Substring(0, temp.Length - 1))
            str.Append(" where agntid='" & Request.QueryString("empid") & "'")


            'temp = str.ToString
            'str.Clear()
            'str.Append(temp.Substring(0, temp.Length - 1))
            'str.Append(")")
            'Try

			
			
            clas.ExecuteNonQuery(str.ToString())
            UpdateCommand2(Request.QueryString("empid"))
            Return True
            'Catch ex As Exception

            '    Session("error") = ex.Message
            '    RadWindowManager1.RadAlert("<ul><li><div style=color:Red;>" & ex.Message & "</div></li></ul>", 300, 150, "Validation Error", Nothing)
            '    Return False
            '    Exit Function
            'End Try
            ' Throw New Exception("UnKnown Exception occured , Please Contact IT Department")
        Catch ex As Exception
            RadWindowManager1.RadAlert(ex.Message, 300, 200, "Exception", "")
            Return False
        End Try
    End Function

    Public Function InsertCommand2(ByVal agntid As Int32) As Boolean
        ' Dim transid As Int32 = getMaxempresID()
        Try
            Dim str As StringBuilder = New StringBuilder
            str.Append("insert into ams.ams_usermaster(id,lgnnam,lgnpwd,lgnroll,lgnsts,lgnagntid) values(ams.ams_usermaster_seq.nextval,'" & TextBox6.Text & "','" & TextBox8.Text & "','" & DropDownList3.SelectedValue & "','" & DropDownList2.SelectedValue & "','" & agntid & "')")




            'If DropDownList4.SelectedValue <> "" Then
            '    str.Append("resultid,")
            'End If
            'Dim temp As String = str.ToString
            'str.Clear()
            'str.Append(temp.Substring(0, temp.Length - 1))
            'str.Append(",  KEYEDON, KEYEDBY,ip) values( ams.ams_agnt_seq.nextval ," & empid & ",")


            'If DropDownList4.SelectedValue <> "" Then
            '    str.Append("'" & DropDownList4.SelectedValue & "',")
            'End If


            ''HttpContext.Current.Request.UserHostAddress
            'str.Append("sysdate," & empno & ",'" & HttpContext.Current.Request.UserHostAddress & "',")


            'temp = str.ToString
            'str.Clear()
            'str.Append(temp.Substring(0, temp.Length - 1))
            'str.Append(")")
            Dim i = clas.ExecuteNonQuery(str.ToString())
            If i = 1 Then

                If chk.Checked = True Then
                    Dim BodyStr = " This is to inform you that Your Login Credentials are : "
                    BodyStr &= "<table style=font-size:12px; line-height:16px; color:#333; font-family:Arial, Helvetica, sans-serif; text-align:justify; ><tr><td>Login ID</td><td>" & TextBox6.Text & "</td></tr><tr><td>Password:</td><td>" & TextBox8.Text & "</td></tr></table>"

                    Dim Auto_Qry = "insert into rahul.autoemail "
                    Auto_Qry &= " ( ID,MAILSUB,MAILTO,MAILINT,MAILFRM,MAILBCC,MAILCC,MAILRPLYTO,DEPT,PRSN,BODYSTT"
                    Auto_Qry &= " ,MAILATCH,MAILSNDON,STATUS,KEYEDON,ACTSNDON,REMARKS,BODYTEMP,MAILTEMP )"
                    Auto_Qry &= " values ("
                    Auto_Qry &= " rahul.automail_seq.nextval,'AMS Login Credentials' ,'" & TextBox11.Text & "',"
                    Auto_Qry &= " 'AMS Login Credentials  For User : '||''||'" & TextBox1.Text & "'||' ','mis_alerts@acumen-services.com','itdept@wwicsgroup.com','rahul.gupta@pinnacleinfoedge.com',null,null,null,"
                    Auto_Qry &= " '" & BodyStr & "',null,sysdate,'I',sysdate,null,null,2,3)"
                    clas.ExecuteNonQuery(Auto_Qry)

                End If


                Return True

            Else



                Dim Err_Msg As String = clas.getexception().Replace(vbCrLf, "").Replace("'", "")
                Err_Msg = Err_Msg.Replace(vbCr, "")
                Err_Msg = Err_Msg.Replace(vbLf, "")
				Session("error") =  Err_Msg
                RadWindowManager1.RadAlert("<ul><li><div style=color:red;>System found some exception:" & Err_Msg & "</div></li></ul>", 300, 100, "Validation Exception Failure", Nothing)
                Return False

            End If
            '  Throw New Exception("UnKnown Exception occured , Please Contact IT Department")
        Catch ex As Exception
            RadWindowManager1.RadAlert(ex.Message, 300, 200, "Exception", "")
            Return False
        End Try

    End Function

    Public Function UpdateCommand2(ByVal agntid As Int32) As Boolean
        Try
            ' Dim transid As Int32 = getMaxempresID()
            Dim str As StringBuilder = New StringBuilder
            str.Append("update ams.ams_usermaster set lgnroll='" & DropDownList3.SelectedValue & "',lgnsts='" & DropDownList2.SelectedValue & "' where lgnagntid='" & agntid & "'")


          '  Response.Write(str.ToString())
           Dim i = clas.ExecuteNonQuery(str.ToString())
            If i = 1 Then

                If chk.Checked = True Then
                    Dim BodyStr = " This is to inform you that Your Login Credentials are : "
                    BodyStr &= "<table style=font-size:12px; line-height:16px; color:#333; font-family:Arial, Helvetica, sans-serif; text-align:justify; ><tr><td>Login ID</td><td>" & TextBox6.Text & "</td></tr><tr><td>Password:</td><td>" & TextBox8.Text & "</td></tr></table>"

                    Dim Auto_Qry = "insert into rahul.autoemail "
                    Auto_Qry &= " ( ID,MAILSUB,MAILTO,MAILINT,MAILFRM,MAILBCC,MAILCC,MAILRPLYTO,DEPT,PRSN,BODYSTT"
                    Auto_Qry &= " ,MAILATCH,MAILSNDON,STATUS,KEYEDON,ACTSNDON,REMARKS,BODYTEMP,MAILTEMP )"
                    Auto_Qry &= " values ("
                    Auto_Qry &= " rahul.automail_seq.nextval,'AMS Login Credentials' ,'" & TextBox11.Text & "',"
                    Auto_Qry &= " 'AMS Login Credentials For User : '||''||'" & TextBox1.Text & "'||' ','mis_alerts@acumen-services.com','itdept@wwicsgroup.com','rahul.gupta@pinnacleinfoedge.com',null,null,null,"
                    Auto_Qry &= " '" & BodyStr & "',null,sysdate,'I',sysdate,null,null,2,3)"
                    clas.ExecuteNonQuery(Auto_Qry)

                End If

                Return True

            Else



                Dim Err_Msg As String = clas.getexception().Replace(vbCrLf, "").Replace("'", "")
                Err_Msg = Err_Msg.Replace(vbCr, "")
                Err_Msg = Err_Msg.Replace(vbLf, "")
				Session("error") =  Err_Msg
                RadWindowManager1.RadAlert("<ul><li><div style=color:red;>System found some exception:" & Err_Msg & "</div></li></ul>", 300, 100, "Validation Exception Failure", Nothing)
                Return False

            End If
        Catch ex As Exception
            RadWindowManager1.RadAlert(ex.Message, 300, 200, "Exception", "")
            Return False
        End Try

    End Function
 
   
   

    Private Sub clear()
        TextBox1.Text = String.Empty
        'TextBox2.Text = String.Empty
        'TextBox3.Text = String.Empty
        'TextBox4.Text = String.Empty
        TextBox5.Text = String.Empty
        TextBox6.Text = String.Empty
        TextBox7.Text = String.Empty
        TextBox8.Text = String.Empty
        'TextBox9.Text = String.Empty
        TextBox10.Text = String.Empty
        TextBox11.Text = String.Empty
        'DropDownList1.ClearSelection()
        'DropDownList1.DataBind()
        DropDownList2.ClearSelection()
        DropDownList2.DataBind()
        TextBox7.Attributes("value") = ""
        TextBox8.Attributes("value") = ""
        TextBox7.Text = ""
        TextBox8.Text = ""
        'DropDownList4.ClearSelection()
        'DropDownList4.DataBind()
        'dtSubmission.SelectedDate = Date.Today
        'label2.Text = Session("lgnagntnam")
        'Button1.Text = "Submit"
        Session.Remove("mode")
        Dim nvc = HttpUtility.ParseQueryString(Request.Url.Query)
        nvc.Remove("mode")
        Dim url As String = Request.Url.AbsolutePath + "?" + nvc.ToString()
        'Response.Redirect(url)
        'Me.Request.QueryString.Remove("mode")
    End Sub

    Protected Sub TextBox11_TextChanged(sender As Object, e As System.EventArgs) Handles TextBox11.TextChanged
        If TextBox11.Text <> "" Then
            If TextBox11.Text <> "" Then
                TextBox11.Text = TextBox11.Text.Replace("'", "")
            End If
            Dim Query_Str As String
            If Button1.Text = "Update" Then
                Query_Str = "Select AGNTNAME,AGNTDESG,AGNTADD,AGNTEML,AGNTTELNO,LASTLOGIN,KEYEDON,AGNTID from AMS.ams_agents where lower(AGNTEML)=lower('" & TextBox11.Text.Trim & "') and AGNTID<>'" & Request.QueryString("empid") & "'"
            Else
                Query_Str = "Select AGNTNAME,AGNTDESG,AGNTADD,AGNTEML,AGNTTELNO,LASTLOGIN,KEYEDON,AGNTID from AMS.ams_agents where lower(AGNTEML)=lower('" & TextBox11.Text.Trim & "')"
            End If

            Dim dt_chk As DataTable = clas.getdata(Query_Str, "QR")
            If dt_chk.Rows.Count > 0 Then
                EmlMsg.Visible = True
                EmlMsg.InnerText = "This Email-Id is already registered for user: " & dt_chk.Rows(0)("AGNTNAME") & vbCrLf & ". Please Enter some other Email-Id."
                'RadWindowManager1.RadAlert("<ul><li><div style=color:green;> This Email-Id is already registered by BDC: " & dt_chk.Rows(0)("AgntName") & vbCrLf & ". Please Enter some other Email-Id.</div></li></ul>", 300, 150, "Validation Success", Nothing)
                'TextBox11.Text = String.Empty
            Else
                EmlMsg.Visible = False
            End If
        Else
            EmlMsg.Visible = False
        End If
    End Sub

    Protected Sub TextBox5_TextChanged(sender As Object, e As System.EventArgs) Handles TextBox5.TextChanged
        If TextBox5.Text <> "" Then
            If TextBox5.Text <> "" Then
                TextBox5.Text = TextBox5.Text.Replace("'", "")
            End If
            Dim Query_Str As String
            If Button1.Text = "Update" Then
                Query_Str = "Select AGNTNAME,AGNTDESG,AGNTADD,AGNTEML,AGNTTELNO,LASTLOGIN,KEYEDON,AGNTID from AMS.ams_agents where AGNTTELNO='" & TextBox5.Text.Trim & "' and AGNTID<>'" & Request.QueryString("empid") & "'"
            Else
                Query_Str = "Select AGNTNAME,AGNTDESG,AGNTADD,AGNTEML,AGNTTELNO,LASTLOGIN,KEYEDON,AGNTID from AMS.ams_agents where AGNTTELNO='" & TextBox5.Text.Trim & "'"
            End If

            Dim dt_chk As DataTable = clas.getdata(Query_Str, "QR")
            If dt_chk.Rows.Count > 0 Then
                MobMsg.Visible = True
                MobMsg.InnerText = "This Mobile Number is already registered for user: " & dt_chk.Rows(0)("AGNTNAME") & vbCrLf & ". Please Enter some other Mobile No."
                'RadWindowManager1.RadAlert("<ul><li><div style=color:green;> This Email-Id is already registered by BDC: " & dt_chk.Rows(0)("AgntName") & vbCrLf & ". Please Enter some other Email-Id.</div></li></ul>", 300, 150, "Validation Success", Nothing)
                'TextBox5.Text = String.Empty
            Else
                MobMsg.Visible = False
            End If
        Else
            MobMsg.Visible = False
        End If
    End Sub
End Class
