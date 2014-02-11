/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package ctrl;

import ctrl.exceptions.IllegalOrphanException;
import ctrl.exceptions.NonexistentEntityException;
import entity.Part;
import java.io.Serializable;
import java.math.BigDecimal;
import java.util.List;
import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.Query;
import javax.persistence.EntityNotFoundException;
import javax.persistence.criteria.CriteriaQuery;
import javax.persistence.criteria.Root;
import entity.Alloy;
import entity.Stage;
import java.util.ArrayList;
import java.util.Collection;

/**
 *
 * @author alexey
 */
public class PartJpaController implements Serializable {

    public PartJpaController(EntityManagerFactory emf) {
        this.emf = emf;
    }
    private EntityManagerFactory emf = null;

    public EntityManager getEntityManager() {
        return emf.createEntityManager();
    }

    public void create(Part part) {
        if (part.getStageCollection() == null) {
            part.setStageCollection(new ArrayList<Stage>());
        }
        EntityManager em = null;
        try {
            em = getEntityManager();
            em.getTransaction().begin();
            Alloy idAlloy = part.getIdAlloy();
            if (idAlloy != null) {
                idAlloy = em.getReference(idAlloy.getClass(), idAlloy.getId());
                part.setIdAlloy(idAlloy);
            }
            Collection<Stage> attachedStageCollection = new ArrayList<Stage>();
            for (Stage stageCollectionStageToAttach : part.getStageCollection()) {
                stageCollectionStageToAttach = em.getReference(stageCollectionStageToAttach.getClass(), stageCollectionStageToAttach.getId());
                attachedStageCollection.add(stageCollectionStageToAttach);
            }
            part.setStageCollection(attachedStageCollection);
            em.persist(part);
            if (idAlloy != null) {
                idAlloy.getPartCollection().add(part);
                idAlloy = em.merge(idAlloy);
            }
            for (Stage stageCollectionStage : part.getStageCollection()) {
                Part oldIdPartOfStageCollectionStage = stageCollectionStage.getIdPart();
                stageCollectionStage.setIdPart(part);
                stageCollectionStage = em.merge(stageCollectionStage);
                if (oldIdPartOfStageCollectionStage != null) {
                    oldIdPartOfStageCollectionStage.getStageCollection().remove(stageCollectionStage);
                    oldIdPartOfStageCollectionStage = em.merge(oldIdPartOfStageCollectionStage);
                }
            }
            em.getTransaction().commit();
        } finally {
            if (em != null) {
                em.close();
            }
        }
    }

