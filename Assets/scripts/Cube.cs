using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cube : MonoBehaviour
{
    private string animationName = "New Animation";
    private Vector2 delta;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "player")
        {

            SceneManager.LoadScene(0);
        }
    }
    public void Delete()
    {
        Destroy(transform.parent.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<Animation>().IsPlaying(animationName))
        {
            delta = transform.position-transform.parent.position;
            delta.y = -0.7f;
            transform.parent.Translate(delta);
            GetComponent<Animation>().Play(animationName);
        }
    }
}
