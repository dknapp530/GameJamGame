using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private int targetRefreshMin;
    [SerializeField]
    private int targetRefreshMax;
    private float timer;

    [SerializeField]
    private float speed;
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private int health;

    void OnEnable()
    {
        health = maxHealth;
        ResetTimer();
        //AquireTarget();
    }

    private void OnDisable()
    {
        GameManager.Instance.EnemyRemoved();
    }

    void ResetTimer()
    {
        timer = Random.Range(targetRefreshMin, targetRefreshMax);
    }

    // Update is called once per frame
    void Update()
    {

        float step = speed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z), step);

        if(timer > 0)
        {
            timer -= Time.deltaTime;
        } else
        {
            ResetTimer();
            AquireTarget();
        }

        if(health <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    void AquireTarget()
    {
        List<GameObject> targets = GameManager.Instance.GetTargets();
        if(target == null)
        {
            target = targets[0];
        }
        float distanceFromCurrentTarget = Vector3.Distance(transform.position, target.transform.position);

        for (var i = 0; i < targets.Count; i++) {
            var tempDist = Vector3.Distance(transform.position, targets[i].transform.position);
            if (tempDist < distanceFromCurrentTarget) {
                target = targets[i];
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            target = other.gameObject;
            ResetTimer();
        }
    }
}
