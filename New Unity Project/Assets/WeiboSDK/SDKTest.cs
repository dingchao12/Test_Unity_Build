using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;

public class SDKTest : MonoBehaviour {

    public Button StaticSDK;
    bool IsOnClick = false;
    string token;

    void Start ()
    {
        Debug.Log("UnityActivty call in");
        StaticSDK.onClick.AddListener(StaticWeiboSDK);
    }

   
    void StaticWeiboSDK()
    {
        IsOnClick = true;
        StaticSDK.transform.Find("Text").GetComponent<Text>().text = "Text~~~~~~";
       // WeiBoSDK.Init();
       // SsoHandleSucessful("");
        //string token = "2.008Kmn2CyY87OC61326af7295UTp8E";
        //string uid = "2154431951";
        //StartCoroutine(Gettimes(token));
        // StartCoroutine(GET(uid, token));
    }

    //void OnApplicationFocus(bool isFocus)
    //{
    //    if (isFocus)
    //    {
    //        if (!IsOnClick)
    //        {
    //            return;
    //        }
    //        Debug.LogError("OnApplicationFocus come in");
    //        WeiBoSDK.PublicCallBack();

    //        if (string.IsNullOrEmpty(WeiBoSDK.uid))
    //        {
    //            return;
    //        }

    //        //StartCoroutine(GET(WeiBoSDK.uid, WeiBoSDK.token));
    //    }
    //}

    public void SsoHandleSucessful(string str)
    {
       // str = "{" + "\"" + "wbCurrentUserID" + "\"" + ":" + "\"" + "2154431951" + "\"" + "," + "\"" + "wbtoken" + "\"" + ":" + "\"" + "2.008Kmn2CDqHUUCe6273213020s4mAK" + "\"" + "}";
        Debug.Log("SsoHandleSucessful call in "+str);
        if (string.IsNullOrEmpty(str))
        {
            return;
        }
        JsonData json = JsonMapper.ToObject(str);
        string uid = json["wbCurrentUserID"].ToString();
        token=json["wbtoken"].ToString();
        Debug.Log("token" + json["wbtoken"].ToString());
      
        StartCoroutine(GET(uid, token));
    }

    public  IEnumerator GET(string uid,string token)
    {
        string uidl = "5340736099";
        Dictionary<string, string> get = new Dictionary<string, string>();
        get.Add("source_id", uid);
        get.Add("target_id", uidl);
        get.Add("access_token", token);
        string Parameters;
        string mContent;
        bool first;
        string url = "https://api.weibo.com/2/friendships/show.json";

        if (get.Count > 0)
        {
            first = true;
            Parameters = "?";
            //从集合中取出所有参数，设置表单参数（AddField()).  
            foreach (KeyValuePair<string, string> post_arg in get)
            {
                if (first)
                    first = false;
                else
                    Parameters += "&";

                Parameters += post_arg.Key + "=" + post_arg.Value;
            }
        }
        else
        {
            Parameters = "";
        }

        var testC = "getURL :" + Parameters;

        Debug.LogError("testC:" + testC);
        //直接URL传值就是get  
        WWW www = new WWW(url + Parameters);
        yield return www;
        var mJindu = www.progress;

        if (www.error != null)
        {
            //GET请求失败  
            mContent = "error :" + www.error;
            StartCoroutine(Gettimes(token));
        }
        else
        {
            //GET请求成功  
            mContent = www.text;
        }
        Debug.LogError("mContent:" + mContent); 
        JsonData json = JsonMapper.ToObject(mContent);
        var uesrdata = json["source"].ToJson();
        JsonData json1 = JsonMapper.ToObject(uesrdata);
        var isfollowing = json1["following"].ToJson();
       
        if (isfollowing == "true")
        {
            StaticSDK.transform.Find("Text").GetComponent<Text>().text = "Text_successful";
        }
    }

    public IEnumerator Gettimes(string token)
    {
        Dictionary<string, string> get = new Dictionary<string, string>();
    
        get.Add("access_token", token);
        string Parameters;
        string mContent;
        bool first;
        string url = "https://api.weibo.com/2/account/rate_limit_status.json";

        if (get.Count > 0)
        {
            first = true;
            Parameters = "?";
            //从集合中取出所有参数，设置表单参数（AddField()).  
            foreach (KeyValuePair<string, string> post_arg in get)
            {
                if (first)
                    first = false;
                else
                    Parameters += "&";

                Parameters += post_arg.Key + "=" + post_arg.Value;
            }
        }
        else
        {
            Parameters = "";
        }

        var testC = "getURL :" + Parameters;

        Debug.LogError("testC:" + testC);
        //直接URL传值就是get  
        WWW www = new WWW(url + Parameters);

        yield return www;
       
        var mJindu = www.progress;

        if (www.error != null)
        {
            //GET请求失败  
            mContent = "error :" + www.error;
        }
        else
        {
            //GET请求成功  
            mContent = www.text;
        }
        Debug.LogError("UNity call back json:" + mContent);
    }
}
