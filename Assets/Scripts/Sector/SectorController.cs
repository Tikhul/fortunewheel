using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SectorController : FortuneWheelElement
{
    public void SetUpSector(float rotationStep, float zRotation, int index)
// Создание и настройка сектора
    {
        Image newSector = Instantiate(Game.Model.WheelModel.SectorPrefab);
        newSector.GetComponent<SectorView>().SetUpSector(newSector, zRotation, index);
        Game.Controller.WheelController.CollectSectorInfo(Game.Model.WheelModel.Sectors[index].Id, newSector.transform.parent.rotation.z - newSector.transform.localEulerAngles.z);
        Game.Controller.WheelController.CollectProbabilityRanges(Game.Model.WheelModel.Sectors[index].Id, Game.Model.WheelModel.Sectors[index].Probability);
    }
}
