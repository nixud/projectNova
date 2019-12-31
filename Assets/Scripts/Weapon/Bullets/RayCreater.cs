using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCreater : MonoBehaviour
{

    public float Damage;
    public float DamageTime;
    public Texture RayTexture;
    public float RayWidth = 0.01f;

    public GameObject ShootHitEffect;
    public GameObject HitEffect;

    //LineRenderer
    private LineRenderer lineRenderer;
    //定义一个Vector3,用来存储鼠标点击的位置
    private Vector3 position;
    //用来索引端点
    //private int index = 0;
    //端点数
    //private int LengthOfLineRenderer = 0;

    void Start()
    {
        //添加LineRenderer组件
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        //设置材质
        lineRenderer.material = new Material(Shader.Find("Unlit/PixelationShader"));
        lineRenderer.material.SetInt("_PixelSize", 16);

        Texture texture = Resources.Load("1", typeof(Texture)) as Texture;
        lineRenderer.material.SetTexture("_MainTex", RayTexture);
        //设置颜色
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.yellow;

        //设置宽度
        lineRenderer.startWidth = RayWidth;
        lineRenderer.endWidth = RayWidth;

    }
}
