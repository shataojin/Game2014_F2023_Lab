using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ScreenBounds
{
    public Boundries horizontal;
    public Boundries vertical;
}

public class BulletBehaviour : MonoBehaviour
{

    [Header("Bullet Properties")]
    public float speed;
    public BulletDirection bulletDirection;
    public BulletManager bulletManager;
    public ScreenBounds bounds;
   // public EnemyBehavior enemyBehavior;
    public Vector3 velocity;
    public BulletType bulletType;

    void Start()
    {
       // SetDirection(bulletDirection);
        bulletManager = FindObjectOfType<BulletManager>();
       // enemyBehavior = FindObjectOfType<EnemyBehavior>();
    }

    void Update()
    {
        Move();
        CheckBounds();
        //CollisionEnemy();
    }

    void Move()
    {
        transform.position += velocity * speed *Time.deltaTime;
        Debug.Log("bullet move ");
    }

    void CheckBounds()
    {
        Debug.Log($"Bullet Position: {transform.position}");
        Debug.Log($"Horizontal Bounds: {bounds.horizontal.min} - {bounds.horizontal.max}");
        Debug.Log($"Vertical Bounds: {bounds.vertical.min} - {bounds.vertical.max}");

        if ((transform.position.x > bounds.horizontal.max) || (transform.position.x < bounds.horizontal.min) || 
            (transform.position.y > bounds.vertical.max) || (transform.position.y < bounds.vertical.min))
        {
            bulletManager.ReturnBullet(this.gameObject, bulletType);
            Debug.Log("bullet CheckBounds ");
        }
    }

    public void SetDirection(BulletDirection direction)
    {
        switch (direction)
        {
            case BulletDirection.UP:
                velocity = Vector3.up;
                break;
            case BulletDirection.RIGHT:
                velocity = Vector3.right;
                break;
            case BulletDirection.DOWN:
                velocity = Vector3.down;
                break;
            case BulletDirection.LEFT:
                velocity = Vector3.left;
                break;
        }
    }

    //void OnTriggerEnter2D(Collider2D collision)
    //{
    //    bulletManager.ReturnBullet(this.gameObject);
    //    Debug.Log("bullet return :" + this.gameObject);
    //}

    ////collision with enmey 
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Enemy"))
    //    {
    //        bulletManager.ReturnBullet(this.gameObject);
    //        enemyBehavior.Reset();
    //        Debug.Log("collision ! rest both ");
    //    }
    //}
    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((bulletType == BulletType.PLAYER) || (bulletType == BulletType.ENEMY && collision.gameObject.CompareTag("Player")))
        {
            bulletManager.ReturnBullet(this.gameObject, bulletType);
        }
    }
}
