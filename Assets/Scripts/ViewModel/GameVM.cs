using UnityEngine;

public class GameVM
{
    public void OpenCloseMenu() => BuildMenuModel.OpenCloseMenu();
    public void ActivateDeactivate() => BuildMenuModel.ActivateDeactivate();
    public void Generate() => TilemapConfigure.Generate();
    public void GetSprite(GameObject button) => BuildMenuModel.GetSprite(button);
    public void Draw(Vector3 mosPos) => TilemapConfigure.Draw(mosPos);
    public void Delete(Vector3 mosPos) => TilemapConfigure.Delete(mosPos);
    public void GenerateBuildMenu() => BuildMenuModel.GenerateBuildMenu();
    public void CameraMove() => CameraModel.Move();
    public void StartCar(CarView car) => CarAI.Start(car);
    public void SortCarSprites() => TilemapConfigure.SortCarSprites();
    public void CarFindPosition(CarView car) => CarAI.FindPosition(car);
    public void MoveCar(CarView car) => CarAI.Move(car);
    public void TurnOnOffLight(CarView car) => CarAI.Lights(car);
    public void SunMove() => CameraModel.SunMove();
    public void ChangeTime() => CameraModel.ChangeTime();
    public void RefreshData() => BuildMenuModel.RefreshData();
}
