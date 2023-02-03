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

    public event Action<Decision> OnDecicionMade;

    public void SetStoryPoint(StoryPoint storyPoint)
    {
        this.storyPoint = storyPoint;
        titleUI.text = storyPoint.Title;
        storyUI.text = storyPoint.Story;
        background.sprite = storyPoint.Background;

        decisionTransform.CommitChildAbortion();
        foreach (var decision in storyPoint.Decisions)
        {
            var card = Instantiate(decisionPrefab);
            card.OnChosen += (decision) =>
            {
                OnDecicionMade?.Invoke(decision);
            };
            card.SetDecision(decision);
            card.transform.SetParent(decisionTransform, false);
        }

    }

    [ContextMenu("Reset Storypoint")]
    private void ResetStorypoint()
    {
        var point = storyPoint;
        storyPoint = null;
        SetStoryPoint(point);
    }

}
