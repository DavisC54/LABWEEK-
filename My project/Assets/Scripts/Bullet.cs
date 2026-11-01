using System.Collections;
using UnityEngine;

/// <summary>
/// Flies forward and turns itself back off when it hits a target or runs out of time.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public float speed = 15f;
    public float lifetime = 2f;

    private Rigidbody2D rb;
    private WaitForSeconds lifetimeWait; // cached so every shot doesn't make a new one

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        lifetimeWait = new WaitForSeconds(lifetime);
    }

    public void Fire(Vector2 position, Quaternion rotation)
    {
        transform.SetPositionAndRotation(position, rotation);
        gameObject.SetActive(true);
        rb.linearVelocity = transform.up * speed;

        // turning the object off stops this too, so an early hit won't leave it running
        StartCoroutine(DisableAfterLifetime());
    }

    private IEnumerator DisableAfterLifetime()
    {
        yield return lifetimeWait;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out Target target))
        {
            return;
        }

        target.Hit();
        gameObject.SetActive(false);
    }
}