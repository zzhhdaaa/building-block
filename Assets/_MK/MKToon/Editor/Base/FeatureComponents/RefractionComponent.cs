//////////////////////////////////////////////////////
// MK Toon Editor Refraction Component				//
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
    internal sealed class RefractionComponent : ShaderGUI
    {
        /////////////////////////////////////////////////////////////////////////////////////////////
		// Properties                                                                              //
		/////////////////////////////////////////////////////////////////////////////////////////////
        private MaterialProperty _refractionDistortionMapScale;
        private MaterialProperty _refractionDistortionMap;
        private MaterialProperty _refractionDistortion;
        private MaterialProperty _refractionDistortionFade;
        private MaterialProperty _indexOfRefraction;

        private MaterialProperty _refractionBehavior;
        internal bool active { get { return _refractionBehavior != null; } }

        internal void FindProperties(MaterialProperty[] props)
        {
            _refractionDistortionMapScale = FindProperty(Properties.refractionDistortionMapScale.uniform.name, props, false);
            _refractionDistortionMap = FindProperty(Properties.refractionDistortionMap.uniform.name, props, false);
            _refractionDistortion = FindProperty(Properties.refractionDistortion.uniform.name, props, false);
            _refractionDistortionFade = FindProperty(Properties.refractionDistortionFade.uniform.name, props, false);
            _indexOfRefraction = FindProperty(Properties.indexOfRefraction.uniform.name, props, false);

            _refractionBehavior = FindProperty(EditorProperties.refractionTab.uniform.name, props, false);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
		// Setup                                                                                   //
		/////////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>
        /// Find similar values from the changed shader
        /// </summary>
        /// <param name="propertiesSrc"></param>
        /// <param name="materialDst"></param>
        /// <param name="materialSrc"></param>
        internal void ConvertSimilarValues(MaterialProperty[] propertiesSrc, Material materialSrc, Material materialDst)
        {
            MaterialProperty bumpMap = FindProperty("_BumpMap", propertiesSrc, false);
            
            _refractionDistortionMap.textureValue = bumpMap.textureValue != null ? bumpMap.textureValue : _refractionDistortionMap.textureValue;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
		// Draw                                                                                    //
		/////////////////////////////////////////////////////////////////////////////////////////////
        internal void DrawRefraction(MaterialEditor materialEditor, MaterialProperty[] properties)
        {
            //All refraction properties needs to be available on the material
            //the refraction tab is used for check
            if(_refractionBehavior != null)
            {
                if(EditorHelper.HandleBehavior(UI.refractionTab.text, "", _refractionBehavior, null, materialEditor, false))
                {
                    FindProperties(properties);
                    EditorGUI.BeginChangeCheck();
                    materialEditor.ShaderProperty(_indexOfRefraction, UI.indexOfRefraction);
                    if(EditorGUI.EndChangeCheck())
                        ManageKeywordsIndexOfRefraction();
                    EditorGUI.BeginChangeCheck();
                    if(_refractionDistortionMap.textureValue != null)
                    {
                        materialEditor.TexturePropertySingleLine(UI.refractionDistortionMap, _refractionDistortionMap, _refractionDistortionMapScale);
                        materialEditor.ShaderProperty(_refractionDistortion, UI.refractionDistortion);
                    }
                    else
                        materialEditor.TexturePropertySingleLine(UI.refractionDistortionMap, _refractionDistortionMap);
                    if (EditorGUI.EndChangeCheck())
                    {
                        ManageKeywordsRefractionMap();
                    }
                    materialEditor.ShaderProperty(_refractionDistortionFade, UI.refractionDistortionFade);
                }

                EditorHelper.DrawSplitter();
            }
        }

        internal void ManageKeywordsRefractionMap()
        {
            if(_refractionBehavior != null)
            {
                foreach (Material mat in _refractionDistortionMap.targets)
                {
                    mat.SetShaderPassEnabled("Always", true);
                    EditorHelper.SetKeyword(Properties.refractionDistortionMap.GetValue(mat) != null, Keywords.refractionDistortionMap, mat);
                    //No Refraction Map = use mesh normals
                }
            }
        }

        internal void ManageKeywordsIndexOfRefraction()
        {
            if(_refractionBehavior != null)
            {
                foreach (Material mat in _indexOfRefraction.targets)
                {
                    EditorHelper.SetKeyword(Properties.indexOfRefraction.GetValue(mat) != 0, Keywords.indexOfRefraction, mat);
                }
            }
        }
    }
}
#endif