using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavioursMove
{
    public float GetCurve(string Mode,float t) {
        if (Mode == "Line")
            return Line();
        else if (Mode == "Sin")
            return Sin(t);
        else if (Mode == "Sin2x")
            return Sin2x(t);
        else if (Mode == "Cos")
            return Cos(t);
        else if (Mode == "Cos2x")
            return Cos2x(t);
        return 0;
    }

    private float Line() {
        return 1;
    }
    private float Sin(float t)
    {
        return 1;
    }
    private float Cos(float t)
    {
        return 1;
    }
    private float Sin2x(float t)
    {
        return 1;
    }
    private float Cos2x(float t)
    {
        return 1;
    }
}
