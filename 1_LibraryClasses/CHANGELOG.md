## 2026-09-15

### I_Register
- `I_Register<TId> : I_Entity<TId>` con `CreateDate`
- `C_Register<TId>` implementa `I_Register<TId>`


### I_Initializable
- Documentado: Init() sin parámetros solo para tipos que generan identidad (Guid).
- int/long/string: solo Init(value) en la clase; no implementan I_Initializable.


### Interfaces
- Eliminados `I_Id` e `I_Entity` no genéricos (usar `I_Id<Guid>` / `I_Entity<Guid>`).
- `C_IdGuid` → `I_Id<Guid>, I_Initializable<Guid>`
- `C_EntityGuid` → `I_Entity<Guid>, I_Initializable<Guid>`

---

## 2026-09-15

### I_Descriptible
- Interfaz con `Description { get; set; }`

---

## 2026-09-14 (tarde)

### Unificación de ficheros + C_Register
- `C_Id.cs`: genérico + Guid/Int/Long/String
- `C_Entity.cs`: genérico + Guid/Int/Long/String
- `C_Register.cs`: `C_Register<TId>` (+ CreateDate UTC) y especializaciones Guid/Int/Long/String
- Eliminados ficheros sueltos `C_IdGuid.cs`, `C_EntityGuid.cs`, etc.

---

## 2026-09-14

### Renombre especializaciones Guid
- `C_Id` → **`C_IdGuid`**
- `C_Entity` → **`C_EntityGuid`**
- Se mantienen `C_Id<TId>` y `C_Entity<TId>` genéricos.
- Próximas variantes naturales: `C_IdInt`, `C_IdLong`, `C_IdString` (y análogas en Entity) cuando hagan falta.

---

## 2026-09-14

### Solo Guid en C_Id / C_Entity (especializados)
- Eliminado `Guid?` de constructores y operadores de `C_Id` y `C_Entity`.
- `Id` expuesto como `Guid` (`new Guid Id`); `Guid.Empty` = no inicializado.
- `IsInitialized` ⇒ `Id != Guid.Empty`.
- La genérica `C_Id<TId>` / `C_Entity<TId>` sigue usando `TId?` para otros tipos (p. ej. `string`).

---

# Changelog — Biblioteca GDC (`_1_LibraryClassesNet10`)

Registro de cambios de diseño, contratos y documentación del proyecto.

---

## 2026-09-13 (continuación)

### Solo Guid en C_Id / C_Entity (especializados)
- Eliminado `Guid?` de constructores y operadores de `C_Id` y `C_Entity`.
- `Id` expuesto como `Guid` (`new Guid Id`); `Guid.Empty` = no inicializado.
- `IsInitialized` ⇒ `Id != Guid.Empty`.
- La genérica `C_Id<TId>` / `C_Entity<TId>` sigue usando `TId?` para otros tipos (p. ej. `string`).


### null vs default (opción B)
- `null` = no asignado; `Guid.Empty` / `default` = sentinela vacío. Ambos ⇒ `IsInitialized = false`, información distinta.
- Eliminado `id ?? Guid.Empty` en constructores de `C_Id` y `C_Entity` (se conserva `null`).


### C_Id constructores (cambio de diseño)
- `C_Id()` ahora genera `Guid.NewGuid()` (inicializado), alineado con `C_Entity()`.
- `C_Id(Guid id) : base(id)` — `Guid.Empty` ⇒ no inicializado.
- `C_Id(Guid? id) : base(id ?? Guid.Empty)`.
- Para usar `Init()`, crear con `new C_Id(Guid.Empty)` o `new C_Id((Guid?)null)`.


### I_Id no genérica
- Añadido `I_Id : I_Id<Guid>` (simetría con `I_Entity`).
- `C_Id` implementa `I_Id` e `I_Initializable<Guid>`.

### Conversión implícita `C_Id` → `Guid`
- Documentado el riesgo: si no está inicializado devuelve `Guid.Empty` y oculta el estado.
- Se recomienda preferir `Guid?` para detectar identidad ausente.

---

## 2026-09-13

### Documentación y estilo
- Unificados todos los namespaces al estilo **con llaves** `{ }` (convención del proyecto).
- Corregidos ejemplos XML en interfaces y clases:
  - Eliminado `IsTransient`; todo usa `IsInitialized`.
  - Ejemplos de `Init()` muestran protección de un solo uso.
  - Ejemplos de `C_Entity` usan `new C_User(null)` antes de `Init()` (el constructor por defecto ya genera Guid).
- Ampliada documentación de `I_Initialized`.

### Inicialización
- `I_Initializable<TResult>` ampliado con:
  - `TResult Init()`
  - `TResult Init(TResult value)`
- `C_Id` implementa `I_Initializable<Guid>`:
  - `Init()` / `Init(Guid)` con protección (no se puede reasignar el Id).
  - Se mantiene `New()` como fábrica estática (crea instancia nueva).
- `C_Entity.Init()` / `Init(Guid)` con la misma protección e inmutabilidad del Id.

### Contratos de entidad
- Añadido `I_Entity.cs`:
  - `I_Entity<TId> : I_Identifiable<TId>`
  - `I_Entity : I_Entity<Guid>, I_Initializable<Guid>`
- `C_Entity<TId> : I_Entity<TId>`
- `C_Entity : C_Entity<Guid>, I_Entity`

### Decisiones de diseño
- `C_Id` = Value Object de identidad (no es entidad).
- `C_Entity` = entidad de dominio.
- Id **inmutable** una vez inicializado.
- `New()` ≠ `Init()` (fábrica vs inicialización de instancia).

---

## 2026-09-12

### Arquitectura y convenciones
- Separados documentos:
  - `CONVENTIONS.md` — nomenclatura, documentación XML, Implicit Usings, namespaces.
  - `ARCHITECTURE.md` — estructura, contratos, decisiones de diseño.
- Añadido `I_Initializable<TResult> : I_Initialized` con `Init()`.
- Documentado Implicit Usings (no hace falta `using System;` explícito).

### Identidad
- Sustituido `IsTransient` por **`IsInitialized`** en todo el modelo.
- `I_Identifiable<TId>` extiende `I_Initialized`.

---

## 2026-09-11

### Entidades
- Refinada documentación de `C_Entity` / `C_Entity<TId>`.
- Constructor `C_Entity()` genera `Guid.NewGuid()` (entidad nace inicializada).
- `null` / `Guid.Empty` → no inicializado.

---

## 2026-09-10

### Base del dominio
- `I_Initialized` — estado de inicialización.
- `I_Identifiable<TId>` — identidad + estado.
- `I_Id<TId>` — Value Object de identidad + igualdad.
- `C_Id<TId>` / `C_Id` — implementación del Value Object.
- `C_Entity<TId>` / `C_Entity` — base abstracta de entidades.
- `I_Named` — nombre lógico/visualización.
- Primeras reglas de nomenclatura (`I_`, `C_`, `E_`) y documentación bilingüe EN + `[ES]`.

---

## Formato de entradas futuras

```
## YYYY-MM-DD
### Área
- Cambio concreto
```
