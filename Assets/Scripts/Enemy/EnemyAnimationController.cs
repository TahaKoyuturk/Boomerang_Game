using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class EnemyAnimationController : MonoBehaviour
{
    private Animator _animator;

    #region Unity Methods
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    #endregion

    #region Methods
    public void PlayRun()
    {
        _animator.SetTrigger("Run");
    }
    public void PlayThrow()
    {
        _animator.SetTrigger("Throw");
    }
    public void PlayDie()
    {
        _animator.SetTrigger("Die");
    }
    #endregion

}
