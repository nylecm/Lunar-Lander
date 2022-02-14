using System;
using JetBrains.Annotations;
using UnityEngine;

/**
 * I am implementing a Lunar Lander Game.
 *
 * What I have done so far:
 *
 * - Basic movement of rocket:
 *   - Rotation & Thrust
 * - Collision Detection, and landing hardness threshold.
 */

public class Player : MonoBehaviour
{
    [SerializeField] private int maxRot;

    private Rigidbody2D rb2;
    private Vector2 velocity;
    private Vector2 prevPos;
    private Vector2 curPos;
    private float curAngle = 0.0f;
    private int fuelSupply = 10000;
    private const float rotIncrement = 0.16f;
    private const float thrustVelocityIncrement = 0.009f;


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
        velocity.x += 1.2f;
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
        //prevVelocityY = r
    }

    private void AddThrust()
    {
        if (fuelSupply == 0)
        {
            return;
        }
        
        // todo get rid of framerate dependence.
        bool posAngle = (curAngle > 0);
        velocity = rb2.velocity;

        if (posAngle)
        {
            velocity.y += (float) Math.Cos(ConvertToRadians(curAngle)) * thrustVelocityIncrement;
            velocity.x += (float) -(Math.Sin(ConvertToRadians(curAngle)) * thrustVelocityIncrement);
        }
        else
        {
            velocity.y += (float) (Math.Cos(ConvertToRadians(-curAngle)) * thrustVelocityIncrement);
            velocity.x += (float) (Math.Sin(ConvertToRadians(-curAngle)) * thrustVelocityIncrement);
        }

        //velocity.y += 0.01f;
        rb2.velocity = velocity;
        fuelSupply -= 1;
        Debug.Log(fuelSupply);
        //rb2.velocity.x = velocityX;
    }

    public double ConvertToRadians(double angle)
    {
        return (Math.PI / 180) * angle;
    }
    
    private void RotateCW()
    {
        if (curAngle < -90)
        {
            return;
        }

        rb2.transform.Rotate(0, 0, -rotIncrement, Space.Self);
        curAngle -= rotIncrement;
        Debug.Log(curAngle);
    }

    private void RotateACW()
    {
        if (curAngle > 90)
        {
            return;
        }

        rb2.transform.Rotate(0, 0, rotIncrement, Space.Self);
        curAngle += rotIncrement;
        Debug.Log(curAngle);
    }

    [UsedImplicitly]
    public void Landed()
    {
        if (rb2.velocity.y < -0.5f)
        {
            Debug.Log(rb2.velocity.y + "Bang!!!");
            //SceneManager.LoadScene("SampleScene"); todo uncomment when done
        }
        else if (rb2.velocity.y < -0.25f)
        {
            
            Debug.Log(rb2.velocity.y + "Hard Landing!");
            //SceneManager.LoadScene("SampleScene"); todo uncomment when done

        }
        else
        {
            Debug.Log(rb2.velocity.y + "BUTTER :)");
            //SceneManager.LoadScene("SampleScene"); todo uncomment when done
        }
    }
}