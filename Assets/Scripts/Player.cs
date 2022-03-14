using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] public float playerSpeed = 30f;

    [SerializeField] private float turningSpeed = 3f;
    [SerializeField] private float kickOffForce = 20f;
    [SerializeField] private Transform[] goalPoint = new Transform[10];
    [SerializeField] private powerups playerPower;

    [Space(10f)]
    [Header("Score System")]
    [SerializeField] public TextMeshProUGUI scoreText;
    [SerializeField] public TextMeshProUGUI scoreTitle;
    [SerializeField] public TextMeshProUGUI deathScoreText;

    [Space(10f)]
    [Header("Misc")]
    [SerializeField] private Animator playerAnim;

    [SerializeField] private GameObject DeathUI;
    [SerializeField] private ParticleSystem scoreParticles;
    [SerializeField] private AudioSource playerAudio;
    [SerializeField] private AudioClip kickedSFX;
    [SerializeField] private AudioClip scoreSFX;
    [SerializeField] private AudioClip powerupSFX;
    

    //local variables
    private float horizontalInput;

    private float verticalInput;
    private float rotationAngle;
    public int score;
    public int increaseScore = 1;

    private ScoreSystem scoreSystem;
    private CamShake cameraShake;
    internal bool isDead;
    ParticleSystem scoreParticlesDestroy;

    //Components
    private Rigidbody2D rb;

    private void Awake()
    {
        Time.timeScale = 1f;
        rb = GetComponent<Rigidbody2D>();
        scoreSystem = GameObject.Find("[ SCORE MANAGER ]").GetComponent<ScoreSystem>();
        cameraShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamShake>();
    }

    public void invokingScript()
    {
        playerPower = GameObject.FindGameObjectWithTag("Powerup").GetComponent<powerups>();
    }

    private void Start()
    {
        score = 0;
        scoreText.text = score.ToString();
        if (deathScoreText != null)
        {
            deathScoreText.text = score.ToString();
        }
        DeathUI.SetActive(false);
    }

    private void Update()
    {
        if (isDead)
            Invoke("DeathScreen", 0.5f);

        if (isDead == true) return;
        Vector2 inputVector = Vector2.zero;
        inputVector.x = Input.GetAxisRaw("Horizontal");
        inputVector.y = Input.GetAxisRaw("Vertical");
        SetInputVector(inputVector);

        if (horizontalInput >= 1)
        {
            playerAnim.SetBool("isRunning", true);
        }
        else
        {
            playerAnim.SetBool("isRunning", false);
        }
    }

    private void FixedUpdate()
    {
        if (isDead == true) return;
        Movement();
        Rotation();
    }

    private void Movement()
    {
        Vector2 forceVector = horizontalInput * playerSpeed * transform.up;
        rb.AddForce(forceVector, ForceMode2D.Force);
    }

    private void Rotation()
    {
        rotationAngle -= verticalInput * turningSpeed;
        rb.MoveRotation(rotationAngle);
    }

    private void SetInputVector(Vector2 inputVector)
    {
        verticalInput = inputVector.x;
        horizontalInput = inputVector.y;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            cameraShake.ShakeCamera();
            playerAudio.PlayOneShot(kickedSFX);
            Vector3 dir = (collision.transform.position - transform.position).normalized;
            Vector3 goalDir = (goalPoint[Random.Range(0, goalPoint.Length)].position).normalized;
            rb.AddForce(goalDir * kickOffForce, ForceMode2D.Impulse);
        }

        if (collision.gameObject.CompareTag("Score"))
        {
            score += increaseScore;
            playerAudio.PlayOneShot(scoreSFX);
            scoreParticlesDestroy = Instantiate(scoreParticles, transform.position, Quaternion.identity);
            Destroy(scoreParticlesDestroy.gameObject,5f);
            scoreText.text = score.ToString();
            deathScoreText.text = score.ToString();
            scoreSystem.SpawnPoints();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Powerup"))
        {
            invokingScript();
            playerAudio.PlayOneShot(powerupSFX);
            playerPower.power(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Goal"))
        {
            isDead = true;
        }
        else
        {
            isDead = false;
        }
    }

    private void DeathScreen()
    {
        Time.timeScale = 0.1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        DeathUI.SetActive(true);
        scoreText.gameObject.SetActive(false);
        scoreTitle.gameObject.SetActive(false);
    }
}