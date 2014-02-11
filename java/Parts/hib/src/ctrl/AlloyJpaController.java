/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package ctrl;

import ctrl.exceptions.IllegalOrphanException;
import ctrl.exceptions.NonexistentEntityException;
import entity.Alloy;
import java.io.Serializable;
import java.math.BigDecimal;
import java.util.List;
import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.Query;
import javax.persistence.EntityNotFoundException;
import javax.persistence.criteria.CriteriaQuery;
import javax.persistence.criteria.Root;
import entity.Part;
import java.util.ArrayList;
import java.util.Collection;

/**
 *
 * @author alexey
 */
public class AlloyJpaController implements Serializable {

    public AlloyJpaController(EntityManagerFactory emf) {
        this.emf = emf;
    }
    private EntityManagerFactory emf = null;

    public EntityManager getEntityManager() {
        return emf.createEntityManager();
    }

    public void create(Alloy alloy) {
        if (alloy.getPartCollection() == null) {
            alloy.setPartCollection(new ArrayList<Part>());
        }
        EntityManager em = null;
        try {
            em = getEntityManager();
            em.getTransaction().begin();
            Collection<Part> attachedPartCollection = new ArrayList<Part>();
            for (Part partCollectionPartToAttach : alloy.getPartCollection()) {
                partCollectionPartToAttach = em.getReference(partCollectionPartToAttach.getClass(), partCollectionPartToAttach.getId());
                attachedPartCollection.add(partCollectionPartToAttach);
            }
            alloy.setPartCollection(attachedPartCollection);
            em.persist(alloy);
            for (Part partCollectionPart : alloy.getPartCollection()) {
                Alloy oldIdAlloyOfPartCollectionPart = partCollectionPart.getIdAlloy();
                partCollectionPart.setIdAlloy(alloy);
                partCollectionPart = em.merge(partCollectionPart);
                if (oldIdAlloyOfPartCollectionPart != null) {
                    oldIdAlloyOfPartCollectionPart.getPartCollection().remove(partCollectionPart);
                    oldIdAlloyOfPartCollectionPart = em.merge(oldIdAlloyOfPartCollectionPart);
                }
            }
            em.getTransaction().commit();
        } finally {
            if (em != null) {
                em.close();
            }
        }
    }

    public void edit(Alloy alloy) throws IllegalOrphanException, NonexistentEntityException, Exception {
        EntityManager em = null;
        try {
            em = getEntityManager();
            em.getTransaction().begin();
            Alloy persistentAlloy = em.find(Alloy.class, alloy.getId());
            Collection<Part> partCollectionOld = persistentAlloy.getPartCollection();
            Collection<Part> partCollectionNew = alloy.getPartCollection();
            List<String> illegalOrphanMessages = null;
            for (Part partCollectionOldPart : partCollectionOld) {
                if (!partCollectionNew.contains(partCollectionOldPart)) {
                    if (illegalOrphanMessages == null) {
                        illegalOrphanMessages = new ArrayList<String>();
                    }
                    illegalOrphanMessages.add("You must retain Part " + partCollectionOldPart + " since its idAlloy field is not nullable.");
                }
            }
            if (illegalOrphanMessages != null) {
                throw new IllegalOrphanException(illegalOrphanMessages);
            }
            Collection<Part> attachedPartCollectionNew = new ArrayList<Part>();
            for (Part partCollectionNewPartToAttach : partCollectionNew) {
                partCollectionNewPartToAttach = em.getReference(partCollectionNewPartToAttach.getClass(), partCollectionNewPartToAttach.getId());
                attachedPartCollectionNew.add(partCollectionNewPartToAttach);
            }
            partCollectionNew = attachedPartCollectionNew;
            alloy.setPartCollection(partCollectionNew);
            alloy = em.merge(alloy);
            for (Part partCollectionNewPart : partCollectionNew) {
                if (!partCollectionOld.contains(partCollectionNewPart)) {
                    Alloy oldIdAlloyOfPartCollectionNewPart = partCollectionNewPart.getIdAlloy();
                    partCollectionNewPart.setIdAlloy(alloy);
                    partCollectionNewPart = em.merge(partCollectionNewPart);
                    if (oldIdAlloyOfPartCollectionNewPart != null && !oldIdAlloyOfPartCollectionNewPart.equals(alloy)) {
                        oldIdAlloyOfPartCollectionNewPart.getPartCollection().remove(partCollectionNewPart);
                        oldIdAlloyOfPartCollectionNewPart = em.merge(oldIdAlloyOfPartCollectionNewPart);
                    }
                }
            }
            em.getTransaction().commit();
        } catch (Exception ex) {
            String msg = ex.getLocalizedMessage();
            if (msg == null || msg.length() == 0) {
                BigDecimal id = alloy.getId();
                if (findAlloy(id) == null) {
                    throw new NonexistentEntityException("The alloy with id " + id + " no longer exists.");
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
            Alloy alloy;
            try {
                alloy = em.getReference(Alloy.class, id);
                alloy.getId();
            } catch (EntityNotFoundException enfe) {
                throw new NonexistentEntityException("The alloy with id " + id + " no longer exists.", enfe);
            }
            List<String> illegalOrphanMessages = null;
            Collection<Part> partCollectionOrphanCheck = alloy.getPartCollection();
            for (Part partCollectionOrphanCheckPart : partCollectionOrphanCheck) {
                if (illegalOrphanMessages == null) {
                    illegalOrphanMessages = new ArrayList<String>();
                }
                illegalOrphanMessages.add("This Alloy (" + alloy + ") cannot be destroyed since the Part " + partCollectionOrphanCheckPart + " in its partCollection field has a non-nullable idAlloy field.");
            }
            if (illegalOrphanMessages != null) {
                throw new IllegalOrphanException(illegalOrphanMessages);
            }
            em.remove(alloy);
            em.getTransaction().commit();
        } finally {
            if (em != null) {
                em.close();
            }
        }
    }

    public List<Alloy> findAlloyEntities() {
        return findAlloyEntities(true, -1, -1);
    }

    public List<Alloy> findAlloyEntities(int maxResults, int firstResult) {
        return findAlloyEntities(false, maxResults, firstResult);
    }

    private List<Alloy> findAlloyEntities(boolean all, int maxResults, int firstResult) {
        EntityManager em = getEntityManager();
        try {
            CriteriaQuery cq = em.getCriteriaBuilder().createQuery();
            cq.select(cq.from(Alloy.class));
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

    public Alloy findAlloy(BigDecimal id) {
        EntityManager em = getEntityManager();
        try {
            return em.find(Alloy.class, id);
        } finally {
            em.close();
        }
    }

    public int getAlloyCount() {
        EntityManager em = getEntityManager();
        try {
            CriteriaQuery cq = em.getCriteriaBuilder().createQuery();
            Root<Alloy> rt = cq.from(Alloy.class);
            cq.select(em.getCriteriaBuilder().count(rt));
            Query q = em.createQuery(cq);
            return ((Long) q.getSingleResult()).intValue();
        } finally {
            em.close();
        }
    }
    
}
