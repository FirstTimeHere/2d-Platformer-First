using TMPro;
using UnityEngine;

public class WeaponInfo : MonoBehaviour
{
    [SerializeField] private RoulletWeapons _roulletWeapons;
    [SerializeField] private TextMeshProUGUI _value;

    private Weapon _weapon;

    private void OnEnable()
    {
        _roulletWeapons.ChangedWeapon += ShowStats;
    }

    private void OnDisable()
    {
        _roulletWeapons.ChangedWeapon -= ShowStats;
    }

    private void ShowStats()
    {
        _weapon = _roulletWeapons.SetWeaponActive();

        _value.text = GetText(_weapon.Value, _weapon.ReloadTime);
    }

    private string GetText(float value, float reloadTime)
    {
        string text = $"Текущее значение урона: {value}" + "\n"
                        + $"Перезарядка: {reloadTime}";

        return text;
    }
}
