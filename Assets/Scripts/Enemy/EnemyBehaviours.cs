using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehaviours
 {
     public GameObject gameObject;
     public float time = 0;
 
     public abstract Vector3 Calculate(float Dt);
     public abstract void Start();
     public abstract void Run(float Dt);
 
     public abstract void End();
 }