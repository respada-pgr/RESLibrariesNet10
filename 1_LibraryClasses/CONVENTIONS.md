# Convenciones de Código — Proyecto GDC

Reglas generales de estilo, nomenclatura y documentación aplicables al desarrollo en C#.

---

## 1. Reglas de Nomenclatura

### 1.1 Prefijos para Tipos

| Tipo              | Prefijo | Ejemplo                                              | Notas |
|-------------------|---------|------------------------------------------------------|-------|
| Interfaces        | `I_`    | `I_Identifiable<TId>`, `I_Named`, `I_Initialized`   | PascalCase después del prefijo |
| Clases            | `C_`    | `C_Entity<TId>`, `C_Id<TId>`, `C_User`               | Concretas y abstractas |
| Enumeraciones     | `E_`    | `E_Status`, `E_UserRole`                             | Reservado para uso futuro |

### 1.2 Parámetros Genéricos

- Comienzan por **`T`** seguido del nombre o propósito en PascalCase.
- Ejemplos: `TId`, `TEntity`, `TKey`, `TValue`, `TResult`.

### 1.3 Propiedades y Métodos

- Nombres en **PascalCase**.
- Claros, autoexplicativos y en **inglés**.
- Ejemplos: `Id`, `IsInitialized`, `Name`, `Init`, `Equals`, `GetHashCode`.

### 1.4 Namespaces

- Basados en la estructura del proyecto.
- **PascalCase**, con guion bajo para componentes numéricos o de librería.
- Estilo **con llaves** (no file-scoped).

```csharp
namespace _1_LibraryClassesNet10.Interfaces
{
    // ...
}
```

```csharp
namespace _1_LibraryClassesNet10.Classes
{
    // ...
}
```

### 1.5 Implicit Usings

El proyecto usa **Implicit Usings** (activado por defecto en .NET 6+).

No es necesario escribir de forma explícita namespaces habituales como:

- `System`
- `System.Collections.Generic`
- `System.Linq`
- `System.Threading.Tasks`

Tipos como `Guid`, `EqualityComparer<T>`, `Console`, `List<T>`, etc. están disponibles sin `using`.

Se controla en el `.csproj`:

```xml
<PropertyGroup>
  <ImplicitUsings>enable</ImplicitUsings>
</PropertyGroup>
```

Solo hay que añadir `using` manuales para namespaces propios del proyecto (por ejemplo `_1_LibraryClassesNet10.Interfaces`).

---

## 2. Reglas de Documentación XML

### 2.1 Bilingüe (Inglés / Español)

- Texto principal en **inglés**.
- Traducción al español justo después, en itálica:

```xml
<para><i>[ES] Texto en español...</i></para>
```

### 2.2 Etiquetas Estándar

| Etiqueta                     | Uso |
|------------------------------|-----|
| `<summary>`                  | Descripción breve de clase, interfaz, propiedad o método |
| `<typeparam name="...">`     | Explicación de cada tipo genérico |
| `<param name="...">`         | Descripción de parámetros |
| `<value>`                    | Valor de una propiedad y estados posibles (ej. `null`) |
| `<returns>`                  | Valor devuelto por un método |
| `<example>` + `<code>`       | Ejemplos prácticos de uso |

### 2.3 Referencias y Palabras Clave

```xml
<see cref="I_Identifiable{TId}"/>
<see cref="C_Entity{TId}"/>
<see langword="null"/>
<see langword="true"/>
<see langword="false"/>
```

### 2.4 Ejemplos de Uso

- Dentro de bloques `<code>`.
- Los signos `<` y `>` se escriben directamente (no se escapan).
- **Solo en inglés**.
- No incluir texto bilingüe dentro de `<example>` (evitar "Usage example / Ejemplo de uso").

**Ejemplo completo:**

```xml
/// <summary>
/// Gets a value indicating whether the object has been properly initialized.
/// <para>
/// An entity is considered initialized when it has a non-default identity assigned.
/// </para>
/// <para><i>[ES] Obtiene un valor que indica si el objeto ha sido inicializado correctamente.
/// Una entidad se considera inicializada cuando tiene una identidad no predeterminada asignada.</i></para>
/// </summary>
/// <value>
/// <see langword="true"/> if the entity has a valid identity assigned; otherwise, <see langword="false"/>.
/// <para><i>[ES] <see langword="true"/> si la entidad tiene una identidad válida asignada; de lo contrario, <see langword="false"/>.</i></para>
/// </value>
/// <example>
/// <code>
/// var order = new C_Order();
/// if (order.IsInitialized)
/// {
///     // Entity is initialized
/// }
/// </code>
/// </example>
```

---

## 3. Resumen Rápido

| Elemento              | Convención                                      |
|-----------------------|-------------------------------------------------|
| Interfaces            | `I_` + PascalCase                               |
| Clases                | `C_` + PascalCase                               |
| Enums                 | `E_` + PascalCase                               |
| Genéricos             | `T` + PascalCase (`TId`, `TResult`)             |
| Propiedades / Métodos | PascalCase (inglés)                             |
| Documentación         | Inglés principal + `[ES]` en itálica            |
| Ejemplos de código    | Solo en inglés                                  |
| Namespaces            | `_1_LibraryClassesNet10.Xxx` **con llaves** `{ }` |
| Implicit Usings       | Activados (no hace falta `using System;`, etc.)   |
