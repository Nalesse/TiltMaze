using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
#pragma warning disable 649

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject[] mazes;
    [SerializeField] private GameObject TitleScreen;
    [SerializeField] private GameObject winUI;
    [SerializeField] private GameObject gameOverUI;
    private static bool gameFirstRun = true;
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnMaze();

        if (!gameFirstRun) return;
        Time.timeScale = 0;
        TitleScreen.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoalReached()
    {
        Time.timeScale = 0;
        winUI.SetActive(true);    
    }

    private void SpawnMaze()
    {
        var spawnIndex = Random.Range(0, mazes.Length);
        Instantiate(mazes[spawnIndex], mazes[spawnIndex].transform.position, mazes[spawnIndex].transform.rotation);
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        TitleScreen.SetActive(false);
    }

    public void QuitGame()
    {
        gameFirstRun = true;
        Application.Quit();
        Debug.Log("Game Quit");
    }

    public void RestartGame()
    {
        gameFirstRun = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator StartTimer(float timerLength)
    {
        float oldMinutes = 0; 
        float oldSeconds = 0;
        for (float i = timerLength; i > 0; i -= Time.deltaTime)
        {
            float minutes = Mathf.FloorToInt(i / 60);
            float seconds = Mathf.FloorToInt(i % 60);

            // Only redraw if not same
            if (oldSeconds != seconds || oldMinutes != minutes)
            {
                timerText.text = "Timer: " + $"{minutes:00}:{seconds:00}";
            }

            oldSeconds = seconds;
            oldMinutes = minutes;
            
            yield return null;
            
        }
        gameOverUI.SetActive(true);
        Time.timeScale = 0;
    }


}
