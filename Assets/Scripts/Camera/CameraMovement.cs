using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMovement : MonoBehaviour
{
    #region Public Properties
    [SerializeField] private Transform _player;

    [SerializeField] private float _speed;
    #endregion

    #region Private Properties
    private Transform _camera;

    private Vector3 _offset;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        _camera = this.transform;

        _offset = _camera.position - _player.position;
    }
    private void Update()
    {
        Follow();
    }

    #endregion

    #region Methods
    private void Follow()
    {
        _camera.DOMoveX(_player.position.x + _offset.x, _speed * Time.deltaTime);
        _camera.DOMoveZ(_player.position.z + _offset.z, _speed * Time.deltaTime);
    }
    #endregion

}