    public void edit(Part part) throws IllegalOrphanException, NonexistentEntityException, Exception {
        EntityManager em = null;
        try {
            em = getEntityManager();
            em.getTransaction().begin();
            Part persistentPart = em.find(Part.class, part.getId());
            Alloy idAlloyOld = persistentPart.getIdAlloy();
            Alloy idAlloyNew = part.getIdAlloy();
            Collection<Stage> stageCollectionOld = persistentPart.getStageCollection();
            Collection<Stage> stageCollectionNew = part.getStageCollection();
            List<String> illegalOrphanMessages = null;
            for (Stage stageCollectionOldStage : stageCollectionOld) {
                if (!stageCollectionNew.contains(stageCollectionOldStage)) {
                    if (illegalOrphanMessages == null) {
                        illegalOrphanMessages = new ArrayList<String>();
                    }
                    illegalOrphanMessages.add("You must retain Stage " + stageCollectionOldStage + " since its idPart field is not nullable.");
                }
            }
            if (illegalOrphanMessages != null) {
                throw new IllegalOrphanException(illegalOrphanMessages);
            }
            if (idAlloyNew != null) {
                idAlloyNew = em.getReference(idAlloyNew.getClass(), idAlloyNew.getId());
                part.setIdAlloy(idAlloyNew);
            }
            Collection<Stage> attachedStageCollectionNew = new ArrayList<Stage>();
            for (Stage stageCollectionNewStageToAttach : stageCollectionNew) {
                stageCollectionNewStageToAttach = em.getReference(stageCollectionNewStageToAttach.getClass(), stageCollectionNewStageToAttach.getId());
                attachedStageCollectionNew.add(stageCollectionNewStageToAttach);
            }
            stageCollectionNew = attachedStageCollectionNew;
            part.setStageCollection(stageCollectionNew);
            part = em.merge(part);
            if (idAlloyOld != null && !idAlloyOld.equals(idAlloyNew)) {
                idAlloyOld.getPartCollection().remove(part);
                idAlloyOld = em.merge(idAlloyOld);
            }
            if (idAlloyNew != null && !idAlloyNew.equals(idAlloyOld)) {
                idAlloyNew.getPartCollection().add(part);
                idAlloyNew = em.merge(idAlloyNew);
            }
            for (Stage stageCollectionNewStage : stageCollectionNew) {
                if (!stageCollectionOld.contains(stageCollectionNewStage)) {
                    Part oldIdPartOfStageCollectionNewStage = stageCollectionNewStage.getIdPart();
                    stageCollectionNewStage.setIdPart(part);
                    stageCollectionNewStage = em.merge(stageCollectionNewStage);
                    if (oldIdPartOfStageCollectionNewStage != null && !oldIdPartOfStageCollectionNewStage.equals(part)) {
                        oldIdPartOfStageCollectionNewStage.getStageCollection().remove(stageCollectionNewStage);
                        oldIdPartOfStageCollectionNewStage = em.merge(oldIdPartOfStageCollectionNewStage);
                    }
                }
            }
            em.getTransaction().commit();
        } catch (Exception ex) {
            String msg = ex.getLocalizedMessage();
            if (msg == null || msg.length() == 0) {
                BigDecimal id = part.getId();
                if (findPart(id) == null) {
                    throw new NonexistentEntityException("The part with id " + id + " no longer exists.");
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
            Part part;
            try {
                part = em.getReference(Part.class, id);
                part.getId();
            } catch (EntityNotFoundException enfe) {
                throw new NonexistentEntityException("The part with id " + id + " no longer exists.", enfe);
            }
            List<String> illegalOrphanMessages = null;
            Collection<Stage> stageCollectionOrphanCheck = part.getStageCollection();
            for (Stage stageCollectionOrphanCheckStage : stageCollectionOrphanCheck) {
                if (illegalOrphanMessages == null) {
                    illegalOrphanMessages = new ArrayList<String>();
                }
                illegalOrphanMessages.add("This Part (" + part + ") cannot be destroyed since the Stage " + stageCollectionOrphanCheckStage + " in its stageCollection field has a non-nullable idPart field.");
            }
            if (illegalOrphanMessages != null) {
                throw new IllegalOrphanException(illegalOrphanMessages);
            }
            Alloy idAlloy = part.getIdAlloy();
            if (idAlloy != null) {
                idAlloy.getPartCollection().remove(part);
                idAlloy = em.merge(idAlloy);
            }
            em.remove(part);
            em.getTransaction().commit();
        } finally {
            if (em != null) {
                em.close();
            }
        }
    }

    public List<Part> findPartEntities() {
        return findPartEntities(true, -1, -1);
    }

    public List<Part> findPartEntities(int maxResults, int firstResult) {
        return findPartEntities(false, maxResults, firstResult);
    }

    private List<Part> findPartEntities(boolean all, int maxResults, int firstResult) {
        EntityManager em = getEntityManager();
        try {
            CriteriaQuery cq = em.getCriteriaBuilder().createQuery();
            cq.select(cq.from(Part.class));
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

    public Part findPart(BigDecimal id) {
        EntityManager em = getEntityManager();
        try {
            return em.find(Part.class, id);
        } finally {
            em.close();
        }
    }

    public int getPartCount() {
        EntityManager em = getEntityManager();
        try {
            CriteriaQuery cq = em.getCriteriaBuilder().createQuery();
            Root<Part> rt = cq.from(Part.class);
            cq.select(em.getCriteriaBuilder().count(rt));
            Query q = em.createQuery(cq);
            return ((Long) q.getSingleResult()).intValue();
        } finally {
            em.close();
        }
    }
    
}
