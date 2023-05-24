using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
    #region Unity Fields

    private Animator _animator;

    #endregion

    #region Methods

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    public void PlayIdle()
    {
        _animator.SetTrigger("Idle");
    }
    public void PlayRun()
    {
        _animator.SetTrigger("Run");
    }
    public void PlayDeath()
    {
        _animator.SetTrigger("Death");
    }

    #endregion

}
