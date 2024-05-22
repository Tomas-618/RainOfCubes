using System;

public interface IReadOnlySpawnerEvents
{
    event Action Spawned;

    event Action Dispawned;
}
