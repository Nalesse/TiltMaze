using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float verticleInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 forward = new Vector3(1, 0, 0);
        Vector3 left = new Vector3(0, 0, -1);

        transform.Rotate(forward * Time.deltaTime * rotationSpeed * verticleInput);
        transform.Rotate(left * Time.deltaTime * rotationSpeed * horizontalInput);

        
    }
}
