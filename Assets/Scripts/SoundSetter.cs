using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSetter : MonoBehaviour
{
    [SerializeField]
    public Button[] buttons;

    private void Start()
    {
        foreach (Button btn in buttons)
        {
            btn.onClick.AddListener(() => SoundManager.Instance.PlaySE("clickSound"));
        }
    }
}
