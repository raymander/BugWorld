using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BugController : MonoBehaviour
{
    public float speed = 0.3f;
    public float rotationSpeed = 80;
    public bool move;

    //Vector2 startPos;
    //Vector2 curPos;
    //Vector2 lastPos;

    //float swipeDistance;
    //float minSwipeDistance;

    //Touch touch;

    //bool brake;

    Transform camTrans;
    Quaternion originalRotation;
    Vector3 deadRotation;

    Animator animator;

    UnityEvent deathRotateEvent;

    private void Awake()
    {
        animator = this.gameObject.transform.GetChild(0).GetComponent<Animator>();
        //startPos = new Vector2(Screen.width / 2, Screen.height / 2);
        //minSwipeDistance = Screen.width / 8;
        camTrans = Camera.main.transform;
    }

    // Use this for initialization
    void Start()
    {
        if (deathRotateEvent == null)
            deathRotateEvent = new UnityEvent();

        deathRotateEvent.AddListener(deadCameraAngle);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.gameOver)
        {
            if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
            {
                move = true;
            }
            else
            {
                move = false;
            }

            if (move)
            {
                animator.enabled = true;
                //stepEvent.AddListener(startCor);
                //stepEvent.Invoke();
                //stepEvent.RemoveListener(startCor);
            }
            else
            {
                animator.enabled = false;
            }

            float translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

            transform.Translate(0, 0, -translation);
            transform.Rotate(0, rotation, 0);

            //if (Input.touchCount > 0)
            //{
            //    IsItBrake();
            //    move = true;
            //    if (brake == true)
            //    {
            //        speed -= initialSpeed / 10;
            //        brake = false;
            //    }
            //    touch = Input.GetTouch(0);

            //    curPos = touch.position;
            //    //Debug.Log("curpos"+ curPos);
            //    Move();
            //    swipeDistance = Mathf.Abs(startPos.x - curPos.x);
            //    //Debug.Log("swipeDist" + swipeDistance);

            //    if (touch.phase != TouchPhase.Ended && swipeDistance > minSwipeDistance)
            //    {
            //        Swipe();
            //    }
            //}
            //else
            //{
            //    move = false;
            //}
            //lastPos = curPos;
        } else
        {
            move = false;
            if (deathRotateEvent != null)
            {
                StartCoroutine("DeathActions");
            }
            return;
        }
    }

    //void Swipe()
    //{
    //    //check if its vertical or horizontal

    //    Vector2 distance = curPos - lastPos;

    //    //Debug.Log("Horizontal move");

    //    if (distance.x > 0)
    //        {
    //            //transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
    //            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    //        }
    //        if (distance.x < 0)
    //        {
    //            //transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
    //            transform.Rotate(-Vector3.up, rotationSpeed * Time.deltaTime);
    //    }
               
    //}

    //void Move()
    //{
    //    transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
    //}

    //void IsItBrake()
    //{
        
    //}

    void startCor()
    {
        StartCoroutine("StepShakeCamera");
    }

    IEnumerator StepShakeCamera()
    {
        originalRotation = camTrans.rotation;
        Vector3 stepRot = originalRotation.eulerAngles;
        while (move)
        {
            yield return new WaitForSeconds(0.1f);
            stepRot.z += 2f;
            Debug.Log("1 " + stepRot.z);
            camTrans.rotation = Quaternion.Euler(stepRot);

            yield return new WaitForSeconds(0.1f);
            stepRot.z -= 2f;
            Debug.Log("2 " + stepRot.z);
            camTrans.rotation = Quaternion.Euler(stepRot);

            yield return new WaitForSeconds(0.1f);
            stepRot.z -= 2f;
            Debug.Log("3 " + stepRot.z);
            camTrans.rotation = Quaternion.Euler(stepRot);

            yield return new WaitForSeconds(0.1f);
            stepRot.z += 2;
            Debug.Log(stepRot.z);
            camTrans.rotation = Quaternion.Euler(stepRot);
        }       
            yield return null;      
    }

    IEnumerator DeathActions()
    {
        StartCoroutine("DeathCameraShake");
        yield return new WaitForSeconds(0.3f);
        deathRotateEvent.Invoke();
        yield return new WaitForSeconds(0.5f);
        deathRotateEvent.RemoveAllListeners();
    }

    IEnumerator DeathCameraShake()
    {
        yield return null;
    }

    void deadCameraAngle()
    {
        deadRotation = camTrans.rotation.eulerAngles;
        camTrans.rotation = Quaternion.Lerp(camTrans.rotation, Quaternion.Euler(deadRotation.x - 45, deadRotation.y + 100, deadRotation.z), 2 * Time.deltaTime);
        camTrans.Translate(-Vector3.up * 0.003f);
    }
}
