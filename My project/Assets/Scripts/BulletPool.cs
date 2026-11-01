using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Makes all the bullets at the start and reuses them instead of spawning new ones.
/// </summary>
public class BulletPool : MonoBehaviour
{
    public Bullet bulletPrefab;
    public int poolSize = 25;

    private List<Bullet> bullets;

    private void Awake()
    {
        bullets = new List<Bullet>(poolSize);

        for (int i = 0; i < poolSize; i++)
        {
            Bullet bullet = Instantiate(bulletPrefab, transform);
            bullet.gameObject.SetActive(false);
            bullets.Add(bullet);
        }
    }

    // returns null if every bullet is already on screen
    public Bullet GetBullet()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            if (!bullets[i].gameObject.activeInHierarchy)
            {
                return bullets[i];
            }
        }

        Debug.Log("out of bullets");
        return null;
    }
}