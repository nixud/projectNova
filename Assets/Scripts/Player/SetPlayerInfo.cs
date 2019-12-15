using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetPlayerInfo : MonoBehaviour
{
    public GameObject CommanderName;
    public GameObject CommanderNameInputField;
    public GameObject CommanderPic;

    public GameObject CommanderGroupName;
    public GameObject CommanderGroupInputField;
    public GameObject CommanderGroupPic;

    public GameObject Ship;
    public GameObject ShipInformation;
    public GameObject ShipPic;

    private ImageLoader PicImageLoader;
    private ImageLoader GroupPicImageLoader;
    private ImageLoader ShipPicImageLoader;

    private int PicNumber1 = 1;
    private int PicNumber2 = 1;
    private int PicNumber3 = 1;

    private void Start()
    {
        PicImageLoader = new ImageLoader("CommanderPic");
        GroupPicImageLoader = new ImageLoader("CommanderGroupPic");
        ShipPicImageLoader = new ImageLoader("ShipPic");

        CommanderPic.GetComponent<Image>().sprite = PicImageLoader.GetPic(PicNumber1);
        CommanderGroupPic.GetComponent<Image>().sprite = GroupPicImageLoader.GetPic(PicNumber2);
        ShipPic.GetComponent<Image>().sprite = ShipPicImageLoader.GetPic(PicNumber3);
    }

    public void CommanderPicChange(int ChangeNum) {
        if (PicNumber1 + ChangeNum > 0)
        {
            if (PicImageLoader.GetNum() >= PicNumber1 + ChangeNum) {
                PicNumber1 += ChangeNum;
                CommanderPic.GetComponent<Image>().sprite = PicImageLoader.GetPic(PicNumber1);
            }
        }
    }
    public void CommanderGroupPicChange(int ChangeNum)
    {
        if (PicNumber2 + ChangeNum > 0)
        {
            if (GroupPicImageLoader.GetNum() >= PicNumber2 + ChangeNum)
            {
                PicNumber2 += ChangeNum;
                CommanderGroupPic.GetComponent<Image>().sprite = GroupPicImageLoader.GetPic(PicNumber2);
            }
        }
    }
    public void ShipPicChange(int ChangeNum)
    {
        if (PicNumber3 + ChangeNum > 0)
        {
            if (ShipPicImageLoader.GetNum() >= PicNumber3 + ChangeNum)
            {
                PicNumber3 += ChangeNum;
                ShipPic.GetComponent<Image>().sprite = ShipPicImageLoader.GetPic(PicNumber3);
            }
        }
    }
}
