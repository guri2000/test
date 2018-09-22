Imports System.Data
Imports System.IO
Imports Telerik.Web.UI
Imports System.Text
Imports System.Web.Configuration
Partial Class MasterPage
    Inherits System.Web.UI.MasterPage
    Dim clas As rahulcpm.commonclass = New rahulcpm.commonclass()
    Dim qry As String
    Dim dt As DataTable
    Dim sydate As String
    Dim empid As Int32 = 0
    Dim locid As Int32 = 0
    Dim msg As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("lgnagntid") = "0" Or Session("lgnagntid") = "" Then
		 ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "RedirectToLogin", "RedirectToLogin()", True)
            'Response.Redirect("login.aspx")
           ' Response.Redirect("logout.aspx")
        End If
      
        If Page.IsPostBack = False Then
            lbllgnnam.Text = Session("lgnagntnam")
            getmastmenu()
            ' ClientScript.RegisterStartupScript(Me.GetType(), "alert", "Setgrid();", True)
            ' ScriptManager.RegisterStartupScript(Me, Page.GetType(), "SessionAlert", "Setgrid();", True)
        End If
        If Session("mode") = "edit" Then
            Session.Remove("mode")
        End If
        bingsession()
        If Request.QueryString("mid") <> "" Then
            bindlabels(Request.QueryString("mid"), "mm")
        ElseIf Request.QueryString("sid") <> "" Then
            bindlabels(Request.QueryString("sid"), "sm")
        Else

            masterpagelabel.Text = ""
            Lblbcumb.Text = ""
            abcummb.Attributes.Add("title", "")
            abcummb.Visible = False
        End If


    End Sub
   
    Private Sub getmastmenu()
        qry = "select menuid,menunam,menuicncls,menulink,(select count(*) from ams.ams_submenu where submenusts='A' and submenumenuid=menuid ) as cnt from ams.ams_menu  where active='A' order by seq asc"
        dt = clas.getdata(qry, "QR")
        If dt.Rows.Count > 0 Then
            'Dim dccol11 = New DataColumn("Action", GetType(String))
            'dt.Columns.Add(dccol11)
            Dim stmenu As StringBuilder = New StringBuilder()
            stmenu.Append("<div id='sidebar'><a href='index.aspx' class='visible-phone'><i class='icon icon-home'></i> Dashboard</a>")
            stmenu.Append("<ul>")

            For i = 0 To dt.Rows.Count - 1
                If dt.Rows(i)("cnt") > 0 Then
                    stmenu.Append("<li class='submenu'> <a href='" & dt.Rows(i)("menulink") & "?mid=" & dt.Rows(i)("menuid") & "'><i class='" & dt.Rows(i)("menuicncls") & "'></i> <span>" & dt.Rows(i)("menunam") & "</span></a> ")
                Else
                    stmenu.Append("<li> <a href='" & dt.Rows(i)("menulink") & "?mid=" & dt.Rows(i)("menuid") & "'><i class='" & dt.Rows(i)("menuicncls") & "'></i> <span>" & dt.Rows(i)("menunam") & "</span></a> ")

                End If
                qry = "select * from ams.ams_submenu where submenusts='A' and submenumenuid=" & dt.Rows(i)("menuid") & " order by submenuseq asc"
                Dim dt1 As DataTable = clas.getdata(qry, "QR")
                If dt1.Rows.Count > 0 Then
                    stmenu.Append("<ul>")
                    For j = 0 To dt1.Rows.Count - 1
                        If dt.Rows(i)("menuid") = "1" Then

                            If Session("lgnroll") = "S" Then
                                stmenu.Append("<li> <a href='" & dt1.Rows(j)("submenulink") & "?sid=" & dt1.Rows(j)("submenuid") & "' title='" & dt1.Rows(j)("submenutooltip") & "' ><i class='" & dt1.Rows(j)("submenuicncls") & "'></i>  <span>" & dt1.Rows(j)("submenunam") & "</span></a></li> ")

                            Else
                                stmenu.Append("<li> <a href='#accessdenied' id='acs-event'  data-toggle='modal' title='" & dt1.Rows(j)("submenutooltip") & "' ><i class='" & dt1.Rows(j)("submenuicncls") & "'></i>  <span>" & dt1.Rows(j)("submenunam") & "</span></a></li> ")


                            End If
                        Else
                            If dt1.Rows(j)("submenuid") = "13" Or dt1.Rows(j)("submenuid") = "14" Then
                                If Session("lgnroll") = "S" Then
                                    stmenu.Append("<li> <a href='" & dt1.Rows(j)("submenulink") & "?sid=" & dt1.Rows(j)("submenuid") & "' title='" & dt1.Rows(j)("submenutooltip") & "' ><i class='" & dt1.Rows(j)("submenuicncls") & "'></i>  <span>" & dt1.Rows(j)("submenunam") & "</span></a></li> ")
                                Else
                                    stmenu.Append("<li> <a href='#accessdenied' id='acs-event'  data-toggle='modal' title='" & dt1.Rows(j)("submenutooltip") & "' ><i class='" & dt1.Rows(j)("submenuicncls") & "'></i>  <span>" & dt1.Rows(j)("submenunam") & "</span></a></li> ")
                                End If
                            Else
                                stmenu.Append("<li> <a href='" & dt1.Rows(j)("submenulink") & "?sid=" & dt1.Rows(j)("submenuid") & "' title='" & dt1.Rows(j)("submenutooltip") & "' ><i class='" & dt1.Rows(j)("submenuicncls") & "'></i>  <span>" & dt1.Rows(j)("submenunam") & "</span></a></li> ")
                            End If


                        End If
                    Next
                    stmenu.Append("</ul>")
                End If
                stmenu.Append("</li>")
            Next
            stmenu.Append("<li>")

            stmenu.Append("<div style='bottom:0;  margin-bottom: 2px; margin-left: 15px;'><img src='img/acumen-logo-banner.png' alt='' border='0'></div>")

            stmenu.Append("</li>")

            stmenu.Append("<li>")

            'Change code 18Sept
            stmenu.Append("<div style='bottom:0;  margin-bottom: 2px; margin-left: 15px;'> <a id='add-event'  data-toggle='modal' class='openIDialog' href='#ChangePwd' data-id='Complaint.aspx' title='Complaint Cell' ><img src='images/Compaint_cell_img.png' alt='' border='0'></a></div>")

            stmenu.Append("</li>")

            stmenu.Append("</ul>")
            stmenu.Append("</div>")
            'mainmenu.DataSource = dt
            'mainmenu.DataBind()
            Lblmenu.Text = stmenu.ToString()

        End If

    End Sub

    Protected Sub bindlabels(ByVal mid As Int32, ByVal st As String)
        If st = "mm" Then
            qry = "select menunam  from ams.ams_menu where menuid='" & mid & "'"
        ElseIf st = "sm" Then
            qry = "select submenunam  from ams.ams_submenu where submenuid='" & mid & "'"

        End If

        dt = clas.getdata(qry, "QR")
        If dt.Rows.Count > 0 Then
            masterpagelabel.Text = dt.Rows(0)(0)
            Lblbcumb.Text = dt.Rows(0)(0)
            abcummb.Attributes.Add("title", dt.Rows(0)(0) & " Screen")
            abcummb.Visible = True
        Else
            masterpagelabel.Text = ""
            Lblbcumb.Text = ""
            abcummb.Attributes.Add("title", "")
            abcummb.Visible = False
        End If
    End Sub

    Protected Sub bingsession()
        Dim str As StringBuilder = New StringBuilder()

       

       
        str.Append("Login Name: " & Session("lgnnam") & "<br/>")
        str.Append("Password: " & Session("lgnpwd") & "<br/>")
        str.Append("Roll: " & Session("lgnroll") & "<br/>")
        str.Append("User Name: " & Session("lgnagntnam") & "<br/>")
        str.Append("Agent ID: " & Session("lgnagntid") & "<br/>")
        str.Append("User IP: " & Session("lgnip") & "<br/>")
        str.Append("Account Status: " & Session("lgnsts") & "<br/>")
        str.Append("Email: " & Session("lgneml") & "<br/>")
        str.Append("Mobile: " & Session("lgnmbl") & "<br/>")
       
        HFSession.Value = Session("lgnagntid")
        HFIP.Value = Session("lgnip")
        lblsess.Text = str.ToString()


    End Sub
 

End Class

