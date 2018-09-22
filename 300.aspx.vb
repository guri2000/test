
Partial Class Default2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim mstlbl As Label = Page.Master.FindControl("masterpagelabel")
        'mstlbl.Text = "Comming Soon"
        Dim abcummb As HtmlAnchor = Page.Master.FindControl("abcummb")
        Dim mstbcmblbl As Label = Page.Master.FindControl("Lblbcumb")
        mstbcmblbl.Text = "Comming Soon"
        abcummb.Attributes.Add("title", "Comming Soon")

    End Sub
End Class
