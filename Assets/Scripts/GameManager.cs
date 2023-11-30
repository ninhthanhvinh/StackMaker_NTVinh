using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public UnityEvent OnLevelPass;

    int points = 0;
    public int Points
    {
        get => points;
        set
        {
            points = value;
        }
    }   

    int currentLevel = 0;

    [SerializeField] List<Level> levels;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI pointsText;
    List<GameObject> loadedContinent = new();

    PlayerController player;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        player = FindObjectOfType<PlayerController>();
    }

    public void ChooseLevel(int levelNumber)
    {
        if (levelNumber > levels.Count)
        {
            LoadLevel(1);
            return;
        } 
            

        LoadLevel(levelNumber);
    }

    public void GetBrick()
    {
        points++;
    }


    public void LoadLevel(int levelNumber)
    {
        if (levelNumber > levels.Count) return;
        var levelIndex = levelNumber - 1;
        currentLevel = levelNumber;
        levelText.text = currentLevel.ToString();
        points = 0;
        // Load level
        player.transform.position = levels[levelIndex].startPoint;
        // Load continent
        GameObject currentContinent = Instantiate(levels[levelIndex].levelPrefab, Vector3.zero, Quaternion.identity);
        loadedContinent.Add(currentContinent);
    }

    public IEnumerator OnWin()
    {
        yield return new WaitForSeconds(4f);
        pointsText.text = points.ToString();
        OnLevelPass.Invoke();
        Destroy(loadedContinent[^1]);
    }

    public void NextLevel()
    {
        if (currentLevel + 1 > levels.Count)
            LoadLevel(1);
        else
        LoadLevel(currentLevel + 1);
    }

    public void RestartLevel()
    {
        LoadLevel(currentLevel);
    }
}
