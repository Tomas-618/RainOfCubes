using System.Collections.Generic;

public interface IReadOnlyLifeTimerSpawner
{
    IReadOnlyCollection<IReadOnlyLifeTimerEvents> AllEntitiesEvents { get; }
}
