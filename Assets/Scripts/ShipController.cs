using System;
using System.Collections;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float thrustForce;

    public Rigidbody2D rb;
    
    public GameObject projectilePrefab;  // Référence au prefab du projectile
    public Transform projectileSpawnPoint;  // Point de départ des projectiles
    public float projectileSpeed;  // Vitesse du projectile
    
    public float timeAbilitty = 10f;
    public bool isAbilityActive = false;
    
    private GameObject newPlayer;


    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }


    void Update()
    {
        // Déplacer le personnage
        Move();

        // Tourner le personnage pour qu'il fasse face à la souris
        RotateTowardsMouse();
        
        //tier un projectile
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<Animator>().SetTrigger("Shoot");  // On déclenche l'animation "Shoot"
            ShootProjectile();
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isAbilityActive)
        {
            CreateNewPlayer();
            isAbilityActive = true;
        }

        if (isAbilityActive)
        {
            timeAbilitty -= Time.deltaTime;
            if (timeAbilitty <= 0)
            {
                if(newPlayer)
                    Destroy(newPlayer); 
                timeAbilitty = 10f;
                StartCoroutine(waitForAbility());
            }
        }
    }
    
    private IEnumerator waitForAbility()
    {
        yield return new WaitForSeconds(10f);
        isAbilityActive = false;
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(moveX, moveY, 0).normalized;
        rb.AddForce(moveDirection * thrustForce);
        
        //diminution de la velocité dans le temps pour eviter que le personnage se deplace trop vite et sois incotrolable
        rb.velocity -= rb.velocity * 0.001f;
    }

    void RotateTowardsMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
        );
        transform.right = direction;
    }
    
    private void CreateNewPlayer()
    {
        // Instancier le personnage à partir du prefab
        newPlayer = Instantiate(gameObject, transform.position, Quaternion.identity);

        newPlayer.GetComponent<ShipController>().isAbilityActive = true;
        
        GoTo goTo = newPlayer.AddComponent<GoTo>();
        goTo.target = new Vector3(transform.position.x + (transform.position.x > 8.5 ? -2 : 2),transform.position.y,0);
    }
    
    
    void ShootProjectile()
    {
        
        // Instancier le projectile
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

        // Calculer la direction vers la souris
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction =  new Vector2(
            mousePosition.x - projectileSpawnPoint.position.x,
            mousePosition.y - projectileSpawnPoint.position.y).normalized;
        // Appliquer la vitesse au projectile
        Rigidbody2D rbProjectile = projectile.GetComponent<Rigidbody2D>();
        rbProjectile.velocity = direction * projectileSpeed;
    }
}