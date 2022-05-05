using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AchievementPanelController : MonoBehaviour
{
    [SerializeField] private AchievementType achievementType;
    
    private void OnEnable()
    {
        var components = GetComponentsInChildren<TextMeshProUGUI>();
        components[0].text = ProfileManager.CurProfile.GetAchievementOfType(achievementType).Name;
        components[1].text = ProfileManager.CurProfile.GetAchievementOfType(achievementType).CurProgress.ToString(); // 2=> rotation
    }
}