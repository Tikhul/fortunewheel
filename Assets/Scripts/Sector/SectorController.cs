using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SectorController : FortuneWheelElement
{
    public void CreateSector(float zRotation, int index, float rotationStep)
    // Создание и настройка сектора
    {
        Image newSector = Instantiate(Game.Model.WheelModel.SectorPrefab);
        newSector.GetComponent<SectorView>().SetUpSector(newSector, zRotation, index, rotationStep);
        Game.Model.WheelModel.SectorViews.Add(newSector.GetComponent<SectorView>());
        Game.Controller.WheelController.CollectSectorInfo(Game.Model.WheelModel.Sectors[index].Id, newSector.transform.parent.rotation.z - newSector.transform.localEulerAngles.z, rotationStep);
        Game.Controller.WheelController.CollectProbabilityRanges(Game.Model.WheelModel.Sectors[index].Id, Game.Model.WheelModel.Sectors[index].Probability);
    }

    public void LaunchSectorTurnOff()
    {
        foreach (SectorView s in Game.Model.WheelModel.SectorViews)
        {
            s.TurnOffSector();
        }

    }
    public void LaunchSectorHighlight()
    {
        foreach (SectorView s in Game.Model.WheelModel.SectorViews)
        {
            s.HighlightSector(Game.Model.WheelModel.ActualWinnerId);
        }
    }
}
