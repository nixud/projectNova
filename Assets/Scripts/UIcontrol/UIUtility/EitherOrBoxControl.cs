using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EitherOrBoxControl : MonoBehaviour
{
    public Text text;
    public event Action OnTruePressed;
    public event Action OnFalsePressed;

    // 同时仅允许一个对象占用
    private GameObject _user;

    private void Start()
    {
        gameObject.SetActive(false);
        OnTruePressed = null;
        OnFalsePressed = null;
        _user = null;
    }

    public void SetInfo(GameObject user, string text, Action onTrue=null, Action onFalse = null)
    {
        if (_user != null)
            return;
        _user = user;
        this.text.text = text;
        OnTruePressed += onTrue;
        OnFalsePressed += onFalse;
    }

    public bool Show(GameObject from)
    {
        if (from != _user)
            return false;
        gameObject.SetActive(true);
        return true;
    }

    public bool Hide(GameObject from)
    {
        if (from != gameObject)
            return false;
        gameObject.SetActive(false);
        _user = null;
        return true;
    }

    public void TruePressed()
    {
        OnTruePressed?.Invoke();
        OnFalsePressed = null;
        OnTruePressed = null;
        Hide(gameObject);
    }

    public void FalsePressed()
    {
        OnFalsePressed?.Invoke();
        OnFalsePressed = null;
        OnTruePressed = null;
        Hide(gameObject);
    }
}
