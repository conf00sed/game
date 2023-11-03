using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Basic settings
    public float moveSpeed = 2.0f; 
    public int health = 3;
    private Transform player;
    private bool isMoving = false;

    // Random interval Sound Player
    public AudioClip soundClip; 
    private AudioSource audioSource;
    public float minInterval = 10.0f;
    public float maxInterval = 20.0f;

    // Drop object
    public GameObject dropPrefab;

    private void Awake()
    {
        // Get components on wake
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = GetRandomSpawnPoint();
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlaySoundWithRandomInterval());
    }

    private Vector3 GetRandomSpawnPoint()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            // Check if camera is active
            Debug.LogError("Main Camera not found. Make sure you have a camera in the scene.");
            return Vector3.zero;
        }

        float cameraHeight = mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        Vector3 randomSpawnPoint = new Vector3(Random.Range(-cameraWidth, cameraWidth), Random.Range(-cameraHeight, cameraHeight), 0f);

        // Ensure the spawn point is outside the camera's view
        randomSpawnPoint.x += Mathf.Sign(randomSpawnPoint.x) * cameraWidth * 1.5f;
        randomSpawnPoint.y += Mathf.Sign(randomSpawnPoint.y) * cameraHeight * 1.5f;

        return randomSpawnPoint;
    }

    private void Update()
    {
        if (!isMoving)
        {
            // Move to player until first contact
            MoveTowardsPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {

        Vector2 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, player.position) < 1.0f)
        {
            isMoving = true;
            player.gameObject.SetActive(false);
        }
    }

    private IEnumerator PlaySoundWithRandomInterval()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));

            audioSource.clip = soundClip;
            audioSource.Play();
        }
    }

}
