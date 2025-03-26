
using System;
using System.Threading;

class Program
{
    static void Main()
    {
        Console.WriteLine("🧪 Ejecutando ejemplos de SemaphoreSlim en C#...");

        Console.WriteLine("----------Ejemplo 1----------");
        for (int i = 1; i <= 5; i++)
            new Thread(() => SemaphoreSlimExamples.AccederRecurso($"Hilo{i}")).Start();
        Thread.Sleep(1000);

        Console.WriteLine("----------Ejemplo 2----------");
        for (int i = 1; i <= 4; i++)
            new Thread(() => SemaphoreSlimExamples.SimularConexion($"Cliente{i}")).Start();
        Thread.Sleep(1200);

        Console.WriteLine("----------Ejemplo 3----------");
        for (int i = 1; i <= 3; i++)
            new Thread(() => SemaphoreSlimExamples.ImprimirDocumento($"Documento{i}")).Start();
        Thread.Sleep(1000);

        Console.WriteLine("----------Ejemplo 4----------");
        new Thread(() => SemaphoreSlimExamples.AccederBD("DBThread")).Start();
        Thread.Sleep(600);

        Console.WriteLine("----------Ejemplo 5----------");
        for (int i = 1; i <= 2; i++)
            new Thread(() => SemaphoreSlimExamples.LeerArchivoConfiguracion($"Lector{i}")).Start();
        Thread.Sleep(800);

        Console.WriteLine("----------Ejemplo 6----------");
        for (int i = 1; i <= 3; i++)
            new Thread(() => SemaphoreSlimExamples.RealizarReserva($"Usuario{i}")).Start();
        Thread.Sleep(1000);

        Console.WriteLine("----------Ejemplo 7----------");
        for (int i = 1; i <= 5; i++)
            new Thread(() => SemaphoreSlimExamples.LeerDatos($"Lector{i}")).Start();
        Thread.Sleep(1000);

        Console.WriteLine("----------Ejemplo 8----------");
        for (int i = 1; i <= 3; i++)
            new Thread(() => SemaphoreSlimExamples.ProcesarPago($"Cliente{i}")).Start();
        Thread.Sleep(1000);

        Console.WriteLine("----------Ejemplo 9----------");
        for (int i = 1; i <= 2; i++)
            new Thread(() => SemaphoreSlimExamples.GenerarReporte($"Origen{i}")).Start();
        Thread.Sleep(1000);

        Console.WriteLine("----------Ejemplo 10----------");
        for (int i = 1; i <= 4; i++)
            new Thread(() => SemaphoreSlimExamples.SubirArchivo($"Usuario{i}")).Start();

        Thread.Sleep(2000);
        Console.WriteLine("✅ Fin de los ejemplos.");
    }
}
