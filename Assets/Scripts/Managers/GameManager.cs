using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject PauseUI;
    private bool isPaused = false;
    private Player player;

    [SerializeField] Toggle minimapToggle;
    [SerializeField] GameObject Minimap;
    [SerializeField] Toggle guidingArrowToggle;
    [SerializeField] GameObject GuidingArrow;

    private void Start()
    {
        PauseUI.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        if (player.isDead == true) return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused == true)
            {
                Time.timeScale = 1.0f;
                PauseUI.SetActive(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                isPaused = false;
            }
            else
            {
                Time.timeScale = 0f;
                PauseUI.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                isPaused = true;
            }
        }
    }

    public void ToMenu()
    {
        Time.timeScale = 1.0f;
        StartCoroutine(MainMenu());
    }

    private IEnumerator MainMenu()
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(0);
    }

    public void Resume()
    {
        Time.timeScale = 1.0f;
        PauseUI.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Restart()
    {
        Time.timeScale = 1.0f;
        StartCoroutine(RestartGame());
    }

    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadSceneAsync(2);
    }

    public void MinimapToggle()
    {
        if (minimapToggle.isOn)
        {
            Minimap.gameObject.SetActive(true);
        }
        else
        {
            Minimap.gameObject.SetActive(false);
        }
    }

    public void ArrowToggle()
    {
        if (guidingArrowToggle.isOn)
        {
            GuidingArrow.gameObject.SetActive(true);
        }
        else
        {
            GuidingArrow.gameObject.SetActive(false);
        }
    }
}