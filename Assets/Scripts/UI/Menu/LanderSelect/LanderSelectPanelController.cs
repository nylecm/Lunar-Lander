using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LanderSelectPanelController : MonoBehaviour
{
    [SerializeField] private string landerFileName;
    private LanderModel thisLander;
    
    private void OnEnable()
    {
        thisLander = LanderManager.MakeLander(landerFileName);
        var components = GetComponentsInChildren<TextMeshProUGUI>(); // todo work out ordering https://answers.unity.com/questions/365397/getcomponentsinchildren-always-the-same-order.html
        // 0 => play
        components[1].text = thisLander.title; // 1 => title
        components[2].text = "Rotation: " + thisLander.rotSpeedMultiplier; // 2=> rotation
        components[3].text = "Durability: " + thisLander.strengthMultiplier; // 3 => durability
        components[4].text = "Tank Capacity: " + thisLander.fuelTankMultiplier; // 4 => tank capacity
        components[5].text = thisLander.description; // 5 => description
        components[6].text = "Thrust: " + thisLander.thrustMultiplier; // 6 => thrust
    }

    public void Play()
    {
        LanderManager.SetCurLander(thisLander);
        SceneManager.LoadScene("SampleScene");
    }
}