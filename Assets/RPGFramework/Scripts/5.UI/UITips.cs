using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI tips window
/// </summary>
public class UITips : MonoBehaviour
{
    [SerializeField]
    private Text infoText;
    [SerializeField]
    private Animator animator;
    private Queue<string> tipsQueue = new Queue<string>();
    private bool isShowing = false;

    /// <summary>
    /// Add tips info message
    /// </summary>
    /// <param name="info"></param>
    public void AddTips(string info)
    {
        tipsQueue.Enqueue(info);
        ShowTips();
    }

    /// <summary>
    /// Show tips window
    /// </summary>
    private void ShowTips()
    {
        if(tipsQueue.Count > 0 && !isShowing)
        {
            infoText.text = tipsQueue.Dequeue();
            animator.Play("Show",0,0);
        }
    }

    #region Animation Event
    private void StartTips()
    {
        isShowing = true;
        
    }

    private void EndTips()
    {
        isShowing = false;
        ShowTips();
    }

    #endregion

}
