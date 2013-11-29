package user;

import java.util.HashMap;
import java.util.Map;
import javax.servlet.http.HttpSession;

public class User {
    private String login;
    private String pass;

    private Map<String, HttpSession> map;

    public String getLogin() {
        return login;
    }

    public boolean equalsPass(String pass) {
        return this.pass.equals(pass);
    }

    public User(String login, String pass) {
        this.login = login.toLowerCase();
        this.pass = pass;
    }

    public HttpSession addSession(HttpSession session) {
        if(session == null)
            return session;

        if (map == null)
            map = new HashMap<String, HttpSession>();

        map.put(session.getId(), session);
        return session;
    }

    public HttpSession getSession(String id) {
        return map == null ? null : map.get(id);
    }
}
