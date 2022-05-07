using System;
using UnityEditor;
using UnityEngine;

public static class LanderManager
{
    private static LanderModel CurLander;

    public static void SetCurLander(LanderModel lander)
    {
        CurLander = lander;
    }
    
    public static LanderModel MakeLander(string landerFileName)
    {
        return Resources.Load<LanderModel>("Landers/" + landerFileName + ".asset");
    }

    public static LanderModel GetCurLander()
    {
        return CurLander;
    }
}