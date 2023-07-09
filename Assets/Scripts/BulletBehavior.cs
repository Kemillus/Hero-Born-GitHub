using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float onScreenDelay = 3f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, onScreenDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
