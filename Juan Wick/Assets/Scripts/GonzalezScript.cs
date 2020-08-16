using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GonzalezScript : MonoBehaviour
{
    public int gonzalezMaxHealth = 15;
    int currentHealth;

    Vector2 movement;
    Vector2 lookDir;

    public float angle = 0;
    public float speed = 1.0f;

    Rigidbody2D rigidbody;

    GameObject chased;
    Vector2 target;

    bool isBumped = false;
    float bumpedTime = 1;
    float bumpedTimer = 0;
    float backupSpeed;

    Animator animator;

    float isHurtTimer;
    float hurtTime = 1.0f;

    bool isDeadTimer;
    float deadTimer = 2.0f;

    public GameObject bulletPrefab;
    public float reloadTime = 2.0f;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        chased = GameObject.FindWithTag("Player");

        backupSpeed = -speed;
        currentHealth = gonzalezMaxHealth;
        isDeadTimer = false;
    }

    // Update is called once per frame
    void Update()
    {

        reloadTime -= Time.deltaTime;
        if (!isBumped)
        {
            target.x = chased.transform.position.x;
            target.y = chased.transform.position.y;
            lookDir = target - rigidbody.position;
            lookDir.Normalize();

            movement = lookDir;
        }
        else
        {
            bumpedTimer -= Time.deltaTime;
            if (bumpedTimer <= 0)
            {
                isBumped = false;
            }
        }
        isHurtTimer -= Time.deltaTime;
        if (isHurtTimer <= 0)
        {
            animator.SetBool("isHurt", false);
        }
        if (isDeadTimer)
        {
            deadTimer -= Time.deltaTime;
            if (deadTimer < 0.0f) Destroy(gameObject);
        }
        if (reloadTime < 0.0f)
        {
            reloadTime = 2.0f;
            Launch();
        }
    }

    void FixedUpdate()
    {
        if (!isBumped)
        {
            cowboyAI();
            rigidbody.MovePosition(rigidbody.position + movement * speed * Time.deltaTime);
        }
        else
        {
            rigidbody.MovePosition(rigidbody.position + movement * backupSpeed * Time.deltaTime);
        }
        if (movement.x != 0 || movement.y != 0) animator.SetBool("isMoving", true);
        else animator.SetBool("isMoving", false);
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rigidbody.rotation = angle - 90;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        PlayerController player = col.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.ChangeHealth(-1);
        }

        if (col.gameObject.tag == "Wall")
        {
            isBumped = true;
            bumpedTimer = bumpedTime;
        }

    }

    public void ChangeGonzalezHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, gonzalezMaxHealth);
        if (currentHealth == 0 && !isDeadTimer)
        {
            animator.SetTrigger("isDead");
            isDeadTimer = true;
        }
        animator.SetBool("isHurt", true);
        isHurtTimer = hurtTime;
    }

    void Launch()
    {
        GameObject bulletObject = Instantiate(bulletPrefab, rigidbody.position, Quaternion.identity);

        GonzalezBullet bulletCopy = bulletObject.GetComponent<GonzalezBullet>();
        bulletCopy.Launch(lookDir, 300f);

        animator.SetTrigger("Launch");
    }

    void cowboyAI ()
    {
        Vector2 relativeForward = lookDir.normalized;
        Vector2 relativeRight = new Vector2(relativeForward.y, -relativeForward.x);
        Vector2 seek;
        Vector2 temp2d;

        float forward;
        float right;
        float closestDistance = 5;
        float distanceFrom;

        //determine distance from target
        seek.x = chased.transform.position.x;
        seek.y = chased.transform.position.y;

        temp2d = seek - rigidbody.position;
        distanceFrom = temp2d.magnitude;

        //determine relativeForward based on distanceFrom
        forward = distanceFrom - closestDistance;

        //determine relativeRight based on time
        right = Mathf.Sin(Time.realtimeSinceStartup) * 20;

        //combine into a movement vector
        temp2d = forward * relativeForward + right * relativeRight;
        movement = temp2d.normalized;

        Debug.Log("movement.x" + movement.x + "movement.y" + movement.y);


    }
}