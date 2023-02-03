using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace BonkIncStandard.Utils.Http
{
    public class HttpRequestHandler
    {
        public delegate void WebRequestCallback<TResult>(bool succes, TResult result);

        public IEnumerator Get<TResult>(string url, WebRequestCallback<TResult> callback) => Get(new Uri(url), callback);
        private IEnumerator Get<TResult>(Uri url, WebRequestCallback<TResult> callback)
        {
            using (var webRequest = UnityWebRequest.Get(url))
            {
                yield return webRequest.SendWebRequest();
                Debug.Log(webRequest.result);
                switch (webRequest.result)
                {
                    case UnityWebRequest.Result.ConnectionError:
                    case UnityWebRequest.Result.DataProcessingError:
                        callback.Invoke(false, default);
                        Debug.LogError(": Error: " + webRequest.error);
                        break;
                    case UnityWebRequest.Result.ProtocolError:
                        Debug.LogError(": HTTP Error: " + webRequest.error);
                        callback.Invoke(false, default);
                        break;
                    case UnityWebRequest.Result.Success:
                        Debug.Log("Get request Complete");
                        var data = JsonUtility.FromJson<TResult>(webRequest.downloadHandler.text);
                        callback.Invoke(true, data);
                        break;
                }
            }
        }

        public IEnumerator Post<TBody, TResult>(string url, TBody body, WebRequestCallback<TResult> callback) => Post(new Uri(url), body, callback);
        private IEnumerator Post<TBody, TResult>(Uri url, TBody body, WebRequestCallback<TResult> callback)
        {
            var json = JsonUtility.ToJson(body);

            using (var www = new UnityWebRequest(url))
            {
                www.method = "POST";
                DownloadHandlerBuffer downloader = new DownloadHandlerBuffer();
                www.downloadHandler = downloader;

                var data = System.Text.Encoding.UTF8.GetBytes(json);
                www.uploadHandler = new UploadHandlerRaw(data);
                www.SetRequestHeader("Content-Type", "application/json");
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(www.error);
                    callback.Invoke(false, default);
                }
                else
                {
                    Debug.Log("Form upload complete!");
                    var returnData = JsonUtility.FromJson<TResult>(www.downloadHandler.text);
                    callback?.Invoke(true, returnData);
                }
            }
        }
        
        public IEnumerator Put<TBody, TResult>(string url, TBody body, WebRequestCallback<TResult> callback) => Put(new Uri(url), body, callback);
        private IEnumerator Put<TBody, TResult>(Uri url, TBody body, WebRequestCallback<TResult> callback)
        {
            var json = JsonUtility.ToJson(body);

            using (var www = new UnityWebRequest(url))
            {
                www.method = "PUT";
                DownloadHandlerBuffer downloader = new DownloadHandlerBuffer();
                www.downloadHandler = downloader;

                var data = System.Text.Encoding.UTF8.GetBytes(json);
                www.uploadHandler = new UploadHandlerRaw(data);
                www.SetRequestHeader("Content-Type", "application/json");
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(www.error);
                    callback.Invoke(false, default);
                }
                else
                {
                    Debug.Log("Form upload complete!");
                    var returnData = JsonUtility.FromJson<TResult>(www.downloadHandler.text);
                    callback?.Invoke(true, returnData);
                }
            }
        }
        
        public IEnumerator Delete<TResult>(string url, WebRequestCallback<TResult> callback) => Delete(new Uri(url), callback);
        private IEnumerator Delete<TResult>(Uri url, WebRequestCallback<TResult> callback)
        {
            using (var www = UnityWebRequest.Delete(url))
            {
                yield return www.SendWebRequest();
                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(www.error);
                    callback.Invoke(false, default);
                }
                else
                {
                    Debug.Log("Form upload complete!");
                    var returnData = JsonUtility.FromJson<TResult>(www.downloadHandler.text);
                    callback?.Invoke(true, returnData);
                }
            }
        }
    }
}