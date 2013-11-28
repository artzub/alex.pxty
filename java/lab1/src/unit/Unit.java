package unit;

import java.math.BigDecimal;
import java.math.MathContext;
import java.math.RoundingMode;

public class Unit {
    public enum UnitTypes {
        Gram,
        Kg,
        Pound,
        Ounce
    }

    private BigDecimal amount;
    private UnitTypes typeUnit;
    private int digits;

    public void setupUnit(BigDecimal amount, UnitTypes type, int digits) {
        this.amount = amount;
        this.typeUnit = type;
        this.digits = digits;
    }

    public Unit(BigDecimal amount, UnitTypes type) {
        setupUnit(amount, type, 2);
    }

    public Unit(double amount, UnitTypes type) {
        setupUnit(new BigDecimal(amount), type, 2);
    }

    public Unit(BigDecimal amount, UnitTypes type, int digits) {
        setupUnit(amount, type, digits);
    }

    public Unit(double amount, UnitTypes type, int digits) {
        setupUnit(new BigDecimal(amount), type, digits);
    }

    public BigDecimal getAmount() {
        BigDecimal temp = amount;
        if (amount.scale() != digits)
            temp = amount.setScale(digits, BigDecimal.ROUND_HALF_DOWN);
        return temp;
    }

    public Unit convertTo(UnitTypes type) {
        return convertTo(type, digits);
    }

    public Unit convertTo(UnitTypes type, int digits) {
        double coff = 1,
               scale = 1;

        switch (typeUnit) {
            case Gram:
                switch (type) {
                    case Pound:
                        coff = 2.20462;
                    case Kg:
                        scale = 0.001;
                        break;
                    case Ounce:
                        coff = 0.035274;
                        break;
                }
                break;
            case Kg:
                switch (type) {
                    case Ounce:
                        coff = 0.035274;
                    case Gram:
                        scale = 1000;
                        break;
                    case Pound:
                        coff = 2.20462;
                        break;
                }
                break;
            case Pound:
                switch (type) {
                    case Gram:
                        coff = 28.3495;
                    case Ounce:
                        scale = 16;
                        break;
                    case Kg:
                        coff = 0.45359;
                        break;
                }
                break;
            case Ounce:
                switch (type) {
                    case Kg:
                        scale = 0.001;
                    case Gram:
                        coff = 28.3495;
                        break;
                    case Pound:
                        scale = 1/16;
                        break;
                }
                break;
        }

        return new Unit(new BigDecimal(amount.doubleValue() * scale * coff), type, digits);
    }

    public Unit add(Unit value) {
        BigDecimal temp = value.amount;
        if (typeUnit != value.typeUnit)
            temp = value.convertTo(typeUnit).amount;
        amount = amount.add(temp);
        return this;
    }

    public Unit subtract(Unit value) {
        BigDecimal temp = value.amount;
        if (typeUnit != value.typeUnit)
            temp = value.convertTo(typeUnit).amount;
        amount = amount.subtract(temp);
        return this;
    }

    public Unit divide(Unit value) {
        BigDecimal temp = value.amount;
        if (typeUnit != value.typeUnit)
            temp = value.convertTo(typeUnit).amount;
        amount = amount.divide(temp);
        return this;
    }

    public Unit multiply(Unit value) {
        BigDecimal temp = value.amount;
        if (typeUnit != value.typeUnit)
            temp = value.convertTo(typeUnit).amount;
        amount = amount.multiply(temp);
        return this;
    }

    @Override
    public String toString() {
        return String.format("%." + digits + "f %s", getAmount(), typeUnit);
    }
}