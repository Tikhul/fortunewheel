using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SectorView : FortuneWheelElement
{
    private int sectorId;
    private bool isHighlighted = false;
    private float transparent = 0.3f;
    private float highlighted = 1.0f;
    private float fadeTime = 1.5f;
    
    [SerializeField] private TMP_Text _idText;


    public TMP_Text IdText
    {
        get => _idText;
        set => _idText = value;
    }

    public void SetUpSector(Image newSector, float zRotation, int index, float rotationStep)
    {
        sectorId = Game.Model.WheelModel.Sectors[index].Id;
        
        SetTransform(newSector, zRotation);
        SetColor(newSector, index);
        SetText(rotationStep);
    }

    private void SetTransform(Image newSector, float zRotation)
// Настройка расположения сектора
    {
        newSector.transform.SetParent(Game.Model.WheelModel.SectorsParent.transform);
        newSector.transform.localScale = new Vector3(1, 1, 1);
        newSector.transform.localPosition = newSector.transform.position;
        newSector.transform.localRotation = Quaternion.Euler(0, 0, zRotation);
    }
    private void SetColor(Image newSector, int index)
// Настройка цвета и спрайта
    {
        newSector.sprite = Game.Model.WheelModel.Sectors[index].SourceImage;
        Color tmp = newSector.color;
        tmp.a = transparent;
        newSector.color = tmp;
        newSector.fillAmount = (float)Math.Round((double)1 / Game.Model.WheelModel.Sectors.Count, 3);
    }
    private void SetText(float rotationStep)
// Настройка текста внутри сектора
    {
        Vector3 pos = new Vector3();
        pos.x = Game.Model.WheelModel.WheelCenter.transform.localPosition.x - Game.Model.WheelModel.Radius * Mathf.Sin(rotationStep * Mathf.Deg2Rad / 2);
        pos.y = Game.Model.WheelModel.WheelCenter.transform.localPosition.y - Game.Model.WheelModel.Radius * Mathf.Cos(rotationStep * Mathf.Deg2Rad / 2);
        pos.z = Game.Model.WheelModel.WheelCenter.transform.localPosition.z;
        IdText.transform.localPosition = pos;
        IdText.transform.localRotation = Quaternion.Euler(0, 0, -rotationStep * 0.5f);
        IdText.text = sectorId.ToString();
    }

    public void HighlightSector(int receivedId)
// Подсветить сектор
    {
        if (receivedId.Equals(sectorId))
        {
            Tween fading = GetComponent<Image>().DOFade(highlighted, fadeTime);
            isHighlighted = true;
            fading.OnComplete(Game.View.StartButtonView.ActivateButton);
        }
    }
    public void TurnOffSector()
 // Убрать подсветку сектора
    {
        if (isHighlighted)
        {
            GetComponent<Image>().DOFade(transparent, fadeTime);
            isHighlighted = true;
        }
    }
}
