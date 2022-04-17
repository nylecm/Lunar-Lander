using System;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "Lander", menuName = "ScriptableObjects/Lander", order = 1)]
public class LanderModel : ScriptableObject
{
    public string title;
    public string description = "N/A";
    public int price = 0;
    public float rotSpeedMultiplier = 1;
    public float thrustMultiplier = 1;
    public float strengthMultiplier = 1;
    public float fuelTankMultiplier = 1;
    public AudioClip rocketSound;
    public Sprite rocketImage;
}