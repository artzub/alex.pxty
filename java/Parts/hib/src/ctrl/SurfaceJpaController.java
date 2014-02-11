/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package ctrl;

import ctrl.exceptions.IllegalOrphanException;
import ctrl.exceptions.NonexistentEntityException;
import entity.Surface;
import java.io.Serializable;
import java.math.BigDecimal;
import java.util.List;
import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.Query;
import javax.persistence.EntityNotFoundException;
import javax.persistence.criteria.CriteriaQuery;
import javax.persistence.criteria.Root;
import entity.Stage;
import java.util.ArrayList;
import java.util.Collection;

/**
 *
 * @author alexey
 */
public class SurfaceJpaController implements Serializable {

    public SurfaceJpaController(EntityManagerFactory emf) {
        this.emf = emf;
    }
    private EntityManagerFactory emf = null;

    public EntityManager getEntityManager() {
        return emf.createEntityManager();
    }

    public void create(Surface surface) {
        if (surface.getStageCollection() == null) {
            surface.setStageCollection(new ArrayList<Stage>());
        }
        EntityManager em = null;
        try {
            em = getEntityManager();
            em.getTransaction().begin();
            Collection<Stage> attachedStageCollection = new ArrayList<Stage>();
            for (Stage stageCollectionStageToAttach : surface.getStageCollection()) {
                stageCollectionStageToAttach = em.getReference(stageCollectionStageToAttach.getClass(), stageCollectionStageToAttach.getId());
                attachedStageCollection.add(stageCollectionStageToAttach);
            }
            surface.setStageCollection(attachedStageCollection);
            em.persist(surface);
            for (Stage stageCollectionStage : surface.getStageCollection()) {
                Surface oldIdSurfaceOfStageCollectionStage = stageCollectionStage.getIdSurface();
                stageCollectionStage.setIdSurface(surface);
                stageCollectionStage = em.merge(stageCollectionStage);
                if (oldIdSurfaceOfStageCollectionStage != null) {
                    oldIdSurfaceOfStageCollectionStage.getStageCollection().remove(stageCollectionStage);
                    oldIdSurfaceOfStageCollectionStage = em.merge(oldIdSurfaceOfStageCollectionStage);
                }
            }
            em.getTransaction().commit();
        } finally {
            if (em != null) {
                em.close();
            }
        }
    }

    public void edit(Surface surface) throws IllegalOrphanException, NonexistentEntityException, Exception {
        EntityManager em = null;
        try {
            em = getEntityManager();
            em.getTransaction().begin();
            Surface persistentSurface = em.find(Surface.class, surface.getId());
            Collection<Stage> stageCollectionOld = persistentSurface.getStageCollection();
            Collection<Stage> stageCollectionNew = surface.getStageCollection();
            List<String> illegalOrphanMessages = null;
            for (Stage stageCollectionOldStage : stageCollectionOld) {
                if (!stageCollectionNew.contains(stageCollectionOldStage)) {
                    if (illegalOrphanMessages == null) {
                        illegalOrphanMessages = new ArrayList<String>();
                    }
                    illegalOrphanMessages.add("You must retain Stage " + stageCollectionOldStage + " since its idSurface field is not nullable.");
                }
            }
            if (illegalOrphanMessages != null) {
                throw new IllegalOrphanException(illegalOrphanMessages);
            }
            Collection<Stage> attachedStageCollectionNew = new ArrayList<Stage>();
            for (Stage stageCollectionNewStageToAttach : stageCollectionNew) {
                stageCollectionNewStageToAttach = em.getReference(stageCollectionNewStageToAttach.getClass(), stageCollectionNewStageToAttach.getId());
                attachedStageCollectionNew.add(stageCollectionNewStageToAttach);
            }
            stageCollectionNew = attachedStageCollectionNew;
            surface.setStageCollection(stageCollectionNew);
            surface = em.merge(surface);
            for (Stage stageCollectionNewStage : stageCollectionNew) {
                if (!stageCollectionOld.contains(stageCollectionNewStage)) {
                    Surface oldIdSurfaceOfStageCollectionNewStage = stageCollectionNewStage.getIdSurface();
                    stageCollectionNewStage.setIdSurface(surface);
                    stageCollectionNewStage = em.merge(stageCollectionNewStage);
                    if (oldIdSurfaceOfStageCollectionNewStage != null && !oldIdSurfaceOfStageCollectionNewStage.equals(surface)) {
                        oldIdSurfaceOfStageCollectionNewStage.getStageCollection().remove(stageCollectionNewStage);
                        oldIdSurfaceOfStageCollectionNewStage = em.merge(oldIdSurfaceOfStageCollectionNewStage);
                    }
                }
            }
            em.getTransaction().commit();
        } catch (Exception ex) {
            String msg = ex.getLocalizedMessage();
            if (msg == null || msg.length() == 0) {
                BigDecimal id = surface.getId();
                if (findSurface(id) == null) {
                    throw new NonexistentEntityException("The surface with id " + id + " no longer exists.");
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
            Surface surface;
            try {
                surface = em.getReference(Surface.class, id);
                surface.getId();
            } catch (EntityNotFoundException enfe) {
                throw new NonexistentEntityException("The surface with id " + id + " no longer exists.", enfe);
            }
            List<String> illegalOrphanMessages = null;
            Collection<Stage> stageCollectionOrphanCheck = surface.getStageCollection();
            for (Stage stageCollectionOrphanCheckStage : stageCollectionOrphanCheck) {
                if (illegalOrphanMessages == null) {
                    illegalOrphanMessages = new ArrayList<String>();
                }
                illegalOrphanMessages.add("This Surface (" + surface + ") cannot be destroyed since the Stage " + stageCollectionOrphanCheckStage + " in its stageCollection field has a non-nullable idSurface field.");
            }
            if (illegalOrphanMessages != null) {
                throw new IllegalOrphanException(illegalOrphanMessages);
            }
            em.remove(surface);
            em.getTransaction().commit();
        } finally {
            if (em != null) {
                em.close();
            }
        }
    }

    public List<Surface> findSurfaceEntities() {
        return findSurfaceEntities(true, -1, -1);
    }

    public List<Surface> findSurfaceEntities(int maxResults, int firstResult) {
        return findSurfaceEntities(false, maxResults, firstResult);
    }

    private List<Surface> findSurfaceEntities(boolean all, int maxResults, int firstResult) {
        EntityManager em = getEntityManager();
        try {
            CriteriaQuery cq = em.getCriteriaBuilder().createQuery();
            cq.select(cq.from(Surface.class));
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

    public Surface findSurface(BigDecimal id) {
        EntityManager em = getEntityManager();
        try {
            return em.find(Surface.class, id);
        } finally {
            em.close();
        }
    }

    public int getSurfaceCount() {
        EntityManager em = getEntityManager();
        try {
            CriteriaQuery cq = em.getCriteriaBuilder().createQuery();
            Root<Surface> rt = cq.from(Surface.class);
            cq.select(em.getCriteriaBuilder().count(rt));
            Query q = em.createQuery(cq);
            return ((Long) q.getSingleResult()).intValue();
        } finally {
            em.close();
        }
    }
    
}
