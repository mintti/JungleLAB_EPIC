using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TH.Core {

    public class UICardUseSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        #region PublicVariables
        #endregion

        #region PrivateVariables
		private Action _onPointerEnter;
		private Action _onPointerExit;

		private ComponentGetter<Image> _image
			= new ComponentGetter<Image>(TypeOfGetter.This);
        #endregion

        #region PublicMethod
		public void Init(Action onPointerEnter, Action onPointerExit) {
			_onPointerEnter = onPointerEnter;
			_onPointerExit = onPointerExit;
		}

		public void OnPointerEnter(PointerEventData eventData)
        {
            Highlight(true);
			_onPointerEnter?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Highlight(false);
			_onPointerExit?.Invoke();
        }
        #endregion

        #region PrivateMethod
		private void Highlight(bool isOn) {
			Color highlighted = Color.white;
			highlighted.a = 0.5f;

			_image.Get(gameObject).color = isOn ? highlighted : Color.white;
		}
        #endregion
    }

}