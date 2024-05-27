class Program
{
    static void Main(string[] args)
    {
        // Получение экземпляра класса Servers
        Servers servers = Servers.Instance;

        // Добавление серверов
        servers.AddServer("http://example.com");
        servers.AddServer("https://example.org");
        servers.AddServer("ftp://invalid.com"); // Не будет добавлен

        // Получение списков серверов
        List<string> httpServers = servers.GetHttpServers();
        List<string> httpsServers = servers.GetHttpsServers();

        // Вывод списков на консоль
        Console.WriteLine("Серверы с адресами, начинающимися с 'http':");
        foreach (var server in httpServers)
        {
            Console.WriteLine(server);
        }

        Console.WriteLine("\nСерверы с адресами, начинающимися с 'https':");
        foreach (var server in httpsServers)
        {
            Console.WriteLine(server);
        }
    }
}
