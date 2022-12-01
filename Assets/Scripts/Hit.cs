using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public Bullet myBullet;
    public float timer = 1;
    private float initialTimer;
    void Start()
    {
        initialTimer = timer; 
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = initialTimer;
            gameObject.SetActive(false);
        }
    }
}
