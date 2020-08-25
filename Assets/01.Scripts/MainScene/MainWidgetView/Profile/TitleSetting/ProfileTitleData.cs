using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProfileTitleData", menuName = "Scriptable Object/ProfileTitleData", order = 0)]
public class ProfileTitleData : ScriptableObject {
    [SerializeField]
    private Title[] titleResources;
    public Title[] TitleResources => titleResources;
}