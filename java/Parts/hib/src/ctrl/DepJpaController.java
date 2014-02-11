/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package ctrl;

import ctrl.exceptions.IllegalOrphanException;
import ctrl.exceptions.NonexistentEntityException;
import entity.Dep;
import java.io.Serializable;
import java.math.BigDecimal;
import java.util.List;
import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.Query;
import javax.persistence.EntityNotFoundException;
import javax.persistence.criteria.CriteriaQuery;
import javax.persistence.criteria.Root;
import entity.TypeDep;
import entity.Stage;
import java.util.ArrayList;
import java.util.Collection;

/**
 *
 * @author alexey
 */
public class DepJpaController implements Serializable {

    public DepJpaController(EntityManagerFactory emf) {
        this.emf = emf;
    }
    private EntityManagerFactory emf = null;

    public EntityManager getEntityManager() {
        return emf.createEntityManager();
    }

    public void create(Dep dep) {
        if (dep.getStageCollection() == null) {
            dep.setStageCollection(new ArrayList<Stage>());
        }
        EntityManager em = null;
        try {
            em = getEntityManager();
            em.getTransaction().begin();
            TypeDep idTypeDep = dep.getIdTypeDep();
            if (idTypeDep != null) {
                idTypeDep = em.getReference(idTypeDep.getClass(), idTypeDep.getId());
                dep.setIdTypeDep(idTypeDep);
            }
            Collection<Stage> attachedStageCollection = new ArrayList<Stage>();
            for (Stage stageCollectionStageToAttach : dep.getStageCollection()) {
                stageCollectionStageToAttach = em.getReference(stageCollectionStageToAttach.getClass(), stageCollectionStageToAttach.getId());
                attachedStageCollection.add(stageCollectionStageToAttach);
            }
            dep.setStageCollection(attachedStageCollection);
            em.persist(dep);
            if (idTypeDep != null) {
                idTypeDep.getDepCollection().add(dep);
                idTypeDep = em.merge(idTypeDep);
            }
            for (Stage stageCollectionStage : dep.getStageCollection()) {
                Dep oldIdDepOfStageCollectionStage = stageCollectionStage.getIdDep();
                stageCollectionStage.setIdDep(dep);
                stageCollectionStage = em.merge(stageCollectionStage);
                if (oldIdDepOfStageCollectionStage != null) {
                    oldIdDepOfStageCollectionStage.getStageCollection().remove(stageCollectionStage);
                    oldIdDepOfStageCollectionStage = em.merge(oldIdDepOfStageCollectionStage);
                }
            }
            em.getTransaction().commit();
        } finally {
            if (em != null) {
                em.close();
            }
        }
    }

