using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCalculations : FortuneWheelElement
{
    public float Acceleration()
// ������ ��������� �� ���� �� ������������ ��������
    {
        return Game.Model.WheelModel.RotationSO.MaxSpeed / Game.Model.WheelModel.RotationSO.TimeToMax;
    }
    
    public float RotationToMax()
// ������ �������� �������� �� ���� �� ������������ ��������
    {
        return (Acceleration() * Game.Model.WheelModel.RotationSO.TimeToMax * Game.Model.WheelModel.RotationSO.TimeToMax) / 2;
    }
    
    public float RotationAtMax()
 // ������ �������� �������� �� ������������ ��������
    {
        return Game.Model.WheelModel.RotationSO.MaxSpeed * Game.Model.WheelModel.RotationSO.TimeAtMax;
    }

    public float Deceleration()
 // ������ ���������� �� ������������ �������� �� ����
    {
        return Game.Model.WheelModel.RotationSO.MaxSpeed / Game.Model.WheelModel.RotationSO.TimeAfterMax;
    }
    
    public float TotalRotationToStop()
 // ������ ����� �������� �������� �� ���������
    {
        return (float)Math.Floor((Deceleration() * Game.Model.WheelModel.RotationSO.TimeAfterMax * Game.Model.WheelModel.RotationSO.TimeAfterMax) / 2);
    }
    // ������ �������� �������� �� 360 ����� ����������

    public float FinalFullRotations()
    {
        return TotalRotationToStop() - (TotalRotationToStop() % 360);
    }

    
    public float FinalFullTime()
 // ������ ������� �������� �� 360 ����� ����������
    {
        float coef = FinalFullRotations() / RotationToWinner();
        return (float)(Game.Model.WheelModel.RotationSO.TimeAfterMax) * coef;
    }

    public float FinalExtraTime()
// ������ ������� ��������, ����������� �� ����������
    {
        return Game.Model.WheelModel.RotationSO.TimeAfterMax - FinalFullTime();
    }
    public float RotationToWinner()
// ������ ��������, ����������� �� ����������
    {
        List<float> degrees = Game.Model.WheelModel.SectorsInfo[Game.Model.WheelModel.ActualWinnerId];
        return UnityEngine.Random.Range(degrees[1], degrees[0]);
    }
}
