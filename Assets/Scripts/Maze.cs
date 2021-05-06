using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Maze : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float xRotationLimit;
    [SerializeField] private float zRotationLimit;

    [SerializeField] private Vector3[] spawnPostions;
    [SerializeField] private GameObject key;
    [SerializeField] private int numberOfKeys;
    [SerializeField] private GameObject portal;


    private GameManager gameManager;
    [SerializeField] private float timerLength;

    private List<Vector3> usedSpawnPos = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        SpawnItems();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        gameManager.StartCoroutine("StartTimer", timerLength);
    }

    // Update is called once per frame
    void Update()
    {
        MazeRotation();
    }

    private void MazeRotation()
    {
        // reference to axis's 
        float verticleInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        // Made my own vector 3 variables because the way the game is ordinated made Vector3.left incorrect  
        Vector3 forwardAndBack = new Vector3(1, 0, 0);
        Vector3 rightAndLeft = new Vector3(0, 0, -1);

        // Rotation controls for rotating the maze
        // forward and back rotation
        transform.Rotate(forwardAndBack * (Time.deltaTime * rotationSpeed * verticleInput));
        // right and left rotation
        transform.Rotate(rightAndLeft * (Time.deltaTime * rotationSpeed * horizontalInput));

        // stores the current Euler rotation inside a variable 
        Vector3 rotation = transform.localEulerAngles;
        // Prevents rotation on the y axis
        rotation.y = 0;

        // Angle limits for every axis, positive and negative directions

        // positive x rotation limit
        if (rotation.x >= xRotationLimit && rotation.x <= 90 && verticleInput > 0)
        {
            rotation.x = xRotationLimit;
        }
        // negative x rotation limit
        if (rotation.x <= (360 - xRotationLimit) && rotation.x >= 270 && verticleInput < 0)
        {
            rotation.x = (360 - xRotationLimit);
        }
        // positive z rotation limit
        if (rotation.z >= zRotationLimit && rotation.z <= 90 && horizontalInput < 0)
        {
            rotation.z = zRotationLimit;
        }
        // negative z rotation limit 
        if (rotation.z <= (360 - zRotationLimit) && rotation.z >= 270 && horizontalInput > 0)
        {
            rotation.z = (360 - zRotationLimit);
        }
        // converts the Euler rotation into a Quaternion
        transform.rotation = Quaternion.Euler(rotation);
    }

    private void SpawnItems()
    {
        var keyOffset = new Vector3(0, 1, 0);
        for (int i = 0; i < numberOfKeys; i++)
        {
            var spawnedKey = Instantiate(key, GenerateSpawnPos() + keyOffset, key.transform.rotation);
            spawnedKey.transform.parent = gameObject.transform;
        }
        
        var portalOffset = new Vector3(0, 0.893f, 0);
        for (int i = 0; i < 2; i++)
        {
            var spawnedPortal = Instantiate(portal, GenerateSpawnPos() + portalOffset, portal.transform.rotation);
            spawnedPortal.transform.parent = gameObject.transform;
        }
        
    }

    private Vector3 GenerateSpawnPos()
    {
        if (usedSpawnPos.Count != spawnPostions.Length)
        {
            var spawnIndex = Random.Range(0, spawnPostions.Length);

            while (usedSpawnPos.Contains(spawnPostions[spawnIndex]))
            {
                spawnIndex = Random.Range(0, spawnPostions.Length);
            }
            usedSpawnPos.Add(spawnPostions[spawnIndex]);
            return spawnPostions[spawnIndex];
        }

        throw new Exception("The number of spawn positions you are trying to generate exceeds the limit. Either add more spawn positions to the map or reduce the number of items you are trying to spawn");
    }
}
