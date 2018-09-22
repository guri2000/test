Imports System.Data
Imports System.Web.UI.WebControls
Imports System.IO
Imports Telerik.Web.UI
Imports System.Text

Imports System.Environment
Imports System.Net

Imports System.Net.Mail
Imports System.Net.Mime

Partial Class Candidate_Feedback
    Inherits System.Web.UI.Page
    Dim clas As rahulcpm.commonclass = New rahulcpm.commonclass()
    Dim Flag As Boolean = False
    Dim Flag_Done As Boolean = False
    Dim SqlQuery As String
    Dim _DataTable As DataTable
    Dim EID As Integer = 0
    Dim JID As Integer = 0
    Dim AID As Integer = 0
    Dim FSNO As Integer = 0

    

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session("lgnagntid") = "0" Or Session("lgnagntid") = "" Then
            RadWindowManager1.RadAlert("session is expired, Please login Again", 300, 200, "Session Expired Error", "")
            Response.Redirect("logout.aspx")
        End If

        If Request.QueryString("cid") IsNot Nothing And Not Request.QueryString("cid") = "" Then
            Dim JIds As String() = Get_JobId(Request.QueryString("cid"))
            JID = JIds(0)
            FSNO = JIds(1)
            If JID <> 0 Then
                Dim Ids As String() = Get_EmpId(JID)
                EID = Ids(0)
                AID = Ids(1)
            End If
        End If

        If Page.IsPostBack = False Then
            Bind_Interview_Status()
            Bind_Agent_LSTBX()

            If Request.QueryString("cid") IsNot Nothing And Not Request.QueryString("cid") = "" Then
                Bind_JobPosition()
                If JID <> 0 Then
                    ddl_Position.SelectedValue = JID
                    Bind_Agent(AID)
                End If
                Clear()
                Bind_Existing_Page(ddl_Position.SelectedValue)
            End If

            'If Request.QueryString("mode") = "edit" Then
            'If Request.QueryString("cid") IsNot Nothing And Not Request.QueryString("cid") = "" Then

            'End If
            'End If

            If Interview_Date.SelectedDate Is Nothing Then
                Interview_Date.SelectedDate = DateTime.Today
            End If

            Dim Chk_flag As Boolean = Chk_FSN_Existance_4_Revoked(FSNO)
            If Chk_flag = True Then
                ImgBtn_Save.Visible = False
                ImgBtn_Refresh.Visible = False
                RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Candidate bearing FSN No : " & FSNO & " already got revoked. User cannot make further changes.</div></li></ul>", 300, 150, "Validation Success", Nothing)
            Else
                ImgBtn_Save.Visible = True
                ImgBtn_Refresh.Visible = True
            End If

        End If



    End Sub

    Protected Sub Bind_Agent_LSTBX()
        Try
            Dim Query_str1 As String = ""
            Query_str1 = " (select b.agntId as AId ,b.AGNTNAME as AName from ams.ams_usermaster a, ams.ams_agents b "
            Query_str1 += " where b.agntid=a.lgnagntid and a.lgnsts='A')"
            Query_str1 += " Union All "
            Query_str1 += " (Select EMPNO as AId ,name || ' - ' || EmpDesignation  as AName from tt.employees where active='T' "
            Query_str1 += " and zbaid=11 and lower(name) not like '%dummy%')"
            Dim DT_3 As DataTable = New DataTable
            DT_3 = clas.getdata(Query_str1, "TX")
            If DT_3.Rows.Count > 0 Then
                Lst_IntPanel.DataSource = DT_3
                Lst_IntPanel.DataTextField = "AName"
                Lst_IntPanel.DataValueField = "AId"
                Lst_IntPanel.DataBind()
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

    Private Sub Bind_JobPosition()
        Try
            Dim qry_str As String
            'qry_str = "Select JobSecuredId as JId ,JobPositionID as Descr from AMS.AMS_JobSecured where VacancyStatusId<>-1 and EmpId='" & EmpId & "'"

            'qry_str = " Select JobSecuredId as JId ,JobPositionID as Descr from ams.ams_jobsecured where VacancyStatusId<>-1 and JobSecuredId in "
            'qry_str += " (Select JobId from ams.ams_candidate where cand_status='A' and fsn in (Select fsn from AMS.AMS_candidate where TransId='" & TId & "') "
            'qry_str += " and JOBID is not null)"

            qry_str = " Select JobSecuredId as JId ,JobPositionID as Descr from ams.ams_jobsecured where VacancyStatusId<>-1 and JobSecuredId in "
            qry_str += " (Select JobId from ams.ams_candidate where cand_status='A' and fsn in ('" & FSNO & "') "
            qry_str += " and JOBID is not null)"

            Dim dt_4 As DataTable = clas.getdata(qry_str, "TX")
            If dt_4.Rows.Count > 0 Then
                ddl_Position.DataTextField = "Descr"
                ddl_Position.DataValueField = "JId"
                ddl_Position.DataSource = dt_4
                ddl_Position.DataBind()
            Else
                ddl_Position.DataTextField = "Descr"
                ddl_Position.DataValueField = "JId"
                ddl_Position.DataSource = dt_4
                ddl_Position.DataBind()
            End If
            'ddl_Position.Items.Insert(0, New ListItem("Show All", ""))
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

    Protected Function Get_JobId(ByVal CId As String) As String()
        Dim Ids(1) As String
        Dim QRY_STR As String = "Select * from AMS.AMS_Candidate where TransId='" & CId & "'"
        Dim dt2 As DataTable = New DataTable
        dt2 = clas.getdata(QRY_STR, "TX")
        Ids(0) = dt2.Rows(0)("Jobid")
        Ids(1) = dt2.Rows(0)("FSN")
        Return Ids
    End Function

    Protected Function Get_EmpId(ByVal JOID As Integer) As String()
        Dim Ids(1) As String
        Dim QRY_STR As String = "Select * from AMS.AMS_JobSecured where JobSecuredId='" & JOID & "'"
        Dim dt2 As DataTable = New DataTable
        dt2 = clas.getdata(QRY_STR, "TX")
        Ids(0) = dt2.Rows(0)("EmpId")
        Ids(1) = dt2.Rows(0)("AgentId")
        Return Ids
    End Function

    Private Sub Bind_Agent(ByVal Id_Agent As String)
        Dim qry_str As String
        qry_str = "select a.agntid as  agnt_Id, a.agntname as agnt_Name from ams.ams_agents a, ams.ams_usermaster b where b.lgnagntid=a.agntid  and a.agntid='" & Id_Agent & "'" 'and b.lgnsts='A'
        Dim dt_2 As DataTable = clas.getdata(qry_str, "TX")
        If dt_2.Rows.Count > 0 Then
            lbl_Agent.Text = dt_2.Rows(0)("agnt_Name")
        End If
    End Sub

    Protected Sub Bind_Interview_Status()
        Dim QRY_STR As String = "Select TransId,Description from ams.ams_intstatus where Scope=1 order by Description ASC"
        Dim dt_INTStatus As DataTable = New DataTable
        dt_INTStatus = clas.getdata(QRY_STR, "TX")
        ddl_Fin_status.DataTextField = "Description"
        ddl_Fin_status.DataValueField = "TransId"
        ddl_Fin_status.DataSource = dt_INTStatus
        ddl_Fin_status.DataBind()
    End Sub

    Protected Sub ddl_Position_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddl_Position.SelectedIndexChanged
        If ddl_Position.SelectedValue <> "" Then
            Clear()
            Bind_Existing_Page(ddl_Position.SelectedValue)
        End If
    End Sub

    Protected Sub Bind_Existing_Page(ByVal Jobid As String)
        Try
            Dim Query_str1 As String = ""
            Query_str1 = " Select"
            Query_str1 += " Transid, FSN, JOBID,INT_DT as INT_DT, PANEL1, PANEL2, PANEL3, PANEL4, PANEL5, FBK_EMPLOYER, FBK_BDC, FINAL_CAND_STATUS, KEYEDON, KEYEDBY"
            Query_str1 += " from ams.ams_feedback "
            Query_str1 += " where JOBID='" & Jobid & "' and FSN='" & FSNO & "'"
            Dim DT_3 As DataTable = New DataTable
            DT_3 = clas.getdata(Query_str1, "TX")
            If DT_3.Rows.Count > 0 Then
                If IsDBNull(DT_3.Rows(0)("INT_DT")) = False Then
                    Interview_Date.SelectedDate = DT_3.Rows(0)("INT_DT")
                Else
                    Interview_Date.SelectedDate = DateTime.Now
                End If

                For Each item As RadListBoxItem In Lst_IntPanel.Items
                    If IsDBNull(DT_3.Rows(0)("PANEL1")) = False Then
                        If item.Value = DT_3.Rows(0)("PANEL1") Then
                            item.Checked = True
                        End If
                    End If
                    If IsDBNull(DT_3.Rows(0)("PANEL2")) = False Then
                        If item.Value = DT_3.Rows(0)("PANEL2") Then
                            item.Checked = True
                        End If
                    End If
                    If IsDBNull(DT_3.Rows(0)("PANEL3")) = False Then
                        If item.Value = DT_3.Rows(0)("PANEL3") Then
                            item.Checked = True
                        End If
                    End If
                    If IsDBNull(DT_3.Rows(0)("PANEL4")) = False Then
                        If item.Value = DT_3.Rows(0)("PANEL4") Then
                            item.Checked = True
                        End If
                    End If
                    If IsDBNull(DT_3.Rows(0)("PANEL5")) = False Then
                        If item.Value = DT_3.Rows(0)("PANEL5") Then
                            item.Checked = True
                        End If
                    End If
                Next
                If IsDBNull(DT_3.Rows(0)("FBK_EMPLOYER")) = False Then
                    txt_Feedback_Emp.Text = DT_3.Rows(0)("FBK_EMPLOYER")
                End If
                If IsDBNull(DT_3.Rows(0)("FBK_BDC")) = False Then
                    txt_Feedback_BDC.Text = DT_3.Rows(0)("FBK_BDC")
                End If
                If IsDBNull(DT_3.Rows(0)("FINAL_CAND_STATUS")) = False Then
                    ddl_Fin_status.SelectedValue = DT_3.Rows(0)("FINAL_CAND_STATUS")
                End If
                ImgBtn_Save.AlternateText = "Update"
            Else

                ImgBtn_Save.AlternateText = "Save"
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

    Protected Function Chk_FSN_Existance_4_Revoked(ByVal FSN As String) As Boolean
        Dim Chk_Flag As Boolean = False
        Dim Qry_str1 As String = String.Empty
        Qry_str1 = "Select * from ams.ams_candidate where FSN='" & FSN & "' and Int_result='5'"
        Dim dt_7 As DataTable = clas.getdata(Qry_str1, "TX")
        If dt_7.Rows.Count > 0 Then
            Chk_Flag = True
        End If
        Return Chk_Flag
    End Function


    Protected Sub ImgBtn_Save_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImgBtn_Save.Click

        Dim Chk_flag As Boolean = Chk_FSN_Existance_4_Revoked(FSNO)
        If Chk_flag = True Then
            RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Candidate bearing FSN No : " & FSNO & " already got revoked. </div></li></ul>", 300, 150, "Validation Success", Nothing)
            Exit Sub
        End If

        If Lst_IntPanel.CheckedItems.Count = 0 Then
            RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Please select panel to save information. </div></li></ul>", 300, 200, "Exception", Nothing)
            Exit Sub
        End If

        If Interview_Date.SelectedDate Is Nothing Then
            RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Please select Interview Date to save information. </div></li></ul>", 300, 200, "Exception", Nothing)
            Exit Sub
        End If

        Dim flag As Boolean = False
        If ImgBtn_Save.AlternateText = "Save" Then

            flag = Insert_Command()
            If flag = True Then
                Update_candidateFinal_Status()
                RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Candidate Interview Record Saved Successfully. </div></li></ul>", 300, 150, "Validation Success", Nothing)
                Clear()
                Bind_Existing_Page(ddl_Position.SelectedValue)
            Else
                Exit Sub
            End If
        ElseIf ImgBtn_Save.AlternateText = "Update" Then
            flag = Update_Command()
            If flag = True Then
                Update_candidateFinal_Status()
                RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Candidate Interview Record Updated Successfully. </div></li></ul>", 300, 150, "Validation Success", Nothing)
                Clear()
                Bind_Existing_Page(ddl_Position.SelectedValue)
            Else
                Exit Sub
            End If
        End If

    End Sub

    Protected Function Insert_Command() As Boolean
        Dim Flag As Boolean = False
        Try
            Dim Panel_1 As String = String.Empty
            Dim Panel_2 As String = String.Empty
            Dim Panel_3 As String = String.Empty
            Dim Panel_4 As String = String.Empty
            Dim Panel_5 As String = String.Empty
            Dim count As Integer = 0


            Dim collection As IList(Of RadListBoxItem) = Lst_IntPanel.CheckedItems
            For Each item As RadListBoxItem In collection
                count += 1
                If count = 1 Then
                    Panel_1 = item.Value
                ElseIf count = 2 Then
                    Panel_2 = item.Value
                ElseIf count = 3 Then
                    Panel_3 = item.Value
                ElseIf count = 4 Then
                    Panel_4 = item.Value
                ElseIf count = 5 Then
                    Panel_5 = item.Value
                End If
            Next

            Dim TransId As Integer = clas.getMaxID("SELECT nvl(max(TRANSID),0)+1 from AMS.AMS_Feedback")

            Dim Insert_Query As StringBuilder = New StringBuilder
            Insert_Query.Append("Insert Into AMS.AMS_Feedback (TRANSID,FSN,JOBID,INT_DT,")
            If Panel_1 <> "" Then
                Insert_Query.Append("PANEL1,")
            End If
            If Panel_2 <> "" Then
                Insert_Query.Append("PANEL2,")
            End If
            If Panel_3 <> "" Then
                Insert_Query.Append("PANEL3,")
            End If
            If Panel_4 <> "" Then
                Insert_Query.Append("PANEL4,")
            End If
            If Panel_5 <> "" Then
                Insert_Query.Append("PANEL5,")
            End If
            If txt_Feedback_Emp.Text <> "" Then
                Insert_Query.Append("FBK_EMPLOYER,")
            End If
            If txt_Feedback_BDC.Text <> "" Then
                Insert_Query.Append("FBK_BDC,")
            End If
            Insert_Query.Append("FINAL_CAND_STATUS,KEYEDON,KEYEDBY)")
            Insert_Query.Append(" Values")
            Insert_Query.Append(" (")
            Insert_Query.Append(" '" & TransId & "', '" & FSNO & "','" & ddl_Position.SelectedValue & "',to_date('" & Interview_Date.SelectedDate.Value.ToString("dd-MMM-yyyy HH:mm:ss") & "','dd-MON-yyyy hh24:mi:ss'),")

            If Panel_1 <> "" Then
                Insert_Query.Append("'" & Panel_1 & "',")
            End If
            If Panel_2 <> "" Then
                Insert_Query.Append("'" & Panel_2 & "',")
            End If
            If Panel_3 <> "" Then
                Insert_Query.Append("'" & Panel_3 & "',")
            End If
            If Panel_4 <> "" Then
                Insert_Query.Append("'" & Panel_4 & "',")
            End If
            If Panel_5 <> "" Then
                Insert_Query.Append("'" & Panel_5 & "',")
            End If
            If txt_Feedback_Emp.Text <> "" Then
                Insert_Query.Append("'" & txt_Feedback_Emp.Text & "',")
            End If
            If txt_Feedback_BDC.Text <> "" Then
                Insert_Query.Append("'" & txt_Feedback_BDC.Text & "',")
            End If
            Insert_Query.Append("'" & ddl_Fin_status.SelectedValue & "',sysdate,'" & Session("lgnagntid") & "'")
            Insert_Query.Append(" )")
            Dim i = clas.ExecuteNonQuery(Insert_Query.ToString())
            If i = 1 Then



                Flag = True
                Return Flag
            Else
                Flag = False
                Dim Err_Msg As String = clas.getexception().Replace(vbCrLf, "")
                Err_Msg = Err_Msg.Replace(vbCr, "")
                Err_Msg = Err_Msg.Replace(vbLf, "")
                RadWindowManager1.RadAlert("<ul><li><div style=color:red;>System found some exception:" & Err_Msg & "</div></li></ul>", 300, 100, "Validation Exception Failure", Nothing)

                '  RadWindowManager1.RadAlert("<ul><li><div style=color:red;> Transaction not Done </div></li></ul>", 300, 150, "Validation Success", Nothing)
            End If
           
            Throw New Exception("UnKnown Exception occured , Please Contact IT Department")
        Catch ex As Exception
            RadWindowManager1.RadAlert("<ul><li><div style=color:green;>" & ex.Message.ToString & "</div></li></ul>", 300, 200, "Exception", Nothing)
            Flag = False
        End Try
        Return Flag
    End Function

    

    Protected Function Update_Command() As Boolean
        Dim Flag As Boolean = False
        Dim TRANSID As String = Request.QueryString("cid")
        Try

            Dim Panel_1 As String = String.Empty
            Dim Panel_2 As String = String.Empty
            Dim Panel_3 As String = String.Empty
            Dim Panel_4 As String = String.Empty
            Dim Panel_5 As String = String.Empty
            Dim count As Integer = 0


            Dim collection As IList(Of RadListBoxItem) = Lst_IntPanel.CheckedItems
            For Each item As RadListBoxItem In collection
                count += 1
                If count = 1 Then
                    Panel_1 = item.Value
                ElseIf count = 2 Then
                    Panel_2 = item.Value
                ElseIf count = 3 Then
                    Panel_3 = item.Value
                ElseIf count = 4 Then
                    Panel_4 = item.Value
                ElseIf count = 5 Then
                    Panel_5 = item.Value
                End If
            Next

            Dim Update_Query As StringBuilder = New StringBuilder

            Update_Query.Append("Update AMS.AMS_Feedback Set ")
            If Interview_Date.SelectedDate IsNot Nothing Then
                Update_Query.Append("INT_DT=to_date('" & Interview_Date.SelectedDate.Value.ToString("dd-MMM-yyyy HH:mm:ss") & "','dd-MON-yyyy hh24:mi:ss') ,")
            Else
                Update_Query.Append("INT_DT=null,")
            End If

            If Panel_1 <> "" Then
                Update_Query.Append("PANEL1='" & Panel_1 & "',")
            Else
                Update_Query.Append("PANEL1=null,")
            End If
            If Panel_2 <> "" Then
                Update_Query.Append("PANEL2='" & Panel_2 & "',")
            Else
                Update_Query.Append("PANEL2=null,")
            End If
            If Panel_3 <> "" Then
                Update_Query.Append("PANEL3='" & Panel_3 & "',")
            Else
                Update_Query.Append("PANEL3=null,")
            End If
            If Panel_4 <> "" Then
                Update_Query.Append("PANEL4='" & Panel_4 & "',")
            Else
                Update_Query.Append("PANEL4=null,")
            End If
            If Panel_5 <> "" Then
                Update_Query.Append("PANEL5='" & Panel_5 & "',")
            Else
                Update_Query.Append("PANEL5=null,")
            End If
            If txt_Feedback_Emp.Text <> "" Then
                Update_Query.Append("FBK_EMPLOYER='" & txt_Feedback_Emp.Text & "',")
            Else
                Update_Query.Append("FBK_EMPLOYER=null,")
            End If
            If txt_Feedback_BDC.Text <> "" Then
                Update_Query.Append("FBK_BDC='" & txt_Feedback_BDC.Text & "',")
            Else
                Update_Query.Append("FBK_BDC=null,")
            End If
            Update_Query.Append("FINAL_CAND_STATUS='" & ddl_Fin_status.SelectedValue & "',KEYEDON=sysdate,KEYEDBY='" & Session("lgnagntid") & "' Where FSN='" & FSNO & "' and JOBID='" & ddl_Position.SelectedValue & "'")

            Dim i = clas.ExecuteNonQuery(Update_Query.ToString())
            If i = 1 Then



                Flag = True
                Return Flag
            Else
                Flag = False
                Dim Err_Msg As String = clas.getexception().Replace(vbCrLf, "")
                Err_Msg = Err_Msg.Replace(vbCr, "")
                Err_Msg = Err_Msg.Replace(vbLf, "")
                RadWindowManager1.RadAlert("<ul><li><div style=color:red;>System found some exception:" & Err_Msg & "</div></li></ul>", 300, 100, "Validation Exception Failure", Nothing)

                '  RadWindowManager1.RadAlert("<ul><li><div style=color:red;> Transaction not Done </div></li></ul>", 300, 150, "Validation Success", Nothing)
            End If

            Throw New Exception("UnKnown Exception occured , Please Contact IT Department")
        Catch ex As Exception
            RadWindowManager1.RadAlert("<ul><li><div style=color:green;>" & ex.Message.ToString & "</div></li></ul>", 300, 200, "Exception", Nothing)
            Flag = False
        End Try
        Return Flag
    End Function

    Protected Sub Update_candidateFinal_Status()
      

        Dim Update_Query As StringBuilder = New StringBuilder
        Update_Query.Append("Update AMS.ams_candidate Set status='" & ddl_Fin_status.SelectedValue & "',")
        Update_Query.Append(" PREV_KEYEDON=KEYEDON,PREV_KEYEDBY=KEYEDBY,KEYEDON=sysdate,KEYEDBY='" & Session("lgnagntid") & "'")
        Update_Query.Append(" where FSN='" & FSNO & "' and JOBid='" & ddl_Position.SelectedValue & "' and Cand_Status='A'")

        clas.ExecuteNonQuery(Update_Query.ToString())

    End Sub
    Protected Sub ImgBtn_Refresh_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImgBtn_Refresh.Click
        If ddl_Position.SelectedValue <> "" Then
            Clear()
            Bind_Existing_Page(ddl_Position.SelectedValue)
        End If
    End Sub

    Protected Sub Lst_IntPanel_ItemCheck(sender As Object, e As Telerik.Web.UI.RadListBoxItemEventArgs) Handles Lst_IntPanel.ItemCheck
        Dim Item_Count As Integer = 0
        Item_Count = Lst_IntPanel.CheckedItems.Count
        If Item_Count > 5 Then
            RadWindowManager1.RadAlert("<ul><li><div style=color:green;> Maximum 5 Panel members are allowed. </div></li></ul>", 420, 100, "Exception", Nothing)
            e.Item.Checked = False
        End If
    End Sub

    Protected Sub Clear()
        Interview_Date.SelectedDate = DateTime.Now
        Lst_IntPanel.ClearChecked()
        txt_Feedback_Emp.Text = String.Empty
        txt_Feedback_BDC.Text = String.Empty
        ddl_Fin_status.ClearSelection()
    End Sub
   
    Protected Sub txt_Feedback_BDC_TextChanged(sender As Object, e As System.EventArgs) Handles txt_Feedback_BDC.TextChanged
        txt_Feedback_BDC.Text = txt_Feedback_BDC.Text.Replace(Chr(39), "")
        txt_Feedback_BDC.Text = txt_Feedback_BDC.Text.Replace(Chr(34), "")
        txt_Feedback_BDC.Text = txt_Feedback_BDC.Text.Replace(Chr(92), "")
    End Sub

    Protected Sub txt_Feedback_Emp_TextChanged(sender As Object, e As System.EventArgs) Handles txt_Feedback_Emp.TextChanged
        txt_Feedback_Emp.Text = txt_Feedback_Emp.Text.Replace(Chr(39), "")
        txt_Feedback_Emp.Text = txt_Feedback_Emp.Text.Replace(Chr(34), "")
        txt_Feedback_Emp.Text = txt_Feedback_Emp.Text.Replace(Chr(92), "")
    End Sub
End Class
