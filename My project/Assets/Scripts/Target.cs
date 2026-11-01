using UnityEngine;

/// <summary>
/// Zigzags down the screen and fires an event when it gets shot or makes it past the gun.
/// </summary>
public class Target : MonoBehaviour
{
    public delegate void TargetHit(int points);
    public static event TargetHit OnTargetHit;

    public delegate void ReachedPlayer();
    public static event ReachedPlayer OnReachedPlayer;

    // set by the builders
    public float fallSpeed;
    public float zigzagSpeed;
    public float zigzagWidth;
    public int points;

    // anything below this counts as getting past the gun
    public float bottomY = -4f;

    private float startX;
    private float zigzagTime;

    private void Start()
    {
        startX = transform.position.x;
    }

    private void Update()
    {
        Move();

        if (transform.position.y > bottomY)
        {
            return;
        }

        Debug.Log("target got through");
        OnReachedPlayer?.Invoke();
        Destroy(gameObject);
    }

    private void Move()
    {
        zigzagTime += Time.deltaTime;

        // offset by half the width so it starts centered on where it spawned instead of snapping left
        float halfWidth = zigzagWidth * 0.5f;
        float x = startX + Mathf.PingPong(zigzagTime * zigzagSpeed + halfWidth, zigzagWidth) - halfWidth;
        float y = transform.position.y - fallSpeed * Time.deltaTime;

        transform.position = new Vector3(x, y, 0f);
    }

    public void Hit()
    {
        OnTargetHit?.Invoke(points);
        Destroy(gameObject);
    }
}