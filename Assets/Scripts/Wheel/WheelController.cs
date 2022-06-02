using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        float initialRotation = 0;
        float rotationStep = (float)Math.Round((double)360 / Game.Model.WheelModel.Sectors.Count, 3);

        for (int i = 0; i < Game.Model.WheelModel.Sectors.Count; i++)
        {
            Game.Controller.SectorController.CreateSector(initialRotation, i, rotationStep);
            initialRotation += rotationStep;
        }
        AlignSectorTexts(rotationStep);
    }

    private void AlignSectorTexts(float rotationStep)
    {
        Vector3 pos = new Vector3();
        pos.x = Game.Model.WheelModel.WheelCenter.transform.localPosition.x - Game.Model.WheelModel.Radius * Mathf.Sin(rotationStep * Mathf.Deg2Rad /2);
        pos.y = Game.Model.WheelModel.WheelCenter.transform.localPosition.y - Game.Model.WheelModel.Radius * Mathf.Cos(rotationStep * Mathf.Deg2Rad /2);
        pos.z = Game.Model.WheelModel.WheelCenter.transform.localPosition.z;
        for (int i=0; i < Game.Model.WheelModel.SectorViews.Count; i++)
        {
            Game.Model.WheelModel.SectorViews[i].IdText.transform.localPosition = pos;
        }
    }
    public void CollectSectorInfo(int id, float rotationZ, float rotationStep)
// ������� � ������� <Id ������� : [��������� ������, �������� ������]>
    {
        List<float> tempList = new List<float>();
        tempList.Add(rotationZ - (360-Game.Model.WheelModel.Arrow.transform.localEulerAngles.z) + 10);
        tempList.Add(rotationZ - (360- rotationStep - Game.Model.WheelModel.Arrow.transform.localEulerAngles.z) - 10);
        Game.Model.WheelModel.SectorsInfo.Add(id, tempList);
 //       Debug.Log(id.ToString() + " " + tempList[0].ToString() + " " + tempList[1].ToString());
    }

    public void CollectProbabilityRanges(int id, int sectorProbability)
// ������� ������� ����������� ���������
    {
        if (sectorProbability > 0)
        {
            if (!Game.Model.WheelModel.ProbabilityRanges.Any())
            {
                List<int> firstList = new List<int>();
                firstList.Add(0);
                firstList.Add(sectorProbability);
                Game.Model.WheelModel.ProbabilityRanges.Add(id, firstList);
                Game.Model.WheelModel.ProbabilitySum += sectorProbability;
            }
            else
            {
                List<int> tempList = new List<int>();
                tempList.Add(Game.Model.WheelModel.ProbabilityRanges.Values.Last()[1] + 1);
                tempList.Add(Game.Model.WheelModel.ProbabilityRanges.Values.Last()[1] + sectorProbability);
                Game.Model.WheelModel.ProbabilityRanges.Add(id, tempList);
                Game.Model.WheelModel.ProbabilitySum += sectorProbability;
            }
        }
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

        if (Game.Model.WheelModel.ProbabilityRanges.Count > 0)
        {
            foreach (KeyValuePair<int, List<int>> kvp in Game.Model.WheelModel.ProbabilityRanges)
            {
                if (kvp.Value[0] <= randWinner && randWinner <= kvp.Value[1])
                {
                    Game.Model.WheelModel.ActualWinnerId = kvp.Key;
                    Debug.Log("��������� id ����������: " + kvp.Key.ToString());
                    break;
                }
            }
        }
        else
        {
            Game.Model.WheelModel.ActualWinnerId = Game.Model.WheelModel.Sectors
                [UnityEngine.Random.Range(0, Game.Model.WheelModel.Sectors.Count)].Id;
            Debug.Log("��������� id ���������� " + Game.Model.WheelModel.ActualWinnerId.ToString());
        }   
    }

    private void ChooseParticularWinner()
// ���������� ���������� �� ���������� id. ���� id ������������, �� ���������� ���������� ��������
    {
        if (Game.Model.WheelModel.SectorsInfo.ContainsKey(Game.Model.WheelModel.ReceivedWinnerId))
        {
            Game.Model.WheelModel.ActualWinnerId = Game.Model.WheelModel.ReceivedWinnerId;
        }
        else
        {
            Debug.Log("������������ id ����������, ���������� ����� ������ ��������");
            ChooseRandomWinner();
        }
    }
}
