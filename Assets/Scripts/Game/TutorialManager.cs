using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    List<TutorialStep> steps;

    private int currentStep = 0;
    GenericDialog dialog;

    public static TutorialManager main;

    private bool started = false;
    private bool finished = true;

    void Awake()
    {
        main = this;
    }

    public void StartTutorial() {
        started = true;
        finished = false;
    }

    public bool GetFinished()
    {
        return finished;
    }

    // Start is called before the first frame update
    void Start()
    {
        dialog = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            if (currentStep < steps.Count && dialog == null)
            {
                TutorialStep step = steps[currentStep];

                dialog = Prefabs.Instantiate<GenericDialog>();
                dialog.Initialize(step.title, step.description, step.dialogTarget);//, new Vector2(300, 300));
            }

            if (dialog != null)
            {
                dialog.UpdatePosition();
            }

            if (Input.GetKeyDown(KeyCode.Space) && dialog != null)
            {
                dialog.Hide();
                currentStep++;
                dialog = null;
            }

            if (Input.GetKeyDown(KeyCode.Return) || currentStep >= steps.Count)
            {
                if (dialog != null)
                {
                    dialog.Hide();
                }
                dialog = null;
                currentStep = steps.Count;
                finished = true;
            }
        }
    }
}

[Serializable]
public class TutorialStep
{
    public Transform dialogTarget;
    public string title;
    public string description;
}
