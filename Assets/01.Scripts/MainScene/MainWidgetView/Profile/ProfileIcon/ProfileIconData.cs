using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class IconData{
    [SerializeField]
    private Sprite _icon;
    public Sprite icon => _icon;

    [SerializeField]
    private bool _isUnlock;
    public bool isUnlock {get => _isUnlock; set => _isUnlock = value;}

}

[CreateAssetMenu(fileName = "ProfileIconData", menuName = "Scriptable Object/ProfileIconData", order = 0)]
public class ProfileIconData : ScriptableObject {
    [SerializeField]
    private List<IconData> iconDatas = new List<IconData>();
    public int iconCount => iconDatas.Count;

    [SerializeField]
    private IconData lockedIcon;

    public IconData GetIcon(int index){
        return iconDatas[index].isUnlock ? iconDatas[index] : lockedIcon;
    }

    public void UnlockIcon(int index){
        iconDatas[index].isUnlock = true;
    }
}