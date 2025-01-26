using UnityEngine;
using System.Collections.Generic;

public class WaveMgr : MonoBehaviour
{
    public Camera orthoCam;
    private static WaveMgr _instance;
    public static WaveMgr Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public int wave = 0;
    private int freqTracker = 0;

    public bool crabSpawned = false;

    public GameObject basicEnemy;
    public GameObject burstEnemy;
    public GameObject projEnemy;
    public GameObject crabEnemy;
    private List<GameObject> basicEnemies;
    private List<GameObject> burstEnemies;
    private List<GameObject> projEnemies;
    private List<GameObject> crabEnemies;
    private CrabEnemy theCrab;

    void Start()
    {
        basicEnemies = EnemyMgr.Instance.CreateEnemyPool(basicEnemy, 500);
        burstEnemies = EnemyMgr.Instance.CreateEnemyPool(burstEnemy, 50);
        projEnemies = EnemyMgr.Instance.CreateEnemyPool(projEnemy, 50);
        crabEnemies = EnemyMgr.Instance.CreateEnemyPool(crabEnemy, 1);

        crabSpawned = false;
    }

    void FixedUpdate()
    {
        if (!GameManager.instance.IsPlaying()) { return; }
        freqTracker++;
        switch (wave)
        {
            case 1:
                SpawnWave(200, basicEnemies);
                SpawnWave(7500, burstEnemies);
                break;
            case 2:
                SpawnWave(100, basicEnemies);
                SpawnWave(4000, burstEnemies);
                SpawnWave(800, projEnemies);
                break;
            case 3:
                SpawnWave(100, projEnemies);
                break;
            case 4:
                SpawnWave(300, basicEnemies);
                SpawnWave(1000, burstEnemies);
                SpawnWave(1000, projEnemies);
                break;
            case 5:
                SpawnWave(100, basicEnemies);
                SpawnWave(2500, burstEnemies);
                SpawnWave(800, projEnemies);
                break;
            case 6:
                //Crab to spawn here
                if (!crabSpawned)
                {
                    theCrab = SpawnTheCrab();
                }
                if (theCrab.GetAscended())
                {
                    SpawnWave(250, basicEnemies);
                    SpawnWave(5000, burstEnemies);
                    SpawnWave(1000, projEnemies);
                }
                break;
        }

        if (freqTracker > 10000)
        {
            freqTracker = 0;
        }
    }

    private CrabEnemy SpawnTheCrab()
    {
        crabSpawned = true;
        GameObject e = EnemyMgr.Instance.GetEnemyFromPool(crabEnemies);
        if (e == null) { return null; }
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float camHalfHeight = orthoCam.orthographicSize;
        float camHalfWidth = screenAspect * camHalfHeight;
        int whereVert = Random.Range(-1, 2); // -1, 0, 1
        int whereHoriz = whereVert == 0 ? Random.Range(0, 2) * 2 - 1 : Random.Range(-1, 2); //-1, 0, 1, but disallow 0,0
        float spawnVert = orthoCam.transform.position.y + Random.Range(-camHalfHeight, camHalfHeight);
        spawnVert += whereVert * 2 * camHalfHeight;
        float spawnHoriz = orthoCam.transform.position.x + Random.Range(-camHalfWidth, camHalfWidth);
        spawnHoriz += whereHoriz * 2 * camHalfWidth;
        e.transform.position = new Vector3(spawnHoriz, spawnVert, 0f);
        e.SetActive(true);

        return e.GetComponent<CrabEnemy>();
    }

    private void SpawnWave(int freq, List<GameObject> pool)
    {
        if (freqTracker % freq == 0)
        {
            GameObject e = EnemyMgr.Instance.GetEnemyFromPool(pool);
            if (e == null) { return; }
            float screenAspect = (float)Screen.width / (float)Screen.height;
            float camHalfHeight = orthoCam.orthographicSize;
            float camHalfWidth = screenAspect * camHalfHeight;
            int whereVert = Random.Range(-1, 2); // -1, 0, 1
            int whereHoriz = whereVert == 0 ? Random.Range(0, 2) * 2 - 1 : Random.Range(-1, 2); //-1, 0, 1, but disallow 0,0
            float spawnVert = orthoCam.transform.position.y + Random.Range(-camHalfHeight, camHalfHeight);
            spawnVert += whereVert * 2 * camHalfHeight;
            float spawnHoriz = orthoCam.transform.position.x + Random.Range(-camHalfWidth, camHalfWidth);
            spawnHoriz += whereHoriz * 2 * camHalfWidth;
            e.transform.position = new Vector3(spawnHoriz, spawnVert, 0f);
            e.SetActive(true);
        }
    }

    public List<GameObject> GetBurstEnemyPool()
    {
        return burstEnemies;
    }

    public void AdvanceWave()
    {
        wave++;
    }
}
