using UnityEngine;
using System;

[Serializable]
public class Title {
    [SerializeField]
    private string _title;
    public string title => _title;

    [SerializeField]
    private string _description;
    public string description => _description;

    [SerializeField]
    private bool _isUnLock;
    public bool isUnLock {get => _isUnLock; set => _isUnLock = value;}
}