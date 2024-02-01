using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSelfDestruct : MonoBehaviour
{
    [SerializeField]
    private float TimeToLive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        TimeToLive -= Time.fixedDeltaTime;

        if(TimeToLive <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}
