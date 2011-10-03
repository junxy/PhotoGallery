<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<PhotoGallery.Core.Tag>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<h1>标记</h1>

<% if (Model.Count() == 1)
   { %>
    <p>有一个标记。</p>
<%} else{ %>
    <p>有 <%= Model.Count() %> 个标记。</p>
<%} %>

<ul class="thumbnails gallery">
    <% foreach (var tag in Model)
       { %>
        <li class="tag">
            <a href="<%= Url.Action("View", new { name = tag.TagName }) %>">
                <img alt="来自 <%= tag.TagName  %> 的图像" src="<%= Url.Action("Thumbnail", new { name = tag.TagName }) %>" class="thumbnail-no-border" />
                <span class="below-image"><%= tag.TagName %></span>
                <span class="image-overlay"><%= tag.TagCount %> 张照片</span>
            </a>
        </li>
    <%} %>
</ul>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
查看所有标记
</asp:Content>

