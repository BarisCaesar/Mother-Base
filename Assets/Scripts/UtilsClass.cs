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
}
