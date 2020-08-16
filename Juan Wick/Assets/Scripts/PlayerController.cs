using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 20;
    int currentHealth;

    public float speed = 3.0f;
    Vector2 movement;

    Vector2 mousePos;
    Vector2 lookDir;
    public float angle;

    public Rigidbody2D rigidBody;
    public Camera cam;

    Animator animator;

    float isHurtTimer;
    float hurtTime = 1.0f;

    public GameObject bulletPrefab;

    public float timeInvincible = 0.5f;
    bool isInvincible;
    float invincibleTimer;

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
        if ((Input.GetMouseButtonDown(0)) && !(PauseMenu.GameIsPaused))
        {
            Launch();
        }

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
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
        if (amount < 0)
        {
            if (isInvincible) return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
            animator.SetBool("isHurt", true);
            isHurtTimer = hurtTime;
        }
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        if (currentHealth == 0)
        {
            SceneManager.LoadScene("DeathScene");
        }

    }

    void Launch()
    {
        GameObject bulletObject = Instantiate(bulletPrefab, rigidBody.position, Quaternion.identity);

        Bullet bulletCopy = bulletObject.GetComponent<Bullet>();
        bulletCopy.Launch(lookDir, 300f);

        animator.SetTrigger("Launch");
    }
}
