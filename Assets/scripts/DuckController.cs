using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DuckController : MonoBehaviour
{


    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpStrenght = 30f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private AudioClip killSound;
    [SerializeField] private AudioClip coinSound;

    private BoxCollider2D boxCollider;
    private AudioSource audioSource;
    private OscillatingPlatform currentPlataform;


    private Rigidbody2D rb;
    private Vector2 moveDir = Vector2.zero;
    private int jumps = 0;
    private float bottomBound = -6.30f;
    private bool invensible = false;
    private float invensibilityTime = 1f;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }


    private void Update()
    {
        isGrounded();
        if (Input.GetKeyDown(KeyCode.Space) && jumps < 1 )
        {

            jumps++;
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpStrenght, ForceMode2D.Impulse);
            audioSource.Play();
        }
        if (transform.position.y < bottomBound)
        {
            GameManager.instance.GameOver();
        }
        if(currentPlataform != null)
        {
            Vector2 plataformMovement = currentPlataform.GetPlatformMovement();
            transform.position += (Vector3)plataformMovement;
        }

    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        HandleMovement();
    }





    private void HandleMovement()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        moveDir.x = horizontalMovement;

        transform.Translate(Vector2.right * speed * moveDir.x * Time.deltaTime);
        if (moveDir.x < 0)
        {
            sprite.flipX = true;
        }
        else if (moveDir.x > 0)
        {
            sprite.flipX = false;
        }
    }


    private void isGrounded()
    {


        float offset = .6f;

        RaycastHit2D rayCastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size + new Vector3(.20f, 0, 0), 0f, Vector2.down, offset, layerMask);
        if (rayCastHit.collider != null)
        {
            jumps = 0;

        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<TortleScript>() != null)
        {
            if (!invensible)
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * (jumpStrenght * 1.3f), ForceMode2D.Impulse);
            }
            Destroy(collision.gameObject);
            audioSource.PlayOneShot(killSound);
        }
        else if (collision.gameObject.layer == 7) // 7 is The coin Layer
        {
            audioSource.PlayOneShot(coinSound);
            GameManager.instance.AddCoins();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.layer == 8)
        {
            GameManager.instance.Win(this);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.GetComponent<TortleScript>() != null && !invensible)
        {
            GetHit();

            //GameManager.instance.GameOver();
        }
        if (collision.gameObject.CompareTag("Plataform"))
        {
            currentPlataform = collision.gameObject.GetComponent<OscillatingPlatform>();
        }
        if(collision.gameObject.CompareTag("Spikes"))
        {
            if (!invensible)
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * (jumpStrenght * 1.1f), ForceMode2D.Impulse);
            }
            GetHit();
        }
        if (collision.gameObject.CompareTag("Saw"))
            GetHit();

    }

    private void GetHit()
    {
        GameManager.instance.RemoveLife();
        invensible = true;
        StartCoroutine(Invensible());
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Plataform"))
        {
            currentPlataform = null;
        }
    }

    IEnumerator Invensible()
    {
        yield return new WaitForSeconds(invensibilityTime);
        invensible = false;
    }
}


