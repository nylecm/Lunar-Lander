using UnityEngine;

public class BottomColisionDetector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        SendMessageUpwards("Landed");
        Debug.Log("Bottom Collision Detector msg sent!");
    }
}