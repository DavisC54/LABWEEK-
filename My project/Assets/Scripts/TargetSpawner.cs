using System.Collections;
using UnityEngine;

/// <summary>
/// Spawns a random target type at the top of the screen on a timer.
/// </summary>
public class TargetSpawner : MonoBehaviour
{
    public Target targetPrefab;
    public float spawnInterval = 1.5f;
    public float spawnY = 5.5f;
    public float spawnHalfWidth = 5f; // keep inside the screen, the zigzag swings past the spawn point

    // Target types
    private const string SmallType = "SmallTarget";
    private const string MediumType = "MediumTarget";
    private const string LargeType = "LargeTarget";

    private string[] targetTypes = { SmallType, MediumType, LargeType };
    private TargetDirector director;
    private WaitForSeconds spawnWait;

    private void Awake()
    {
        director = new TargetDirector();
        spawnWait = new WaitForSeconds(spawnInterval);
    }

    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return spawnWait;
            SpawnRandomTarget();
        }
    }

    private void SpawnRandomTarget()
    {
        Vector2 position = new Vector2(Random.Range(-spawnHalfWidth, spawnHalfWidth), spawnY);
        SpawnTarget(targetTypes[Random.Range(0, targetTypes.Length)], position);
    }

    // SaveManager uses this too, the type doubles as the save id so loading knows what to rebuild
    public Target SpawnTarget(string type, Vector2 position)
    {
        TargetBuilder builder;

        switch (type)
        {
            case SmallType:
                builder = new SmallTarget(targetPrefab, position);
                break;
            case MediumType:
                builder = new MediumTarget(targetPrefab, position);
                break;
            default:
                builder = new LargeTarget(targetPrefab, position);
                break;
        }

        director.Construct(builder);
        builder.Target.GetComponent<TransformSaver>().saveID = type;
        return builder.Target;
    }
}