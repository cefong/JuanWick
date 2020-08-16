using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rigidbody2d;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        GonzalezScript gonzalez = other.collider.GetComponent<GonzalezScript>();
        if (gonzalez != null)
        {
            gonzalez.ChangeGonzalezHealth(-1);
        }
        EnemyBehaviourChase enemy = other.collider.GetComponent<EnemyBehaviourChase>();
        if (enemy != null)
        {
            enemy.ChangeHealth(-1);
        }
        Destroy(gameObject);
    }
}
