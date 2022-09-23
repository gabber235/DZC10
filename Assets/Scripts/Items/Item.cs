using System;
using System.Collections.Generic;

[Serializable]
public class Item
{
    public Item(string name)
    {
        Name = name;
    }

    public string Name { get; }

    private sealed class NameEqualityComparer : IEqualityComparer<Item>
    {
        public bool Equals(Item x, Item y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Name == y.Name;
        }

        public int GetHashCode(Item obj)
        {
            return (obj.Name != null ? obj.Name.GetHashCode() : 0);
        }
    }

    public static IEqualityComparer<Item> NameComparer { get; } = new NameEqualityComparer();
}

public class Ingredient : Item
{
    public Ingredient(string name) : base(name)
    {}
}

public class Cocktail : Item
{
    public Cocktail(string name) : base(name)
    {}
}