using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    #region Properties

    [SerializeField] private EnemyAnimationController _animationController;
    [SerializeField] NavMeshAgent agent;

    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private int health = 1;

    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float bulletSpeed = 10f;

    private Rigidbody rb;
    private Transform player;
    
    private float lastFireTime;
    
    //the bool that checks the enemy is dead or not
    private bool isDead = false;
    #endregion

    #region Unity Methods
    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        _animationController.PlayRun();

        if (!PlayerPrefs.HasKey("Damage"))
            PlayerPrefs.SetInt("Damage", PlayerPrefs.GetInt("Damage"));
        else
            PlayerPrefs.SetInt("Damage", 1);
    }

    [System.Obsolete]
    private void FixedUpdate()
    {

        player = FindObjectOfType<PlayerController>().transform;
        //The place that allows enemy skeletons to run towards the player
        agent.SetDestination(player.position);

        //The place that tells how close the enemy skeletons are to the player that he should stop
        agent.stoppingDistance = 7f;
        
        if (!isDead)
        {
            if ((Time.time > lastFireTime + 1f / fireRate) && agent.remainingDistance <= 8f)
            {
                agent.Stop();
                _animationController.PlayThrow();
                
                lastFireTime = Time.time;
                Shoot();
            }
            else
            {
                _animationController.PlayRun();
                agent.Resume();
            }
                
        }
    }
    [System.Obsolete]
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bummerang"))
        {
            health-= PlayerPrefs.GetInt("Damage");

            if (health <= 0)
            {
                isDead = true;

                _animationController.PlayDie();
                //Stops the skeleton enemy
                agent.Stop();
                //Setting dead skeleton enemy count
                PlayerPrefs.SetInt("DeadCount", PlayerPrefs.GetInt("DeadCount") + 1);

                if(PlayerPrefs.GetInt("DeadCount") == 20)
                {
                    Time.timeScale = 0.0f;
                    UIController.instance.GameOverPanel();
                }
                UIController.instance.DeadCountText();

                //Stopping the addforce of the boomerang object
                rb.isKinematic = true;

                //Destroying bumerang
                Destroy(collision.gameObject);

                rb.isKinematic = true;

                StartCoroutine(EnemyDeathTimer());
            }
        }
    }
    #endregion

    #region Methods
    //The shooting function that provide shooting to the player target
    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * bulletSpeed;
        Destroy(bullet, 2f);
    }
    //Enemy Death Timer for destroying enemy object
    IEnumerator EnemyDeathTimer()
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
    public int GetHealth()
    {
        return health;
    }
    #endregion

}
