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
    private float ammo;
    [SerializeField]
    private PlayerControllerScript playerController;
    void Start()
    {
        BulletPool = new List<GameObject>();
        GameObject tmp;
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
        if (ammo > 0)
        {
            GameObject bullet = null;
            for (int i = 0; i < BulletPool.Count; i++)
            {
                if (!BulletPool[i].activeInHierarchy)
                {
                    bullet = BulletPool[i];
                    break;
                }
            }
            if (bullet != null)
            {
                bullet.transform.rotation = playerController.GetPlayerLookRotation(); //Adjust the rotation so it's 90 degrees from the player
                bullet.transform.position = playerController.GetPlayerPosition(); //Adjust position so that it starts in front of the player
                bullet.SetActive(true);
            }
        }
    }
}
