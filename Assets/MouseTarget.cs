using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTarget : MonoBehaviour
{


    [Header("Clamps")]
    [SerializeField] float y_clamp = 0f; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.x = 0;
        if (worldPosition.y < y_clamp)
        {
            worldPosition.y = y_clamp;
        }
        // worldPosition.z += Camera.main.transform.position.z;
        // worldPosition.y += Camera.main.transform.position.y;
        transform.position = worldPosition;
    }
}
