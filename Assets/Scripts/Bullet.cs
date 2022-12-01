using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 1;
    public float damage = 1;
    public float lifetime = 3;
    public GameObject bulletHit;
    public Vector3 hitOffset = new Vector3(0,0,0);
    public float countdownSeconds = 1;
    public ParticleSystem particles;
    
    private BulletController controller;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Collider2D collider2d;
    private float initialLifeTime;
    private bool timerDisabled = false;
    private void Start()
    {
        initialLifeTime = lifetime;
        controller = GameObject.Find("BulletController").GetComponent<BulletController>();
    }
    void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2d = GetComponent<Collider2D>();
        collider2d.enabled = true;
        spriteRenderer.enabled = true;
        if (transform.localScale.x == -1) {
            particles.transform.localScale = new Vector3(-1,1,1);
            rb.velocity = new Vector2(-speed, 0);
        } 
        else rb.velocity = new Vector2(speed,0);
    }
    private void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime < 0 && !timerDisabled) {
            DisableMe(false);
        }   
    }
    public void DisableMe(bool hit = true)
    {
        timerDisabled = true;
        rb.velocity = new Vector2(0, 0);
        particles.Stop();
        collider2d.enabled = false;
        spriteRenderer.enabled = false;
        if (hit) controller.GimmeHit(this, transform.position+(hitOffset*transform.localScale.x));
        transform.localScale = new Vector3(1, 1, 1);
        StartCoroutine(Countdown());
    }
    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(countdownSeconds);
        particles.transform.localScale = new Vector3(1, 1, 1);
        lifetime = initialLifeTime;
        timerDisabled = false;
        gameObject.SetActive(false);
    }
}
