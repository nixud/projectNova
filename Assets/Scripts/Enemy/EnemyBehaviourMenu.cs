#if UNITY_EDITOR

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Accessibility;

public class EnemyBehaviourMenu : EditorWindow
{
    private static List<EnemyBehaviourContainer> _enemyBehaviourContainers = new List<EnemyBehaviourContainer>();

    // Show Container
    private static EnemyBehaviourContainer _enemyBehaviourContainer = new EnemyBehaviourContainer();
    private static string[] _containersNumber;
    private static int[] _containerIndex;
    private static int _containerPos = 0;
    private static int _containerPosTemp = 0;
    private static bool _shouldChangeContainer = true;
    
    // Show baseBehList
    private static List<BaseBehaviourContainer> _baseBehaviourContainers = new List<BaseBehaviourContainer>();
    private static string[] _baseBehListCount;
    private static int[] _baseBehListIndex;
    private static int _baseBehListPos = 0;
    private static int _baseBehListPosTemp = 0;
    private static bool _shouldChangeBaseBehList = true;
    
    // Show baseBeh
    private static BaseBehaviourContainer _baseBehaviourContainer;
    private static string[] _basebehNames;
    private static int[] _baseBehindex;
    private static int _baseBehPos = 0;
    private static int _baseBehPosTemp = 0;
    private static bool _shouldChangeBaseBeh = true;

    [MenuItem("Nova/EnemyBehaviourMenu", false, 0)]
    private static void Init()
    {
        EnemyBehaviourMenu menu = GetWindow<EnemyBehaviourMenu>();
        // init
        LoadData();
        RefreshBehContainer();
        RefreshBaseBehList();
        RefreshBaseBeh();
        menu.Show();
    }

    private void OnGUI()
    {


        // Show container
        GUILayout.Label("行为列表");
        _containerPos = EditorGUILayout.IntPopup(_containerPos, _containersNumber, _containerIndex);
        
        GUILayout.Label("行为信息");
        _enemyBehaviourContainer.number = EditorGUILayout.TextField("Number", _enemyBehaviourContainer.number);
        _baseBehListPos = EditorGUILayout.IntPopup("行为链", _baseBehListPos, _baseBehListCount, _baseBehListIndex);
        
        DrawBaseBehList();
        DrawBaseBeh();

        if (GUILayout.Button("添加基础行为"))
        {
            AddBaseBeh();
            Refresh(false, false, false);
        }

        if (GUILayout.Button("删除基础行为"))
        {
            DeleteBaseBeh();
            Refresh(false, false, true);
        }
        
        if (GUILayout.Button("删除行为链"))
        {
            DeleteBaseBehList();
            Refresh(false, true, true);
        }

        if (GUILayout.Button("添加行为链"))
        {
            AddBaseBehList();
            Refresh(false, false, true);
        }

        if (GUILayout.Button("添加新行为"))
        {
            AddEnemyBehaviourContainer();
            Refresh(false, true, true);
        }

        if (GUILayout.Button("删除行为"))
        {
            DeleteBeh();
        }
        
        if (GUILayout.Button("保存"))
        {
            SaveData();
            Refresh(false, false, false, true);
        }
    }

    private void Update()
    {
        if (_containerPosTemp != _containerPos || _shouldChangeContainer)
        {
            _containerPosTemp = _containerPos;
            _enemyBehaviourContainer = _enemyBehaviourContainers[_containerPos];
            RefreshBaseBehList(false);
            _shouldChangeContainer = false;
        }

        if (_baseBehListPosTemp != _baseBehListPos || _shouldChangeBaseBehList)
        {
            _baseBehListPosTemp = _baseBehListPos;
            _baseBehaviourContainers = _enemyBehaviourContainer.behaviourGroup[_baseBehListPos];
            RefreshBaseBeh(false);
            _shouldChangeBaseBehList = false;
        }

        if (_baseBehPosTemp != _baseBehPos || _shouldChangeBaseBeh)
        {
            _baseBehPosTemp = _baseBehPos;
            _baseBehaviourContainer = _baseBehaviourContainers[_baseBehPos];
            _shouldChangeBaseBeh = false;
        }
    }

