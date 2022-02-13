using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb2;
    private Vector2 velocity;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        rb2 = GetComponent<Rigidbody2D>();
        rb2.gravityScale = 0.3f;
        velocity = new Vector2();
        velocity.x += 1.5f;
        rb2.velocity = velocity;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) AddThrust(); 
        if (Input.GetKeyDown(KeyCode.A)) RotateCW(); 
        if (Input.GetKeyDown(KeyCode.D)) RotateACW();
        rb2.transform.Rotate(0, 0, 0.1f, Space.Self);

    }

    private void AddThrust()
    {
        // In the direction currently pointed at add thrust:
        velocity = rb2.velocity;
        velocity.y += 1;
        rb2.velocity = velocity;
        //rb2.velocity.x = velocityX;
    }
    
    // private void RotateCW()
    // {
    //     velocity = rb2.velocity;
    //     velocity.y -= 2;
    //     rb2.velocity = velocity;
    // }
    
    private void RotateCW()
    {
        
        //velocity.x += 10;
        //rb2.velocity.x = velocityX;
    }
    
    private void RotateACW()
    {
        //velocity.x += 10;
        //rb2.velocity.x = velocityX;
    }
}
