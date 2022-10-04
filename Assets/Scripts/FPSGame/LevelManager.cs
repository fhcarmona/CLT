using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI startText, scoreText, hitCounterText;
    private DuckSpawner spawner;
    private GameObject buttonGroup, restartButton, aim;

    private float delayBegin, score;
    private int hitCount, missCount, level, unlockedLevels;

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

        // 
        BlockLevels();
    }

    private void Update()
    {
        UpdateScoreText();
        UpdateHitCounterText();
    }

    // Custom methods
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

    private void BlockLevels()
    {
        LoadGame();

        // Disable levels
        for (int index = 0; index < duckQuantityList.Length; index++)
        {
            Debug.Log("Button - Level " + (index + 1).ToString());
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

    public void CheckGameOver()
    {
        int ducksCount = hitCount + missCount;

        Debug.Log($"Level[{level}], Total Miss Hit Count[{ducksCount}], Level Duck Count[{duckQuantityList[level]}], Duck Hits[{hitCount}], Duck Miss[{missCount}]");
        
        if (ducksCount >= duckQuantityList[level])
        {
            if (hitCount >= duckRequiredList[level])
                SaveGame();

            aim.gameObject.SetActive(false);
            restartButton.SetActive(true);
        }
    }

    private void SaveGame()
    {
        Debug.Log($"A={PlayerPrefs.GetInt("UnlockedLevels")}");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("UnlockedLevels", level > 15 ? level : (level + 2));
        PlayerPrefs.Save();
        Debug.Log($"B={PlayerPrefs.GetInt("UnlockedLevels")}");
    }

    private void LoadGame()
    {
        Debug.Log($"C={PlayerPrefs.GetInt("UnlockedLevels")}");

        if (PlayerPrefs.HasKey("UnlockedLevels"))
        {
            unlockedLevels = PlayerPrefs.GetInt("UnlockedLevels");
            Debug.Log($"D={PlayerPrefs.GetInt("UnlockedLevels")}");
        }
        else
        {
            unlockedLevels = 1;
            Debug.Log($"E={PlayerPrefs.GetInt("UnlockedLevels")}");
        }
    }
}
