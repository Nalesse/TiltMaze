using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private bool hasKey = false;
    // Start is called before the first frame update
    void Start()
    {
        
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasKey)
        {
            if (collision.collider.CompareTag("Obsticle"))
            {
                Destroy(collision.gameObject);
                hasKey = false;
            }
        }
    }
}
