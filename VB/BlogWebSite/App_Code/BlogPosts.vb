Option Infer On

Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Web
Imports System.Xml.Serialization

Namespace BlogWebSite.Code
    <XmlRoot("BlogPosts")> _
    Public Class BlogPostsProvider
        <XmlElement("BlogPost")> _
        Public Property BlogPosts() As List(Of BlogPost)


        Private Shared current_Renamed As BlogPostsProvider
        Private Shared ReadOnly Property Current() As BlogPostsProvider
            Get
                If current_Renamed Is Nothing Then
                    Dim filePath As String = HttpContext.Current.Server.MapPath("~/App_Data/BlogPosts.xml")
                    Using reader As New StreamReader(filePath)
                        Dim serializer As New XmlSerializer(GetType(BlogPostsProvider))
                        current_Renamed = CType(serializer.Deserialize(reader), BlogPostsProvider)
                    End Using
                End If
                Return current_Renamed
            End Get
        End Property
        Public Shared Function GetBlogPost(ByVal id? As Integer) As BlogPost
            If Not id.HasValue Then
                Return Nothing
            End If
            Return Current.BlogPosts.FirstOrDefault(Function(p) id.Equals(p.Id))
        End Function

        Public Shared Function GetBlogPosts() As List(Of BlogPost)
            Return GetBlogPosts(Nothing, Nothing)
        End Function
        Public Shared Function GetBlogPosts(ByVal year? As Integer, ByVal month? As Integer) As List(Of BlogPost)

            Dim blogPosts_Renamed As List(Of BlogPost) = Current.BlogPosts
            If blogPosts_Renamed Is Nothing Then
                Return Nothing
            End If

            If year.HasValue Then
                blogPosts_Renamed = blogPosts_Renamed.FindAll(Function(p) year.Equals(p.Date.Year))
            End If
            If month.HasValue Then
                blogPosts_Renamed = blogPosts_Renamed.FindAll(Function(p) month.Equals(p.Date.Month))
            End If
            Return blogPosts_Renamed
        End Function
        Public Shared Function GetBlogsByCategories() As Dictionary(Of Integer, Dictionary(Of Integer, IEnumerable(Of BlogPost)))
            Dim result = New Dictionary(Of Integer, Dictionary(Of Integer, IEnumerable(Of BlogPost)))()
            Dim yearsGroup = Current.BlogPosts.GroupBy(Function(p) p.Date.Year).OrderByDescending(Function(g) g.Key)
            For Each yearGroup In yearsGroup
                result(yearGroup.Key) = New Dictionary(Of Integer, IEnumerable(Of BlogPost))()
                Dim monthGroups = yearGroup.GroupBy(Function(p) p.Date.Month).OrderByDescending(Function(g) g.Key)
                For Each monthGroup In monthGroups
                    result(yearGroup.Key)(monthGroup.Key) = monthGroup.ToList()
                Next monthGroup
            Next yearGroup
            Return result
        End Function
    End Class

    Public Class BlogPost
        Public Property Id() As Integer
        Public Property Category() As String
        Public Property Subject() As String
        Public Property Body() As String
        Public Property ImageUrl() As String

        <XmlIgnore> _
        Public Property [Date]() As Date
        Public Property DateString() As String
            Get
                Return Me.Date.ToString("yyyy-MM-dd HH:mm:ss")
            End Get
            Set(ByVal value As String)
                Me.Date = Date.Parse(value)
            End Set
        End Property
    End Class
End Namespace