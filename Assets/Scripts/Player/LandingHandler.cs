using UnityEngine;

public class LandingHandler : MonoBehaviour
{
    private AchievementManager _achievementManager;
    private int _softLandingsInARow = 0;
    private int _landingsInARow = 0;

    private void OnEnable()
    {
        Player.OnHardLanding += HandleHardLanding;
        Player.OnSoftLanding += HandleSoftLanding;
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
        _achievementManager.NotifyAchievementProgress(AchievementType.TenthLanding);
        _achievementManager.NotifyAchievementProgress(AchievementType.FiftiethLanding);
        _achievementManager.NotifyAchievementProgress(AchievementType.HundredthLanding);
    }

    private void HandleSoftLanding()
    {
        Debug.Log("Notify Soft Landing");
        _landingsInARow++;
        _softLandingsInARow++;
        if (_landingsInARow == 5) _achievementManager.NotifyAchievementProgress(AchievementType.FiveLandingsInOneGame);
        if (_softLandingsInARow == 3) _achievementManager.NotifyAchievementProgress(AchievementType.ThreeButtersInARow);
        _achievementManager.NotifyAchievementProgress(AchievementType.Butter);
        _achievementManager.NotifyAchievementProgress(AchievementType.TenthLanding);
        _achievementManager.NotifyAchievementProgress(AchievementType.FiftiethLanding);
        _achievementManager.NotifyAchievementProgress(AchievementType.HundredthLanding);
    }
}