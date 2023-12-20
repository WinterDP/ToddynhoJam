using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaFeedback : MonoBehaviour
{
    private RectTransform _rectTransformReference;
    private PlayerStamina playerStamina;
    [SerializeField]
    private Slider staminaSlider;
    [SerializeField]
    private Image _staminaSliderFillArea;

    private void Awake()
    {
        _rectTransformReference = GetComponentInChildren<RectTransform>();
    }
    void Start()
    {
        playerStamina = GetComponent<PlayerStamina>();
        staminaSlider.value = playerStamina.MaxStamina / playerStamina.MaxStamina;
    }

    public void UpdateStaminaBar(float currentValue)
    {
        if (currentValue != playerStamina.MaxStamina)
        {
            _rectTransformReference.rotation = Quaternion.Euler(0, 0, 0);

            if (currentValue < playerStamina.MinStaminaToMove)
            {
                _staminaSliderFillArea.color = new Color(.8f, 0, .4f);
            }
            else
            {
                _staminaSliderFillArea.color = Color.white;
            }
            staminaSlider.value = currentValue / playerStamina.MaxStamina;
        }
        else
        {
            _staminaSliderFillArea.color = new Color(0, 0, 0, 0);
        }
        
    }

}
