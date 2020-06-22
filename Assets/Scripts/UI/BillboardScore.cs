using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BillboardScore : MonoBehaviour
{

    private Transform camTransform;
    private Quaternion originalRotation;
    private RectTransform rt;

    [SerializeField]
    private Text txtScore;

    [SerializeField]
    private Animator animator;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        originalRotation = transform.rotation;
        camTransform = Camera.main.transform;
    }

    public void Initialize(Vector3 pos, int score)
    {
        txtScore.text = score + "";
        transform.SetParent(UIManager.main.WorldSpaceCanvas);
        transform.position = pos;
        animator.SetTrigger("Show");
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        transform.rotation = originalRotation * camTransform.rotation;
    }

}
