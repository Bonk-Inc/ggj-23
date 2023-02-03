using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryPlayer : MonoBehaviour
{

    [SerializeField]
    private StoryPointUi storyPointUi;


    [SerializeField]
    private StoryPoint initialStorypoint;

    [SerializeField]
    private List<StoryPoint> possibleRandomPoints;

    private void Start()
    {
        StartStory();
    }

    public void StartStory()
    {
        storyPointUi.OnDecicionMade += OnDecisionMade;
        storyPointUi.SetStoryPoint(initialStorypoint);
    }

    private void OnDecisionMade(Decision decision)
    {
        var nextPoint = decision.Next ?? possibleRandomPoints.GetRandom();
        storyPointUi.SetStoryPoint(nextPoint);
    }
}
