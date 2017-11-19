using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Tuple<T,U, P, L>
{
    public T Item1 { get; private set; }
    public U Item2 { get; private set; }
    public P Item3 { get; private set; }
    public L Item4 { get; private set; }
    
    public Tuple(T item1, U item2, P item3, L item4)
    {
        Item1 = item1;
        Item2 = item2;
        Item3 = item3;
        Item4 = item4;
    }
}

public static class Tuple
{
    public static Tuple<T, U, P, L> Create<T, U, P, L>(T item1, U item2, P item3, L item4)
    {
        return new Tuple<T, U, P, L>(item1, item2, item3, item4);
    }
    
}


public partial class Tuple<T, U> {
    
    public T Item1 { get; private set; }
    public U Item2 { get; private set; }
    
    public Tuple(T item1, U item2)
    {
        Item1 = item1;
        Item2 = item2;
    }

    
    
    
    
    public static Tuple<T, U> Create <T, U>(T item1, U item2) {
        return new Tuple<T, U>(item1, item2);
    }

    
    
}

