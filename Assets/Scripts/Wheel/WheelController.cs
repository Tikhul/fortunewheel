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
        double rotationStep = Math.Round((double)360 / Game.Model.WheelModel.Sectors.Count, 3);

        for (int i = 0; i < Game.Model.WheelModel.Sectors.Count; i++)
        {
            Game.Controller.SectorController.SetUpSector(rotationStep, initialRotation, i);
            initialRotation += rotationStep;
        }
    }

    public void CollectSectorInfo(int id, double initZ, double finalZ)
// ������� � ������� <Id ������� : [��������� ������, �������� ������]>
    {
        List<double> tempList = new List<double>();
        tempList.Add(initZ);
        tempList.Add(finalZ);
        Game.Model.WheelModel.SectorsInfo.Add(id, tempList);
//     Debug.Log(id.ToString() + " " + initZ.ToString() + " " + finalZ.ToString());
    }

    public void CollectProbabilityInfo(int sectorProbability)
// ���� ���� ����� �����������
    {
        Game.Model.WheelModel.ProbabilitySum += sectorProbability;
    }

    public void LaunchWinnerChoice()
// ����� ���������� (������)
    {
        if (Game.Model.WheelModel.RandomWinner)
        {
            ChooseRandomWinner();
        }
        else
        {
            ChooseParticularWinner();
        }
    }
    private void ChooseRandomWinner()
// ����� �������-���������� � ����������� �� ����������� ���������
    {
        int randWinner = UnityEngine.Random.Range(1, Game.Model.WheelModel.ProbabilitySum + 1);
        foreach (SectorSO sector in Game.Model.WheelModel.Sectors)
        {
            for(int i = 1; i <= sector.Probability; i++)
            {
                if (i == randWinner)
                {
                    Game.Model.WheelModel.WinnerId = sector.Id;
                    break;
                }
            }
        }
    }

    private void ChooseParticularWinner()
// ���������� ���������� �� ���������� id. ���� id ������������, �� ���������� ���������� ��������
    {
        if (Game.Model.WheelModel.SectorsInfo.ContainsKey(Game.Model.WheelModel.WinnerId))
        {
            Game.Model.WheelModel.WinnerId = Game.Model.WheelModel.WinnerId;
        }
        else
        {
            Debug.Log("������������ id ����������, ���������� ����� ������ ��������");
            ChooseRandomWinner();
        }
    }
}
