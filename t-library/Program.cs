public class Program
{
    public static void Main(string[] args)
    {
        Library library = new Library();

        while (true)
        {
            Console.WriteLine("T-Библиотека");
            Console.WriteLine("1. Добавить книгу");
            Console.WriteLine("2. Просмотреть все книги");
            Console.WriteLine("3. Удалить книгу");
            Console.WriteLine("4. Поиск книги");
            Console.WriteLine("5. Изменить статус книги");
            Console.WriteLine("6. Просмотреть избранное");
            Console.WriteLine("7. Выход");
            Console.Write("Выбор: ");

            switch (Console.ReadLine())
            {
                case "1":
                    library.AddBook();
                    break;
                case "2":
                    library.ViewBooks();
                    break;
                case "3":
                    library.DeleteBook();
                    break;
                case "4":
                    library.SearchBooks();
                    break;
                case "5":
                    library.UpdateBookStatus();
                    break;
                case "6":
                    library.ViewFavorites();
                    break;
                case "7":
                    return;
                default:
                    Console.WriteLine("Такого варианта нет");
                    break;
            }
        }
    }
}
