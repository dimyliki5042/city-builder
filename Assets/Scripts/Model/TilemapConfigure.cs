using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapConfigure
{
    static private TilemapView tilemapView;
    static private int width = 100, height = 100;
    static public void Generate()
    {
        tilemapView = TilemapView.Instance;
        Tile startTile = ScriptableObject.CreateInstance<Tile>();
        startTile.sprite = tilemapView.startSprite;
        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
                tilemapView.ground.SetTile(new Vector3Int(x, y, 0), startTile);
    }
    static public void Draw(Vector3 mosPos)
    {
        if (BuildMenuModel.choosenSprite == null) return;
        Tilemap selectedMap = null;
        Tile choosenTile = ScriptableObject.CreateInstance<Tile>();
        choosenTile.sprite = BuildMenuModel.choosenSprite;
        Vector3Int tilePos = tilemapView.ground.WorldToCell(tilemapView.cam.ScreenToWorldPoint(mosPos));
        if (choosenTile.sprite.name.StartsWith("road") && !tilemapView.roadMap.Contains(tilePos) && Resources.Money >= 50)
        {
            selectedMap = tilemapView.ground;
            tilemapView.roadMap.Add(tilePos);
            Resources.Money -= (int)Resources.Cost.Road;
            if (tilemapView.roadMap.Count % 4 == 0) SpawnCar();
        }
        else if (!choosenTile.sprite.name.StartsWith("road") && tilemapView.roadMap.Contains(tilePos))
        {
            selectedMap = tilemapView.ground;
            tilemapView.roadMap.Remove(tilePos);
            Resources.Money += (int)Resources.Cost.Road / 2;
        }
        else if (choosenTile.sprite.name.StartsWith("block") && Resources.Money >= 100)
        {
            if (!tilemapView.firstFloorList.Contains(tilePos))
            {
                selectedMap = tilemapView.firstFloor;
                tilemapView.firstFloorList.Add(tilePos);
            }
            else if (!tilemapView.secondFloorList.Contains(tilePos))
            {
                selectedMap = tilemapView.secondFloor;
                tilemapView.secondFloorList.Add(tilePos);
            }
            else return;
            Resources.Money -= (int)Resources.Cost.Block;
            Resources.People += 4;
        }
        else if (choosenTile.sprite.name.StartsWith("roof") && Resources.Money >= 100)
        {
            selectedMap = tilemapView.roofFloor;
            tilemapView.roofFloorList.Add(tilePos);
            Resources.Money -= (int)Resources.Cost.Block;
        }
        tilePos.z = 0;
        selectedMap.SetTile(tilePos, choosenTile);
        BuildMenuModel.RefreshData();
    }
    static public void Delete(Vector3 mosPos)
    {
        Tile tile = ScriptableObject.CreateInstance<Tile>();
        tile.sprite = null;
        Vector3Int tilePos = tilemapView.ground.WorldToCell(tilemapView.cam.ScreenToWorldPoint(mosPos));
        if (tilemapView.roofFloorList.Contains(tilePos))
        {
            tilemapView.roofFloor.SetTile(tilePos, tile);
            tilemapView.roofFloorList.Remove(tilePos);
        }
        else if (tilemapView.secondFloorList.Contains(tilePos))
        {
            tilemapView.secondFloor.SetTile(tilePos, tile);
            tilemapView.secondFloorList.Remove(tilePos);
            Resources.People -= 4;
        }
        else if (tilemapView.firstFloorList.Contains(tilePos))
        {
            tilemapView.firstFloor.SetTile(tilePos, tile);
            tilemapView.firstFloorList.Remove(tilePos);
            Resources.People -= 4;
        }
        BuildMenuModel.RefreshData();
    }
    static private void SpawnCar()
    {
        if(tilemapView.roadMap.Count > 3)
        {
            GameObject car = GameObject.Instantiate(tilemapView.carPrefab);
            int random = Random.Range(0, tilemapView.roadMap.Count);
            car.name = ParseSpriteName(tilemapView.allCarSprites[random]);
            car.GetComponent<SpriteRenderer>().sprite = tilemapView.carSprites[car.name][0];
        }
    }
    static private string ParseSpriteName(Sprite sprite)
    {
        string[] arrName = sprite.name.Split("_");
        if (arrName.Length == 3) return arrName[2].Remove(arrName[2].Length - 3);
        else return arrName[2] + " " + arrName[3].Remove(arrName[3].Length - 3);
    }
    static public void SortCarSprites()
    {
        foreach (Sprite sprite in tilemapView.allCarSprites)
        {
            string key = ParseSpriteName(sprite);
            if (tilemapView.carSprites.ContainsKey(key)) tilemapView.carSprites[key].Add(sprite);
            else tilemapView.carSprites.Add(key, new List<Sprite>() { sprite });
        }
    }
}
