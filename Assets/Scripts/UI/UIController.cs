using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    #region Unity Properties

    public static UIController instance;

    [SerializeField] private GameObject insideUiPanel;

	[SerializeField] private GameObject gameOverPanel;

    [SerializeField] private TMP_Text deadCount;

    [SerializeField] private TMP_Text coinCount;

    [SerializeField] private TMP_Text bulletDamageText;

    [SerializeField] private TMP_Text bulletSpeedText;

    [SerializeField] private TMP_Text gameOverCoinText;

    [SerializeField] private TMP_Text gameOverDeathBodyText;

    [SerializeField] private Image bulletDamageButton;

    [SerializeField] private Image bulletSpeedButton;

    #endregion

    #region Unity Methods
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        if (!PlayerPrefs.HasKey("SpeedCost"))
        {
            PlayerPrefs.SetInt("SpeedCost", 5);
        }

        if (!PlayerPrefs.HasKey("DamageCost"))
        {
            PlayerPrefs.SetInt("DamageCost", 5);
        }

        insideUiPanel.SetActive(true);
        CoinText();
        DeadCountText();
        
        bulletDamageText.text = PlayerPrefs.GetInt("DamageCost").ToString();
        bulletSpeedText.text = PlayerPrefs.GetInt("SpeedCost").ToString();

    }

    #endregion

    #region Methods
    public void CoinText()
    {
        coinCount.text = PlayerPrefs.GetInt("Coin").ToString();
        CheckDamageButtonColor();
        CheckSpeedButtonColor();
    }
    public void DeadCountText()
    {
        deadCount.text = PlayerPrefs.GetInt("DeadCount").ToString();
    }
    public void GameOverPanel()
    {
        insideUiPanel.SetActive(false);
        gameOverPanel.SetActive(true);
        gameOverCoinText.text=PlayerPrefs.GetInt("Coin").ToString() ;
        gameOverDeathBodyText.text= PlayerPrefs.GetInt("DeadCount").ToString();
        PlayerPrefs.SetInt("DeadCount", 0);
        PlayerPrefs.Save();
    }
    public void InsideUıPanel()
    {
        insideUiPanel.SetActive(true);
        gameOverPanel.SetActive(false);
    }
    public void RestartButton()
    {
        Time.timeScale= 1.0f;
        PlayerPrefs.SetInt("DeadCount", 0);
        PlayerPrefs.Save();
        InsideUıPanel();
        SceneManager.LoadScene(0);
    }
    public void BulletDamageButton()
    {
        if (PlayerPrefs.GetInt("Coin") >= PlayerPrefs.GetInt("DamageCost"))
        {
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") - PlayerPrefs.GetInt("DamageCost"));
            CoinText();
            PlayerPrefs.SetInt("Damage", PlayerPrefs.GetInt("Damage") * 2);
            PlayerPrefs.SetInt("DamageCost", PlayerPrefs.GetInt("DamageCost") +5);
            bulletDamageText.text= PlayerPrefs.GetInt("DamageCost").ToString();
        }
    }
    public void BulletSpeedButton()
    {
        if (PlayerPrefs.GetInt("Coin") >= PlayerPrefs.GetInt("SpeedCost"))
        {
            PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") - PlayerPrefs.GetInt("SpeedCost"));
            CoinText();
            PlayerPrefs.SetInt("Speed", PlayerPrefs.GetInt("Speed") * 2);
            PlayerPrefs.SetInt("SpeedCost", PlayerPrefs.GetInt("SpeedCost") +5);
        }


    }
    void CheckSpeedButtonColor()
    {
        if(PlayerPrefs.GetInt("Coin") >= PlayerPrefs.GetInt("SpeedCost"))
        {
            bulletSpeedButton.color = Color.white;
        }
        else
        {
            bulletSpeedButton.color = Color.grey;
        }
    }
    void CheckDamageButtonColor()
    {
        if (PlayerPrefs.GetInt("Coin") >= PlayerPrefs.GetInt("DamageCost"))
        {
            bulletDamageButton.color = Color.white;
        }
        else
        {
            bulletDamageButton.color = Color.grey;
        }
    }
    #endregion
}
