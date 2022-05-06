using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Menu.AchievementViewer
{
    public class AchievementBack : MonoBehaviour
    {
        public void Back()
        {
            SceneManager.LoadScene("ProfileSelectMenu");
        }
    }
}