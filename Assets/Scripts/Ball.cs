using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private bool hasKey = false;
    List<GameObject> portals = new List<GameObject>();
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        portals = GameObject.FindGameObjectsWithTag("Portal").ToList();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            hasKey = true;
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Goal"))
        {
            gameManager.GoalReached();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Destroys the obstacle if the key has been collected and the ball hits the obstacle  
        if (hasKey)
        {
            if (collision.collider.CompareTag("Obsticle"))
            {
                Destroy(collision.gameObject);
                hasKey = false;
            }
        }

        if (collision.collider.CompareTag("Portal"))
        {
            // Gets the index of the portal that the ball collided with then sends the ball to the other portal

            int index = portals.IndexOf(collision.gameObject);
            Vector3 offset = new Vector3(2, 0, 0);
            
            switch(index)
            {
                case 0:
                    transform.position = portals[index + 1].transform.position + offset;
                    break;
                case 1:
                    transform.position = portals[index - 1].transform.position + offset;
                    break;
            }
        }
    }
}
