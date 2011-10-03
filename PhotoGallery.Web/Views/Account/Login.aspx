<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PhotoGallery.Web.ViewModels.LoginViewModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Title" runat="server">
    登录
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>
        登录</h1>
    <p>
        请在下面输入您的电子邮件和密码。如果您没有帐户， 请<a href="<%= Url.Action("Register")%>">注册</a>。
    </p>
    <%= Html.ValidationSummary(true) %>
    <% using (Html.BeginForm())
       { %>
    <fieldset class="no-legend">
        <legend>登录</legend>
        <ol>
            <li class="email">
                <%= Html.LabelFor(m => m.Email) %>
                <%= Html.TextBoxFor(m => m.Email, new { title = "电子邮件地址", placeholder = "Email address" })%>
                <%= Html.ValidationMessageFor(m => m.Email) %>
            </li>
            <li class="Password">
                <%= Html.LabelFor(m => m.Password) %>
                <%= Html.PasswordFor(m => m.Password, new { title = "密码" })%>
                <%= Html.ValidationMessageFor(m => m.Password)%>
            </li>
            <li class="remember-me">
                <%= Html.LabelFor(m => m.RememberMe) %>
                <%= Html.CheckBoxFor(m => m.RememberMe, new { title = "记住我", value = true })%>
            </li>
        </ol>
        <p class="form-actions">
            <input type="submit" value="登录" title="登录" />
        </p>
    </fieldset>
    <%} %>
</asp:Content>