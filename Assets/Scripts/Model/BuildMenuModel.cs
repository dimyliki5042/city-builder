using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public static class BuildMenuModel
{
    static public Sprite choosenSprite;
    static public void OpenCloseMenu() => BuildMenu.Instance.anim.SetBool("isOpen", !BuildMenu.Instance.anim.GetBool("isOpen"));
    static public void ActivateDeactivate() => BuildMenu.Instance.transform.GetChild(0).gameObject.SetActive(BuildMenu.Instance.anim.GetBool("isOpen"));
    static public void GetSprite(GameObject button) => choosenSprite = button.GetComponent<Image>().sprite;
    static public void GenerateBuildMenu()
    {
        BuildMenu buildMenu = BuildMenu.Instance;
        for(int i = 0; i < buildMenu.buildings.Count; i++)
        {
            GameObject objectButton = new GameObject(buildMenu.buildings[i].name, typeof(Image), typeof(Button));
            objectButton.GetComponent<Image>().sprite = buildMenu.buildings[i];
            objectButton.transform.SetParent(buildMenu.objectsGO.transform, false);
            objectButton.GetComponent<Button>().onClick.AddListener(delegate { BuildMenu.Instance.GetSprite(); });
        }
        for (int i = 0; i < buildMenu.roads.Count; i++)
        {
            GameObject roadButton = new GameObject(buildMenu.roads[i].name, typeof(Image), typeof(Button));
            roadButton.GetComponent<Image>().sprite = buildMenu.roads[i];
            roadButton.transform.SetParent(buildMenu.roadGO.transform, false);
            roadButton.GetComponent<Button>().onClick.AddListener(delegate { BuildMenu.Instance.GetSprite(); });
        }
        buildMenu = null;
    }
    static public void RefreshData()
    {
        BuildMenu.Instance.Energy.text = Resources.Energy.ToString();
        BuildMenu.Instance.Money.text = Resources.Money.ToString();
        BuildMenu.Instance.Toxin.text = Resources.Toxin.ToString();
        BuildMenu.Instance.People.text = Resources.People.ToString();
    }
}
