using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static GameManager sharedInstance;
    public GameObject[] enemyPrefabs;
    [SerializeField] Button returnToMenuButton;
    Text countText;
    Text healthText;
    [SerializeField] Text gameOverText;
    PlayerController playerController;
    float[] posZArray;
    [SerializeField] float spawnPosZ = 42;
    [SerializeField] float spawnPosY = 2;
    [SerializeField] float spawnRangeX = 30;
    [SerializeField] float spawnRate = 3;
    [SerializeField] float gameOverTextSpeed = 150;
    bool isGameActive = true;
    int points = 0;
    readonly float spawnRateChangeInterval = 30;
    readonly float spawnRateDecreaseCoefficient = 0.8f;
    const int POINTS_INCREASE_INTERVAL = 1;

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

    //ENCAPSULATION
    public bool IsGameActive
    {
        set { isGameActive = value; }
        get { return IsGameActive; }
    }

    //ENCAPSULATION
    public int Points
    {
        get { return points; }
    }

    private void Start()
    {
        StartCoroutine(SpawnRandomEnemy());
        StartCoroutine(DecreaseSpawnRate());
        StartCoroutine(CountPoints());
        posZArray = new float[] { -spawnPosZ, spawnPosZ };
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        countText = GameObject.Find("Count Text").GetComponent<Text>();
        healthText = GameObject.Find("Health Text").GetComponent<Text>();
    }

    private void Update()
    {
        ShowPlayersHealth();
        MoveGameOverText();
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

    IEnumerator CountPoints()
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(POINTS_INCREASE_INTERVAL);
            points++;
            countText.text = "Count: " + points;
        }
    }

    IEnumerator DecreaseSpawnRate()
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(spawnRateChangeInterval);
            spawnRate *= spawnRateDecreaseCoefficient;
        }
    }

    //ASTRACTION
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

    //ASTRACTION
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

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    public void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        returnToMenuButton.gameObject.SetActive(true);
        Storage.instance.CheckRecord();
    }
}
