using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

public class Library
{
    private List<Book> books = new List<Book>();
    private const string FilePath = "library.json";

    public Library()
    {
        LoadBooks();
    }
    public void AddBook()
    {
        Console.WriteLine("Добавить книгу:");
        Console.Write("Название: ");
        string title = Console.ReadLine();
        Console.Write("Автор: ");
        string author = Console.ReadLine();
        Console.Write("Жанр: ");
        string genre = Console.ReadLine();
        Console.Write("Год издания: ");
        int.TryParse(Console.ReadLine(), out int year);
        Console.Write("Краткое описание: ");
        string description = Console.ReadLine();
        books.Add(new Book { Title = title, Author = author, Genre = genre, Year = year, Description = description });
        SaveBooks();
        Console.WriteLine("Книга добавлена");
    }

    public void ViewBooks()
    {
        if (books.Count == 0)
        {
            Console.WriteLine("Библиотека пустая");
            return;
        }
        Console.WriteLine("Сортировка:");
        Console.WriteLine("1. По названию");
        Console.WriteLine("2. По автору");
        Console.WriteLine("3. По году");
        Console.Write("Выберите нужный вариант: ");
        string sortOption = Console.ReadLine();
        var sortedBooks = books.AsEnumerable();

        switch (sortOption)
        {
            case "1":
                sortedBooks = books.OrderBy(b => b.Title);
                break;
            case "2":
                sortedBooks = books.OrderBy(b => b.Author);
                break;
            case "3":
                sortedBooks = books.OrderBy(b => b.Year);
                break;
        }
        Console.WriteLine("Все книги:");
        foreach (var book in sortedBooks)
        {
            Console.WriteLine($"- '{book.Title}' от {book.Author} ({book.Year}), Жанр: {book.Genre}, Статус: {(book.IsRead ? "Прочитана" : "Не прочитана")}, {(book.IsFavorite ? "В избранном" : "")}");
        }
    }

    public void DeleteBook()
    {
        Console.WriteLine("Удалить:");
        for (int i = 0; i < books.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {books[i].Title}");
        }
        Console.Write("Введите номер для удаления: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= books.Count)
        {
            books.RemoveAt(index - 1);
            SaveBooks();
            Console.WriteLine("Книга удалена");
        }
        else
        {
            Console.WriteLine("Неверный номер");
        }
    }

    public void SearchBooks()
    {
        Console.Write("Введите слово для поиска: ");
        string keyword = Console.ReadLine();
        var foundBooks = books.Where(b => (b.Title != null && b.Title.ToLower().Contains(keyword)) || 
                                           (b.Author != null && b.Author.ToLower().Contains(keyword)) || 
                                           (b.Description != null && b.Description.ToLower().Contains(keyword)));

        if (!foundBooks.Any())
        {
            Console.WriteLine("Книги не найдены");
            return;
        }

        Console.WriteLine("Найденные книги:");
        foreach (var book in foundBooks)
        {
            Console.WriteLine($"- '{book.Title}' от {book.Author}");
        }
    }

    public void UpdateBookStatus()
    {
        Console.WriteLine("Изменение статуса книги: ");
        for (int i = 0; i < books.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {books[i].Title}");
        }
        Console.Write("Введите номер книги для изменения: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= books.Count)
        {
            Book book = books[index - 1];
            Console.WriteLine($"Выбрана книга: '{book.Title}'");
            Console.WriteLine("1. Изменить информацию о прочтении");
            Console.WriteLine("2. Добавить/удалить из избранного");
            Console.Write("Выберите действие: ");
            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    book.IsRead = !book.IsRead;
                    Console.WriteLine($"Статус изменен на '{(book.IsRead ? "Прочитана" : "Не прочитана")}'");
                    break;
                case "2":
                    book.IsFavorite = !book.IsFavorite;
                    Console.WriteLine(book.IsFavorite ? "Книга добавлена в избранное" : "Книга удалена из избранного");
                    break;
                default:
                    Console.WriteLine("Неверный выбор");
                    break;
            }
            SaveBooks();
        }
        else
        {
            Console.WriteLine("Такого варианта нет");
        }
    }
    
    public void ViewFavorites()
    {
        var favoriteBooks = books.Where(b => b.IsFavorite);
        if (!favoriteBooks.Any())
        {
            Console.WriteLine("Список избранных книг пуст");
            return;
        }
        Console.WriteLine("Избранные книги: ");
        foreach (var book in favoriteBooks)
        {
            Console.WriteLine($"- '{book.Title}' от {book.Author}");
        }
    }
    public void SaveBooks()
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
        };
        string json = JsonSerializer.Serialize(books, options);
        File.WriteAllText(FilePath, json);
    }
    private void LoadBooks()
    {
        if (File.Exists(FilePath))
        {
            string json = File.ReadAllText(FilePath);
            books = JsonSerializer.Deserialize<List<Book>>(json) ?? new List<Book>();
        }
    }
}