using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    // Unity classes variables
    [SerializeField]
    private TextMeshProUGUI startText, scoreText, hitCounterText;
    private DuckSpawner spawner;
    private GameObject buttonGroup, restartButton, aim;

    // Game user interface 
    private float delayBegin, score;
    private int hitCount, missCount, level, unlockedLevels;

    // Level difficulty parameters
    private int[] duckQuantityList = { 3, 4, 5, 6, 8, 10, 12, 14, 17, 20, 23, 26, 30, 34, 38, 42};
    private int[] duckRequiredList = { 1, 1, 2, 2, 4, 4, 6, 6, 8, 8, 12, 12, 16, 16, 24, 24 };
    private float[] duckSpeedList = { 0.1f, 0.11f, 0.12f, 0.13f, 0.15f, 0.17f, 0.19f, 0.21f, 0.24f, 0.27f, 0.30f, 0.33f, 0.37f, 0.41f, 0.45f, 0.49f };

    // Monobehavior methods
    private void Awake()
    {
        // Get the spawner component to begin the level
        spawner = GetComponent<DuckSpawner>();

        buttonGroup = GameObject.Find("Group - LevelButton");
        restartButton = GameObject.Find("Button - Restart");
        aim = GameObject.Find("Aim");

        // Default behaviours
        restartButton.SetActive(false);
        startText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        hitCounterText.gameObject.SetActive(false);
        aim.gameObject.SetActive(false);

        // Define which level is playable
        BlockLevels();
    }

    private void Update()
    {
        // Every frame update the score and hit counter
        UpdateScoreText();
        UpdateHitCounterText();
    }

    // Define the delay to start the game and set the ui/gui when delay ended
    IEnumerator BeginDelay(int level)
    {
        // Initial timer user interface
        while (delayBegin >= 0)
        {
            startText.text = (delayBegin--).ToString();

            yield return new WaitForSeconds(1);
        }

        // Define which UI is enable
        startText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        hitCounterText.gameObject.SetActive(true);
        aim.gameObject.SetActive(true);

        SetDifficulty(level);
    }

    // Read the user preferences [windows register] and define which level is enabled
    private void BlockLevels()
    {
        LoadGame();

        // Disable levels
        for (int index = 0; index < duckQuantityList.Length; index++)
        {
            // Define which button is interactable or not
            if ((index + 1) <= unlockedLevels)
                GameObject.Find("Button - Level " + (index + 1).ToString()).GetComponent<Button>().interactable = true;
            else
                GameObject.Find("Button - Level " + (index + 1).ToString()).GetComponent<Button>().interactable = false;

        }
    }

    // Update the score text
    private void UpdateScoreText()
    {
        scoreText.SetText($"Score: {score:0}");
    }

    // Update the counter text
    private void UpdateHitCounterText()
    {
        hitCounterText.SetText($"Hits: {hitCount} - Miss: {missCount}");
    }

    // Set the custom duck spawn difficulty by level
    private void SetDifficulty(int level)
    {
        spawner.SpawnDuck(duckQuantityList[level], 0.75f, duckSpeedList[level]);
    }

    // Definitions to begin a new game
    public void StartGame(int difficulty)
    {
        // Reset variables
        restartButton.SetActive(false);
        buttonGroup.SetActive(false);
        startText.gameObject.SetActive(true);
        aim.gameObject.SetActive(true);

        delayBegin = 3;
        score = 0;
        hitCount = 0;
        missCount = 0;
        level = difficulty;

        // Delay to start
        StartCoroutine(BeginDelay(level));
    }

    // Increment the score define in the DuckMovement class
    public void AddScore(float points)
    {
        score += points;
    }

    // Increment the hit counter
    public void AddHitCount()
    {
        hitCount += 1;
    }

    // Increment the miss counter
    public void AddMissCount()
    {
        missCount += 1;
    }

    // Every time the duck is hit or miss, verify if the game is over
    public void CheckGameOver()
    {
        int ducksCount = hitCount + missCount;
        
        // When the hit miss count is higher or equal the duck level spawn quantity, is considered the level ended
        if (ducksCount >= duckQuantityList[level])
        {
            // When the player hit the required ducks by level, succeeded
            if (hitCount >= duckRequiredList[level])
                SaveGame();

            aim.gameObject.SetActive(false);
            restartButton.SetActive(true);
        }
    }

    // Save the current unlocked level in the windows register
    private void SaveGame()
    {
        PlayerPrefs.SetInt("UnlockedLevels", level > 15 ? level : (level + 2)); // Don't allow the data greater than seventeen
        PlayerPrefs.Save();
    }

    // Search in the register the data to unlock the levels
    private void LoadGame()
    {
        // When has saved data on windows registers
        if (PlayerPrefs.HasKey("UnlockedLevels"))
            unlockedLevels = PlayerPrefs.GetInt("UnlockedLevels");
        else
            unlockedLevels = 1;
    }
}
