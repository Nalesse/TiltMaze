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
    [SerializeField] private float minPortalDistance;


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
        // adds an offset so that the portal spawns in correctly 
        var portalOffset = new Vector3(0, 0.893f, 0);
        // the for loop iterates two times so that two portals are spawned
        for (int i = 0; i < 2; i++)
        {
            // Generates a random spawn position
            var portalPosition = GenerateSpawnPos();
            if (i == 1)
            {
                // on the second iteration before the second portal is spawned the distance
                // of the spawn position is compared to the first portal to make sure they do not spawn too close together  
                var firstPortal = GameObject.FindGameObjectWithTag("Portal");
                while (Vector3.Distance(firstPortal.transform.position, portalPosition) < minPortalDistance)
                {
                    portalPosition = GenerateSpawnPos();
                }
            }

            var spawnedPortal = Instantiate(portal, portalPosition + portalOffset, portal.transform.rotation);
            spawnedPortal.transform.parent = gameObject.transform;
            
        }

        var keyOffset = new Vector3(0, 1, 0);
        //the key is in a loop just so that any number of keys can be spawned, only 1 is spawned on every level because
        //I did not have enough time to figure out how to ensure the keys spawned before the gates. But I left the loop so that I can build on it later 
        for (int i = 0; i < numberOfKeys; i++)
        {
            var spawnedKey = Instantiate(key, GenerateSpawnPos() + keyOffset, key.transform.rotation);
            spawnedKey.transform.parent = gameObject.transform;
        }

    }

    private Vector3 GenerateSpawnPos()
    {
        // this method ensures that a unique spawn position is generated every time, by putting the used positions in a separate array. 
        if (usedSpawnPos.Count == spawnPostions.Length)
            throw new Exception(
                "The number of spawn positions you are trying to generate exceeds the limit. Either add more spawn positions to the map or reduce the number of items you are trying to spawn");
        var spawnIndex = Random.Range(0, spawnPostions.Length);

        while (usedSpawnPos.Contains(spawnPostions[spawnIndex]))
        {
            spawnIndex = Random.Range(0, spawnPostions.Length);
        }
        usedSpawnPos.Add(spawnPostions[spawnIndex]);
        return spawnPostions[spawnIndex];

    }
}
