using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.UI;

public class HelpButton : MainUIObject {
    [SerializeField]
    private GameObject pannerObject;

    [SerializeField]
    private Image background;

    [SerializeField]
    private CanvasGroup canvasGroup;

    
    public override void Execute() {
        pannerObject.gameObject.SetActive(true);
        GameManager.Instance.widgetViewer.WidgetsOpen(background, canvasGroup);
        GameManager.Instance.SomeUIInteraction = true;
    }

    public void CloseHelpPanner() {
        GameManager.Instance.widgetViewer.WidgetsClose(background, () => {
            pannerObject.gameObject.SetActive(false);
            GameManager.Instance.SomeUIInteraction = false;
        },canvasGroup);
    }

}
