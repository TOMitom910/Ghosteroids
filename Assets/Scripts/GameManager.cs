using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject ennemyPrefabSmall;
    [SerializeField]
    private GameObject ennemyPrefabMedium;
    [SerializeField]
    private GameObject ennemyPrefabLarge;

    private GameObject newEnemy;

    public GameObject playerLife;
    
    public TextMeshProUGUI scoreText;
    
    public int actualRound;
    
    public float timeSurvived = 0f;
    
    public int playerPoints = 0;
    
    public int numberOfLives = 3;  // Nombre de vies du joueur
     
    
    public float spawnDistance = 10.0f;  // Distance de spawn par rapport au centre de la zone de jeu
    public int numberOfSmallerAsteroids = 2;  // Nombre d'astéroïdes plus petits à respawn
    public float minSplitAngle = 15f;  // Angle minimum de la direction de l'astéroïde plus petit
    public float maxSplitAngle = 45f;  // Angle maximum de la direction de l'astéroïde plus petit
    public float speed = 5f;  // Vitesse de l'astéroïde
    
    private void Start()
    {
        actualRound = 1;
        RoundManager("start");
    }
    
    private void Update()
    {
        timeSurvived += Time.deltaTime;
    }

    public void CreateEnnemy(string ennemyType, Vector2 spawnPosition, Vector2 direction)
    {
        Quaternion spawnRotation = Quaternion.Euler(0, 0, Random.Range(minSplitAngle, maxSplitAngle) * 1);
        
        switch (ennemyType)
        {
            case "SmallEnemy":
                newEnemy = Instantiate(ennemyPrefabSmall, spawnPosition, spawnRotation);
                break;
            case "MediumEnemy":
                newEnemy = Instantiate(ennemyPrefabMedium, spawnPosition, spawnRotation);
                speed = 3f;
                break;
            case "LargeEnemy":
                newEnemy = Instantiate(ennemyPrefabLarge, spawnPosition, spawnRotation);
                speed = 2f;
                break;
        }
        Rigidbody2D rbSmaller = newEnemy.GetComponent<Rigidbody2D>();
        if (rbSmaller != null)
        {
            rbSmaller.velocity = direction.normalized* speed;
        }
        
    }
    public void RoundManager(string etat)
    {
        Debug.Log("Round "+ actualRound +" started");
        //lancer un tour de jeu et finir le tour
        if (etat == "start")
        {
            StartCoroutine(StartRound());
        }
        else if (etat == "end")
        {
            StartCoroutine(EndRound());
        }
                
    }

    private IEnumerator StartRound()
    {
        for (int i = 0; i < 5 * actualRound; i++)
        {
            //Vector3 spawnPosition = new Vector2(Random.Range(-8.5f, 8.5f), Random.Range(-4.5f, 4.5f));
            Vector2 spawnPosition = Random.insideUnitCircle.normalized * spawnDistance;;
            CreateEnnemy("LargeEnemy", spawnPosition, (Vector2)gameObject.transform.position - spawnPosition);
            yield return new WaitForSeconds(5f / actualRound);
        }
        RoundManager("end");
    }
    
    private IEnumerator EndRound()
    {
        yield return new WaitForSeconds(5);
        if (GameObject.FindGameObjectWithTag("Enemy"))
        {
            StartCoroutine(EndRound());
        }
        else
        {
            actualRound++;
            RoundManager("start");
        }
    }
}
