using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class BackgroundEffectController : MonoBehaviour {

    [SerializeField]
    private bool editMode = false;
    
    private bool EditMode => editMode;
    private bool ReverseBoolean => !editMode;
    
    [ShowIf("ReverseBoolean")]
    [SerializeField]
    [Header("Objects")]
    private ParticleSystem particleSystem;
    
    [ShowIf("EditMode")]
    [SerializeField]
    [BoxGroup("Frequency")]
    [InfoBox("생성 빈도 값")]
    [OnValueChanged("OnValueChange")]
    private float frequency;
    
    [ShowIf("EditMode")]
    [SerializeField]
    [BoxGroup("Size")]
    [InfoBox("최소값과 최대값을 정하면 그 사이에서 랜덤으로 나옴")]
    [MinMaxSlider(0.0f, 1.0f)]
    [OnValueChanged("OnValueChange")]
    private Vector2 startSize;

    [ShowIf("EditMode")]
    [SerializeField]
    [BoxGroup("Alpha")]
    [InfoBox("이펙트 알파 값")]
    [MaxValue(1.0f)]
    [OnValueChanged("OnValueChange")]
    private float alpha;

    [ShowIf("EditMode")]
    [SerializeField]
    [BoxGroup("Move Speed")]
    [InfoBox("이펙트 이동 속도")]
    [OnValueChanged("OnValueChange")]
    private float moveSpeed;
    
    [ShowIf("EditMode")]
    [Button("Apply")]
    public void OnValueChange() {
        var particleMainSetting = particleSystem.main;

        particleMainSetting.startSize = new ParticleSystem.MinMaxCurve(startSize[0], startSize[1]);
        particleMainSetting.startColor = new Color(1, 1, 1, alpha);
        particleMainSetting.simulationSpeed = moveSpeed;

        var particleEmissionSetting = particleSystem.emission;

        particleEmissionSetting.rateOverTime = frequency;
    }
}
