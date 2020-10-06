using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody body;
    bool OnGround = true;
    float y;
    float x;
    float z;
    public GameObject place;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 0)
        {
            //body.constraints = RigidbodyConstraints.FreezePosition;
            //body.constraints = RigidbodyConstraints.None;
            //body.constraints = RigidbodyConstraints.FreezePositionZ;
            //Vector3 pos = place.transform.position;
            //Debug.Log("pos:"+pos);
            ////нужная нам позиция- позиция шара с z+1
            ////Вычисляем расстояние до точки, куда нам нужно попасть
            //float distance = Vector3.Distance(transform.position, pos);
            ////Вычисляем направление
            //Vector3 dir = (pos - transform.position).normalized;
            ////Выставляем координату "у"-угол
            //dir.y = 0.5f;
            ////вычисляем силу
            //var force = Mathf.Sqrt(distance * 10f) * dir * body.mass;
            //Debug.Log(force);
            //body.AddForce(force, ForceMode.Impulse);
            //pos.x += 1.5f;
            //pos.y -= 0.7f;
            //place.transform.position = pos;
            //OnGround = true;
        }
        if (collision.gameObject.name == "player")
        {
            Debug.Log("коснулся игрока");
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 0)
        {

            Debug.Log(x);
            y -= 0.7f;
            OnGround = false;
        }
    }
    void Start()
    {
        body = GetComponent<Rigidbody>();
        y = transform.position.y;
        z = transform.position.z;
        x = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
