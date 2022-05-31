using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SectorView : FortuneWheelElement
{
    [SerializeField] private TMP_Text _idText;

    public TMP_Text IdText
    {
        get => _idText;
        set => _idText = value;
    }

    public void SetText(string id)
    {
        IdText.text = id;
    }
}
