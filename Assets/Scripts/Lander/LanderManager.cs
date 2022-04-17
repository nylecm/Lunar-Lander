using System;
using UnityEditor;
using UnityEngine;

public static class LanderManager
{
    public static LanderModel GetLander(string landerName)
    {
        //return Resources.Load<LanderModel>("Landers/UFO.asset");
        LanderModel lander = AssetDatabase.LoadAssetAtPath<LanderModel>("Assets/Landers/Apollo.asset");
        Debug.Log(lander.description);
        return lander;
        // var objectsOfType = .FindObjectsOfType<LanderModel>();
        // LanderModel targetLander = null;
        //
        // for (int i = 0; i < objectsOfType.Length; i++)
        // {
        //     if (objectsOfType[i].title.Equals(landerName))
        //     {
        //         Debug.Log("found, with thrust mult of: " + objectsOfType[i].thrustMultiplier );
        //         targetLander = objectsOfType[i];
        //     }
        //     else
        //     {
        //         Debug.Log("Unwanted with thrust mult of: " + objectsOfType[i].thrustMultiplier );
        //     }
        // }
        //
        // if (targetLander != null)
        // {
        //     return targetLander;
        // }
        // else
        // {
        //     throw new ArgumentException("Lander with name " + landerName + " does not exist." + objectsOfType.Length);
        // }
    }
}
