<%@ page import="user.User" %>
<%@ include file="head.jsp" %>
<%
    Cookie cookies[] = request.getCookies();
    String error = "";
    for (Cookie cookie : cookies)
        if (cookie.getName().equals("logError"))
            error = cookie.getValue();

    Object u = request.getSession().getAttribute("user");
    if (u == null || !(u instanceof User)) {
%>
    <a href="reg.jsp">Sing up</a>
<%
        if (!error.isEmpty()) {%>
<span style="color: red"><%=error%></span>
<%
        }
%>
<form action="/singin" method="get">
    <label for="login">Login:</label><input id="login" name="login" type="text"/><br />
    <label for="pass">Password:</label><input id="pass" name="pass" type="password"/><br />
    <input type="submit" value="Sing In">
</form>
<%  }
    else {
%>
    <h1>Hello, <%=((User) u).getLogin()%></h1>
    <a href="/singin?logout">Logout</a><br />
<%
    }
%>
</body>
</html>