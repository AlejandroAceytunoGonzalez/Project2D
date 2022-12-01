using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTitle : MonoBehaviour
{
    public float speed = 0.2f;
    public float limit = 1000;
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = transform.position + new Vector3(1,0,0)*speed;
        if (transform.position.x >= limit) transform.position = new Vector3(0,0,-10);
    }
}
