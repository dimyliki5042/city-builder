using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.EventSystems;

public class BuildMenu : MonoBehaviour
{
    public static BuildMenu Instance { get; set; }
    private GameVM gameVM;
    [Header("Sprites")]
    public List<Sprite> buildings;
    public List<Sprite> roads;
    public GameObject roadGO;
    public GameObject objectsGO;
    [Header("Resources")]
    public Text Energy;
    public Text Money;
    public Text Toxin;
    public Text People;
    [HideInInspector] public Animator anim;
    private void Start()
    {
        Instance = this;
        gameVM = new GameVM();
        anim = GetComponent<Animator>();
        gameVM.GenerateBuildMenu();
        gameVM.RefreshData();
    }
    public void OpenCloseMenu() => gameVM.OpenCloseMenu();
    public void ActivateDeactivate() => gameVM.ActivateDeactivate();
    public void GetSprite() => gameVM.GetSprite(EventSystem.current.currentSelectedGameObject);
}
