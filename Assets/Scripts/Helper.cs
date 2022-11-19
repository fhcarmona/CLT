using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
    public static float GetPrefByKeyName(string keyName)
    {
        if (PlayerPrefs.HasKey(keyName))
            return PlayerPrefs.GetFloat(keyName);

        return 0;
    }
}