    private static void DeleteBeh()
    {
        _enemyBehaviourContainers.Remove(_enemyBehaviourContainer);
        Refresh(true, true, true);
    }

    private static void AddEnemyBehaviourContainer()
    {
        _enemyBehaviourContainers.Add(new EnemyBehaviourContainer());
        _enemyBehaviourContainer = _enemyBehaviourContainers[_enemyBehaviourContainers.Count - 1];
        _containerPos = _enemyBehaviourContainers.Count - 1;
        AddBaseBehList();
    }
    
    private static void AddBaseBeh()
    {
        _baseBehaviourContainers.Add(new BaseBehaviourContainer());
        _baseBehPos = _baseBehaviourContainers.Count - 1;
    }

    private static void DeleteBaseBeh()
    {
        _baseBehaviourContainers.Remove(_baseBehaviourContainer);
    }

    private static void AddBaseBehList()
    {
        _enemyBehaviourContainer.behaviourGroup.Add(new List<BaseBehaviourContainer>());
        _baseBehaviourContainers =
            _enemyBehaviourContainer.behaviourGroup[_enemyBehaviourContainer.behaviourGroup.Count - 1];
        _baseBehListPos = _enemyBehaviourContainer.behaviourGroup.Count - 1;
        AddBaseBeh();
    }
    
    private static void DeleteBaseBehList()
    {
        _enemyBehaviourContainer.behaviourGroup.Remove(_baseBehaviourContainers);
    }

    private static void DrawBaseBeh()
    {
        GUILayout.BeginVertical("Box");
        _baseBehaviourContainer.Type = (BehaviourEnum) EditorGUILayout.EnumPopup("行为", _baseBehaviourContainer.Type);

        _baseBehaviourContainer.Time = EditorGUILayout.FloatField("Time", _baseBehaviourContainer.Time);

        if (_baseBehaviourContainer.Type == BehaviourEnum.Kamikaze ||
            _baseBehaviourContainer.Type == BehaviourEnum.Move ||
            _baseBehaviourContainer.Type == BehaviourEnum.MoveForward || 
            _baseBehaviourContainer.Type == BehaviourEnum.Track)
            _baseBehaviourContainer.Speed = EditorGUILayout.FloatField("NowSpeed", _baseBehaviourContainer.Speed);

        if (_baseBehaviourContainer.Type == BehaviourEnum.Move ||
            _baseBehaviourContainer.Type == BehaviourEnum.MoveForward)
        {
            _baseBehaviourContainer.Vector1.x = EditorGUILayout.FloatField("x", _baseBehaviourContainer.Vector1.x);
            _baseBehaviourContainer.Vector1.y = EditorGUILayout.FloatField("y", _baseBehaviourContainer.Vector1.y);
            _baseBehaviourContainer.Vector1.z = EditorGUILayout.FloatField("z", _baseBehaviourContainer.Vector1.z);
        }

        if (_baseBehaviourContainer.Type == BehaviourEnum.MoveBetween)
        {
            _baseBehaviourContainer.Speed = EditorGUILayout.FloatField("NowSpeed", _baseBehaviourContainer.Speed);
            GUILayout.Label("point1");
            _baseBehaviourContainer.Vector1.x = EditorGUILayout.FloatField("x", _baseBehaviourContainer.Vector1.x);
            _baseBehaviourContainer.Vector1.y = EditorGUILayout.FloatField("y", _baseBehaviourContainer.Vector1.y);
            _baseBehaviourContainer.Vector1.z = EditorGUILayout.FloatField("z", _baseBehaviourContainer.Vector1.z);
            GUILayout.Label("point2");
            _baseBehaviourContainer.vector2.x = EditorGUILayout.FloatField("x", _baseBehaviourContainer.vector2.x);
            _baseBehaviourContainer.vector2.y = EditorGUILayout.FloatField("y", _baseBehaviourContainer.vector2.y);
            _baseBehaviourContainer.vector2.z = EditorGUILayout.FloatField("z", _baseBehaviourContainer.vector2.z);
        }

        if (_baseBehaviourContainer.Type == BehaviourEnum.MoveToPoint || _baseBehaviourContainer.Type == BehaviourEnum.MoveForwardToPoint)
        {
            _baseBehaviourContainer.Speed = EditorGUILayout.FloatField("NowSpeed", _baseBehaviourContainer.Speed);
            GUILayout.Label("Target");
            _baseBehaviourContainer.Vector1.x = EditorGUILayout.FloatField("x", _baseBehaviourContainer.Vector1.x);
            _baseBehaviourContainer.Vector1.y = EditorGUILayout.FloatField("y", _baseBehaviourContainer.Vector1.y);
            _baseBehaviourContainer.Vector1.z = EditorGUILayout.FloatField("z", _baseBehaviourContainer.Vector1.z);
        }

        if (_baseBehaviourContainer.Type == BehaviourEnum.MoveForwardChangeVelocity)
        {
            _baseBehaviourContainer.Speed = EditorGUILayout.FloatField("StartSpeed", _baseBehaviourContainer.Speed);
            _baseBehaviourContainer.f_field1 = EditorGUILayout.FloatField("EndSpeed", _baseBehaviourContainer.f_field1);
            GUILayout.Label("Target");
            _baseBehaviourContainer.Vector1.x = EditorGUILayout.FloatField("x", _baseBehaviourContainer.Vector1.x);
            _baseBehaviourContainer.Vector1.y = EditorGUILayout.FloatField("y", _baseBehaviourContainer.Vector1.y);
            _baseBehaviourContainer.Vector1.z = EditorGUILayout.FloatField("z", _baseBehaviourContainer.Vector1.z);
        }


        GUILayout.EndVertical();
    }

