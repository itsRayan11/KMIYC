using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class powerups : MonoBehaviour
{
    [SerializeField]float speedMultiplier = 1.1f;
    [SerializeField] Player footBall;
    [SerializeField] GameObject[] enemyScript;
    [SerializeField] float enemymultiplier = 0.5f;
    [SerializeField] GameObject pickupParticle;
    [SerializeField] GameObject defuffParticle;
    [SerializeField] TextMeshProUGUI scoreOnGUI;
    Enemy currEnemy;
    GameObject localGameObject;

 

    private void Awake()
    {
        footBall = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        enemyScript = GameObject.FindGameObjectsWithTag("Enemy");
       
        
    }
    public void power(GameObject bonuses) {
        if (bonuses.name.Contains("playerSpeed")) {
            localGameObject = bonuses;
          
            StartCoroutine(speedUp());
            GameObject particle = Instantiate(pickupParticle, localGameObject.transform.position, Quaternion.identity);
            Destroy(particle, 5f);

        }
        if (bonuses.name.Contains("enemySlow")) {
            localGameObject = bonuses;
    
            StartCoroutine(enemySlow());
            GameObject particle = Instantiate(pickupParticle, localGameObject.transform.position, Quaternion.identity);
            Destroy(particle, 5f);


        }
        if (bonuses.name.Contains("scoreBooster")) {
            localGameObject = bonuses;
            
            StartCoroutine(boostScore());
            GameObject particle = Instantiate(pickupParticle, localGameObject.transform.position, Quaternion.identity);
            Destroy(particle, 5f);
        }
        if (bonuses.name.Contains("scoreDebuff")) {
            localGameObject = bonuses;
            if (footBall.score >= 5) {
                footBall.score -= 5;
                footBall.scoreText.text = footBall.score.ToString();
            }
            GameObject particle = Instantiate(defuffParticle, localGameObject.transform.position, Quaternion.identity);
            Destroy(particle, 5f);
            Destroy(localGameObject);
        
        }

      
    }
    IEnumerator speedUp()
    {
        footBall.playerSpeed *= speedMultiplier;
        localGameObject.GetComponent<SpriteRenderer>().enabled = false;
        localGameObject.GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(3f);
        footBall.playerSpeed /= speedMultiplier;
        Destroy(localGameObject);
       

    }
    IEnumerator enemySlow() {
        for (int i = 0; i < enemyScript.Length-1; i++) {
            currEnemy = enemyScript[i].GetComponent<Enemy>();
            currEnemy.enemySpeed *= enemymultiplier;
        }
        localGameObject.GetComponent<SpriteRenderer>().enabled = false;
        localGameObject.GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(3f);
        for (int i = 0; i < enemyScript.Length-1; i++)
        {
            currEnemy = enemyScript[i].GetComponent<Enemy>();
            currEnemy.enemySpeed /= enemymultiplier;
        }
        Destroy(localGameObject);
        
        
    }
    IEnumerator boostScore() {
        footBall.increaseScore = 2;
        localGameObject.GetComponent<SpriteRenderer>().enabled = false;
        localGameObject.GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(15f);
        
        footBall.increaseScore = 1;
        Destroy(localGameObject);
    
    }
    IEnumerator scoreDebuff() {

        yield return new WaitForSeconds(0.5f);
    }
    

}
