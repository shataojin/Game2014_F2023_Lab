using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Range(1, 10)]
    public int enemyNumber = 4;
    public GameObject enemyPrefab;
    public List<GameObject> enemyList;

    // Start is called before the first frame update
    void Start()
    {
        BuildEnemyList();
    }

    private void BuildEnemyList()
    {
        for (int i = 0; i < enemyNumber; i++)
        {
            var enemy = Instantiate(enemyPrefab);
            enemyList.Add(enemy);
        }
    }

}
