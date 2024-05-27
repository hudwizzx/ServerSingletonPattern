using System;
using System.Collections.Generic;
using System.Threading;

public sealed class Servers
{
    // Ленивая инициализация экземпляра класса с использованием Lazy<T>
    private static readonly Lazy<Servers> lazyInstance = new Lazy<Servers>(() => new Servers());

    // Список серверов
    private List<string> serversList = new List<string>();

    // Объект для блокировки при многопоточном доступе
    private static readonly object lockObject = new object();

    // Приватный конструктор для предотвращения создания экземпляров класса извне
    private Servers() { }

    // Метод для доступа к единственному экземпляру класса
    public static Servers Instance
    {
        get { return lazyInstance.Value; }
    }

    // Добавление сервера в список
    public bool AddServer(string server)
    {
        // Проверка адреса сервера
        if (!server.StartsWith("http://") && !server.StartsWith("https://"))
        {
            Console.WriteLine("Ошибка: Добавление возможно только для адресов, начинающихся с 'http' или 'https'.");
            return false;
        }

        lock (lockObject)
        {
            // Проверка на дубликаты
            if (serversList.Contains(server))
            {
                Console.WriteLine("Ошибка: Сервер уже существует в списке.");
                return false;
            }

            serversList.Add(server);
        }

        return true;
    }

    // Получить список серверов, адреса которых начинаются с 'http'
    public List<string> GetHttpServers()
    {
        List<string> httpServers = new List<string>();

        lock (lockObject)
        {
            foreach (var server in serversList)
            {
                if (server.StartsWith("http://"))
                {
                    httpServers.Add(server);
                }
            }
        }

        return httpServers;
    }

    // Получить список серверов, адреса которых начинаются с 'https'
    public List<string> GetHttpsServers()
    {
        List<string> httpsServers = new List<string>();

        lock (lockObject)
        {
            foreach (var server in serversList)
            {
                if (server.StartsWith("https://"))
                {
                    httpsServers.Add(server);
                }
            }
        }

        return httpsServers;
    }
}
