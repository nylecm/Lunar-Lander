using System.Threading.Tasks;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CentralTextUI : MonoBehaviour
{
    private TextMeshProUGUI _centralText;

    private void OnEnable()
    {
        Player.OnLanded += UpdateText;
    }

    private void OnDisable()
    {
        Player.OnLanded -= UpdateText;
    }

    private void Awake()
    {
        _centralText = GetComponent<TextMeshProUGUI>();
    }

    private async void UpdateText(LandedCentreMessage msg)
    {
        _centralText.text = msg.ToString();
        await Task.Delay(2000);
        _centralText.text = "";
    }
}