using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBarSprite;

    public static HealthBar Instance;

    Camera camera;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        camera = Camera.main;
    }

    public void FixedUpdate()
    {
        transform.forward = camera.transform.forward;
    }

    public void UpdateHealthBar(float maxHealth, float curremtHealth)
    {
        healthBarSprite.fillAmount = curremtHealth / maxHealth;
    }
}
