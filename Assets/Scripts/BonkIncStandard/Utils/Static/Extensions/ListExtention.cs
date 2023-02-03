using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ListExtention
{
    public static T GetRandom<T>(this List<T> list)
    {
        if(list.Count == 0) 
            throw new System.Exception("Cannot get random element from empty list");
        
        var randomIndex = Random.Range(0, list.Count);
        return list[randomIndex];
    }
    
    public static bool UnorderedEqual<T>(this ICollection<T> a, ICollection<T> b)
    {
        if (a.Count != b.Count)
            return false;
        
        var d = new Dictionary<T, int>();
        foreach (var item in a)
        {
            int c;
            if (d.TryGetValue(item, out c))
                d[item] = c + 1;
            else 
                d.Add(item, 1);
        }

        foreach (var item in b)
        {
            int c;
            if (!d.TryGetValue(item, out c) || c == 0)
                return false;

            d[item] = c - 1;
        }

        return d.Values.All(v => v == 0);
    }
}