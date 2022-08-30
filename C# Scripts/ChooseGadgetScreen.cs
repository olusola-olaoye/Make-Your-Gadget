/*
 * 
 * Developed by Olusola Olaoye, 2021
 * 
 
 * 
 */

using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
public class ChooseGadgetScreen : MonoBehaviour
{
    [SerializeField]
    [Range(1, 3)]
    private float zoom_speed = 1;

    [SerializeField]
    [Range(1, 10)]
    private float turn_speed = 3;

    [SerializeField]
    private Camera camera_;

    [SerializeField]
    private Gadget[] all_gadgets;

    public Gadget current_gadget
    {
        get;
        set;
    }

    // this number is what would be used to go through list of gadgets in a particular category
    private int current_minimum_gadget_postion = 0;

    [SerializeField]
    private MapButtonToBottomPanel[] buttons_to_buttom_sheet;

    private BottomPanel active_bottom_panel;

    private EventSystem event_system;

    private void OnEnable()
    {
        event_system = FindObjectOfType<EventSystem>();
        spawnCurrentGadget();

        foreach(MapButtonToBottomPanel map in buttons_to_buttom_sheet)
        {
            map.button.onClick.RemoveAllListeners();
            map.button.onClick.AddListener(() => hideAllButtomSheetsExcept(map.panel));
        }
        
    }
    
    private void hideAllButtomSheetsExcept(BottomPanel panel)
    {
        foreach (MapButtonToBottomPanel map in buttons_to_buttom_sheet)
        {
            map.panel.gameObject.SetActive(false);
        }
        active_bottom_panel = panel;
        panel.gameObject.SetActive(true);
    }

    // destroy previous existing gadget and spawn new one
    private void spawnCurrentGadget()
    {
        if(current_gadget)
        {
            Destroy(current_gadget.gameObject);
        }

        current_gadget = Instantiate(getCurrentGadget());

    }
    
    private Gadget getCurrentGadget()
    {
        for (int i = current_minimum_gadget_postion; i < all_gadgets.Length; i++)
        {
            if (all_gadgets[i].category == ApplictionController.Instance.category)
            {
                current_minimum_gadget_postion = i;
                return all_gadgets[i];
            }
        }
        for (int i = 0; i < all_gadgets.Length; i++)
        {
            if (all_gadgets[i].category == ApplictionController.Instance.category)
            {
                current_minimum_gadget_postion = i;
                return all_gadgets[i];
            }
        }
        return all_gadgets[0];
    }

    public void nextGadget()
    {
        current_minimum_gadget_postion += 1;
        current_minimum_gadget_postion = Mathf.Clamp(current_minimum_gadget_postion, 0, all_gadgets.Length);
        spawnCurrentGadget();
        active_bottom_panel.initializePanel();
    }

    public void previouGadget()
    {
        current_minimum_gadget_postion -= 1;
        current_minimum_gadget_postion = Mathf.Clamp(current_minimum_gadget_postion, 0, all_gadgets.Length);
        spawnCurrentGadget();
        active_bottom_panel.initializePanel();
    }

    private void Update()
    {
        backButtonPressed();
        updateZoomAndTurnGesture();
    }

    private void backButtonPressed()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            // go back home
            ApplictionController.Instance.current_application_state = ApplictionController.Appstate.Home;
        }
    }

    public void updateZoomAndTurnGesture()
    {
        zoomGesture();
        turnGesture();
    }

    // zoom camera via field of view
    private void zoomGesture()
    {
        camera_.fieldOfView += Input.GetAxis("Vertical") * zoom_speed ;
        camera_.fieldOfView = Mathf.Clamp(camera_.fieldOfView, 30, 70);
    }

    // rotate current gadget on y axis
    private void turnGesture()
    {
        current_gadget.transform.rotation = Quaternion.Euler(
                            current_gadget.transform.rotation.eulerAngles.x,
                            current_gadget.transform.rotation.eulerAngles.y +( Input.GetAxis("Horizontal") * turn_speed),
                            current_gadget.transform.rotation.eulerAngles.z);
    }

    public void toggleGadgetAvatar()
    {
        if(current_gadget.avatar)
        {
            current_gadget.avatar.SetActive(!current_gadget.avatar.activeSelf);
        }
    }

    public void takeScreenShot()
    {
        StartCoroutine(processScreenShot());
    }
    private IEnumerator processScreenShot()
    {
        
        Canvas canvas = FindObjectOfType<Canvas>();
        canvas.enabled = false;
        yield return new WaitForEndOfFrame();
        // save in desktop location
        ScreenCapture.CaptureScreenshot(System.Environment.GetFolderPath(
                                        System.Environment.SpecialFolder.Desktop) + "/" + stripSlashesColumnsAndSpacesAway(DateTime.Now.ToString()) + ".png");
        canvas.enabled = true;
        yield return null;
    }

    // remove slashes from a string
    private string stripSlashesColumnsAndSpacesAway(string word)
    {
        string formatted_word = word.Replace("/", "");
        formatted_word = formatted_word.Replace(" ", "");
        formatted_word = formatted_word.Replace(":", "");

        return formatted_word;
    }


    [System.Serializable]
    private class MapButtonToBottomPanel // display a bottom sheet when a button is clicked
    {
        public Button button;
        public BottomPanel panel;
    }
}
