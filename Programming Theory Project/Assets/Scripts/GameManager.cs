using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static GameManager sharedInstance;
    public GameObject[] enemyPrefabs;
    Text countText;
    Text healthText;
    [SerializeField] Text gameOverText;
    PlayerController playerController;
    float[] posZArray;
    [SerializeField] float spawnPosZ = 50;
    [SerializeField] float spawnPosY = 2;
    [SerializeField] float spawnRangeX = 30;
    [SerializeField] float spawnRate = 3;
    [SerializeField] float gameOverTextSpeed = 150;
    bool isGameActive = true;
    float points = 0;
    readonly float spawnRateChangeInterval = 30;
    readonly float spawnRateDecreaseCoefficient = 0.8f;
    const float POINT_INCREASE_PER_SECOND = 1;

    private void Awake()
    {
        sharedInstance = this;
    }
    public static GameManager SharedInstance
    {
        get 
        { 
            return sharedInstance; 
        }
    }

    public bool IsGameActive
    {
        set { isGameActive = value; }
        get { return IsGameActive; }
    }

    private void Start()
    {
        StartCoroutine(SpawnRandomEnemy());
        StartCoroutine(DecreaseSpawnRate());
        posZArray = new float[] { -spawnPosZ, spawnPosZ };
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        countText = GameObject.Find("Count Text").GetComponent<Text>();
        healthText = GameObject.Find("Health Text").GetComponent<Text>();
    }

    private void Update()
    {
        SpawnEnemy();
        CountPoints();
        ShowPlayersHealth();
        MoveGameOverText();
    }

    //Delete before building the game
    void SpawnEnemy()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            int animalIndex = 0;
            Instantiate(enemyPrefabs[animalIndex], GiveRandomSpawnPosition(), enemyPrefabs[animalIndex].transform.rotation);
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            int animalIndex = 1;
            Instantiate(enemyPrefabs[animalIndex], GiveRandomSpawnPosition(), enemyPrefabs[animalIndex].transform.rotation);
        }        
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            int animalIndex = 2;
            Instantiate(enemyPrefabs[animalIndex], GiveRandomSpawnPosition(), enemyPrefabs[animalIndex].transform.rotation);
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            GameOver();
        }
    }

    IEnumerator SpawnRandomEnemy()
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int animalIndex = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[animalIndex], GiveRandomSpawnPosition(), enemyPrefabs[animalIndex].transform.rotation);
        }
    }

    void CountPoints()
    {
        points += POINT_INCREASE_PER_SECOND * Time.deltaTime;
        countText.text = "Count: " + (int)points;
    }

    IEnumerator DecreaseSpawnRate()
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(spawnRateChangeInterval);
            spawnRate *= spawnRateDecreaseCoefficient;
        }
    }

    void ShowPlayersHealth()
    {
        if(playerController.Health < 0)
        {
            playerController.Health = 0;
        }

        healthText.text = "Health: " + playerController.Health;
    }

    private Vector3 GiveRandomSpawnPosition()
    {
        int posZArrayIndex = Random.Range(0, posZArray.Length);
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), spawnPosY, posZArray[posZArrayIndex]);
        return spawnPos;
    }

    void MoveGameOverText()
    {
        if (!isGameActive)
        {
            if(gameOverText.rectTransform.localPosition.x < 0)
            {
                gameOverText.transform.Translate(gameOverTextSpeed * Time.deltaTime * Vector2.right);
            }
        }
    }

    public void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
    }
}
