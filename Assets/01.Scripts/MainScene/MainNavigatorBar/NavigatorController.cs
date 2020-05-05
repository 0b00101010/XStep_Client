using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigatorController : MonoBehaviour, INavigatorButtonObserver
{
    [SerializeField]
    private NavigatorButton[] navigatorButtons;
    private NavigatorButton selectNavigatorButton;

    private List<Image> navigatorButtonImages = new List<Image>();

    [SerializeField]
    private Color selectColor;

    [SerializeField]
    private Color defaultColor;

    private void Awake(){
        for(int i = 0; i < navigatorButtons.Length; i++){
            navigatorButtonImages.Add(navigatorButtons[i].GetComponent<Image>());
            navigatorButtons[i].SettingObserver(this);
        }
        
        selectNavigatorButton = navigatorButtons[0];
        MainSceneManager.instance.uiController.SetInformationBoxText(selectNavigatorButton.TapName);
        ButtonColorSetting();
    }

    public void SelectNotify(NavigatorButton navigatorButton){
        selectNavigatorButton = navigatorButton;
        MainSceneManager.instance.uiController.SetInformationBoxText(selectNavigatorButton.TapName);
        ButtonColorSetting();
    }    

    private void ButtonColorSetting(){
        for(int i = 0; i < navigatorButtons.Length; i++){
            if(navigatorButtons[i].Equals(selectNavigatorButton)){
                navigatorButtonImages[i].color = selectColor;
                continue;
            }

            navigatorButtonImages[i].color = defaultColor;
            
        }
    }

    private void MoveWidgets(){
        // TODO : Widgets moving
    }

    private void OnDestroy(){
        for(int i = 0; i < navigatorButtons.Length; i++){
            navigatorButtons[i].RemoveObserver();
        }
    }
}
