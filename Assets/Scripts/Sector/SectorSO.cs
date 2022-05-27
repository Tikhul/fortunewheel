using UnityEngine;

[CreateAssetMenu(fileName = "SectorScriptableObject", menuName = "ScriptableObjects/SectorSO", order = 1)]
public class SectorSO : ScriptableObject
{
    [SerializeField] private int _probability;
    [SerializeField] private Sprite _sourceImage;

    public Sprite SourceImage
    {
        get => _sourceImage;
        set => _sourceImage = value;
    }

    public int Probability
    {
        get => _probability;
        set => _probability = value;
    }
}

