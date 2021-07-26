using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _background;
    [SerializeField] private Text _text;
    [SerializeField] private Color _normalBackgroundColor;
    [SerializeField] private Color _hoverBackgroundColor;

    [SerializeField] private Color _normalTextColor;
    [SerializeField] private Color _hoverTextColor;
    private void Start()
    {
        _background = GetComponent<Image>();
        _text = GetComponentInChildren<Text>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _background.color = _hoverBackgroundColor;
        _text.color = _hoverTextColor;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _background.color = _normalBackgroundColor;
        _text.color = _normalTextColor;
    }
}