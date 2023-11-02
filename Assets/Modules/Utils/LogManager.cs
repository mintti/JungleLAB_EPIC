using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH.Core {

public class LogManager : IManager
{
	public const string ERROR_CARD_DECK_NOT_INIT = "CardDeck has not initialized.";
	public const string ERROR_CARD_NOT_IN_HAND = "Card is not in hand.";

	public enum LogType {
		Info,
		Warning,
		Error,
	}

	public LogManager() {
		
	}

    #region PublicVariables
	#endregion

	#region PrivateVariables
	#endregion

	#region PublicMethod
	public void Init() {

	}

	public void Log(string message, LogType logType=LogType.Info) {
		#if UNITY_EDITOR
		LogInEditor(message, logType);
		#else
		LogInBuild(message, logType);
		#endif
	}
	#endregion
    
	#region PrivateMethod
	private void LogInEditor(string message, LogType logType=LogType.Info) {
		if (logType == LogType.Info) {
			Debug.Log($"Debug: {message}");
		} else if (logType == LogType.Warning) {
			Debug.LogWarning($"Warning: {message}");
		} else if (logType == LogType.Error) {
			Debug.LogError($"Error: {message}");
		}
	}

	private void LogInBuild(string message, LogType logType=LogType.Info) {
	}
	#endregion
}

}