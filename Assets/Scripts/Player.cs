using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore;

public class Player : MonoBehaviour
{
    [SerializeField] private int maxRot;

    private Rigidbody2D rb2;
    private Vector2 velocity;
    private Vector2 prevPos;
    private Vector2 curPos;
    private float curAngle = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Awake()
    {
        rb2 = GetComponent<Rigidbody2D>();
        rb2.gravityScale = 0.3f;
        curPos = rb2.position;
        prevPos = curPos;
        velocity = new Vector2();
        velocity.x += 1.5f;
        rb2.velocity = velocity;
    }

    // Update is called once per frame
    void Update()
    {
        curPos = rb2.position;
        if (Input.GetKey(KeyCode.W)) AddThrust();
        if (Input.GetKey(KeyCode.A)) RotateCW();
        if (Input.GetKey(KeyCode.D)) RotateACW();

        prevPos = curPos;
    }

    private void AddThrust()
    {
        // todo have direction of lander determine :

        // Calculate heading of lander:

        float speed = 0.5f;
        bool posAngle = (curAngle > 0);
        velocity = rb2.velocity;

        if (posAngle)
        {
            velocity.y = (float) Math.Cos(ConvertToRadians(curAngle)) * speed;
            velocity.x = (float) -(Math.Sin(ConvertToRadians(curAngle)) * speed);

        }
        else
        {
            velocity.y = (float) (Math.Cos(ConvertToRadians(-curAngle)) * speed);
            velocity.x = (float) (Math.Sin(ConvertToRadians(-curAngle)) * speed);

        }

        //velocity.y += 0.01f;
        rb2.velocity = velocity;
        //rb2.velocity.x = velocityX;
    }
    
    public double ConvertToRadians(double angle)
    {
        return (Math.PI / 180) * angle;
    }

    // private void RotateCW()
    // {
    //     velocity = rb2.velocity;
    //     velocity.y -= 2;
    //     rb2.velocity = velocity;
    // }

    private void RotateCW()
    {
        // Debug.Log(rb2.transform.rotation.z);
        // if (rb2.transform.rotation.z > -0.5) // todo fix
        // {
        rb2.transform.Rotate(0, 0, -0.12f, Space.Self);
        curAngle -= 0.12f;
        Debug.Log(curAngle);
        //}
        //velocity.x += 10;
        //rb2.velocity.x = velocityX;
    }

    private void RotateACW()
    {
        //if (rb2.transform.rotation.z < 0.5) // todo fix
        //{
        rb2.transform.Rotate(0, 0, 0.12f, Space.Self);
        curAngle += 0.12f;
        Debug.Log(curAngle);

        //}
        //velocity.x += 10;
        //rb2.velocity.x = velocityX;
    }
}