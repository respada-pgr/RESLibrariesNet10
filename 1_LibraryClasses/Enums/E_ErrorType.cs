namespace _1_LibraryClassesNet10.Enums
{
    public enum E_ErrorType
    {
        NONE = 0,             // Sin error (Éxito)
        ND = -1,              // No determinado / Desconocido

        // Errores de Dominio / Negocio (100+)
        VALIDATION = 100,     // Datos de entrada inválidos o faltantes
        NOT_FOUND = 101,      // El registro/recurso no existe
        DUPLICATE = 102,      // Ya existe un registro con la misma clave
        CONFLICT = 103,       // Regla de negocio violada (ej. borrar cliente con facturas)
        UNAUTHORIZED = 104,   // No tiene permisos para esta acción

        // Errores Técnicos / Sistema (500+)
        ERROR = 500,          // Error genérico de ejecución
        EXCEPTION = 501,      // Excepción no controlada (try-catch)
        DATABASE = 502,       // Fallo en la capa de datos / SQL
        IO = 503,             // Fallo al leer/escribir ficheros
        NETWORK = 504         // Fallo de red / Timeout
    }
}
