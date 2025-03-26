
using System;
using System.Threading;

public static class SemaphoreSlimExamples
{
    private static SemaphoreSlim _semaforo = new(3);
    private static SemaphoreSlim _limiteAPI = new(2);
    private static SemaphoreSlim _impresorasDisponibles = new(2);
    private static SemaphoreSlim _accesoBD = new(1);
    private static SemaphoreSlim _configFileAccess = new(1);
    private static SemaphoreSlim _reservas = new(2);
    private static SemaphoreSlim _lecturaConcurrencia = new(3);
    private static SemaphoreSlim _checkout = new(2);
    private static SemaphoreSlim _reporte = new(1);
    private static SemaphoreSlim _ftpAcceso = new(2);

    public static void AccederRecurso(string nombre)
    {
        _semaforo.Wait();
        try
        {
            Console.WriteLine($"{nombre} accediendo al recurso...");
            Thread.Sleep(500);
        }
        finally
        {
            Console.WriteLine($"{nombre} liberando recurso.");
            _semaforo.Release();
        }
    }

    public static void SimularConexion(string cliente)
    {
        _limiteAPI.Wait();
        try
        {
            Console.WriteLine($"{cliente} conectado a la API.");
            Thread.Sleep(700);
        }
        finally
        {
            Console.WriteLine($"{cliente} finalizó su conexión.");
            _limiteAPI.Release();
        }
    }

    public static void ImprimirDocumento(string documento)
    {
        _impresorasDisponibles.Wait();
        try
        {
            Console.WriteLine($"Imprimiendo {documento}...");
            Thread.Sleep(600);
        }
        finally
        {
            Console.WriteLine($"{documento} finalizó impresión.");
            _impresorasDisponibles.Release();
        }
    }

    public static void AccederBD(string hilo)
    {
        if (_accesoBD.Wait(500))
        {
            try
            {
                Console.WriteLine($"{hilo} accedió a la base de datos.");
                Thread.Sleep(400);
            }
            finally
            {
                Console.WriteLine($"{hilo} liberó acceso BD.");
                _accesoBD.Release();
            }
        }
        else
        {
            Console.WriteLine($"{hilo} no pudo acceder (timeout).");
        }
    }

    public static void LeerArchivoConfiguracion(string lector)
    {
        _configFileAccess.Wait();
        try
        {
            Console.WriteLine($"{lector} leyendo configuración...");
            Thread.Sleep(300);
        }
        finally
        {
            Console.WriteLine($"{lector} terminó de leer.");
            _configFileAccess.Release();
        }
    }

    public static void RealizarReserva(string usuario)
    {
        _reservas.Wait();
        try
        {
            Console.WriteLine($"{usuario} realizando reserva...");
            Thread.Sleep(400);
        }
        finally
        {
            Console.WriteLine($"{usuario} finalizó su reserva.");
            _reservas.Release();
        }
    }

    public static void LeerDatos(string lector)
    {
        _lecturaConcurrencia.Wait();
        try
        {
            Console.WriteLine($"{lector} leyendo datos compartidos...");
            Thread.Sleep(300);
        }
        finally
        {
            Console.WriteLine($"{lector} terminó de leer.");
            _lecturaConcurrencia.Release();
        }
    }

    public static void ProcesarPago(string cliente)
    {
        _checkout.Wait();
        try
        {
            Console.WriteLine($"{cliente} procesando pago...");
            Thread.Sleep(600);
        }
        finally
        {
            Console.WriteLine($"{cliente} completó pago.");
            _checkout.Release();
        }
    }

    public static void GenerarReporte(string origen)
    {
        _reporte.Wait();
        try
        {
            Console.WriteLine($"{origen} generando reporte...");
            Thread.Sleep(500);
        }
        finally
        {
            Console.WriteLine($"{origen} liberó generador de reportes.");
            _reporte.Release();
        }
    }

    public static void SubirArchivo(string usuario)
    {
        _ftpAcceso.Wait();
        try
        {
            Console.WriteLine($"{usuario} subiendo archivo...");
            Thread.Sleep(700);
        }
        finally
        {
            Console.WriteLine($"{usuario} terminó la subida.");
            _ftpAcceso.Release();
        }
    }
}
