using UnityEngine;

[System.Serializable]
public class FloatNumberRange
{
    [SerializeField]
    private float min, max;

    public float Min
    {
        get => min;
        set
        {
            min = value;
            OrderRange();
        }
    }

    public float Max
    {
        get => min;
        set
        {
            max = value;
            OrderRange();
        }
    }

    public bool IsInRange(float number)
    {
        return number >= min && number <= max;
    }

    public float RandomInRange()
    {
        return Random.Range(min, max);
    }

    private void OrderRange()
    {
        if (min > max)
        {
            var temp = min;
            min = max;
            max = min;
        }
    }

}

[System.Serializable]
public class IntNumberRange
{
    [SerializeField]
    private int min, max;

    public int Min
    {
        get => min;
        set
        {
            min = value;
            OrderRange();
        }
    }

    public int Max
    {
        get => min;
        set
        {
            max = value;
            OrderRange();
        }
    }

    public bool IsInRange(int number)
    {
        return number >= min && number <= max;
    }

    public int RandomInRange()
    {
        return Random.Range(min, max);
    }

    private void OrderRange()
    {
        if (min > max)
        {
            var temp = min;
            min = max;
            max = min;
        }
    }
}