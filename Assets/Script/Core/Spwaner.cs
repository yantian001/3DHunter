using UnityEngine;
using System.Collections;

public class Spwaner : MonoBehaviour
{


    public GameObject[] Enemys;

    int enemyCount = 0;

    public void OnEnable()
    {
        LeanTween.addListener((int)Events.ENEMYDIE, OnEnemyDie);
    }

    public void OnDisable()
    {
        LeanTween.removeListener((int)Events.ENEMYDIE, OnEnemyDie);
    }


    void OnEnemyDie(LTEvent evt)
    {
        enemyCount--;
        Debug.Log("Enemy Count:" + enemyCount.ToString());
    }

    // Use this for initialization
    void Start()
    {
        enemyCount = 0;

        if (!(Enemys.Length > 0))
        {
            Debug.LogError("Dont have enemys to spwan.");
            return;
        }
        SpawnItem[] spawns = (SpawnItem[])FindObjectsOfType(typeof(SpawnItem));
        if (!(spawns.Length > 0))
        {
            Debug.LogError("Dont have enemys spwan position");
        }
        foreach (SpawnItem si in spawns)
        {
            if (!si.isMuti)
            {
                if (GameValue.level >= si.beginLevel)
                {
                    GameObject obj = Instantiate(GetSpawnObject(), si.transform.position, si.transform.rotation) as GameObject;
                    enemyCount++;
                }
            }
            else
            {
                if (si.itemConfigs != null && si.itemConfigs.Length > 0)
                {
                    foreach (SpawnItemConfig sic in si.itemConfigs)
                    {
                        if (GameValue.level >= sic.level)
                        {
                            for (int i = 0; i < sic.count; i++)
                            {
                                Vector3 spawnPoint = DetectGround(si.transform.position + new Vector3(Random.Range(-(int)(si.transform.localScale.x / 2.0f), (int)(si.transform.localScale.x / 2.0f)), 0, Random.Range((int)(-si.transform.localScale.z / 2.0f), (int)(si.transform.localScale.z / 2.0f))));
                                Instantiate(GetSpawnObject(), spawnPoint, si.transform.rotation);
                                enemyCount++;
                            }
                        }
                    }
                }
            }
        }
    }

    GameObject GetSpawnObject()
    {
        if (Enemys.Length < 1)
        {
            return null;
        }
        int index = Random.Range(0, Enemys.Length);
        return Enemys[index];
    }

    /// <summary>
    /// 获取当前敌人总数
    /// </summary>
    /// <returns></returns>
    public int GetEnemyCount()
    {
        return enemyCount;
    }
    /// <summary>
    /// 是否最后一个敌人
    /// </summary>
    /// <returns></returns>
    public bool IsLastEnemy()
    {
        return enemyCount == 1;
    }
    /// <summary>
    /// 是否存在敌人 
    /// </summary>
    /// <returns></returns>
    public bool HasEnemy()
    {
        return enemyCount > 0;
    }

    Vector3 DetectGround(Vector3 position)
    {
        RaycastHit hit;
        if (Physics.Raycast(position, -Vector3.up, out hit, 1000.0f))
        {
            return hit.point;
        }
        return position;
    }
}
