using Camera;
using UnityEngine;
using Utils.Singleton;

public class CameraPanningManager : DontDestroyMonoBehaviourSingleton<CameraPanningManager>
{
	[SerializeField] private float transitionSpeed = 5f;
	private Vector3 startMousePosition;
	private Vector3 startCameraPosition;
	private bool panning;
	private Rect bounds;

	private bool initialized;

	private Vector3 targetCameraPosition;

	public void StartPanning(Rect bounds)
	{
		this.bounds = bounds;
		if (CameraHolder.Instance?.MainCamera != null)
		{
			this.bounds = bounds;
			startMousePosition = Input.mousePosition;
			startCameraPosition = CameraHolder.Instance.MainCamera.transform.position;
			panning = true;
		} 
	}

	private Vector3 KeepInBounds(Vector3 position)
	{
		position.x = Mathf.Clamp(position.x,bounds.xMin, bounds.xMax);
		position.y = Mathf.Clamp(position.y,bounds.yMin, bounds.yMax);
		return position;
	}

	public void Update()
	{
		if (CameraHolder.Instance?.MainCamera != null)
		{
			if (!initialized)
			{
				initialized = true;
				targetCameraPosition = CameraHolder.Instance.MainCamera.transform.position;
			}
			if (panning)
			{
				targetCameraPosition = 
					KeepInBounds(
						startCameraPosition 
						- CameraHolder.Instance.MainCamera.ScreenToWorldPoint(Input.mousePosition) 
						+ CameraHolder.Instance.MainCamera.ScreenToWorldPoint(startMousePosition)
					);
				
			}
			CameraHolder.Instance.MainCamera.transform.position = Vector3.Lerp(CameraHolder.Instance.MainCamera.transform.position, targetCameraPosition, Time.deltaTime * transitionSpeed);
		}
	}

	public void EndPanning()
	{
		if (panning) {
			panning = false;	
		}
	}
}
