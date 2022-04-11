using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * I am implementing a Lunar Lander Game.
 *
 * What I have done so far:
 *
 * - Basic movement of rocket:
 *   - Rotation & Thrust
 *   - Collision Detection, and landing hardness threshold.
 */
public class Player : MonoBehaviour
{
    [SerializeField] private int maxRot;

    private Rigidbody2D _rb2;
    private Vector2 _velocity;
    private Vector2 _prevPos;
    private Vector2 _curPos;
    private float _curAngle = 0.0f;
    private int _fuelSupply = 10000;
    private const float RotIncrement = 0.16f;
    private const float ThrustVelocityIncrement = 0.015f;

    public static event Action<int> OnFuelChange;
    public static event Action<float> OnVSpeedChange; 
    public static event Action<float> OnHSpeedChange; 

    private void Awake()
    {
        _rb2 = GetComponent<Rigidbody2D>();
        _rb2.gravityScale = 0.3f;
        _curPos = _rb2.position;
        _prevPos = _curPos;
        _velocity = new Vector2();
        _velocity.x = 1.2f;
        _rb2.velocity = _velocity;
    }

    // Update is called once per frame

    private void Update()
    {
        _curPos = _rb2.position;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) AddThrust();
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) RotateCW();
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) RotateACW();

        _prevPos = _curPos;
        OnVSpeedChange?.Invoke(_rb2.velocity.y);
        OnHSpeedChange?.Invoke(_rb2.velocity.x);
        //prevVelocityY = r
    }

    /**
     * Adds thrust in the direction the rocket is pointing.
     */
    private void AddThrust()
    {
        if (_fuelSupply == 0)
        {
            Debug.Log("No fuel: Unable to Add Thrust!");
            return;
        }

        // todo get rid of framerate dependence.
        bool posAngle = (_curAngle > 0);
        _velocity = _rb2.velocity;

        if (posAngle)
        {
            _velocity.y += (float) Math.Cos(ConvertToRadians(_curAngle)) * ThrustVelocityIncrement;
            _velocity.x += (float) -(Math.Sin(ConvertToRadians(_curAngle)) * ThrustVelocityIncrement);
        }
        else
        {
            _velocity.y += (float) (Math.Cos(ConvertToRadians(-_curAngle)) * ThrustVelocityIncrement);
            _velocity.x += (float) (Math.Sin(ConvertToRadians(-_curAngle)) * ThrustVelocityIncrement);
        }

        //velocity.y += 0.01f;
        _rb2.velocity = _velocity;
        _fuelSupply -= 1;
        OnFuelChange?.Invoke(_fuelSupply);
        Debug.Log(_fuelSupply);
        //rb2.velocity.x = velocityX;
    }

    public double ConvertToRadians(double angle)
    {
        return (Math.PI / 180) * angle;
    }

    private void RotateCW()
    {
        if (_curAngle < -90)
        {
            return;
        }

        _rb2.transform.Rotate(0, 0, -RotIncrement, Space.Self);
        _curAngle -= RotIncrement;
        Debug.Log(_curAngle);
    }

    private void RotateACW()
    {
        if (_curAngle > 90)
        {
            return;
        }

        _rb2.transform.Rotate(0, 0, RotIncrement, Space.Self);
        _curAngle += RotIncrement;
        Debug.Log(_curAngle);
    }

    [UsedImplicitly]
    public void Landed()
    {
        if (_rb2.velocity.y < -1.4f)
        {
            Debug.Log(_rb2.velocity.y + "Bang!!!");
            SceneManager.LoadScene("SampleScene");
        }
        else if (_rb2.velocity.y < -0.7f)
        {
            Debug.Log(_rb2.velocity.y + "Hard Landing!");
            SceneManager.LoadScene("SampleScene");
        }
        else
        {
            Debug.Log(_rb2.velocity.y + "BUTTER :)");
            SceneManager.LoadScene("SampleScene");
        }
    }
}