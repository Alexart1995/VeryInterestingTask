using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine.Networking;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;


public class JSONReader : MonoBehaviour
{
    [SerializeField] private string url;
    //public TextAsset filename;
    public class Root
    {
        public List<List<List<double>>> coordinates { get; set; }
    }
    void Start()
    {
        StartCoroutine(SendRequest());
    }
    private IEnumerator SendRequest()
    {
        //UnityWebRequestTexture.GetTexture
        using (UnityWebRequest request = UnityWebRequest.Get(url))

        {
            yield return request.SendWebRequest();
            string json = request.downloadHandler.text;
            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(File.ReadAllText(json));
            if (request.isNetworkError || request.isHttpError)
                Debug.Log(request.error);
            else
            {
                Debug.Log(json);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
