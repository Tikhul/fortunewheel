using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SectorController : FortuneWheelElement
{
    public void SetUpSector(double rotationStep, double zRotation, int index)
// Создание и настройка сектора
    {
        Image newSector = Instantiate(Game.Model.WheelModel.SectorPrefab);
        newSector.transform.SetParent(Game.Model.WheelModel.WheelCanvas.transform);
        newSector.sprite = Game.Model.WheelModel.Sectors[index].SourceImage;
        newSector.transform.localScale = new Vector3(1, 1, 1);
        newSector.transform.localPosition = newSector.transform.position;
        newSector.transform.localRotation = Quaternion.Euler(0, 0, (float)zRotation);
        newSector.fillAmount = (float)Math.Round((double)1 / Game.Model.WheelModel.Sectors.Count, 3);
        Game.Controller.WheelController.CollectSectorInfo(index, zRotation - rotationStep, zRotation);
        Game.Controller.WheelController.CollectProbabilityInfo(Game.Model.WheelModel.Sectors[index].Probability);
    }
}
