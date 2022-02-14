using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionDetector : MonoBehaviour
{
    // private List<GameObject> Colliders = new List<GameObject>();
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        SendMessage("Landed");
        Debug.Log("Hit msg sent");
    }

    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     if (Colliders.Contains(other.gameObject)) Colliders.Remove(other.gameObject);
    // }
}