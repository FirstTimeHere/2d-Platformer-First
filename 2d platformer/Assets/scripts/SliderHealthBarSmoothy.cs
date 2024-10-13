using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderHealthBarSmoothy : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private float _sliderSpeedChange;

    private float _delay = 0.01f;

    private Slider _slider;

    private Coroutine _coroutine;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = _health.Value;
        _slider.value = _health.CurrentPoint;
    }

    private void OnEnable()
    {
        _health.ChangedHealth += ChangeSlider;
        _health.Dead += StopSlider;
    }

    private void OnDisable()
    {
        _health.ChangedHealth -= ChangeSlider;
        _health.Dead -= StopSlider;
    }

    private void ChangeSlider()
    {
        _coroutine = StartCoroutine(GetChangeHealth(_delay));
    }

    private void StopSlider()
    {
        StopCoroutine(GetChangeHealth(_delay));
        _coroutine = null;
    }

    private IEnumerator GetChangeHealth(float delay)
    {
        var wait = new WaitForSeconds(delay);

        while (enabled)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, _health.CurrentPoint, Time.deltaTime * _sliderSpeedChange);
            yield return wait;
        }
    }
}
