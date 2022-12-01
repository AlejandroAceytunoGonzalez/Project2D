using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float life = 5;
    private Bullet bullet;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            bullet = collision.GetComponent<Bullet>();
            life -= bullet.damage;
            bullet.DisableMe();
            if (life <= 0) Destroy(gameObject);
        }

    }
}
