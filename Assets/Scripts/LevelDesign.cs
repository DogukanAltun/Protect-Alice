using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelDesign : MonoBehaviour
{
    public LevelManager[] levels;
    public LevelManager level;
    public int levelIndex = 1;
    public int enemySpeed;
    public int enemyGetFaster;
    public int enemyRange;
    public GameObject enemies;
    public GameObject walls;
    public int[] destroyWall;
    public int[] enemyLength;
    public CanvasManager canvasManager;
    public static LevelDesign instance;
    public void Awake()
    {
        instance = this;
    }
    
    public void Start()
    {
        canvasManager = FindObjectOfType<CanvasManager>();
        levelIndex = PlayerPrefs.GetInt("LevelIndex");
        level = levels[levelIndex];
        enemies = GameObject.FindGameObjectWithTag("Enemies");
        walls = GameObject.FindGameObjectWithTag("Walls");
        enemyLength = level.EnemyLength;
        enemySpeed = level.EnemySpeed;
        enemyGetFaster = level.EnemyGetFaster;
        enemyRange = level.EnemyRange;
        destroyWall = level.DestroyWall;
        SetWalls();
        SetEnemies();
    }

    public void SetWalls()
    {
        for (int i = 0; i < destroyWall.Length; i++)
        {
           int j = destroyWall[i];
           walls.transform.GetChild(j).gameObject.SetActive(false);
        }   
    }
    public void SetEnemies()
    {
        for (int i = 0; i < enemyLength.Length; i++)
        {
         int j = enemyLength[i];
         GameObject enemy = enemies.transform.GetChild(j).gameObject;
         enemy.SetActive(true);
        }
    }

}
