using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class HoldToPickup : MonoBehaviour
{
	[SerializeField]
	[Tooltip("Probably just the main camera.. but referenced here to avoid Camera.main calls")]
	private Camera camera;
	[SerializeField]
	[Tooltip("The layers the items to pickup will be on")]
	private LayerMask layerMask;
	[SerializeField]
	[Tooltip("How long it takes to pick up an item.")]
	private float pickupTime = 2f;
	[SerializeField]
	[Tooltip("The root of the images (progress image should be a child of this too)")]
	private RectTransform pickupImageRoot;
	[SerializeField]
	[Tooltip("The ring around the button that fills")]
	private Image pickupProgressImage;


	private Item itemBeingPickedUp;
	private float currentPickupTimerElapsed;

	private void Update()
	{
		SelectItemBeingPickedUpFromRay();

		if (HasItemTargetted())
		{
			pickupImageRoot.gameObject.SetActive(true);

			if (Input.GetKey(KeyCode.E))
			{
				IncrementPickupProgressAndTryComplete();
			}
			else
			{
				currentPickupTimerElapsed = 0f;
			}

			UpdatePickupProgressImage();
		}
		else
		{
			pickupImageRoot.gameObject.SetActive(false);
			currentPickupTimerElapsed = 0f;
		}
	}

	private bool HasItemTargetted()
	{
		return itemBeingPickedUp != null;
	}

	private void IncrementPickupProgressAndTryComplete()
	{
		currentPickupTimerElapsed += Time.deltaTime;
		if (currentPickupTimerElapsed >= pickupTime)
		{
			OpenMultiplayerLobbySystem();
		}
	}

	private void UpdatePickupProgressImage()
	{
		float pct = currentPickupTimerElapsed / pickupTime;
		pickupProgressImage.fillAmount = pct;
	}

	private void SelectItemBeingPickedUpFromRay()
	{
		Ray ray = camera.ViewportPointToRay(Vector3.one / 2f);
		Debug.DrawRay(ray.origin, ray.direction * 2f, Color.red);

		RaycastHit hitInfo;
		if (Physics.Raycast(ray, out hitInfo, 2f, layerMask))
		{
			
			var hitItem = hitInfo.collider.GetComponent<Item>();

			if (hitItem == null)
			{
				itemBeingPickedUp = null;
			}
			else if (hitItem != null && hitItem != itemBeingPickedUp)
			{
				
				itemBeingPickedUp = hitItem;

			}
		}
		else
		{
			itemBeingPickedUp = null;
		}
	}

	private void OpenMultiplayerLobbySystem()
	{
		SceneManager.LoadScene(1);
	}
}