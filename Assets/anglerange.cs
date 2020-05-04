using System.Collections;
using System.Collections.Generic;

public class AngleRange
{
    private float downlimit;
    private float uplimit;
    private float targetangle;

    //приводит угол к диапазону от 0 включительно до 360 невключительно
    public float CheckRange(float angle)
    {
        angle = angle % 360;
        if (angle < 0)
            angle += 360;

        return angle;
    }

    //проверяется целевой угол, если не разрешён, то он устанаваливается в ближашую границу разрешённого диапазона
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

    //устанаваливаются границы разрешённого диапазона, если целевой угол не попадает в него, то он устанавливается в ближайшую границу разрешённого диапазона
    public bool SetLimits(float angle1, float angle2)
    {
        downlimit = CheckRange(angle1);
        uplimit = CheckRange(angle2);

        if (uplimit == 0)
            uplimit = 360;

        checklimits();

        return true;
    }

    //возвращает нижнюю границу разрешённого диапазона в смысле сектора окружности
    public float GetDownLimit()
    {
        return downlimit;
    }

    //возвращает верхнюю границу разрешённого диапазона в смысле сектора окружности
    public float GetUpLimit()
    {
        return uplimit;
    }

    //проверяет поападание угла в разрешённый диапазон
    public bool IsAllow(float angle)
    {
        if (downlimit == 0 && uplimit == 360)
        {
            return true;
        }

        angle = CheckRange(angle);

        if  (downlimit < uplimit && downlimit <= angle && angle <= uplimit)
            return true;

        if (downlimit > uplimit && uplimit <= angle || angle <= downlimit)
            return true;

        return false;
    }

    //возвращает кратчайшую дистанцию от 1-ого угла ко 2-му со знаком направления и с учётом разрешённого диапазона
    public float Delta(float angle1, float angle2)
    {
        angle1 = CheckRange(angle1);
        angle2 = CheckRange(angle2);

        float delta = angle2 - angle1;

        if ((downlimit == 0 && uplimit == 360) || !IsAllow(angle1) || !IsAllow(angle2))
        {
            if (UnityEngine.Mathf.Abs(delta) > 180)
            {
                delta -= UnityEngine.Mathf.Sign(delta) * 360;
            }
        }
        else
        {
            if ((downlimit > uplimit) && (
                ((downlimit < angle1 || UnityEngine.Mathf.Abs(downlimit - angle1) < 0.01) && 
                angle1 < 360 && 
                (0 < angle2 || UnityEngine.Mathf.Abs(angle2) < 0.01) && 
                (angle2 < uplimit || UnityEngine.Mathf.Abs(uplimit - angle2) < 0.01))
                ||
                ((downlimit < angle2 || UnityEngine.Mathf.Abs(downlimit - angle2) < 0.01) && 
                angle2 < 360 && 
                (0 < angle1 || UnityEngine.Mathf.Abs(angle1) < 0.01) && 
                (angle1 < uplimit || UnityEngine.Mathf.Abs(uplimit - angle1) < 0.01))
                ))
            {
                delta -= UnityEngine.Mathf.Sign(delta) * 360;
            }
        }

        return delta;
    }

    //возвращает дистанцию от 1-ого угла ко 2-му без знака направления и без учёта разрешённого диапазона
    public float Distance(float angle1, float angle2)
    {
        angle1 = CheckRange(angle1);
        angle2 = CheckRange(angle2);

        float distance = UnityEngine.Mathf.Abs(angle2 - angle1);

        if (distance > 180)
            distance = 360 - distance;

        return distance;
    }
    
    //устанаваливается целевой угол, при непопадании в разрешённый диапазон корректируется в ближайшую границу разрешённого диапазона
    public void SetTarget(float angle)
    {
        targetangle = CheckRange(angle);
        checklimits();
    }

    //возвращает целевой угол
    public float GetTarget()
    {
        return targetangle;
    }
}
