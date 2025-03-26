# Ejemplos de `SemaphoreSlim` en C# (detalle por ejemplo)

Este documento contiene 10 ejemplos progresivos y prácticos para comprender el uso de `SemaphoreSlim` en C#, diseñados para ejecutarse en contextos multihilo usando `Thread`. Cada ejemplo incluye una explicación de su funcionamiento y por qué `SemaphoreSlim` es la opción más adecuada.

---

## 🧪 Ejemplo 1: Uso básico de SemaphoreSlim

```csharp
private static readonly SemaphoreSlim _semaforo = new(1);

public static string Acceder()
{
    _semaforo.Wait();
    try { return "Acceso permitido a sección crítica"; }
    finally { _semaforo.Release(); }
}
```

🔍 Controla acceso exclusivo a una sección crítica con un solo hilo a la vez.

✅ **¿Por qué `SemaphoreSlim`?**  
Es más liviano que `Mutex` y más flexible que `lock`, ideal para casos simples con mejor rendimiento y sin bloquear el hilo principal.

---

## 🧪 Ejemplo 2: Control de concurrencia con 3 hilos

```csharp
private static readonly SemaphoreSlim _semaforo = new(3);

public static string Acceder(string nombre)
{
    _semaforo.Wait();
    try { return $"{nombre} está accediendo"; }
    finally { _semaforo.Release(); }
}
```

🔍 Permite que hasta 3 hilos accedan simultáneamente a la sección protegida.

✅ **¿Por qué `SemaphoreSlim`?**  
Permite configurar el número de accesos concurrentes sin necesidad de estructuras más complejas como `BlockingCollection`.

---

## 🧪 Ejemplo 3: Con timeout en la espera

```csharp
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
```

🔍 Intenta acceder a un recurso compartido, pero no bloquea indefinidamente.

✅ **¿Por qué `SemaphoreSlim`?**  
`lock` no permite timeout. Este control ayuda a evitar deadlocks o esperas eternas.

---

## 🧪 Ejemplo 4: Simulación de tareas pesadas

```csharp
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
```

🔍 Controla acceso concurrente a una sección costosa (como E/S o CPU intensiva).

✅ **¿Por qué `SemaphoreSlim`?**  
Permite limitar los hilos que ejecutan trabajo pesado al mismo tiempo, reduciendo la sobrecarga del sistema.

---

## 🧪 Ejemplo 5: Acceso a lista compartida

```csharp
private static readonly SemaphoreSlim _semaforo = new(1);
private static List<string> _mensajes = new();

public static void Agregar(string mensaje)
{
    _semaforo.Wait();
    try { _mensajes.Add(mensaje); }
    finally { _semaforo.Release(); }
}

public static List<string> Obtener()
{
    _semaforo.Wait();
    try { return new List<string>(_mensajes); }
    finally { _semaforo.Release(); }
}
```

🔍 Maneja múltiples hilos escribiendo en una lista sin corrupción de datos.

✅ **¿Por qué `SemaphoreSlim`?**  
Alternativa liviana a `lock` y más flexible para tareas asincrónicas en escenarios modernos.

---

## 🧪 Ejemplo 6: Control de stock limitado

```csharp
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
```

🔍 Simula un sistema de compras con inventario compartido.

✅ **¿Por qué `SemaphoreSlim`?**  
Ideal cuando múltiples compradores acceden al mismo recurso. Controla las condiciones críticas sin bloquear.

---

## 🧪 Ejemplo 7: Escritura en archivo concurrente

```csharp
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
```

🔍 Evita que varios hilos escriban en el archivo al mismo tiempo, previniendo corrupción.

✅ **¿Por qué `SemaphoreSlim`?**  
`lock` no garantiza exclusión si se usa desde métodos async o múltiples fuentes. `SemaphoreSlim` sí.

---

## 🧪 Ejemplo 8: Logger multihilo

```csharp
private static readonly SemaphoreSlim _semaforo = new(1);

public static void Log(string mensaje)
{
    _semaforo.Wait();
    try { Console.WriteLine($"[LOG] {mensaje}"); }
    finally { _semaforo.Release(); }
}
```

🔍 Previene mezcla de salidas en consola al registrar mensajes concurrentemente.

✅ **¿Por qué `SemaphoreSlim`?**  
Es la mejor alternativa cuando el logging es compartido por múltiples hilos de forma intensiva.

---

## 🧪 Ejemplo 9: Productor-consumidor simple

```csharp
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
```

🔍 Maneja intercambio de datos entre productores y consumidores sincronizados.

✅ **¿Por qué `SemaphoreSlim`?**  
Mejor que `lock` si se quiere escalar más adelante a `async/await` y mantener control fino.

---

## 🧪 Ejemplo 10: Control de múltiples recursos con 2 entradas

```csharp
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
```

🔍 Permite limitar a 2 procesos concurrentes en tareas costosas.

✅ **¿Por qué `SemaphoreSlim`?**  
Permite definir un número máximo de hilos activos sin complejidad adicional.

---
