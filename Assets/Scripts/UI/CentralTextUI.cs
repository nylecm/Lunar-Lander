using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CentralTextUI : MonoBehaviour
{
    private TextMeshProUGUI _centralText;

    private void OnEnable()
    {
        Player.OnLanded += UpdateText;
        //Player.OnFuelChange += UpdateFuel;
    }

    private void OnDisable()
    {
        Player.OnLanded -= UpdateText;
    }

    private void Awake()
    {
        _centralText = GetComponent<TextMeshProUGUI>();
    }

    private void UpdateText(LandedCentreMessage msg)
    {
        _centralText.text = msg.ToString();
    }
}