using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class EitherOrBoxWithTitleImage
{
    private static readonly string prefabPath;
    private static GameObject box = null;

    public static event Action OnTruePressed;
    public static event Action OnFalsePressed;
    
    public static void ShowBox(Vector2 position, Vector2 scale, string iconPath, string title, string description, bool useImageButton)
    {
        CreateBox(position, scale);
        box.GetComponent<EitherOrBoxWithTitleImageControl>().SetInfo(
            Resources.Load<Sprite>(iconPath),
            title,
            description,
            useImageButton);
        box.SetActive(true);
    }
    
    public static void ShowBox(Vector2 position, Vector2 scale, string iconPath, string title, string description, string trueButtonText, string falseButtonText)
    {
        CreateBox(position, scale);
        box.GetComponent<EitherOrBoxWithTitleImageControl>().SetInfo(
            Resources.Load<Sprite>(iconPath),
            title,
            description,
            trueButtonText,
            falseButtonText);
        box.SetActive(true);
    }

    private static void CreateBox(Vector2 position, Vector2 scale)
    {
        if (box == null)
        {
            var temp = Resources.Load<GameObject>(prefabPath);
            box = GameObject.Instantiate(temp);
            box.SetActive(false);
        }
        box.transform.position = position;
        box.transform.localScale = scale;
    }
    
    public static void TruePressed()
    {
        OnTruePressed?.Invoke();
    }

    public static void FalsePressed()
    {
        OnFalsePressed?.Invoke();
    }
}
