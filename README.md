# Módulo 5: `SemaphoreSlim` en C#

## 🚦 ¿Qué es un `SemaphoreSlim`?
`SemaphoreSlim` es un mecanismo de sincronización ligero que **limita la cantidad de hilos que pueden acceder simultáneamente a un recurso o sección de código**.

A diferencia de `Mutex` o `lock`, permite **hasta N accesos concurrentes**.

---

## 🏠 Escenario práctico: **Control de acceso a terminales de impresión**

Imaginemos que tenemos 3 terminales de impresión y 10 usuarios que quieren imprimir al mismo tiempo. `SemaphoreSlim` se encarga de permitir que **solo 3** puedan imprimir simultáneamente.

### Archivos

#### `PrintManager.cs`
```csharp
using System;
using System.Threading;
using System.Threading.Tasks;

public class PrintManager
{
    private readonly SemaphoreSlim _semaforo;

    public PrintManager(int maxConcurrent)
    {
        _semaforo = new SemaphoreSlim(maxConcurrent);
    }

    public async Task ImprimirAsync(string usuario)
    {
        Console.WriteLine($"{usuario} esperando impresora...");
        await _semaforo.WaitAsync();

        try
        {
            Console.WriteLine($"{usuario} imprimiendo...");
            await Task.Delay(2000); // Simula impresión
            Console.WriteLine($"{usuario} terminó de imprimir.");
        }
        finally
        {
            _semaforo.Release();
            Console.WriteLine($"{usuario} liberó impresora.");
        }
    }
}
```

#### `Program.cs`
```csharp
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        var manager = new PrintManager(3);
        var tareas = new List<Task>();

        for (int i = 1; i <= 10; i++)
        {
            string usuario = $"Usuario {i}";
            tareas.Add(manager.ImprimirAsync(usuario));
        }

        await Task.WhenAll(tareas);
    }
}
```

---

## 🤔 Diferencias clave: `SemaphoreSlim` vs `Semaphore`

| Característica | `SemaphoreSlim` | `Semaphore` |
|----------------|------------------|-------------|
| Asíncrono     | ✅ Sí            | ❌ No      |
| Liviano       | ✅ Sí            | ❌ No      |
| Entre procesos| ❌ No           | ✅ Sí      |

---

## 🧼 Buenas prácticas con `SemaphoreSlim`

| Regla | Motivo |
|-------|--------|
| ✅ Usá `try/finally` para garantizar `Release()` | Evita fugas de semáforo |
| ✅ Usá `WaitAsync()` con `async/await` | Más eficiente en I/O |
| ❌ No uses en escenarios entre procesos | No está diseñado para eso |

---
