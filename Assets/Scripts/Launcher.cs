using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Launcher : MonoBehaviour
{
    [SerializeField] private Button _launchButton;
    [SerializeField] private TextMeshProUGUI _buttonText;
    [SerializeField] private List<Indicator> _indicators;

    private int _numberIndicator = 0;

    private bool _isLaunch = false;
    private int _seconds;

    private void Start()
    {
        foreach (var i in _indicators)
        {
            i.OnComplete += CoroutineComplete;
            i.ResetCoroutine();
        }
        ResetAll();
        _launchButton.onClick.AddListener(Clicked);
    }

    private void Clicked()
    {
        if (_isLaunch)
            ResetAll();
        else
            Launch();
    }

    private void ResetAll()
    {
        _isLaunch = false;
        _buttonText.text = "Start";
        _numberIndicator = 0;
        foreach (var i in _indicators)
        {
            i.ResetCoroutine();
        }
    }
    private void Launch()
    {
        _isLaunch = true;
        _seconds = Random.Range(10, 21);
        _buttonText.text = "Stop";

        LaunchCurrentIndicator();
    }

    private void CoroutineComplete()
    {
        _numberIndicator++;
        if (_numberIndicator > _indicators.Count - 1)
            _numberIndicator = 0;

        LaunchCurrentIndicator();
    }

    private void LaunchCurrentIndicator()
    {
        _indicators[_numberIndicator].Launch(_seconds);

        foreach (var i in _indicators)
        {
            if (i == _indicators[_numberIndicator])
                continue;
            else
                i.MakeInactive();
        }
    }
}
