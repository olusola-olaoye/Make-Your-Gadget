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

public class ChangeGadgetColorBottomPanel : BottomPanel
{
  
    [SerializeField]
    private ChangeGadgetColorBottomPanelRow row_prefab;

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
        foreach(ChangeGadgetColorBottomPanelRow row in row_container.GetComponentsInChildren<ChangeGadgetColorBottomPanelRow>())
        {
            Destroy(row.gameObject);
        }
        //create row for each color class in the found gadget
        foreach(ColorClass color_class in FindObjectOfType<Gadget>().color_parts)
        {
            ChangeGadgetColorBottomPanelRow row = Instantiate(row_prefab);
            row.transform.SetParent(row_container, false);
            row.title_text.text = color_class.color_class_name;

            foreach(Button button in row.color_buttons_container.GetComponentsInChildren<Button>())
            {
                button.onClick.AddListener(() => colorMeshRenderes(button.GetComponent<Image>().color, color_class.color_parts)
                    );
            }
        }
    }

    private void colorMeshRenderes(Color color, MeshRenderer[] renderes)
    {
        foreach(MeshRenderer renderer in renderes)
        {
            renderer.material.SetColor("_Color", color);
        }
    }
}
