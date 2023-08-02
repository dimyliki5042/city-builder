using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuVM
{
    public void LogIn() => Authorization.LogIn();
    public void Register() => Authorization.Register();
    public void Play() => Authorization.Play();
}
