using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum ShotType { Rifle, Shotgun }
[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon", order = 0)]
public class Weapon : ScriptableObject
{
    public float damage = 5;
    public float fireRate;
    public GameObject projectile;
    public ShotType shotType = ShotType.Rifle;
}
