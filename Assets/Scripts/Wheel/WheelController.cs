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
//     Debug.Log(id.ToString() + " " + initZ.ToString() + " " + finalZ.ToString());
    }

    public void CollectProbabilityInfo(int sectorProbability)
// Сбор всех чисел вероятности
    {
        Game.Model.WheelModel.ProbabilitySum += sectorProbability;
    }

    public void CollectProbabilityRanges(int sectorProbability)
// Собираю отрезки вероятности выпадения
    {
        if (sectorProbability > 0)
        {
            if (!Game.Model.WheelModel.ProbabilityRanges.Any())
            {
                List<int> firstList = new List<int>();
                firstList.Add(0);
                firstList.Add(sectorProbability);
                Game.Model.WheelModel.ProbabilityRanges.Add(firstList);
                Debug.Log(firstList[0].ToString() + firstList[1].ToString());
            }
            else
            {
                List<int> tempList = new List<int>();
                tempList.Add(Game.Model.WheelModel.ProbabilityRanges.Last()[1] + 1);
                tempList.Add(Game.Model.WheelModel.ProbabilityRanges.Last()[1] + sectorProbability);
                Game.Model.WheelModel.ProbabilityRanges.Add(tempList);
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
        foreach (SectorSO sector in Game.Model.WheelModel.Sectors)
        {
            for(int i = 1; i <= sector.Probability; i++)
            {
                if (i == randWinner)
                {
                    Game.Model.WheelModel.ActualWinnerId = sector.Id;
                    break;
                }
            }
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
