using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NumberChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

    private NumberPos _numberPos;
    private ChangeNumberManager numberManager;
    [SerializeField]
    private bool isPlus;
    
    void Start() {
        _numberPos = GetComponentInParent<NumberPos>();
        numberManager = FindObjectOfType<ChangeNumberManager>();
    }

    public void OnMouseEnter() {
        Debug.Log("enter");
    }

    public void OnMouseExit() {
        Debug.Log("exit");
    }

    public void OnMouseDown() {
        if (isPlus) {
            OnMouseExit();
            numberManager.plusOne(_numberPos.getPos());
            OnMouseEnter();
        } else {
            OnMouseExit();
            numberManager.minusOne(_numberPos.getPos());
            OnMouseEnter();
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        numberManager.mouseOn(_numberPos.getPos(), isPlus);
    }

    public void OnPointerExit(PointerEventData eventData) {
        numberManager.mouseOver(_numberPos.getPos(), isPlus);
    }
    
    
    public void OnPointerClick(PointerEventData eventData) {
        if (isPlus) {
            OnPointerExit(eventData);
            numberManager.plusOne(_numberPos.getPos());
            OnPointerEnter(eventData);
        } else {
            OnPointerExit(eventData);
            numberManager.minusOne(_numberPos.getPos());
            OnPointerEnter(eventData);
        }
    }
}
