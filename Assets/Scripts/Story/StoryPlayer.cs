using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoryPlayer : MonoBehaviour
{

    [SerializeField]
    private StoryPointUi storyPointUi;

    [SerializeField]
    private RunStorage runStorage;

    [SerializeField]
    private GamePoint gameoverPoint;

    [SerializeField]
    private GamePoint initialStorypoint;

    [SerializeField]
    private Combat combat;

    [SerializeField]
    private Player playerStats;

    [SerializeField]
    private List<GamePoint> generalRandomPoints;

    private List<GamePoint> generalUnlockedPoints = new();

    [SerializeField]
    private List<AreaStoryPoints> pointsPerArea;

    [field: SerializeField]
    public StoryArea Area { get; set; }

    private void Start()
    {
        StartStory();
    }

    public void StartStory()
    {
        playerStats.ResetStats();
        runStorage.ReunlockAll();
        storyPointUi.OnDecicionMade += OnDecisionMade;
        SetNextGamepoint(initialStorypoint);
    }

    public void StopStory()
    {
        storyPointUi.OnDecicionMade -= OnDecisionMade;
        pointsPerArea.ForEach((asp) => asp.UnlockedPoints.Clear());
        generalUnlockedPoints.Clear();
    }

    public void UnlockPoint(StoryArea area, GamePoint gamepoint)
    {
        if (area == StoryArea.None)
        {
            generalUnlockedPoints.Add(gamepoint);
        }
        else
        {
            pointsPerArea.Find((asp) => asp.Area == area)?.UnlockedPoints.Add(gamepoint);
        }
    }

    public bool HasUnlocked(StoryArea area, GamePoint gamepoint)
    {
        if (area == StoryArea.None)
        {
            return generalUnlockedPoints.Contains(gamepoint);
        }
        else
        {
            return pointsPerArea.Find((asp) => asp.Area == area)?.UnlockedPoints.Contains(gamepoint) ?? true;
        }
    }

    private void OnDecisionMade(Decision decision)
    {
        foreach (var effect in decision.Effects)
        {
            effect.effect.DoEffect(effect.OverrideParams ? effect.Params : null);
        }

        var isDead = playerStats.Health.CurrentValue <= playerStats.Health.MinValue;
        var nextPoint = isDead ? gameoverPoint : decision.Next ?? GetRandomPoint();
        if (decision.ExplainingPoint.Title != string.Empty)
        {
            storyPointUi.OnExaplainerClosed += () =>
            {
                SetNextGamepoint(nextPoint);
            };
            storyPointUi.SetExplainer(decision.ExplainingPoint);
        }
        else
        {
            SetNextGamepoint(nextPoint);
        }

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
            Combat.Outcome.Lose => combatpoint.NextLose ?? gameoverPoint,
            _ => null
        } ?? GetRandomPoint();
        SetNextGamepoint(nextPoint);
    }

    private GamePoint GetRandomPoint()
    {
        var areapoints = pointsPerArea.Find((areapoints) => areapoints.Area == Area).GetAllPoints();
        return generalRandomPoints.Union(generalUnlockedPoints).Union(areapoints).ToList().GetRandom();
    }

    [System.Serializable]
    private class AreaStoryPoints
    {

        [field: SerializeField]
        public StoryArea Area { get; private set; }

        [field: SerializeField]
        public List<GamePoint> Gamepoints { get; private set; }

        public List<GamePoint> UnlockedPoints { get; private set; } = new();

        public List<GamePoint> GetAllPoints()
        {
            return Gamepoints.Union(UnlockedPoints).ToList();
        }
    }

    [ContextMenu("Validate")]
    private void ResetStorypoint()
    {
        HashSet<GamePoint> visited = new();
        Queue<GamePoint> toVisit = new();
        toVisit.Enqueue(initialStorypoint);
        generalRandomPoints.ForEach((a) => { if (!toVisit.Contains(a)) { toVisit.Enqueue(a); } });
        pointsPerArea.ForEach((ppa) => ppa.Gamepoints.ForEach((a) => { if (!toVisit.Contains(a)) { toVisit.Enqueue(a); } }));
        bool allgood = true;
        while (toVisit.Count > 0)
        {
            var next = toVisit.Dequeue();
            switch (next)
            {
                case StoryPoint storypoint:
                    bool ispossible = false;

                    if (storypoint.Decisions.Count == 0)
                    {
                        allgood = false;
                        Debug.LogWarning($"storypoint {storypoint.name} doesn't have any decisions.");
                        continue;
                    }

                    foreach (var decision in storypoint.Decisions)
                    {

                        if (decision.Guards == null || decision.Guards.Count == 0)
                        {
                            ispossible = true;
                        }

                        if (decision.Next == null || visited.Contains(decision.Next) || toVisit.Contains(decision.Next))
                            continue;

                        toVisit.Enqueue(decision.Next);
                    }

                    if (!ispossible)
                    {
                        allgood = false;
                        Debug.LogWarning($"storypoint {storypoint.name} isn't always possible to get out of");
                    }

                    break;
                case CombatPoint combatpoint:

                    if (combatpoint.NextFlee != null && !visited.Contains(combatpoint.NextFlee) && !toVisit.Contains(combatpoint.NextFlee))
                        toVisit.Enqueue(combatpoint.NextFlee);

                    if (combatpoint.NextWin != null && !visited.Contains(combatpoint.NextWin) && !toVisit.Contains(combatpoint.NextWin))
                        toVisit.Enqueue(combatpoint.NextFlee);

                    break;
                default:
                    throw new System.ArgumentException($"The given argument was of type {next.GetType()} which was unknown");
            }
        }

        if (allgood)
        {
            Debug.Log("Everything seems to be alright!");
        }

    }

}
