using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatlhBoost : MonoBehaviour
{
    [SerializeField] private float increaseAmount = 1;
    [SerializeField] private float increaseDelay = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
             StartCoroutine(IncreaseHealthAmount(other.gameObject));
        }   
    }
    IEnumerator IncreaseHealthAmount(GameObject obj)
    {
        if(obj.GetComponent<PlayerController>().PlayerHealth<= obj.GetComponent<PlayerController>().MaxHealth)
        {
            obj.GetComponent<PlayerController>().PlayerHealth += increaseAmount;
            HealthBar.Instance.UpdateHealthBar(obj.GetComponent<PlayerController>().MaxHealth, obj.GetComponent<PlayerController>().PlayerHealth);
        }
        yield return new WaitForSeconds(increaseDelay);
        
    }
}
