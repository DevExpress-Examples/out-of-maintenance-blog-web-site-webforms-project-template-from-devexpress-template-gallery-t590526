Option Infer On

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports BlogWebSite.Code

    Partial Public Class BlogTimeline
        Inherits System.Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            PrepareDataView()
        End Sub
        Private Sub PrepareDataView()
            Dim year? As Integer = If(Request.Params("year") IsNot Nothing, Integer.Parse(Request.Params("year")), DirectCast(Nothing, Integer?))
            Dim month? As Integer = If(Request.Params("month") IsNot Nothing, Integer.Parse(Request.Params("month")), DirectCast(Nothing, Integer?))
            BlogPostsDataView.DataSource = BlogPostsProvider.GetBlogPosts(year, month)
            BlogPostsDataView.DataBind()
        End Sub
    End Class