using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProfileSelectMenuController : MonoBehaviour
{
    public void PlayGame(int profile)
    {
        Debug.Log("Opening SampleScene, with profile: " + profile);
        if (ProfileModel.DoesProfileExist(profile))
        {
            ProfileManager.CurProfile = ProfileModel.Load(profile);
        }
        else
        {
            ProfileManager.CurProfile = new ProfileModel(profile, 0, 0, new List<AchievementModel>());
            ProfileManager.CurProfile.Save(); 
        }

        Debug.Log(ProfileManager.CurProfile.HighScore);
        SceneManager.LoadScene("ShipSelectMenu");
    }

    private void OnEnable()
    {
        //throw new NotImplementedException();
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
