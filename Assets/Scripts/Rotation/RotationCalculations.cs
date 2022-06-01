using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCalculations : FortuneWheelElement
{
    public float Acceleration()
// –асчет ускорени€ от нул€ до максимальной скорости
    {
        return Game.Model.RotationModel.MaxSpeed / Game.Model.RotationModel.TimeToMax;
    }
    
    public float RotationToMax()
// –асчет градусов поворота от нул€ до максимальной скорости
    {
        return (Acceleration() * Game.Model.RotationModel.TimeToMax * Game.Model.RotationModel.TimeToMax) / 2;
    }
    
    public float RotationAtMax()
 // –асчет градусов поворота на максимальной скорости
    {
        return Game.Model.RotationModel.MaxSpeed * Game.Model.RotationModel.TimeAtMax;
    }

    public float Deceleration()
 // –асчет замедлени€ от максимальной скорости до нул€
    {
        return Game.Model.RotationModel.MaxSpeed / Game.Model.RotationModel.TimeAfterMax;
    }
    
    public float TotalRotationToStop()
 // –асчет общих градусов поворота до остановки
    {
        return (float)Math.Floor((Deceleration() * Game.Model.RotationModel.TimeAfterMax * Game.Model.RotationModel.TimeAfterMax) / 2);
    }
    // –асчет градусов поворота на 360 после замедлени€

    public float FinalFullRotations()
    {
        return TotalRotationToStop() - (TotalRotationToStop() % 360);
    }

    
    public float FinalFullTime()
 // –асчет времени вращени€ на 360 после замедлени€
    {
        return (float)(Game.Model.RotationModel.TimeAfterMax - Math.Sqrt(2 * TotalRotationToStop() / Deceleration()));
    }

    public float FinalExtraTime()
// –асчет времени поворота, оставшегос€ до победител€
    {
        return Game.Model.RotationModel.TimeAfterMax - FinalFullTime();
    }
    public float RotationToWinner()
// –асчет поворота, оставшегос€ до победител€
    {
        List<float> degrees = Game.Model.WheelModel.SectorsInfo[Game.Model.WheelModel.ActualWinnerId];
        float maxDegree = (float)(degrees[1] - 180);
        float minDegree = (float)(degrees[0] - 180);
        return UnityEngine.Random.Range(degrees[0], degrees[1]);
    }
}
