using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Indicator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textField;
    [SerializeField] private Image _ingicator;

    private Color _inactiveColor = Color.yellow;
    private Color _awaitColor = Color.red;
    private Color _runColor = Color.green;

    private int _seconds;
    public UnityAction OnComplete;
    private IEnumerator _coroutine;
    
    private IEnumerator TimerLaunch()
    {
        _ingicator.color = _runColor;
        float timer = _seconds;
        while (timer >= 0)
        {
            timer -= Time.deltaTime;
            _textField.text = Math.Clamp(Math.Floor(timer), 0, _seconds).ToString();
            yield return null;
        }

        OnComplete?.Invoke();
    }

    public void Launch(int seconds)
    {
        _seconds = seconds;
        _coroutine = TimerLaunch();
        StartCoroutine(_coroutine);
    }

    public void MakeInactive()
    {
        _ingicator.color = _awaitColor;
    }

    public void ResetCoroutine()
    {
        if(_coroutine != null) StopCoroutine(_coroutine);
        _ingicator.color = _inactiveColor;
        _textField.text = 0.ToString();
    }
}
