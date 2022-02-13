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
        velocity = new Vector2();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) MvUp(); 
        if (Input.GetKeyDown(KeyCode.S)) MvDn();
        if (Input.GetKeyDown(KeyCode.A)) MvLeft(); 
        if (Input.GetKeyDown(KeyCode.D)) MvRight();
    }

    private void MvUp()
    {
        velocity = rb2.velocity;
        velocity.y += 2;
        rb2.velocity = velocity;
        //rb2.velocity.x = velocityX;
    }
    
    private void MvDn()
    {
        velocity = rb2.velocity;
        velocity.y -= 2;
        rb2.velocity = velocity;
        //rb2.velocity.x = velocityX;
    }
    
    private void MvLeft()
    {
        velocity.x += 10;
        //rb2.velocity.x = velocityX;
    }
    
    private void MvRight()
    {
        velocity.x += 10;
        //rb2.velocity.x = velocityX;
    }
}
