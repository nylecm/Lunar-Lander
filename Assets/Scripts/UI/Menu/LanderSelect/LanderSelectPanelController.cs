using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LanderSelectPanelController : MonoBehaviour
{
    [SerializeField] private string landerFileName;
    private LanderModel thisLander;
    
    private void OnEnable()
    {
        thisLander = LanderManager.MakeLander(landerFileName);
    }

    public void Play()
    {
        LanderManager.SetCurLander(thisLander);
        SceneManager.LoadScene("SampleScene");
    }
}