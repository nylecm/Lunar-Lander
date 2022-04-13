using System;
using UnityEngine;

public class OutOfFuelEvent : MonoBehaviour
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
        if (fuel == 0) PerformOutOfFuelActions();
    }

    private void PerformOutOfFuelActions()
    {
        Debug.Log("No fuel: Unable to Add Thrust!");
        Debug.Log("Notify Achievement Out of Fuel ID:");
        _achievementManager.NotifyAchievementComplete("0"); // todo unhardcode the ids maybe use an enum.

        // todo Disable Thrust here
    }
}