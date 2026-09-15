# Arquitectura del Proyecto GDC

Decisiones de diseño, estructura y contratos específicos de este proyecto.

---

## 1. Estructura del Proyecto

```
_1_LibraryClassesNet10
├── Interfaces/
│   ├── I_Identifiable.cs      // I_Identifiable<TId> : I_Initialized
│   ├── I_Id.cs                // I_Id<TId> : I_Identifiable<TId>, IEquatable<>
│   ├── I_Entity.cs            // I_Entity<TId> : I_Identifiable<TId>
│   │                          // I_Entity : I_Entity<Guid>, I_Initializable<Guid>
│   ├── I_Named.cs             // I_Named
│   ├── I_Initialized.cs       // I_Initialized
│   └── I_Initializable.cs     // I_Initializable<TResult> : I_Initialized
└── Classes/
    ├── C_Id.cs                // C_Id<TId>, C_IdGuid, C_IdInt, C_IdLong, C_IdString
    ├── C_Entity.cs            // C_Entity<TId>, C_EntityGuid, C_EntityInt, C_EntityLong, C_EntityString
    └── C_Register.cs          // C_Register<TId>, C_RegisterGuid, Int, Long, String

```

---

## 2. Contratos de Identidad

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
- Las clases concretas heredan de ella (`C_User`, `C_Order`, etc.).

---

## 3. Contratos de Inicialización

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

## 4. Otros Contratos

### `I_Named`

- Objetos con nombre lógico o de visualización.
- Expone `string Name { get; }` (nunca `null`).

---

## 5. Decisiones de Diseño Relevantes

| Decisión | Elección | Motivo |
|----------|----------|--------|
| Estado de identidad | Solo `IsInitialized` | Un único concepto, más simple |
| `C_Entity` (Guid) por defecto | Genera Guid nuevo | Las entidades suelen nacer con identidad |
| `C_Id` por defecto | No inicializado | Value Object de identidad; se asigna explícitamente |
| `I_Initializable` genérico | `TResult Init()` | Flexibilidad en el valor de retorno |
| Clases base de entidad | `abstract` | Evitar instancias directas sin significado de dominio |
