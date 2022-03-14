using UnityEngine;
using XGLabs;

public class PowerupSystem : MonoBehaviour
{
    [SerializeField] private float spawnAfter = 15;
    [SerializeField] private Transform[] spawnPowerups;
    [SerializeField] private GameObject[] powerPrefabs;
    [SerializeField] private Player playerScore;
    [SerializeField] SpriteRenderer mySprite;
    private GameObject bonuses;
    [SerializeField]spriteChanger mySpriteChanger;

    private void Start()
    {
        InvokeRepeating("SPowerups", spawnAfter, spawnAfter);
        playerScore = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        

    }
   

    private void SPowerups()
    {
        int randPower = Random.Range(0,4);
        int randSpawnPoint = Random.Range(0, spawnPowerups.Length - 1);
        if (bonuses == null && playerScore.score >= 3)
        {
            bonuses = Instantiate(powerPrefabs[randPower], spawnPowerups[randSpawnPoint].position, transform.rotation);
            if (bonuses.name.Contains("scoreDebuff")) {

                mySpriteChanger.change();
            }
        }
    }
    
}