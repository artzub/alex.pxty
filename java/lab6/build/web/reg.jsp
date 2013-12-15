<%@ include file="head.jsp" %>
<%
    Cookie cookies[] = request.getCookies();
    String error = ""; if(cookies != null) {
    for (Cookie cookie : cookies)
        if (cookie.getName().equals("regError"))
            error = cookie.getValue(); }
    if (!error.isEmpty()) {%>
    <span style="color: red"><%=error%></span>
<%
    }
%>
<form action="singup" method="get">
    <label for="login">Login:</label><input id="login" name="login" type="text"/><br />
    <label for="pass">Password:</label><input id="pass" name="pass" type="password"/><br />
    <input type="submit" value="Sing Up">
</form>
</body>
</html>