using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CarAI
{
    static private TilemapView tilemapView;
    static private float speed = 1f;
    static private List<Vector3Int> carRotation = new List<Vector3Int>()
    {
        new Vector3Int(-1, 0, 0),
        new Vector3Int(0, 1, 0),
        new Vector3Int(1, 0, 0),
        new Vector3Int(0, -1, 0)
    };
    static public void Start(CarView car)
    {
        tilemapView = TilemapView.Instance;
        int index;
        do
        {
            index = Random.Range(0, tilemapView.roadMap.Count);
        }while(tilemapView.busyRoadMap.Contains(tilemapView.roadMap[index]));
        tilemapView.busyRoadMap.Add(tilemapView.roadMap[index]);
        car.Index = tilemapView.busyRoadMap.Count - 1;
        car.transform.position = tilemapView.ground.CellToWorld(tilemapView.roadMap[index]);
        car.transform.position += new Vector3(0, 0.5f, 9.9f);
    }
    static public void FindPosition(CarView car)
    {
        Vector3Int carPos = tilemapView.busyRoadMap[car.Index];
        List<Vector3Int> newPositions = new List<Vector3Int>()
        {
            carPos + carRotation[0],
            carPos + carRotation[1],
            carPos + carRotation[2],
            carPos + carRotation[3]

        };
        newPositions.RemoveAll(x => !tilemapView.roadMap.Contains(x) || x == car.prevPos || tilemapView.busyRoadMap.Contains(x));
        car.prevPos = carPos;
        if (newPositions.Count == 0)
        {
            car.StartCoroutine(car.Wait());
            return;
        }
        int index = Random.Range(0, newPositions.Count);
        car.newPos = tilemapView.ground.CellToWorld(newPositions[index]);
        tilemapView.busyRoadMap[car.Index] = newPositions[index];
        car.newPos += new Vector3(0, 0.5f, 9.9f);
        int rotationIndex = carRotation.IndexOf(newPositions[index] - carPos);
        car.GetComponent<SpriteRenderer>().sprite = tilemapView.carSprites[car.name][rotationIndex];
        car.isMove = true;
    }
    public static void Move(CarView car)
    {
        car.transform.position = Vector3.MoveTowards(car.transform.position, car.newPos, Time.deltaTime * speed);
        if (car.transform.position == car.newPos)
        {
            car.isMove = false;
            car.isFindPosition = true;
        }
    }
    public static void Lights(CarView car)
    {
        if(!CameraView.Instance.isDay) car.lights.SetActive(true);
        else car.lights.SetActive(false);
    }
}
