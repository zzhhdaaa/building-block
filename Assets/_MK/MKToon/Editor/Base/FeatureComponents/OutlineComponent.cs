//////////////////////////////////////////////////////
// MK Toon Editor Outline Component				    //
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

namespace MK.Toon.Editor
{
    internal sealed class OutlineComponent : ShaderGUI
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
		// Properties                                                                              //
		/////////////////////////////////////////////////////////////////////////////////////////////
        private MaterialProperty _outline;
        private MaterialProperty _outlineData;
        private MaterialProperty _outlineMap;
        private MaterialProperty _outlineSize;
        private MaterialProperty _outlineColor;
        private MaterialProperty _outlineNoise;

        #if MK_TOON_OUTLINE_FADING_LINEAR  || MK_TOON_OUTLINE_FADING_EXPONENTIAL || MK_TOON_OUTLINE_FADING_INVERSE_EXPONENTIAL
            private MaterialProperty _outlineFadeMin;
            private MaterialProperty _outlineFadeMax;
        #endif

        private MaterialProperty _outlineBehavior;
        internal bool active { get { return _outlineBehavior != null; } }

        internal void FindProperties(MaterialProperty[] props)
        {
            _outline = FindProperty(Properties.outline.uniform.name, props, false);
            _outlineData = FindProperty(Properties.outlineData.uniform.name, props, false);
            _outlineMap = FindProperty(Properties.outlineMap.uniform.name, props, false);
            _outlineSize = FindProperty(Properties.outlineSize.uniform.name, props, false);
            _outlineColor = FindProperty(Properties.outlineColor.uniform.name, props, false);
            _outlineNoise = FindProperty(Properties.outlineNoise.uniform.name, props, false);

            #if MK_TOON_OUTLINE_FADING_LINEAR  || MK_TOON_OUTLINE_FADING_EXPONENTIAL || MK_TOON_OUTLINE_FADING_INVERSE_EXPONENTIAL
                _outlineFadeMin = FindProperty(Properties.outlineFadeMin.uniform.name, props, false);
                _outlineFadeMax = FindProperty(Properties.outlineFadeMax.uniform.name, props, false);
            #endif

            _outlineBehavior = FindProperty(EditorProperties.outlineTab.uniform.name, props, false);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
		// Draw                                                                                    //
		/////////////////////////////////////////////////////////////////////////////////////////////
        internal void DrawOutline(MaterialEditor materialEditor, MaterialProperty[] properties)
        {
            //All outline properties needs to be available on the material
            //the outline tab is used for check
            if(_outlineBehavior != null)
            {
                if(EditorHelper.HandleBehavior(UI.outlineTab.text, "", _outlineBehavior, null, materialEditor, false))
                {
                    FindProperties(properties);
                    EditorGUI.BeginChangeCheck();
                    materialEditor.ShaderProperty(_outline, UI.outline);
                    if (EditorGUI.EndChangeCheck())
                    {
                        ManageKeywordsOutline();
                    }
                    if((Outline) _outline.floatValue != Outline.HullOrigin)
                    {
                        EditorGUI.BeginChangeCheck();
                        materialEditor.ShaderProperty(_outlineData, UI.outlineData);
                        if (EditorGUI.EndChangeCheck())
                        {
                            ManageKeywordsOutlineData();
                        }
                    }

                    materialEditor.ShaderProperty(_outlineColor, UI.outlineColor);
                    EditorGUI.BeginChangeCheck();
                    materialEditor.TexturePropertySingleLine(UI.outlineMap, _outlineMap, _outlineSize);
                    if(EditorGUI.EndChangeCheck())
                        ManageKeywordsOutlineMap();
                    #if MK_TOON_OUTLINE_FADING_LINEAR  || MK_TOON_OUTLINE_FADING_EXPONENTIAL || MK_TOON_OUTLINE_FADING_INVERSE_EXPONENTIAL
                        materialEditor.ShaderProperty(_outlineFadeMin, UI.outlineFadeMin);
                        materialEditor.ShaderProperty(_outlineFadeMax, UI.outlineFadeMax);
                    #endif
                    EditorGUI.BeginChangeCheck();
                    materialEditor.ShaderProperty(_outlineNoise, UI.outlineNoise);
                    if(EditorGUI.EndChangeCheck())
                        ManageKeywordsOutlineNoise();
                }

                EditorHelper.DrawSplitter();
            }
        }

        internal void ManageKeywordsOutline()
        {
            if(_outlineBehavior != null)
            {
                foreach (Material mat in _outline.targets)
                {
                    mat.SetShaderPassEnabled("Always", true);
                    EditorHelper.SetKeyword(Properties.outline.GetValue(mat) == Outline.HullClip, Keywords.outline[2], mat);
                    EditorHelper.SetKeyword(Properties.outline.GetValue(mat) == Outline.HullOrigin, Keywords.outline[1], mat);
                }
            }
        }
        internal void ManageKeywordsOutlineData()
        {
            if(_outlineBehavior != null)
            {
                foreach (Material mat in _outlineData.targets)
                {
                    EditorHelper.SetKeyword(Properties.outlineData.GetValue(mat) == OutlineData.Baked, Keywords.outlineData, mat);
                }
            }
        }
        internal void ManageKeywordsOutlineNoise()
        {
            if(_outlineBehavior != null)
            {
                foreach (Material mat in _outlineNoise.targets)
                {
                    EditorHelper.SetKeyword(Properties.outlineNoise.GetValue(mat) != 0, Keywords.outlineNoise, mat);
                }
            }
        }
        internal void ManageKeywordsOutlineMap()
        {
            if(_outlineBehavior != null)
            {
                foreach (Material mat in _outlineMap.targets)
                {
                    EditorHelper.SetKeyword(Properties.outlineMap.GetValue(mat) != null, Keywords.outlineMap, mat);
                }
            }
        }
    }
}
#endif