using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProfileSummaryFiller : MonoBehaviour
{
    [SerializeField] private int profileID;

    private void OnEnable()
    {
        if (!ProfileModel.DoesProfileExist(profileID)) return;

        var profile = ProfileModel.Load(profileID);
        Debug.Log(profile.Username);
        Debug.Log(profile.HighScore);
        var components = GetComponentsInChildren<TextMeshProUGUI>();
        components[0].text = "Profile 1 - " + profile.Username;
        components[2].text = "High Score: " + profile.HighScore;
    }
}