using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxFactor : MonoBehaviour
{

    public Transform toFollow;
    public float ratio;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new (toFollow.position.x*ratio,transform.position.y, transform.position.z);
    }
}
