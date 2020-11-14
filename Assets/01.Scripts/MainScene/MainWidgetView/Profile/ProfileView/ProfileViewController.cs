using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProfileViewController : MonoBehaviour
{   
    [Header("Viewer")]
    [SerializeField]
    private GameObject viewerObject;

    [SerializeField]
    private Image background;

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private TextMeshProUGUI titleText;

    private bool isOpen;
    private bool isClosing;

    private List<ProfileSettingView> settingViews = new List<ProfileSettingView>();
    private ProfileSettingView currentOpenView;

    private void Start(){
        var settingViews = gameObject.GetComponentsInChildren<ProfileSettingView>(true);
        foreach(var view in settingViews){
            view.Execute();
            view.gameObject.SetActive(false);

            this.settingViews.Add(view);
        }

        viewerObject.gameObject.SetActive(false);
    }

    public void OpenView(int index){
        currentOpenView.Exit();
        currentOpenView = settingViews[index];
        currentOpenView.Execute();
        titleText.text = currentOpenView.titleString;
    }

    public void OpenWidget(){
        if(!isOpen && !GameManager.Instance.SomeUIInteraction){
            viewerObject.gameObject.SetActive(true);
            settingViews[0].gameObject.SetActive(true);

            currentOpenView = settingViews[0];

            GameManager.Instance.widgetViewer.WidgetsOpen(background, canvasGroup);
            GameManager.Instance.SomeUIInteraction = true;
            isOpen = true;
        }
    }

    public void CloseWidget(){
        if(!isClosing){
            GameManager.Instance.widgetViewer.WidgetsClose(background, () => {
                GameManager.Instance.SomeUIInteraction = false;
                isClosing = false;
                isOpen = false;

                viewerObject.gameObject.SetActive(false);

                settingViews.ForEach((view) => {
                    view.Exit();
                });
            }, canvasGroup);

            isClosing = true;
        }
    }
    
}
