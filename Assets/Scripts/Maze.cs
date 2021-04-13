using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] float verticleInput;
    [SerializeField] float horizontalInput;
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(0, 5, 0);
    }

    // Update is called once per frame
    void Update()
    {
        verticleInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        
        Vector3 forward = new Vector3(1, 0, 0);
        Vector3 left = new Vector3(0, 0, -1);
        Vector3 up = new Vector3(0, 1, 0);

        transform.Rotate(forward * Time.deltaTime * rotationSpeed * verticleInput);
        transform.Rotate(left * Time.deltaTime * rotationSpeed * horizontalInput);

        Vector3 myRotation = transform.rotation.eulerAngles;
        myRotation.y = 0;
        transform.rotation = Quaternion.Euler(myRotation);


    }
}
