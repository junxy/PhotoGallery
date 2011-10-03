<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PhotoGallery.Web.ViewModels.RegisterViewModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
    注册
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        帐户创建</h1>
    <p>
        使用以下表单创建新帐户。
    </p>

    <%= Html.ValidationSummary(true) %>
    
    <% using (Html.BeginForm())
       {%>
    <fieldset class="no-legend">
        <legend>帐户创建</legend>
        <ol>
            <li class="email">
                <%= Html.LabelFor(model => model.Email) %>
                <%= Html.TextBoxFor(model => model.Email, new { title = "电子邮件地址", placeholder = "Email address" })%>
                <%= Html.ValidationMessageFor(model => model.Email) %>
            </li>
            <li class="password">
                <%= Html.LabelFor(model => model.Password) %>
                <%= Html.EditorFor(model => model.Password)%>
                <%= Html.ValidationMessageFor(model => model.Password) %>
            </li>
            <li class="confirm-password">
                <%= Html.LabelFor(model => model.ConfirmPassword) %>
                <%= Html.EditorFor(model => model.ConfirmPassword)%>
                <%= Html.ValidationMessageFor(model => model.ConfirmPassword) %>
            </li>
            <li class="recaptcha">
            
            </li>
        </ol>
        <p class="form-actions">
            <input type="submit" value="注册" title="注册" />
        </p>
    </fieldset>
    <% } %>
</asp:Content>