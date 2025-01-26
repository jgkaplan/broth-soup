using UnityEngine;
using System.Collections.Generic;

public class EnemyMgr : MonoBehaviour
{

    private static EnemyMgr _instance;
    public static EnemyMgr Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    public List<GameObject> CreateEnemyPool(GameObject e, int n)
    {
        List<GameObject> newPool = new List<GameObject>();
        for (int i = 0; i < n; i ++)
        {
            newPool.Add(Instantiate(e, transform));
            newPool[i].GetComponent<Enemy>().InitEnemy(i);
            newPool[i].SetActive(false);
        }

        return newPool;
    }

    public GameObject GetEnemyFromPool(List<GameObject> pool)
    {
        for (int i = 0; i < pool.Count; i ++)
        {
            if (!pool[i].activeSelf)
            {
                return pool[i];
            }
        }

        return null;
    }

    public void ReturnEnemyToPool(GameObject e)
    {
        e.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
