/*
 * 
 * Developed by Olusola Olaoye, 2021
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBackgroundColorBottomPanel : BottomPanel
{
    [SerializeField]
    private RectTransform color_container;

    [SerializeField]
    private Material background_material;

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public override void initializePanel()
    {
        foreach (Button button in color_container.GetComponentsInChildren<Button>())
        {
            button.onClick.AddListener(() => background_material.color = button.GetComponent<Image>().color
                );
        }
    }
}
