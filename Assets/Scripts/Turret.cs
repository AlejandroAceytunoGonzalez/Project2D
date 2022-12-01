using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Vector2 offset = new Vector2(-0.4f,-0.1f);
    public float range = 12;
    public float animationSeconds = 2;
    public float cooldown = 1;
    public float chargedCountDown = 0.5f;
    public GameObject chargingParticles;
    public BulletController bulletController;

    private bool isShooting = false;
    private RaycastHit2D[] laserHits;
    private Vector2 origin;
    private Animator animator;
    private float initialCooldown;

    private void Start()
    {
        animator = GetComponent<Animator>();
        initialCooldown = cooldown;
        cooldown = 0;
    }
    void Update()
    {
        cooldown -= Time.deltaTime;
        animator.SetBool("isShooting", isShooting);
        origin = new Vector2(transform.position.x+offset.x*transform.localScale.x, transform.position.y + offset.y)  ;
        laserHits = Physics2D.RaycastAll(origin,Vector2.left*transform.localScale.x,range);
        Debug.DrawRay(origin, Vector2.left * range, Color.red);
        if (!isShooting)
        foreach (var hit in laserHits)
        {
            if (hit.collider == null) continue;
            if (hit.collider.tag == "Player") if(cooldown <= 0) StartCoroutine(Shoot());
        }
    }
    IEnumerator Shoot()
    {
        isShooting = true;
        cooldown = initialCooldown+animationSeconds;
        yield return new WaitForSeconds(animationSeconds-chargedCountDown); //Animation time
        chargingParticles.SetActive(true);
        yield return new WaitForSeconds(chargedCountDown);
        bulletController.GimmeEnemyTurretBullet(origin, -transform.localScale);
        chargingParticles.SetActive(false);
        isShooting = false;
    }
}
