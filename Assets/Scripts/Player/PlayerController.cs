using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    #region Properties
    [SerializeField] private FloatingJoystick _joystick;

    [SerializeField] private float moveSpeed;

    [SerializeField] private float rotateSpeed;

    [SerializeField] private float maxHealth {get => 5;}

    [SerializeField] private HealthBar healthBar;

    private PlayerAnimationController _animationController;

    private Rigidbody _rb;

    private Vector3 _moveVector;

    private bool isDead;

    private float currentHealth;
    #endregion

    #region Public Fields
    public float MaxHealth
    {
        get { return maxHealth; }
    }
    public float PlayerHealth {
        get { return currentHealth; }
        set { currentHealth = value; }
    }
    #endregion

    #region Unity Methods
    private void Start()
    {
        _animationController = GetComponent<PlayerAnimationController>();

        _rb = GetComponent<Rigidbody>();

        isDead = false;

        currentHealth = maxHealth;

        _rb.isKinematic= false;

        healthBar.UpdateHealthBar(maxHealth, currentHealth);

        //Checks the saving mechanics if the coin saving mechanics are present or absent
        if (!PlayerPrefs.HasKey("Coin"))
        {
            PlayerPrefs.SetInt("Coin", 0);
        }
    }
    private void Update()
    {
        //If the player is not dead
        if (!isDead)
        {
            Move();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            //Updating Health Bar
            healthBar.UpdateHealthBar(maxHealth, currentHealth);
            
            //Stops bullet's addforce power
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;

            //Decreasing current health
            currentHealth--;

            //If player is dead
            if (currentHealth <= 0 && !isDead)
            {
                isDead = true;

                //Provides us to not move after death
                _rb.isKinematic = true;

                _animationController.PlayDeath();

                //This invoke waits for the player's timer for the player's death animation
                Invoke("DeathTimer", 2);
            }
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Coin"))
        {
            AudioController.Instance.PlayCoinSound();
            //Updates Coin
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + 1);

            //Updating coin text
            UIController.instance.CoinText();

            Destroy(collision.gameObject);
        }
    }
    #endregion

    #region Methods

    //The function that provides player move by using joystick
    private void Move()
    {
        _moveVector = Vector3.zero;
        //movement on the x-axis
        _moveVector.x = _joystick.Horizontal * moveSpeed * Time.deltaTime;
        //movement on the z-axis
        _moveVector.z = _joystick.Vertical * moveSpeed * Time.deltaTime;

        //Controlling that player move by using joystick
        if(_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            Vector3 direction = Vector3.RotateTowards(transform.forward,_moveVector,rotateSpeed* Time.deltaTime,0.0f);

            transform.rotation = Quaternion.LookRotation(direction);

            _animationController.PlayRun();
        }
        //Controlling that player doesn't move by using joystick
        else if (_joystick.Horizontal == 0 && _joystick.Vertical == 0)
            _animationController.PlayIdle();
        
        //Movement force
        _rb.MovePosition(_rb.position + _moveVector);
    }
    private void DeathTimer()
    {
        Time.timeScale = 0.0f;

        UIController.instance.GameOverPanel();
    }
    #endregion

}
