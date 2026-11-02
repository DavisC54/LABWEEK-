/// <summary>
/// Anything that can be saved and loaded implements this.
/// </summary>
public interface ISaveable
{
    string SaveID { get; }
    TransformData SavedData { get; }
    void LoadFromData(TransformData data);
}