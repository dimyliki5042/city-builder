using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Registration : MonoBehaviour
{
    public static Registration Instance { get; set; }
    private MenuVM vm;
    [Header("Info")]
    public InputField firstname;
    public InputField lastname;
    public InputField email;
    public InputField password;
    void Start()
    {
        Instance = this;
        vm = new MenuVM();
    }
    public void Register() => vm.Register();
}
