using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarView : MonoBehaviour
{
    private GameVM vm;
    [HideInInspector] public int Index;
    [HideInInspector] public bool isMove;
    [HideInInspector] public bool isFindPosition;
    [HideInInspector] public GameObject lights;
    [HideInInspector] public Vector3 newPos;
    [HideInInspector] public Vector3 prevPos;
    private void Start()
    {
        lights = transform.GetChild(0).gameObject;
        isMove = false;
        isFindPosition = false;
        vm = new GameVM();
        vm.StartCar(this);
        vm.CarFindPosition(this);
    }
    private void Update()
    {
        if (isMove) vm.MoveCar(this);
    }
    private void FixedUpdate()
    {
        if (isFindPosition)
        {
            vm.CarFindPosition(this);
            isFindPosition = false;
        }
        vm.TurnOnOffLight(this);
    }
    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        vm.CarFindPosition(this);
    }
}
