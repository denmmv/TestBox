using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoatController : MonoBehaviour
{
    private Rigidbody _goat;
    private Animator _animator;
    [SerializeField] private GameObject _deadCollider;
    private GameObject dead;

    private int _distantion;
    private int _speed;
    private int _direction;
    private Vector3 _move;
    private Vector3 _pointFinish;

    Coroutine GoatMove;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.applyRootMotion = false;
        _goat = this.GetComponent<Rigidbody>();
        _distantion = Main.instance.distantion;
        _speed = Main.instance.speed;
        _direction = ((int)Main.instance.dir);
        _move = new Vector3(0,0, -_speed);
        direction(_direction);
        Vector3 _pointFinish = _move * _distantion;
        GoatMove = StartCoroutine(goatMove(_move,_speed,_distantion));
        dead = Instantiate(_deadCollider, _pointFinish, Quaternion.Euler(0, 0, 0));
    }
    private void direction(int dir)
    {
        switch (dir)
        {
            case 0:
                _move = new Vector3(0,0,-1);
                break;
            case 1:
                _move = new Vector3(-1,0,0);
                break;
            case 2:
                _move = new Vector3(0,0,1);
                break;
            case 3:
                _move = new Vector3(1,0,0);
                break;
        }
    }
    IEnumerator goatMove(Vector3 dir, int spd, int dist)
    {
        while (true)
        {
            Debug.Log(_goat.transform.position);
            _goat.velocity = new Vector3(0, _goat.velocity.y,0) + _goat.transform.forward * -_speed;
            yield return new WaitForFixedUpdate();
        }              
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == dead)
        {
            StopCoroutine(GoatMove);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
