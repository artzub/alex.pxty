/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package ctrl;

import ctrl.exceptions.IllegalOrphanException;
import ctrl.exceptions.NonexistentEntityException;
import entity.TypeDep;
import java.io.Serializable;
import java.math.BigDecimal;
import java.util.List;
import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.Query;
import javax.persistence.EntityNotFoundException;
import javax.persistence.criteria.CriteriaQuery;
import javax.persistence.criteria.Root;
import entity.Dep;
import java.util.ArrayList;
import java.util.Collection;

/**
 *
 * @author alexey
 */
public class TypeDepJpaController implements Serializable {

    public TypeDepJpaController(EntityManagerFactory emf) {
        this.emf = emf;
    }
    private EntityManagerFactory emf = null;

    public EntityManager getEntityManager() {
        return emf.createEntityManager();
    }

    public void create(TypeDep typeDep) {
        if (typeDep.getDepCollection() == null) {
            typeDep.setDepCollection(new ArrayList<Dep>());
        }
        EntityManager em = null;
        try {
            em = getEntityManager();
            em.getTransaction().begin();
            Collection<Dep> attachedDepCollection = new ArrayList<Dep>();
            for (Dep depCollectionDepToAttach : typeDep.getDepCollection()) {
                depCollectionDepToAttach = em.getReference(depCollectionDepToAttach.getClass(), depCollectionDepToAttach.getId());
                attachedDepCollection.add(depCollectionDepToAttach);
            }
            typeDep.setDepCollection(attachedDepCollection);
            em.persist(typeDep);
            for (Dep depCollectionDep : typeDep.getDepCollection()) {
                TypeDep oldIdTypeDepOfDepCollectionDep = depCollectionDep.getIdTypeDep();
                depCollectionDep.setIdTypeDep(typeDep);
                depCollectionDep = em.merge(depCollectionDep);
                if (oldIdTypeDepOfDepCollectionDep != null) {
                    oldIdTypeDepOfDepCollectionDep.getDepCollection().remove(depCollectionDep);
                    oldIdTypeDepOfDepCollectionDep = em.merge(oldIdTypeDepOfDepCollectionDep);
                }
            }
            em.getTransaction().commit();
        } finally {
            if (em != null) {
                em.close();
            }
        }
    }

    public void edit(TypeDep typeDep) throws IllegalOrphanException, NonexistentEntityException, Exception {
        EntityManager em = null;
        try {
            em = getEntityManager();
            em.getTransaction().begin();
            TypeDep persistentTypeDep = em.find(TypeDep.class, typeDep.getId());
            Collection<Dep> depCollectionOld = persistentTypeDep.getDepCollection();
            Collection<Dep> depCollectionNew = typeDep.getDepCollection();
            List<String> illegalOrphanMessages = null;
            for (Dep depCollectionOldDep : depCollectionOld) {
                if (!depCollectionNew.contains(depCollectionOldDep)) {
                    if (illegalOrphanMessages == null) {
                        illegalOrphanMessages = new ArrayList<String>();
                    }
                    illegalOrphanMessages.add("You must retain Dep " + depCollectionOldDep + " since its idTypeDep field is not nullable.");
                }
            }
            if (illegalOrphanMessages != null) {
                throw new IllegalOrphanException(illegalOrphanMessages);
            }
            Collection<Dep> attachedDepCollectionNew = new ArrayList<Dep>();
            for (Dep depCollectionNewDepToAttach : depCollectionNew) {
                depCollectionNewDepToAttach = em.getReference(depCollectionNewDepToAttach.getClass(), depCollectionNewDepToAttach.getId());
                attachedDepCollectionNew.add(depCollectionNewDepToAttach);
            }
            depCollectionNew = attachedDepCollectionNew;
            typeDep.setDepCollection(depCollectionNew);
            typeDep = em.merge(typeDep);
            for (Dep depCollectionNewDep : depCollectionNew) {
                if (!depCollectionOld.contains(depCollectionNewDep)) {
                    TypeDep oldIdTypeDepOfDepCollectionNewDep = depCollectionNewDep.getIdTypeDep();
                    depCollectionNewDep.setIdTypeDep(typeDep);
                    depCollectionNewDep = em.merge(depCollectionNewDep);
                    if (oldIdTypeDepOfDepCollectionNewDep != null && !oldIdTypeDepOfDepCollectionNewDep.equals(typeDep)) {
                        oldIdTypeDepOfDepCollectionNewDep.getDepCollection().remove(depCollectionNewDep);
                        oldIdTypeDepOfDepCollectionNewDep = em.merge(oldIdTypeDepOfDepCollectionNewDep);
                    }
                }
            }
            em.getTransaction().commit();
        } catch (Exception ex) {
            String msg = ex.getLocalizedMessage();
            if (msg == null || msg.length() == 0) {
                BigDecimal id = typeDep.getId();
                if (findTypeDep(id) == null) {
                    throw new NonexistentEntityException("The typeDep with id " + id + " no longer exists.");
                }
            }
            throw ex;
        } finally {
            if (em != null) {
                em.close();
            }
        }
    }

