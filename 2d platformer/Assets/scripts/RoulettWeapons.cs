using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class RoulletWeapons : MonoBehaviour
{
    private Player _player;
    private KeyCodes _keys = new KeyCodes();

    private List<Weapon> _weapons = new List<Weapon>();

    private int _indexWeapon = 0;

    public event Action ChangedWeapon;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _player.AddedWeapon += AddWeapon;
    }

    private void OnDisable()
    {
        _player.AddedWeapon -= AddWeapon;
    }

    private void Update()
    {
       ChangeWeapon();
    }

    private void ChangeWeapon()
    {
        if (Input.GetKeyUp(_keys.ChangeWeapon))
        {
            _indexWeapon = ++_indexWeapon % _weapons.Count;

            for (int i = 0; i < _weapons.Count; i++)
            {
                _weapons[i].gameObject.SetActive(false);
            }

            _weapons[_indexWeapon].gameObject.SetActive(true);
        }

        ChangedWeapon?.Invoke();
    }

    private void AddWeapon()
    {
        _weapons = _player.SetWeapons();
    }

    public Weapon SetWeaponActive()
    {
        return _weapons[_indexWeapon];
    }
}
