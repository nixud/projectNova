using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Plugin : IComparable
{
    public string Number;
    public string Description;
    public string Name;
    public string PicPath;
    public int Price;

    public RareLevel rareLevel;

    public int CompareTo(object obj)
    {
        Plugin p = obj as Plugin;
        return this.Number.CompareTo(p.Number);
    }
}
