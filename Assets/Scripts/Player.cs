using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float life = 5;
    private Bullet bullet;
    public PlayerController player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyBullet")
        {
            bullet = collision.GetComponent<Bullet>();
            life -= bullet.damage;
            bullet.DisableMe();
            player.StartCoroutine(player.Hurting());
            if (life <= 0) Destroy(gameObject);
        }

    }
}
