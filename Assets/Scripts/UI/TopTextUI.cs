using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TopTextUI : MonoBehaviour
{
    private TextMeshProUGUI _topText;

    private void OnEnable()
    {
        AchievementManager.OnAchievementProgress += UpdateText;
    }

    private void OnDisable()
    {
        AchievementManager.OnAchievementProgress -= UpdateText;
    }

    private void Awake()
    {
        _topText = GetComponent<TextMeshProUGUI>();
    }

    private async void UpdateText(AchievementModel achievementModel)
    {
        if (achievementModel.IsComplete())
        {
            _topText.text = "Achievement Unlocked:\n" + achievementModel.Name + "!";
            await Task.Delay(2000);
            _topText.text = "";
        }
    }
}