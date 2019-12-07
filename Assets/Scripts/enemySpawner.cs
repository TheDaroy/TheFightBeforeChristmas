using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[System.Serializable]
public class SubWave
{
    
    public int spawnLocation;

   
    public Enemy[] enemies;
    

}
[System.Serializable]
public class Wave
{
    public float timeBetweenSubWaves = 5;
    public SubWave[] subWaves;
    

    
}

public class EnemySpawner : MonoBehaviour
{
    public Spawnlocations spawnLocationList;
    public Wave[] waves;
    int currentWave;
    int currentSubWave;

    int enemyIndex = 0;
    bool waveCleared;
    float subSpawntimer;
    
    public float downTimeTime = 60;
    float downTimeTimer = 0;

    public Transform curves;
    public Transform target;

    private void Update()
    {
        if (waves.Length > 0)
        {
            WaveDeathCheck();
        }
       
        if (waveCleared)
        {
            DownTime();
        }

        if (waves.Length > 0)
        {
            SubDowntime();
        }
       
    }


 
    void WaveDeathCheck()
    {
        
        for (int i = 0; i < waves[currentWave].subWaves.Length; i++)
        {
            for (int x = 0; x < waves[currentWave].subWaves[i].enemies.Length; x++)
            {
                //waves[currentWave].subWaves[i].enemies.First(enemy => enemy.Dead);
                if (!waves[currentWave].subWaves[i].enemies[x].Dead)
                {
                    waveCleared = false;
                    return;
                } 
            }
            
        }
    }
   void SubDowntime()
    {
        subSpawntimer = subSpawntimer + Time.deltaTime;
        if (subSpawntimer >= waves[currentWave].timeBetweenSubWaves)
        {
            subSpawntimer = 0;
            SpawnSubwave();
        }

    }

    void DownTime()
    {
        downTimeTimer = downTimeTimer + Time.deltaTime;
        if (downTimeTimer >= downTimeTime)
        {
            NextWave();
        }
    }
   
    void NextWave()
    {
        currentWave++;
        SpawnSubwave();
    }

 void SpawnSubwave()
    {
        subSpawntimer = subSpawntimer + Time.deltaTime;
        if (subSpawntimer >= waves[currentWave].timeBetweenSubWaves)
        {

            Enemy temp;
            
            for (int i = 0; i < waves[currentWave].subWaves[currentSubWave].enemies.Length; i++)
            {
                if (waves[currentWave].subWaves[currentSubWave].enemies[i])
                {
                    temp = Instantiate(waves[currentWave].subWaves[currentSubWave].enemies[i]
                   , spawnLocationList.spawnlocations[waves[currentWave].subWaves[currentSubWave].spawnLocation].transform.position
                   , spawnLocationList.spawnlocations[waves[currentWave].subWaves[currentSubWave].spawnLocation].transform.rotation);
                    temp.index = enemyIndex;
                    temp.curve = curves.GetChild(waves[currentWave].subWaves[currentSubWave].spawnLocation).GetComponent<BezierCurve>();
                    temp.towerTarget = target;
                    temp.SetTarget(target);
                    enemyIndex++;
                }
                
            }
            currentSubWave++;
            subSpawntimer = 0;
        }
    }

}
