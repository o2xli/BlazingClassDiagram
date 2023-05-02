namespace BlazingClassDiagram.Models
{
    [Flags]
    internal enum ObjectTypes
    {
        None = 0,
        Class = 1,
        Record = 2,
        Interface = 4,
        Struct = 8
    }
}
