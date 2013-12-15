package user;

import javax.servlet.ServletException;
import javax.servlet.http.*;
import java.io.IOException;

public class RegServlet extends HttpServlet {
    @Override
    protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        Users users = Users.GetInstance();

        String login = req.getParameter("login");
        String pass = req.getParameter("pass");

        if (login == null || pass == null || login.isEmpty() || pass.isEmpty()) {
            Cookie c = new Cookie("regError", "Pass or Login are null");
            c.setHttpOnly(true);
            c.setMaxAge(1);
            resp.addCookie(c);
            resp.sendRedirect("reg.jsp");
            return;
        }

        login = login.trim();
        User user = users.getUser(login);

        if (user != null) {
            Cookie c = new Cookie("regError", "User already exists");
            c.setHttpOnly(true);
            c.setMaxAge(1);
            resp.addCookie(c);
            resp.sendRedirect("reg.jsp");
            return;
        }

        users.add(new User(login,pass));

        resp.sendRedirect("index.jsp");
    }
}

