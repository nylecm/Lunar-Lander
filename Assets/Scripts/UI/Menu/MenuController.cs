
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("ProfileSelectMenu");
    }

    public void Exit()
    {
        Application.Quit();
    }
}