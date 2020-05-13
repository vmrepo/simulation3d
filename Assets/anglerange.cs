using System.Collections;
using System.Collections.Generic;

public class AngleRange
{
    private float downlimit;
    private float uplimit;
    private float targetangle;
    private bool controlallow = false;

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
            if (targetangle < downlimit || LE(uplimit, targetangle))
            {
                if (Distance(targetangle, downlimit) < Distance(targetangle, uplimit))
                    targetangle = downlimit < 360 ? downlimit : 0;
                else
                    targetangle = uplimit < 360 ? uplimit : 0;
            }
        }
        else
        {
            if (targetangle < downlimit && LE(uplimit, targetangle))
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

        controlallow = true;

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

    public bool LE(float v1, float v2)
    {
        return v1 < v2 || UnityEngine.Mathf.Abs(v2 - v1) < 0.01;
    }

    //проверяет поападание угла в разрешённый диапазон
    public bool IsAllow(float angle)
    {
        if (downlimit == 0 && uplimit == 360)
        {
            return true;
        }

        angle = CheckRange(angle);

        if  (downlimit < uplimit && LE(downlimit, angle) && LE(angle, uplimit))
            return true;

        if (downlimit > uplimit && LE(uplimit, angle) || LE(angle, downlimit))
            return true;

        return false;
    }

    //возвращает кратчайшую дистанцию от 1-ого угла ко 2-му со знаком направления
    //если установлен controlallow, то путь не должен лежать через запрещённый участок
    //controlallow устанавливается при установке лимитов, чтобы путь к границе был адекватен, затем сбрасывается
    //в рабочем состоянии проверка пути, чаще всего не требуется
    //и наоборот при его включении в физике иногда из-за погрешностей могут путаться разрешённые и запрещённые пути
    public float Delta(float angle1, float angle2)
    {
        angle1 = CheckRange(angle1);
        angle2 = CheckRange(angle2);

        float delta = angle2 - angle1;

        if (!controlallow)
        {
            if (UnityEngine.Mathf.Abs(delta) > 180)
            {
                delta -= UnityEngine.Mathf.Sign(delta) * 360;
            }

            return delta;
        }

        if ((downlimit == 0 && uplimit == 360) || !IsAllow(angle1) || !IsAllow(angle2))
        {
            if (UnityEngine.Mathf.Abs(delta) > 180)
            {
                delta -= UnityEngine.Mathf.Sign(delta) * 360;
            }
        }
        else
        {
            if ((downlimit > uplimit && 
                LE(downlimit, angle1) && 
                angle1 < 360 && 
                LE(0, angle2) && 
                LE(angle2, uplimit))
                ||
                (LE(downlimit, angle2) && 
                angle2 < 360 && 
                LE(0, angle1) && 
                LE(angle1, uplimit))
                )
            {
                delta -= UnityEngine.Mathf.Sign(delta) * 360;
            }
        }

        controlallow = false;

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
