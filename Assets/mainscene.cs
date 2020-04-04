using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainscene : MonoBehaviour
{
    private manipulator1 manipulator0 = null;
    private manipulator2 manipulator1 = null;

    // Start is called before the first frame update
    void Start()
    {
        Server0.Start();

        manipulator0 = new manipulator1();
        manipulator0.Place(-1, 0, 0, 0);

        manipulator1 = new manipulator2();
        manipulator1.Place(1, 0, 0, 0);

        GameObject.Find("Main Camera").GetComponent<camera>().targetposition = new Vector3(0, 1, 0);
    }

    void OnApplicationQuit()
    {
        Server0.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        float delta = 10.0f;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            manipulator0.SetPos0(manipulator0.GetPos0() - delta);
            manipulator1.SetPos0(manipulator1.GetPos0() - delta);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            manipulator0.SetPos0(manipulator0.GetPos0() + delta);
            manipulator1.SetPos0(manipulator1.GetPos0() + delta);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            manipulator0.SetPos1(manipulator0.GetPos1() - delta);
            manipulator1.SetPos1(manipulator1.GetPos1() - delta);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            manipulator0.SetPos1(manipulator0.GetPos1() + delta);
            manipulator1.SetPos1(manipulator1.GetPos1() + delta);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            manipulator0.SetPos2(manipulator0.GetPos2() - delta);
            manipulator1.SetPos2(manipulator1.GetPos2() - delta);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            manipulator0.SetPos2(manipulator0.GetPos2() + delta);
            manipulator1.SetPos2(manipulator1.GetPos2() + delta);
        }
    }
}
