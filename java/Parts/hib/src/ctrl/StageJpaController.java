/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package ctrl;

import ctrl.exceptions.IllegalOrphanException;
import ctrl.exceptions.NonexistentEntityException;
import java.io.Serializable;
import java.math.BigDecimal;
import java.util.List;
import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.Query;
import javax.persistence.EntityNotFoundException;
import javax.persistence.criteria.CriteriaQuery;
import javax.persistence.criteria.Root;
import entity.Surface;
import entity.Stage;
import entity.Part;
import entity.Dep;
import java.util.ArrayList;
import java.util.Collection;

/**
 *
 * @author alexey
 */
public class StageJpaController implements Serializable {

    public StageJpaController(EntityManagerFactory emf) {
        this.emf = emf;
    }
    private EntityManagerFactory emf = null;

    public EntityManager getEntityManager() {
        return emf.createEntityManager();
    }

    public void create(Stage stage) {
        if (stage.getStageCollection() == null) {
            stage.setStageCollection(new ArrayList<Stage>());
        }
        if (stage.getStageCollection1() == null) {
            stage.setStageCollection1(new ArrayList<Stage>());
        }
        EntityManager em = null;
        try {
            em = getEntityManager();
            em.getTransaction().begin();
            Surface idSurface = stage.getIdSurface();
            if (idSurface != null) {
                idSurface = em.getReference(idSurface.getClass(), idSurface.getId());
                stage.setIdSurface(idSurface);
            }
            Stage idPrev = stage.getIdPrev();
            if (idPrev != null) {
                idPrev = em.getReference(idPrev.getClass(), idPrev.getId());
                stage.setIdPrev(idPrev);
            }
            Stage idNext = stage.getIdNext();
            if (idNext != null) {
                idNext = em.getReference(idNext.getClass(), idNext.getId());
                stage.setIdNext(idNext);
            }
            Part idPart = stage.getIdPart();
            if (idPart != null) {
                idPart = em.getReference(idPart.getClass(), idPart.getId());
                stage.setIdPart(idPart);
            }
            Dep idDep = stage.getIdDep();
            if (idDep != null) {
                idDep = em.getReference(idDep.getClass(), idDep.getId());
                stage.setIdDep(idDep);
            }
            Collection<Stage> attachedStageCollection = new ArrayList<Stage>();
            for (Stage stageCollectionStageToAttach : stage.getStageCollection()) {
                stageCollectionStageToAttach = em.getReference(stageCollectionStageToAttach.getClass(), stageCollectionStageToAttach.getId());
                attachedStageCollection.add(stageCollectionStageToAttach);
            }
            stage.setStageCollection(attachedStageCollection);
            Collection<Stage> attachedStageCollection1 = new ArrayList<Stage>();
            for (Stage stageCollection1StageToAttach : stage.getStageCollection1()) {
                stageCollection1StageToAttach = em.getReference(stageCollection1StageToAttach.getClass(), stageCollection1StageToAttach.getId());
                attachedStageCollection1.add(stageCollection1StageToAttach);
            }
            stage.setStageCollection1(attachedStageCollection1);
            em.persist(stage);
            if (idSurface != null) {
                idSurface.getStageCollection().add(stage);
                idSurface = em.merge(idSurface);
            }
            if (idPrev != null) {
                idPrev.getStageCollection().add(stage);
                idPrev = em.merge(idPrev);
            }
            if (idNext != null) {
                idNext.getStageCollection().add(stage);
                idNext = em.merge(idNext);
            }
            if (idPart != null) {
                idPart.getStageCollection().add(stage);
                idPart = em.merge(idPart);
            }
            if (idDep != null) {
                idDep.getStageCollection().add(stage);
                idDep = em.merge(idDep);
            }
            for (Stage stageCollectionStage : stage.getStageCollection()) {
                Stage oldIdPrevOfStageCollectionStage = stageCollectionStage.getIdPrev();
                stageCollectionStage.setIdPrev(stage);
                stageCollectionStage = em.merge(stageCollectionStage);
                if (oldIdPrevOfStageCollectionStage != null) {
                    oldIdPrevOfStageCollectionStage.getStageCollection().remove(stageCollectionStage);
                    oldIdPrevOfStageCollectionStage = em.merge(oldIdPrevOfStageCollectionStage);
                }
            }
            for (Stage stageCollection1Stage : stage.getStageCollection1()) {
                Stage oldIdNextOfStageCollection1Stage = stageCollection1Stage.getIdNext();
                stageCollection1Stage.setIdNext(stage);
                stageCollection1Stage = em.merge(stageCollection1Stage);
                if (oldIdNextOfStageCollection1Stage != null) {
                    oldIdNextOfStageCollection1Stage.getStageCollection1().remove(stageCollection1Stage);
                    oldIdNextOfStageCollection1Stage = em.merge(oldIdNextOfStageCollection1Stage);
                }
            }
            em.getTransaction().commit();
        } finally {
            if (em != null) {
                em.close();
            }
        }
    }

