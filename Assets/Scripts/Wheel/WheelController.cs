using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelController : FortuneWheelElement
{
    private void Start()
    {
        CreateWheel();
    }
    private void CreateWheel()
    {
        double initialRotation = 0;
        double rotationStep = 360 / Game.Model.WheelModel.Sectors.Count;

        for (int i = 0; i < Game.Model.WheelModel.Sectors.Count; i++)
        {
            Image newSector = Instantiate(Game.Model.WheelModel.SectorPrefab);
            newSector.transform.parent = Game.Model.WheelModel.WheelCanvas.transform;
            newSector.sprite = Game.Model.WheelModel.Sectors[i].SourceImage;
            newSector.GetComponent<SectorView>().SetRotation(initialRotation+=Math.Round(rotationStep, 3));
            initialRotation += Math.Round(rotationStep, 3);
        }
    }
    private void CollectProbabilitySum()
// Общая вероятность по всем секторам
    {

    }
}
