using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField]
    Vector2 _speedRange;

    float _verticalSpeed;
    float _horizontalSpeed;

    [SerializeField]
    Boundries _verticalBoundries;
    [SerializeField]
    Boundries _horizontalBoundries;

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2( Mathf.PingPong(_horizontalSpeed * Time.time,_horizontalBoundries.max - _horizontalBoundries.min) + _horizontalBoundries.min
            , transform.position.y - _verticalSpeed * Time.deltaTime);

        //Debug.Log($"Bullet Position: {transform.position}");
        //Debug.Log($"Horizontal Bounds: {_horizontalBoundries.min} - {_horizontalBoundries.max}");
        //Debug.Log($"Vertical Bounds: {_verticalBoundries.min} - {_verticalBoundries.max}");
        //Spawn / Reset
        if (_verticalBoundries.min > transform.position.y)
        {
            Reset();
        }

    }

    public void Reset()
    {
        _verticalSpeed = Random.Range(_speedRange.x, _speedRange.y);
        _horizontalSpeed = Random.Range(_speedRange.x, _speedRange.y);
        transform.position = new Vector2(Random.Range(_horizontalBoundries.min, _horizontalBoundries.max), _verticalBoundries.max);
        Debug.Log("enemy ou of Boundries, reset");
    }
}
