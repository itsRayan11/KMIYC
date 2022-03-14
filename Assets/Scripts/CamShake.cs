using UnityEngine;

public class CamShake : MonoBehaviour
{
    [SerializeField] private Animator camShake;

    public void ShakeCamera()
    {
        camShake.SetTrigger("Shake");
    }
}