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

    private void UpdateFuel(float fuel)
    {
        if (fuel < 0.0f) fuel = 0; // todo round it
        if (_fuelText != null) _fuelText.text = "Fuel: " + fuel;
    }
}