    public void edit(Stage stage) throws IllegalOrphanException, NonexistentEntityException, Exception {
        EntityManager em = null;
        try {
            em = getEntityManager();
            em.getTransaction().begin();
            Stage persistentStage = em.find(Stage.class, stage.getId());
            Surface idSurfaceOld = persistentStage.getIdSurface();
            Surface idSurfaceNew = stage.getIdSurface();
            Stage idPrevOld = persistentStage.getIdPrev();
            Stage idPrevNew = stage.getIdPrev();
            Stage idNextOld = persistentStage.getIdNext();
            Stage idNextNew = stage.getIdNext();
            Part idPartOld = persistentStage.getIdPart();
            Part idPartNew = stage.getIdPart();
            Dep idDepOld = persistentStage.getIdDep();
            Dep idDepNew = stage.getIdDep();
            Collection<Stage> stageCollectionOld = persistentStage.getStageCollection();
            Collection<Stage> stageCollectionNew = stage.getStageCollection();
            Collection<Stage> stageCollection1Old = persistentStage.getStageCollection1();
            Collection<Stage> stageCollection1New = stage.getStageCollection1();
            List<String> illegalOrphanMessages = null;
            for (Stage stageCollectionOldStage : stageCollectionOld) {
                if (!stageCollectionNew.contains(stageCollectionOldStage)) {
                    if (illegalOrphanMessages == null) {
                        illegalOrphanMessages = new ArrayList<String>();
                    }
                    illegalOrphanMessages.add("You must retain Stage " + stageCollectionOldStage + " since its idPrev field is not nullable.");
                }
            }
            for (Stage stageCollection1OldStage : stageCollection1Old) {
                if (!stageCollection1New.contains(stageCollection1OldStage)) {
                    if (illegalOrphanMessages == null) {
                        illegalOrphanMessages = new ArrayList<String>();
                    }
                    illegalOrphanMessages.add("You must retain Stage " + stageCollection1OldStage + " since its idNext field is not nullable.");
                }
            }
            if (illegalOrphanMessages != null) {
                throw new IllegalOrphanException(illegalOrphanMessages);
            }
            if (idSurfaceNew != null) {
                idSurfaceNew = em.getReference(idSurfaceNew.getClass(), idSurfaceNew.getId());
                stage.setIdSurface(idSurfaceNew);
            }
            if (idPrevNew != null) {
                idPrevNew = em.getReference(idPrevNew.getClass(), idPrevNew.getId());
                stage.setIdPrev(idPrevNew);
            }
            if (idNextNew != null) {
                idNextNew = em.getReference(idNextNew.getClass(), idNextNew.getId());
                stage.setIdNext(idNextNew);
            }
            if (idPartNew != null) {
                idPartNew = em.getReference(idPartNew.getClass(), idPartNew.getId());
                stage.setIdPart(idPartNew);
            }
            if (idDepNew != null) {
                idDepNew = em.getReference(idDepNew.getClass(), idDepNew.getId());
                stage.setIdDep(idDepNew);
            }
            Collection<Stage> attachedStageCollectionNew = new ArrayList<Stage>();
            for (Stage stageCollectionNewStageToAttach : stageCollectionNew) {
                stageCollectionNewStageToAttach = em.getReference(stageCollectionNewStageToAttach.getClass(), stageCollectionNewStageToAttach.getId());
                attachedStageCollectionNew.add(stageCollectionNewStageToAttach);
            }
            stageCollectionNew = attachedStageCollectionNew;
            stage.setStageCollection(stageCollectionNew);
            Collection<Stage> attachedStageCollection1New = new ArrayList<Stage>();
            for (Stage stageCollection1NewStageToAttach : stageCollection1New) {
                stageCollection1NewStageToAttach = em.getReference(stageCollection1NewStageToAttach.getClass(), stageCollection1NewStageToAttach.getId());
                attachedStageCollection1New.add(stageCollection1NewStageToAttach);
            }
            stageCollection1New = attachedStageCollection1New;
            stage.setStageCollection1(stageCollection1New);
            stage = em.merge(stage);
            if (idSurfaceOld != null && !idSurfaceOld.equals(idSurfaceNew)) {
                idSurfaceOld.getStageCollection().remove(stage);
                idSurfaceOld = em.merge(idSurfaceOld);
            }
            if (idSurfaceNew != null && !idSurfaceNew.equals(idSurfaceOld)) {
                idSurfaceNew.getStageCollection().add(stage);
                idSurfaceNew = em.merge(idSurfaceNew);
            }
            if (idPrevOld != null && !idPrevOld.equals(idPrevNew)) {
                idPrevOld.getStageCollection().remove(stage);
                idPrevOld = em.merge(idPrevOld);
            }
            if (idPrevNew != null && !idPrevNew.equals(idPrevOld)) {
                idPrevNew.getStageCollection().add(stage);
                idPrevNew = em.merge(idPrevNew);
            }
            if (idNextOld != null && !idNextOld.equals(idNextNew)) {
                idNextOld.getStageCollection().remove(stage);
                idNextOld = em.merge(idNextOld);
            }
            if (idNextNew != null && !idNextNew.equals(idNextOld)) {
                idNextNew.getStageCollection().add(stage);
                idNextNew = em.merge(idNextNew);
            }
            if (idPartOld != null && !idPartOld.equals(idPartNew)) {
                idPartOld.getStageCollection().remove(stage);
                idPartOld = em.merge(idPartOld);
            }
            if (idPartNew != null && !idPartNew.equals(idPartOld)) {
                idPartNew.getStageCollection().add(stage);
                idPartNew = em.merge(idPartNew);
            }
            if (idDepOld != null && !idDepOld.equals(idDepNew)) {
                idDepOld.getStageCollection().remove(stage);
                idDepOld = em.merge(idDepOld);
            }
            if (idDepNew != null && !idDepNew.equals(idDepOld)) {
                idDepNew.getStageCollection().add(stage);
                idDepNew = em.merge(idDepNew);
            }
            for (Stage stageCollectionNewStage : stageCollectionNew) {
                if (!stageCollectionOld.contains(stageCollectionNewStage)) {
                    Stage oldIdPrevOfStageCollectionNewStage = stageCollectionNewStage.getIdPrev();
                    stageCollectionNewStage.setIdPrev(stage);
                    stageCollectionNewStage = em.merge(stageCollectionNewStage);
                    if (oldIdPrevOfStageCollectionNewStage != null && !oldIdPrevOfStageCollectionNewStage.equals(stage)) {
                        oldIdPrevOfStageCollectionNewStage.getStageCollection().remove(stageCollectionNewStage);
                        oldIdPrevOfStageCollectionNewStage = em.merge(oldIdPrevOfStageCollectionNewStage);
                    }
                }
            }
            for (Stage stageCollection1NewStage : stageCollection1New) {
                if (!stageCollection1Old.contains(stageCollection1NewStage)) {
                    Stage oldIdNextOfStageCollection1NewStage = stageCollection1NewStage.getIdNext();
                    stageCollection1NewStage.setIdNext(stage);
                    stageCollection1NewStage = em.merge(stageCollection1NewStage);
                    if (oldIdNextOfStageCollection1NewStage != null && !oldIdNextOfStageCollection1NewStage.equals(stage)) {
                        oldIdNextOfStageCollection1NewStage.getStageCollection1().remove(stageCollection1NewStage);
                        oldIdNextOfStageCollection1NewStage = em.merge(oldIdNextOfStageCollection1NewStage);
                    }
                }
            }
            em.getTransaction().commit();
        } catch (Exception ex) {
            String msg = ex.getLocalizedMessage();
            if (msg == null || msg.length() == 0) {
                BigDecimal id = stage.getId();
                if (findStage(id) == null) {
                    throw new NonexistentEntityException("The stage with id " + id + " no longer exists.");
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
            Stage stage;
            try {
                stage = em.getReference(Stage.class, id);
                stage.getId();
            } catch (EntityNotFoundException enfe) {
                throw new NonexistentEntityException("The stage with id " + id + " no longer exists.", enfe);
            }
            List<String> illegalOrphanMessages = null;
            Collection<Stage> stageCollectionOrphanCheck = stage.getStageCollection();
            for (Stage stageCollectionOrphanCheckStage : stageCollectionOrphanCheck) {
                if (illegalOrphanMessages == null) {
                    illegalOrphanMessages = new ArrayList<String>();
                }
                illegalOrphanMessages.add("This Stage (" + stage + ") cannot be destroyed since the Stage " + stageCollectionOrphanCheckStage + " in its stageCollection field has a non-nullable idPrev field.");
            }
            Collection<Stage> stageCollection1OrphanCheck = stage.getStageCollection1();
            for (Stage stageCollection1OrphanCheckStage : stageCollection1OrphanCheck) {
                if (illegalOrphanMessages == null) {
                    illegalOrphanMessages = new ArrayList<String>();
                }
                illegalOrphanMessages.add("This Stage (" + stage + ") cannot be destroyed since the Stage " + stageCollection1OrphanCheckStage + " in its stageCollection1 field has a non-nullable idNext field.");
            }
            if (illegalOrphanMessages != null) {
                throw new IllegalOrphanException(illegalOrphanMessages);
            }
            Surface idSurface = stage.getIdSurface();
            if (idSurface != null) {
                idSurface.getStageCollection().remove(stage);
                idSurface = em.merge(idSurface);
            }
            Stage idPrev = stage.getIdPrev();
            if (idPrev != null) {
                idPrev.getStageCollection().remove(stage);
                idPrev = em.merge(idPrev);
            }
            Stage idNext = stage.getIdNext();
            if (idNext != null) {
                idNext.getStageCollection().remove(stage);
                idNext = em.merge(idNext);
            }
            Part idPart = stage.getIdPart();
            if (idPart != null) {
                idPart.getStageCollection().remove(stage);
                idPart = em.merge(idPart);
            }
            Dep idDep = stage.getIdDep();
            if (idDep != null) {
                idDep.getStageCollection().remove(stage);
                idDep = em.merge(idDep);
            }
            em.remove(stage);
            em.getTransaction().commit();
        } finally {
            if (em != null) {
                em.close();
            }
        }
    }

    public List<Stage> findStageEntities() {
        return findStageEntities(true, -1, -1);
    }

    public List<Stage> findStageEntities(int maxResults, int firstResult) {
        return findStageEntities(false, maxResults, firstResult);
    }

    private List<Stage> findStageEntities(boolean all, int maxResults, int firstResult) {
        EntityManager em = getEntityManager();
        try {
            CriteriaQuery cq = em.getCriteriaBuilder().createQuery();
            cq.select(cq.from(Stage.class));
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

    public Stage findStage(BigDecimal id) {
        EntityManager em = getEntityManager();
        try {
            return em.find(Stage.class, id);
        } finally {
            em.close();
        }
    }

    public int getStageCount() {
        EntityManager em = getEntityManager();
        try {
            CriteriaQuery cq = em.getCriteriaBuilder().createQuery();
            Root<Stage> rt = cq.from(Stage.class);
            cq.select(em.getCriteriaBuilder().count(rt));
            Query q = em.createQuery(cq);
            return ((Long) q.getSingleResult()).intValue();
        } finally {
            em.close();
        }
    }
    
}
