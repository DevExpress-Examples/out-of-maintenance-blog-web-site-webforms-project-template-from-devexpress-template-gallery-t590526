Option Infer On

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports BlogWebSite.Code

    Partial Public Class BlogPost
        Inherits System.Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)

            Dim id_Renamed? As Integer = GetBlogPostId()
            Dim blogPost = BlogPostsProvider.GetBlogPost(id_Renamed)
            If blogPost Is Nothing Then
                Response.Redirect("BlogTimeline.aspx")
            End If
            Subject.InnerText = blogPost.Subject
            Page.Title = Subject.InnerText
            [Date].Text = blogPost.Date.ToString("ddd, MMM dd yyyy")
            Body.InnerText = blogPost.Body
            BlogImage.Src = blogPost.ImageUrl
        End Sub
        Private Function GetBlogPostId() As Integer?

            Dim id_Renamed As Integer = Nothing
            Dim strId = Request.Params("id")
            Return If(Integer.TryParse(strId, id_Renamed), id_Renamed, DirectCast(Nothing, Integer?))
        End Function
    End Class