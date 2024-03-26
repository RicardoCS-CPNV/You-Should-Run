using UnityEngine;

public class ReverseMovement : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement playerMovement = collision.transform.GetComponent<PlayerMovement>();
            playerMovement.isReverseMovement = true;
            Destroy(gameObject);
        }
    }
}

