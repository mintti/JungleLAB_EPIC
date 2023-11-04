using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using System;
using DG.Tweening;
using TH.Core;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(PlayerDefence))]
public class Player : MonoBehaviour {
	public const int LAST_POSITION = 15;

	#region PublicVariables
	public Func<int, IEnumerator> OnMove {
		get {
			return _onMove;
		}
		set {
			_onMove = value;
		}
	}

	public Action<int> OnTileAction {
		get {
			return _onTileAction;
		}
		set {
			_onTileAction = value;
		}
	}

	public int Position => _position;
	#endregion

	#region PrivateVariables
	[ShowInInspector, ReadOnly] private int _position;

	// 플레이어 이벤트
	private Func<int, IEnumerator> _onMove;
	private Action<int> _onTileAction;

	// 플레이어 속성들
	private PlayerHealth _health;
	private PlayerDefence _defence;

	// 플레이어 능력
	private Dictionary<Type, PlayerAbility> _abilities;
	#endregion

	#region PublicMethod
	[Button]
	public void Init() {
		// 속성 값 초기화
		_health = GetComponent<PlayerHealth>();
		_defence = GetComponent<PlayerDefence>();
		_health.Init();
		_defence.Init();

		// 플레이어 능력 초기화
		_abilities = GetComponents<PlayerAbility>().ToDictionary(x => x.GetType(), x => x);

		// 플레이어 위치 초기화
		_position = 7;
		MoveTo(_position);
	}

	public T Ability<T>() where T : PlayerAbility {
		return _abilities[typeof(T)] as T;
	}

	public void PreUpdatePlayer() {
		UIManager.I.UIMain.gameObject.SetActive(true);
		_defence.ResetValue();
	}

	public void PostUpdatePlayer() {
		UIManager.I.UIMain.gameObject.SetActive(false);
	}

	public IEnumerator UpdatePlayer() {
		// 플레이어 능력 업데이트
		foreach (var ability in _abilities.Values) {
			ability.PreUpdate();
		}

		// 플레이어 능력 업데이트
		foreach (var ability in _abilities.Values) {
			ability.UpdateAbility();
		}

		// 플레이어 능력 업데이트
		foreach (var ability in _abilities.Values) {
			ability.PostUpdate();
		}

		yield return null;
	}

	public void Defence(int value) {
		_defence.ChangeValue(value);
	}

	public void Hit(int damage) {
		if (_defence.Value >= damage) {
			_defence.ChangeValue(-damage);
			return;
		}

		_defence.ResetValue();
		_health.ChangeValue(-damage + _defence.Value);
	}

	public IEnumerator Move(int value) {
		if (_onMove != null) {
			StartCoroutine(_onMove?.Invoke(value));
		}

		for(int i = 0; i < value; i++)
        {
			int prePos = _position;
            _position = BoardManager.I.GetNextIndex(_position);

			if (prePos == LAST_POSITION && _position == 0) {
				yield return UIManager.I.UINewSkillSelector.ActiveNewSkillSelector();
			}

            yield return MoveTo(_position, 0.5f);

			BoardManager.I.OnPass(_position);
        }

		BoardManager.I.OnEvent(_position);
	}

	public IEnumerator MoveTo(int index, float time) {
		_position = index;
		Vector3 targetPos = BoardManager.I.GetTilePos(index);
		transform.DOMove(targetPos, time);
		yield return new WaitForSeconds(time);
	}

	public IEnumerator Teleport(int index, float time=0.5f) {
		_position = index;
		yield return MoveTo(_position, time);
		BoardManager.I.GetTile(_position).debuff?.OnDebuff();
		BoardManager.I.OnEvent(_position);
	}

	public void TileAction(Card.Type type, int value) {
		_onTileAction?.Invoke(value);

		if (type == Card.Type.Wizard) {
			Ability<PlayerMagic>().CastingGauge += value;
		}

		BoardManager.I.tiles[_position].OnAction(value);
	}
	#endregion
    
	#region PrivateMethod
	[Button]
	private void MoveTo(int index) {
		_position = index;
		Vector3 targetPos = BoardManager.I.GetTilePos(index);
		transform.position = targetPos;
	}
	#endregion
}