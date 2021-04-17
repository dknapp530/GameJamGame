using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Renderer renderer;
    private void Start()
    {
        renderer = gameObject.GetComponent<Renderer>();
    }
    void Update()
    {
        if (renderer.isVisible)
        {
            Vector3 lookPosition = new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z);
            transform.LookAt(lookPosition, Vector3.up);
        }
    }
}
