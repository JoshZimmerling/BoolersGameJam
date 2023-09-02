using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{

    Vector2 targetPos;
    
    float currentSpeed;
    float acceleration;
    [SerializeField] float thrust; 

    Rigidbody2D rb; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
                
    }


    private void FixedUpdate()
    {
        rb.AddForce(new Vector2(transform.position.x, transform.position.y) - targetPos * thrust); 
    }


    public void setTargetDestination(Vector2 target)
    {
        targetPos = target;
    }
}
