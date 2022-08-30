/*
 * 
 * Developed by Olusola Olaoye, 2021
 * 
 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGadgetPartsBottomPanel : BottomPanel
{

    [SerializeField]
    private ChangeGadgetPartsBottomPanelRow row_prefab;

    [SerializeField]
    private RectTransform row_container;


    protected override void OnEnable()
    {
        base.OnEnable();
    }

    public override void initializePanel()
    {
        base.initializePanel();
        // destroy any row that may exist in container
        foreach (ChangeGadgetPartsBottomPanelRow row_ in row_container.GetComponentsInChildren<ChangeGadgetPartsBottomPanelRow>())
        {
            Destroy(row_.gameObject);
        }

        ChangeGadgetPartsBottomPanelRow row = Instantiate(row_prefab);
        row.transform.SetParent(row_container, false);
        row.title_text.text = "Spare Parts";
        row.parts_slider.minValue = 0;

        // slider value should be from 0 to the number of spare parts in the found gdget,  minus 1
        row.parts_slider.maxValue = FindObjectOfType<Gadget>().spare_parts.Length - 1;

        // select spare part based on slider value
        row.parts_slider.onValueChanged.AddListener(delegate { FindObjectOfType<Gadget>().showSparePartIndex((int)row.parts_slider.value); });
    }

}
