using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProfileSelectMenuController : MonoBehaviour
{
    private void OnEnable()
    {
        //throw new NotImplementedException();
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
