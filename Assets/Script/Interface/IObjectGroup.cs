using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IObjectGroup<T>
{
    void Add(T summon);
    void Remove(T summon);
    void Update();
    void Replace();
}
