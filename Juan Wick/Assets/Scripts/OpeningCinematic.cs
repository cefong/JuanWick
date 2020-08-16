using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpeningCinematic : MonoBehaviour
{
    
    float timeCurrent;
    int timeInt;
    int timeIntPrevious;

    public GameObject textBox;
    public GameObject[] character = new GameObject [5];
    public GameObject bulletPrefab;

    //walk is how many seconds walking forward
    //dir is what direction facing. 0 --> stopped (probably not used), 1 --> up, 2 --> right, 3 --> down, 4 --> left
    float[] walk = new float [5];
    float[] dir = new float [5];

    float speed;

    void Start()
    {

        timeCurrent = 0;
        timeInt = 0;
        timeIntPrevious = 0;

        for (int i = 0; i < 5; i++)
        {
            walk[i] = 0;
            dir[i] = 0;
        }


        speed = 2.0f;

    }

    void Update()
    {
        timeIntPrevious = timeInt;
        timeCurrent += Time.deltaTime;
        timeInt = (int)Mathf.Floor(timeCurrent);

        //Debug.Log("timeCurrent, " + timeCurrent + ", timeInt, " + timeInt + ", timeIntPrevious, " + timeIntPrevious);

        if (timeInt != timeIntPrevious)
        {
            checkEvent();
        }
        
    }

    void FixedUpdate()
    {
        for (int i = 0; i < 5; i++)
        {
            characterUpdate(i);
        }
        
    }

    void checkEvent()
    {
        switch (timeInt)
        {
            case 1:
                textBox.GetComponent<Text>().text = "Sure has been a while since we seen the ol' west huh Pal?";
                walk[0] = 6;
                dir[0] = 2;
                walk[1] = 6;
                dir[1] = 2;
                break;
            case 2:

                break;
            case 3:
                break;
                textBox.GetComponent<Text>().text = "";
            case 4:
                break;
            case 5:
                break;
            case 6:
                textBox.GetComponent<Text>().text = "You there, with the horse!";
                walk[2] = 4;
                dir[2] = 2;
                walk[3] = 4;
                dir[3] = 2;
                walk[4] = 4;
                dir[4] = 2;
                break;
            case 7:
                break;
            case 8:
                dir[0] = 3;
                break;
                textBox.GetComponent<Text>().text = "";
            case 9:
                dir[0] = 4;
                break;
            case 10:
                break;
            case 11:
                textBox.GetComponent<Text>().text = "Gimme all yer' lassos!";
                break;
            case 12:
                break;
            case 13:
                break;
                textBox.GetComponent<Text>().text = "";
            case 14:
                break;
            case 15:
                textBox.GetComponent<Text>().text = "Nohow partner, we don't need yer kind 'round here";
                break;
            case 16:
                dir[2] = 2;
                walk[2] = 0.5f;
                break;
            case 17:
                textBox.GetComponent<Text>().text = "Suit yourself,";
                break;
            case 18:
                break;
            case 19:
                textBox.GetComponent<Text>().text = "";
                break;
            case 20:
                break;
            case 21:
                shoot(character[2].GetComponent<Rigidbody2D>().position, character[1].GetComponent<Rigidbody2D>().position);
                break;
            case 22:
                textBox.GetComponent<Text>().text = "Buddy";
                break;
            case 23:
                walk[2] = 1.5f;
                dir[2] = 1;
                walk[3] = 1.0f;
                dir[3] = 1;
                walk[4] = 2.0f;
                dir[4] = 1;
                break;
            case 24:
                break;
            case 25:
                walk[2] = 10f;
                dir[2] = 2;
                walk[3] = 10f;
                dir[3] = 2;
                walk[4] = 10f;
                dir[4] = 2;
                break;
            case 26:
                break;
            case 27:
                break;
            case 28:
                break;
            case 29:
                break;
            case 30:
                break;
            case 31:
                walk[0] = 1;
                dir[0] = 1;
                break;
            case 32:
                walk[0] = 10;
                dir[0] = 2;
                break;
            case 33:
                break;
            case 34:
                break;
            case 35:
                break;
            case 36:
                break;
            case 37:
                break;
            case 38:
                break;
            case 39:
                break;
            case 40:
                break;




        }
    }

    void characterUpdate (int i)
    {

        Rigidbody2D rb = character[i].GetComponent<Rigidbody2D>();

        Vector2 movement;



        switch (dir[i])
        {
            case 1:
                movement.x = 0;
                movement.y = 1;
                break;
            case 2:
                movement.x = 1;
                movement.y = 0;
                break;
            case 3:
                movement.x = 0;
                movement.y = -1;
                break;
            case 4:
                movement.x = -1;
                movement.y = 0;
                break;
            default:
                movement.x = 0;
                movement.y = 0;
                break;
        }

        if (walk[i] > 0)
        {
            walk[i] -= Time.deltaTime;
            rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
        }

        if (i != 1)
        {
            rb.rotation = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg - 90;
        }
    }

    void shoot(Vector2 shooter, Vector2 target)
    {
        Vector2 direction = target - shooter;
        GameObject bulletObject = Instantiate(bulletPrefab, shooter, Quaternion.identity);

        GonzalezBullet bulletCopy = bulletObject.GetComponent<GonzalezBullet>();
        bulletCopy.Launch(direction, 300f);

        //animator.SetTrigger("Launch");
    }
}
