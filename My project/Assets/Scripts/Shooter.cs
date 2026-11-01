using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Stationary gun that aims at the mouse and shoots from the pool.
/// </summary>
public class Shooter : MonoBehaviour
{
    // Shooting
    public BulletPool bulletPool;
    public Transform firePoint;
    public float fireCooldown = 0.15f;

    // Lives
    public int lives = 3;
    public GameObject gameOverText;

    private Camera cam;
    private float nextFireTime;

    private void OnEnable()
    {
        Target.OnReachedPlayer += TakeHit;
    }

    private void OnDisable()
    {
        Target.OnReachedPlayer -= TakeHit;
    }

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        Mouse mouse = Mouse.current;
        if (mouse == null)
        {
            return;
        }

        AimAtMouse(mouse);

        if (mouse.leftButton.isPressed && Time.time >= nextFireTime)
        {
            Fire();
        }
    }

    private void AimAtMouse(Mouse mouse)
    {
        Vector2 mouseWorld = cam.ScreenToWorldPoint(mouse.position.ReadValue());
        Vector2 direction = mouseWorld - (Vector2)transform.position;

        // sprite points up instead of right so take 90 off
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void Fire()
    {
        Bullet bullet = bulletPool.GetBullet();
        if (bullet == null)
        {
            return;
        }

        nextFireTime = Time.time + fireCooldown;
        bullet.Fire(firePoint.position, firePoint.rotation);
    }

    private void TakeHit()
    {
        lives--;
        Debug.Log("hit, lives left: " + lives);

        if (lives > 0)
        {
            return;
        }

        Debug.Log("dead");
        gameOverText.SetActive(true);
        Time.timeScale = 0f;

        // disabling unsubscribes too so it can't keep taking hits after dying
        enabled = false;
    }
}