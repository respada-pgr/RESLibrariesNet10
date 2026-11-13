# Pendientes / backlog — RESLibrariesNet10

Lista de temas a valorar o completar en **cualquier proyecto** de la solución  
(`1_LibraryClasses`, `2_LibraryHelpers`, `3_LibraryServices`, `4_LibraryUtils`, host).

**Contexto:** librerías **genéricas reutilizables** para aplicaciones .NET.

**Ubicación:** `RESLibrariesNet10/Docs/PENDING.md`

---

## Checkpoint (2026-10-12) — orden por dependencias

- [x] **`2_LibraryUtils`** (antes 4) — depende de Classes + Helpers  
- [x] **`3_LibraryServices`** (antes 3) — depende de Classes + Helpers + Utils  

Orden de compilación: **1 Classes → 2 Helpers → 3 Utils → 4 Services → Host**

---


## Checkpoint (2026-10-12) — renumeración post-fusión

- [x] `3_LibraryHelpers` → **`2_LibraryHelpers`**
- [x] `3_LibraryServices` → **`3_LibraryServices`**
- [x] `5_LibraryUtils` → **`4_LibraryUtils`**

Orden: **1 Classes → 2 Helpers → 3 Services → 4 Utils → Host**  
(Nota: Utils puede depender de Helpers; Services de Classes/Helpers/Utils según referencias.)

---


## Checkpoint (2026-10-12) — unificar Base + Classes

- [x] **`1_LibraryBase` + `2_LibraryClasses` → `1_LibraryClasses`** (proyecto único)
- [x] Helpers de texto: `2_LibraryHelpers/Text` → **`1_LibraryClasses/Text/Helpers`**
  - Namespace: `_1_LibraryClasses.Text` (`C_Hlp_Text`, `C_Hlp_String`)
- [x] Solución y ProjectReference actualizados (sin Base/Classes separados)

Orden de compilación: **Classes → Helpers → Utils → Services → Host**

---



## Checkpoint (2026-10-08) — división Base / Classes

- [x] **`1_LibraryBase`**: tipos **sin** `using System.*` / externos (44 ficheros)
- [x] **`2_LibraryClasses`**: resto de lo que era `1_LibraryClasses` (32 ficheros); referencia a Base
- [x] Renumeración: Helpers→**3**, Services→**4**, Utils→**5**
- [x] Mismo `RootNamespace` / namespaces de código `_1_LibraryClasses` en Base y Classes
- [x] Solución y ProjectReference actualizados

Orden de compilación: Base → Classes → Helpers → Utils → Services → Host

## Checkpoint (2026-10-07)

### Hecho recientemente
- [x] Namespace raíz para Entity/Item/Pagination/ValueDated: `_1_LibraryClasses` (sin `.Base` / `.Classes`)
- [x] `C_EventArgs` / `C_Exception`: propiedad **`CreateDate`** (antes `DateTime`)
- [x] Regiones CONVENTIONS §4 en `C_Entity`, `C_Register`, `C_EntityNamed`, `C_RegisterNamed` (copias `.old` conservadas)
- [x] Helpers File documentados; modelos Log documentados; GLogger / C_Srv_Logger documentados
- [x] Log: modelos en `1_LibraryClasses/Log`; loggers en `4_LibraryUtils/Log`; `C_Hlp_DebugLog` / `C_Hlp_StackTrace` en Classes
- [x] Named sin `I_Named`: copia de nombre solo si el origen es tipo Named y `Name` no vacío

### Foco actual
Pulir **1_LibraryClasses** y mantener Utils/Services estables.

---

## Alta
| Tarea | Notas |
|-------|--------|
| README de `1_LibraryClasses` (qué es la lib + mapa de carpetas) | Documentación de entrada |
| Revisar usings residuales / ImplicitUsings tras cambio de namespace | Compilar en Visual Studio |



## Checkpoint (2026-11-12) — copia / Clone / CreateDate

### Convención deseada (clases con estado)
- [ ] **Constructor de copia** `C_Foo(C_Foo source)`: copia los valores de la instancia; **no** reutilizar el `CreateDate` del origen (si la clase tiene `CreateDate`).
- [ ] **Método `Clone()`**: nueva instancia equivalente; **`CreateDate` nuevo** (momento del clone), no el del original.
- [ ] Aplicar de forma sistemática en tipos de dominio (`C_Result*`, `C_Word`/`C_String`/`C_Text`, `C_Entity`/`C_Register`, Log, Event, …) según tengan sentido.

### Revisar modelo de `CreateDate` (prioridad media)
- [ ] **Cuándo se asigna** `CreateDate` (ctor vacío, factorías, `Set*`, copia, clone) no está claro ni unificado.
- [ ] Valorar: ¿inmutable tras creación? ¿solo en factorías? ¿`Init` lo toca? ¿Result/Event/Register comparten la misma regla?
- [ ] Documentar la regla en CONVENTIONS cuando se decida.
- [ ] Evitar que el constructor de copia o mutaciones posteriores “refresquen” la fecha sin querer (salvo `Clone`, que sí debe llevar fecha nueva).


## Media
| Tarea | Notas |
|-------|--------|
| Unificar estilo de enums (MAYÚSCULAS en miembros) | Objetivo de convención |
| Ordenar regiones de `C_Result` al estándar CONVENTIONS §4 | Ya tiene regiones, menos limpias |
| Cuando se confirmen `.old`, borrar `C_Entity.cs.old` etc. | Limpieza |

## Baja
| Tarea | Notas |
|-------|--------|
| Actualizar `ARCHITECTURE.md` al estado real | Base, Register, Log, sin GDC |
| Tests del núcleo (Id, Init, Register rehidratación, Permissions, Logger) | |
| Valorar CQRS / FluentResults | Aparcar hasta núcleo estable |
| Inventariar Event / Result / List / Time / Url y documentar | |

## Sin prioridad (ideas)
- `I_AuditEntry` / auditoría con usuario por acción
- Más especializaciones o helpers de texto
- Logger de fichero con cola asíncrona (descartado por ahora)

---

## Estructura de capas (orden de compilación)
1. `1_LibraryBase` — contratos/entidades sin dependencias System explícitas  
2. `2_LibraryClasses` — Log/Event/Result/… con System (Text, Xml, Json…)  
3. `2_LibraryHelpers` — File, Text, Timer…  
4. `4_LibraryUtils` — GLogger, C_Logger_Memory/File, C_Srv_Logger  
5. `3_LibraryServices` — Crypto, FileService…  
6. Host `RESLibrariesNet10`

## Convención de regiones (resumen)
`PRIVATE FIELDS` → `PUBLIC PROPERTIES` → `CONSTRUCTORS` → `PUBLIC METHODS` → `PRIVATE METHODS`  
Ver `Docs/CONVENTIONS.md` §4.
