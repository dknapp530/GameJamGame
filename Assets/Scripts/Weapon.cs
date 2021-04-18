using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private List<GameObject> BulletPool;
    [SerializeField]
    private GameObject objectToPool;
    [SerializeField]
    private int poolMax;
    [SerializeField]
    private float spawnDistance = 1.0f;
    [SerializeField]
    private float spawnHeight = 0.001f;
    [SerializeField]
    private bool isShooting = false;
    Vector3 spawnHeightVector;
    [SerializeField]
    private PlayerControllerScript playerController;
    [SerializeField]
    private Ammo myAmmo;
    void Start()
    {

        BulletPool = new List<GameObject>();
        GameObject tmp;
        spawnHeightVector = new Vector3(0,spawnHeight,0);
        for (int i = 0; i < poolMax; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            BulletPool.Add(tmp);
        }
    }

    void Update()
    {
       
    }

    public void Fire()
    {
        if (myAmmo.CheckAmmo())
        {
            GameObject bullet = null;
            for (int i = 0; i < BulletPool.Count; i++)
            {
                if (!BulletPool[i].activeInHierarchy)
                {
                    bullet = BulletPool[i];
                    isShooting = true;
                    break;
                }
            }
            if (bullet != null)
            {
                bullet.transform.rotation = playerController.GetPlayerLookRotation();
                bullet.transform.position = playerController.GetPlayerForward() + spawnHeightVector + playerController.GetPlayerPosition()*spawnDistance;
                bullet.SetActive(true);
            }
            else
            {
                isShooting = false;
            }
        }
    }
    public bool CheckShooting()
    {
        return isShooting;
    }
}
