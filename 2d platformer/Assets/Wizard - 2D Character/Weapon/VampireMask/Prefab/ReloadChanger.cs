using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ReloadChanger : MonoBehaviour
{
    [SerializeField] private VampireMask _mask;
    [SerializeField] private float _speedSliderChange;

    private float _delay = 0.01f;

    private Slider _slider;

    private WaitForSeconds _wait;

    private Coroutine _coroutine;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.value = _mask.ReloadTime;
        _slider.maxValue = _mask.ReloadTime;
        _wait = new WaitForSeconds(_delay);
    }

    private void OnEnable()
    {
        _mask.GetUse += Reload;
        _mask.Reloaded += StopCorutine;
    }

    private void OnDisable()
    {
        _mask.GetUse -= Reload;
        _mask.Reloaded -= StopCorutine;
    }

    private void Reload()
    {
        _coroutine = StartCoroutine(GetChangeReaload());
    }

    private void StopCorutine()
    {
        StopCoroutine(GetChangeReaload());
        _coroutine = null;
    }

    private IEnumerator GetChangeReaload()
    {
        while (enabled)
        {
            _slider.value = Mathf.Lerp(_slider.value, _mask.ReloadTime, Time.deltaTime * _speedSliderChange);
            yield return _wait;
        }
    }
}
