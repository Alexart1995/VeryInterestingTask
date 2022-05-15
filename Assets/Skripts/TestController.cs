using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class TestController : MonoBehaviour
{
    public class Root
    {
        public List<List<List<int>>> coordinates { get; set; }
    }

    [ContextMenu("Test Get")]
    public async void TestGet()
    {
        var url = "https://my-json-server.typicode.com/Alexart1995/VII/db";
        using var www = UnityWebRequest.Get(url);
        www.SetRequestHeader("Content-Type", "application/json");

        var request = www.SendWebRequest();

        while (!request.isDone)
            await Task.Yield();
        var jsonResponse = www.downloadHandler.text;
        if (www.result != UnityWebRequest.Result.Success)
            Debug.LogError($"Failed {www.downloadHandler.text}");
        
        try
        {
            var result = JsonConvert.DeserializeObject<Root>(jsonResponse);
            Debug.Log($"Success {www.downloadHandler.text}");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error parsing");
        }

    }    
}
