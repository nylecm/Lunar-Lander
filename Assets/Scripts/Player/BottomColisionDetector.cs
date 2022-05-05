using System;
using UnityEditor;
using UnityEngine;

public class BottomColisionDetector : MonoBehaviour
{
    // private readonly PhysicsMaterial2D _platformMaterial =
    //     AssetDatabase.LoadAssetAtPath<PhysicsMaterial2D>("Assets/Materials/Moon Surface.physicsMaterial2D");

    private void OnTriggerEnter2D(Collider2D col)
    {
        //if (col.sharedMaterial.name.Equals(_platformMaterial.name))
        //{
        if (col.gameObject.GetComponent<PowerUpDot>() != null)
        {
            Debug.Log("bah");
            
        }
        else
        {
            Debug.Log("Bottom Collision Detector Landing msg sent!");
            SendMessageUpwards("Landed");
        }
        //}
        //else
        //{

        //}
    }
}