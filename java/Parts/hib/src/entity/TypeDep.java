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
@Table(name = "TYPE_DEP", uniqueConstraints = {
    @UniqueConstraint(columnNames = {"NAME"})})
@NamedQueries({
    @NamedQuery(name = "TypeDep.findAll", query = "SELECT t FROM TypeDep t"),
    @NamedQuery(name = "TypeDep.findById", query = "SELECT t FROM TypeDep t WHERE t.id = :id"),
    @NamedQuery(name = "TypeDep.findByName", query = "SELECT t FROM TypeDep t WHERE t.name = :name")})
public class TypeDep implements Serializable {
    private static final long serialVersionUID = 1L;
    // @Max(value=?)  @Min(value=?)//if you know range of your decimal fields consider using these annotations to enforce field validation
    @Id
    @GeneratedValue(strategy=GenerationType.SEQUENCE, generator="tSequenceGenerator")
    @SequenceGenerator(allocationSize=1, schema = "PARTS", name="tSequenceGenerator", sequenceName = "SEQ_PART")
    @Basic(optional = false)
    @Column(name = "ID", nullable = false, precision = 0, scale = -127)
    private BigDecimal id;
    @Basic(optional = false)
    @Column(name = "NAME", nullable = false, length = 256)
    private String name;
    @OneToMany(cascade = CascadeType.ALL, mappedBy = "idTypeDep", fetch = FetchType.LAZY)
    private Collection<Dep> depCollection;

    public TypeDep() {
    }

    public TypeDep(BigDecimal id) {
        this.id = id;
    }

    public TypeDep(BigDecimal id, String name) {
        this.id = id;
        this.name = name;
    }

    public BigDecimal getId() {
        return id;
    }

    public void setId(BigDecimal id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public Collection<Dep> getDepCollection() {
        return depCollection;
    }

    public void setDepCollection(Collection<Dep> depCollection) {
        this.depCollection = depCollection;
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
        if (!(object instanceof TypeDep)) {
            return false;
        }
        TypeDep other = (TypeDep) object;
        if ((this.id == null && other.id != null) || (this.id != null && !this.id.equals(other.id))) {
            return false;
        }
        return true;
    }

    @Override
    public String toString() {
        return name;
    }
    
}
