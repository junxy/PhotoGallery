<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PhotoGallery.Core.User>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        编辑配置文件</h1>
    <p>
        <%= Html.Encode(Model.DisplayName) %>，欢迎回来。您可以使用下面的表单来更改您的显示名称(显示在注释中) 
        和个人简介(显示在<a href="/User/View/#">公共配置文件</a>上)。
    </p>
    <% using (Html.BeginForm())
       {%>
    <fieldset class="no-legend">
        <legend>编辑配置文件</legend>
        <ol>
            <li>
                <%= Html.LabelFor(model => model.DisplayName) %>
                <%= Html.TextBoxFor(model => model.DisplayName, new { title = "更改显示名称", placeholder = "Display name" })%>
            </li>
            <li>
                <%= Html.LabelFor(model => model.Bio) %>
                <%= Html.TextAreaFor(model => model.Bio, new { title = "更改个人简介", rows = "4", cols = "50", placeholder = "Biography" })%>
            </li>
        </ol>
        <p class="form-actions">
            <input type="submit" value="保存" title="保存更改" />
        </p>
    </fieldset>
    <% } %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
    编辑配置文件 -
    <%= Model.DisplayName %>
</asp:Content>