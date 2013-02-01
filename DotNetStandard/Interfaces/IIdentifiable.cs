namespace DotNetStandard.Interfaces
{
    public interface IIdentifiable<out T>
    {
        T Id { get;}
    }
}