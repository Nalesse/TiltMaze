using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
        // decrements by time.DeltaTime to countdown in real time
        for (float i = timerLength; i > 0; i -= Time.deltaTime)
        {
            // converts the current time left into minutes and seconds
            float minutes = Mathf.FloorToInt(i / 60);
            float seconds = Mathf.FloorToInt(i % 60);

            // Only redraw if not same. This is just to reduce the amount of time that is spent on this loop, for performance reasons.
            if (oldSeconds != seconds || oldMinutes != minutes)
            {
                // formats the minutes and seconds to always be two digits so that it looks like a timer 
                timerText.text = "Timer: " + $"{minutes:00}:{seconds:00}";
            }

            oldSeconds = seconds;
            oldMinutes = minutes;

            //briefly exits the IEnumerator so that Time.deltaTime can catch up.
            //without this, the game would be stuck in the IEnumerator and time.DeltaTime would not be able to change.  
            yield return null;
            
        }
        gameOverUI.SetActive(true);
        Time.timeScale = 0;
    }


}
