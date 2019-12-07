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
    int currentWave=0;
    int currentSubWave=0;

    int enemyIndex = 0;
    bool waveCleared = true;
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
            Debug.Log("test");
            
            SpawnSubwave();
            subSpawntimer = 0;
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
        Debug.Log("test2");
        subSpawntimer = subSpawntimer + Time.deltaTime;
        if (subSpawntimer >= waves[currentWave].timeBetweenSubWaves && currentSubWave < waves[currentWave].subWaves.Length)
        {

            GameObject temp;
            Debug.Log(currentSubWave);
            for (int i = 0; i < waves[currentWave].subWaves[currentSubWave].enemies.Length; i++)
            {
                if (waves[currentWave].subWaves[currentSubWave].enemies[i].gameObject)
                {

                    float offSet = Random.Range(-10, 10);
                    Vector3 test = new Vector3(offSet, 0, 0);
                    Vector2 spawnPoint =  new Vector2(spawnLocationList.spawnlocations[waves[currentWave].subWaves[currentSubWave].spawnLocation].transform.position.x, spawnLocationList.spawnlocations[waves[currentWave].subWaves[currentSubWave].spawnLocation].transform.position.y + offSet);
                    temp = Instantiate(waves[currentWave].subWaves[currentSubWave].enemies[i].gameObject,spawnPoint,
                    spawnLocationList.spawnlocations[waves[currentWave].subWaves[currentSubWave].spawnLocation].transform.rotation);

                    Enemy tempE = temp.GetComponent<Enemy>();
                    tempE.offset = test;
                    tempE.index = enemyIndex;
                    tempE.curve = curves.GetChild(waves[currentWave].subWaves[currentSubWave].spawnLocation).GetComponent<BezierCurve>();
                    tempE.towerTarget = target;
                    tempE.SetTarget(target);
                    enemyIndex++;
                }
                
            }
            currentSubWave++;
            subSpawntimer = 0;
        }
    }

}
