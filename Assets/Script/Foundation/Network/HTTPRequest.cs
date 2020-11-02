using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using Utf8Json;

using static Network.WebRequest;

public class HTTPRequest : MonoBehaviour
{
    struct Packet<T>
    {
        public string Uri;
        public RequestMethod Method;
        public ResultType Type;
        public T Delegate;
    }
    
    public void Request<T>(string uri, ResultType type, T dlg)
    {
        Packet<T> p = new Packet<T>();
        p.Uri = uri;
        p.Type = type;
        p.Delegate = dlg;
        p.Method = RequestMethod.GET;
        StartCoroutine(Send(p));
    }

    IEnumerator Send<T>(Packet<T> p)
    {
        UnityWebRequest req = UnityWebRequest.Get(p.Uri);
        yield return req.SendWebRequest();

        if (req.isNetworkError || req.isHttpError)
        {
            Debug.Log(req.error);
        }
        else
        {
            DataParse(p, req);
        }
    }

    void DataParse<T>(Packet<T> p, UnityWebRequest req)
    {
        string str = req.downloadHandler.text;
        Debug.Log(str);
        switch (p.Type)
        {
            case ResultType.String:
                {
                    GetString Delegate = p.Delegate as GetString;
                    if(Delegate != null)
                    {
                        Delegate(req.downloadHandler.text);
                    }
                }
                break;

            case ResultType.Json:
                {
                    GetDynamic Delegate = p.Delegate as GetDynamic;
                    if (Delegate != null)
                    {
                        Delegate(JsonSerializer.Deserialize<dynamic>(req.downloadHandler.text));
                    }
                }
                break;
        }
    }    
}