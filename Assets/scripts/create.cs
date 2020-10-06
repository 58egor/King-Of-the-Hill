using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class create : MonoBehaviour
{
    public GameObject prefab;
    public bool created = true;
    public GameObject[] SpawnPoints;
    public GameObject[] SpawnObjects;
    public double timer = 2f;
    public GameObject obj;
    double time=0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "player" && created)
        {
            timer -= 0.1f;
            Vector3 vec = transform.position;
            vec.y += 0.7f * 8;
            vec.x += -9.8f-0.7f*2;
            obj=Instantiate(prefab, vec, Quaternion.identity);
            obj.GetComponent<create>().timer = timer;
            created = false;
            if(timer>0.8f)
            Debug.Log("Timer:" + timer);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (created)
        {
            spawn();
        }
        else
        {
            if (obj.GetComponent<create>().obj != null)
            if (!obj.GetComponent<create>().obj.GetComponent<create>().created)
            {
                Destroy(transform.gameObject);
            }
        }
    }
    void spawn()
    {
        if (time < 0)
        {
           int i = Random.Range(0, SpawnPoints.Length);
            Debug.Log("Random:" + i);
            Vector3 vec = SpawnPoints[i].transform.position;
            Instantiate(SpawnObjects[0], vec, Quaternion.identity);
            time = timer;
        }
        else
        {
            time -= Time.deltaTime;
        }
    }
}
