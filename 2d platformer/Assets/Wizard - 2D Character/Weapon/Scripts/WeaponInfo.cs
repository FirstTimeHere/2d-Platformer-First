using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInfo : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TextMeshProUGUI _value;
    [SerializeField] private Slider _slider;
    [SerializeField] private float _sliderSpeedChange;


    private Weapon _weapon;

    private void OnEnable()
    {
        _player.ChangedWeapon += ShowStats;
    }

    private void OnDisable()
    {
        _player.ChangedWeapon -= ShowStats;
    }

    //private void Update()
    //{
    //        _slider.value = Mathf.MoveTowards(_slider.value, mask.TempTime, Time.deltaTime * _sliderSpeedChange);
    //}

    private void ShowStats()
    {
        _weapon = _player.SetWeaponActive();

        if (_weapon is VampireMask mask)
        {

            //_slider.minValue = 0;
            //_slider.maxValue = mask.TempTime;
            _value.text = $"Текущее значение урона: {mask.Value}" + "\n"
                        + $"Перезарядка: {mask.TempTime}";
        }
        else
        {
            _value.text = $"Текущее значение урона: {_weapon.Value}" + "\n"
            + $"Перезарядка: {_weapon.ReloadTime}";
        }
    }
}
