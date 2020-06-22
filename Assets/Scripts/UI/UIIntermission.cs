using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIIntermission : MonoBehaviour
{
    [SerializeField]
    private Text txtTime;
    [SerializeField]
    private Text txtInfo;
    [SerializeField]
    private GameObject container;
    public void UpdateTime(float time) {
        if (!container.activeSelf) {
            container.SetActive(true);
        }
        txtTime.text = (int)time + "s";
    }

    public void UpdateInfo(string info) {
        txtInfo.text = info;
    }

    // Update is called once per frame
    public void Hide() {
        container.SetActive(false);
    }
}
