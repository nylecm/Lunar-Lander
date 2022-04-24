using System;
using UnityEngine;

public class LandingAchievementHandler : MonoBehaviour
{
    private AchievementManager _achievementManager;
    private int _softLandingsInARow;
    private int _landingsInARow;
    
    private void OnEnable()
    {
        Player.OnHardLanding += HandleHardLanding;
        Player.OnSoftLanding += HandleSoftLanding;
        Player.OnTouchDown += HandleTouchDown;
    }

    private void OnDisable()
    {
        Player.OnHardLanding -= HandleHardLanding;
        Player.OnSoftLanding -= HandleSoftLanding;
    }

    private void Awake()
    {
        _achievementManager = FindObjectOfType<AchievementManager>();
    }

    private void HandleHardLanding()
    {
        Debug.Log("Notify Hard Landing");
        _landingsInARow++;
        if (_landingsInARow == 5) _achievementManager.NotifyAchievementProgress(AchievementType.FiveLandingsInOneGame);
        HandleLandingAchievements();
    }

    private void HandleSoftLanding()
    {
        Debug.Log("Notify Soft Landing");
        _landingsInARow++;
        _softLandingsInARow++;
        if (_landingsInARow == 5) _achievementManager.NotifyAchievementProgress(AchievementType.FiveLandingsInOneGame);
        if (_softLandingsInARow == 3) _achievementManager.NotifyAchievementProgress(AchievementType.ThreeButtersInARow);
        _achievementManager.NotifyAchievementProgress(AchievementType.Butter);
        HandleLandingAchievements();
    }

    private void HandleLandingAchievements()
    {
        _achievementManager.NotifyAchievementProgress(AchievementType.FirstLanding);
        _achievementManager.NotifyAchievementProgress(AchievementType.TenthLanding);
        _achievementManager.NotifyAchievementProgress(AchievementType.FiftiethLanding);
        _achievementManager.NotifyAchievementProgress(AchievementType.HundredthLanding);
    }

    private void HandleTouchDown(CentreMessage centreMessage)
    {
        if (centreMessage.Points >= 200) _achievementManager.NotifyAchievementProgress(AchievementType.HighScoreOver200);
        if (centreMessage.Points >= 500) _achievementManager.NotifyAchievementProgress(AchievementType.HighScoreOver500);
    }
}