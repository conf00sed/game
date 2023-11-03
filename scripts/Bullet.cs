using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int damage = 3;

    private void Start()
    {
        // destroy bullet after 3 seconds
        Destroy(gameObject, 3f);
    }


    private void OnTriggeonEnter2D(Collider2D collision)
    {
        // if bullet trigers collider
        if (collision.gameObject.GetComponent<EnemyController>() != null)
        {
            // destroy both zombie and bullet
            Destroy(collision.gameObject);
            Destroy(gameObject); 
        }
    }
}
