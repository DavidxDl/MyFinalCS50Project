using System;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private LayerMask layer; // number of the layer mask
    [SerializeField] private LayerMask player;
    
    [SerializeField] private float speed = 20;
    [SerializeField] private float rayDistance = 1;
    [SerializeField] private float offset;

    private bool resetPosition = false;
    private BoxCollider2D collider;

    private float initialPosition;
 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        initialPosition = transform.position.y;
    }
    private void FixedUpdate()
    {
        if (transform.position.y == initialPosition) rb.velocity = Vector3.zero;

        Collision();

        if(resetPosition && transform.position.y < initialPosition)
        {
            rb.velocity = Vector3.up * speed * Time.deltaTime;
        }
        else
        {
            resetPosition = false;
        }
    }

   private void Collision()
    {
             // if collide with ground goes up
        if ( Physics2D.Raycast(collider.bounds.center - new Vector3(0, collider.bounds.extents.y - offset, 0), Vector2.down, rayDistance, layer))
        {
            
            resetPosition = true;
        }
        // if it collide with the player it goes up
        if (Physics2D.Raycast(collider.bounds.center - new Vector3(0, collider.bounds.extents.y - offset, 0), Vector2.down, rayDistance, player))
            resetPosition = true;
    }

    

}
