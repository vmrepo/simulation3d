using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainscene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #if !UNITY_EDITOR

        string[] args = System.Environment.GetCommandLineArgs();

        for (int i = 1; i < args.Length ; i++)
        {
            if (args[i].Substring(0, 6).ToLower() == "port0:")
            {
                int port;
                if (Int32.TryParse(args[i].Substring(6), out port))
                {
                    Server0.Port = port;
                }
                else
                {
                    Server0.Log("bad parmeter port");
                }
            }

            if (args[i].Substring(0, 8).ToLower() == "logfile:")
            {
                Server0.Logfile = args[i].Substring(8);
            }
        }

        #endif

        GameObject.Find("Main Camera").GetComponent<camera>().targetposition = new Vector3(0, 0, 0);
        Server0.Start();
    }

    // Update is called once per frame
    void Update()
    {
        Server0.Update();

        if (Input.GetKeyDown(KeyCode.F10))
        {
            Application.Quit();
        }
    }

    void OnApplicationQuit()
    {
        Server0.Stop();
    }
}