    public void destroy(BigDecimal id) throws IllegalOrphanException, NonexistentEntityException {
        EntityManager em = null;
        try {
            em = getEntityManager();
            em.getTransaction().begin();
            TypeDep typeDep;
            try {
                typeDep = em.getReference(TypeDep.class, id);
                typeDep.getId();
            } catch (EntityNotFoundException enfe) {
                throw new NonexistentEntityException("The typeDep with id " + id + " no longer exists.", enfe);
            }
            List<String> illegalOrphanMessages = null;
            Collection<Dep> depCollectionOrphanCheck = typeDep.getDepCollection();
            for (Dep depCollectionOrphanCheckDep : depCollectionOrphanCheck) {
                if (illegalOrphanMessages == null) {
                    illegalOrphanMessages = new ArrayList<String>();
                }
                illegalOrphanMessages.add("This TypeDep (" + typeDep + ") cannot be destroyed since the Dep " + depCollectionOrphanCheckDep + " in its depCollection field has a non-nullable idTypeDep field.");
            }
            if (illegalOrphanMessages != null) {
                throw new IllegalOrphanException(illegalOrphanMessages);
            }
            em.remove(typeDep);
            em.getTransaction().commit();
        } finally {
            if (em != null) {
                em.close();
            }
        }
    }

    public List<TypeDep> findTypeDepEntities() {
        return findTypeDepEntities(true, -1, -1);
    }

    public List<TypeDep> findTypeDepEntities(int maxResults, int firstResult) {
        return findTypeDepEntities(false, maxResults, firstResult);
    }

    private List<TypeDep> findTypeDepEntities(boolean all, int maxResults, int firstResult) {
        EntityManager em = getEntityManager();
        try {
            CriteriaQuery cq = em.getCriteriaBuilder().createQuery();
            cq.select(cq.from(TypeDep.class));
            Query q = em.createQuery(cq);
            if (!all) {
                q.setMaxResults(maxResults);
                q.setFirstResult(firstResult);
            }
            return q.getResultList();
        } finally {
            em.close();
        }
    }

    public TypeDep findTypeDep(BigDecimal id) {
        EntityManager em = getEntityManager();
        try {
            return em.find(TypeDep.class, id);
        } finally {
            em.close();
        }
    }

    public int getTypeDepCount() {
        EntityManager em = getEntityManager();
        try {
            CriteriaQuery cq = em.getCriteriaBuilder().createQuery();
            Root<TypeDep> rt = cq.from(TypeDep.class);
            cq.select(em.getCriteriaBuilder().count(rt));
            Query q = em.createQuery(cq);
            return ((Long) q.getSingleResult()).intValue();
        } finally {
            em.close();
        }
    }
    
}
