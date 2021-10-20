using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if((PlayerPrefs.GetInt("Gift01", 1)) != 0)
        {
            Debug.Log("package picked up and savesd");
        }
        Debug.Log(PlayerPrefs.GetInt("Gift01", 0));
    }
}
