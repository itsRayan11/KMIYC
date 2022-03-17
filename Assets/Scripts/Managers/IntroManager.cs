using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private Animator transitionAnim;

    private void Start()
    {
        StartCoroutine(ToNextLevel());
    }

    private IEnumerator ToNextLevel()
    {
        yield return new WaitForSeconds(30f);
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync(2);
    }
}