using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class VampireMask : Weapon
{
    [SerializeField] private float _value;
    [SerializeField] private float _direction;
    [SerializeField] private float _precentHeal;
    [SerializeField] private float _time;

    [SerializeField] private LayerMask _targetMask;


    private KeyCodes _keys = new KeyCodes();

    private Vector2 _directionVampire;

    /// <summary>
    /// делаем луч на определенное (короткое) расстояние и проверяем если есть враг запускает отсчет по времмени
    /// например секунд 5
    /// и преобразовываем процент хила от урона (все преобразуется через инспектор (процент, урон)
    /// 
    /// также сделать выбор между оружиями, если реализован подбор
    /// делать неактивными оружие, есди его заменили (потом возвращать)
    /// </summary>

    public override float Value => _value;


    private void Awake()
    {
        _directionVampire = new Vector2(_direction, 0);
    }

    private void Update()
    {
        if (enabled)
        {
            Debug.DrawRay(transform.position,_directionVampire, Color.red);
            GetUse();
        }
    }

    private void GetUse()
    {
        if (Input.GetKey(_keys.Attack) && IsItEnemy())
        {
            StartCoroutine(GetTimerAblity(_time));
        }
        else
        {
            StopCoroutine(GetTimerAblity(0));
        }
    }

    private bool IsItEnemy()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.right, _direction, _targetMask);

        if (hit)
        {
            if (hit.collider.TryGetComponent(out Enemy enemy))
            {
                Debug.Log(enemy.Damage);
                return true;
            }
        }

        return false;
    }

    private IEnumerator GetTimerAblity(float time)
    {
        var wait = new WaitForSeconds(time);

        while (_time >= 0)
        {
            _time--;
            yield return wait;
        }
    }
}