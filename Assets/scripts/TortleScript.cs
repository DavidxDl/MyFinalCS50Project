using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TortleScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed = 4;
    [SerializeField] private float leftBound = 13f;
    [SerializeField] private float rightBound = 24f;
    [SerializeField] private SpriteRenderer sprite;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        if(transform.position.x < leftBound)
        {
            transform.position = new Vector2(leftBound + 0.1f, transform.position.y);
            speed *= -1;
            sprite.flipX = false;
        }
        if (transform.position.x > rightBound)
        {
            transform.position = new Vector2(rightBound - 0.1f, transform.position.y);
            speed *= -1;
            sprite.flipX = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        speed *= -1;
        if(sprite.flipX)
        {
            sprite.flipX = false;
        }
        else
        {
            sprite.flipX = true;
        }
    }
}
