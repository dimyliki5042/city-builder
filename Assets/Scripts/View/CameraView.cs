using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraView : MonoBehaviour
{
    public static CameraView Instance { get; set; }
    public float speed;
    [HideInInspector] public float angle;
    [Header("Day/Night Parameters")]
    public bool isDay;
    [HideInInspector] public bool change;
    public Transform sun;
    public Quaternion newQuat;
    private GameVM vm;
    private void Start()
    {
        angle = 0;
        isDay = true;
        Instance = this;
        vm = new GameVM();
        StartCoroutine(ChangeTime());
    }
    private void FixedUpdate()
    {
        vm.CameraMove();
        if (change)
        {
            vm.SunMove();
            if (angle == -90 || angle == 0)
            {
                change = false;
                StartCoroutine(ChangeTime());
            }
        }
    }
    public IEnumerator ChangeTime()
    {
        vm.ChangeTime();
        yield return new WaitForSeconds(30f);
        change = true;
        isDay = !isDay;
    }
}
