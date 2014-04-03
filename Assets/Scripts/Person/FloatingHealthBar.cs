using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GUITexture))]
public class FloatingHealthBar : MonoBehaviour {

	public Transform target;                    // Object that this label should follow
	public Vector3 offset = Vector3.up;     // Units in world space to offset; 1 unit above object by default
	public bool clampToScreen = false;      // If true, label will be visible even if object is off screen
	public float clampBorderSize = .05f;        // viewport space left around borders when a label is being clamped
	public bool useMainCamera = true;       // Used on camera tagged 'MainCamera'
	public Camera cameraToUse;              // Only used if 'useMainCamera' is false
	
	private Camera displayCamera;
	private Transform displayElementTransform;  //transform for this element
	private Transform displayCameraTransform;   //transform for camera in use
	
	void Start()
	{
		displayElementTransform = transform;
		
		if (useMainCamera)
			displayCamera = Camera.main;
		else
			displayCamera = cameraToUse;
		
		displayCameraTransform = displayCamera.transform;
	}
	
	void Update()
	{
		SetElementPositionRespectCamera();
	}
	
	/// <summary>
	/// <remarks>element position will display if camera is turned off,
	///     but will not respond to target's position properly</remarks>
	/// </summary>
	void SetElementPositionRespectCamera()
	{
		if (clampToScreen)
		{
			Vector3 relativePosition = displayCameraTransform.InverseTransformPoint(target.position);
			
			float cameraMiddleGroundZ = 1.0f;     //used as middle ground for camera's relative position
			
			relativePosition.z = Mathf.Max(relativePosition.z, cameraMiddleGroundZ);
			
			//set position for element's transform
			Vector3 displayTransformPoint = displayCameraTransform.TransformPoint(relativePosition + offset);
			displayElementTransform.position = displayCamera.WorldToViewportPoint(displayTransformPoint);
		}
		else
		{
			displayElementTransform.position = displayCamera.WorldToViewportPoint(target.position + offset);
		}
	}
}
