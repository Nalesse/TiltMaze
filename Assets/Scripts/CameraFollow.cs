using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject ball;

    private GameObject ballPos;

    // Start is called before the first frame update
    void Start()
    {
        ballPos = GameObject.FindGameObjectWithTag("Ball");
    }

    // Update is called once per frame
    void Update()
    {
        ball = ballPos;

        float offset = -30;

        // follows the ball on the z axis + an offset 
        transform.position = new Vector3(transform.position.x, transform.position.y, ball.transform.position.z + offset);
    }
}
