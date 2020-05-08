using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pivot
{
    public GameObject Object = null;//объект прикрепления
    public Vector3 position = Vector3.zero;//позиция точки прикрепления в локальных координатах объекта прикрепления    
    public Quaternion rotation = Quaternion.identity;//направление прикрепления в локальных координатах объекта прикрепления
}

public class device
{
    public virtual void Place()
    {
    }

    public virtual void Remove()
    {
    }

    public virtual void KinematicUpdate()
    {
    }
}
