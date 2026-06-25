# Almacenes Por Ahí — Aplicación Móvil (.NET MAUI)

## Módulo 1: Gestión de Inventario — CRUD de Producto

Aplicación con **.NET MAUI** (.NET 10), patrón **MVVM** y **Entity Framework Core 10**
sobre **SQL Server**. Incluye una **pantalla de inicio** (menú principal) con el logo y
acceso al módulo de inventario, que implementa un CRUD completo de la entidad **Producto**.

---

## 1. Requisitos

- **Visual Studio 2022/2026** con la carga de trabajo *.NET MAUI* (variante Windows).
- **.NET 10 SDK**.
- **SQL Server LocalDB** (incluido con Visual Studio).

> Configurado para ejecutarse en **Windows Machine** (modo desempaquetado).

---

## 2. Base de datos (SQL Server, único motor)

La cadena de conexión vive en **`appsettings.json`**, no en el código:

```json
{
  "ConnectionStrings": {
    "SqlServer": "Server=(localdb)\\MSSQLLocalDB;Database=AlmacenesPorAhi;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

La base de datos se crea sola al iniciar (`EnsureCreated`) con 4 productos de ejemplo.
Usa autenticación de Windows (sin usuario ni contraseña).

Si tu instancia es distinta a LocalDB, ajusta `Server=`:
- SQL Express: `Server=.\SQLEXPRESS;...`
- Instancia por defecto: `Server=.;...` o `Server=localhost;...`

---

## 3. Ejecutar

1. Abre `AlmacenesPorAhi.sln`.
2. Clic derecho en la solución → **Restaurar paquetes NuGet**.
3. Selecciona **Windows Machine** y presiona **F5**.
4. Se abre el **menú principal**; pulsa **Gestión de Inventario** para entrar al CRUD.

---

## 4. Arquitectura (MVVM + capa de Servicio)

```
Vista (XAML) → ViewModel → Service → DbContext → SQL Server
```

| Carpeta | Responsabilidad |
|---------|-----------------|
| `Models/`       | Entidades del dominio (`Producto`). |
| `Data/`         | `AppDbContext` de EF Core + datos de semilla. |
| `Services/`     | Acceso a datos y lógica de negocio (`IProductoService`, `ProductoService`). |
| `ViewModels/`   | Lógica de presentación y comandos: `MainMenuViewModel`, `ProductoListViewModel`, `ProductoFormViewModel`. |
| `Views/`        | Interfaces XAML: `MainMenuPage` (inicio), `ProductoListPage`, `ProductoFormPage`. |
| `Helpers/`      | Utilidades transversales (`DatabaseErrors`). |

> **Nota de diseño:** se usa MVVM con una capa de servicio, tal como pide la rúbrica.
> No se agrega un patrón Repository porque el `DbContext` de EF Core ya es, en sí mismo,
> una implementación de Repository + Unit of Work; añadir otro sería redundante para este
> alcance. El `ViewModel` nunca accede a la base de datos directamente: siempre pasa por
> el servicio.

---

## 5. Dependencias (alineadas a .NET 10)

| Paquete | Versión |
|---------|---------|
| CommunityToolkit.Mvvm | 8.4.0 |
| Microsoft.EntityFrameworkCore | 10.0.9 |
| Microsoft.EntityFrameworkCore.SqlServer | 10.0.9 |
| Microsoft.Extensions.Configuration.Json | 10.0.9 |
| Microsoft.Extensions.Logging.Debug | 10.0.9 |

Todo el stack de EF Core comparte la misma versión (requisito de Microsoft) y coincide
con .NET 10 / MAUI 10, evitando conflictos de versiones.

---

## 6. Manejo de errores

Si la base de datos no está accesible, la app **no se cae**: muestra el error en un
banner (en el listado) o en un diálogo (al guardar/eliminar), traducido por
`Helpers/DatabaseErrors`.

---

## 7. Paleta visual — "Earthy Premium"

- **Primario** (identidad): `#2D4236` Verde Bosque — barra, franjas y títulos.
- **Acento** (conversión): `#E76F51` Terracota / `#D4A373` Oro Viejo — botón de acción
  principal (Gestión de Inventario, Agregar, Guardar).
- **Fondo** (neutro claro): `#FAF7F2` Arena / Lino.
- **Superficie** (tarjetas): `#FFFFFF` Blanco Puro.
- **Texto principal**: `#1E2522` Negro Verdoso.
