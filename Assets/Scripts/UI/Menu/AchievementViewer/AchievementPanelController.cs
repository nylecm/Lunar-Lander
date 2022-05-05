using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class AchievementPanelController : MonoBehaviour
{
    [SerializeField] private AchievementType achievementType;

    private void OnEnable()
    {
        var components = GetComponentsInChildren<TextMeshProUGUI>();
        var thisAchievement = ProfileManager.CurProfile.GetAchievementOfType(achievementType);
        if (ProfileManager.CurProfile.GetAchievementOfType(achievementType) == null)
        {
            thisAchievement = AchievementFactory.MakeAchievementOfType(achievementType);
            components[0].text = thisAchievement.Name + "\n" + thisAchievement.Description;
            components[1].text = "Locked";
        }
        else
        {
            components[0].text = thisAchievement.Name;
            if (thisAchievement.IsComplete())
            {
                GetComponent<UnityEngine.UI.Image>().color = new Color(0,1,0);
                components[1].text = "Unlocked";
            }
            else
            {
                GetComponent<UnityEngine.UI.Image>().color = new Color(0,0,1);
                components[1].text =
                    "Progress: " + thisAchievement.CurProgress + "/" + thisAchievement.ProgressRequired;
            }
        }
    }
}