using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float timer = 300.0f;
    private float currentTime = 300.0f;
    private float bulletSpeed = 10.0f;
    void OnEnable()
    {
        currentTime = timer;
    }



    void Update()
    {
        // Move the bullet forward
        transform.position += transform.forward * Time.deltaTime * bulletSpeed;
        if (currentTime > 0f)
        {
            currentTime -= Time.deltaTime;
        } else
        {
            gameObject.SetActive(false);
        }
    }
}
