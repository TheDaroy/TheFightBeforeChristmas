using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Building : entity
{
    public float cost;
    public int maxLevels = 2;
    player Player;
    public GameObject levelPrefab;
    public GameObject levelSpawnLocation;
    public Building mainBuilding;

    public float levelHealthIncrease = 500;
    public override void Start()
    {
        base.Start();
        mainBuilding = this;
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player = collision.gameObject.GetComponent<player>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player = null;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Player && Input.GetKeyDown(KeyCode.X) && Player.goldAmount >= cost)
        {
            GameObject temp = Instantiate(levelPrefab, levelSpawnLocation.transform.position, levelSpawnLocation.transform.rotation);         
            temp.transform.GetComponent<BuildingLevel>().mainBuilding = this;
            levelHealthIncrease += maxHealth;
            health = maxHealth;
            levelSpawnLocation = temp.GetComponent<BuildingLevel>().levelSpawnLocation;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            GameObject temp = Instantiate(levelPrefab, levelSpawnLocation.transform.position, levelSpawnLocation.transform.rotation);
            temp.transform.GetComponent<BuildingLevel>().mainBuilding = this;
            levelHealthIncrease += maxHealth;
            health = maxHealth;
            levelSpawnLocation = temp.GetComponent<BuildingLevel>().levelSpawnLocation;
            temp.GetComponent<SpriteRenderer>().sortingOrder
        }


        if (Dead)
        {
            SceneManager.LoadScene(0);
        }
    }
}
