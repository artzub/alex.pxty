package user;

import java.util.HashMap;
import java.util.Map;

public class Users {
    private Map<String, User> users;
    private static Users instance;

    private Users() {
        users = new HashMap<String, User>();
    }

    public static Users GetInstance() {
        if (instance == null)
            instance = new Users();
        return instance;
    }

    public User add(User user) {
        if (user == null)
            return null;
        users.put(user.getLogin(), user);
        return user;
    }

    public User getUser(String login) {
        return users.get(login.toLowerCase());
    }
}
