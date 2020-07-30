using System;
using UnityEngine;
using UnityEngine.UI;

// 直接通过静态类 EitherOrBoxWithTitleImage 使用
public class EitherOrBoxWithTitleImageControl : MonoBehaviour
{
    public GameObject box;
    
    public Image itemIcon;
    public Text title;
    public Text description;

    public Image trueButtonImage;
    public Image falseButtonImage;
    public Text trueButtonText;
    public Text falseButtonText;

    private static readonly string _trueButtonDefaultString = "确定";
    private static readonly string _falseButtonDefaultString = "取消";

    public void SetInfo(Sprite icon, string title, string description, bool useImageButton)
    {
        if (useImageButton)
        {
            itemIcon.sprite = icon;
            this.title.text = title;
            this.description.text = description;
            trueButtonText.gameObject.SetActive(false);
            falseButtonText.gameObject.SetActive(false);
            trueButtonImage.gameObject.SetActive(true);
            falseButtonImage.gameObject.SetActive(true);
        }
        else
        {
            SetInfo(icon, title, description, _trueButtonDefaultString, _falseButtonDefaultString);
        }
    }

    public void SetInfo(Sprite icon, string title, string description, string trueButtonText, string falseButtonText)
    {
        itemIcon.sprite = icon;
        this.title.text = title;
        this.description.text = description;
        
        this.trueButtonText.gameObject.SetActive(true);
        this.falseButtonText.gameObject.SetActive(true);
        this.trueButtonText.text = trueButtonText;
        this.falseButtonText.text = falseButtonText;
        this.trueButtonImage.gameObject.SetActive(false);
        this.falseButtonImage.gameObject.SetActive(false);
    }
    
    public void OnTruePressed()
    {
        EitherOrBoxWithTitleImage.TruePressed();
    }

    public void OnFalsePressed()
    {
        EitherOrBoxWithTitleImage.FalsePressed();
    }
}
