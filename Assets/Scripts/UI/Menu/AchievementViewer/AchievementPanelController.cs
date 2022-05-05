using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AchievementPanelController : MonoBehaviour
{
    private void OnEnable()
    {
        var components = GetComponentsInChildren<TextMeshProUGUI>();
        components[0].text = ProfileManager.CurProfile.GetAchievementOfType(AchievementType.Butter).Name;
        components[1].text = ProfileManager.CurProfile.GetAchievementOfType(AchievementType.Butter).CurProgress.ToString(); // 2=> rotation
    }
}