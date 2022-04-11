using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class FuelUI : MonoBehaviour
{
    private TextMeshProUGUI fuelText;

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
        fuelText = GetComponent<TextMeshProUGUI>();
    }

    private void UpdateFuel(int fuel)
    {
        fuelText.text = "Fuel: " + fuel.ToString();
    }
}
