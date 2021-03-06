using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions;
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
    private AudioSource _audioSrc;

    private ParticleSystem _particleSystem;

    private Vector2 _velocity;
    private float _curAngle;
    private const int FuelConsumptionIncrement = 15;
    private int _points;

    private bool _hasLandingBeenDetected;

    //private AchievementManager _achievementManager;

    private LanderModel _lander;
    private float _rotIncrement = 64f;
    private float _thrustVelocityIncrement = 6f; // Thrust Added Per Second
    private float _hardLandingVSpeedThresh = -1.5f;
    private float _softLandingVSpeedThresh = -0.7f;

    private const int HardLandingPoints = 25;
    private const int SoftLandingPoints = 100;

    public static event Action<float> OnFuelChange;
    public static event Action<float> OnVSpeedChange;
    public static event Action<float> OnHSpeedChange;
    public static event Action OnHardLanding;
    public static event Action OnSoftLanding;
    public static event Action<CentreMessage> OnTouchDown;
    
    private void Start()
    {
        //_achievementManager = FindObjectOfType<AchievementManager>();
    }

    private void Awake()
    {
        Debug.Log(ProfileManager.CurProfile.ID);
        Debug.Log(ProfileManager.CurProfile.HighScore);
        _lander = LanderManager.GetCurLander();
        if (_lander == null)
        {
            LanderManager.SetCurLander(LanderManager.MakeLander("UFO"));
            _lander = LanderManager.GetCurLander();
            Debug.Assert(_lander != null);
        }

        _audioSrc = GetComponent<AudioSource>();
        _audioSrc.Pause();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        _particleSystem.Stop();
        _rb2 = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();

        OnFuelChange?.Invoke(LanderManager.GetCurLander().fuelTankMultiplier * fuelSupply);
        OnHSpeedChange?.Invoke(_velocity.x);
        OnVSpeedChange?.Invoke(_velocity.y);

        if (_lander != null) SetConstantsForLoadedLander();

        EnterStartingPosition();
    }

    private void SetConstantsForLoadedLander()
    {
        _thrustVelocityIncrement *= _lander.thrustMultiplier;
        _rotIncrement *= _lander.rotSpeedMultiplier;
        _hardLandingVSpeedThresh *= _lander.strengthMultiplier;
        _softLandingVSpeedThresh *= _lander.strengthMultiplier;
        fuelSupply *= _lander.fuelTankMultiplier;
        OnFuelChange?.Invoke(fuelSupply);
        GetComponent<SpriteRenderer>().sprite = _lander.rocketImage;
        // todo spirte...
        // todo sound...
    }

    private void EnterStartingPosition()
    {
        _hasLandingBeenDetected = false;
        Debug.Log("You have: " + _points + " points.");
        _transform.position = new Vector3(-20, 25, 0);
        _velocity = new Vector2();
        _velocity.x = 1.2f;
        _rb2.velocity = _velocity;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
        
        float deltaTime = Time.deltaTime;
        float adjustedRotationIncrement = _rotIncrement * deltaTime;
        float adjustedThrustVelocityIncrement = _thrustVelocityIncrement * Time.deltaTime;
        float adjustedFuelConsumptionIncrement = FuelConsumptionIncrement * deltaTime;
        //_curPos = _rb2.position;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            AddThrust(adjustedThrustVelocityIncrement, adjustedFuelConsumptionIncrement);
        }
        else
        {
            StopThrustEffects();
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) RotateACW(adjustedRotationIncrement);
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) RotateCW(adjustedRotationIncrement);

        OnVSpeedChange?.Invoke(_rb2.velocity.y);
        OnHSpeedChange?.Invoke(_rb2.velocity.x);
        //OnAltitudeChange
    }

    /**
     * Adds thrust in the direction the rocket is pointing.
     */
    private void AddThrust(float adjustedThrustVelocityIncrement, float adjustedFuelConsumptionIncrement)
    {
        if (fuelSupply <= 0)
        {
            StopThrustEffects();
            //OnFuelChange?.Invoke(fuelSupply);
            return;
        }

        if (_particleSystem.isStopped)
        {
            _particleSystem.Play();
        }

        if (!_audioSrc.isPlaying)
        {
            _audioSrc.UnPause();
        }

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

        _rb2.velocity = _velocity;
        fuelSupply -= adjustedFuelConsumptionIncrement;
        OnFuelChange?.Invoke(fuelSupply);
    }

    private void StopThrustEffects()
    {
        if (_particleSystem.isPlaying) _particleSystem.Stop();
        if (_audioSrc.isPlaying) _audioSrc.Pause();
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
        if (_hasLandingBeenDetected) return;
        _hasLandingBeenDetected = true;

        // Classifying the hardness and angle of the landing, note negative velocity.
        if (_curAngle > 10 || _curAngle < -10) // todo extract const
        {
            Debug.Log("Landing Angle to Extreme: " + _curAngle);
            HandleGameFailure();
        }
        else if (_rb2.velocity.y < _hardLandingVSpeedThresh) // FAILURE: vertical speed greater than approx. 300 ft/m
        {
            Debug.Log(_rb2.velocity.y + "Bang!!!");
            HandleGameFailure();
        }
        else if
            (_rb2.velocity.y < _softLandingVSpeedThresh) // HARD LANDING: vertical speed approx. between 150 & 300 ft/m
        {
            Debug.Log(_rb2.velocity.y + "Hard Landing!");
            if (fuelSupply > 0)
            {
                _points += HardLandingPoints;
                OnHardLanding?.Invoke();
                OnTouchDown?.Invoke(new CentreMessage("Hard Landing", HardLandingPoints));
                ProfileManager.CurProfile.NumberOfLandings += 1;
                EnterStartingPosition();
            }
            else // Close to impossible (gotta cover all cases).
            {
                _points += HardLandingPoints;
                OnHardLanding?.Invoke();
                OnTouchDown?.Invoke(new CentreMessage("Hard Landing", HardLandingPoints));
                ProfileManager.CurProfile.NumberOfLandings += 1;
                HandleGameFailure();
            }
        }
        else // SOFT LANDING: vertical speed approx. less then 150 ft/m
        {
            Debug.Log(_rb2.velocity.y + "BUTTER :)");
            if (fuelSupply > 0)
            {
                _points += SoftLandingPoints;
                OnSoftLanding?.Invoke();
                OnTouchDown?.Invoke(new CentreMessage("BUTTER :)", SoftLandingPoints));
                ProfileManager.CurProfile.NumberOfLandings += 1;
                EnterStartingPosition();
            }
            else // Close to impossible (gotta cover all cases).
            {
                _points += SoftLandingPoints;
                OnSoftLanding?.Invoke();
                OnTouchDown?.Invoke(new CentreMessage("BUTTER :)", SoftLandingPoints));
                ProfileManager.CurProfile.NumberOfLandings += 1;
                HandleGameFailure();
            }
        }
    }

    public void LandedNonBottom()
    {
        if (_hasLandingBeenDetected) return;
        _hasLandingBeenDetected = true;
        HandleGameFailure();
    }

    private void HandleGameFailure()
    {
        if (ProfileManager.CurProfile.HighScore < _points)
        {
            ProfileManager.CurProfile.HighScore = _points;
        }

        ProfileManager.CurProfile.Save();
        OnTouchDown?.Invoke(new CentreMessage("Game Over!", _points, "SampleScene"));
        Debug.Log("You have failed the game with: " + _points + " points.");
    }

    public void HandleFuelPowerUp()
    {
        fuelSupply += 100;
    }

    public void HandleStopPowerUp()
    {
        _rb2.velocity = new Vector2(0, 0);
    }
}