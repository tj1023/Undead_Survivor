using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;

    int level;
    float[] timer = new float[5];
    bool[] boss = Enumerable.Repeat(false, 5).ToArray();

    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        for(int i=0; i<spawnData.Length; i++)
            timer[i] += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 10f), spawnData.Length);

        for(int i=0; i<spawnData.Length; i++)
        {
            if (timer[i] > spawnData[i].spawnTime)
            {
                if (level < i) continue;
                Spawn(i);
                timer[i] = 0;
            }

            if (level > 0 && !boss[level - 1])
            {
                boss[level - 1] = true;
                SpawnBoss(level - 1);
            }
        }

    }

    void Spawn(int level)
    {
        GameObject enemy = GameManager.instance.pool.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
    void SpawnBoss(int level)
    {
        GameObject Boss = GameManager.instance.pool.Get(3);
        Boss.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        Boss.GetComponent<Enemy>().InitBoss(spawnData[level]);
    }
}

[System.Serializable]
public class SpawnData
{
    public float spawnTime;
    public int spriteType;
    public int health;
    public float speed;
    public int exp;
}
