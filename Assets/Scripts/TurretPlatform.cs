using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPlatform : MonoBehaviour
{
    public float cost;
    player Player;
    public GameObject spawnLocation;
    public GameObject turretPrefab;
    public bool activeTurret = false;

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
    private void Update()
    {
        if (Player && Input.GetKeyDown(KeyCode.X) && Player.goldAmount >= cost && activeTurret == false)
        {
            GameObject temp = Instantiate(turretPrefab, spawnLocation.transform.position, spawnLocation.transform.rotation);
            activeTurret = true;
            temp.transform.GetComponent<Turret>().platform = this;
        }
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    Debug.Log("F");
        //    GameObject temp = Instantiate(turretPrefab, spawnLocation.transform.position, spawnLocation.transform.rotation) ;
        //    activeTurret = true;
        //    Debug.Log("S");
        //    temp.transform.GetComponent<Turret>().platform = this;
        //}
    }
}
