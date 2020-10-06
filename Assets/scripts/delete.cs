using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delete : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (!transform.parent.GetComponent<create>().created)
            other.GetComponent<Cube>().Delete();

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
