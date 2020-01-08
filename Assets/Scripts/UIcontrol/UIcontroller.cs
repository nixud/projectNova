using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIcontroller : MonoBehaviour
{
    public List<GameObject> bloods;
    int bloodsNumMax;
    int bloodsNow;
    int ArmorNum = 0;

    public Sprite FullBlood;
    public Sprite EmptyBlood;
    public Sprite HalfBlood;
    public Sprite Armor;

    public GameObject Bloodimage;

    public void Init(int bloodsNum) {
        bloodsNumMax = bloodsNum;
        bloodsNow = bloodsNumMax * 2;
        if (bloodsNum <= bloods.Count)
        {
            for (int i = 0; i < bloodsNum; i++)
            {
                bloods[i].GetComponent<Image>().sprite = FullBlood;
            }
            for (int i = bloodsNum; i < bloods.Count; i++)
                bloods[i].SetActive(false);
        }
        else {
            for (int i = 0; i < bloods.Count; i++)
            {
                bloods[i].GetComponent<Image>().sprite = FullBlood;
            }
        }
    }

    public void AddArmor() {
        ArmorNum++;
        if (bloodsNumMax + ArmorNum <= bloods.Count)
        {
            for (int i = bloodsNumMax; i < bloodsNumMax + ArmorNum; i++)
            {
                bloods[i].SetActive(true);
                bloods[i].GetComponent<Image>().sprite = Armor;
            }
        }

    }

    public void DestoryArmor() {
        if (ArmorNum > 0) ArmorNum--;
        if(bloodsNumMax + ArmorNum<= bloods.Count)
            bloods[bloodsNumMax + ArmorNum].SetActive(false);
    }

    public void DestroyAllArmor()
    {
        ArmorNum = 0;
        if (bloodsNumMax <= bloods.Count)
            for (int i = bloodsNumMax; i < bloods.Count; i++) {
                bloods[i].SetActive(false);
            }
    }

    public void FreshBlood(){
        if (bloodsNumMax <= bloods.Count && bloodsNow <= bloods.Count)
        {
            for (int i = 0; i < (bloodsNow / 2); i++)
            {
                bloods[i].GetComponent<Image>().sprite = FullBlood;
            }
            for (int i = bloodsNow / 2; i < bloodsNumMax; i++)
                bloods[i].GetComponent<Image>().sprite = EmptyBlood;
            if (bloodsNow % 2 == 1) bloods[bloodsNow / 2].GetComponent<Image>().sprite = HalfBlood;
        }
        else {
            for (int i = 0; i < (bloodsNow / 2); i++)
            {
                bloods[i].GetComponent<Image>().sprite = FullBlood;
            }
            for (int i = bloodsNow / 2; i < bloods.Count; i++)
                bloods[i].GetComponent<Image>().sprite = EmptyBlood;
            if (bloodsNow % 2 == 1) bloods[bloodsNow / 2].GetComponent<Image>().sprite = HalfBlood;
        }
    }

    public static UIcontroller operator -- (UIcontroller uIcontroller) {
        if (uIcontroller.ArmorNum > 0)
        {
            uIcontroller.ArmorNum--;
            uIcontroller.DestoryArmor();
        }
        else
        {
            uIcontroller.bloodsNow--;
            uIcontroller.FreshBlood();
        }
        /*
        for (int i = uIcontroller.bloodsNow; i < uIcontroller.bloodsNumMax; i++)
        {
            uIcontroller.bloods[i].GetComponent<Image>().sprite = uIcontroller.EmptyBlood;
        }*/
        return uIcontroller;
    }
    public static UIcontroller operator ++ (UIcontroller uIcontroller)
    {
        uIcontroller.bloodsNow++;
        uIcontroller.FreshBlood();
        /*
        if (uIcontroller.bloodsNow > uIcontroller.bloodsNumMax) uIcontroller.bloodsNow = uIcontroller.bloodsNumMax;

        uIcontroller.bloods[uIcontroller.bloodsNow - 1].GetComponent<Image>().sprite = uIcontroller.FullBlood;
        */
        return uIcontroller;
    }
}
