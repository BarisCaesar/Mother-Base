using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilsClass
{
    private static Camera MainCamera;
    public static Vector3 GetMouseWorldPosition()
    {
        if(MainCamera == null) MainCamera = Camera.main;
        
        Vector3 MouseWorldPosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);
        MouseWorldPosition.z = 0f;

        return MouseWorldPosition;
    }

    public static Vector3 GetRandomDirection() 
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    public static float GetAngleFromVector(Vector3 Vec)
    {
        float Radians = Mathf.Atan2(Vec.y, Vec.x);
        float Degrees = Radians * Mathf.Rad2Deg;
        return Degrees;
    }
}
