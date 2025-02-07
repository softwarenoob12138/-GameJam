using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckObject : MonoBehaviour
{


    public Vector3 oldPosition;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Transport>() != null)
        {
            Transport transport = other.GetComponent<Transport>();
            transport.isMoving = true;
            transform.GetComponent<Collider2D>().enabled = false;
            GameManagerInDream.instance.GetComponent<GameManagerInDream>().GetState = State.Shorten;
        }

        else if(other.GetComponent<Transport2>() != null)
        {
            Transport2 transport = other.GetComponent<Transport2>();
            transport.isMoving = true;
            transform.GetComponent<Collider2D>().enabled = false;
            GameManagerInDream2.instance.GetComponent<GameManagerInDream2>().GetState = State.Shorten;
        }

        else if(other.GetComponent<SelfDestroy>() != null)
        {
            oldPosition = other.transform.position;
            other.transform.parent = transform;  // 让抓取到的物品 接入 父级
            GameManagerInDream.instance.GetComponent<GameManagerInDream>().GetState = State.Shorten;
            transform.GetComponent<Collider2D>().enabled = false;
            other.GetComponent<Collider2D>().enabled = false;

        }

        else if(other.GetComponent<SelfDestroy2>() != null)
        {
            oldPosition = other.transform.position;
            other.transform.parent = transform;  // 让抓取到的物品 接入 父级
            GameManagerInDream2.instance.GetComponent<GameManagerInDream2>().GetState = State.Shorten;
            transform.GetComponent<Collider2D>().enabled = false;
            other.GetComponent<Collider2D>().enabled = false;

        }

    }

}
