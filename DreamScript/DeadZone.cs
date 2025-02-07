using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<CheckObject>() != null)
        {
            GameManagerInDream.instance.GetComponent<GameManagerInDream>().GetState = State.Shorten;
           
        }
    }
}
