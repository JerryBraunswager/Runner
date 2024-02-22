using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class Bar : MonoBehaviour
{
    [SerializeField] private PlayerController _player;

    private Slider _slider;

    private void OnEnable()
    {
        _player.AbilityChargeChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        _player.AbilityChargeChanged -= OnValueChanged;
    }

    private void Start()
    {
        _slider = GetComponent<Slider>();
    }

    public void OnValueChanged(float value, float maxValue)
    {
        _slider.value = value / maxValue;
    }
}
