using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCalculations : FortuneWheelElement
{
    public float Acceleration()
// –асчет ускорени€ от нул€ до максимальной скорости
    {
        return Game.Model.WheelModel.RotationSO.MaxSpeed / Game.Model.WheelModel.RotationSO.TimeToMax;
    }
    
    public float RotationToMax()
// –асчет градусов поворота от нул€ до максимальной скорости
    {
        return (Acceleration() * Game.Model.WheelModel.RotationSO.TimeToMax * Game.Model.WheelModel.RotationSO.TimeToMax) / 2;
    }
    
    public float RotationAtMax()
 // –асчет градусов поворота на максимальной скорости
    {
        return Game.Model.WheelModel.RotationSO.MaxSpeed * Game.Model.WheelModel.RotationSO.TimeAtMax;
    }

    public float Deceleration()
 // –асчет замедлени€ от максимальной скорости до нул€
    {
        return Game.Model.WheelModel.RotationSO.MaxSpeed / Game.Model.WheelModel.RotationSO.TimeAfterMax;
    }
    
    public float TotalRotationToStop()
 // –асчет общих градусов поворота до остановки
    {
        return (float)Math.Floor((Deceleration() * Game.Model.WheelModel.RotationSO.TimeAfterMax * Game.Model.WheelModel.RotationSO.TimeAfterMax) / 2);
    }
    // –асчет градусов поворота на 360 после замедлени€

    public float FinalFullRotations()
    {
        return TotalRotationToStop() - (TotalRotationToStop() % 360);
    }

    
    public float FinalFullTime()
 // –асчет времени вращени€ на 360 после замедлени€
    {
        float coef = FinalFullRotations() / RotationToWinner();
        return (float)(Game.Model.WheelModel.RotationSO.TimeAfterMax) * coef;
    }

    public float FinalExtraTime()
// –асчет времени поворота, оставшегос€ до победител€
    {
        return Game.Model.WheelModel.RotationSO.TimeAfterMax - FinalFullTime();
    }
    public float RotationToWinner()
// –асчет поворота, оставшегос€ до победител€
    {
        List<float> degrees = Game.Model.WheelModel.SectorsInfo[Game.Model.WheelModel.ActualWinnerId];
        return UnityEngine.Random.Range(degrees[1], degrees[0]);
    }
}
