using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;


public class WeiBoSDK 
{
    public static string token;
    public static string uid;
    static AndroidJavaClass jc;
    static AndroidJavaObject jo;

    public static void Init()
    {
        jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        // jo.Call("getSSOLogin", jo);
        jo.Call("getSSOLogin", jo);
    }

    public static void PublicCallBack()
    {
        token = jo.Call<string>("getWeiboToken");
        uid = jo.Call<string>("getWeiboUid");
        Debug.LogError("PublicCallBack token:" + token + " uid:" + uid);
    }

  

}
