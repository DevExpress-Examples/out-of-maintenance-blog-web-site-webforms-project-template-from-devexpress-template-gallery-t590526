Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DevExpress.Web

    Partial Public Class Layout
        Inherits System.Web.UI.MasterPage


        Private showSearch_Renamed As Boolean = True
        Public Property ShowSearch() As Boolean
            Get
                Return showSearch_Renamed
            End Get
            Set(ByVal value As Boolean)
                showSearch_Renamed = value
            End Set
        End Property

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            Page.Header.DataBind()
            SearchBlock.Visible = ShowSearch
            If IsPostBack AndAlso hfAction.Contains("search") Then
                Session("query") = Search.Text
                Response.Redirect("~/Pages/Search.aspx")
            End If
        End Sub
    End Class