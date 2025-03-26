
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

public static class SemaforoBasico
{
    private static readonly SemaphoreSlim _semaforo = new(1);

    public static string Acceder()
    {
        _semaforo.Wait();
        try { return "Acceso permitido a sección crítica"; }
        finally { _semaforo.Release(); }
    }
}

public static class SemaforoMultiplesHilos
{
    private static readonly SemaphoreSlim _semaforo = new(3);

    public static string Acceder(string nombre)
    {
        _semaforo.Wait();
        try { return $"{nombre} está accediendo"; }
        finally { _semaforo.Release(); }
    }
}

public static class SemaforoConTimeout
{
    private static readonly SemaphoreSlim _semaforo = new(1);

    public static string IntentarAcceder()
    {
        if (_semaforo.Wait(500))
        {
            try { return "Acceso exitoso"; }
            finally { _semaforo.Release(); }
        }
        return "Timeout esperando el semáforo";
    }
}

public static class TareaPesada
{
    private static readonly SemaphoreSlim _semaforo = new(2);

    public static void Procesar(string nombre)
    {
        _semaforo.Wait();
        try
        {
            Console.WriteLine($"{nombre} procesando...");
            Thread.Sleep(1000);
            Console.WriteLine($"{nombre} finalizó.");
        }
        finally { _semaforo.Release(); }
    }
}

public static class ListaProtegida
{
    private static readonly SemaphoreSlim _semaforo = new(1);
    private static List<string> _mensajes = new();

    public static void Agregar(string mensaje)
    {
        _semaforo.Wait();
        try { _mensajes.Add(mensaje); }
        finally { _semaforo.Release(); }
    }

    public static string Obtener()
    {
        _semaforo.Wait();
        try { return string.Join(", ", _mensajes); }
        finally { _semaforo.Release(); }
    }
}

public static class ControlDeStock
{
    private static readonly SemaphoreSlim _semaforo = new(1);
    private static int _stock = 5;

    public static string Comprar(string usuario)
    {
        _semaforo.Wait();
        try
        {
            if (_stock > 0)
            {
                _stock--;
                return $"{usuario} compró. Stock: {_stock}";
            }
            return $"{usuario} no pudo comprar. Sin stock.";
        }
        finally { _semaforo.Release(); }
    }
}

public static class EscrituraArchivo
{
    private static readonly SemaphoreSlim _semaforo = new(1);

    public static void Escribir(string texto)
    {
        _semaforo.Wait();
        try
        {
            File.AppendAllText("log_semaforo.txt", texto + Environment.NewLine);
        }
        finally { _semaforo.Release(); }
    }
}

public static class Logger
{
    private static readonly SemaphoreSlim _semaforo = new(1);

    public static void Log(string mensaje)
    {
        _semaforo.Wait();
        try { Console.WriteLine($"[LOG] {mensaje}"); }
        finally { _semaforo.Release(); }
    }
}

public static class ProductorConsumidor
{
    private static readonly SemaphoreSlim _semaforo = new(1);
    private static Queue<int> _cola = new();

    public static void Producir(int dato)
    {
        _semaforo.Wait();
        try { _cola.Enqueue(dato); }
        finally { _semaforo.Release(); }
    }

    public static string Consumir()
    {
        _semaforo.Wait();
        try
        {
            return _cola.Count > 0 ? $"Consumido: {_cola.Dequeue()}" : "Cola vacía";
        }
        finally { _semaforo.Release(); }
    }
}

public static class RecursosLimitados
{
    private static readonly SemaphoreSlim _semaforo = new(2);

    public static void Ejecutar(string nombre)
    {
        _semaforo.Wait();
        try
        {
            Console.WriteLine($"{nombre} accediendo recurso compartido...");
            Thread.Sleep(1000);
            Console.WriteLine($"{nombre} finalizó.");
        }
        finally { _semaforo.Release(); }
    }
}
