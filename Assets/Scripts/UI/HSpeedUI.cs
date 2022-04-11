using System;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class HSpeedUI : MonoBehaviour
{
    private TextMeshProUGUI _hSpeedText;

    private void OnEnable()
    {
        Player.OnHSpeedChange += UpdateHSpeed;
    }

    private void OnDisable()
    {
        Player.OnHSpeedChange -= UpdateHSpeed;
    }

    private void Awake()
    {
        _hSpeedText = GetComponent<TextMeshProUGUI>();
    }

    private void UpdateHSpeed(float hSpeed)
    {
        hSpeed = (float)Math.Round(hSpeed * 197.0f);
        _hSpeedText.text = "H-Speed: " + hSpeed + "fpm";
    }
}
