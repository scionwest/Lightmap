namespace Lightmap.Modeling
{
    public interface IDatabaseModeler
    {
        string DatabaseName { get; }

        DatabaseModelingOptions Create();
    }
}
