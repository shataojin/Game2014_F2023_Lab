using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavour : MonoBehaviour
{
    [SerializeField]
    [Header("Bullet Properties")]
    public Transform bulletSpawnPoint;
    public BulletManager bulletManager;
    public float fireRate = 0.2f;

    [SerializeField]
    private float _speed = 10;

    [SerializeField]
    private Boundries _horizontalBoundries, _verticalBoundries;

    Camera _camera;

    Vector2 destination;

    [SerializeField]
    bool _isMobilePlatform = false;

        // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;

        _isMobilePlatform = Application.platform == RuntimePlatform.Android ||
                            Application.platform == RuntimePlatform.IPhonePlayer;

        bulletManager = FindObjectOfType<BulletManager>();

        InvokeRepeating("FireBullets", 0.0f, fireRate);

    }

    // Update is called once per frame
    void Update()
    {
        if(_isMobilePlatform)
            GetTouchScreenInput();
        else
            GetTraditionalInput();
        
        Move();
        CheckBoundries();
     
    }

    void GetTouchScreenInput()
    {

        foreach (Touch touch in Input.touches)
        {
            destination = _camera.ScreenToWorldPoint(touch.position);
            destination = Vector2.Lerp(transform.position, destination, _speed * Time.deltaTime);

        }
    }

    void GetTraditionalInput()
    {
        float xAxis = Input.GetAxisRaw("Horizontal") * _speed * Time.deltaTime;
        float yAxis = Input.GetAxisRaw("Vertical") * _speed * Time.deltaTime;

        destination = new Vector2(transform.position.x + xAxis, transform.position.y + yAxis);

    }

    void Move()
    {
        transform.position = destination;
    }

    void CheckBoundries()
    {
        //Boundries
        if (transform.position.x < _horizontalBoundries.min)
        {
            transform.position = new Vector3(_horizontalBoundries.max, transform.position.y, transform.position.z);
        }

        if (transform.position.x > _horizontalBoundries.max)
        {
            transform.position = new Vector3(_horizontalBoundries.min, transform.position.y, transform.position.z);
        }

        if (transform.position.y > _verticalBoundries.max)
        {
            transform.position = new Vector3(transform.position.x, _verticalBoundries.max);
        }

        if (transform.position.y < _verticalBoundries.min)
        {
            transform.position = new Vector3(transform.position.x, _verticalBoundries.min, 5);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Debug.Log("I Got HIT!!!");
        }
    }

    void FireBullets()
    {
        var bullet = bulletManager.GetBullet(bulletSpawnPoint.position, BulletDirection.UP);
    }
}
