using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangeShoot : MonoBehaviour
{
    #region Public Properties
    [SerializeField] private GameObject boomerangPrefab;
    [SerializeField] private AnimationCurve animationCurve;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Speed"))
        {
            PlayerPrefs.SetInt("Speed", PlayerPrefs.GetInt("Speed"));
        }
        else
        {
            PlayerPrefs.SetInt("Speed", 10);
        }
    }
    #endregion

    #region Methods
    public void ShootEnemy(Transform enemyPos)
    {
        AudioController.Instance.PlayBummerangSound();
        GameObject bumerang = Instantiate(boomerangPrefab, transform.position, Quaternion.identity);
        Vector3 direction = (enemyPos.position - transform.position).normalized;
        bumerang.GetComponent<Rigidbody>().velocity = direction * PlayerPrefs.GetInt("Speed");

        //bumerang.transform.DOMove(enemyPos.position, 1f).SetEase(animationCurve);
        //bumerang.transform.DOJump(enemyPos.position, 2f, 2, 1f, true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && other.gameObject.GetComponent<EnemyController>().GetHealth()!=0)
        {
            ShootEnemy(other.gameObject.transform);
        }
    }
    #endregion

}
