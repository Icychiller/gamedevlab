using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameConstants gameConstants;
    private int spawnCount = 0;
    private int overLimit = 0;
    void spawnFromPooler(ObjectType i)
    {
        GameObject item = ObjectPooler.SharedInstance.GetPooledObject(i);
        if (item != null)
        {
            item.transform.position = new Vector3(Random.Range(22f,28f), -0.5f, 0);
            item.SetActive(true);
            CentralManager.centralManagerInstance.registerEnemy(item.transform.GetChild(1).gameObject.GetComponent<EnemyController>().id, this);
        }
        else
        {
            Debug.Log("Not Enough Items in Pool!");
        }
    }

    void Awake()
    {
        spawnMax();
    }
    void Start()
    {
        GameManager.OnCoinCollect += increaseSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnCount < gameConstants.maxEnemySpawn + overLimit)
        {
            spawnFromPooler(ObjectType.greenEnemy);
            spawnCount += 1;
            Debug.Log("Refresh Count:"+spawnCount);
            Debug.Log("Max Count:"+(gameConstants.maxEnemySpawn + overLimit));
        }
    }

    public void spawnMax()
    {
        while (spawnCount < gameConstants.maxEnemySpawn + overLimit)
        {
            spawnFromPooler(ObjectType.greenEnemy);
            spawnCount += 1;
            Debug.Log(spawnCount);
        }
    }

    public void enemyDead()
    {
        Debug.Log("Enemy Dead Called");
        spawnCount -= 1;
        Debug.Log("New spawnCount is: "+spawnCount);
    }

    public void increaseSpawn()
    {
        if(this.overLimit < 3)
        {
            overLimit +=1;
        }
    }
}
