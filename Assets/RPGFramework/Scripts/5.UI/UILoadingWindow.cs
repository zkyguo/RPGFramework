using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[UIElement(true, "UI/UILoadingWindow",4)]
public class UILoadingWindow : UIWindowsBase
{
    [SerializeField]
    private Text progressText;
    [SerializeField]
    private Image fillImage;

    public override void Show()
    {
        base.Show();
        Updateprogress(0);
    }

    /// <summary>
    /// Update progress bar value
    /// </summary>
    /// <param name="progressValue"></param>
    private void Updateprogress(float progressValue)
    {
        progressText.text = (int)(progressValue * 100) + "%";
        fillImage.fillAmount = progressValue;
    }

    protected override void AddEventListener()
    {
        base.AddEventListener();
        EventManager.AddEventListener<float>("LoadingSceneProgress", Updateprogress);
        EventManager.AddEventListener("LoadingFinish", LoadingFinish);
    }

    /// <summary>
    /// When loading finished
    /// </summary>
    private void LoadingFinish()
    {
        Close();
    }
    protected override void RemoveEventListener()
    {
        base.RemoveEventListener();
        EventManager.CancelEventListener<float>("LoadingSceneProgress", Updateprogress);
        EventManager.CancelEventListener("LoadingFinish", LoadingFinish);
    }
}
