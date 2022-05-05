using System;
using UnityEditor;
using UnityEngine;

public class BottomColisionDetector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<PowerUpDot>() != null) return;

        Debug.Log("Bottom Collision Detector Landing msg sent!");
        SendMessageUpwards("Landed");
    }
}