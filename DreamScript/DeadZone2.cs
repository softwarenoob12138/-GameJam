using UnityEngine;

public class DeadZone2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<CheckObject>() != null)
        {
            GameManagerInDream2.instance.GetComponent<GameManagerInDream2>().GetState = State.Shorten;

        }
    }
}
