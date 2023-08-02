using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    MenuVM vm;
    private void Start()
    {
        vm = new MenuVM();
    }
    public void Play() => vm.Play();
}
