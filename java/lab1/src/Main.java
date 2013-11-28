import unit.*;

public class Main {
    public static void main(String[] args) {
        Unit t = new Unit(100.45789889, Unit.UnitTypes.Kg, 2);
        System.out.println("current=>");
        System.out.println(t);
        System.out.println("convert=>");
        System.out.println("Gram: " + t.convertTo(Unit.UnitTypes.Gram));
        System.out.println("Pound: " + t.convertTo(Unit.UnitTypes.Pound, 4));
        System.out.println("Ounce: " + t.convertTo(Unit.UnitTypes.Ounce));
    }
}
