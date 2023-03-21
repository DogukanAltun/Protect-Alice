using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "level")]
public class LevelManager : ScriptableObject
{
    public int level;

    public int[] EnemyLength;

    public int EnemySpeed;

    public int EnemyGetFaster;

    public int EnemyRange;

    public int[] DestroyWall;

}
