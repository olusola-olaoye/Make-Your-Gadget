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
using System.IO;


public class FileChooserPanel : MonoBehaviour
{

    [SerializeField]
    private FileButton file_button_prefab;

    [SerializeField]
    private InputField location_input_field;

    [SerializeField]
    private RectTransform files_container;


    [SerializeField]
    private Button desktop_location_button;

    [SerializeField]
    private Button document_location_button;

    [SerializeField]
    private Button picture_location_button;



    public Button ok_button;

    public Button cancel_button;


    public string chosen_file
    {
        get;
        private set;
    }

    // Start is called before the first frame update
    void Start()
    {
        setDesktopLocation();

        desktop_location_button.onClick.AddListener(() => setDesktopLocation());
        document_location_button.onClick.AddListener(() => setDocumentLocation());
        picture_location_button.onClick.AddListener(() => setPictureLocation());

        cancel_button.onClick.AddListener(() => Destroy(gameObject));

    }

    private void setDesktopLocation()
    {
        location_input_field.text = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
        updateFilesList();
    }

     private void setDocumentLocation()
     {
        location_input_field.text = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        updateFilesList();
     }

     private void setPictureLocation()
     {
        location_input_field.text = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyPictures);
        updateFilesList();
     }



    private void updateFilesList()
    {

        clearFilesList();


        var directory_info = new DirectoryInfo(location_input_field.text);

        var files_info = directory_info.GetFiles();

        var folder_info = directory_info.GetDirectories();


        foreach (var file in files_info)
        {
            // if the file is an image
            if (file.Extension.ToLower() == ".png"
                ||
                file.Extension.ToLower() == ".jpg"
                ||
                file.Extension.ToLower() == ".jpeg")
                {
                    FileButton file_button = Instantiate(file_button_prefab);

                    file_button.transform.SetParent(files_container.transform, false);

                    file_button.text.text = file.Name;

                    file_button.button.onClick.AddListener(() => chosen_file = location_input_field.text + "/" + file.Name);
                }

        }
    }

    private void clearFilesList()
    {
        // clear file buttons that are children of the files container
        foreach (FileButton filebutton in files_container.GetComponentsInChildren<FileButton>())
        {
            Destroy(filebutton.gameObject);
        }
    }


}
