using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH.Core {

public class LogManager : IManager
{
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
			Debug.Log(message);
		} else if (logType == LogType.Warning) {
			Debug.LogWarning(message);
		} else if (logType == LogType.Error) {
			Debug.LogError(message);
		}
	}

	private void LogInBuild(string message, LogType logType=LogType.Info) {
	}
	#endregion
}

}