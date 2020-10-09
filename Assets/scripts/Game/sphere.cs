using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphere : MonoBehaviour
{//данный срипт дает нам информацию шар приземлилися или нет
    Rigidbody body;
    public bool OnGround=true;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 0)
        {
            body.constraints= RigidbodyConstraints.FreezeAll;
            OnGround = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 0)
        {
            OnGround = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        body.freezeRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
