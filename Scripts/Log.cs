using UnityEngine;
using UnityEditor;

public class Log : MonoBehaviour
{
    static bool IS_DEBUG = false;
    public static void LOGD(string tag, string message)
    {
        if (IS_DEBUG)
        {
            Debug.Log("LOGD:" + tag + ": " + message);
        }
    }
    public static void LOGW(string tag, string message)
    {
        Debug.Log("LOGW:" + tag + ": " + message);
    }
    public static void LOGE(string tag, string message)
    {
        Debug.Log("LOGE:" + tag + ": " + message);
    }
}
