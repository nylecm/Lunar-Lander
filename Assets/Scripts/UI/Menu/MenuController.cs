using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void PlayGame()
    {
        ProfileManager.CurProfile = new ProfileModel(0, "nylecm", 100, 10, new[] {-200.3f, -300, 5f, -334, 445f},
            new[] {100, 50, 25}, new List<AchievementModel>());
        ProfileManager.CurProfile.Save();
        SceneManager.LoadScene("ProfileSelectMenu");
    }

    public void Exit()
    {
        Application.Quit(0);
    }
}