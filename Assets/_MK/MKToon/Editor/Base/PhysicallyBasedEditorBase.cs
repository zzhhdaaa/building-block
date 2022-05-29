//////////////////////////////////////////////////////
// MK Toon PBS Editor Base						    //
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

namespace MK.Toon.Editor
{
    /// <summary>
    /// Base class for pbs editors
    /// </summary>
    internal abstract class PhysicallyBasedEditorBase : SimpleEditorBase
    {
        public PhysicallyBasedEditorBase()
        {
            _shaderTemplate = ShaderTemplate.PhysicallyBased;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
		// Properties                                                                              //
		/////////////////////////////////////////////////////////////////////////////////////////////     

        /////////////////
        // Options     //
        /////////////////
        private MaterialProperty _workflow;

        /////////////////
        // Input       //
        /////////////////
        private MaterialProperty _metallic;
        private MaterialProperty _roughness;
        private MaterialProperty _roughnessMap;
        private MaterialProperty _metallicMap;
        private MaterialProperty _parallax;
        private MaterialProperty _heightMap;
        private MaterialProperty _lightTransmission;
        private MaterialProperty _lightTransmissionDistortion;
        private MaterialProperty _lightTransmissionColor;
        private MaterialProperty _thicknessMap;
        private MaterialProperty _occlusionMapIntensity;
        private MaterialProperty _occlusionMap;

        /////////////////
        // Detail      //
        /////////////////
        private MaterialProperty _detailBlend;
        private MaterialProperty _detailColor;
        private MaterialProperty _detailMix;
        private MaterialProperty _detailMap;
        private MaterialProperty _detailNormalMapIntensity;
        private MaterialProperty _detailNormalMap;

        /////////////////
        // Stylize     //
        /////////////////
        private MaterialProperty _lightTransmissionRamp;
        private MaterialProperty _lightTransmissionSmoothness;
        private MaterialProperty _lightTransmissionThresholdOffset;
        private MaterialProperty _goochBrightMap;
        private MaterialProperty _goochDarkMap;
        
        /////////////////
        // Advanced    //
        /////////////////
        private MaterialProperty _diffuse;
        private MaterialProperty _specularAnisotrophy;
        private MaterialProperty _lightTransmissionIntensity;
        private MaterialProperty _fresnelHighlights;

        protected override void FindProperties(MaterialProperty[] props)
        {
            base.FindProperties(props);

            _workflow = FindProperty(Properties.workflow.uniform.name, props);
            _metallic = FindProperty(Properties.metallic.uniform.name, props);
            _roughness = FindProperty(Properties.roughness.uniform.name, props);
            _metallicMap = FindProperty(Properties.metallicMap.uniform.name, props);
            _roughnessMap = FindProperty(Properties.roughnessMap.uniform.name, props);
            _parallax = FindProperty(Properties.parallax.uniform.name, props);
            _heightMap = FindProperty(Properties.heightMap.uniform.name, props);
            _lightTransmission = FindProperty(Properties.lightTransmission.uniform.name, props);
            _lightTransmissionDistortion = FindProperty(Properties.lightTransmissionDistortion.uniform.name, props);
            _lightTransmissionColor = FindProperty(Properties.lightTransmissionColor.uniform.name, props);
            _thicknessMap = FindProperty(Properties.thicknessMap.uniform.name, props);
            _occlusionMapIntensity = FindProperty(Properties.occlusionMapIntensity.uniform.name, props);
            _occlusionMap = FindProperty(Properties.occlusionMap.uniform.name, props);

            _detailBlend = FindProperty(Properties.detailBlend.uniform.name, props);
            _detailColor = FindProperty(Properties.detailColor.uniform.name, props);
            _detailMix = FindProperty(Properties.detailMix.uniform.name, props);
            _detailMap = FindProperty(Properties.detailMap.uniform.name, props);
            _detailNormalMapIntensity = FindProperty(Properties.detailNormalMapIntensity.uniform.name, props);
            _detailNormalMap = FindProperty(Properties.detailNormalMap.uniform.name, props);
            
            _lightTransmissionRamp = FindProperty(Properties.lightTransmissionRamp.uniform.name, props);
            _lightTransmissionSmoothness = FindProperty(Properties.lightTransmissionSmoothness.uniform.name, props);
            _lightTransmissionThresholdOffset = FindProperty(Properties.lightTransmissionThresholdOffset.uniform.name, props);
            _goochBrightMap = FindProperty(Properties.goochBrightMap.uniform.name, props);
            _goochDarkMap = FindProperty(Properties.goochDarkMap.uniform.name, props);

            _diffuse = FindProperty(Properties.diffuse.uniform.name, props);
            _specularAnisotrophy = FindProperty(Properties.anisotropy.uniform.name, props);
            _lightTransmissionIntensity = FindProperty(Properties.lightTransmissionIntensity.uniform.name, props);
            _fresnelHighlights = FindProperty(Properties.fresnelHighlights.uniform.name, props);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
		// Draw                                                                                    //
		/////////////////////////////////////////////////////////////////////////////////////////////

        protected override void ConvertSimilarValues(MaterialProperty[] propertiesSrc, Material materialSrc, Material materialDst)
        {
            base.ConvertSimilarValues(propertiesSrc, materialSrc, materialDst);

            MaterialProperty workflowMode = FindProperty("_WorkflowMode", propertiesSrc, false);
            MaterialProperty specColor = FindProperty("_SpecColor", propertiesSrc, false);
            MaterialProperty metallic = FindProperty("_Metallic", propertiesSrc, false);
            MaterialProperty smoothness = FindProperty("_Smoothness", propertiesSrc, false);
            MaterialProperty glossiness = FindProperty("_Glossiness", propertiesSrc, false);
            MaterialProperty metallicGlossMap = FindProperty("_MetallicGlossMap", propertiesSrc, false);
            MaterialProperty glossMapScale = FindProperty("_GlossMapScale", propertiesSrc, false);
            MaterialProperty specGlossMap = FindProperty("_SpecGlossMap", propertiesSrc, false);
            MaterialProperty parallax = FindProperty("_Parallax", propertiesSrc, false);
            MaterialProperty parallaxMap = FindProperty("_ParallaxMap", propertiesSrc, false);
            MaterialProperty occlusionStrength = FindProperty("_OcclusionStrength", propertiesSrc, false);
            MaterialProperty occlusionMap = FindProperty("_OcclusionMap", propertiesSrc, false);
            MaterialProperty glossyReflections = FindProperty("_GlossyReflections", propertiesSrc, false);
            MaterialProperty glossinessSource = FindProperty("_GlossinessSource", propertiesSrc, false);
            MaterialProperty environmentReflections = FindProperty("_EnvironmentReflections", propertiesSrc, false);
            MaterialProperty specularHighlights = FindProperty("_SpecularHighlights", propertiesSrc, false);
            MaterialProperty detailAlbedoMap = FindProperty("_DetailAlbedoMap", propertiesSrc, false);
            MaterialProperty detailNormalMap = FindProperty("_DetailNormalMap", propertiesSrc, false);
            MaterialProperty detailNormalMapScale = FindProperty("_DetailNormalMapScale", propertiesSrc, false);
            MaterialProperty specular = FindProperty("_Specular", propertiesSrc, false);

            bool srcIsMKLit = materialSrc.shader.name.Contains("MK/Toon/") && !materialSrc.shader.name.Contains("Unlit");

            if(!materialSrc.shader.name.Contains("MK/Toon/"))
            {
                switch(materialSrc.shader.name)
                {
                    //Legacy
                    case "Standard":
                        Properties.workflow.SetValue(materialDst, Workflow.Metallic);
                        if(metallicGlossMap != null)
                            Properties.metallicMap.SetValue(materialDst, metallicGlossMap.textureValue);
                        if(metallic != null)
                            Properties.metallic.SetValue(materialDst, metallic.floatValue);
                        if(glossMapScale != null && glossiness != null)
                            Properties.smoothness.SetValue(materialDst, Properties.metallicMap.GetValue(materialDst) ? glossMapScale.floatValue : glossiness.floatValue);
                        Properties.fresnelHighlights.SetValue(materialDst, true);
                    break;
                    case "Standard (Specular setup)":
                        Properties.workflow.SetValue(materialDst, Workflow.Specular);
                        if(specGlossMap != null)
                            Properties.specularMap.SetValue(materialDst, specGlossMap.textureValue);
                        if(specColor != null)
                            Properties.specularColor.SetValue(materialDst, specColor.colorValue);
                        if(glossMapScale != null && glossiness != null)
                            Properties.smoothness.SetValue(materialDst, Properties.specularMap.GetValue(materialDst) ? glossMapScale.floatValue : glossiness.floatValue);
                        Properties.fresnelHighlights.SetValue(materialDst, true);
                    break;
                    case "Autodesk Interactive":
                        Properties.workflow.SetValue(materialDst, Workflow.Roughness);
                        if(metallicGlossMap != null)
                            Properties.metallicMap.SetValue(materialDst, metallicGlossMap.textureValue);
                        if(specGlossMap != null)
                            Properties.roughnessMap.SetValue(materialDst, specGlossMap.textureValue);
                        if(metallic != null)
                            Properties.metallic.SetValue(materialDst, metallic.floatValue);
                        if(glossiness != null)
                            Properties.roughness.SetValue(materialDst, glossiness.floatValue);
                        Properties.fresnelHighlights.SetValue(materialDst, true);
                    break;

                    //URP
                    case "Universal Render Pipeline/Baked Lit":
                    case "Universal Render Pipeline/Simple Lit":
                    case "Universal Render Pipeline/Lit":
                        if(workflowMode != null)
                            Properties.workflow.SetValue(materialDst, workflowMode.floatValue > 0 ? Workflow.Metallic : Workflow.Specular);
                        else
                            Properties.workflow.SetValue(materialDst, Workflow.Metallic);

                        switch(Properties.workflow.GetValue(materialDst))
                        {
                            case Workflow.Specular:
                                if(specGlossMap != null)
                                    Properties.specularMap.SetValue(materialDst, specGlossMap.textureValue);
                                if(specColor != null)
                                    Properties.specularColor.SetValue(materialDst, specColor.colorValue);
                                if(glossMapScale != null)
                                    Properties.smoothness.SetValue(materialDst, Properties.metallicMap.GetValue(materialDst) ? glossMapScale.floatValue : smoothness.floatValue);
                            break;
                            //Metallic
                            default:
                                if(metallicGlossMap != null)
                                    Properties.metallicMap.SetValue(materialDst, metallicGlossMap.textureValue);
                                if(metallic != null)
                                    Properties.metallic.SetValue(materialDst, metallic.floatValue);
                                if(glossMapScale != null)
                                    Properties.smoothness.SetValue(materialDst, Properties.metallicMap.GetValue(materialDst) ? glossMapScale.floatValue : smoothness.floatValue);
                            break;
                        }
                        Properties.fresnelHighlights.SetValue(materialDst, true);
                    break;
                    case "Universal Render Pipeline/Autodesk Interactive":
                    case "Universal Render Pipeline/Autodesk Interactive Masked":
                    case "Universal Render Pipeline/Autodesk Interactive Transparent":
                        Properties.workflow.SetValue(materialDst, Workflow.Roughness);
                        if(metallicGlossMap != null)
                            Properties.metallicMap.SetValue(materialDst, metallicGlossMap.textureValue);
                        if(specGlossMap != null)
                            Properties.roughnessMap.SetValue(materialDst, specGlossMap.textureValue);
                        if(metallic != null)
                            Properties.metallic.SetValue(materialDst, metallic.floatValue);
                        if(glossiness != null)
                            Properties.roughness.SetValue(materialDst, glossiness.floatValue);
                        Properties.fresnelHighlights.SetValue(materialDst, true);
                    break;

                    //Default Fallback Setup
                    default:
                        Properties.workflow.SetValue(materialDst, Workflow.Metallic);
                    break;
                }

                //Inverted Toggle
                if(specularHighlights != null)
                    Properties.specular.SetValue(materialDst, specularHighlights.floatValue > 0 ? Specular.Isotropic : Specular.Off);
                if(parallax != null)
                    Properties.parallax.SetValue(materialDst, parallax.floatValue);
                if(parallaxMap != null)
                    Properties.heightMap.SetValue(materialDst, parallaxMap.textureValue);
                if(occlusionStrength != null)
                    Properties.occlusionMapIntensity.SetValue(materialDst, occlusionStrength.floatValue);
                if(occlusionMap != null)
                    Properties.occlusionMap.SetValue(materialDst, occlusionMap.textureValue);
                if(detailAlbedoMap != null)
                    Properties.detailMap.SetValue(materialDst, detailAlbedoMap.textureValue);
                if(detailNormalMap != null)
                    Properties.detailNormalMap.SetValue(materialDst, detailNormalMap.textureValue);
                if(detailAlbedoMap != null || detailNormalMap != null)
                {
                    Properties.detailTiling.SetValue(materialDst, materialSrc.GetTextureScale(detailAlbedoMap.name));
                    Properties.detailOffset.SetValue(materialDst, materialSrc.GetTextureOffset(detailAlbedoMap.name));
                }
                if(detailNormalMapScale != null)
                    Properties.detailNormalMapIntensity.SetValue(materialDst, detailNormalMapScale.floatValue);
                if(glossyReflections != null)
                    Properties.environmentReflections.SetValue(materialDst, glossyReflections.floatValue > 0 ? EnvironmentReflection.Advanced : EnvironmentReflection.Ambient);
                if(environmentReflections != null)
                    Properties.environmentReflections.SetValue(materialDst, environmentReflections.floatValue > 0 ? EnvironmentReflection.Advanced : EnvironmentReflection.Ambient);
            }
        }
        /////////////////
        // Options     //
        /////////////////
        
        protected virtual void DrawWorkflow(MaterialEditor materialEditor)
        {
            EditorGUI.BeginChangeCheck();
            materialEditor.ShaderProperty(_workflow, UI.workflow);
            if(EditorGUI.EndChangeCheck())
            {
                ManageKeywordsWorkflow();
            }
        }

        protected override void DrawOptionsContent(MaterialEditor materialEditor)
        {
            DrawWorkflow(materialEditor);
            DrawSurfaceType(materialEditor);
            DrawBlend(materialEditor);
            DrawCustomBlend(materialEditor);
            DrawRenderFace(materialEditor);
            DrawAlphaClipping(materialEditor);
        }

        /////////////////
        // Input       //
        /////////////////

        protected virtual void DrawPBSProperties(MaterialEditor materialEditor)
        {
            EditorGUI.BeginChangeCheck();
            if(_workflow.floatValue == (int)MK.Toon.Workflow.Specular)
            {
                materialEditor.TexturePropertySingleLine(UI.specularMap, _specularMap, _specularMap.textureValue == null ? _specularColor : null);
                materialEditor.ShaderProperty(_smoothness, UI.smoothness, 2);
            }
            else if(_workflow.floatValue == (int)MK.Toon.Workflow.Roughness)
            {
                materialEditor.TexturePropertySingleLine(UI.metallicMap, _metallicMap, _metallicMap.textureValue == null ? _metallic : null);
                materialEditor.TexturePropertySingleLine(UI.roughnessMap, _roughnessMap, _roughnessMap.textureValue == null ? _roughness : null);
            }
            else //Metallic
            {
                materialEditor.TexturePropertySingleLine(UI.metallicMap, _metallicMap, _metallicMap.textureValue == null ? _metallic : null);
                materialEditor.ShaderProperty(_smoothness, UI.smoothness, 2);
            }
            if(EditorGUI.EndChangeCheck())
            {
                ManageKeywordsPBSMaps();
            }
        }

        protected virtual void DrawHeightMap(MaterialEditor materialEditor)
        {
            EditorGUI.BeginChangeCheck();
            if (_heightMap.textureValue == null)
                materialEditor.TexturePropertySingleLine(UI.heightMap, _heightMap);
            else
                materialEditor.TexturePropertySingleLine(UI.heightMap, _heightMap, _parallax);
            if (EditorGUI.EndChangeCheck())
            {
                ManageKeywordsParallax();
                ManageKeywordsHeightMap();
            }
        }

        protected virtual void DrawLightTransmission(MaterialEditor materialEditor)
        {
            EditorGUI.BeginChangeCheck();
            if (_lightTransmission.floatValue != (int) LightTransmission.Off)
                materialEditor.TexturePropertySingleLine(UI.thicknessMap, _thicknessMap, _lightTransmissionColor, _lightTransmissionDistortion);
            if (EditorGUI.EndChangeCheck())
            {
                ManageKeywordsThicknessMap();
            }
        }

        protected virtual void DrawOcclusionMap(MaterialEditor materialEditor)
        {
            EditorGUI.BeginChangeCheck();
            if(_occlusionMap.textureValue == null)
                materialEditor.TexturePropertySingleLine(UI.occlusionMap, _occlusionMap);
            else
            {
                materialEditor.TexturePropertySingleLine(UI.occlusionMap, _occlusionMap, _occlusionMapIntensity);
            }
            if(EditorGUI.EndChangeCheck())
            {
                ManageKeywordsOcclusionMap();
            }
        }

        protected void DrawDetailHeader()
        {
            EditorGUILayout.LabelField("Detail: ", UnityEditor.EditorStyles.boldLabel);
        }

        protected void DrawDetailScaleTransform(MaterialEditor materialEditor)
        {
            materialEditor.TextureScaleOffsetProperty(_detailMap);
        }

        protected virtual void DrawDetailMap(MaterialEditor materialEditor)
        {
            EditorGUI.BeginChangeCheck();
            if(_detailMap.textureValue != null)
                materialEditor.TexturePropertySingleLine(UI.detailMap, _detailMap, _detailColor, _detailBlend);
            else
                materialEditor.TexturePropertySingleLine(UI.detailMap, _detailMap);
            if (EditorGUI.EndChangeCheck())
            {
                ManageKeywordsDetailMap();
                ManageKeywordsDetailBlend();
            }
        }

        protected virtual void DrawDetailBlend(MaterialEditor materialEditor)
        {
            if(_detailMap.textureValue != null)
                materialEditor.ShaderProperty(_detailMix, UI.detailMix, 2);
        }

        protected virtual void DrawDetailNormalMap(MaterialEditor materialEditor)
        {
            EditorGUI.BeginChangeCheck();
            if(_detailNormalMap.textureValue == null)
            {
                materialEditor.TexturePropertySingleLine(UI.detailNormalMap, _detailNormalMap);
            }
            else
            {
                materialEditor.TexturePropertySingleLine(UI.detailNormalMap, _detailNormalMap, _detailNormalMapIntensity);
            }
            if(EditorGUI.EndChangeCheck())
            {
                ManageKeywordsDetailNormalMap();
            }
        }

        protected override void DrawInputContent(MaterialEditor materialEditor)
        {
            DrawMainHeader();
            DrawAlbedoMap(materialEditor);
            DrawPBSProperties(materialEditor);
            DrawNormalMap(materialEditor);
            DrawHeightMap(materialEditor);
            DrawLightTransmission(materialEditor);
            DrawOcclusionMap(materialEditor);
            DrawEmissionMap(materialEditor);
            DrawAlbedoScaleTransform(materialEditor);

            EditorHelper.Divider();
            DrawDetailHeader();
            DrawDetailMap(materialEditor);
            DrawDetailBlend(materialEditor);
            DrawDetailNormalMap(materialEditor);
            DrawDetailScaleTransform(materialEditor);
        }

        /////////////////
        // Stylize     //
        /////////////////
        protected override void DrawLightingThreshold(MaterialEditor materialEditor)
        {
            base.DrawLightingThreshold(materialEditor);
            if(_thresholdMap.textureValue != null && _lightTransmission.floatValue != (int) LightTransmission.Off)
                materialEditor.ShaderProperty(_lightTransmissionThresholdOffset, UI.lightTransmissionThresholdOffset);
        }

        protected override void DrawLightingSmoothness(MaterialEditor materialEditor)
        {
            base.DrawLightingSmoothness(materialEditor);
            if(_lightTransmission.floatValue != (int) LightTransmission.Off)
                materialEditor.ShaderProperty(_lightTransmissionSmoothness, UI.lightTransmissionSmoothness);
        }

        protected override void DrawLightingRampWarning()
        {
            //This override could be optimized some day, see base implementation
            bool displayWarning = false;
            bool d = _diffuseRamp.textureValue == null;
            bool s = (Specular) _specular.floatValue != Specular.Off && _specularRamp.textureValue == null;
            bool r = (Rim) _rim.floatValue != Rim.Off && _rimRamp.textureValue == null;
            bool lt = (LightTransmission) _lightTransmission.floatValue != LightTransmission.Off && _lightTransmissionRamp.textureValue == null;

            if((Light) _light.floatValue == Light.Ramp)
            {
                if(d || s || r || lt)
                    displayWarning = true;
            }

            if(displayWarning)
                DrawLightingRampWarningHeader();
        }

        protected override void DrawLightingRamp(MaterialEditor materialEditor)
        {
            base.DrawLightingRamp(materialEditor);
            if((LightTransmission) _lightTransmission.floatValue != LightTransmission.Off)
                materialEditor.TexturePropertySingleLine(UI.lightTransmissionRamp, _lightTransmissionRamp);
        }

        protected override void DrawGooch(MaterialEditor materialEditor)
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
            EditorGUI.BeginChangeCheck();
            materialEditor.TexturePropertySingleLine(UI.goochBrightMap, _goochBrightMap, _goochBrightColor);
            if(EditorGUI.EndChangeCheck())
            {
                ManageKeywordsGoochBrightMap();
            }
            EditorGUI.BeginChangeCheck();
            materialEditor.TexturePropertySingleLine(UI.goochDarkMap, _goochDarkMap, _goochDarkColor);
            if(EditorGUI.EndChangeCheck())
            {
                ManageKeywordsGoochDarkMap();
            }
        }

        /////////////////
        // Advanced    //
        /////////////////
        protected virtual void DrawDiffuseMode(MaterialEditor materialEditor)
        {
            EditorGUI.BeginChangeCheck();
            materialEditor.ShaderProperty(_diffuse, UI.diffuse);
            if (EditorGUI.EndChangeCheck())
            {
                ManageKeywordsDiffuse();
            }
        }

        protected virtual void DrawLightTransmissionMode(MaterialEditor materialEditor)
        {
            EditorGUI.BeginChangeCheck();
            materialEditor.ShaderProperty(_lightTransmission, UI.lightTransmission);
            if (EditorGUI.EndChangeCheck())
            {
                ManageKeywordsLightTransmission();
            }
        }

        protected virtual void DrawAnisotrophy(MaterialEditor materialEditor)
        {
            if(_specular.floatValue == (int)Specular.Anisotropic)
            {
                materialEditor.ShaderProperty(_specularAnisotrophy, UI.anisotropy, 1);
            }
        }

        protected virtual void DrawLightTransmissionIntensity(MaterialEditor materialEditor)
        {
            if(_lightTransmission.floatValue != (int) LightTransmission.Off)
            {
                materialEditor.ShaderProperty(_lightTransmissionIntensity, UI.lightTransmissionIntensity, 1);
            }
        }

        protected virtual void DrawFresnelHighlights(MaterialEditor materialEditor)
        {
            if(_environmentReflections.floatValue != (int)EnvironmentReflection.Off)
            {
                EditorGUI.BeginChangeCheck();
                materialEditor.ShaderProperty(_fresnelHighlights, UI.fresnelHighlights);
                if (EditorGUI.EndChangeCheck())
                {
                    ManageKeywordsFresnelHighlights();
                }
            }
        }

        protected override void DrawAdvancedLighting(MaterialEditor materialEditor)
        {
            DrawLightingHeader();
            DrawReceiveShadows(materialEditor);
            DrawWrappedLighting(materialEditor);
            DrawDiffuseMode(materialEditor);
            DrawSpecularMode(materialEditor);
            DrawAnisotrophy(materialEditor);
            DrawLightTransmissionMode(materialEditor);
            DrawLightTransmissionIntensity(materialEditor);
            DrawEnvironmentReflections(materialEditor);
            DrawFresnelHighlights(materialEditor);
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

        private void ManageKeywordsWorkflow()
        {
            //Workflow
            foreach (Material mat in _workflow.targets)
            {
                EditorHelper.SetKeyword(Properties.workflow.GetValue(mat) == MK.Toon.Workflow.Specular, Keywords.workflow[0], mat);
                EditorHelper.SetKeyword(Properties.workflow.GetValue(mat) == MK.Toon.Workflow.Roughness, Keywords.workflow[1], mat);
                //No Keyword set == Unlit
            }
        }

        private void ManageKeywordsPBSMaps()
        {
            //Workflow
            foreach (Material mat in _workflow.targets)
            {
                EditorHelper.SetKeyword(Properties.metallicMap.GetValue(mat) && (Properties.workflow.GetValue(mat) == Workflow.Metallic || Properties.workflow.GetValue(mat) == Workflow.Roughness) || Properties.specularMap.GetValue(mat) && Properties.workflow.GetValue(mat) == Workflow.Specular, Keywords.pbsMap0, mat);
                EditorHelper.SetKeyword(Properties.roughnessMap.GetValue(mat), Keywords.pbsMap1, mat);
            }
        }

        private void ManageKeywordsHeightMap()
        {
            //Height map
            foreach (Material mat in _heightMap.targets)
            {
                EditorHelper.SetKeyword(Properties.heightMap.GetValue(mat), Keywords.heightMap, mat);
            }
        }

        private void ManageKeywordsParallax()
        {
            //Parallax
            foreach (Material mat in _parallax.targets)
            {
                EditorHelper.SetKeyword(Properties.heightMap.GetValue(mat) && Properties.parallax.GetValue(mat) != 0.0f, Keywords.parallax, mat);
            }
        }

        private void ManageKeywordsLightTransmission()
        {
            //LightTransmission
            foreach (Material mat in _lightTransmission.targets)
            {
                EditorHelper.SetKeyword(Properties.lightTransmission.GetValue(mat) == LightTransmission.Translucent, Keywords.lightTransmission[1], mat);
                EditorHelper.SetKeyword(Properties.lightTransmission.GetValue(mat) == LightTransmission.SubSurfaceScattering, Keywords.lightTransmission[2], mat);
            }
        }

        private void ManageKeywordsOcclusionMap()
        {
            //Occlusion
            foreach (Material mat in _occlusionMap.targets)
            {
                EditorHelper.SetKeyword(Properties.occlusionMap.GetValue(mat), Keywords.occlusionMap, mat);
            }
        }

        private void ManageKeywordsDetailMap()
        {
            //detail albedo
            foreach (Material mat in _detailMap.targets)
            {
                EditorHelper.SetKeyword(Properties.detailMap.GetValue(mat), Keywords.detailMap, mat);
            }
        }
        private void ManageKeywordsDetailBlend()
        {
            //detail albedo blend
            foreach (Material mat in _detailBlend.targets)
            {
                EditorHelper.SetKeyword(Properties.detailBlend.GetValue(mat) == MK.Toon.DetailBlend.Mix, Keywords.detailBlend[1], mat);
                EditorHelper.SetKeyword(Properties.detailBlend.GetValue(mat) == MK.Toon.DetailBlend.Add, Keywords.detailBlend[2], mat);
                //no detail map set = No blending / No Detail required
            }
        }

        private void ManageKeywordsDetailNormalMap()
        {
            //detail bump
            foreach (Material mat in _detailNormalMap.targets)
            {
                EditorHelper.SetKeyword(Properties.detailNormalMap.GetValue(mat), Keywords.detailNormalMap, mat);
            }
        }

        private void ManageKeywordsDiffuse()
        {
            //diffuse light
            foreach (Material mat in _specular.targets)
            {
                EditorHelper.SetKeyword(Properties.diffuse.GetValue(mat) == Diffuse.OrenNayar, Keywords.diffuse[1], mat);
                EditorHelper.SetKeyword(Properties.diffuse.GetValue(mat) == Diffuse.Minnaert, Keywords.diffuse[2], mat);
                //No Keyword = Lambert
            }
        }

        private void ManageKeywordsFresnelHighlights()
        {
            //env reflections
            foreach (Material mat in _environmentReflections.targets)
            {
                EditorHelper.SetKeyword(Properties.fresnelHighlights.GetValue(mat), Keywords.fresnelHighlights, mat);
            }
        }

        private void ManageKeywordsGoochRamp()
        {
            //Gooch Ramp
            foreach (Material mat in _goochRamp.targets)
            {
                EditorHelper.SetKeyword(Properties.goochRamp.GetValue(mat), Keywords.goochRamp, mat);
            }
        }

        private void ManageKeywordsGoochBrightMap()
        {
            //Gooch Bright Map
            foreach (Material mat in _goochBrightMap.targets)
            {
                EditorHelper.SetKeyword(Properties.goochBrightMap.GetValue(mat), Keywords.goochBrightMap, mat);
            }
        }

        private void ManageKeywordsGoochDarkMap()
        {
            //Gooch Dark Map
            foreach (Material mat in _goochDarkMap.targets)
            {
                EditorHelper.SetKeyword(Properties.goochDarkMap.GetValue(mat), Keywords.goochDarkMap, mat);
            }
        }

        private void ManageKeywordsThicknessMap()
        {
            //Thickness
            foreach (Material mat in _thicknessMap.targets)
            {
                EditorHelper.SetKeyword(Properties.thicknessMap.GetValue(mat), Keywords.thicknessMap, mat);
            }
        }

        protected override void UpdateKeywords()
        {
            base.UpdateKeywords();
            ManageKeywordsWorkflow();
            ManageKeywordsHeightMap();
            ManageKeywordsParallax();
            ManageKeywordsLightTransmission();
            ManageKeywordsOcclusionMap();
            ManageKeywordsDetailMap();
            ManageKeywordsDetailBlend();
            ManageKeywordsDetailNormalMap();
            ManageKeywordsDiffuse();
            ManageKeywordsFresnelHighlights();
            ManageKeywordsGoochBrightMap();
            ManageKeywordsGoochDarkMap();
            ManageKeywordsThicknessMap();
            ManageKeywordsPBSMaps();
        }
    }
}
#endif