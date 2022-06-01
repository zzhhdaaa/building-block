//////////////////////////////////////////////////////
// MK Toon Examples Spectate Default    	    	//
//					                                //
// Created by Michael Kremmel                       //
// www.michaelkremmel.de                            //
// Copyright © 2020 All rights reserved.            //
//////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MK.Toon.Examples
{
    public class SpectateDefault : Spectate
    {
        #if UNITY_POST_PROCESSING_STACK_V2
        [SerializeField]
        UnityEngine.Rendering.PostProcessing.PostProcessVolume _ppsv2Volume = null;

        protected override void PPSetup(string name)
        {
            if(_ppsv2Volume == null)
                return;
                
            UnityEngine.Rendering.PostProcessing.AmbientOcclusion aoSettings;
            _ppsv2Volume.sharedProfile.TryGetSettings(out aoSettings);

            if(name.Contains("Stencil"))
                aoSettings.enabled.value = false;
            else
                aoSettings.enabled.value = true;
        }
        #endif
    }
}