    public void edit(Dep dep) throws IllegalOrphanException, NonexistentEntityException, Exception {
        EntityManager em = null;
        try {
            em = getEntityManager();
            em.getTransaction().begin();
            Dep persistentDep = em.find(Dep.class, dep.getId());
            TypeDep idTypeDepOld = persistentDep.getIdTypeDep();
            TypeDep idTypeDepNew = dep.getIdTypeDep();
            Collection<Stage> stageCollectionOld = persistentDep.getStageCollection();
            Collection<Stage> stageCollectionNew = dep.getStageCollection();
            List<String> illegalOrphanMessages = null;
            for (Stage stageCollectionOldStage : stageCollectionOld) {
                if (!stageCollectionNew.contains(stageCollectionOldStage)) {
                    if (illegalOrphanMessages == null) {
                        illegalOrphanMessages = new ArrayList<String>();
                    }
                    illegalOrphanMessages.add("You must retain Stage " + stageCollectionOldStage + " since its idDep field is not nullable.");
                }
            }
            if (illegalOrphanMessages != null) {
                throw new IllegalOrphanException(illegalOrphanMessages);
            }
            if (idTypeDepNew != null) {
                idTypeDepNew = em.getReference(idTypeDepNew.getClass(), idTypeDepNew.getId());
                dep.setIdTypeDep(idTypeDepNew);
            }
            Collection<Stage> attachedStageCollectionNew = new ArrayList<Stage>();
            for (Stage stageCollectionNewStageToAttach : stageCollectionNew) {
                stageCollectionNewStageToAttach = em.getReference(stageCollectionNewStageToAttach.getClass(), stageCollectionNewStageToAttach.getId());
                attachedStageCollectionNew.add(stageCollectionNewStageToAttach);
            }
            stageCollectionNew = attachedStageCollectionNew;
            dep.setStageCollection(stageCollectionNew);
            dep = em.merge(dep);
            if (idTypeDepOld != null && !idTypeDepOld.equals(idTypeDepNew)) {
                idTypeDepOld.getDepCollection().remove(dep);
                idTypeDepOld = em.merge(idTypeDepOld);
            }
            if (idTypeDepNew != null && !idTypeDepNew.equals(idTypeDepOld)) {
                idTypeDepNew.getDepCollection().add(dep);
                idTypeDepNew = em.merge(idTypeDepNew);
            }
            for (Stage stageCollectionNewStage : stageCollectionNew) {
                if (!stageCollectionOld.contains(stageCollectionNewStage)) {
                    Dep oldIdDepOfStageCollectionNewStage = stageCollectionNewStage.getIdDep();
                    stageCollectionNewStage.setIdDep(dep);
                    stageCollectionNewStage = em.merge(stageCollectionNewStage);
                    if (oldIdDepOfStageCollectionNewStage != null && !oldIdDepOfStageCollectionNewStage.equals(dep)) {
                        oldIdDepOfStageCollectionNewStage.getStageCollection().remove(stageCollectionNewStage);
                        oldIdDepOfStageCollectionNewStage = em.merge(oldIdDepOfStageCollectionNewStage);
                    }
                }
            }
            em.getTransaction().commit();
        } catch (Exception ex) {
            String msg = ex.getLocalizedMessage();
            if (msg == null || msg.length() == 0) {
                BigDecimal id = dep.getId();
                if (findDep(id) == null) {
                    throw new NonexistentEntityException("The dep with id " + id + " no longer exists.");
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
            Dep dep;
            try {
                dep = em.getReference(Dep.class, id);
                dep.getId();
            } catch (EntityNotFoundException enfe) {
                throw new NonexistentEntityException("The dep with id " + id + " no longer exists.", enfe);
            }
            List<String> illegalOrphanMessages = null;
            Collection<Stage> stageCollectionOrphanCheck = dep.getStageCollection();
            for (Stage stageCollectionOrphanCheckStage : stageCollectionOrphanCheck) {
                if (illegalOrphanMessages == null) {
                    illegalOrphanMessages = new ArrayList<String>();
                }
                illegalOrphanMessages.add("This Dep (" + dep + ") cannot be destroyed since the Stage " + stageCollectionOrphanCheckStage + " in its stageCollection field has a non-nullable idDep field.");
            }
            if (illegalOrphanMessages != null) {
                throw new IllegalOrphanException(illegalOrphanMessages);
            }
            TypeDep idTypeDep = dep.getIdTypeDep();
            if (idTypeDep != null) {
                idTypeDep.getDepCollection().remove(dep);
                idTypeDep = em.merge(idTypeDep);
            }
            em.remove(dep);
            em.getTransaction().commit();
        } finally {
            if (em != null) {
                em.close();
            }
        }
    }

    public List<Dep> findDepEntities() {
        return findDepEntities(true, -1, -1);
    }

    public List<Dep> findDepEntities(int maxResults, int firstResult) {
        return findDepEntities(false, maxResults, firstResult);
    }

    private List<Dep> findDepEntities(boolean all, int maxResults, int firstResult) {
        EntityManager em = getEntityManager();
        try {
            CriteriaQuery cq = em.getCriteriaBuilder().createQuery();
            cq.select(cq.from(Dep.class));
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

    public Dep findDep(BigDecimal id) {
        EntityManager em = getEntityManager();
        try {
            return em.find(Dep.class, id);
        } finally {
            em.close();
        }
    }

    public int getDepCount() {
        EntityManager em = getEntityManager();
        try {
            CriteriaQuery cq = em.getCriteriaBuilder().createQuery();
            Root<Dep> rt = cq.from(Dep.class);
            cq.select(em.getCriteriaBuilder().count(rt));
            Query q = em.createQuery(cq);
            return ((Long) q.getSingleResult()).intValue();
        } finally {
            em.close();
        }
    }
    
}
