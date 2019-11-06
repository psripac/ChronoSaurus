using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum weaponType {Sword=0, gun1=1};
    public weaponType Type = weaponType.Sword;

    public float damage = 0.0f;
    public float range = 1.0f;
    public int ammo = -1;
    public float recoveryDelay = 0.0f;
    public bool collected = false;
    public bool isEquipped = false;
    public bool canFire = true;

    public Weapon nextWeapon = null;


    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
