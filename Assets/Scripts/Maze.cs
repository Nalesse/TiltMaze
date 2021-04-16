using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float xRotationLimit;
    [SerializeField] private float zRotationLimit;

    [SerializeField] private Vector3[] spawnPostions;
    [SerializeField] private GameObject key;
    [SerializeField] private GameObject portal;
    // Start is called before the first frame update
    void Start()
    {
        SpawnItems();
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
        transform.Rotate(forwardAndBack * Time.deltaTime * rotationSpeed * verticleInput);
        // right and left rotation
        transform.Rotate(rightAndLeft * Time.deltaTime * rotationSpeed * horizontalInput);

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
        int spawnIndex = Random.Range(0, spawnPostions.Length);

        var spawnedKey = Instantiate(key, spawnPostions[spawnIndex], key.transform.rotation);
        spawnedKey.transform.parent = gameObject.transform;

        while (spawnPostions[spawnIndex] == spawnedKey.transform.position)
        {
            Debug.Log("spawnIndex was equal");

            spawnIndex = Random.Range(0, spawnPostions.Length);
        }
        var spawnedPortal = Instantiate(portal, spawnPostions[spawnIndex], portal.transform.rotation);
        spawnedPortal.transform.parent = gameObject.transform;

  

    }
}
