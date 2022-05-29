//////////////////////////////////////////////////////
// MK Toon Particles Unlit Editor        			//
//					                                //
// Created by Michael Kremmel                       //
// www.michaelkremmel.de                            //
// Copyright © 2021 All rights reserved.            //
//////////////////////////////////////////////////////

#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System;
using UnityEditor.Utils;
using UnityEditorInternal;
using EditorHelper = MK.Toon.Editor.EditorHelper;
using MK.Toon;

namespace MK.Toon.Editor.Legacy
{
    internal sealed class ParticlesUnlitEditor : MK.Toon.Editor.UnlitEditorBase {}
}
#endif