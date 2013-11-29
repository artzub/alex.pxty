<%@ page import="java.util.Map" %>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<html>
    <head>
        <title>Test calc</title>
    </head>
    <body>
        <form action="" method="get">
            <label for="col">Columns:</label><input id="col" name="cols" type="number" value="${param.cols}"/>
            <label for="row">Rows:</label><input id="row" name="rows" type="number" value="${param.rows}"/>
            <input type="submit" value="Calc"/>
        </form>

        <% if(request.getParameter("cols") != null && request.getParameter("rows") != null) {
            int cols = Integer.parseInt(request.getParameter("cols"));
            int rows = Integer.parseInt(request.getParameter("rows"));

            if (cols < 1 || rows < 1)
                return;
        %>
        <table>
        <%  for(int i = 1; i < rows + 1; i++) {%>
            <tr>
        <%      for(int j = 1; j < cols + 1; j++) {%>
                <td><%= (i*j) %></td>
                <%}%>
            </tr>
        <%  }%>
        </table>
        <%}%>
    </body>
</html>