package bookcase;

import java.util.*;

public class Bookshelf implements Shelf<Book> {
    private Map<String, List<Book>> booksByAuthor;
    private List<Book> books;
    private Comparator<Book> defaultComparator;
    private Comparator<Book> defaultComparatorDesc;

    private enum SortBy {
        None,
        Title,
        Author,
        Genre
    }

    @Override
    public void add(Book book) {
        if (book == null)
            return;

        getBooks().add(book);
        addToBooksByAuthor(book);
    }

    @Override
    public void addFirst(Book book) {
        addBefore(null, book);
    }

    @Override
    public void addLast(Book book) {
        add(book);
    }

    @Override
    public void addBefore(Book cur, Book book) {
        int index = -1;
        if (book == null)
            return;

        if (cur != null)
            index = getBooks().indexOf(cur);

        if (index < 0)
            index = 0;
        getBooks().add(index, book);
        addToBooksByAuthor(book);
    }

    @Override
    public void addAfter(Book cur, Book book) {
        int index = getBooks().isEmpty() ? 0 : getBooks().size() - 1;
        if (book == null)
            return;

        if (cur != null && !books.isEmpty()) {
            index = books.indexOf(cur);
            index += index > -1 ? 1 : 0;
        }

        books.add(index, book);

        addToBooksByAuthor(book);
    }

    public List<Book> getBooksByAuthor(String author) {
        if (author.isEmpty())
            return null;

        return getBookByAuthor().get(author);
    }

    public List<Book> getBooksByAuthor(Book book) {
        return getBooksByAuthor(book != null ? book.getAuthor() : "");
    }

    @Override
    public void remove(Book book) {
        if(book == null)
            return;

        getBooks().remove(book);
        removeFromBooksByAuthor(book);
    }

    @Override
    public void sort(boolean desc) {
        sortBy(SortBy.None, desc);
    }

    public void sortByAuthor(boolean desc) {
        sortBy(SortBy.Author, desc);
    }

    public void sortByTitle(boolean desc) {
        sortBy(SortBy.Title, desc);
    }

    public void sortByGenre(boolean desc) {
        sortBy(SortBy.Genre, desc);
    }

    private void sortBy(SortBy type, boolean desc) {
        if(getBooks().isEmpty())
            return;

        Collections.sort(books,getBookComparator(type, desc));

    }

    @Override
    public boolean hasItem(Book book) {
        return book != null
                && !getBooks().isEmpty()
                && books.contains(book);
    }

    private Map<String, List<Book>> getBookByAuthor() {
        if (booksByAuthor == null)
            booksByAuthor = new HashMap<String, List<Book>>();
        return booksByAuthor;
    }

    private boolean addToBooksByAuthor(Book book){
        String author = book != null ? book.getAuthor() : "";

        if (author.isEmpty())
            return false;

        List<Book> list = getBookByAuthor().get(author);
        if(list == null) {
            list = new LinkedList<Book>();
            booksByAuthor.put(author, list);
        }

        return list.add(book);
    }

    private boolean removeFromBooksByAuthor(Book book) {
        String author = book != null ? book.getAuthor() : "";

        if (author.isEmpty())
            return false;

        List<Book> list = getBookByAuthor().get(author);
        return list == null || list.isEmpty() || !list.contains(book) || list.remove(book);
    }

    private List<Book> getBooks() {
        if (books == null)
            books = new ArrayList<Book>();
        return books;
    }

    @Override
    public String toString() {
        return Arrays.toString(getBooks().toArray());
    }

    private Comparator<Book> getBookComparator(SortBy type,final boolean desc) {
        if (!desc && defaultComparator == null) {
            defaultComparator = new Comparator<Book>() {
                @Override
                public int compare(Book o1, Book o2) {
                    return o1.compareTo(o2);
                }
            };
        }

        if (desc && defaultComparatorDesc == null) {
            defaultComparatorDesc = new Comparator<Book>() {
                @Override
                public int compare(Book o1, Book o2) {
                    return o2.compareTo(o1);
                }
            };
        }

        Comparator<Book> bookComparator = desc ? defaultComparatorDesc : defaultComparator;

        switch (type) {
            case Title:
                bookComparator = new Comparator<Book>() {
                    @Override
                    public int compare(Book o1, Book o2) {
                        return desc ? o2.compareToByTitle(o1) : o1.compareToByTitle(o2);
                    }
                };
                break;
            case Author:
                bookComparator = new Comparator<Book>() {
                    @Override
                    public int compare(Book o1, Book o2) {
                        return desc ? o2.compareToByAuthor(o1) : o1.compareToByAuthor(o2);
                    }
                };
                break;
            case Genre:
                bookComparator = new Comparator<Book>() {
                    @Override
                    public int compare(Book o1, Book o2) {
                        return desc ? o2.compareToByGenre(o1) : o1.compareToByGenre(o2);
                    }
                };
                break;
        }

        return bookComparator;
    }
}