using UnityEngine;

public class OutOfFuelEventHandler : MonoBehaviour
{
    private AchievementManager _achievementManager;
    
    private void OnEnable()
    {
        Player.OnFuelChange += CheckRocketFuel;
    }

    private void OnDisable()
    {
        Player.OnFuelChange -= CheckRocketFuel;
    }

    private void Awake()
    {
        _achievementManager = FindObjectOfType<AchievementManager>();
    }

    private void CheckRocketFuel(float fuel)
    {
        if (fuel <= 0) PerformOutOfFuelActions();
    }

    private void PerformOutOfFuelActions()
    {
        Debug.Log("No fuel: Unable to Add Thrust!");
        Debug.Log("Notify Achievement Out of Fuel ID:");
        _achievementManager.NotifyAchievementProgress(AchievementType.OutOfFuel);
    }
}