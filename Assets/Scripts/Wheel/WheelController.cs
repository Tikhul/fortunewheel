using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WheelController : FortuneWheelElement
{
    private void Start()
    {
        CreateWheel();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RotateWheel();
        }
    }
    private void CreateWheel()
 // Создание колеса
    {
        double initialRotation = 0;
        double rotationStep = Math.Round((double)360 / Game.Model.WheelModel.Sectors.Count, 3);

        for (int i = 0; i < Game.Model.WheelModel.Sectors.Count; i++)
        {
            Game.Controller.SectorController.SetUpSector(rotationStep, initialRotation, i);
            initialRotation += rotationStep;
        }
    }

    public void CollectSectorInfo(int id, double initZ, double finalZ)
// Словарь в формате <Id сектора : [начальный градус, конечный градус]>
    {
        List<double> tempList = new List<double>();
        tempList.Add(initZ);
        tempList.Add(finalZ);
        Game.Model.WheelModel.SectorsInfo.Add(id, tempList);
//        Debug.Log(id.ToString() + " " + initZ.ToString() + " " + finalZ.ToString());
    }

    public void CollectProbabilityInfo(int sectorProbability)
// Сбор всех чисел вероятности
    {
        Game.Model.WheelModel.ProbabilitySum += sectorProbability;
    }

    private void RotateWheel()
    {
        Debug.Log("RotateWheel");
        Game.Model.WheelModel.SectorsParent.transform.DORotate(new Vector3(0, 0, 360), 5, RotateMode.FastBeyond360)
            ;
    }
}
