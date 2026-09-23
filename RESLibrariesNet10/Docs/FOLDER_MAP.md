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

```text
1_LibraryClasses/
├── README.md
├── Base/                 # núcleo definitivo
│   C_Id, C_Entity, C_EntityNamed, C_Register, C_RegisterNamed,
│   C_Item, C_ValueDated, C_Permissions, C_Action, C_Pagination
├── Interfaces/
├── Enums/
├── Event/                # helpers / soporte (pendiente vs Utils)
├── Result/
├── List/
├── Time/
├── Url/
└── Files/                # C_FilePath
```

| Carpeta | Rol |
|---------|-----|
| **Base** | Dominio reutilizable |
| **Interfaces / Enums** | Contratos y enumeraciones |
| **Event, Result, List, Time, Url, Files** | Soporte / helpers |

`Database/` eliminada. Sin `Docs/` local: la documentación de diseño está en el host.

---

## 2_LibraryUtils

```text
2_LibraryUtils/
├── Converters/       # C_JsonConverter
├── FileManager/      # C_Mng_Files
├── Logger/           # C_Logger, modelos, enums
├── TextManager/      # C_Mng_String, C_Mng_Text
└── Timer/            # C_Timer
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
