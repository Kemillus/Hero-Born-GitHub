using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : BaseClass
{

    public override string State { get => state; set => state = value; }

    public override void Initialize()
    {
        state = "Manager initialized";
        Debug.Log(state);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
