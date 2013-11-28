package bookcase;

public interface Shelf<T> {
    void add(T item);
    void addFirst(T item);
    void addLast(T item);
    void addBefore(T cur, T item);
    void addAfter(T cur, T item);
    void remove(T book);
    void sort(boolean desc);
    boolean hasItem(T book);
}