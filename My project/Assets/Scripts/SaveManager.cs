using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// S saves and L loads. Score goes to a binary file and transforms go to a json file.
/// </summary>
public class SaveManager : MonoBehaviour
{
    public ScoreManager scoreManager;
    public TargetSpawner targetSpawner;

    private const string ScoreFileName = "score.dat";
    private const string TransformFileName = "transforms.json";

    private string scorePath;
    private string transformPath;

    private void Awake()
    {
        // persistentDataPath sticks around between runs and builds
        scorePath = Path.Combine(Application.persistentDataPath, ScoreFileName);
        transformPath = Path.Combine(Application.persistentDataPath, TransformFileName);
    }

    private void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null)
        {
            return;
        }

        if (keyboard.sKey.wasPressedThisFrame)
        {
            SaveGame();
        }
        else if (keyboard.lKey.wasPressedThisFrame)
        {
            LoadGame();
        }
    }

    private void SaveGame()
    {
        scoreManager.SaveScore(scorePath);

        // grab the data from everything with a TransformSaver
        SaveFile saveFile = new SaveFile();
        foreach (ISaveable saveable in FindSaveables())
        {
            saveFile.objects.Add(saveable.SavedData);
        }

        File.WriteAllText(transformPath, JsonUtility.ToJson(saveFile, true));
        Debug.Log("saved " + saveFile.objects.Count + " objects to " + Application.persistentDataPath);
    }

    private void LoadGame()
    {
        if (!File.Exists(transformPath))
        {
            Debug.Log("no save file yet");
            return;
        }

        scoreManager.LoadScore(scorePath);

        SaveFile saveFile = JsonUtility.FromJson<SaveFile>(File.ReadAllText(transformPath));
        ClearTargets();

        // only the shooter is left after clearing so no duplicate ids here
        Dictionary<string, ISaveable> existing = FindSaveables().ToDictionary(s => s.SaveID);

        foreach (TransformData data in saveFile.objects)
        {
            if (existing.TryGetValue(data.saveID, out ISaveable saveable))
            {
                saveable.LoadFromData(data);
                continue;
            }

            // anything not already in the scene is a target, so rebuild it from its type
            Target target = targetSpawner.SpawnTarget(data.saveID, data.localPosition);
            target.GetComponent<TransformSaver>().LoadFromData(data);
        }

        Debug.Log("loaded " + saveFile.objects.Count + " objects");
    }

    private IEnumerable<ISaveable> FindSaveables()
    {
        return FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<ISaveable>();
    }

    // targets on screen now aren't the ones that got saved, so clear them before rebuilding
    private void ClearTargets()
    {
        foreach (Target target in FindObjectsByType<Target>(FindObjectsSortMode.None))
        {
            // Destroy waits till end of frame, turning it off first keeps it out of FindSaveables
            target.gameObject.SetActive(false);
            Destroy(target.gameObject);
        }
    }
}