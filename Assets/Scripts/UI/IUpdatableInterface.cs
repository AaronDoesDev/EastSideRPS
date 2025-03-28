using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ESG.RockPaperScissors
{
    // A bitwise enum that allows UpdateInterface implementations to update only the
    // indicated UI elements. Custom combinations can be put together using the bar operator (|).
    public enum InterfaceUpdateFlags {
        None = 0,
        DisplayName = 1,
        Money = 2,
        HandSignal = 4,
        All = 8
    }

    // Requires method(s) that allow a class to update the specified UI elements
    // when another service makes changes to their associated data.
    public interface IUpdatableInterface
    {
        public void UpdateInterface(InterfaceUpdateFlags flags);
    }
}