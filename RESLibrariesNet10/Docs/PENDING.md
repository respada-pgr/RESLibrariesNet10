# Pendientes / backlog — RESLibrariesNet10 (solución completa)

Lista de temas a valorar o completar en **cualquier proyecto** de la solución (`1_LibraryClasses`, `2_LibraryUtils`, `3_LibraryServices`, host).

**Contexto:** librerías **genéricas reutilizables** para cualquier aplicación .NET que las referencie.

**Ubicación:** proyecto principal **`RESLibrariesNet10/Docs/PENDING.md`** (Visual Studio no gestiona bien carpetas físicas solo a nivel de solución).

---

## Tareas priorizadas (2026-09-21)

### Alta

- [x] Estructura de carpetas del repo `dev` reproducida en artifacts (Base, Interfaces, Enums, Database, Event, Result, List, Time, Url, Files)
- [x] **README / mapa de carpetas:** `1_LibraryClasses/README.md` + `1_LibraryClasses/Docs/FOLDER_MAP.md`; README de solución en `RESLibrariesNet10/`.

### Media

- [x] ARCHITECTURE y FOLDER_MAP a nivel solución (capítulos por proyecto)
- [x] **Docs de solución centralizados** en `RESLibrariesNet10/Docs/`: PENDING, CONVENTIONS, ARCHITECTURE, FOLDER_MAP, CHANGELOG, DEV_PROMPT (capítulos por proyecto donde aplica).
- [x] **`C_RegBBDD_*` eliminadas**; `C_Pagination` movida a `Base/`; carpeta `Database/` eliminada.
- [ ] **Unificar estilo de enums**: MAYÚSCULAS en todos los miembros, o documentar excepciones (`E_Language`, `E_Priority`, `E_Result*`, etc.).

### Baja

- [ ] **Helpers / utilidades fuera de `Base`**: decidir qué queda en `1_LibraryClasses` y qué pasa a `2_LibraryUtils`.
  - **Claramente helpers (candidatos a Utils):** `C_FilePath` (`Files/`), `C_Url` / `C_Uri` (`Url/`), `C_DateTime` / `C_NullDateTime` (`Time/`), `C_List_String` (`List/`), `C_Pagination` (ahora en `Base/`).
  - **Soporte transversal (valorar Classes vs Utils):** `C_Result*` (`Result/`), `C_Error` / `C_Warning` / `C_Exception` / `C_EventArgs` (`Event/`).
  - **Ya en Utils (OK):** `C_Mng_Files`, `C_Mng_String` / `C_Mng_Text`, `C_JsonConverter`, Logger, `C_Timer`.
  - Criterio: dominio/núcleo → `1_LibraryClasses/Base`; ayuda técnica (rutas, texto, IO, fechas wrapper) → `2_LibraryUtils`.
- [ ] **Inventariar** `Event` / `Result` / `List` / `Time` / `Url` / `Files` (ligado al punto anterior): documentar rol o mover.
- [ ] **Actualizar `ARCHITECTURE.md`** al estado real (`Base/`, Register, sin `I_Entity` no genérica). Dejarlo para cuando la solución esté más pulida.
- [ ] **Tests del núcleo**: Id, `Init`, rehidratación de Register, Permissions.

---

## Sin prioridad asignada

### Inventario de diseño (acordado)

**Núcleo (mantener)**  
- `I_Initialized` → `I_Identifiable<TId>` → `I_Entity<TId>` → `I_Register<TId>`  
- `I_Auditable`, `I_SoftDeletable` (dentro de Register)  
- `C_Entity*`, `C_Register*`  
- `I_Id` / `C_Id*` si se encapsulan identificadores  

**Muy recomendable**  
- `I_Named`  
- `C_RegisterNamed*` / `C_EntityNamed*` si muchas entidades llevan nombre  

**Opcional**  
- `I_Descriptible`, `I_Sortable`, `I_Initializable` (solo donde haga falta)  

**No crear de momento**  
- `I_Activatable`, `I_Codeable`, `I_Versionable`, `I_Validatable`, `I_Measurable`, etc. hasta que varios tipos las compartan  

**C_Entity vs C_Register**  
```text
C_Entity*     → identidad de entidad (sin auditoría ni soft-delete obligatorios)
C_Register*   → Entity + CreateDate/UpdateDate + IsDeleted/DeleteDate
```
- Sin soft-delete / auditoría completa → `C_Entity` / `C_EntityNamed`  
- Registro de negocio completo → `C_Register` / `C_RegisterNamed`  

### Otros (sin prioridad)

- [ ] Valorar CQRS (ver sección siguiente); no adoptar hasta que haga falta  
- [ ] ¿Hace falta `C_IdNamed`? (suele no: el nombre va en la entidad)  
- [ ] Primera entidad de ejemplo en un proyecto consumidor  
- [ ] Valorar `I_Sortable` en alguna base o solo por entidad  
- [ ] Sincronizar diseño documentado ↔ Visual Studio ↔ rama `dev`  
- [ ] Actualizar `DEV_PROMPT.md` / docs auxiliares cuando cambie el núcleo  
- [x] Inventario de interfaces/clases del núcleo revisado  

---

## Referencia: CQRS (sin prioridad)

Separar **comandos** (escritura) y **consultas** (lectura) solo si el modelo único empieza a doler.

**Recomendación:** un solo modelo `C_*` + repositorios clásicos; DTOs de lectura solo donde la UI lo pida.

---

## Referencias rápidas de contratos actuales

```text
C_EntityNamed<TId>   : C_Entity<TId>, I_Entity<TId>, I_Named     → Guid/Int/Long/String
C_RegisterNamed<TId> : C_Register<TId>, I_Register<TId>, I_Named → Guid/Int/Long/String
```
