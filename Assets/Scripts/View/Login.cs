using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public static Login Instance { get; set; }
    public GameObject menu;
    private MenuVM vm;
    [Header("Info")]
    public InputField login;
    public InputField password;
    void Start()
    {
        Instance = this;
        vm = new MenuVM();
    }
    public void LogIn()
    {
        vm.LogIn();
    }
}
