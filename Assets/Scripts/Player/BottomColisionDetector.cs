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
            SendMessageUpwards("Landed");
            Debug.Log("Bottom Collision Detector msg sent!");
        //}
        //else
        //{
            
        //}
    }
}