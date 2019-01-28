using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodTrigger : MonoBehaviour

{
    public int health;
    public List<GameObject> healthStats = new List<GameObject>();
    GameObject lastStat;
    public bool gameOver;
    public bool home;

    void Awake()
    {
        health = 8;
        //health is AN INDEX NOT A QUANTITY
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("minusHealth");
    }

    // Update is called once per frame
    void Update()
    {
        if (health >= 0)
        {
           lastStat = healthStats[health];
        }       
        else
        {
            GameManager.instance.gameOver = true;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Food")
        {
            Destroy(col.gameObject);

            if (health < 8) { 
            health += 1;
            healthStats[health].SetActive(true);
            }
        }

        if (col.gameObject.tag == "Home")
        {
            home = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        
        if (col.gameObject.tag == "Home")
        {
            home = false;
        }
    }

    IEnumerator minusHealth()
    {
        while (health <= 8 && !gameOver)
        {
            if (home)
            {
                yield return null;
            }
            else
            {
                yield return new WaitForSeconds(3f);
                lastStat.SetActive(false);
                health -= 1;
            }              
        }
        yield return null;

    }
}
