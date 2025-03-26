# Ejemplos prácticos y profesionales de `SemaphoreSlim` en C#

Este documento presenta 10 ejemplos realistas y técnicamente justificados del uso de `SemaphoreSlim` en C#. Todos los ejemplos están diseñados con hilos (`Thread`) para demostrar cómo `SemaphoreSlim` permite limitar la concurrencia en escenarios prácticos y controlados.

---

## 🧪 Ejemplo 1: Control de acceso concurrente a un recurso

```csharp
private static SemaphoreSlim _semaforo = new(3); // permite hasta 3 accesos simultáneos

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
```

🔍 Controla que no más de 3 hilos usen un recurso al mismo tiempo.

✅ **¿Por qué `SemaphoreSlim`?**  
Es liviano y eficaz para limitar concurrencia. `lock` y `Monitor` solo permiten un acceso exclusivo.

📊 **Comparación con otros mecanismos:**
- 🔐 `lock`, `Monitor`: permiten solo un acceso. No sirven si queremos concurrencia parcial.
- 🧵 `Mutex`: más costoso, no es ideal dentro del proceso.
- 🔄 `Barrier`: no regula concurrencia, coordina fases.

---

## 🧪 Ejemplo 2: Limitar conexiones simultáneas a una API simulada

```csharp
private static SemaphoreSlim _limiteAPI = new(2); // máximo 2 peticiones a la vez

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
```

🔍 Simula un límite de concurrencia en servicios externos o APIs.

✅ **¿Por qué `SemaphoreSlim`?**  
Permite limitar el acceso sin bloquear completamente como lo haría `lock`.

📊 **Comparación con otros mecanismos:**
- 🔐 `lock`, `Monitor`: no permiten múltiplos accesos simultáneos.
- 🧵 `Mutex`: no configurable para múltiples accesos.
- 🔄 `Barrier`: no sirve para este patrón.

---

## 🧪 Ejemplo 3: Cola de impresión con concurrencia limitada

```csharp
private static SemaphoreSlim _impresorasDisponibles = new(2); // 2 impresoras

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
```

🔍 Simula una cola de impresión donde solo dos impresoras están disponibles.

✅ **¿Por qué `SemaphoreSlim`?**  
Ideal cuando se dispone de una cantidad fija de recursos iguales.

📊 **Comparación con otros mecanismos:**
- 🔐 `lock`: bloquea completamente.
- 🧵 `Mutex`: no permite configurar concurrencia.
- 🔄 `Barrier`: no aplica.

---

## 🧪 Ejemplo 4: Control de acceso a base de datos desde múltiples hilos

```csharp
private static SemaphoreSlim _accesoBD = new(1); // exclusivo pero con timeout

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
```

🔍 Permite acceso exclusivo, pero con control de tiempo.

✅ **¿Por qué `SemaphoreSlim`?**  
Combina control de exclusividad con manejo de timeout sin bloqueos duros.

📊 **Comparación con otros mecanismos:**
- 🔐 `lock`: no soporta timeout.
- 🧵 `Mutex`: sí lo permite pero es más pesado.
- 🔄 `Barrier`: no aplica.

---

## 🧪 Ejemplo 5: Límite de acceso a archivo de configuración

```csharp
private static SemaphoreSlim _configFileAccess = new(1);

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
```

🔍 Limita acceso concurrente a un recurso sensible como archivo de configuración.

✅ **¿Por qué `SemaphoreSlim`?**  
Flexible, más eficiente que `Mutex` si no se requiere visibilidad entre procesos.

📊 **Comparación con otros mecanismos:**
- 🔐 `lock`: bloquea completamente sin control de timeout.
- 🧵 `Monitor`: no ofrece límite de concurrencia.
- 🔄 `Barrier`: no diseñado para este tipo de exclusividad.

---

## 🧪 Ejemplo 6: Control de concurrencia en sistema de reservas

```csharp
private static SemaphoreSlim _reservas = new(2); // solo 2 usuarios reservan al mismo tiempo

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
```

🔍 Simula acceso concurrente limitado a un sistema de reservas.

✅ **¿Por qué `SemaphoreSlim`?**  
Limita la presión sobre el backend sin bloquear completamente a todos los usuarios.

📊 **Comparación con otros mecanismos:**
- 🔐 `lock`: bloquea a todos.
- 🧵 `Mutex`: innecesariamente pesado.
- 🔄 `Barrier`: no aplica.

---

## 🧪 Ejemplo 7: Lectores concurrentes con escritura bloqueada (simulado)

```csharp
private static SemaphoreSlim _lecturaConcurrencia = new(3);

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
```

🔍 Permite que hasta 3 lectores lean en paralelo.

✅ **¿Por qué `SemaphoreSlim`?**  
Simula concurrencia parcial, ideal en escenarios de lectura donde no se modifica el recurso.

📊 **Comparación con otros mecanismos:**
- 🔐 `lock`: serializa completamente.
- 🧵 `Mutex`: sería exagerado.
- 🔄 `Barrier`: no aplica.

---

## 🧪 Ejemplo 8: Control de concurrencia en simulación de checkout de carrito

```csharp
private static SemaphoreSlim _checkout = new(2);

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
```

🔍 Limita cuántos usuarios procesan su pago al mismo tiempo.

✅ **¿Por qué `SemaphoreSlim`?**  
Simula la capacidad limitada del backend de pagos.

📊 **Comparación con otros mecanismos:**
- 🔐 `lock`: bloquea totalmente.
- 🧵 `Mutex`: no se necesita entre procesos.
- 🔄 `Barrier`: no ayuda en control de capacidad.

---

## 🧪 Ejemplo 9: Controlar accesos simultáneos a módulo de reporte

```csharp
private static SemaphoreSlim _reporte = new(1);

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
```

🔍 Garantiza exclusividad en la generación de un recurso costoso.

✅ **¿Por qué `SemaphoreSlim`?**  
Ideal para exclusividad con capacidad de extender a múltiples accesos si se requiriera más adelante.

📊 **Comparación con otros mecanismos:**
- 🔐 `lock`: puede funcionar, pero menos flexible.
- 🧵 `Mutex`: válido si se comparte entre procesos.
- 🔄 `Barrier`: no aplica.

---

## 🧪 Ejemplo 10: Simulación de acceso limitado a recurso externo (como servidor FTP)

```csharp
private static SemaphoreSlim _ftpAcceso = new(2);

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
```

🔍 Controla cuántos clientes pueden interactuar con un servicio externo al mismo tiempo.

✅ **¿Por qué `SemaphoreSlim`?**  
Permite concurrencia parcial mientras se mantiene control de carga.

📊 **Comparación con otros mecanismos:**
- 🔐 `lock`, `Monitor`: bloqueo total.
- 🧵 `Mutex`: innecesario salvo sincronización externa.
- 🔄 `Barrier`: no adecuado para control de concurrencia.

---
