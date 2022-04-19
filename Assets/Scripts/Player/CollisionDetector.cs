using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    // private List<GameObject> Colliders = new List<GameObject>();
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        SendMessage("HandleGameFailure"); // todo make this a bad collision detector
        Debug.Log("Non-Bottom Collision Detector msg sent!");
    }

    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     if (Colliders.Contains(other.gameObject)) Colliders.Remove(other.gameObject);
    // }
}
