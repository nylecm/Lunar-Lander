using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProfileSelectMenuController : MonoBehaviour
{
    public void PlayGame(int profile)
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
