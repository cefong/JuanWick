using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 5;
    int currentHealth;

    public float speed = 3.0f;
    Vector2 movement;

    Vector2 mousePos;
    Vector2 lookDir;
    public float angle = 0;

    public Rigidbody2D rigidBody;
    public Camera cam;

    Animator animator;

    float isHurtTimer;
    float hurtTime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        isHurtTimer -= Time.deltaTime;
        if (isHurtTimer <= 0)
        {
            animator.SetBool("isHurt", false);
        }
    }

    void FixedUpdate()
    {
        rigidBody.MovePosition(rigidBody.position + movement * speed * Time.deltaTime);
        if (movement.x != 0 || movement.y != 0) animator.SetBool("isMoving", true);
        else animator.SetBool("isMoving", false);
        lookDir = mousePos - rigidBody.position;
        lookDir.Normalize(); // set the length of the vector to 1 since only the direction, not magnitude matters
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rigidBody.rotation = angle - 90;
        // if the mouse is pointing in approximately the same direction we wanna move, then move in that direction
    }

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
        if (amount < 0)
        {
            animator.SetBool("isHurt", true);
            isHurtTimer = hurtTime;
        }
    }
}
