using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class FuelUI : MonoBehaviour
{
    private TextMeshProUGUI _fuelText;

    private void OnEnable()
    {
        Player.OnFuelChange += UpdateFuel;
    }

    private void OnDisable()
    {
        Player.OnFuelChange -= UpdateFuel;
    }

    private void Awake()
    {
        _fuelText = GetComponent<TextMeshProUGUI>();
    }

    private void UpdateFuel(int fuel)
    {
        if (_fuelText != null) _fuelText.text = "Fuel: " + fuel.ToString();
    }
}
