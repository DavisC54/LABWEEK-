using UnityEngine;

/// <summary>
/// Saves and loads the position, rotation, and scale of whatever it's on.
/// </summary>
public class TransformSaver : MonoBehaviour, ISaveable
{
    public string saveID; // targets get theirs from the spawner, shooter is set in the inspector

    public string SaveID
    {
        get { return saveID; }
    }

    public TransformData SavedData
    {
        get
        {
            TransformData data = new TransformData();
            data.saveID = saveID;
            data.localPosition = transform.localPosition;
            data.localRotation = transform.localRotation;
            data.localScale = transform.localScale;
            return data;
        }
    }

    public void LoadFromData(TransformData data)
    {
        transform.localPosition = data.localPosition;
        transform.localRotation = data.localRotation;
        transform.localScale = data.localScale;
    }
}