#region Using
using UnityEngine;
using System;
using System.Collections;
#endregion // Using

#region File description
/*
 * File		: SkyBoxPicker
 * Project	: SkyBoxCollection One
 * Person	: Karl Gillott
 * Company	: ElectricWolf.co.uk
 * Started	: 27/06/2013
 *
 * List all skyboxes in the material array as numbered buttons.
 * With the current skybox shown in [ ]
 *
*/
#endregion // File description


public class SkyBoxPicker : MonoBehaviour {

	#region Varibales
	public Material[] SkyboxList;
	int m_nCurrent = 0;
	#endregion // Varibales
	
	#region Set-up
	void Start ()
	{
		UpdateSkyBox();
	}
	#endregion // Set-up
	
	#region functions
	void UpdateSkyBox()
	{
		try{
			RenderSettings.skybox= SkyboxList[m_nCurrent];
		}
		catch
		{
			// can't find this skybox !
		}
	}
	#endregion functions
		
	#region GUI
	void OnGUI()
	{
		for ( int i = 1 ;i < 11 ; i++ )
		{
			if ( m_nCurrent+1 != i )
			{
				if ( GUI.Button(new Rect(48 + ( i * 45 ), Screen.height-64 , 44	, 32), i.ToString() ) )
				{
					m_nCurrent = i-1;
					UpdateSkyBox();
				}	
			}
			else
			{
				GUI.Button(new Rect(48 + ( i * 45 ), Screen.height-64 , 44, 32), "["+i.ToString()+"]" );
			}
		}
	
	}
	#endregion // GUI
}
