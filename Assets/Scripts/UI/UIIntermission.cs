using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIIntermission : MonoBehaviour
{
    [SerializeField]
    private Text txtTime;
    [SerializeField]
    private GameObject container;
    public void UpdateTime(float time) {
        if (!container.activeSelf) {
            container.SetActive(true);
        }
        txtTime.text = (int)time + "s";
    }

    // Update is called once per frame
    public void Hide() {
        container.SetActive(false);
    }
}
