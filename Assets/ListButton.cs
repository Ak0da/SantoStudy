using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListButton : MonoBehaviour
{
    [SerializeField]
    GameRules gameRules;
    public int listIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        gameRules.ButtonClicked(listIndex);
    }
}
