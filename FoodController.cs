using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    public GameObject[] flakes;
    public float rotationSpeed; 
    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = 50;

        //needs to be changed when flakes are set to spawn automatically
        flakes = GameObject.FindGameObjectsWithTag("Food");
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject flake in flakes)
        {
            flake.transform.Rotate(0, rotationSpeed*Time.deltaTime, 0);
        }
    }
}
