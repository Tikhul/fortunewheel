using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WheelController : FortuneWheelElement
{
    private Dictionary<int, List<float>> tempDict = new Dictionary<int, List<float>>();
    private void Start()
    {
        CreateWheel();
    }
    private void CreateWheel()
 // Создание колеса
    {
        float initialRotation = 0;
        float rotationStep = (float)Math.Round((double)360 / Game.Model.WheelModel.Sectors.Count, 3);

        for (int i = 0; i < Game.Model.WheelModel.Sectors.Count; i++)
        {
            Game.Controller.SectorController.SetUpSector(rotationStep, initialRotation, i);
            initialRotation += rotationStep;
        }
    }

    public void CollectTemporarySectorInfo(int id, float initZ, float finalZ)
// Словарь в формате <Id сектора : [начальный градус, конечный градус]> (локальный поворот для детей)
    {
        List<float> tempList = new List<float>();
        tempList.Add(initZ);
        tempList.Add(finalZ);
        tempDict.Add(id, tempList);
        if(tempDict.Count == Game.Model.WheelModel.Sectors.Count)
        {
            CollectCorrectSectorInfo();
        }
    }

    private void CollectCorrectSectorInfo()
    {
        for (int i=0; i < tempDict.Count; i++)
        {
            if(i == 0)
            {
                List<float> tempList = new List<float>();
                tempList.Add(tempDict.Values.ToList()[i][0] + tempDict.Values.ToList()[i][1] - 180);
                tempList.Add(tempDict.Values.ToList()[i][0] - 180);
                Game.Model.WheelModel.SectorsInfo.Add(tempDict.Keys.ToList()[i], tempList);
                Debug.Log(tempDict.Keys.ToList()[i].ToString() + " " + tempList[0].ToString() + " " + tempList[1].ToString());
            }
            else
            {
                List<float> tempList = new List<float>();
                tempList.Add(Game.Model.WheelModel.SectorsInfo.Values.ToList()[i - 1][1]);
                tempList.Add(tempList[0] - tempDict.Values.ToList()[i][0]);
                Game.Model.WheelModel.SectorsInfo.Add(tempDict.Keys.ToList()[i], tempList);
                Debug.Log(tempDict.Keys.ToList()[i].ToString() + " " + tempList[0].ToString() + " " + tempList[1].ToString());
            }
        }
    }

    public void CollectProbabilityInfo(int sectorProbability)
// Сбор всех чисел вероятности
    {
        Game.Model.WheelModel.ProbabilitySum += sectorProbability;
    }

    public void CollectProbabilityRanges(int id, int sectorProbability)
// Собираю отрезки вероятности выпадения
    {
        if (sectorProbability > 0)
        {
            if (!Game.Model.WheelModel.ProbabilityRanges.Any())
            {
                List<int> firstList = new List<int>();
                firstList.Add(0);
                firstList.Add(sectorProbability);
                Game.Model.WheelModel.ProbabilityRanges.Add(id, firstList);
            }
            else
            {
                List<int> tempList = new List<int>();
                tempList.Add(Game.Model.WheelModel.ProbabilityRanges.Values.Last()[1] + 1);
                tempList.Add(Game.Model.WheelModel.ProbabilityRanges.Values.Last()[1] + sectorProbability);
                Game.Model.WheelModel.ProbabilityRanges.Add(id, tempList);
            }
        }
    }
    public void LaunchWinnerChoice()
// Выбор победителя (запуск)
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
// Выбор сектора-победителя в зависимости от вероятности выпадения
    {
        Debug.Log("ChooseRandomWinner");
        int randWinner = UnityEngine.Random.Range(1, Game.Model.WheelModel.ProbabilitySum + 1);

        if (Game.Model.WheelModel.ProbabilityRanges.Count > 0)
        {
            foreach (KeyValuePair<int, List<int>> kvp in Game.Model.WheelModel.ProbabilityRanges)
            {
                if (kvp.Value[0] <= randWinner && randWinner <= kvp.Value[1])
                {
                    Game.Model.WheelModel.ActualWinnerId = kvp.Key;
                    Debug.Log(kvp.Key);
                    break;
                }
            }
        }
        else
        {
            Game.Model.WheelModel.ActualWinnerId = Game.Model.WheelModel.Sectors
                [UnityEngine.Random.Range(0, Game.Model.WheelModel.Sectors.Count)].Id;
        }   
    }

    private void ChooseParticularWinner()
// Назначение победителя по указанному id. Если id Некорректный, то победитель выбирается рандомно
    {
        Debug.Log("ChooseParticularWinner");
        if (Game.Model.WheelModel.SectorsInfo.ContainsKey(Game.Model.WheelModel.ReceivedWinnerId))
        {
            Game.Model.WheelModel.ActualWinnerId = Game.Model.WheelModel.ReceivedWinnerId;
        }
        else
        {
            Debug.Log("Некорректный id победителя, победитель будет выбран рандомно");
            ChooseRandomWinner();
        }
    }
}
