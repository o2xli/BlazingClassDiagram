namespace BlazingClassDiagram.Models
{
    [Flags]
    internal enum AccessModifier
    {
        None = 0,
        Private = 2,
        Public = 4,
        Protected = 8,
        Internal = 16
    }
}
