using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileIconHandler : MonoBehaviour
{
    private ProfileIconData _iconData;
    public ProfileIconData iconData => _iconData;

    private void Awake(){
        _iconData = Resources.Load<ProfileIconData>("Player Setting/IconData");
    }
}
