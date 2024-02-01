using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSpeed : MonoBehaviour
{
    [SerializeField]
    private bool IsLeft;

    // Start is called before the first frame update
    void Start()
    {
        float XScale = IsLeft ? -1 : 1;

        float speedScale = 20.0f;
        float angularSpeedScale = 200.0f;

        Vector2 NewSpeed = new Vector2(XScale,4);
        GetComponent<Rigidbody2D>().velocity = NewSpeed.normalized * speedScale;
        GetComponent<Rigidbody2D>().angularVelocity = -angularSpeedScale * XScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
