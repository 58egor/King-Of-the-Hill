using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class control : MonoBehaviour, IBeginDragHandler,IDragHandler
{
    int numb = 0;
    float up = 0;
    int chet = 0;
    public Text score;
    float upx = 0.65f;
    public void OnBeginDrag(PointerEventData evenData)
    {
        if (sphere.GetComponent<sphere>().OnGround)
        {
            if (evenData.delta.x > 0)//свайп влево
            {
                if (numb + 1 < 3)
                {
                    sphere.constraints = RigidbodyConstraints.None;
                    Vector3 pos = sphere.transform.position;
                    //нужная нам позиция- позиция шара с z+1
                    pos.z = numb + 1;
                    //Вычисляем расстояние до точки, куда нам нужно попасть
                    float distance = Vector3.Distance(sphere.transform.position, pos);
                    //Вычисляем направление
                    Vector3 dir = (pos - sphere.transform.position).normalized;
                    //Выставляем координату "у"-угол
                    dir.y = 0.5f;
                    //вычисляем силу
                    var force = Mathf.Sqrt(distance * 10f) * dir * sphere.mass;
                    Debug.Log(force);
                    sphere.AddForce(force, ForceMode.Impulse);
                    numb++;
                    sphere.GetComponent<sphere>().OnGround = false;
                }
                else
                {
                    sphere.constraints = RigidbodyConstraints.None;
                    sphere.velocity = new Vector2(0, 3.5f);
                }
                Debug.Log("Свайп влево");
            }
            if (evenData.delta.x < 0)//свайп влево
            {
                if (numb - 1 > -3)
                {
                    sphere.constraints = RigidbodyConstraints.None;
                    Vector3 pos = sphere.transform.position;
                    pos.z = numb - 1;
                    float distance = Vector3.Distance(sphere.transform.position, pos);
                    Vector3 dir = (pos - sphere.transform.position).normalized;
                    dir.y = 0.5f;
                    var force = Mathf.Sqrt(distance * 10f) * dir * sphere.mass;
                    Debug.Log(force);
                    sphere.AddForce(force, ForceMode.Impulse);
                    numb--;
                    sphere.GetComponent<sphere>().OnGround = false;
                }
                else
                {
                    sphere.constraints = RigidbodyConstraints.None;
                    sphere.velocity = new Vector2(0, 3.5f);
                }
                Debug.Log("Свайп вправо");
            }
        }
    }
    public void OnDrag(PointerEventData evenData)
    {
        
    }
    public Rigidbody sphere;
    // Start is called before the first frame update
    void Start()
    {
        float ratio = 1f * Screen.height / Screen.width;
        score.SetNativeSize();
        float ortSize = 100 / 200f;
        Camera.main.orthographicSize = ortSize;
        //transform.LookAt(sphere.transform);

    }
    public Vector2 startPos;
    public Vector2 direction;
    public bool directionChosen;
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && sphere.GetComponent<sphere>().OnGround)
        {
            Touch touch = Input.GetTouch(0);

            // Handle finger movements based on touch phase.
            switch (touch.phase)
            {
                // Record initial touch position.
                case TouchPhase.Began:
                    startPos = touch.position;
                    directionChosen = false;
                    break;

                // Determine direction by comparing the current touch position with the initial one.
                case TouchPhase.Moved:
                    direction = touch.position - startPos;
                    break;

                // Report that a direction has been chosen when the finger is lifted.
                case TouchPhase.Ended:
                    directionChosen = true;
                    break;
            }
        }
        if (directionChosen && sphere.GetComponent<sphere>().OnGround)
        {
            Debug.Log(direction);
            sphere.constraints = RigidbodyConstraints.None;
            Vector3 pos = sphere.transform.position;
            pos.y = up + 0.7f;
            pos.x = upx-0.7f;
            float distance = Vector3.Distance(sphere.transform.position, pos);
            Vector3 dir = (pos - sphere.transform.position).normalized;
            dir.y = 1.5f;
            var force = Mathf.Sqrt(distance * 10f) * dir * sphere.mass;
            Debug.Log(force);
            sphere.AddForce(force, ForceMode.Impulse);
            up+=0.7f;
            upx -= 1.4f;
            directionChosen = false;
            sphere.GetComponent<sphere>().OnGround = false;
            chet++;
        }
        scr();
    }
    void scr()
    {
        score.text = "Score:" + chet;
    }
    void FixedUpdate()
    {

        //transform.LookAt(sphere.transform);
        //Debug.Log(dist);
        ////vec = Camera.main.ScreenToWorldPoint(vec);
        ////Debug.Log(vec);
        Vector3 vec = sphere.transform.position;
        vec.y += 5.99f;
        vec.x += 3.56f;
        vec.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, vec, 0.25f);

    }

}
