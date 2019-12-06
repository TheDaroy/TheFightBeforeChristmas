using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SubWave
{
    public float timeBetweenSubWaves = 5;
    public int spawnLocation;
    public GameObject[] enemy;
    

}
[System.Serializable]
public class Wave
{
    
    public SubWave[] subWaves;
    

    
}

public class EnemySpawner : MonoBehaviour
{
    public Spawnlocations spawnLocationList;
    public Wave[] waves;


    private void Update()
    {
        
    }

}
