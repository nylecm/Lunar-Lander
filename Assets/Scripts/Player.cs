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
    [SerializeField] private int fuelSupply;

    private Rigidbody2D _rb2;
    private Vector2 _velocity;
    private Vector2 _prevPos;
    private Vector2 _curPos;
    private float _curAngle = 0.0f;
    private const float RotIncrement = 0.16f;
    //private float _thrustVelocityIncrement = 0.020f;
    private const float _thrustVelocityIncrement = 8.000f;
    private bool _isFuelDepleted = false;

    private AchievementManager _achievementManager;
    private const int OutOfFuelAchievementID = 0;
    private const int HardLandingAchievementID = 1;
    private const int SoftLandingAchievementID = 2;
    private const int ButterLandingAchievementID = 3;
    
    public static event Action<int> OnFuelChange;
    public static event Action<float> OnVSpeedChange;
    public static event Action<float> OnHSpeedChange;

    private void Start()
    {
        _achievementManager = FindObjectOfType<AchievementManager>();
    }

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
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) RotateACW();
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) RotateCW();

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
        if (fuelSupply <= 0)
        {
            return;
        }

        // todo get rid of framerate dependence.
        bool posAngle = (_curAngle > 0);
        _velocity = _rb2.velocity;

        Debug.Log("TVRDT = " + Time.deltaTime);

        float adjustedThrustVelocityIncrement = _thrustVelocityIncrement * Time.deltaTime;
        Debug.Log("TVR = " + adjustedThrustVelocityIncrement);
        if (posAngle)
        {
            _velocity.y += (float) Math.Cos(ConvertToRadians(_curAngle)) * adjustedThrustVelocityIncrement;
            _velocity.x += (float) -(Math.Sin(ConvertToRadians(_curAngle)) * adjustedThrustVelocityIncrement);
        }
        else
        {
            _velocity.y += (float) (Math.Cos(ConvertToRadians(-_curAngle)) * adjustedThrustVelocityIncrement);
            _velocity.x += (float) (Math.Sin(ConvertToRadians(-_curAngle)) * adjustedThrustVelocityIncrement);
        }

        //velocity.y += 0.01f;
        _rb2.velocity = _velocity;
        fuelSupply -= 1;
        OnFuelChange?.Invoke(fuelSupply);

        if (fuelSupply % 100 == 0)
        {
            Debug.Log("Fuel Supply: " + fuelSupply);
        }
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
        //Debug.Log(_curAngle);
    }

    private void RotateACW()
    {
        if (_curAngle > 90)
        {
            return;
        }

        _rb2.transform.Rotate(0, 0, RotIncrement, Space.Self);
        _curAngle += RotIncrement;
        //Debug.Log(_curAngle);
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