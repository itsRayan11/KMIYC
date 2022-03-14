using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject scorePrefab;
    [SerializeField] GameObject scorePointer;
    GameObject spawnedScore;
    

    private void Update()
    {

        SpawnPoints();
        pointToScore();

    }
    public void SpawnPoints()
    {
        if (spawnedScore == null)
        {
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);
            spawnedScore = Instantiate(scorePrefab, spawnPoints[randSpawnPoint].position, transform.rotation);
            
            StartCoroutine(selfDestruct());
        }

    }
    void pointToScore() {
       
        if (spawnedScore != null)
        {
            var offset = -100f;
            Vector2 direction = spawnedScore.transform.position - scorePointer.transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            scorePointer.transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
        }
        else {
            scorePointer.transform.rotation = Quaternion.identity;
        
        }

    }
    IEnumerator selfDestruct()
    {
        yield return new WaitForSeconds(30f);
        Destroy(spawnedScore);
    }

}
