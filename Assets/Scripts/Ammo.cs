using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField]
    private float ammo = 100.0f;
    private readonly Weapon gunBoy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void RefillAmmo()
    {

    }
    public bool CheckAmmo()
    {
        bool returnValue = false;
        if(ammo > 0)
        {
            returnValue = true;
            ammo--;
        }
        return returnValue;
    }

    public float GetAmmo()
    {
        return ammo;
    }
}
