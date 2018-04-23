Option Infer On

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports BlogWebSite.Code

    Partial Public Class Search
        Inherits System.Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            DirectCast(Page.Master, Layout).ShowSearch = False
            Dim query = TryCast(Session("query"), String)
            If Not String.IsNullOrEmpty(query) Then
                Session("query") = Nothing
                SearchGridView.SearchPanelFilter = query
            End If
            SearchGridView.DataSource = BlogPostsProvider.GetBlogPosts()
            SearchGridView.DataBind()
        End Sub
    End Class