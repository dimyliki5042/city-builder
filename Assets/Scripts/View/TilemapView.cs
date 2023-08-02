using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;
using UnityEngine.Tilemaps;

public class TilemapView : MonoBehaviour
{
    public static TilemapView Instance { get; set; }
    public Tilemap ground;
    public Tilemap firstFloor;
    public Tilemap secondFloor;
    public Tilemap roofFloor;
    public Sprite startSprite;
    [HideInInspector] public Camera cam;
    private GameVM vm;
    [Header("Tiles")]
    public List<Vector3Int> firstFloorList;
    public List<Vector3Int> secondFloorList;
    public List<Vector3Int> roofFloorList;
    public List<Vector3Int> roadMap;
    public List<Vector3Int> busyRoadMap;
    [Space]
    public GameObject carPrefab;
    public List<Sprite> allCarSprites;
    public Dictionary<string, List<Sprite>> carSprites = new Dictionary<string, List<Sprite>>();
    private void Start()
    {
        Instance = this;
        cam = Camera.main;
        vm = new GameVM();
        vm.Generate();
        vm.SortCarSprites();
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.currentSelectedGameObject) vm.Draw(Input.mousePosition);
        if (Input.GetMouseButtonDown(1) && !EventSystem.current.currentSelectedGameObject)
        {
            print("DELETE");
            vm.Delete(Input.mousePosition);
        }
    }
}
