using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    [SerializeField] float bulletSpeed = 10f;
    PlayerMovement player;
    float xSpeed;


    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
        
    }

    
    void Update()
    {        
        myRigidBody.velocity = new Vector2(xSpeed, 0f);
        //Debug.Log($"Show sighn {player.transform.localScale.x}");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
