using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool dash;
		public bool attack;
		public bool skill1;
		public bool skill2;
		public bool skill3;
		public bool interact;
		public bool aim;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if (cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			DashInput(value.isPressed);
		}
		public void OnAttack(InputValue value)
		{
			AttackInput(value.isPressed);
		}
		public void OnSkill1(InputValue value)
		{
			Skill1Input(value.isPressed);
		}
		public void OnSkill2(InputValue value)
		{
			Skill2Input(value.isPressed);
		}
		public void OnSkill3(InputValue value)
		{
			Skill3Input(value.isPressed);
		}
		public void OnInteract(InputValue value)
		{
			InteractInput(value.isPressed);
		}
		public void OnDash(InputValue value)
		{
			DashInput(value.isPressed);
		}
		public void OnAim(InputValue value)
		{
			AimInput(value.isPressed);
		}
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		}

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void DashInput(bool newSprintState)
		{
			dash = newSprintState;
		}
		public void AttackInput(bool newAttackState)
		{
			attack = newAttackState;
		}
		public void Skill1Input(bool newSkill1State)
		{
			skill1 = newSkill1State;
		}
		public void Skill2Input(bool newSkill2State)
		{
			skill2 = newSkill2State;

		}
		public void Skill3Input(bool newSkill3State)
		{
			skill3 = newSkill3State;

		}
		public void InteractInput(bool newInteractState)
		{
			interact = newInteractState;
		}
		public void AimInput(bool newAimState)
		{
			aim = newAimState;
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}

}