using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMoues : MonoBehaviour
{
    [SerializeField]
    private float rotCamXAxisSpeed = 5;

    [SerializeField]
    private float rotCamYAxisSpeed = 5;

    [SerializeField]
    private float limitMinX = -80;

    [SerializeField]
    private float limitMaxX = 80;

    private float eulerAngleX;
    private float eulerAngleY;

    void Start()
    {
        eulerAngleX = 0;
        eulerAngleY = 180;
    }
    
    public void UpdateRotate(float mouseX, float mouseY)
    {
        eulerAngleY += mouseX * rotCamYAxisSpeed;
        eulerAngleX -= mouseY * rotCamXAxisSpeed;

        eulerAngleX = ClampAngle(eulerAngleX, limitMinX, limitMaxX);

        transform.rotation = Quaternion.Euler(eulerAngleX, eulerAngleY, 0);
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;

        return Mathf.Clamp(angle, min, max);
    }
}
