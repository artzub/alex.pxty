import bookcase.*;

import java.util.ArrayList;

public class Main {
    public static void main(String[] args) {
        Shelf<Book> bookshelf = new Bookshelf();
        Book test = new Book("Hyperion Cantos", "Dan Simmons", Book.Genres.ScienceFiction);

        System.out.println("Book test: " + test);

        System.out.println("Add book:");
        bookshelf.add(test);
        System.out.println(bookshelf);
        System.out.println();

        System.out.println("Add book:");
        bookshelf.add(new Book("Dune Messiah", "Frank Herbert", Book.Genres.ScienceFiction));
        System.out.println(bookshelf);

        System.out.println();

        System.out.println("Add book after test:");
        bookshelf.addAfter(test, new Book("Dune", "Frank Herbert", Book.Genres.ScienceFiction));
        System.out.println(bookshelf);
        System.out.println();

        System.out.println("Add book before test:");
        bookshelf.addBefore(test, new Book("Забыть резервацию", "Алексей Калугин", Book.Genres.ScienceFiction));
        System.out.println(bookshelf);
        System.out.println();

        System.out.println("Add book first:");
        bookshelf.addFirst(new Book("Ender's Game", "Orson Scott Card", Book.Genres.ScienceFiction));
        System.out.println(bookshelf);
        System.out.println();

        System.out.println("Add book last:");
        bookshelf.addLast(new Book("Foundation", "Isaac Asimov"));
        System.out.println(bookshelf);
        System.out.println();

        System.out.println("Sorting:");
        bookshelf.sort(false);
        System.out.println(bookshelf);
        System.out.println();

        System.out.println("Sorting desc:");
        bookshelf.sort(true);
        System.out.println(bookshelf);
        System.out.println();

        System.out.println("Sorting by author desc:");
        ((Bookshelf)bookshelf).sortByAuthor(true);
        System.out.println(bookshelf);
        System.out.println();

        System.out.println("Sorting by title:");
        ((Bookshelf)bookshelf).sortByTitle(false);
        System.out.println(bookshelf);
        System.out.println();

        System.out.println("Sorting by genre desc:");
        ((Bookshelf)bookshelf).sortByGenre(true);
        System.out.println(bookshelf);
        System.out.println();

        System.out.println("Book test: " + test);

        System.out.println("Has book test:" + bookshelf.hasItem(test));

        System.out.println("Remove test");
        bookshelf.remove(test);

        System.out.println("Has book test:" + bookshelf.hasItem(test));
        System.out.println();

        System.out.println("Books by Frank Herbert:" + ((Bookshelf) bookshelf).getBooksByAuthor("Frank Herbert"));
    }
}
