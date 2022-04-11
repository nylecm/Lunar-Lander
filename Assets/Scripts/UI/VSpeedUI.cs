using System;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class VSpeedUI : MonoBehaviour
{
    private TextMeshProUGUI _vSpeedText;

    private void OnEnable()
    {
        Player.OnVSpeedChange += UpdateVSpeed;
    }

    private void OnDisable()
    {
        Player.OnVSpeedChange -= UpdateVSpeed;
    }

    private void Awake()
    {
        _vSpeedText = GetComponent<TextMeshProUGUI>();
    }

    private void UpdateVSpeed(float vSpeed)
    {
        vSpeed = (float)Math.Round(vSpeed * 197.0f);
        _vSpeedText.text = "V-Speed: " + vSpeed + "fpm";
    }
}
