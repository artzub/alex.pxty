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
@Table(name = "PART", uniqueConstraints = {
    @UniqueConstraint(columnNames = {"ID_ALLOY", "NAME", "BLNUM"})})
@NamedQueries({
    @NamedQuery(name = "Part.findAll", query = "SELECT p FROM Part p"),
    @NamedQuery(name = "Part.findById", query = "SELECT p FROM Part p WHERE p.id = :id"),
    @NamedQuery(name = "Part.findByName", query = "SELECT p FROM Part p WHERE p.name = :name"),
    @NamedQuery(name = "Part.findByCost", query = "SELECT p FROM Part p WHERE p.cost = :cost"),
    @NamedQuery(name = "Part.findByBlnum", query = "SELECT p FROM Part p WHERE p.blnum = :blnum")})
public class Part implements Serializable {
    private static final long serialVersionUID = 1L;
    // @Max(value=?)  @Min(value=?)//if you know range of your decimal fields consider using these annotations to enforce field validation
    @Id    
    @GeneratedValue(strategy=GenerationType.SEQUENCE, generator="pSequenceGenerator")
    @SequenceGenerator(allocationSize=1, schema = "PARTS", name="pSequenceGenerator", sequenceName = "SEQ_PART")
    @Basic(optional = false)
    @Column(name = "ID", nullable = false, precision = 0, scale = -127)
    private BigDecimal id;
    @Basic(optional = false)
    @Column(name = "NAME", nullable = false, length = 256)
    private String name;
    @Basic(optional = false)
    @Column(name = "COST", nullable = false)
    private BigInteger cost;
    @Basic(optional = false)
    @Column(name = "BLNUM", nullable = false, length = 20)
    private String blnum;
    @OneToMany(cascade = CascadeType.ALL, mappedBy = "idPart", fetch = FetchType.LAZY)
    private Collection<Stage> stageCollection;
    @JoinColumn(name = "ID_ALLOY", referencedColumnName = "ID", nullable = false)
    @ManyToOne(optional = false, fetch = FetchType.LAZY)
    private Alloy idAlloy;

    public Part() {
    }

    public Part(BigDecimal id) {
        this.id = id;
    }

    public Part(BigDecimal id, String name, BigInteger cost, String blnum) {
        this.id = id;
        this.name = name;
        this.cost = cost;
        this.blnum = blnum;
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

    public BigInteger getCost() {
        return cost;
    }

    public void setCost(BigInteger cost) {
        this.cost = cost;
    }

    public String getBlnum() {
        return blnum;
    }

    public void setBlnum(String blnum) {
        this.blnum = blnum;
    }

    public Collection<Stage> getStageCollection() {
        return stageCollection;
    }

    public void setStageCollection(Collection<Stage> stageCollection) {
        this.stageCollection = stageCollection;
    }

    public Alloy getIdAlloy() {
        return idAlloy;
    }

    public void setIdAlloy(Alloy idAlloy) {
        this.idAlloy = idAlloy;
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
        if (!(object instanceof Part)) {
            return false;
        }
        Part other = (Part) object;
        if ((this.id == null && other.id != null) || (this.id != null && !this.id.equals(other.id))) {
            return false;
        }
        return true;
    }

    @Override
    public String toString() {
        return blnum + " " + name + "(" + idAlloy + ")";
    }
    
}
