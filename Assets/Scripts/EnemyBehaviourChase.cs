using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourChase : MonoBehaviour
{

    Vector2 movement;
    Vector2 lookDir;

    public float angle = 0;
    float speed = 1.0f;

    Rigidbody2D rigidbody;

    GameObject chased;
    Vector2 target;

    bool isBumped = false;
    float bumpedTime = 1;
    float bumpedTimer = 0;
    float backupSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        chased = GameObject.FindWithTag("Player");

        backupSpeed = -speed;

    }

    // Update is called once per frame
    void Update()
    {
        if (!isBumped)
        {
            target.x = chased.transform.position.x;
            target.y = chased.transform.position.y;
            lookDir = target - rigidbody.position;
            lookDir.Normalize();

            movement = lookDir;
        } else
        {
            bumpedTimer -= Time.deltaTime;
            if (bumpedTimer <= 0)
            {
                isBumped = false;
            }
        }
            

        
    }

    void FixedUpdate()
    {
        if (!isBumped)
        {
            rigidbody.MovePosition(rigidbody.position + movement * speed * Time.deltaTime);
        } else
        {
            rigidbody.MovePosition(rigidbody.position + movement * backupSpeed * Time.deltaTime);

        }

        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rigidbody.rotation = angle - 90;
    }

    void OnCollisionEnter2D (Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            isBumped = true;
            bumpedTimer = bumpedTime;
        }

    }
}
