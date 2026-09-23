# Changelog — RESLibrariesNet10

Historial de cambios de la solución. Organizado por proyecto.

---

## 1_LibraryClasses

## 2026-09-22

### Database eliminada
- Eliminadas `C_RegBBDD_*` (sin usos en el repo)
- `C_Pagination` movida a `Base/` (namespace `Classes`)
- Carpeta `Database/` eliminada

---

## 2026-09-20

### Carpetas
- `Classes/` renombrada a **`Base/`**
- Nueva carpeta **`Domain/`** (valorada; no usada en el diseño actual)

---

## 2026-09-18

### Enums en MAYÚSCULAS
- `E_Action`: UNDEFINED, CREATE, UPDATE, ...
- `E_Permissions`: NONE, READ, WRITE, EXECUTE, DELETE, ALL

---

## 2026-09-18

### C_Action
- `E_Action` (Undefined, Create, Read, Update, Delete, Execute)
- `C_Action<TId> : C_EntityNamed<TId>, I_Descriptible`
- `C_ActionGuid` (sustituye el antiguo `C_Action` no genérico)

---

## 2026-09-18

### C_ValueDated
- Renombrado: énfasis en valor capturado en una fecha
- `C_ValueDated<TValue>` + `C_ValueDatedInt`, `C_ValueDatedFloat`, `C_ValueDatedString`
- Sustituye `C_IntValue_Date` / `C_FloatValue_Date` / `C_StringValue_Date` (sin guion bajo)

---

## 2026-09-18

### Permissions
- `Enums/E_Permissions.cs` (Flags)
- `Interfaces/I_Permissions.cs`
- `Base/C_Permissions.cs` (operadores +, -, |, &; igualdad; conversiones implícitas)
- Docs estilo [ES]; ejemplos solo en inglés; fix |/& y null en +/-

---

## 2026-09-18

### C_Item
- `C_Item<TId, TValue> : C_Entity<TId>` con propiedad `Value`
- Especializaciones: `C_ItemGuid<TValue>`, `C_ItemInt<TValue>`, `C_ItemLong<TValue>`, `C_ItemString<TValue>`

---

## 2026-09-18

### Named sin interfaces compuestas
- Eliminadas `I_EntityNamed` e `I_RegisterNamed`
- `C_EntityNamed` → `I_Entity<TId>`, `I_Named`
- `C_RegisterNamed` → `I_Register<TId>`, `I_Named`

---

## 2026-09-16

### Organización en carpetas
- `Interfaces/` — contratos `I_*`
- `Base/` — implementaciones `C_*`
- `Docs/` — PROJECT, CONVENTIONS, ARCHITECTURE, CHANGELOG, PENDING, DEV_PROMPT

---

## 2026-09-16

### Named + pendientes
- `I_Entity<TId> + I_Named`, `I_Register<TId> + I_Named`
- `C_EntityNamed` / `C_RegisterNamed` + especializaciones Guid/Int/Long/String
- `PENDING.md` (backlog; CQRS con ejemplos)

---

## 2026-09-16

### Especializaciones sin guion bajo
- `C_IdGuid`, `C_EntityGuid`, `C_RegisterGuid`, etc. (no `C_Id_Guid`)

---

## 2026-09-16

### I_Sortable
- Renombrado desde `I_Orderable`
- Interfaz con `SortOrder { get; set; }`

---

## 2026-09-15 (noche)

### Rehidratación C_Register
- Ctor `(id, createDate, updateDate?, deleteDate?)` en genérico y especializaciones
- Ctor desde `I_Register<TId>` (copia auditoría + soft-delete)
- `IsDeleted` se deriva de `deleteDate is not null`


### I_Register = Entity + Auditable + SoftDeletable
- Nuevos: `I_Auditable`, `I_SoftDeletable`
- `I_Register<TId> : I_Entity<TId>, I_Auditable, I_SoftDeletable`
- `C_Register`: `CreateDate`, `UpdateDate`, `IsDeleted`, `DeleteDate`; helpers `MarkUpdated`, `SoftDelete`, `Restore`
- Renombrado `CreateDate` → `CreateDate`

---

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

# Changelog — 1_LibraryClasses (`_1_LibraryClassesNet10`) — historial previo

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


---

## 2_LibraryUtils

_(sin entradas aún)_

---

## 3_LibraryServices

_(sin entradas aún)_

---

## RESLibrariesNet10 (host)

- 2026-09-23: Documentación de solución centralizada en `RESLibrariesNet10/Docs/`.
