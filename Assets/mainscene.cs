using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainscene : MonoBehaviour
{
    private manipulator2 manipulator = null;

    // Start is called before the first frame update
    void Start()
    {
        manipulator = new manipulator2();
        manipulator.Place(0, 0, 0, 0);

        GameObject.Find("Main Camera").GetComponent<camera>().targetposition = new Vector3(0, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float delta = 10.0f;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            manipulator.SetPos0(manipulator.GetPos0() - delta);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            manipulator.SetPos0(manipulator.GetPos0() + delta);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            manipulator.SetPos1(manipulator.GetPos1() - delta);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            manipulator.SetPos1(manipulator.GetPos1() + delta);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            manipulator.SetPos2(manipulator.GetPos2() - delta);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            manipulator.SetPos2(manipulator.GetPos2() + delta);
        }
    }
}
