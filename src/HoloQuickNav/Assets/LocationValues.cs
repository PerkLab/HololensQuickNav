using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationValues : MonoBehaviour {

    public static float NoseR;
    public static float NoseA;
    public static float NoseS;

    public static float REyeR;
    public static float REyeA;
    public static float REyeS;

    public static float LEyeR;
    public static float LEyeA;
    public static float LEyeS;

    void OnEnable()
    {
        NoseR = -4.118f * 0.001f * -1f;
        NoseA = 131.456f * 0.001f;
        NoseS = -13.409f * 0.001f;

        REyeR = 31.411f * 0.001f * -1f;
        REyeA = 105.699f * 0.001f;
        REyeS = 26.204f * 0.001f;

        LEyeR = -38.416f * 0.001f * -1f;
        LEyeA = 98.403f * 0.001f;
        LEyeS = 21.539f * 0.001f;
    }
}
