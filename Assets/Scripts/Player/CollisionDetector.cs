using System;
using UnityEditor;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    // private readonly PhysicsMaterial2D _platformMaterial =
    //     AssetDatabase.LoadAssetAtPath<PhysicsMaterial2D>("Assets/Materials/Moon Surface.physicsMaterial2D");

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<PowerUpDot>() != null)
        {
            if (col.gameObject.GetComponent<PowerUpDot>().PowerUpType == PowerUpType.FUEL)
            {
                SendMessageUpwards("HandleFuelPowerUp");
            }
            else if (col.gameObject.GetComponent<PowerUpDot>().PowerUpType == PowerUpType.STOP)
            {
                SendMessageUpwards("HandleStopPowerUp");
            }
        }
        else
        {
            Debug.Log("Non-Bottom Collision Detector Landing msg sent!");
            SendMessageUpwards("LandedNonBottom");
        }

        //}
        // else
        // {
        //     
        // }
    }
}