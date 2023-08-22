using System;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;
using UnityEngine.UIElements;

public class Dropper : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private int ground = 3; // number of the layer mask
    [SerializeField] private float speed = 20;
    private bool resetPosition = false;

    private float initialPosition;
 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position.y;
    }
    private void Update()
    {
        if (transform.position.y == initialPosition) rb.velocity = Vector3.zero;

        if(resetPosition && transform.position.y < initialPosition)
        {
            rb.velocity = Vector3.up * speed * Time.deltaTime;
        }
        else
        {
            resetPosition = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == ground)
        {
            
            resetPosition = true;
        }
    }

    

}
