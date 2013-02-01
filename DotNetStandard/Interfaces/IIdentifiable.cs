namespace DotNetStandard.Interfaces
{
    interface IIdentifiable<out T>
    {
        T Id { get;}
    }
}