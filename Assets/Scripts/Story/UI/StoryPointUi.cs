using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryPointUi : MonoBehaviour
{

    [SerializeField]
    private TMPro.TextMeshProUGUI titleUI;

    [SerializeField]
    private TMPro.TextMeshProUGUI storyUI;

    [SerializeField]
    private Image background;

    [SerializeField]
    private DecisionUI decisionPrefab;

    [SerializeField]
    private Transform decisionTransform;

    [SerializeField]
    private StoryPoint storyPoint;

    [SerializeField]
    private Image clock;

    public event Action<Decision> OnDecicionMade;

    private Coroutine timerRoutine = null;

    [SerializeField]
    private Transform explainationNextButton;

    public void SetStoryPoint(StoryPoint storyPoint)
    {
        this.storyPoint = storyPoint;
        titleUI.text = storyPoint.Title;
        storyUI.text = storyPoint.Story;
        background.sprite = storyPoint.Background;
        background.color = storyPoint.BackgroundColor;
        titleUI.color = storyPoint.TextColor;
        storyUI.color = storyPoint.TextColor;
        clock.gameObject.SetActive(false);

        if (storyPoint.DecisionTime > 0 && storyPoint.DefaultDecision == null)
        {
            throw new Exception($"Storypoint {storyPoint.Title} has a decision time but not a default decision");
        }
        else if (storyPoint.DecisionTime < 0 && storyPoint.DefaultDecision != null)
        {
            throw new Exception($"Storypoint {storyPoint.Title} has a decision default decision but not a time given");
        }
        else if (storyPoint.DecisionTime > 0 && storyPoint.DefaultDecision != null)
        {
            StartTimer();
        }

        decisionTransform.CommitChildAbortion();
        foreach (var decision in storyPoint.Decisions)
        {

            if (!CheckGuards(decision))
                continue;

            var card = Instantiate(decisionPrefab);
            card.OnChosen += MakeDecision;
            card.SetDecision(decision);
            card.transform.SetParent(decisionTransform, false);
        }

    }

    private bool CheckGuards(Decision decision)
    {
        foreach (var guard in decision.Guards)
        {
            if (!guard.CheckGuardCondition())
            {
                return false;
            }
        }
        return true;
    }

    public event Action OnExaplainerClosed;
    public void SetExplainer(Explainer storyPoint)
    {
        decisionTransform.gameObject.SetActive(false);
        explainationNextButton.gameObject.SetActive(true);
        titleUI.text = storyPoint.Title;
        storyUI.text = storyPoint.Explaination;
    }

    public void FinishExplaination()
    {
        decisionTransform.gameObject.SetActive(true);
        explainationNextButton.gameObject.SetActive(false);
        titleUI.text = String.Empty;
        storyUI.text = String.Empty;
        OnExaplainerClosed.Invoke();
        OnExaplainerClosed = null;
    }

    private void MakeDecision(Decision decision)
    {
        StopTimer();
        OnDecicionMade?.Invoke(decision);
    }

    private void StartTimer()
    {
        clock.gameObject.SetActive(true);
        timerRoutine = StartCoroutine(Timer());
    }

    private void StopTimer()
    {
        clock.gameObject.SetActive(false);
        if (timerRoutine != null)
        {
            StopCoroutine(timerRoutine);
        }
        timerRoutine = null;
    }

    private IEnumerator Timer()
    {
        clock.fillAmount = 1;
        var timeLeft = storyPoint.DecisionTime;
        while (timeLeft > 0)
        {
            yield return null;
            timeLeft -= Time.deltaTime;
            clock.fillAmount = 1 / storyPoint.DecisionTime * timeLeft;
        }
        MakeDecision(storyPoint.DefaultDecision);
    }

    [ContextMenu("Reset Storypoint")]
    private void ResetStorypoint()
    {
        var point = storyPoint;
        storyPoint = null;
        SetStoryPoint(point);
    }

}
