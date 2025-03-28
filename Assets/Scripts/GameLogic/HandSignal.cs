using UnityEngine;
using System.Collections;

namespace ESG.RockPaperScissors
{
	// Widely used enum listing all valid hand signals, as well as a 'none' special case.
	// The integer values allow for some simple handling that might normally involve
	// switch statements or parsing.
	public enum HandSignal
	{
		None = -1,
		Rock = 0,
		Paper = 1,
		Scissors = 2
	}
}