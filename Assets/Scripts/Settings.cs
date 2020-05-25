using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 全局设置
public class Settings
{
    #region 单例
    
    private static Settings _settings;

    private Settings()
    {
    }

    public Settings Instance
    {
        get
        {
            if (_settings == null)
                _settings = new Settings();
            return _settings;
        }
    }
    
    #endregion // 单例

    public int MainVolume;   // 主音量
    public int MusicVolume;  // 音乐音量
    public int SoundVolume;  // 音效音量

    public bool AutoFire;    // 自动开火
    public bool LeftHanded; // 惯用左手
}
