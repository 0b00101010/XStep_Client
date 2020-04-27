using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewSongInformation : MonoBehaviour
{
    [SerializeField]
    private Image backgroundImage;
    
    [SerializeField]
    private Image[] childWidgetsImage;

    [SerializeField]
    private Text[] childWidgetsText;

    private List<object> childWidgets = new List<object>();

    private void Awake(){
        childWidgets.AddRange(childWidgetsImage);
        childWidgets.AddRange(childWidgetsText);
    }
    public void OpenSongInformation(){
        GameManager.instance.widgetViewer.WidgetsOpen(backgroundImage, childWidgets.ToArray());
    }

    public void CloseSongInformation(){
        GameManager.instance.widgetViewer.WidgetsClose(backgroundImage, childWidgets.ToArray());
    }

}
