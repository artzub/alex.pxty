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
@Table(name = "ALLOY", uniqueConstraints = {
    @UniqueConstraint(columnNames = {"NAME"})})
@NamedQueries({
    @NamedQuery(name = "Alloy.findAll", query = "SELECT a FROM Alloy a"),
    @NamedQuery(name = "Alloy.findById", query = "SELECT a FROM Alloy a WHERE a.id = :id"),
    @NamedQuery(name = "Alloy.findByName", query = "SELECT a FROM Alloy a WHERE a.name = :name")})
public class Alloy implements Serializable {
    private static final long serialVersionUID = 1L;
    // @Max(value=?)  @Min(value=?)//if you know range of your decimal fields consider using these annotations to enforce field validation
    @Id
    @GeneratedValue(strategy=GenerationType.SEQUENCE, generator="aSequenceGenerator")
    @SequenceGenerator(allocationSize=1, schema = "PARTS", name="aSequenceGenerator", sequenceName = "SEQ_ALLOY")
    @Basic(optional = false)
    @Column(name = "ID", nullable = false, precision = 0, scale = -127)
    private BigDecimal id;
    @Basic(optional = false)
    @Column(name = "NAME", nullable = false, length = 256)
    private String name;
    @OneToMany(cascade = CascadeType.ALL, mappedBy = "idAlloy", fetch = FetchType.LAZY)
    private Collection<Part> partCollection;

    public Alloy() {
    }

    public Alloy(BigDecimal id) {
        this.id = id;
    }

    public Alloy(BigDecimal id, String name) {
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

    public Collection<Part> getPartCollection() {
        return partCollection;
    }

    public void setPartCollection(Collection<Part> partCollection) {
        this.partCollection = partCollection;
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
        if (!(object instanceof Alloy)) {
            return false;
        }
        Alloy other = (Alloy) object;
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
