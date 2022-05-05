using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProfileSummaryController : MonoBehaviour
{
    [SerializeField] private int profileID;
    private ProfileModel _profileModel;
    
    private void OnEnable()
    {
        LoadProfile();
        Debug.Log(_profileModel.HighScore);
        var components = GetComponentsInChildren<TextMeshProUGUI>();
        components[2].text = "High Score: " + _profileModel.HighScore;
        components[3].text = "No. Of Landings: " + _profileModel.NumberOfLandings;
        components[4].text = "Achievements: " + _profileModel.NumberOfAchievementsUnlocked() + "/12";
    }

    private void LoadProfile()
    {
        if (ProfileModel.DoesProfileExist(profileID))
        {
            _profileModel = ProfileModel.Load(profileID);
        }
    }
    
    public void PlayGame()
    {
        if (_profileModel == null)
        {
            _profileModel = new ProfileModel(profileID, 0, 0, new List<AchievementModel>());
            _profileModel.Save();

        }
        ProfileManager.CurProfile = _profileModel;
        SceneManager.LoadScene("ShipSelectMenu");
    }

    public void LoadAchievementMenu()
    {
        if (_profileModel == null) return;
        ProfileManager.CurProfile = _profileModel;
        SceneManager.LoadScene("AchievementViewer");
    }
}