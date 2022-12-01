using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocity = 0;
    public float jumpForce = 0;
    public bool hasJump = true;
    public float xInput;
    public bool isCrouching = false;
    public Vector2 respawn;
    public GameObject chargeObject;
    public GameObject chargeParticle;
    public GameObject chargeSound;
    public float charge = 2;
    public bool isShooting = false;
    public bool isHurt;
    public float hurtTimer = 0.2f;

    public float cooldown = 0.2f;
    public PlatformEffector2D effector;
    public BulletController bulletController;

    private float timerGoThough = 0.2f;
    private bool pendingGoThrough = false;
    private bool pendingShoot = false;
    private float initialCooldown;
    private float initialCharge;
    private float initialEffector;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialCharge = charge;
        initialCooldown = cooldown;
        initialEffector = effector.surfaceArc;
        cooldown = 0;
        charge = 0;
    }

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(xInput * velocity, rb.velocity.y);
        if (xInput < 0) {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
            chargeObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        if (xInput > 0)
        {
            gameObject.transform.localScale = Vector3.one;
            chargeObject.transform.localScale = Vector3.one;
        }
        cooldown -= Time.deltaTime;
        if (Input.GetButton("Fire1") && !isHurt)
        {
            chargeObject.SetActive(true);
            isShooting = true;
            charge += Time.deltaTime;
            if (charge > 0.2f) chargeSound.SetActive(true);
            if (charge > initialCharge) chargeParticle.SetActive(true);
        }
        if ((Input.GetButtonUp("Fire1")|| pendingShoot) && !isHurt) if (cooldown <= 0) Shoot(); else pendingShoot = true;
        
        if (Input.GetAxis("Vertical") < 0 && xInput == 0 && hasJump)
        {
            isCrouching = true;
            if (Input.GetButtonDown("Jump") && !pendingGoThrough) pendingGoThrough = true;
            if (pendingGoThrough) {
                effector.surfaceArc = 0;
                if (timerGoThough <= 0) {
                    pendingGoThrough = false;
                    timerGoThough = 0.2f;
                } 
                else timerGoThough -= Time.deltaTime;
            }
            else effector.surfaceArc = initialEffector;
        }
        else {
            isCrouching = false;
            effector.surfaceArc = initialEffector;
            if (Input.GetButton("Jump") && hasJump) Jump();
        }
    }
    void Jump()
    {
        hasJump = false;
        rb.velocity = new Vector2(rb.velocity.x,jumpForce);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground") hasJump = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case ("DeathZone"):
                transform.position = new Vector3(respawn.x,respawn.y,0);
                break;
        }
    }
    private void Shoot()
    {
        if (charge < initialCharge)
        {
            bulletController.GimmeBulletNormal(chargeObject.transform.position, transform.localScale);
        }
        else
        {
            bulletController.GimmeBulletCharged(chargeObject.transform.position, transform.localScale);
        }
        charge = 0;
        isShooting = false;
        pendingShoot = false;
        chargeObject.SetActive(false);
        chargeParticle.SetActive(false);
        chargeSound.SetActive(false);
        cooldown = initialCooldown;
    }
    public IEnumerator Hurting()
    {
        isHurt = true;
        yield return new WaitForSeconds(hurtTimer);
        isHurt=false;
    }
}
