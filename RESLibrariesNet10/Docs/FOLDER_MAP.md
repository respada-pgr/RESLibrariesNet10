# Mapa de carpetas — RESLibrariesNet10

Árbol de la **solución** (artifacts / repo `dev`).

**Backlog:** [PENDING.md](PENDING.md)

---

## Solución

```text
RESLibrariesNet10/                 (repo)
├── README.md                      → enlace corto al host
├── RESLibrariesNet10.slnx
├── 1_LibraryClasses/
├── 2_LibraryHelpers/
├── 2_LibraryUtils/

├── 3_LibraryServices/
└── RESLibrariesNet10/             ← proyecto host + Docs de solución
    ├── README.md
    ├── Docs/
    │   ├── PENDING.md
    │   ├── CONVENTIONS.md
    │   ├── ARCHITECTURE.md
    │   ├── FOLDER_MAP.md
    │   ├── CHANGELOG.md
    │   └── DEV_PROMPT.md
    ├── Form1.cs
    └── Program.cs
```

---

## 1_LibraryClasses

Incluye **`Log/`**: modelos reutilizables de logging (`C_LogLine`, `C_LogStore`, …).
La fachada global **`GLogger`** vive en `2_LibraryUtils/GlobalLogger`.



```text
1_LibraryClasses/
├── README.md
├── Base/                 # núcleo definitivo
│   C_Id, C_Entity, C_EntityNamed, C_Register, C_RegisterNamed,
│   C_Item, C_ValueDated, C_Permissions, C_Action, C_Pagination
├── Interfaces/
├── Enums/
├── Event/                # soporte (errores, avisos)
└── Result/               # resultados de operaciones
```

| Carpeta | Rol |
|---------|-----|
| **Base** | Dominio reutilizable |
| **Interfaces / Enums** | Contratos y enumeraciones |
| **Event, Result** | Soporte transversal (aún en Classes) |

`Database/` eliminada. Sin `Docs/` local: la documentación de diseño está en el host.

---

## 2_LibraryHelpers

```text
2_LibraryHelpers/
├── 2_LibraryUtils/

├── Converters/       # C_JsonConverter
├── File/           # C_File, C_FilePath
├── Logger/           # C_Logger, modelos, enums
├── Text/      # C_String, C_Text
└── Timer/            # C_Hlp_Timer
```

---

## 3_LibraryServices

```text
3_LibraryServices/
├── Files/
├── Services/
│   ├── Crypto/       # AES-GCM, PBKDF2
│   └── Files/
```

---

## RESLibrariesNet10 (host)

```text
RESLibrariesNet10/
├── Docs/             # documentación de TODA la solución
├── Form1.*
└── Program.cs
```


## 2_LibraryUtils

```text
2_LibraryUtils/
└── GlobalLogger/
    ├── GLogger.cs
    ├── GDebugLogger.cs
    ├── Enums/
    ├── Managers/          # C_StackTrace (o mover a Helpers si se prefiere)
    └── Models/
```

Logger **global** (estático). Services puede usarlo; Helpers **no** referencia Utils (evita ciclo).


### Log (1_LibraryClasses)

```text
Log/
├── E_LogLineType.cs
├── E_LogAddLineMode.cs
├── C_LogLine.cs              : I_LogLine
├── C_LoggedLine.cs           : I_LoggedLine
├── C_LogStore.cs       : I_LogStore
├── C_LogException.cs
├── C_StackTraceInfo.cs       : I_StackTraceInfo
├── C_Result_Logged.cs
└── Interfaces/
    ├── I_LogLine.cs
    ├── I_LoggedLine.cs
    ├── I_LogStore.cs
    └── I_StackTraceInfo.cs
```

Namespace: `_1_LibraryClasses.Log` (clases, enums e interfaces).
