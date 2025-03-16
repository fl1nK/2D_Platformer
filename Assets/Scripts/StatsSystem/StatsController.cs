using System;
using System.Collections.Generic;
using System.Linq;
using Core.Services.Updater;
using StatsSystem.Enum;
using UnityEngine;

namespace StatsSystem
{
    public class StatsController : IDisposable, IStatValueGiver

    {
    private readonly List<Stat> _currentStats;
    private readonly List<StatModificator> _activeModificator;

    public StatsController(List<Stat> currentStats)
    {
        _currentStats = currentStats;
        _activeModificator = new List<StatModificator>();
        ProjectUpdater.Instance.UpdateCalled += OnUpdate;
    }

    public float GetStatValue(StatType statType) => _currentStats.Find(stat => stat.Type == statType).Value;

    public void ProcessModificator(StatModificator modificator)
    {
        var statToChange = _currentStats.Find(stat => stat.Type == modificator.Stat.Type);
        if (statToChange == null)
            return;

        var addedValue = modificator.Type == StatModificatorType.Additive
            ? statToChange + modificator.Stat
            : statToChange * modificator.Stat;

        statToChange.SetStatValue(statToChange + addedValue);
        if (modificator.Duration < 0)
            return;

        if (_activeModificator.Contains(modificator))
        {
            _activeModificator.Remove(modificator);
        }
        else
        {
            var addedStat = new Stat(modificator.Stat.Type, -addedValue);
            var tempModificator =
                new StatModificator(addedStat, StatModificatorType.Additive, modificator.Duration, Time.time);
            _activeModificator.Add(tempModificator);
        }
    }

    public void Dispose() => ProjectUpdater.Instance.UpdateCalled -= OnUpdate;
    private void OnUpdate()
    {
        if (_activeModificator.Count == 0)
            return;

        var expiredModificator =
            _activeModificator.Where(modificator => modificator.StartTime + modificator.Duration >= Time.time);

        foreach (var modificator in expiredModificator)
        {
            ProcessModificator(modificator);
        }
    }
    }
}