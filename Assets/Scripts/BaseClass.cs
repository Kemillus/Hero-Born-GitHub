using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseClass
{
    protected string state;

    public abstract string State { get; set; }

    public abstract void Initialize();
}
