using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESG.RockPaperScissors
{
    public enum InterfaceUpdateFlags {
        None = 0,
        DisplayName = 1,
        Money = 2,
        HandSignal = 4,
        All = 8
    }

    public interface IUpdatableInterface
    {
        public void UpdateInterface(InterfaceUpdateFlags flags);
    }
}