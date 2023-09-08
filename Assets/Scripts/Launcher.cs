using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Launcher : MonoBehaviour
{
    [SerializeField] private Button _launchButton;
    [SerializeField] private TextMeshProUGUI _buttonText;
    [SerializeField] private TextMeshProUGUI _textField1;
    [SerializeField] private TextMeshProUGUI _textField2;
    [SerializeField] private Image _ingicator1;
    [SerializeField] private Image _ingicator2;

    private IEnumerator _currentCoroutine;

    private Color _inactiveColor = Color.yellow;
    private Color _awaitColor = Color.red;
    private Color _activeColor = Color.green;

    private bool _isLaunch = false;
    private int _seconds;

    private void Start()
    {
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

        if(_currentCoroutine != null) StopCoroutine(_currentCoroutine);

        _buttonText.text = "Start";
        _ingicator1.color = _inactiveColor;
        _ingicator2.color = _inactiveColor;

        _textField1.text = 0.ToString();
        _textField2.text = 0.ToString();
    }
    private void Launch()
    {
        _isLaunch = true;
        _seconds = Random.Range(10, 21);

        _ingicator1.color = _awaitColor;
        _ingicator2.color = _awaitColor;
        _buttonText.text = "Stop";

        _currentCoroutine = TimerOne();
        StartCoroutine(_currentCoroutine);
    }

    private IEnumerator TimerOne()
    {
        _ingicator1.color = _activeColor;
        float timer = _seconds;
        while(timer >= 0)
        {
            timer -= Time.deltaTime;
            _textField1.text = Math.Clamp(Math.Floor(timer), 0, _seconds).ToString();
            yield return null;
        }

        _ingicator1.color = _awaitColor;

        _currentCoroutine = TimerTwo();
        StartCoroutine(_currentCoroutine);
    }

    private IEnumerator TimerTwo()
    {
        _ingicator2.color = _activeColor;
        float timer = _seconds;
        while (timer >= 0)
        {
            timer -= Time.deltaTime;
            _textField2.text = Math.Clamp(Math.Floor(timer), 0, _seconds).ToString();
            yield return null;
        }

        _ingicator2.color = _awaitColor;

        _currentCoroutine = TimerOne();
        StartCoroutine(_currentCoroutine);
    }
}
