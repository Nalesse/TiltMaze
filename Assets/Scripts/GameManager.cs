using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject[] mazes;
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnMaze();
    }

    // Update is called once per frame
    void Update()
    {
        //scoreText.text = "Level: " + score.ToString();
    }

    public void GoalReached()
    {
        Debug.Log("Goal Reached");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    private void SpawnMaze()
    {
        int spawnIndex = Random.Range(0, mazes.Length);
        Instantiate(mazes[spawnIndex], mazes[spawnIndex].transform.position, mazes[spawnIndex].transform.rotation);
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
                timerText.text = "Timer: " + string.Format("{0:00}:{1:00}", minutes, seconds);
            }

            oldSeconds = seconds;
            oldMinutes = minutes;
            
            yield return null;
            
        }
        Debug.Log("Game Over");
    }


}
