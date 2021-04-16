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
}
