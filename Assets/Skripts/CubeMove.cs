using System.Collections;
using System.IO;
using System.Net;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

[Serializable]
public class Geopoly
{
    public int loop { get; set; }
    public double time { get; set; }
    public List<List<List<double>>> coordinates { get; set; }
}

[Serializable]
public class Root
{
    public Geopoly geopoly { get; set; }
}


public class CubeMove : MonoBehaviour
{
    public GameObject                       cube;
    public float                            speed;
    public float                            distance;
    private bool                            finished;
    public static  int                      key;
    public static int                       k;
    public float                            x0;
    public float                            y0;
    public float                            z0;
    public int                              toStart;
    [SerializeField] public float           time;
    public int                              loop;
    [SerializeField] private string         url;
    public Root                             JsonData;
    private void Start()
    {
        finished = false;
        key = 0;
        k = 0;
        x0 = transform.position.x;
        y0 = transform.position.y;
        z0 = transform.position.z;
        StartCoroutine(GetText());
        
    }
    IEnumerator GetText()
    {
        WWW w = new WWW(url);
        yield return w;
        if (w.error != null)
        {
            Debug.Log("Error " + w.error);
        }
        else
        {
            string json = w.text;
            JsonData = JsonConvert.DeserializeObject<Root>(json);
            Debug.Log(JsonData.geopoly.coordinates[0][3][0]);
            time = (float) JsonData.geopoly.time;
            loop = JsonData.geopoly.loop;


            ////////// SPEED CALCULATION FOR TIME /////////////////
            distance = Mathf.Sqrt((Mathf.Pow(x0 - (float) JsonData.geopoly.coordinates[0][0][0], 2)
                            + Mathf.Pow(y0 - (float)JsonData.geopoly.coordinates[0][0][1], 2) 
                            + Mathf.Pow(z0 - (float)JsonData.geopoly.coordinates[0][0][2], 2)));
            for (int i = 0; i < JsonData.geopoly.coordinates.Count; i++)
            {
                for (int j = 0; j < JsonData.geopoly.coordinates[i].Count - 1; j++)
                {
                        distance += Mathf.Sqrt((Mathf.Pow((float) JsonData.geopoly.coordinates[i][j][0] 
                                                - (float)JsonData.geopoly.coordinates[i][j + 1][0], 2)
                                            + Mathf.Pow((float)JsonData.geopoly.coordinates[i][j][1] - 
                                                (float)JsonData.geopoly.coordinates[i][j + 1][1], 2)
                                            + Mathf.Pow((float)JsonData.geopoly.coordinates[i][j][2] - 
                                                (float)JsonData.geopoly.coordinates[i][j + 1][2], 2)));
                }
            }
            speed = distance / time;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || finished == true)
        {
            finished = true;
            if ((k >= JsonData.geopoly.coordinates[0].Count ||
                cube.transform.localPosition.x == (float)JsonData.geopoly.coordinates[0][k][0]
                && cube.transform.localPosition.y == (float)JsonData.geopoly.coordinates[0][k][1] &&
                    cube.transform.localPosition.z == (float)JsonData.geopoly.coordinates[0][k][2]
                    && JsonData.geopoly.coordinates[0].Count - 1 == k) && loop == 0)
                finished = false;
            else if (cube.transform.localPosition.x == (float)JsonData.geopoly.coordinates[0][k][0] &&
                    cube.transform.localPosition.y == (float)JsonData.geopoly.coordinates[0][k][1] &&
                    cube.transform.localPosition.z == (float)JsonData.geopoly.coordinates[0][k][2] &&
                    JsonData.geopoly.coordinates[0].Count - 1 != k && toStart == 0)
                k++;
            else if (cube.transform.localPosition.x == (float)JsonData.geopoly.coordinates[0][k][0] &&
                    cube.transform.localPosition.y == (float)JsonData.geopoly.coordinates[0][k][1] &&
                    cube.transform.localPosition.z == (float)JsonData.geopoly.coordinates[0][k][2]
                    && JsonData.geopoly.coordinates[0].Count - 1 == k && loop == 1)
            {
                    k = 0;
                    toStart = 1;
            }

            if (toStart == 1)
            {
                if (cube.transform.localPosition.x != x0 &&
                    cube.transform.localPosition.y != y0 &&
                    cube.transform.localPosition.z != z0)

                {
                    cube.transform.position = Vector3.MoveTowards(transform.position,
                            new Vector3(x0, y0, z0), speed * Time.deltaTime);
                }
                else
                    toStart = 0;
            }
            else if (toStart == 0)
            {
                cube.transform.position = Vector3.MoveTowards(transform.position,
                        new Vector3((float)JsonData.geopoly.coordinates[0][k][0],
                        (float)JsonData.geopoly.coordinates[0][k][1],
                        (float)JsonData.geopoly.coordinates[0][k][2]), speed * Time.deltaTime);
            }
        }
    }
}
