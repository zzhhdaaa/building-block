//////////////////////////////////////////////////////
// MK Toon Simple Editor Base						//
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
    /// <summary>
    /// Base class for simple lit editors
    /// </summary>
    internal abstract class SimpleEditorBase : UnlitEditorBase
    {
        public SimpleEditorBase()
        {
            _shaderTemplate = ShaderTemplate.Simple;
        }
        /////////////////////////////////////////////////////////////////////////////////////////////
		// Properties                                                                              //
		/////////////////////////////////////////////////////////////////////////////////////////////

        /////////////////
        // Input       //
        /////////////////
        protected MaterialProperty _specularColor;
        protected MaterialProperty _smoothness;
        protected MaterialProperty _specularMap;
        protected MaterialProperty _normalMapIntensity;
        protected MaterialProperty _normalMap;
        protected MaterialProperty _emissionColor;
        protected MaterialProperty _emissionMap;

        /////////////////
        // Stylize     //
        /////////////////
        protected MaterialProperty _diffuseSmoothness;
        protected MaterialProperty _diffuseThresholdOffset;
        protected MaterialProperty _specularSmoothness;
        protected MaterialProperty _specularThresholdOffset;
        protected MaterialProperty _rimSmoothness;
        protected MaterialProperty _rimThresholdOffset;
        protected MaterialProperty _light;
        protected MaterialProperty _diffuseRamp;
        protected MaterialProperty _specularRamp;
        protected MaterialProperty _rimRamp;
        protected MaterialProperty _lightBands;
        protected MaterialProperty _lightBandsScale;
        protected MaterialProperty _lightThreshold;
        protected MaterialProperty _thresholdMap;
        protected MaterialProperty _thresholdMapScale;
        protected MaterialProperty _goochRampIntensity;
        protected MaterialProperty _goochRamp;
        protected MaterialProperty _goochBrightColor;
        protected MaterialProperty _goochDarkColor;
        protected MaterialProperty _iridescence;
        protected MaterialProperty _iridescenceRamp;
        protected MaterialProperty _iridescenceSize;
        protected MaterialProperty _iridescenceColor;
        protected MaterialProperty _iridescenceThresholdOffset;
        protected MaterialProperty _iridescenceSmoothness;
        protected MaterialProperty _rim;
        protected MaterialProperty _rimColor;
        protected MaterialProperty _rimBrightColor;
        protected MaterialProperty _rimDarkColor;
        protected MaterialProperty _rimSize;
        protected MaterialProperty _artistic;
        protected MaterialProperty _artisticProjection;
        protected MaterialProperty _artisticFrequency;
        protected MaterialProperty _drawnMapScale;
        protected MaterialProperty _drawnMap;
        protected MaterialProperty _hatchingMapScale;
        protected MaterialProperty _hatchingBrightMap;
        protected MaterialProperty _hatchingDarkMap;
        protected MaterialProperty _drawnClampMin;
        protected MaterialProperty _drawnClampMax;
        protected MaterialProperty _sketchMapScale;
        protected MaterialProperty _sketchMap;
        
        /////////////////
        // Advanced    //
        /////////////////
        protected MaterialProperty _receiveShadows;
        protected MaterialProperty _wrappedDiffuse;
        protected MaterialProperty _specular;
        protected MaterialProperty _specularIntensity;
        protected MaterialProperty _environmentReflections;

        protected override void FindProperties(MaterialProperty[] props)
        {
            base.FindProperties(props);

            _specularColor = FindProperty(Properties.specularColor.uniform.name, props);
            _smoothness = FindProperty(Properties.smoothness.uniform.name, props);
            _specularMap = FindProperty(Properties.specularMap.uniform.name, props);
            _normalMapIntensity = FindProperty(Properties.normalMapIntensity.uniform.name, props);
            _normalMap = FindProperty(Properties.normalMap.uniform.name, props);
            _emissionColor = FindProperty(Properties.emissionColor.uniform.name, props);
            _emissionMap = FindProperty(Properties.emissionMap.uniform.name, props);
            
            _artistic = FindProperty(Properties.artistic.uniform.name, props);
            _artisticProjection = FindProperty(Properties.artisticProjection.uniform.name, props);
            _artisticFrequency = FindProperty(Properties.artisticFrequency.uniform.name, props);
            _drawnMapScale = FindProperty(Properties.drawnMapScale.uniform.name, props);
            _drawnMap = FindProperty(Properties.drawnMap.uniform.name, props);
            _hatchingMapScale = FindProperty(Properties.hatchingMapScale.uniform.name, props);
            _hatchingBrightMap = FindProperty(Properties.hatchingBrightMap.uniform.name, props);
            _hatchingDarkMap = FindProperty(Properties.hatchingDarkMap.uniform.name, props);
            _drawnClampMin = FindProperty(Properties.drawnClampMin.uniform.name, props);
            _drawnClampMax = FindProperty(Properties.drawnClampMax.uniform.name, props);
            _sketchMapScale = FindProperty(Properties.sketchMapScale.uniform.name, props);
            _sketchMap = FindProperty(Properties.sketchMap.uniform.name, props);
            _diffuseSmoothness = FindProperty(Properties.diffuseSmoothness.uniform.name, props);
            _diffuseThresholdOffset = FindProperty(Properties.diffuseThresholdOffset.uniform.name, props);
            _specularSmoothness = FindProperty(Properties.specularSmoothness.uniform.name, props);
            _specularThresholdOffset = FindProperty(Properties.specularThresholdOffset.uniform.name, props);
            _rimSmoothness = FindProperty(Properties.rimSmoothness.uniform.name, props);
            _rimThresholdOffset = FindProperty(Properties.rimThresholdOffset.uniform.name, props);
            _light = FindProperty(Properties.light.uniform.name, props);
            _diffuseRamp = FindProperty(Properties.diffuseRamp.uniform.name, props);
            _specularRamp = FindProperty(Properties.specularRamp.uniform.name, props);
            _rimRamp = FindProperty(Properties.rimRamp.uniform.name, props);
            _lightBands = FindProperty(Properties.lightBands.uniform.name, props);
            _lightBandsScale = FindProperty(Properties.lightBandsScale.uniform.name, props);
            _lightThreshold = FindProperty(Properties.lightThreshold.uniform.name, props);
            _thresholdMap = FindProperty(Properties.thresholdMap.uniform.name, props);
            _thresholdMapScale = FindProperty(Properties.thresholdMapScale.uniform.name, props);
            _goochRampIntensity = FindProperty(Properties.goochRampIntensity.uniform.name, props);
            _goochRamp = FindProperty(Properties.goochRamp.uniform.name, props);
            _goochBrightColor = FindProperty(Properties.goochBrightColor.uniform.name, props);
            _goochDarkColor = FindProperty(Properties.goochDarkColor.uniform.name, props);
            _iridescence = FindProperty(Properties.iridescence.uniform.name, props);
            _iridescenceRamp = FindProperty(Properties.iridescenceRamp.uniform.name, props);
            _iridescenceSize = FindProperty(Properties.iridescenceSize.uniform.name, props);
            _iridescenceColor = FindProperty(Properties.iridescenceColor.uniform.name, props);
            _iridescenceSmoothness = FindProperty(Properties.iridescenceSmoothness.uniform.name, props);
            _iridescenceThresholdOffset = FindProperty(Properties.iridescenceThresholdOffset.uniform.name, props);
            _rim = FindProperty(Properties.rim.uniform.name, props);
            _rimSize = FindProperty(Properties.rimSize.uniform.name, props);
            _rimColor = FindProperty(Properties.rimColor.uniform.name, props);
            _rimBrightColor = FindProperty(Properties.rimBrightColor.uniform.name, props);
            _rimDarkColor = FindProperty(Properties.rimDarkColor.uniform.name, props);
            
            _receiveShadows = FindProperty(Properties.receiveShadows.uniform.name, props);
            _wrappedDiffuse = FindProperty(Properties.wrappedLighting.uniform.name, props);
            _specular = FindProperty(Properties.specular.uniform.name, props);
            _specularIntensity = FindProperty(Properties.specularIntensity.uniform.name, props);
            _environmentReflections = FindProperty(Properties.environmentReflections.uniform.name, props);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
		// Draw                                                                                    //
		/////////////////////////////////////////////////////////////////////////////////////////////
        protected override void ConvertSimilarValues(MaterialProperty[] propertiesSrc, Material materialSrc, Material materialDst)
        {
            base.ConvertSimilarValues(propertiesSrc, materialSrc, materialDst);

            MaterialProperty specColor = FindProperty("_SpecColor", propertiesSrc, false);
            MaterialProperty glossiness = FindProperty("_Glossiness", propertiesSrc, false);
            MaterialProperty metallicGlossMap = FindProperty("_MetallicGlossMap", propertiesSrc, false);
            MaterialProperty glossMapScale = FindProperty("_GlossMapScale", propertiesSrc, false);
            MaterialProperty specGlossMap = FindProperty("_SpecGlossMap", propertiesSrc, false);
            MaterialProperty bumpMap = FindProperty("_BumpMap", propertiesSrc, false);
            MaterialProperty bumpScale = FindProperty("_BumpScale", propertiesSrc, false);
            MaterialProperty specularHighlights = FindProperty("_SpecularHighlights", propertiesSrc, false);
            MaterialProperty glossyReflections = FindProperty("_GlossyReflections", propertiesSrc, false);
            MaterialProperty environmentReflections = FindProperty("_EnvironmentReflections", propertiesSrc, false);
            MaterialProperty specular = FindProperty("_Specular", propertiesSrc, false);
            
            if(metallicGlossMap != null)
                Properties.specularMap.SetValue(materialDst, metallicGlossMap.textureValue);
            if(specGlossMap != null)
                Properties.specularMap.SetValue(materialDst, specGlossMap.textureValue);
            
            if(specColor != null)
                Properties.specularColor.SetValue(materialDst, specColor.colorValue);
            if(glossiness != null)
                Properties.smoothness.SetValue(materialDst, glossiness.floatValue);
        
            if(bumpScale != null)
                Properties.normalMapIntensity.SetValue(materialDst, bumpScale.floatValue);
            if(bumpMap != null)
                Properties.normalMap.SetValue(materialDst, bumpMap.textureValue);

            bool srctIsMKLitSimple = materialSrc.shader.name.Contains("MK/Toon/") && materialSrc.shader.name.Contains("Simple");
            bool srcIsMKLitPBS = materialSrc.shader.name.Contains("MK/Toon/") && materialSrc.shader.name.Contains("Physically Based");
            bool srcIsMKUnlit = materialSrc.shader.name.Contains("MK/Toon/") && materialSrc.shader.name.Contains("Unlit");

            bool dstIsMKLitSimple = materialDst.shader.name.Contains("MK/Toon/") && materialDst.shader.name.Contains("Simple");
            bool dstIsMKLitPBS = materialDst.shader.name.Contains("MK/Toon/") && materialDst.shader.name.Contains("Physically Based");
            bool dstIsMKUnlit = materialDst.shader.name.Contains("MK/Toon/") && materialDst.shader.name.Contains("Unlit");

            if(specularHighlights != null && !srcIsMKUnlit && !srctIsMKLitSimple && !srcIsMKLitPBS)
                Properties.specular.SetValue(materialDst, specularHighlights.floatValue > 0 ? Specular.Isotropic : Specular.Off);
            else if(specular == null && srcIsMKUnlit && (dstIsMKLitSimple || dstIsMKLitPBS))
                if(dstIsMKLitPBS)
                    Properties.specular.SetValue(materialDst, Properties.specular.GetValue(materialDst));
                else
                    Properties.specular.SetValue(materialDst, Properties.specular.GetValue(materialDst) > 0 ? Specular.Isotropic : Specular.Off);
            else if(srcIsMKLitPBS && dstIsMKLitSimple)
                Properties.specular.SetValue(materialDst, Properties.specular.GetValue(materialSrc) > 0 ? Specular.Isotropic : Specular.Off);
            else
                {}
            
            if(glossyReflections != null && !srcIsMKUnlit && !srctIsMKLitSimple && !srcIsMKLitPBS)
                Properties.environmentReflections.SetValue(materialDst, glossyReflections.floatValue > 0 ? EnvironmentReflection.Advanced : EnvironmentReflection.Ambient);
            else if(environmentReflections == null && srcIsMKUnlit && (dstIsMKLitSimple || dstIsMKLitPBS))
                if(dstIsMKLitPBS)
                    Properties.environmentReflections.SetValue(materialDst, Properties.environmentReflections.GetValue(materialDst));
                else
                    Properties.environmentReflections.SetValue(materialDst, (Properties.environmentReflections.GetValue(materialDst)) > EnvironmentReflection.Ambient ? EnvironmentReflection.Ambient : Properties.environmentReflections.GetValue(materialDst));
            else if(srcIsMKUnlit && (dstIsMKLitSimple || dstIsMKLitPBS))
                if(dstIsMKLitPBS)
                    Properties.environmentReflections.SetValue(materialDst, Properties.environmentReflections.GetValue(materialDst));
                else
                {
                    Properties.environmentReflections.SetValue(materialDst, (Properties.environmentReflections.GetValue(materialDst)) > EnvironmentReflection.Ambient ? EnvironmentReflection.Ambient : Properties.environmentReflections.GetValue(materialDst));
                }
            else if(srcIsMKLitPBS && dstIsMKLitSimple)
                Properties.environmentReflections.SetValue(materialDst, (Properties.environmentReflections.GetValue(materialSrc)) > EnvironmentReflection.Ambient ? EnvironmentReflection.Ambient : Properties.environmentReflections.GetValue(materialSrc));
            else
                {}
        }

        /////////////////
        // Input       //
        /////////////////
        protected virtual void DrawSpecularMap(MaterialEditor materialEditor)
        {
            EditorGUI.BeginChangeCheck();
            materialEditor.TexturePropertySingleLine(UI.specularMap, _specularMap, _specularMap.textureValue == null ? _specularColor : null);
            if(EditorGUI.EndChangeCheck())
            {
                ManageKeywordsSpecularMap();
            }
        }

        protected virtual void DrawSmoothness(MaterialEditor materialEditor)
        {
            materialEditor.ShaderProperty(_smoothness, UI.smoothness, 2);
        }

        protected virtual void DrawNormalMap(MaterialEditor materialEditor)
        {
            EditorGUI.BeginChangeCheck();
            if (_normalMap.textureValue == null)
                materialEditor.TexturePropertySingleLine(UI.normalMap, _normalMap);
            else
                materialEditor.TexturePropertySingleLine(UI.normalMap, _normalMap, _normalMapIntensity);
            if (EditorGUI.EndChangeCheck())
            {
                ManageKeywordsNormalMap();
            }
        }

        protected virtual void DrawEmissionFlags(MaterialEditor materialEditor)
        {
            materialEditor.LightmapEmissionFlagsProperty(MaterialEditor.kMiniTextureFieldLabelIndentLevel, _emissionColor.colorValue.maxColorComponent > 0.0f, true);
        }

        protected virtual void EmissionRealtimeSetup(Material material)
        {

        }

        protected void DrawEmissionMap(MaterialEditor materialEditor)
        {
            EditorGUI.BeginChangeCheck();
            materialEditor.TexturePropertyWithHDRColor(UI.emissionMap, _emissionMap, _emissionColor, false);
            DrawEmissionFlags(materialEditor);
            if(EditorGUI.EndChangeCheck())
            {
                ManageKeywordsEmission();
                ManageKeywordsEmissionMap();
            }
        }

        protected override void DrawInputContent(MaterialEditor materialEditor)
        {
            DrawMainHeader();
            DrawAlbedoMap(materialEditor);
            /*
            using (new EditorGUI.DisabledScope(_specular.floatValue == 0))
            {
                DrawSpecularMap(materialEditor);
                DrawSmoothness(materialEditor);
            }
            */
            if((Specular) _specular.floatValue != Specular.Off)
            {
                DrawSpecularMap(materialEditor);
                DrawSmoothness(materialEditor);
            }
            DrawNormalMap(materialEditor);
            DrawEmissionMap(materialEditor);
            DrawAlbedoScaleTransform(materialEditor);
        }

        /////////////////
        // Stylize     //
        /////////////////
        protected void DrawLightingHeader()
        {
            EditorGUILayout.LabelField("Lighting:", UnityEditor.EditorStyles.boldLabel);
        }
        protected void DrawLightingThresholdOffsetHeader()
        {
            EditorGUILayout.LabelField("Threshold Offset:", UnityEditor.EditorStyles.boldLabel);
        }
        protected void DrawLightingSmoothnessHeader()
        {
            EditorGUILayout.LabelField("Smoothness:", UnityEditor.EditorStyles.boldLabel);
        }
        protected void DrawLightingRampHeader()
        {
            EditorGUILayout.LabelField("Ramps:", UnityEditor.EditorStyles.boldLabel);
        }
        
        protected virtual void DrawLightingThreshold(MaterialEditor materialEditor)
        {
            //EditorGUILayout.LabelField("Threshold:", UnityEditor.EditorStyles.boldLabel);
            DrawLightingThresholdOffsetHeader();
            EditorGUI.BeginChangeCheck();
            if(_thresholdMap.textureValue != null)
                materialEditor.TexturePropertySingleLine(UI.thresholdMap, _thresholdMap, _thresholdMapScale);
            else
                materialEditor.TexturePropertySingleLine(UI.thresholdMap, _thresholdMap);
            if(EditorGUI.EndChangeCheck())
            {
                ManageKeywordsThresholdMap();
            }
            if(_thresholdMap.textureValue != null)
            {
                materialEditor.ShaderProperty(_diffuseThresholdOffset, UI.diffuseThresholdOffset);
                if(_specular.floatValue != (int) Specular.Off)
                    materialEditor.ShaderProperty(_specularThresholdOffset, UI.specularThresholdOffset);
                if(_rim.floatValue != (int) Rim.Off)
                    materialEditor.ShaderProperty(_rimThresholdOffset, UI.rimThresholdOffset);
                if(_iridescence.floatValue != (int) Iridescence.Off)
                    materialEditor.ShaderProperty(_iridescenceThresholdOffset, UI.iridescenceThresholdOffset);
            }
        }

        protected virtual void DrawLightingSmoothness(MaterialEditor materialEditor)
        {
            //EditorGUILayout.LabelField("Smoothness:", UnityEditor.EditorStyles.boldLabel);
            DrawLightingSmoothnessHeader();
            materialEditor.ShaderProperty(_diffuseSmoothness, UI.diffuseSmoothness);
            if(_specular.floatValue != (int) Specular.Off)
                materialEditor.ShaderProperty(_specularSmoothness, UI.specularSmoothness);
            if(_rim.floatValue != (int) Rim.Off)
                materialEditor.ShaderProperty(_rimSmoothness, UI.rimSmoothness);
            if(_iridescence.floatValue != (int) Iridescence.Off)
                materialEditor.ShaderProperty(_iridescenceSmoothness, UI.iridescenceSmoothness);
        }

        protected void DrawLightingRampWarningHeader()
        {
            EditorGUILayout.LabelField("Please set a ramp for every active lighting feature to make the lighting work properly.",  UnityEditor.EditorStyles.wordWrappedLabel);
        }
        protected virtual void DrawLightingRampWarning()
        {
            bool displayWarning = false;
            bool d = _diffuseRamp.textureValue == null;
            bool s = (Specular) _specular.floatValue != Specular.Off && _specularRamp.textureValue == null;
            bool r = (Rim) _rim.floatValue != Rim.Off && _rimRamp.textureValue == null;

            if((Light) _light.floatValue == Light.Ramp)
            {
                if(d || s || r)
                    displayWarning = true;
            }

            if(displayWarning)
                DrawLightingRampWarningHeader();
        }

        protected virtual void DrawLightingRamp(MaterialEditor materialEditor)
        {
            DrawLightingRampHeader();
            DrawLightingRampWarning();
            materialEditor.TexturePropertySingleLine(UI.diffuseRamp, _diffuseRamp);
            if((Specular) _specular.floatValue != Specular.Off)
                materialEditor.TexturePropertySingleLine(UI.specularRamp, _specularRamp);
            if((Rim) _rim.floatValue != Rim.Off)
                materialEditor.TexturePropertySingleLine(UI.rimRamp, _rimRamp);
        }

        protected virtual void DrawLightingStylize(MaterialEditor materialEditor)
        {
            DrawLightingHeader();
            EditorGUI.BeginChangeCheck();
            materialEditor.ShaderProperty(_light, UI.light);
            if(EditorGUI.EndChangeCheck())
            {
                ManageKeywordsLight();
            }
            if (_light.floatValue == (int)(Light.Cel) || _light.floatValue == (int)(Light.Banded))
                materialEditor.ShaderProperty(_lightThreshold, UI.lightThreshold);
            if (_light.floatValue == (int)(Light.Banded))
            {
                materialEditor.ShaderProperty(_lightBands, UI.lightBands);
                materialEditor.ShaderProperty(_lightBandsScale, UI.lightBandsScale);
            }
            EditorHelper.Divider();
            
            DrawLightingThreshold(materialEditor);
            
            if (_light.floatValue == (int)(Light.Ramp))
            {
                EditorHelper.Divider();
                DrawLightingRamp(materialEditor);
            }
            if (_light.floatValue != (int)(Light.Ramp) && _light.floatValue != (int)(Light.Builtin))
            {
                EditorHelper.Divider();
                DrawLightingSmoothness(materialEditor);
            }
        }

        protected void DrawGoochHeader()
        {
            EditorGUILayout.LabelField("Gooch:", UnityEditor.EditorStyles.boldLabel);
        }

        protected virtual void DrawGooch(MaterialEditor materialEditor)
        {
            DrawGoochHeader();
            EditorGUI.BeginChangeCheck();
            if(_goochRamp.textureValue != null)
                materialEditor.TexturePropertySingleLine(UI.goochRamp, _goochRamp, _goochRampIntensity);
            else
                materialEditor.TexturePropertySingleLine(UI.goochRamp, _goochRamp);
            if(EditorGUI.EndChangeCheck())
            {
                ManageKeywordsGoochRamp();
            }
            materialEditor.ShaderProperty(_goochBrightColor, UI.goochBrightMap);
            materialEditor.ShaderProperty(_goochDarkColor, UI.goochDarkMap);
        }

        protected void DrawRimHeader()
        {
            EditorGUILayout.LabelField("Rim:", UnityEditor.EditorStyles.boldLabel);
        }

        protected virtual void DrawRim(MaterialEditor materialEditor)
        {
            //DrawRimHeader();
            EditorGUI.BeginChangeCheck();
            SetBoldFontStyle(true);
            materialEditor.ShaderProperty(_rim, UI.rim);
            SetBoldFontStyle(false);
            if(EditorGUI.EndChangeCheck())
            {
                ManageKeywordsRim();
            }
            if((Rim)_rim.floatValue != Rim.Off)
            {
                if((Rim)_rim.floatValue == Rim.Default)
                {
                    materialEditor.ShaderProperty(_rimColor, UI.rimColor);
                }
                else if((Rim)_rim.floatValue == Rim.Split)
                {
                    materialEditor.ShaderProperty(_rimBrightColor, UI.rimBrightColor);
                    materialEditor.ShaderProperty(_rimDarkColor, UI.rimDarkColor);
                }
                materialEditor.ShaderProperty(_rimSize, UI.rimSize);
            }
        }

        protected void DrawIridescenceHeader()
        {
            EditorGUILayout.LabelField("Iridescence:", UnityEditor.EditorStyles.boldLabel);
        }

        protected virtual void DrawIridescence(MaterialEditor materialEditor)
        {
            //DrawIridescenceHeader();
            EditorGUI.BeginChangeCheck();
            SetBoldFontStyle(true);
            materialEditor.ShaderProperty(_iridescence, UI.iridescence);
            SetBoldFontStyle(false);
            if(EditorGUI.EndChangeCheck())
            {
                ManageKeywordsIridescence();
            }
            if((Iridescence)_iridescence.floatValue != Iridescence.Off)
            {
                materialEditor.TexturePropertySingleLine(UI.iridescenceRamp, _iridescenceRamp, _iridescenceColor);
                materialEditor.ShaderProperty(_iridescenceSize, UI.iridescenceSize, 2);
            }
        }

        protected void DrawArtisticHeader()
        {
            EditorGUILayout.LabelField("Artistic:", UnityEditor.EditorStyles.boldLabel);
        }

        protected virtual void DrawArtistic(MaterialEditor materialEditor)
        {   
            //DrawArtisticHeader();
            EditorGUI.BeginChangeCheck();
            SetBoldFontStyle(true);
            materialEditor.ShaderProperty(_artistic, UI.artistic);
            SetBoldFontStyle(false);
            if(EditorGUI.EndChangeCheck())
            {
                ManageKeywordsArtistic();
            }
            if(_artistic.floatValue != (int)(Artistic.Off))
            {
                EditorGUI.BeginChangeCheck();
                materialEditor.ShaderProperty(_artisticProjection, UI.artisticProjection);
                if(EditorGUI.EndChangeCheck())
                {
                    ManageKeywordsArtisticProjection();
                }
                if(_artistic.floatValue == (int)(Artistic.Hatching))
                {
                    if(_hatchingBrightMap.textureValue != null || _hatchingDarkMap.textureValue != null)
                        materialEditor.TexturePropertySingleLine(UI.hatchingBrightMap, _hatchingBrightMap, _hatchingMapScale);
                    else
                        materialEditor.TexturePropertySingleLine(UI.hatchingBrightMap, _hatchingBrightMap);
                    materialEditor.TexturePropertySingleLine(UI.hatchingDarkMap, _hatchingDarkMap);
                    if(_hatchingBrightMap.textureValue != null || _hatchingDarkMap.textureValue != null)
                    {
                        EditorGUI.BeginChangeCheck();
                        materialEditor.ShaderProperty(_artisticFrequency, UI.artisticStutterFreqency);
                        if(EditorGUI.EndChangeCheck())
                        {
                            ManageKeywordsArtisticAnimation();
                        }
                    }
                }
                else if(_artistic.floatValue == (int)(Artistic.Drawn))
                {
                    if(_drawnMap.textureValue != null)
                    {
                        materialEditor.TexturePropertySingleLine(UI.drawnMap, _drawnMap, _drawnMapScale);
                        materialEditor.ShaderProperty(_drawnClampMin, UI.drawnClampMin);
                        materialEditor.ShaderProperty(_drawnClampMax, UI.drawnClampMax);
                        EditorGUI.BeginChangeCheck();
                        materialEditor.ShaderProperty(_artisticFrequency, UI.artisticStutterFreqency);
                        if(EditorGUI.EndChangeCheck())
                        {
                            ManageKeywordsArtisticAnimation();
                        }
                    }
                    else
                        materialEditor.TexturePropertySingleLine(UI.drawnMap, _drawnMap);
                }
                else if(_artistic.floatValue == (int)(Artistic.Sketch))
                {
                    if(_sketchMap.textureValue != null)
                    {
                        materialEditor.TexturePropertySingleLine(UI.sketchMap, _sketchMap, _sketchMapScale);
                        EditorGUI.BeginChangeCheck();
                        materialEditor.ShaderProperty(_artisticFrequency, UI.artisticStutterFreqency);
                        if(EditorGUI.EndChangeCheck())
                        {
                            ManageKeywordsArtisticAnimation();
                        }
                    }
                    else
                        materialEditor.TexturePropertySingleLine(UI.sketchMap, _sketchMap);
                }
            }
        }

        protected override void DrawStylizeContent(MaterialEditor materialEditor)
        {
            DrawLightingStylize(materialEditor);

            EditorHelper.Divider();
            DrawGooch(materialEditor);

            EditorHelper.Divider();
            DrawRim(materialEditor);

            EditorHelper.Divider();
            DrawIridescence(materialEditor);

            EditorHelper.Divider();
            DrawArtistic(materialEditor);

            EditorHelper.Divider();
            DrawColorGrading(materialEditor);

            EditorHelper.Divider();
            DrawDissolve(materialEditor);

            EditorHelper.Divider();
            DrawVertexAnimation(materialEditor);
        }

        /////////////////
        // Advanced    //
        /////////////////
        protected virtual void DrawReceiveShadows(MaterialEditor materialEditor)
        { 
            EditorGUI.BeginChangeCheck();
            materialEditor.ShaderProperty(_receiveShadows, UI.receiveShadows);
            if (EditorGUI.EndChangeCheck())
            {
                ManageKeywordsReceiveShadows();
            }
        }

        protected virtual void DrawWrappedLighting(MaterialEditor materialEditor)
        { 
            EditorGUI.BeginChangeCheck();
            materialEditor.ShaderProperty(_wrappedDiffuse, UI.wrappedLighting);
            if (EditorGUI.EndChangeCheck())
            {
                ManageKeywordsWrappedDiffuse();
            }
        }

        protected virtual void DrawSpecularMode(MaterialEditor materialEditor)
        { 
            EditorGUI.BeginChangeCheck();
            materialEditor.ShaderProperty(_specular, UI.specular);
            if (EditorGUI.EndChangeCheck())
            {
                ManageKeywordsSpecularMode();
            }
            if(_specular.floatValue != (int)Specular.Off)
            {
                materialEditor.ShaderProperty(_specularIntensity, UI.specularIntensity, 1);
            }
        }

        protected virtual void DrawEnvironmentReflections(MaterialEditor materialEditor)
        {
            EditorGUI.BeginChangeCheck();
            materialEditor.ShaderProperty(_environmentReflections, UI.environmentReflections);
            if (EditorGUI.EndChangeCheck())
            {
                ManageKeywordsEnvironmentReflections();
            }
        }

        protected virtual void DrawAdvancedLighting(MaterialEditor materialEditor)
        {
            DrawLightingHeader();

            DrawReceiveShadows(materialEditor);
            DrawWrappedLighting(materialEditor);
            DrawSpecularMode(materialEditor);
            DrawEnvironmentReflections(materialEditor);
        }

        protected override void DrawPipeline(MaterialEditor materialEditor)
        {
            DrawPipelineHeader();

            materialEditor.EnableInstancingField();
            materialEditor.DoubleSidedGIField();
            DrawRenderPriority(materialEditor);
        }

        protected override void DrawAdvancedContent(MaterialEditor materialEditor)
        {
            DrawAdvancedLighting(materialEditor);
            
            EditorHelper.Divider();
            DrawPipeline(materialEditor);

            EditorHelper.Divider();
            DrawStencil(materialEditor);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
		// Variants Setup                                                                          //
		/////////////////////////////////////////////////////////////////////////////////////////////
        private void ManageKeywordsNormalMap()
        {
            //Normalmap
            foreach (Material mat in _normalMap.targets)
            {
                EditorHelper.SetKeyword(Properties.normalMap.GetValue(mat), Keywords.normalMap, mat);
            }
        }

        private void ManageKeywordsEmission()
        {
            //emission
            foreach (Material mat in _emissionColor.targets)
            {
                EmissionRealtimeSetup(mat);
                MaterialEditor.FixupEmissiveFlag(mat);
                //bool shouldEmissionBeEnabled = (mat.globalIlluminationFlags & MaterialGlobalIlluminationFlags.EmissiveIsBlack) == 0;
                EditorHelper.SetKeyword(Properties.emissionColor.GetValue(mat).maxColorComponent > 0, Keywords.emission, mat);
                //black color emissive = emission Off
            }
        }

        private void ManageKeywordsEmissionMap()
        {
            //emission
            foreach (Material mat in _emissionMap.targets)
            {
                EditorHelper.SetKeyword(Properties.emissionMap.GetValue(mat), Keywords.emissionMap, mat);
            }
        }

        private void ManageKeywordsSpecularMode()
        {
            //spec highlights
            foreach (Material mat in _specular.targets)
            {
                EditorHelper.SetKeyword(Properties.specular.GetValue(mat) == Specular.Isotropic, Keywords.specular[1], mat);
                EditorHelper.SetKeyword(Properties.specular.GetValue(mat) == Specular.Anisotropic, Keywords.specular[2], mat);
                //No Keyword = Specular Off
            }
        }

        private void ManageKeywordsSpecularMap()
        {
            //Workflow
            foreach (Material mat in _specularMap.targets)
            {
                EditorHelper.SetKeyword(Properties.specularMap.GetValue(mat) && _shaderTemplate == ShaderTemplate.Simple, Keywords.pbsMap0, mat);
            }
        }

        private void ManageKeywordsLight()
        {
            //Light Style
            foreach (Material mat in _light.targets)
            {
                EditorHelper.SetKeyword(Properties.light.GetValue(mat) == Light.Cel, Keywords.light[1], mat);
                EditorHelper.SetKeyword(Properties.light.GetValue(mat) == Light.Banded, Keywords.light[2], mat);
                EditorHelper.SetKeyword(Properties.light.GetValue(mat) == Light.Ramp, Keywords.light[3], mat);
                //No keyword = WorldSpace light
            }
        }

        private void ManageKeywordsArtistic()
        {
            //Artistic
            foreach (Material mat in _artistic.targets)
            {
                EditorHelper.SetKeyword(Properties.artistic.GetValue(mat) == Artistic.Drawn , Keywords.artistic[1], mat);
                EditorHelper.SetKeyword(Properties.artistic.GetValue(mat) == Artistic.Hatching , Keywords.artistic[2], mat);
                EditorHelper.SetKeyword(Properties.artistic.GetValue(mat) == Artistic.Sketch , Keywords.artistic[3], mat);
                //No keyword = no artistic
            }
        }

        private void ManageKeywordsArtisticAnimation()
        {
            //Artistic Animation
            foreach (Material mat in _artisticFrequency.targets)
            {
                EditorHelper.SetKeyword(Properties.artisticFrequency.GetValue(mat) != 0.0f , Keywords.artisticAnimation, mat);
            }
        }

        private void ManageKeywordsArtisticProjection()
        {
            //Artistic Projection
            foreach (Material mat in _artisticProjection.targets)
            {
                EditorHelper.SetKeyword(Properties.artisticProjection.GetValue(mat) == ArtisticProjection.ScreenSpace , Keywords.artisticProjection[1], mat);
            }
        }

        private void ManageKeywordsRim()
        {
            //rim style
            foreach (Material mat in _rim.targets)
            {
                EditorHelper.SetKeyword(Properties.rim.GetValue(mat) == Rim.Default, Keywords.rim[1], mat);
                EditorHelper.SetKeyword(Properties.rim.GetValue(mat) == Rim.Split, Keywords.rim[2], mat);
                //No keyword = rim off
            }
        }

        private void ManageKeywordsIridescence()
        {
            //Iridescence
            foreach (Material mat in _iridescence.targets)
            {
                EditorHelper.SetKeyword(Properties.iridescence.GetValue(mat) == Iridescence.On, Keywords.iridescence[1], mat);
                //No keyword = iridescence off
            }
        }

        private void ManageKeywordsGoochRamp()
        {
            //Gooch Ramp
            foreach (Material mat in _goochRamp.targets)
            {
                EditorHelper.SetKeyword(Properties.goochRamp.GetValue(mat), Keywords.goochRamp, mat);
                //No keyword = ramp off
            }
        }

        private void ManageKeywordsReceiveShadows()
        {
            //env reflections
            foreach (Material mat in _receiveShadows.targets)
            {
                EditorHelper.SetKeyword(Properties.receiveShadows.GetValue(mat), Keywords.receiveShadows, mat);
            }
        }

        private void ManageKeywordsWrappedDiffuse()
        {
            //env reflections
            foreach (Material mat in _wrappedDiffuse.targets)
            {
                EditorHelper.SetKeyword(Properties.wrappedLighting.GetValue(mat), Keywords.wrappedLighting, mat);
            }
        }

        private void ManageKeywordsEnvironmentReflections()
        {
            //env reflections
            foreach (Material mat in _environmentReflections.targets)
            {
                EditorHelper.SetKeyword(Properties.environmentReflections.GetValue(mat) == EnvironmentReflection.Ambient, Keywords.environmentReflections[1], mat);
                EditorHelper.SetKeyword(Properties.environmentReflections.GetValue(mat) == EnvironmentReflection.Advanced, Keywords.environmentReflections[2], mat);
                //No Keyword = Reflections Disabled
            }
        }

        private void ManageKeywordsThresholdMap()
        {
            //env reflections
            foreach (Material mat in _thresholdMap.targets)
            {
                EditorHelper.SetKeyword(Properties.thresholdMap.GetValue(mat), Keywords.thresholdMap, mat);
                //No Keyword = Reflections Disabled
            }
        }

        protected override void UpdateKeywords()
        {
            base.UpdateKeywords();
            ManageKeywordsNormalMap();
            ManageKeywordsEmission();
            ManageKeywordsEmissionMap();
            ManageKeywordsSpecularMode();
            ManageKeywordsSpecularMap();
            ManageKeywordsLight();
            ManageKeywordsArtistic();
            ManageKeywordsArtisticProjection();
            ManageKeywordsRim();
            ManageKeywordsIridescence();
            ManageKeywordsReceiveShadows();
            ManageKeywordsWrappedDiffuse();
            ManageKeywordsEnvironmentReflections();
            ManageKeywordsThresholdMap();
            ManageKeywordsGoochRamp();
        }
    }
}

#endif