import java.math.BigDecimal;
import java.math.MathContext;
import java.math.RoundingMode;

public class Main {

    public enum UnitType {Pound, Kg, Gram, Ounce}

    public static class Unit {
        private BigDecimal amount;
        private UnitType typeUnit;
        private int digits;

        public void setupUnit(BigDecimal amount, UnitType type, int digits) {
            this.amount = amount;
            this.typeUnit = type;
            this.digits = digits;
        }

        public Unit(BigDecimal amount, UnitType type) {
            setupUnit(amount, type, 2);
        }

        public Unit(double amount, UnitType type) {
            setupUnit(new BigDecimal(amount), type, 2);
        }

        public Unit(BigDecimal amount, UnitType type, int digits) {
            setupUnit(amount, type, digits);
        }

        public Unit(double amount, UnitType type, int digits) {
            setupUnit(new BigDecimal(amount), type, digits);
        }

        public  BigDecimal getAmount() {
            return amount.round(new MathContext(digits, RoundingMode.HALF_UP));
        }


        @Override
        public String toString() {
            return getAmount() + " " + typeUnit;
        }
    }

    public static void main(String[] args) {
        Unit t = new Unit(100.4545, UnitType.Kg, 2);
        System.out.println("test");
        System.out.println(t);
    }
}
