using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  BOHF2DSpriteTools_DA
{

	public class ScriptTool_DA : MonoBehaviour 
	{

		//換算圖片修正座標
		public static void fn_ChangeRect(Transform transfrom_ChangeTransform,Sprite sprite_ChangeBefor,Sprite sprit_ChangeAfter,bool bool_IsLeft)
		{

			if (transfrom_ChangeTransform == null) 
			{
				transfrom_ChangeTransform.GetComponent<SpriteRenderer> ().sprite = sprit_ChangeAfter;
				return;
			}

			float float_ChangeBeforeX;
			float float_ChangeBeforeY;

			//計算切換圖片前圖片的一半寬
			float_ChangeBeforeX = (sprite_ChangeBefor.rect.width/2) * 0.01f;
			float_ChangeBeforeY = (sprite_ChangeBefor.rect.height/2) * 0.01f;

			if (bool_IsLeft) {
				transfrom_ChangeTransform.position = new Vector3 ((transfrom_ChangeTransform.position.x - ((sprit_ChangeAfter.rect.width / 2) * 0.01f)),
																  (transfrom_ChangeTransform.position.y - ((sprit_ChangeAfter.rect.height / 2) * 0.01f)),
																   transfrom_ChangeTransform.position.z);
			} 
			else 
			{
				transfrom_ChangeTransform.position = new Vector3 ((transfrom_ChangeTransform.position.x + ((sprit_ChangeAfter.rect.width / 2) * 0.01f)),
																  (transfrom_ChangeTransform.position.y - ((sprit_ChangeAfter.rect.height / 2) * 0.01f)),
																   transfrom_ChangeTransform.position.z);
			}

			transfrom_ChangeTransform.GetComponent<SpriteRenderer> ().sprite = sprit_ChangeAfter;
		} 
	}
}