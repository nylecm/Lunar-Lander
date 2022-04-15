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
    [SerializeField] private float fuelSupply;

    private Rigidbody2D _rb2;
    private Transform _transform;

    private Vector2 _velocity;

    //private Vector2 _prevPos;
    //private Vector2 _curPos;
    private float _curAngle;
    private const float RotIncrement = 64f;
    private const float ThrustVelocityIncrement = 6f; // Thrust Added Per Second
    private const int FuelConsumptionIncrement = 15;
    private int _points;

    private AchievementManager _achievementManager;
    // private const int OutOfFuelAchievementID = 0;
    // private const int HardLandingAchievementID = 1;
    // private const int SoftLandingAchievementID = 2;
    // private const int ButterLandingAchievementID = 3;

    public static event Action<float> OnFuelChange;
    public static event Action<float> OnVSpeedChange;
    public static event Action<float> OnHSpeedChange;
    public static event Action<LandedCentreMessage> OnLanded;

    private void Start()
    {
        _achievementManager = FindObjectOfType<AchievementManager>();
    }

    private void Awake()
    {
        _rb2 = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
        EnterStartingPosition();
        OnFuelChange?.Invoke(300); // TODO GET STARTING FUEL HERE PROGRAMMATICALLY.
        OnHSpeedChange?.Invoke(_velocity.x);
        OnVSpeedChange?.Invoke(_velocity.y);
    }

    private void EnterStartingPosition()
    {
        Debug.Log("You have: " + _points + " points.");
        _transform.position = new Vector3(-15, 8.5f, 0);
        _velocity = new Vector2();
        _velocity.x = 1.2f;
        _rb2.velocity = _velocity;
    }

    // Update is called once per frame

    private void Update()
    {
        float deltaTime = Time.deltaTime;
        float adjustedRotationIncrement = RotIncrement * deltaTime;
        float adjustedThrustVelocityIncrement = ThrustVelocityIncrement * Time.deltaTime;
        float adjustedFuelConsumptionIncrement = FuelConsumptionIncrement * deltaTime;
        //_curPos = _rb2.position;


        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            AddThrust(adjustedThrustVelocityIncrement, adjustedFuelConsumptionIncrement);
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) RotateACW(adjustedRotationIncrement);
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) RotateCW(adjustedRotationIncrement);

        //_prevPos = _curPos;
        OnVSpeedChange?.Invoke(_rb2.velocity.y);
        OnHSpeedChange?.Invoke(_rb2.velocity.x);
        //prevVelocityY = r
    }

    /**
     * Adds thrust in the direction the rocket is pointing.
     */
    private void AddThrust(float adjustedThrustVelocityIncrement, float adjustedFuelConsumptionIncrement)
    {
        if (fuelSupply <= 0) return;

        bool posAngle = (_curAngle > 0);
        _velocity = _rb2.velocity;

        // Debug.Log("TVRDT = " + Time.deltaTime);
        // Debug.Log("TVR = " + adjustedThrustVelocityIncrement);
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
        fuelSupply -= adjustedFuelConsumptionIncrement;
        OnFuelChange?.Invoke(fuelSupply);

        if (fuelSupply % 100 == 0)
        {
            Debug.Log("Fuel Supply: " + fuelSupply);
        }
        //rb2.velocity.x = velocityX;
    }

    private double ConvertToRadians(double angle)
    {
        return (Math.PI / 180) * angle;
    }

    private void RotateCW(float adjustedRotationIncrement)
    {
        if (_curAngle < -maxRot) return;

        _rb2.transform.Rotate(0, 0, -adjustedRotationIncrement, Space.Self);
        _curAngle -= adjustedRotationIncrement;
        // Debug.Log(_curAngle);
    }

    private void RotateACW(float adjustedRotationIncrement)
    {
        if (_curAngle > maxRot) return;

        _rb2.transform.Rotate(0, 0, adjustedRotationIncrement, Space.Self);
        _curAngle += adjustedRotationIncrement;
        // Debug.Log(_curAngle);
    }

    [UsedImplicitly]
    public void Landed() // todo check if this needs to be public
    {
        // Classifying the hardness of the landing, note negative velocity.
        if (_rb2.velocity.y < -1.5f) // FAILURE: vertical speed greater than approx. 300 ft/m
        {
            Debug.Log(_rb2.velocity.y + "Bang!!!");
            HandleGameFailure();
        }
        else if (_rb2.velocity.y < -0.7f) // HARD LANDING: vertical speed approx. between 150 & 300 ft/m
        {
            Debug.Log(_rb2.velocity.y + "Hard Landing!");
            if (fuelSupply > 0)
            {
                _points += 100;
                OnLanded?.Invoke(new LandedCentreMessage("Hard Landing", 25));
                EnterStartingPosition();
            }
            else // Close to impossible (gotta cover all cases).
            {
                _points += 25;
                HandleGameFailure();
            }
        }
        else // SOFT LANDING: vertical speed approx. less then 150 ft/m
        {
            Debug.Log(_rb2.velocity.y + "BUTTER :)");
            if (fuelSupply > 0)
            {
                _points += 100;
                OnLanded?.Invoke(new LandedCentreMessage("BUTTER :)", 100));
                EnterStartingPosition();
            }
            else // Close to impossible (gotta cover all cases).
            {
                _points += 100;
                HandleGameFailure();
            }
        }
    }

    private void HandleGameFailure()
    {
        Debug.Log("You have failed the game with: " + _points + " points.");
        SceneManager.LoadScene("SampleScene");
    }
}