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
 // �������� ������
    {
        double initialRotation = 0;
        double rotationStep = 360 / Game.Model.WheelModel.Sectors.Count;

        for (int i = 0; i < Game.Model.WheelModel.Sectors.Count; i++)
        {
            Game.Controller.SectorController.SetUpSector(initialRotation, i);
            initialRotation += Math.Round(rotationStep, 3);
        }
    }
    private void CollectProbabilitySum()
// ����� ����������� �� ���� ��������
    {

    }

    private void CollectSectorInfo()
// ������� � ������� Id ������� : [��������� ������, �������� ������, ����������� ���������]
    {

    }
}
