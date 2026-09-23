# Arquitectura — RESLibrariesNet10

Visión de diseño de la **solución completa**. Cada librería tiene su capítulo.

| Proyecto | Responsabilidad |
|----------|-----------------|
| **1_LibraryClasses** | Núcleo de dominio: identidad, entidad, registro, permisos, acciones… |
| **2_LibraryUtils** | Utilidades técnicas (ficheros, texto, logger, JSON, timer) |
| **3_LibraryServices** | Servicios de aplicación (ficheros, criptografía, …) |
| **RESLibrariesNet10** | Host WinForms de prueba + documentación de solución |

**Backlog:** [PENDING.md](PENDING.md) · **Mapa:** [FOLDER_MAP.md](FOLDER_MAP.md) · **Estilo:** [CONVENTIONS.md](CONVENTIONS.md)

---

## Capítulo: 1_LibraryClasses

Decisiones de diseño, estructura y contratos de la librería **1_LibraryClasses**.

---

### Estructura de carpetas (lib)

```
1_LibraryClasses/
├── Interfaces/          # contratos I_*
├── Enums/               # E_*
├── Base/                # C_Id, C_Entity, C_Register, C_Pagination, …
├── Event/ Result/ List/ Time/ Url/ Files/   # soporte / helpers (pendiente valorar vs Utils)
└── Docs/
    ├── ARCHITECTURE.md
    ├── CONVENTIONS.md
    ├── CHANGELOG.md
    ├── FOLDER_MAP.md
    └── DEV_PROMPT.md

# Backlog de toda la solución:
#   RESLibrariesNet10/Docs/PENDING.md
```


---

### Contratos de Identidad

### 2.1 `I_Identifiable<TId>`

- Contrato base para objetos con identificador único.
- Extiende `I_Initialized`.
- Expone `TId? Id { get; }`.

### 2.2 `I_Id<TId>`

- Especialización de `I_Identifiable<TId>` para Value Objects de identidad.
- Añade igualdad por valor (`IEquatable<I_Id<TId>>`).
- Implementado por `C_Id<TId>` / `C_Id`.

### 2.3 `C_Id<TId>` / `C_Id`

- Value Object que encapsula un identificador.
- `C_Id` es la especialización para `Guid`.
- Constructor por defecto → **no inicializado** (`Id = null`).
- `C_Id.New()` → genera un nuevo `Guid` (inicializado).
- `null` = no asignado; `Guid.Empty` = sentinela vacío; ambos ⇒ no inicializado (información distinta).

### 2.4 `I_Entity<TId>` / `I_Entity`

- Contrato para entidades de dominio (no para value objects de identidad).
- `I_Entity<TId> : I_Identifiable<TId>`
- `I_Entity : I_Entity<Guid>, I_Initializable<Guid>`

### 2.5 `C_Entity<TId>` / `C_Entity`

- Clase base abstracta para entidades de dominio.
- `C_Entity<TId> : I_Entity<TId>`
- `C_Entity : C_Entity<Guid>, I_Entity`
- `C_Entity` (Guid):
  - Solo `Guid` (no `Guid?`); `Guid.Empty` = no inicializado.
  - Constructor por defecto → **genera un nuevo Guid** (inicializado).
  - `Init()` / `Init(Guid)` → solo si aún no está inicializado; si ya lo está, excepción.
  - `Id` expuesto como `Guid` en la especialización.
- Las clases concretas heredan de ella (`C_Customer`, `C_Product`, etc. en el proyecto consumidor).

---

### Contratos de Inicialización

| Interfaz | Propósito | Miembros |
|----------|-----------|----------|
| `I_Initialized` | Consultar estado | `bool IsInitialized { get; }` |
| `I_Initializable<TResult>` | Inicializar + consultar estado | `Init()` y `Init(TResult value)` |

### Reglas

- `IsInitialized` es la única forma de consultar el estado (no se usa `IsTransient`).
- Un objeto está **inicializado** cuando tiene una identidad no predeterminada:
  ```csharp
  Id is not null && !EqualityComparer<TId>.Default.Equals(Id, default!)
  ```
- `I_Initializable<TResult>` **extiende** `I_Initialized`.
- Métodos:
  - `TResult Init()` — genera un valor nuevo
  - `TResult Init(TResult value)` — asigna un valor concreto
- La inicialización es **de un solo uso**: si ya está inicializado → `InvalidOperationException` (el Id no se cambia).
- `C_Id.New()` crea una instancia **nueva** ya inicializada; no modifica instancias existentes.
- `C_Id` implementa `I_Initializable<Guid>` (`Init` + `Init(Guid)`).

### Ejemplo

```csharp
public class C_Configuration : I_Initializable<Guid>
{
    public bool IsInitialized { get; private set; }

    public Guid Init()
    {
        // Lógica de inicialización...
        IsInitialized = true;
        return Guid.NewGuid();
    }
}
```

---

### Otros Contratos

### `I_Named`

- Objetos con nombre lógico o de visualización.
- Expone `string Name { get; }` (nunca `null`).

---

### Decisiones de Diseño Relevantes

| Decisión | Elección | Motivo |
|----------|----------|--------|
| Estado de identidad | Solo `IsInitialized` | Un único concepto, más simple |
| `C_Entity` (Guid) por defecto | Genera Guid nuevo | Las entidades suelen nacer con identidad |
| `C_Id` por defecto | No inicializado | Value Object de identidad; se asigna explícitamente |
| `I_Initializable` genérico | `TResult Init()` | Flexibilidad en el valor de retorno |
| Clases base de entidad | `abstract` | Evitar instancias directas sin significado de dominio |


---

## Capítulo: 2_LibraryUtils

Utilidades reutilizables. Sin modelo de dominio propio.

Carpetas actuales: `Converters/`, `FileManager/`, `Logger/`, `TextManager/`, `Timer/`.

_(Ampliar cuando se documente el diseño de Utils.)_

---

## Capítulo: 3_LibraryServices

Servicios de infraestructura de aplicación.

Carpetas actuales: `Services/Crypto/`, `Services/Files/`, `Files/`.

_(Ampliar cuando se documente el diseño de Services.)_

---

## Capítulo: RESLibrariesNet10 (host)

App de prueba WinForms. Aloja `Docs/` de la solución (PENDING, ARCHITECTURE, etc.).
