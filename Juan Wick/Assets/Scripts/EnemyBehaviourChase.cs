using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourChase : MonoBehaviour
{
    public int enemyMaxHealth = 3; // controls the health of the enemy
    int currentHealth; // the current health of the enemy

    Vector2 movement; // which way it is moving
    Vector2 lookDir; // which way it is planning to move

    Rigidbody2D rigidbody; // declares a rigidbody

    GameObject chased; // declares a game object that is being chased
    Vector2 target; // coordinates of the chased object

    bool isBumped = false; // is the enemy bumping into anything?
    public float bumpedTime = 0.5f; // how long will the enemy by fixing position for
    float bumpedTimer = 0;

    public int damage = 1; // how much damage does this cause the player

    Animator animator; // the animator controller for this enemy

    public bool isAttacking = false; // does it start off attacking
    public float redZone = 1.0f; // from how far away can you activate this enemy

    public float minSpeed = 0.0f; // gives characters a range of movement
    public float maxSpeed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>(); // gets the animation of the enemy
        rigidbody = GetComponent<Rigidbody2D>(); // gets the rigidbody of the enemy
        chased = GameObject.FindWithTag("Player"); // locks onto the player gameobject

        currentHealth = enemyMaxHealth; // set the enemy to max health
    }

    // Update is called once per frame
    void Update()
    {
        target = chased.transform.position; // gets coords of the player
        if (!isAttacking && (target.x > (rigidbody.position.x - redZone)) && (target.x < (rigidbody.position.x + redZone)) &&
            (target.y > (rigidbody.position.y - redZone)) && (target.y < (rigidbody.position.y + redZone))) // if the player is close enough to trigger it
        {
            isAttacking = true; // activate the enemy
            animator.SetTrigger("isMoving"); // play the moving animation
        }

        if (!isAttacking) return; // if it's not activated yet, return

        if (!isBumped)
        {
            lookDir = target - rigidbody.position; // sets the direction to move to get to the player
            lookDir.Normalize(); // get the direction (no magnitude)
        } else
        {
            bumpedTimer -= Time.deltaTime; // take time off the bumper timer
            if (bumpedTimer <= 0)
            {
                isBumped = false; // if the timer is done, the enemy is no longer backing up
            }
        }
    }

    void FixedUpdate()
    {
        float speed = Random.Range(minSpeed, maxSpeed); // set the speed to something between the specified values
        if (!isAttacking) return;
        if (!isBumped)
        {
            rigidbody.MovePosition(rigidbody.position + lookDir * speed * Time.deltaTime); // move toward the player
        } else
        {
            rigidbody.MovePosition(rigidbody.position + lookDir * (-speed) * Time.deltaTime); // move the enemy away from what it was walking toward

        }
        
    }

    void OnCollisionEnter2D (Collision2D col) // this function only triggers when something collides 
    { 
        if (col.gameObject.tag == "Wall") // check if we collided with a wall
        {
            isBumped = true; // then we have bumped into the wall
            bumpedTimer = bumpedTime; // reset the timer
        }

    }

    void OnCollisionStay2D(Collision2D col) // this triggers when anything is touching the enemy
    {
        PlayerController player = col.gameObject.GetComponent<PlayerController>(); // get the player controller script if we're touching the player
        if (player != null)
        {
            player.ChangeHealth(-damage); // hurt the player
        }
    }

    public void ChangeHealth(int amount) 
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, enemyMaxHealth); // the enemy is hurt
        if (currentHealth == 0) Destroy(gameObject); // if it has no health remaining, destroy it
    }
}
