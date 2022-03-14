using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject CreditsUI;
    [SerializeField] private Animator transitionAnim;

    private void Start()
    {
        if (CreditsUI != null)
        {
            CreditsUI.SetActive(false);
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Play()
    {
        StartCoroutine(PlayGame());
    }

    private IEnumerator PlayGame()
    {
        if (PlayerPrefs.HasKey("HasDoneIntro"))
        {
            transitionAnim.SetTrigger("End");
            yield return new WaitForSeconds(1f);
            SceneManager.LoadSceneAsync(2);
        }
        else
        {
            transitionAnim.SetTrigger("End");
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene("Intro");
        }

        PlayerPrefs.SetString("HasDoneIntro", "yes");
    }

    public void Credits()
    {
        StartCoroutine(GameCredits());
    }

    private IEnumerator GameCredits()
    {
        yield return new WaitForSeconds(0.5f);
        CreditsUI.SetActive(true);
    }

    public void Quit()
    {
        StartCoroutine(QuitGame());
    }

    private IEnumerator QuitGame()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1f);
        Application.Quit();
    }

    public void BackButton()
    {
        Invoke("ToMenu", 0.5f);
    }

    private void ToMenu()
    {
        CreditsUI.SetActive(false);
    }
}