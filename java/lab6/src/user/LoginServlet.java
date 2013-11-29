package user;

import javax.servlet.ServletException;
import javax.servlet.http.*;
import java.io.IOException;

public class LoginServlet extends HttpServlet {
    @Override
    protected void doGet(HttpServletRequest req, HttpServletResponse resp) throws ServletException, IOException {
        req.getSession().removeAttribute("user");

        if (req.getParameter("logout") == null) {
            Users users = Users.GetInstance();

            User user = users.getUser(req.getParameter("login"));

            if (user == null) {
                Cookie c = new Cookie("logError", "User not exists");
                c.setHttpOnly(true);
                c.setMaxAge(1);
                resp.addCookie(c);
            }
            else if (!user.equalsPass(req.getParameter("pass"))) {
                Cookie c = new Cookie("logError", "Pass is not correct");
                c.setHttpOnly(true);
                c.setMaxAge(1);
                resp.addCookie(c);
            }
            else {
                HttpSession session = user.getSession(req.getSession().getId());
                if (session == null)
                    session = user.addSession(req.getSession());
                session.setAttribute("user", user);
            }
        }

        resp.sendRedirect("index.jsp");
    }
}