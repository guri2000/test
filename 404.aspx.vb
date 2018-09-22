
Partial Class Default2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim mstlbl As Label = Page.Master.FindControl("masterpagelabel")
        'mstlbl.Text = "Error 404"
        Dim abcummb As HtmlAnchor = Page.Master.FindControl("abcummb")
        Dim mstbcmblbl As Label = Page.Master.FindControl("Lblbcumb")
        mstbcmblbl.Text = "Error 404"
        abcummb.Attributes.Add("title", "Error 404")

    End Sub
End Class
