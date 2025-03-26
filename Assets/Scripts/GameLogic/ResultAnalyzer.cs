using UnityEngine;
using System.Collections;

namespace ESG.RockPaperScissors
{
	public enum Result
	{
		Won,
		Lost,
		Draw
	}

	public class ResultAnalyzer
	{
		public static Result GetResultState(HandSignal playerHand, HandSignal enemyHand)
		{
			if (isStronger(playerHand, enemyHand))
			{
				return Result.Won;
			}
			else if (isStronger(enemyHand, playerHand))
			{
				return Result.Lost;
			}
			else
			{
				return Result.Draw;
			}
		}

		private static bool isStronger (HandSignal firstHand, HandSignal secondHand)
		{
			switch (firstHand)
			{
				case HandSignal.Rock:
				{
					switch (secondHand)
					{
						case HandSignal.Scissors:
							return true;
						case HandSignal.Paper:
							return false;
					}
					break;
				}
				case HandSignal.Paper:
				{
					switch (secondHand)
					{
						case HandSignal.Rock:
							return true;
						case HandSignal.Scissors:
							return false;
					}
					break;
				}
				case HandSignal.Scissors:
				{
					switch (secondHand)
					{
						case HandSignal.Paper:
							return true;
						case HandSignal.Rock:
							return false;
					}
					break;
				}
			}

			return false;
		}
	}
}