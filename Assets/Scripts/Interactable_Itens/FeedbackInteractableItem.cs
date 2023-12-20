using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackInteractableItem : MonoBehaviour
{
    private RectTransform _rectTransformReference;
    [SerializeField]
    private Slider _interactableTimeSlider;
    [SerializeField]
    private Image _interactableTimeFillArea;
    // Start is called before the first frame update

    private void Awake()
    {
        _rectTransformReference = GetComponentInChildren<RectTransform>();
    }

    void Start()
    {
        _rectTransformReference.rotation = Quaternion.Euler(0, 0, 0);
    }

    // Update is called once per frame
    public void UpdateInteractableBar(float currentValue, float timeToInteract)
    {
        float sliderValue = 1 - currentValue/timeToInteract;
        if (sliderValue <= 1)
        {
            _rectTransformReference.rotation = Quaternion.Euler(0, 0, 0);

            _interactableTimeFillArea.color = Color.white;

            _interactableTimeSlider.value = sliderValue;
        }
        else
        {
            _interactableTimeFillArea.color = new Color(0, 0, 0, 0);
        }

    }
}
