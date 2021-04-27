using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private bool hasKey = false;
    public List<GameObject> portals = new List<GameObject>();
    private GameManager gameManager;
    private GameObject key;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        key = GameObject.FindGameObjectWithTag("KeyUI");
        key.gameObject.SetActive(false);
        portals = GameObject.FindGameObjectsWithTag("Portal").ToList();
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
            key.gameObject.SetActive(false);
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
                key.SetActive(false);
            }
        }

        if (collision.collider.CompareTag("Portal"))
        {
            // Gets the index of the portal that the ball collided with then sends the ball to the other portal

            int index = portals.IndexOf(collision.gameObject);
            Vector3 offset = new Vector3(2, 0, 0);

            int normalIndex = index == 0 ? 1 : 0;

            // This is the long form of the unary operator above
            //int normalIndex = 0;
            //if (index == 0)
            //{
            //    normalIndex = 1;
            //}
            //else if (index == 1)
            //{
            //    normalIndex = 0;
            //}

            transform.position = portals[normalIndex].transform.position + offset;
        }
    }
}
