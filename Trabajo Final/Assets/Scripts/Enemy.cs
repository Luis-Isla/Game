using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Behaivor
{
    follower, viewer
}
public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public Behaivor type;

    public GameObject target;
    public GameObject spectators_prefabs;
    public float speed = 1f;
    public float dist;
    public float Trigger_time = 1f;
    public float Evolution_time = 15f;

    void Start()
    {
        target = GameObject.Find("Player_Position");
        type = Behaivor.viewer;
        transform.gameObject.tag = "Spectator";
    }
    void Update()
    {
        
        dist = Vector3.Distance(target.transform.position, transform.position);
        transform.LookAt(target.transform);
        
        switch (type)
        {
            case Behaivor.viewer:
                transform.LookAt(target.transform);
                Evolution_time=Evolution();
                break;
            case Behaivor.follower:
                Follow();

               
                break;
        }
    }
    
    private void Follow()
    {
        if (dist <= 20)
        {
           transform.position = Vector3.Lerp(transform.position, target.transform.position, speed * Time.deltaTime);
            
        }
    }

    private float Evolution()
    {
        Evolution_time = Evolution_time - 1f * Time.deltaTime;
        if (Evolution_time < 0)
        {
            type = Behaivor.follower;
            Evolution_time = 15f;
            
            transform.gameObject.tag = "Untagged";
        }
        return Evolution_time;
    }
   
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player_light")
        {
            Trigger_time = Trigger_time - 1f * Time.deltaTime;
            if (Trigger_time < 0)
            {
                Debug.Log("se encontro");
               
                Instantiate(spectators_prefabs, generate_position(), transform.rotation);
                Instantiate(gameObject, generate_position(), transform.rotation);
                Destroy(gameObject);
                Trigger_time = 1f;

            }
            
        }
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "Spectator" && other.gameObject.name == "Player_light")
        {
            Destroy(gameObject);
            Debug.Log("Se elimino");
        }
    }

    Vector3 generate_position()
    {
        Vector3 position=new Vector3(Random.Range(-13, 12), gameObject.transform.position.y, Random.Range(-13, 5));
        return position;
    }
}
