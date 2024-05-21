using System;

public interface IReadOnlyLifeTimerEvents
{
    event Action<LifeTimer> Died;
}
