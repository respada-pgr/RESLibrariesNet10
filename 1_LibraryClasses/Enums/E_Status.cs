namespace _1_LibraryClassesNet10.Enums
{
    public enum E_Status
    {
        UNDEFINED = -1,
        INITIAL = 0,
        PENDING = 1,
        PROCESSING = 2,
        SUCCESSFUL = 3,
        ERROR = 4,

        PENDING_CREATE = 100,
        PENDING_INSERT = 101,
        PENDING_UPDATE = 102,
        PENDING_DELETE = 103,
        PENDING_LOAD = 104,
        PENDING_SAVE = 105,
        PENDING_EXECUTE = 106,
        PENDING_SEND = 107,
        PENDING_RECEIVE = 108,
        PENDING_VALIDATE = 109,

        CREATING = 200,
        INSERTING = 201,
        UPDATING = 202,
        DELETING = 203,
        LOADING = 204,
        SAVING = 205,
        RUNNING = 206,
        SENDING = 207,
        RECEIVING = 208,
        VALIDATING = 209,

        CREATED = 300,
        INSERTED = 301,
        UPDATED = 302,
        DELETED = 303,
        LOADED = 304,
        SAVED = 305,
        FINISHED = 306,
        SENT = 307,
        RECEIVED = 308,
        VALIDATED = 309,

        NEW = 600,
        READY = 601,
        DUPLICATED = 602,
        EXISTS = 603,
        NOT_EXISTS = 604
    }
}