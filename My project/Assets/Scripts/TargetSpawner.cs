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
        TargetBuilder builder;

        switch (Random.Range(0, 3))
        {
            case 0:
                builder = new SmallTarget(targetPrefab, position);
                break;
            case 1:
                builder = new MediumTarget(targetPrefab, position);
                break;
            default:
                builder = new LargeTarget(targetPrefab, position);
                break;
        }

        director.Construct(builder);
    }
}