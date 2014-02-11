/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package entity;

import java.io.Serializable;
import java.math.BigDecimal;
import java.util.Collection;
import javax.persistence.*;

/**
 *
 * @author alexey
 */
@Entity
@Table(name = "STAGE", uniqueConstraints = {
    @UniqueConstraint(columnNames = {"ID_PREV", "ID_NEXT", "ID_DEP", "ID_PART", "ID_SURFACE"})})
@NamedQueries({
    @NamedQuery(name = "Stage.findAll", query = "SELECT s FROM Stage s"),
    @NamedQuery(name = "Stage.findById", query = "SELECT s FROM Stage s WHERE s.id = :id")})
public class Stage implements Serializable {
    private static final long serialVersionUID = 1L;
    // @Max(value=?)  @Min(value=?)//if you know range of your decimal fields consider using these annotations to enforce field validation
    @Id
    @GeneratedValue(strategy=GenerationType.SEQUENCE, generator="stSequenceGenerator")
    @SequenceGenerator(allocationSize=1, schema = "PARTS", name="stSequenceGenerator", sequenceName = "SEQ_STAGE")
    @Basic(optional = false)
    @Column(name = "ID", nullable = false, precision = 0, scale = -127)    
    private BigDecimal id;
    @JoinColumn(name = "ID_SURFACE", referencedColumnName = "ID", nullable = false)
    @ManyToOne(optional = false, fetch = FetchType.LAZY)
    private Surface idSurface;
    @OneToMany(cascade = CascadeType.ALL, mappedBy = "idPrev", fetch = FetchType.LAZY)
    private Collection<Stage> stageCollection;
    @JoinColumn(name = "ID_PREV", referencedColumnName = "ID", nullable = false)
    @ManyToOne(optional = false, fetch = FetchType.LAZY)
    private Stage idPrev;
    @OneToMany(cascade = CascadeType.ALL, mappedBy = "idNext", fetch = FetchType.LAZY)
    private Collection<Stage> stageCollection1;
    @JoinColumn(name = "ID_NEXT", referencedColumnName = "ID", nullable = false)
    @ManyToOne(optional = false, fetch = FetchType.LAZY)
    private Stage idNext;
    @JoinColumn(name = "ID_PART", referencedColumnName = "ID", nullable = false)
    @ManyToOne(optional = false, fetch = FetchType.LAZY)
    private Part idPart;
    @JoinColumn(name = "ID_DEP", referencedColumnName = "ID", nullable = false)
    @ManyToOne(optional = false, fetch = FetchType.LAZY)
    private Dep idDep;

    public Stage() {
    }

    public Stage(BigDecimal id) {
        this.id = id;
    }

    public BigDecimal getId() {
        return id;
    }

    public void setId(BigDecimal id) {
        this.id = id;
    }

    public Surface getIdSurface() {
        return idSurface;
    }

    public void setIdSurface(Surface idSurface) {
        this.idSurface = idSurface;
    }

    public Collection<Stage> getStageCollection() {
        return stageCollection;
    }

    public void setStageCollection(Collection<Stage> stageCollection) {
        this.stageCollection = stageCollection;
    }

    public Stage getIdPrev() {
        return idPrev;
    }

    public void setIdPrev(Stage idPrev) {
        this.idPrev = idPrev;
    }

    public Collection<Stage> getStageCollection1() {
        return stageCollection1;
    }

    public void setStageCollection1(Collection<Stage> stageCollection1) {
        this.stageCollection1 = stageCollection1;
    }

    public Stage getIdNext() {
        return idNext;
    }

    public void setIdNext(Stage idNext) {
        this.idNext = idNext;
    }

    public Part getIdPart() {
        return idPart;
    }

    public void setIdPart(Part idPart) {
        this.idPart = idPart;
    }

    public Dep getIdDep() {
        return idDep;
    }

    public void setIdDep(Dep idDep) {
        this.idDep = idDep;
    }

    @Override
    public int hashCode() {
        int hash = 0;
        hash += (id != null ? id.hashCode() : 0);
        return hash;
    }

    @Override
    public boolean equals(Object object) {
        // TODO: Warning - this method won't work in the case the id fields are not set
        if (!(object instanceof Stage)) {
            return false;
        }
        Stage other = (Stage) object;
        if ((this.id == null && other.id != null) || (this.id != null && !this.id.equals(other.id))) {
            return false;
        }
        return true;
    }

    @Override
    public String toString() {
        return id + " " + idDep;
    }
    
}
