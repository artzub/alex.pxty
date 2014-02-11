/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package entity;

import java.io.Serializable;
import java.math.BigDecimal;
import java.math.BigInteger;
import java.util.Collection;
import javax.persistence.*;

/**
 *
 * @author alexey
 */
@Entity
@Table(name = "DEP", uniqueConstraints = {
    @UniqueConstraint(columnNames = {"NUM"})})
@NamedQueries({
    @NamedQuery(name = "Dep.findAll", query = "SELECT d FROM Dep d"),
    @NamedQuery(name = "Dep.findById", query = "SELECT d FROM Dep d WHERE d.id = :id"),
    @NamedQuery(name = "Dep.findByNum", query = "SELECT d FROM Dep d WHERE d.num = :num")})
public class Dep implements Serializable {
    private static final long serialVersionUID = 1L;
    // @Max(value=?)  @Min(value=?)//if you know range of your decimal fields consider using these annotations to enforce field validation
    @Id
    @GeneratedValue(strategy=GenerationType.SEQUENCE, generator="dSequenceGenerator")
    @SequenceGenerator(allocationSize=1, schema = "PARTS", name="dSequenceGenerator", sequenceName = "SEQ_DEP")
    @Basic(optional = false)
    @Column(name = "ID", nullable = false, precision = 0, scale = -127)
    private BigDecimal id;
    @Basic(optional = false)
    @Column(name = "NUM", nullable = false)
    private BigInteger num;
    @OneToMany(cascade = CascadeType.ALL, mappedBy = "idDep", fetch = FetchType.LAZY)
    private Collection<Stage> stageCollection;
    @JoinColumn(name = "ID_TYPE_DEP", referencedColumnName = "ID", nullable = false)
    @ManyToOne(optional = false, fetch = FetchType.LAZY)
    private TypeDep idTypeDep;

    public Dep() {
    }

    public Dep(BigDecimal id) {
        this.id = id;
    }

    public Dep(BigDecimal id, BigInteger num) {
        this.id = id;
        this.num = num;
    }

    public BigDecimal getId() {
        return id;
    }

    public void setId(BigDecimal id) {
        this.id = id;
    }

    public BigInteger getNum() {
        return num;
    }

    public void setNum(BigInteger num) {
        this.num = num;
    }

    public Collection<Stage> getStageCollection() {
        return stageCollection;
    }

    public void setStageCollection(Collection<Stage> stageCollection) {
        this.stageCollection = stageCollection;
    }

    public TypeDep getIdTypeDep() {
        return idTypeDep;
    }

    public void setIdTypeDep(TypeDep idTypeDep) {
        this.idTypeDep = idTypeDep;
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
        if (!(object instanceof Dep)) {
            return false;
        }
        Dep other = (Dep) object;
        if ((this.id == null && other.id != null) || (this.id != null && !this.id.equals(other.id))) {
            return false;
        }
        return true;
    }

    @Override
    public String toString() {
        return num + " " + idTypeDep.toString();
    }
    
}
