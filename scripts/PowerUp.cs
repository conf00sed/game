using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float powerUpValue = 2f; // Adjust the power-up value as needed

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            Debug.Log("powerup detected");

            // Access player shooting script
            PlayerShooting playerPowerUp = other.GetComponent<PlayerShooting>();

            // Increase player stats
            playerPowerUp.IncreasePower(powerUpValue);

            // Destroy powerup object
            Destroy(gameObject);
        }
    }
}
