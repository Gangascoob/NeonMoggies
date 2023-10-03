using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScore : MonoBehaviour
{

    private GameController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Sign"))
        {
            controller.ScoreUp();
            Debug.Log("score");
            Destroy(gameObject);
        }
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Sign"))
        {
            controller.ScoreUp();
            Debug.Log("score");
            Destroy(gameObject);
        }
    }
}
