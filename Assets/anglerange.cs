using System.Collections;
using System.Collections.Generic;

public class AngleRange
{
    private float downlimit;
    private float uplimit;
    private float targetangle;

    private float checkrange(float angle)
    {
        angle = angle % 360;
        if (angle < 0)
            angle += 360;

        return angle;
    }

    private void checklimits()
    {
        if (downlimit < uplimit)
        {
            if (targetangle < downlimit || uplimit <= targetangle)
            {
                if (Distance(targetangle, downlimit) < Distance(targetangle, uplimit))
                    targetangle = downlimit < 360 ? downlimit : 0;
                else
                    targetangle = uplimit < 360 ? uplimit : 0;
            }
        }
        else
        {
            if (targetangle < downlimit && uplimit <= targetangle)
            {
                if (Distance(targetangle, downlimit) < Distance(targetangle, uplimit))
                    targetangle = downlimit < 360 ? downlimit : 0;
                else
                    targetangle = uplimit < 360 ? uplimit : 0;
            }
        }
    }

    public bool SetLimits(float angle1, float angle2)
    {
        downlimit = checkrange(angle1);
        uplimit = checkrange(angle2);

        if (uplimit == 0)
            uplimit = 360;

        checklimits();

        return true;
    }

    public float GetDownLimit()
    {
        return downlimit;
    }

    public float GetUpLimit()
    {
        return uplimit;
    }

    public float Distance(float angle1, float angle2)
    {
        angle1 = checkrange(angle1);
        angle2 = checkrange(angle2);

        float distance = UnityEngine.Mathf.Abs(angle2 - angle1);

        if (distance > 180)
            distance = 360 - distance;

        return distance;
    }

    public void SetTarget(float angle)
    {
        targetangle = checkrange(angle);
        checklimits();
    }

    public float GetTarget()
    {
        return targetangle;
    }
}
