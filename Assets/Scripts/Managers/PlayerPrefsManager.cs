using UnityEngine;

public class PlayerPrefsManager : MonoSingleton<PlayerPrefsManager>
{
    protected override void Awake()
    {
        base.Awake();
        
        DontDestroyOnLoad(gameObject);
    }

    public void SaveInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }

    public void SaveFloat(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
    }

    public void SaveString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }

    public void SaveBool(string key, bool value)
    {
        PlayerPrefs.SetInt(key, value? 1 : 0);
    }

    public bool LoadInt(string key, out int value)
    {
        if (!PlayerPrefs.HasKey(key))
        {
            value = 0;
            return false;
        }
        
        value = PlayerPrefs.GetInt(key);
        return true;
    }
    
    public bool LoadFloat(string key, out float value)
    {
        if (!PlayerPrefs.HasKey(key))
        {
            value = 0.0f;
            return false;
        }
        
        value = PlayerPrefs.GetFloat(key);
        return true;
    }
    
    public bool LoadString(string key, out string value)
    {
        if (!PlayerPrefs.HasKey(key))
        {
            value = "";
            return false;
        }
        
        value = PlayerPrefs.GetString(key);
        return true;
    }
    
    public bool LoadBool(string key, out bool value)
    {
        if (!PlayerPrefs.HasKey(key))
        {
            value = false;
            return false;
        }
        
        value = PlayerPrefs.GetInt(key) != 0;
        return true;
    }
}
