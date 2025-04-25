using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Application
{
    public static class SaveManager
    {
        private static readonly Dictionary<TypeSkinItems, string> skinKeyMap = new()
    {
        { TypeSkinItems.BrushRed, GlobalConst.SkinFirst },
        { TypeSkinItems.BrushGreen, GlobalConst.SkinSecond },
        { TypeSkinItems.BrushDarkBlue, GlobalConst.SkinThird },
        { TypeSkinItems.BrushPurple, GlobalConst.SkinFourth },
        { TypeSkinItems.BrushYellow, GlobalConst.SkinFifth },
        { TypeSkinItems.BrushBlue, GlobalConst.SkinSixth },
        { TypeSkinItems.BrushLightPurple, GlobalConst.SkinSeventh },
        { TypeSkinItems.BigBrushRed, GlobalConst.SkinEighth },
        { TypeSkinItems.BigBrushGreen, GlobalConst.SkinNineth },
        { TypeSkinItems.BigBrushDarkBlue, GlobalConst.SkinTenth },
        { TypeSkinItems.BigBrushPurple, GlobalConst.SkinEleventh },
        { TypeSkinItems.BigBrushYellow, GlobalConst.SkinTwelveth }
    };

        public static void Save(string key, string saveData)
        {
            PlayerPrefs.SetString(key, saveData);
        }
        public static void Save(TypeSkinItems saveData)
        {
            if (skinKeyMap.TryGetValue(saveData, out string key))
            {
                PlayerPrefs.SetString(key, saveData.ToString());
                PlayerPrefs.Save();
            }
        }
        public static void Save(string key, bool saveData)
        {
            if (saveData)
            {
                PlayerPrefs.SetInt(key, 1);
            }
            else
            {
                PlayerPrefs.SetInt(key, 0);
            }
        }
        public static void Save(string key, int saveData)
        {
            PlayerPrefs.SetInt(key, saveData);
        }
        public static int LoadInt(string key)
        {
            if (PlayerPrefs.HasKey(key))
            {
                var loadedString = PlayerPrefs.GetInt(key);
                return loadedString;
            }

            return 0;
        }
        public static string LoadString(string key)
        {
            if (PlayerPrefs.HasKey(key))
            {
                string loadedString = PlayerPrefs.GetString(key);
                return loadedString;
            }

            return null;
        }
        public static string LoadString(string key, string value)
        {
            if (PlayerPrefs.HasKey(key))
            {
                string loadedString = PlayerPrefs.GetString(key, value);
                return loadedString;
            }

            return null;
        }
        public static bool LoadBool(string key)
        {
            if (PlayerPrefs.HasKey(key))
            {
                var loadedint = PlayerPrefs.GetInt(key);
                if (loadedint == 1)
                    return true;
                else
                    return false;
            }

            return false;
        }
        public static void ResetAll()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}