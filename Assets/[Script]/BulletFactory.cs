using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class BulletFactory : MonoBehaviour
{
    private GameObject bulletPrefab;
    private Sprite playerBulletSprite;
    private Sprite enemyBulletSprite;
    private Transform bulletParent;

    void Start()
    {
        Initialize();

    }

    private void Initialize()
    {
        playerBulletSprite = Resources.Load<Sprite>("Sprites/Bullet");
        enemyBulletSprite = Resources.Load<Sprite>("Sprites/EnemySmallBullet");
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        bulletParent = GameObject.Find("Bullets").transform;
    }

    public GameObject CreateBullet(BulletType type)
    {
        GameObject bullet = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity, bulletParent);

        // Null checks for bulletPrefab and sprites
        if (bullet == null)
        {
            Debug.LogError("Bullet prefab not found.");
            return null; // Return null to indicate an error
        }

        if (playerBulletSprite == null || enemyBulletSprite == null)
        {
            Debug.LogError("Bullet sprites not found.");
            return null; // Return null to indicate an error
        }

        bullet.GetComponent<BulletBehaviour>().bulletType = type;

        switch (type)
        {
            case BulletType.PLAYER:
                bullet.GetComponent<SpriteRenderer>().sprite = playerBulletSprite;
                bullet.GetComponent<BulletBehaviour>().SetDirection(BulletDirection.UP);
                bullet.name = "PlayerBullet";
                break;
            case BulletType.ENEMY:
                bullet.GetComponent<SpriteRenderer>().sprite = enemyBulletSprite;
                bullet.GetComponent<BulletBehaviour>().SetDirection(BulletDirection.DOWN);
                bullet.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 180.0f);
                bullet.name = "EnemyBullet";
                break;
        }

        bullet.SetActive(false);
        return bullet;
    }
}
