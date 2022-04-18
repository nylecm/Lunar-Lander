using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LanderSelectMenuController : MonoBehaviour
{
    public void Back()
    {
        SceneManager.LoadScene("ProfileSelectMenu");
    }
}