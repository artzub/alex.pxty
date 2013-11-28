package bookcase;

public class Book implements Comparable<Book> {

    public enum Genres {
        ScienceFiction,
        Fantasy,
        Classic,
        Other
    }

    public enum CoverTypes {
        Hard,
        Soft
    }

    private String title;
    private String author;
    private int pageCount;
    private Genres genre;
    private CoverTypes coverTypes;

    private void initBook(String title, String author, int pageCount, Genres genre, CoverTypes coverTypes) {
        this.title = title;
        this.author = author;
        this.pageCount = pageCount;
        this.genre = genre;
        this.coverTypes = coverTypes;
    }

    /**
     * Create the new instance of <see cref="Book">Book</see>.
     * @param title
     * @param author
     * @param pageCount
     * @param genre
     * @param coverTypes
     */
    public Book(String title, String author, int pageCount, Genres genre, CoverTypes coverTypes) {
        initBook(title, author, pageCount, genre, coverTypes);
    }

    public Book(String title, String author) {
        initBook(title, author, 1, Genres.Other, CoverTypes.Soft);
    }

    public Book(String title, String author, Genres genre) {
        initBook(title, author, 1, genre, CoverTypes.Soft);
    }

    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    public String getAuthor() {
        return author;
    }

    public void setAuthor(String author) {
        this.author = author;
    }

    public int getPageCount() {
        return pageCount;
    }

    public void setPageCount(int pageCount) {
        this.pageCount = pageCount;
    }

    public Genres getGenre() {
        return genre;
    }

    public void setGenre(Genres genre) {
        this.genre = genre;
    }

    public CoverTypes getCoverTypes() {
        return coverTypes;
    }

    public void setCoverTypes(CoverTypes coverTypes) {
        this.coverTypes = coverTypes;
    }

    @Override
    public boolean equals(Object obj) {
        if (obj == null || !(obj instanceof Book))
            return false;

        boolean result = obj == this;

        if (!result) {
            Book book = (Book) obj;
            result = book.author.equals(author)
                && book.title.equals(title)
                && book.pageCount == pageCount
                && book.genre == genre
                && book.coverTypes == coverTypes;
        }

        return result;
    }

    @Override
    public String toString() {
        return String.format("%s by %s (%s)", title, author, genre);
    }

    @Override
    public int compareTo(Book book) {
        if (book == null)
            return 1;

        return toString().compareTo(book.toString());
    }

    public int compareToByGenre(Book book) {
        if (book == null)
            return 1;

        return genre.toString().compareTo(book.genre.toString());
    }

    public int compareToByAuthor(Book book) {
        if (book == null)
            return 1;

        return author.compareTo(book.author);
    }

    public int compareToByTitle(Book book) {
        if (book == null)
            return 1;

        return title.compareTo(book.title);
    }
}
