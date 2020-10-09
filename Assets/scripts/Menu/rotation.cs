using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation : MonoBehaviour
{
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        i = Random.Range(0, 2);
    }
    //скрипт который вращае объекты для менню
    // Update is called once per frame
    void Update()
    {
       if(i==0)
        transform.Rotate(new Vector3(0, 45, 0) * Time.deltaTime);
       if (i == 1)
        transform.Rotate(new Vector3(45, 0, 0) * Time.deltaTime);
        if (i == 2)
         transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime);
    }
}
