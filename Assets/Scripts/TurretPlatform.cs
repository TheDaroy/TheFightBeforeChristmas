using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPlatform : MonoBehaviour
{
    public float cost;
    player Player;
    GameObject spawnLocation;
    GameObject turret;
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
        if (Player && Input.GetKeyDown(KeyCode.X) && Player.goldAmount >= cost)
        {
            GameObject tamp = Instantiate(turret, spawnLocation.transform.position, spawnLocation.transform.rotation);

        }
    }
}
