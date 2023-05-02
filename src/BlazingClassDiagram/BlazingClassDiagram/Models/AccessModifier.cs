namespace BlazingClassDiagram.Models
{
    [Flags]
    internal enum AccessModifier
    {
        None = 0,
        Private = 1,
        Public = 2,
        Protected = 4,
        Internal = 8
    }
}
