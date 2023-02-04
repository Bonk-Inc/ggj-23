using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryPlayer : MonoBehaviour
{

    [SerializeField]
    private StoryPointUi storyPointUi;


    [SerializeField]
    private GamePoint initialStorypoint;

    [SerializeField]
    private Combat combat;

    [SerializeField]
    private List<GamePoint> possibleRandomPoints;

    private void Start()
    {
        StartStory();
    }

    public void StartStory()
    {
        storyPointUi.OnDecicionMade += OnDecisionMade;
        SetNextGamepoint(initialStorypoint);
    }

    private void OnDecisionMade(Decision decision)
    {
        foreach (var effect in decision.Effects)
        {
            effect.DoEffect(decision.overrideParameters ? decision.Params : null);
        }

        var nextPoint = decision.Next ?? possibleRandomPoints.GetRandom();
        SetNextGamepoint(nextPoint);
    }

    private void SetNextGamepoint(GamePoint gamepoint)
    {
        switch (gamepoint)
        {
            case StoryPoint storypoint:
                storyPointUi.SetStoryPoint(storypoint);
                break;
            case CombatPoint combatpoint:
                combat.StartCombat(combatpoint.Enemies, (outcome) => OnCombatFinished(outcome, combatpoint));
                break;
            default:
                throw new System.ArgumentException($"The given argument was of type {gamepoint.GetType()} which was unknown");
        }

    }

    private void OnCombatFinished(Combat.Outcome outcome, CombatPoint combatpoint)
    {
        GamePoint nextPoint = outcome switch
        {
            Combat.Outcome.Win => combatpoint.NextWin,
            Combat.Outcome.Flee => combatpoint.NextFlee,
            Combat.Outcome.Lose => combatpoint.NextLose,
            _ => null
        } ?? possibleRandomPoints.GetRandom();
        SetNextGamepoint(nextPoint);
    }

}
