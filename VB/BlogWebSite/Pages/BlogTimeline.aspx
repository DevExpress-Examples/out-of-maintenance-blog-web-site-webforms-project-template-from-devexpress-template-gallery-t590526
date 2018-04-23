<%@ Page Title="Blog" Language="vb" AutoEventWireup="true" MasterPageFile="~/Layout.master" CodeFile="BlogTimeline.aspx.vb" Inherits="BlogTimeline" %>
<%@ Register Src="~/UserControls/BlogNavigation.ascx" TagPrefix="dx" TagName="BlogNavigation" %>
<script runat=server>
Protected Function GetPreviewText() As String
    Dim previewTextLength = 335
    Dim blogBody = Eval("Body").ToString()
    Return If(blogBody.Length > previewTextLength, blogBody.Substring(0, previewTextLength), blogBody)
End Function
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
<div class="container">
    <div class="row">
        <div class="col-md-9">
            <div class="page-header">
                <h1>Blog</h1>
            </div>
            <dx:ASPxDataView ID="BlogPostsDataView" runat="server" Width="100%" CssClass="dataViewPosts" PagerAlign="Left">
                <ContentStyle Paddings-Padding="0px" Border-BorderWidth="0px" />
                <ItemStyle Width="100%" Paddings-Padding="0px" />
                <SettingsTableLayout ColumnCount="1" RowsPerPage="3" />
                <PagerSettings Position="Bottom" SEOFriendly="Enabled" EnableAdaptivity="True" />
                <ItemTemplate>
                    <section class="row">
                        <div class="col-sm-5"><a href="BlogPost.aspx?id=<%#Eval("ID")%>"><img class="blogImage" src='<%#Page.ResolveClientUrl(Eval("ImageUrl").ToString())%>' alt=""></a></div>
                        <article class="col-sm-7">
                            <h3><a href="BlogPost.aspx?id=<%#Eval("ID")%>"><%#Eval("Subject")%></a></h3>
                            <p><%#GetPreviewText()%>...</p>
                            <a href="BlogPost.aspx?id=<%#Eval("ID")%>"><big>Read more</big></a>
                        </article>
                    </section>
                    <hr />
                </ItemTemplate>
            </dx:ASPxDataView>
        </div>                
        <nav class="col-md-3">
            <dx:BlogNavigation runat="server"/>
        </nav>
    </div>
</div>
</asp:Content>