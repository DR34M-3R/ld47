using UnityEngine;
using SimpleJSON;

public class Loc
{
    private static JSONNode data;
    private static bool inited;
    private static string lang;

    public static string Get(string key)
    {
        if (!inited)
        {
            Init();
        }
        return data[key][lang];
    }
    
    public static void Init()
    {
        lang = "ru";
        inited = true;
        var t = Resources.Load<TextAsset>("Localization/localization");
        data = JSON.Parse(t.text);
        data = data["data"];
    }
}