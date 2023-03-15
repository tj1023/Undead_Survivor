using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Control")]
    public float gameTime;
    public float maxGameTime = 30 * 60f;
    [Header("# Player Info")]
    public int health;
    public int maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 5, 10, 20, 30, 50, 75, 100, 150, 200, 300, 9999 };
    [Header("# Game Object")]
    public Player player;
    public PoolManager pool;
    public Enemy enemy;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
            gameTime = maxGameTime;
    }

    public void GetExp(int gexp)
    {
        exp+=gexp;

        if (exp >= nextExp[level])
        {
            exp -= nextExp[level];
            level++;
        }
    }
}
