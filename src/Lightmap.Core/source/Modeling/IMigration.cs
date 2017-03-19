namespace Lightmap.Modeling
{
    public interface IMigration
    {
        IDataModel DataModel { get; }

        void Apply();

        void Revert();
    }
}