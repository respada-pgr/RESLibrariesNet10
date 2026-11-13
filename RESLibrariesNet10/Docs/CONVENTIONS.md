# Convenciones de Código — RESLibrariesNet10

Reglas de estilo, nomenclatura y documentación XML para **toda la solución**  
(`1_LibraryClasses`, `2_LibraryHelpers`, `3_LibraryServices`, host).

> Si un proyecto necesita una excepción, documentarla en un apartado al final con el nombre del proyecto.

---

## 1. Reglas de Nomenclatura

### 1.1 Prefijos para Tipos

| Tipo              | Prefijo | Ejemplo                                              | Notas |
|-------------------|---------|------------------------------------------------------|-------|
| Interfaces        | `I_`    | `I_Identifiable<TId>`, `I_Identifiable`, `I_Initialized`   | PascalCase después del prefijo |
| Clases (dominio)  | `C_`     | `C_Entity<TId>`, `C_Id<TId>`, `C_Register`            | Núcleo en `1_LibraryClasses` |
| Helpers           | `C_Hlp_` | `C_Hlp_File`, `C_Hlp_String`, `C_Hlp_StackTrace`   | Utilidades en `2_LibraryHelpers` |
| Services          | `C_Srv_` | `C_Srv_Files`, …                                      | Servicios en `3_LibraryServices` (cuando se renombren) |
| Enumeraciones     | `E_`     | `E_Status`, `E_Permissions`                           | Miembros en MAYÚSCULAS (objetivo) |

### 1.1.1 Excepciones al prefijo `C_`

| Tipo | Nombre | Motivo |
|------|--------|--------|
| Logger global (Utils) | `GLogger`, `C_Hlp_DebugLog` | Infraestructura estática global; carpeta/namespace `GlobalLogger` |

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
namespace _1_LibraryClasses.Interfaces
{
    // ...
}
```

```csharp
namespace _1_LibraryClasses.Classes
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

Solo hay que añadir `using` manuales para namespaces propios del proyecto (por ejemplo `_1_LibraryClasses.Interfaces`).

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
| Namespaces            | `_1_LibraryClasses.Xxx` **con llaves** `{ }` |
| Implicit Usings       | Activados (no hace falta `using System;`, etc.)   |


---

## 4. Organización interna de clases (`#region`)

Usar regiones **solo en tipos con bastante código** (muchas propiedades, constructores u sobrecargas). Clases pequeñas no las necesitan.

### 4.1 Orden recomendado (de arriba a abajo)

| Orden | Región | Contenido |
|------:|--------|-----------|
| 1 | `PRIVATE FIELDS` | Campos privados (`_camelCase`) |
| 2 | `PUBLIC PROPERTIES` | Propiedades públicas (y `protected` de contrato si aplica) |
| 3 | `PROTECTED PROPERTIES` | Solo si hay un bloque claro de propiedades protegidas |
| 4 | `CONSTRUCTORS` | Todos los constructores |
| 5 | `PUBLIC METHODS` | API pública de instancia |
| 6 | `PROTECTED METHODS` | Métodos reutilizables por herederos |
| 7 | `PRIVATE METHODS` | Helpers privados de la clase |
| 8 | `OPERATORS` / `NESTED TYPES` | Si existen |

Nombres de región en **inglés**, `SCREAMING` o `Title Case` coherente en todo el proyecto. Preferido:

```csharp
#region PRIVATE FIELDS
#region PUBLIC PROPERTIES
#region CONSTRUCTORS
#region PUBLIC METHODS
#region PRIVATE METHODS
```

### 4.2 Reglas

1. **La visibilidad de la región debe coincidir con el contenido**  
   - `PRIVATE METHODS` → solo `private`  
   - `PUBLIC METHODS` → `public` (y eventualmente `internal` de API)  
   - No meter métodos `public` en `INTERNAL` / `PRIVATE`.

2. **No usar** nombres confusos como `INTERNAL METHODS` para código público.

3. **Constantes**  
   - `private const` / `static readonly` al inicio de `PRIVATE FIELDS`, o región `CONSTANTS` antes de campos si hay muchas.

4. **Miembros `static`**  
   - Pueden ir en la misma región por visibilidad (`PUBLIC METHODS`) o en `PUBLIC STATIC METHODS` si el bloque es grande.

5. **Especializaciones** (`C_LogLine : C_LogLine<long>`)  
   - Solo `CONSTRUCTORS` (y overrides si los hay); no repetir campos de la base.

6. **Interfaces**  
   - Normalmente sin regiones; si el contrato es muy grande, agrupar por responsabilidad (identidad, auditoría, etc.).


6. **Alineación**  
   `#region` / `#endregion` **sin tabulación**: alineados a la columna 0 (izquierda del fichero), no indentados con el cuerpo de la clase.

### 4.3 Ejemplo mínimo

```csharp
public class C_Example
{
#region PRIVATE FIELDS
    private int _count;
#endregion

#region PUBLIC PROPERTIES
    public int Count => _count;
#endregion

#region CONSTRUCTORS
    public C_Example() { }
#endregion

#region PUBLIC METHODS
    public void Reset() => _count = 0;
#endregion

#region PRIVATE METHODS
    private void Touch() { }
#endregion
}
```



---

## Copy constructor and Clone (pending formalization)

- Prefer a **copy constructor** `Type(Type source)` that copies state.
- Prefer **`Clone()`** that returns a new instance with a **new `CreateDate`** when the type has one.
- Exact rules for **when `CreateDate` is set** are **open** — see `PENDING.md` (2026-11-12).