    private static void DrawBaseBehList()
    {
        GUILayout.BeginVertical("Box");
        _baseBehPos = GUILayout.SelectionGrid(_baseBehPos, _basebehNames, 1);
        GUILayout.EndVertical();
    }

    private static void RefreshBaseBeh(bool guiling = false)
    {
        _basebehNames = new string[_baseBehaviourContainers.Count];
        _baseBehindex = new int[_baseBehaviourContainers.Count];

        for (int i = 0; i < _baseBehaviourContainers.Count; i++)
        {
            _basebehNames[i] = _baseBehaviourContainers[i].Type.ToString();
            _baseBehindex[i] = i;
        }
        
        if (guiling)
            _baseBehPos = 0;
        _shouldChangeBaseBeh = true;
    }
    
    private static void RefreshBaseBehList(bool guiling = false)
    {
        _baseBehListCount = new string[_enemyBehaviourContainer.behaviourGroup.Count];
        _baseBehListIndex = new int[_enemyBehaviourContainer.behaviourGroup.Count];

        for (int i = 0; i < _enemyBehaviourContainer.behaviourGroup.Count; i++)
        {
            _baseBehListCount[i] = i.ToString();
            _baseBehListIndex[i] = i;
        }

        _shouldChangeBaseBehList = true;
        if (guiling)
            _baseBehListPos = 0;
    }
    
    private static void RefreshBehContainer(bool guiling = false)
    {
        _containersNumber = new string[_enemyBehaviourContainers.Count];
        _containerIndex = new int[_enemyBehaviourContainers.Count];
        for (int i = 0; i < _enemyBehaviourContainers.Count; i++)
        {
            _containersNumber[i] = _enemyBehaviourContainers[i].number;
            _containerIndex[i] = i;
        }

        if (guiling)
            _containerPos = 0;
        _shouldChangeContainer = true;
    }

    private static void LoadData()
    {
        _enemyBehaviourContainers = EnemyBehaviourLoader.LoadBehaviours();
    }

    private static void SaveData()
    {
        EnemyBehaviourLoader.SaveBehaviour(_enemyBehaviourContainers);
    }
    
    private static void Refresh(bool b1, bool b2, bool b3, bool reload = false)
    {
        if (reload)
            LoadData();
        RefreshBehContainer(b1);
        RefreshBaseBehList(b2);
        RefreshBaseBeh(b3);
    }
}

#endif