using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CountEnemy : MonoBehaviour
{

    private GameObject[] enemies;
    int enemyCount;
    int totalEnemyCount;

    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Cowboy");
        totalEnemyCount = enemies.Length;
        
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Cowboy");
        enemyCount = enemies.Length;
        EnemyCountBar.instance.SetValue(1.0f - (enemyCount / (float)totalEnemyCount));
        if (enemyCount == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
