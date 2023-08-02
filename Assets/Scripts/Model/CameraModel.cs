using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraModel
{
    public static void Move()
    {
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        CameraView.Instance.transform.Translate(direction * CameraView.Instance.speed);
    }
    public static void SunMove()
    {
        CameraView.Instance.sun.localRotation = Quaternion.Lerp(CameraView.Instance.sun.localRotation, CameraView.Instance.newQuat, Time.fixedDeltaTime);
        CameraView.Instance.angle = Mathf.Repeat(CameraView.Instance.sun.localEulerAngles.x + 180, 360) - 180;
    }
    public static void ChangeTime()
    {
        float x = Mathf.Abs(CameraView.Instance.angle) - 90;
        CameraView.Instance.newQuat = Quaternion.Euler(x, 0, 0);
    }
}
