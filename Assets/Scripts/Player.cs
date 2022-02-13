using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        float angle = curAngle;

        velocity = rb2.velocity;
        velocity.y = (float) Math.Cos(angle) * 0.30f;
        velocity.x = (float) Math.Sin(angle) * 0.30f;
        //velocity.y += 0.01f;
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