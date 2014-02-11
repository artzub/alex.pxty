/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package console;


import java.io.*;
import java.sql.*;
import java.util.HashMap;
import java.util.Locale;

public class Console {
    static final String CONNECTED = "connected: %s%n";
    
    static final String[] tables = new String[] {
        "Part", 
        "Alloy",
        "Surface",
        "Type_Dep",
        "Departament",
        "Stage"
    };
    
    static PrintStream cout;

    public static void main(String[] args) {
        
        String drv = "oracle.jdbc.OracleDriver";
        String server = "localhost";
        String port = "1521";
        String sid = "xe";
        String url = "jdbc:oracle:thin:@"+ server + ":" + port + ":" + sid;
        String login = "parts"; 
        String password = "Lighting";
        int cmd = 0;
        int last = -1;
        String temp = "";
        String next = "";
        
        int i = 0;
        
        cout = System.out;
        
        try {
            Class.forName(drv);
            Locale.setDefault(Locale.ENGLISH);
            Connection conn = DriverManager.getConnection(url, login, password);
            
            cout.printf(CONNECTED, url);
            
            while(true) {                
                next = printMenu(cmd, true);
                
                if (next.equals("0")) {
                    cmd = last;
                    if (last < 0)
                        break;
                    if (last == 0)
                        last = -1;
                    continue;
                }

                switch(cmd) {
                    case 0:
                        last = cmd;
                        cmd = Integer.parseInt(next);
                        break;
                    case 1:
                        try {
                            i = Integer.parseInt(next);
                        } catch (Exception e) {
                            continue;
                        }
                        if (i > 0 && i < tables.length) {
                            cout.println(tables[i - 1]);
                            printSelectResult(selectData(conn, tables[i - 1]));
                        }
                        break;
                    case 2:
                        try {
                            i = Integer.parseInt(next);
                        } catch (Exception e) {
                            continue;
                        }
                        last = cmd;
                        cmd = 20;
                        break;
                    case 20:
                        cmd = 2;
                        last = 0;
                        if (i > 0 && i < tables.length) {
                            cout.println(tables[i - 1]);
                            printSelectResult(selectData(conn, tables[i - 1], next));
                        }
                        break;
                }
            }
        } catch (Exception e) {
            System.out.println(e.getMessage());
        }
    }
    
    private static String printMenu(int type, boolean clear) throws IOException {
        if (clear)
            cout.println("\f");
        
        boolean end = true;
        
        switch(type) {
            case 0:
                cout.println("1: Select data from tables...");
                cout.println("2: Select data from tables by id...");
                cout.println("3: Add data into tables...");
                cout.println("4: Edit data into tables...");
                cout.println("5: Remove data from tables...");
                cout.println("6: Remove data from tables by id...");
                break;
            case 2:
            case 1:
                for (int i = 0; i < tables.length; i++)
                    cout.println(String.format("%d: %s", i + 1, tables[i]));
                break;
            case 20:
                cout.print("Enter id record");
                end = false;
                break;
                
        }
        
        if(end) {
            cout.println("——————");
            cout.println("0: To back or exit...");
        }
        
        return readLine("> ");
    }    
    
    private static String readLine(String format, Object... args) throws IOException {
        String res;
        if (System.console() != null) {
            res = System.console().readLine(format, args);
        }
        else {
            cout.printf(format, args);
            BufferedReader reader = new BufferedReader(new InputStreamReader(System.in));
            res = reader.readLine();
        }
        return res; 
    }
    
    private static ResultSet selectData(Connection conn, String table) throws SQLException{
        return conn.createStatement().executeQuery(String.format("Select * from %s", table));
    }
    
    private static ResultSet selectData(Connection conn, String table, String id) throws SQLException{
        return conn.createStatement().executeQuery(String.format("Select * from %s where id=%s", table, id));
    }
    
    private static void printSelectResult(ResultSet res) throws SQLException {
        if (res == null)
            return;
        
        boolean initHeader = true;
        int fullRowSize;
        int colCount = 0;
        int curSize = 0;
        String curStr;
        String rowDelimiter = "+";
        HashMap<Integer, Integer> cols = new HashMap();
        
        while(res.next()) {
            if (initHeader) {
                ResultSetMetaData meta = res.getMetaData();       
                cout.print("|");
                colCount = meta.getColumnCount();
                for (int i = 1; i <= colCount; i++) {                    
                    curStr = meta.getColumnLabel(i);
                    curSize = meta.getColumnDisplaySize(i);
                    
                    cols.put(i, curSize);
                    curStr = fillSpace(curStr, curSize);
                    
                    rowDelimiter += fillChar(curStr.length(), '-') + "+";
                    cout.print(curStr + "|");
                }
                cout.print("\n");
                cout.println(rowDelimiter);
                initHeader = false;
            }            
            
            cout.print("|");
            for (int i = 1; i <= colCount; i++) {                
                curStr = fillSpace(res.getString(i), cols.get(i));
                cout.print(curStr + "|");
            }
            cout.print("\n");
        }
        cout.println(rowDelimiter);
    }
    
    private static String fillSpace(String name, int displaySize) {
        String result = " " + name;
        if (result.length() < displaySize) {
            result += fillChar(displaySize - result.length(), ' ');
        }
        else
            result = result.substring(0, displaySize);
        return result; 
    }
    
    private static String fillChar(int length, char sin) {
        String res = "";
        for (int i = 0; i < length; i++) {
            res += sin;
        }
        return res;
    }
}

