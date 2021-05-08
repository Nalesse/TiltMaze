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

    private bool portalEnabled = true;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        key = GameObject.FindGameObjectWithTag("KeyUI");
        key.SetActive(false);
        portals = GameObject.FindGameObjectsWithTag("Portal").ToList();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            hasKey = true;
            key.SetActive(true);
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
            if (portalEnabled)
            {
                int index = portals.IndexOf(collision.gameObject);
                Vector3 offset = new Vector3(0, 0, -2);
                int normalIndex = index == 0 ? 1 : 0;
                transform.position = portals[normalIndex].transform.position + offset;
                StartCoroutine(PortalDelay());
            }
            

            // This is the long form of the unary operator above. This is kept in as a note to myself.
            //int normalIndex = 0;
            //if (index == 0)
            //{
            //    normalIndex = 1;
            //}
            //else if (index == 1)
            //{
            //    normalIndex = 0;
            //}

            
        }
    }

    private IEnumerator PortalDelay()
    {
        // adds a portal delay so you don't go back and forth between the portals
        portalEnabled = false;
        yield return new WaitForSeconds(2);
        portalEnabled = true;
    }
}
