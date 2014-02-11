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
@Table(name = "SURFACE", uniqueConstraints = {
    @UniqueConstraint(columnNames = {"NAME"})})
@NamedQueries({
    @NamedQuery(name = "Surface.findAll", query = "SELECT s FROM Surface s"),
    @NamedQuery(name = "Surface.findById", query = "SELECT s FROM Surface s WHERE s.id = :id"),
    @NamedQuery(name = "Surface.findByName", query = "SELECT s FROM Surface s WHERE s.name = :name")})
public class Surface implements Serializable {
    private static final long serialVersionUID = 1L;
    // @Max(value=?)  @Min(value=?)//if you know range of your decimal fields consider using these annotations to enforce field validation
    @Id
    @GeneratedValue(strategy=GenerationType.SEQUENCE, generator="surSequenceGenerator")
    @SequenceGenerator(allocationSize=1, schema = "PARTS", name="surSequenceGenerator", sequenceName = "SEQ_SURFACE")
    @Basic(optional = false)
    @Column(name = "ID", nullable = false, precision = 0, scale = -127)
    private BigDecimal id;
    @Basic(optional = false)
    @Column(name = "NAME", nullable = false, length = 256)
    private String name;
    @OneToMany(cascade = CascadeType.ALL, mappedBy = "idSurface", fetch = FetchType.LAZY)
    private Collection<Stage> stageCollection;

    public Surface() {
    }

    public Surface(BigDecimal id) {
        this.id = id;
    }

    public Surface(BigDecimal id, String name) {
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

    public Collection<Stage> getStageCollection() {
        return stageCollection;
    }

    public void setStageCollection(Collection<Stage> stageCollection) {
        this.stageCollection = stageCollection;
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
        if (!(object instanceof Surface)) {
            return false;
        }
        Surface other = (Surface) object;
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
