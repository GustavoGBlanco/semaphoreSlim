
using System;
using System.Threading;

class SemaphoreExamplesApp
{
    static void Main()
    {
        Console.WriteLine("----------Ejemplo 1----------");
        Console.WriteLine(SemaforoBasico.Acceder());
        Console.WriteLine();

        Console.WriteLine("----------Ejemplo 2----------");
        for (int i = 1; i <= 5; i++)
        {
            string nombre = $"Hilo{i}";
            new Thread(() => Console.WriteLine(SemaforoMultiplesHilos.Acceder(nombre))).Start();
        }
        Thread.Sleep(1000);
        Console.WriteLine();

        Console.WriteLine("----------Ejemplo 3----------");
        Console.WriteLine(SemaforoConTimeout.IntentarAcceder());
        Console.WriteLine();

        Console.WriteLine("----------Ejemplo 4----------");
        new Thread(() => TareaPesada.Procesar("Tarea1")).Start();
        new Thread(() => TareaPesada.Procesar("Tarea2")).Start();
        Thread.Sleep(1500);
        Console.WriteLine();

        Console.WriteLine("----------Ejemplo 5----------");
        ListaProtegida.Agregar("Hola");
        ListaProtegida.Agregar("Mundo");
        Console.WriteLine("Mensajes: " + ListaProtegida.Obtener());
        Console.WriteLine();

        Console.WriteLine("----------Ejemplo 6----------");
        for (int i = 1; i <= 7; i++)
        {
            string usuario = $"Cliente{i}";
            new Thread(() => Console.WriteLine(ControlDeStock.Comprar(usuario))).Start();
        }
        Thread.Sleep(1000);
        Console.WriteLine();

        Console.WriteLine("----------Ejemplo 7----------");
        EscrituraArchivo.Escribir("Entrada de log del hilo principal.");
        Console.WriteLine("Texto guardado en log_semaforo.txt");
        Console.WriteLine();

        Console.WriteLine("----------Ejemplo 8----------");
        new Thread(() => Logger.Log("Mensaje desde hilo")).Start();
        Thread.Sleep(500);
        Console.WriteLine();

        Console.WriteLine("----------Ejemplo 9----------");
        new Thread(() => Console.WriteLine(ProductorConsumidor.Consumir())).Start();
        Thread.Sleep(200);
        ProductorConsumidor.Producir(42);
        Thread.Sleep(500);
        Console.WriteLine();

        Console.WriteLine("----------Ejemplo 10----------");
        for (int i = 1; i <= 4; i++)
        {
            string nombre = $"Proceso{i}";
            new Thread(() => RecursosLimitados.Ejecutar(nombre)).Start();
        }
        Thread.Sleep(2000);
        Console.WriteLine();
    }
}
