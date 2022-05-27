using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortuneWheelElement : MonoBehaviour
{
    private FortuneWheelGame _game;
    public FortuneWheelGame Game
    {
        get
        {
            if (_game == null) _game = FindObjectOfType<FortuneWheelGame>();
            return _game;
        }
    }
}

public class FortuneWheelGame : MonoBehaviour
{
    [SerializeField] private FortuneWheelModel _model;
    [SerializeField] private FortuneWheelController _controller;
    [SerializeField] private FortuneWheelView _view;

    public FortuneWheelModel Model
    {
        get => _model;
    }
    public FortuneWheelController Controller
    {
        get => _controller;
    }
    public FortuneWheelView View
    {
        get => _view;
    }
}
