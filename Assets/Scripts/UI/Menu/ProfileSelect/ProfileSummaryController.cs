using TMPro;
using UnityEngine;

public class ProfileSummaryController : MonoBehaviour
{
    [SerializeField] private int profileID;

    private void OnEnable()
    {
        if (!ProfileModel.DoesProfileExist(profileID)) return;

        var profile = ProfileModel.Load(profileID);
        Debug.Log(profile.HighScore);
        var components = GetComponentsInChildren<TextMeshProUGUI>();
        components[2].text = "High Score: " + profile.HighScore;
        components[3].text = "No. Of Landings: " + profile.NumberOfLandings;
        components[4].text = "Achievements: " + profile.NumberOfAchievementsUnlocked().ToString() + "/13";
    }
}