package util;

import ctrl.AlloyJpaController;
import javax.persistence.EntityManagerFactory;

public class DBM {
    
    private static AlloyJpaController ajc;

    public static AlloyJpaController getAjc() {
        if (emf == null)
            throw new NullPointerException("EntityManagerFactory isn't initialized. Used InitializeDBM method");
        
        if (ajc == null)
            ajc = new AlloyJpaController(emf);
        
        return ajc;
    }

    public static EntityManagerFactory getEmf() {
        return emf;
    }
    
    private static EntityManagerFactory emf;
    
    public static void InitializeDBM(EntityManagerFactory emf) {
        DBM.emf = emf;
    }
    
        
}
