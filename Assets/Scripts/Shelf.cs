using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    private List<GameObject> BulletPool;
    [SerializeField]
    private GameObject objectToPool;
    [SerializeField]
    private int poolMax;
    [SerializeField]
    private float ammo;
    [SerializeField]
    private List<GameObject> targets;
    [SerializeField]
    private float timer;
    // Start is called before the first frame update
    void OnEnable()
    {
        targets = new List<GameObject>();
        timer = 1f;
        BulletPool = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < poolMax; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            BulletPool.Add(tmp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            timer = 1f;
            if(targets.Count > 0)
            {
                CheckForDisabled();
                if (targets.Count > 0)
                {
                    Fire();
                }
            }
        }
    }

    public void CheckForDisabled()
    {
        for(int i = 0; i < targets.Count; i++)
        {
            if (!targets[i].activeInHierarchy)
            {
                targets.RemoveAt(i);
                i--;
            }
        }
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
                Vector3 fireDir = new Vector3(targets[0].transform.position.x - transform.position.x, 0, targets[0].transform.position.z - transform.position.z);
                bullet.transform.rotation = Quaternion.LookRotation(fireDir);
                bullet.transform.position = transform.position;
                bullet.SetActive(true);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            targets.Add(other.gameObject);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            for(int i = 0; i < targets.Count; i++)
            {
                if(other.gameObject == targets[i])
                {
                    targets.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
