Option Infer On

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports BlogWebSite.Code

    Partial Public Class BlogNavigation
        Inherits System.Web.UI.UserControl

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            PrepareNavBar()
        End Sub
        Private Sub PrepareNavBar()
            Dim blogTimelineUrl = "~/Pages/BlogTimeline.aspx"
            BlogCategoriesNavBar.Groups.Add("Archive (All)").NavigateUrl = blogTimelineUrl
            For Each yearsGroup In BlogPostsProvider.GetBlogsByCategories()
                Dim navBarGroup = BlogCategoriesNavBar.Groups.Add(String.Format("{0} ({1})", yearsGroup.Key.ToString(), yearsGroup.Value.Sum(Function(b) b.Value.Count())))
                navBarGroup.NavigateUrl = String.Format("{0}?year={1}", blogTimelineUrl, yearsGroup.Key)
                For Each monthGroup In yearsGroup.Value
                    Dim blogs = monthGroup.Value
                    Dim navBarItem = navBarGroup.Items.Add()
                    Dim month = blogs.First().Date.ToString("MMMM")
                    navBarItem.Text = String.Format("{0} ({1})", month, blogs.Count())
                    navBarItem.NavigateUrl = String.Format("{0}?year={1}&month={2}", blogTimelineUrl, yearsGroup.Key, monthGroup.Key)
                Next monthGroup
            Next yearsGroup
        End Sub
    End Class