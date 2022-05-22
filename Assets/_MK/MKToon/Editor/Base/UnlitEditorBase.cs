//////////////////////////////////////////////////////
// MK Toon Unlit Editor Base						//
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

// ------------------------------------------------------------------------------------------
// Note:
// All Editors are based on 3 different Lit modes and should inherit from at least one
// Outline, Refraction and Particles: Are treated as a "component"
//
// Every feature added in the future that use an additional render pass should be treated as a component
// Components should only be drawn if at least one nessecary Property (all properties still required) is found
//
// UnlitBase = ShaderGUI + Particles + Outline + Refraction + virtual base functions
//
// Shader Template:
// UnlitBase => Simple => Physically Based
//
// Enabling/Disabling shader passes would make the whole thing much easier (avoid variants for outline and refraction), however it only works for builtin lightModes, not for custom passes
// ------------------------------------------------------------------------------------------

namespace MK.Toon.Editor
{
    /// <summary>
    /// Base class for unlit editors
    /// </summary>
    internal abstract class UnlitEditorBase : ShaderGUI
    {
        public UnlitEditorBase()
        {
            _shaderTemplate = ShaderTemplate.Unlit;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
		// Properties                                                                              //
		/////////////////////////////////////////////////////////////////////////////////////////////
        protected ShaderTemplate _shaderTemplate;  
   
        /////////////////
        // Options     //
        /////////////////
        protected MaterialProperty _surface;
        protected MaterialProperty _zWrite;
        protected MaterialProperty _blend;
        protected MaterialProperty _zTest;
        protected MaterialProperty _blendSrc;
        protected MaterialProperty _blendDst;
        protected MaterialProperty _alphaClipping;
        protected MaterialProperty _renderFace;

        /////////////////
        // Input       //
        /////////////////
        protected MaterialProperty _albedoColor;
        protected MaterialProperty _alphaCutoff;
        protected MaterialProperty _albedoMap;

        /////////////////
        // Stylize     //
        /////////////////
        protected MaterialProperty _contrast;
        protected MaterialProperty _saturation;
        protected MaterialProperty _brightness;
        protected MaterialProperty _colorGrading;
        protected MaterialProperty _vertexAnimation;
        protected MaterialProperty _vertexAnimationStutter;
        protected MaterialProperty _vertexAnimationFrequency;
        protected MaterialProperty _vertexAnimationIntensity;
        protected MaterialProperty _vertexAnimationMap;
        protected MaterialProperty _dissolve;
        protected MaterialProperty _dissolveMap;
        protected MaterialProperty _dissolveMapScale;
        protected MaterialProperty _dissolveAmount;
        protected MaterialProperty _dissolveBorderSize;
        protected MaterialProperty _dissolveBorderRamp;
        protected MaterialProperty _dissolveBorderColor;

        /////////////////
        // Advanced    //
        /////////////////
        protected MaterialProperty _renderPriority;

        //Stencil
        protected MaterialProperty _stencil;
        protected MaterialProperty _stencilRef;
        protected MaterialProperty _stencilReadMask;
        protected MaterialProperty _stencilWriteMask;
        protected MaterialProperty _stencilComp;
        protected MaterialProperty _stencilPass;
        protected MaterialProperty _stencilFail;
        protected MaterialProperty _stencilZFail;

        /////////////////
        // Editor Only //
        /////////////////
        protected MaterialProperty _initialized;
        protected MaterialProperty _optionsTab;
        protected MaterialProperty _inputTab;
        protected MaterialProperty _stylizeTab;
        protected MaterialProperty _advancedTab;
        protected FontStyle _defaultFontStyle;

        /////////////////
        // Outline     //
        /////////////////
        protected MK.Toon.Editor.OutlineComponent _outline = new MK.Toon.Editor.OutlineComponent();

        /////////////////
        // Refraction  //
        /////////////////
        protected MK.Toon.Editor.RefractionComponent _refraction = new MK.Toon.Editor.RefractionComponent();

        /////////////////
        // Particles   //
        /////////////////
        protected MK.Toon.Editor.ParticlesComponent _particles = new MK.Toon.Editor.ParticlesComponent();

        /// <summary>
        /// Find Properties to draw the editor
        /// </summary>
        /// <param name="props"></param>
        protected virtual void FindProperties(MaterialProperty[] props)
        {
            _surface = FindProperty(Properties.surface.uniform.name, props);
            _zWrite = FindProperty(Properties.zWrite.uniform.name, props);
            _blend = FindProperty(Properties.blend.uniform.name, props);
            _zTest = FindProperty(Properties.zTest.uniform.name, props);
            _blendSrc = FindProperty(Properties.blendSrc.uniform.name, props);
            _blendDst = FindProperty(Properties.blendDst.uniform.name, props);
            _alphaClipping = FindProperty(Properties.alphaClipping.uniform.name, props);
            _renderFace = FindProperty(Properties.renderFace.uniform.name, props);

            _albedoColor = FindProperty(Properties.albedoColor.uniform.name, props);
            _alphaCutoff = FindProperty(Properties.alphaCutoff.uniform.name, props);
            _albedoMap = FindProperty(Properties.albedoMap.uniform.name, props);

            _colorGrading = FindProperty(Properties.colorGrading.uniform.name, props);
            _contrast = FindProperty(Properties.contrast.uniform.name, props);
            _saturation = FindProperty(Properties.saturation.uniform.name, props);
            _brightness = FindProperty(Properties.brightness.uniform.name, props);

            _vertexAnimation = FindProperty(Properties.vertexAnimation.uniform.name, props);
            _vertexAnimationStutter = FindProperty(Properties.vertexAnimationStutter.uniform.name, props);
            _vertexAnimationFrequency = FindProperty(Properties.vertexAnimationFrequency.uniform.name, props);
            _vertexAnimationIntensity = FindProperty(Properties.vertexAnimationIntensity.uniform.name, props);
            _vertexAnimationMap = FindProperty(Properties.vertexAnimationMap.uniform.name, props);

            _dissolve = FindProperty(Properties.dissolve.uniform.name, props);
            _dissolveMap = FindProperty(Properties.dissolveMap.uniform.name, props);
            _dissolveMapScale = FindProperty(Properties.dissolveMapScale.uniform.name, props);
            _dissolveAmount = FindProperty(Properties.dissolveAmount.uniform.name, props);
            _dissolveBorderRamp = FindProperty(Properties.dissolveBorderRamp.uniform.name, props);
            _dissolveBorderSize = FindProperty(Properties.dissolveBorderSize.uniform.name, props);
            _dissolveBorderColor = FindProperty(Properties.dissolveBorderColor.uniform.name, props);
            
            _renderPriority = FindProperty(Properties.renderPriority.uniform.name, props);

            _stencil = FindProperty(Properties.stencil.uniform.name, props);
            _stencilRef = FindProperty(Properties.stencilRef.uniform.name, props);
            _stencilReadMask = FindProperty(Properties.stencilReadMask.uniform.name, props);
            _stencilWriteMask = FindProperty(Properties.stencilWriteMask.uniform.name, props);
            _stencilComp = FindProperty(Properties.stencilComp.uniform.name, props);
            _stencilPass = FindProperty(Properties.stencilPass.uniform.name, props);
            _stencilFail = FindProperty(Properties.stencilFail.uniform.name, props);
            _stencilZFail = FindProperty(Properties.stencilZFail.uniform.name, props);

            _initialized = FindProperty(EditorProperties.initialized.uniform.name, props);
            _optionsTab = FindProperty(EditorProperties.optionsTab.uniform.name, props);
            _inputTab = FindProperty(EditorProperties.inputTab.uniform.name, props);
            _stylizeTab = FindProperty(EditorProperties.stylizeTab.uniform.name, props);
            _advancedTab = FindProperty(EditorProperties.advancedTab.uniform.name, props);

            _outline.FindProperties(props);
            _refraction.FindProperties(props);
            _particles.FindProperties(props);
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
        protected virtual void ConvertSimilarValues(MaterialProperty[] propertiesSrc, Material materialSrc, Material materialDst)
        {
            MaterialProperty mainTex = FindProperty("_MainTex", propertiesSrc, false);
            MaterialProperty color = FindProperty("_Color", propertiesSrc, false);
            MaterialProperty baseTex = FindProperty("_BaseTex", propertiesSrc, false);
            MaterialProperty baseMap = FindProperty("_BaseMap", propertiesSrc, false);
            MaterialProperty baseColor = FindProperty("_BaseColor", propertiesSrc, false);
            MaterialProperty cutoff = FindProperty("_Cutoff", propertiesSrc, false);
            MaterialProperty cull = FindProperty("_Cull", propertiesSrc, false);
            MaterialProperty alphaClip = FindProperty("_AlphaClip", propertiesSrc, false);
            MaterialProperty surface = FindProperty("_SurfaceType", propertiesSrc, false);
            MaterialProperty mode = FindProperty("_Mode", propertiesSrc, false);
            MaterialProperty blend = FindProperty("_BlendMode", propertiesSrc, false);

            if(mode != null)
                Properties.surface.SetValue(materialDst, mode.floatValue <= 1 ? Surface.Opaque : Surface.Transparent);
            if(surface != null)
                Properties.surface.SetValue(materialDst, surface.floatValue > 0 ? Surface.Transparent : Surface.Opaque);
            if(blend != null)
                Properties.blend.SetValue(materialDst, (Blend) ((int) blend.floatValue));

            if(materialSrc.shader.name.Contains("Universal Render Pipeline") || materialSrc.shader.name.Contains("Lightweight Render Pipeline"))
            {
                if(alphaClip != null)
                    Properties.alphaClipping.SetValue(materialDst, alphaClip.floatValue > 0 ? true : false);
                if(baseTex != null)
                    Properties.albedoMap.SetValue(materialDst, baseTex.textureValue);
                if(baseMap != null)
                    Properties.albedoMap.SetValue(materialDst, baseMap.textureValue);
                if(baseColor != null)
                    Properties.albedoColor.SetValue(materialDst, baseColor.colorValue);
            }
            else
            {
                if(mode != null)
                    Properties.alphaClipping.SetValue(materialDst, mode.floatValue == 1 ? true : false);
                if(mainTex != null)
                {
                    Properties.albedoMap.SetValue(materialDst, mainTex.textureValue);
                    Properties.mainTiling.SetValue(materialDst, materialSrc.mainTextureScale);
                    Properties.mainOffset.SetValue(materialDst, materialSrc.mainTextureOffset);
                }
                if(color != null)
                    Properties.albedoColor.SetValue(materialDst, color.colorValue);
            }

            if(cutoff != null)
                Properties.alphaCutoff.SetValue(materialDst, cutoff.floatValue);
            if(cull != null)
                Properties.renderFace.SetValue(materialDst, (RenderFace) cull.floatValue);
        }

        /// <summary>
        /// Unity Message AssignNewShaderToMaterial, override MaterialSetup instead
        /// </summary>
        /// <param name="materialDst"></param>
        /// <param name="oldShader"></param>
        /// <param name="newShader"></param>
        public override void AssignNewShaderToMaterial(Material materialDst, Shader oldShader, Shader newShader)
        {
            MaterialSetup(materialDst, oldShader, newShader);
        }

        protected void SetBoldFontStyle(bool b)
        {
            EditorStyles.label.fontStyle = b ? FontStyle.Bold : _defaultFontStyle;
        }

        /// <summary>
        /// Unity Message OnGUI, override DrawInspector instead
        /// </summary>
        /// <param name="materialEditor"></param>
        /// <param name="properties"></param>
        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
        {
            _defaultFontStyle = EditorStyles.label.fontStyle;
            FindProperties(properties);

            DrawInspector(materialEditor, properties);
            if(_initialized.floatValue == 0)
            {
                foreach(Material mat in _initialized.targets)
                {
                    EditorProperties.initialized.SetValue(mat, true);
                }
                Initialize(materialEditor);
            }
        }

        protected virtual void Initialize(MaterialEditor materialEditor) 
        {
            UpdateKeywords();
        }

        /// <summary>
        /// Setup the material when shader is changed
        /// </summary>
        /// <param name="materialDst"></param>
        /// <param name="oldShader"></param>
        /// <param name="newShader"></param>
        protected virtual void MaterialSetup(Material materialDst, Shader oldShader, Shader newShader)
        {            
            Material materialSrc = new Material(materialDst);
            MaterialProperty[] propertiesSrc = MaterialEditor.GetMaterialProperties(new Material[] { materialSrc });

            base.AssignNewShaderToMaterial(materialDst, oldShader, newShader);

            MaterialProperty[] propertiesDst = MaterialEditor.GetMaterialProperties(new Material[] { materialDst });
            FindProperties(propertiesDst);
            _particles.FindProperties(propertiesDst);
            _outline.FindProperties(propertiesDst);
            _refraction.FindProperties(propertiesDst);

            ConvertSimilarValues(propertiesSrc, materialSrc, materialDst);

            if(_outline.active)
            {
                Properties.surface.SetValue(materialDst, Surface.Opaque, Properties.alphaClipping.GetValue(materialDst));
            }
            if(_refraction.active)
            {
                Properties.surface.SetValue(materialDst, Surface.Transparent, Properties.alphaClipping.GetValue(materialDst));
            }

            UpdateKeywords();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
		// Draw                                                                                    //
		/////////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>
        /// Unlit warning for legacy renderpipeline
        /// </summary>
        protected void LegacyUnlitWarning()
        {
            EditorGUILayout.HelpBox("To completely unlit, please disable shadow casting & recieving on the MeshRenderer", MessageType.Info);
        }

        /////////////////
        // Options     //
        /////////////////
        protected bool OptionsBehavior(MaterialEditor materialEditor)
        {
            return EditorHelper.HandleBehavior("Options", "", _optionsTab, null, materialEditor, false);
        }
        
        protected virtual void DrawSurfaceType(MaterialEditor materialEditor)
        {
            EditorGUI.BeginChangeCheck();
            materialEditor.ShaderProperty(_surface, UI.surface);
            if(EditorGUI.EndChangeCheck())
            {
                ManageKeywordsSurface();
            }
        }

        /*
        protected virtual void DrawZWrite(MaterialEditor materialEditor)
        {
            EditorGUI.BeginChangeCheck();
            materialEditor.ShaderProperty(_zWrite, UI.zWrite, 1);
            EditorGUI.EndChangeCheck();
        }
        */

        protected virtual void DrawCustomBlend(MaterialEditor materialEditor)
        {
            if((Blend) _blend.floatValue == Blend.Custom)
            {
                materialEditor.ShaderProperty(_zWrite, UI.zWrite, 1);
                materialEditor.ShaderProperty(_zTest, UI.zTest, 1);
                materialEditor.ShaderProperty(_blendSrc, UI.blendSrc, 1);
                materialEditor.ShaderProperty(_blendDst, UI.blendDst, 1);
            }
        }

        protected virtual void DrawBlend(MaterialEditor materialEditor)
        {
            /*
            EditorGUI.BeginChangeCheck();
            if((Surface)_surface.floatValue == Surface.Transparent)
                materialEditor.ShaderProperty(_blend, UI.blend);
            if(EditorGUI.EndChangeCheck())
            {
                ManageKeywordsBlend();
            }
            */

            EditorGUI.showMixedValue = _blend.hasMixedValue;
            Blend blend = (Blend) _blend.floatValue;

            EditorGUI.BeginChangeCheck();
            if((Surface) _surface.floatValue == Surface.Transparent)
                blend = (Blend) EditorGUILayout.EnumPopup(UI.blend, (Blend) blend);
            else
                blend = (Blend) EditorGUILayout.EnumPopup(UI.blend, (BlendOpaque) blend);
            if(EditorGUI.EndChangeCheck())
            {
                materialEditor.RegisterPropertyChangeUndo("Blend");
                _blend.floatValue = (int) blend;
                ManageKeywordsBlend();
                ManageKeywordsSurface();
            }
            EditorGUI.showMixedValue = false;
        }

        protected virtual void DrawRenderFace(MaterialEditor materialEditor)
        {
            materialEditor.ShaderProperty(_renderFace, UI.renderFace);
        }

        protected virtual void DrawAlphaClipping(MaterialEditor materialEditor)
        {
            EditorGUI.BeginChangeCheck();
            materialEditor.ShaderProperty(_alphaClipping, UI.alphaClipping);
            if(EditorGUI.EndChangeCheck())
            {
                ManageKeywordsAlphaClipping();
                UpdateSystemProperties();
            }
            if(_alphaClipping.floatValue == 1)
            {
                EditorGUI.BeginChangeCheck();
                materialEditor.ShaderProperty(_alphaCutoff, UI.alphaCutoff);
                if(EditorGUI.EndChangeCheck())
                {
                    UpdateSystemProperties();
                }
            }
        }

        /// <summary>
        /// Draw the content of the Options Behavior
        /// </summary>
        /// <param name="materialEditor"></param>
        protected virtual void DrawOptionsContent(MaterialEditor materialEditor)
        {
            DrawSurfaceType(materialEditor);
            DrawBlend(materialEditor);
            DrawCustomBlend(materialEditor);
            DrawRenderFace(materialEditor);
            DrawAlphaClipping(materialEditor);
        }

        private void DrawOptions(MaterialEditor materialEditor)
        {
            EditorHelper.DrawSplitter();
            if(OptionsBehavior(materialEditor))
            {
                DrawOptionsContent(materialEditor);
            }
        }

        /////////////////
        // Input       //
        /////////////////
        protected bool InputBehavior(MaterialEditor materialEditor)
        {
            return EditorHelper.HandleBehavior("Input", "", _inputTab, null, materialEditor, true);
        }

        protected void DrawMainHeader()
        {
            EditorGUILayout.LabelField("Main: ", UnityEditor.EditorStyles.boldLabel);
        }

        protected virtual void DrawAlbedoMap(MaterialEditor materialEditor)
        {
            EditorGUI.BeginChangeCheck();
            materialEditor.TexturePropertySingleLine(UI.albedoMap, _albedoMap, _albedoColor);
            if(EditorGUI.EndChangeCheck())
            {
                ManageKeywordsAlbedoMap();
                UpdateSystemProperties();
            }
        }

        protected void DrawAlbedoScaleTransform(MaterialEditor materialEditor)
        {
            materialEditor.TextureScaleOffsetProperty(_albedoMap);
        }
            
        /// <summary>
        /// Draw the Input Content
        /// </summary>
        /// <param name="materialEditor"></param>
        protected virtual void DrawInputContent(MaterialEditor materialEditor)
        {
            DrawMainHeader();
            DrawAlbedoMap(materialEditor);
            DrawAlbedoScaleTransform(materialEditor);
        }

        private void DrawInput(MaterialEditor materialEditor)
        {
            if(InputBehavior(materialEditor))
            {
                DrawInputContent(materialEditor);
            }
        }

        /////////////////
        // Stylize     //
        /////////////////
        protected bool StylizeBehavior(MaterialEditor materialEditor)
        {
            return EditorHelper.HandleBehavior("Stylize", "", _stylizeTab, null, materialEditor, true);
        }

        protected void DrawColorGradingHeader()
        {
            EditorGUILayout.LabelField("Color Grading:", UnityEditor.EditorStyles.boldLabel);
        }

        protected virtual void DrawColorGrading(MaterialEditor materialEditor)
        {
            //DrawColorGradingHeader();
            EditorGUI.BeginChangeCheck();
            SetBoldFontStyle(true);
            materialEditor.ShaderProperty(_colorGrading, UI.colorGrading);
            SetBoldFontStyle(false);
            if(EditorGUI.EndChangeCheck())
            {
                ManageKeywordsColorGrading();
            }
            if(_colorGrading.floatValue != (int)(ColorGrading.Off))
            {
                materialEditor.ShaderProperty(_contrast, UI.contrast);
                materialEditor.ShaderProperty(_saturation, UI.saturation);
                materialEditor.ShaderProperty(_brightness, UI.brightness);
            }
        }

        protected void DrawDissolveHeader()
        {
            EditorGUILayout.LabelField("Dissolve:", UnityEditor.EditorStyles.boldLabel);
        }

        protected virtual void DrawDissolve(MaterialEditor materialEditor)
        {
            //DrawDissolveHeader();
            EditorGUI.BeginChangeCheck();
            SetBoldFontStyle(true);
            materialEditor.ShaderProperty(_dissolve, UI.dissolve);
            SetBoldFontStyle(false);
            if (EditorGUI.EndChangeCheck())
            {
                ManageKeywordsDissolve();
            }
            if(_dissolve.floatValue != (int)Dissolve.Off)
            {
                if(_dissolveMap.textureValue != null)
                    materialEditor.TexturePropertySingleLine(UI.dissolveMap, _dissolveMap, _dissolveMapScale);
                else
                    materialEditor.TexturePropertySingleLine(UI.dissolveMap, _dissolveMap);

                if(_dissolveMap.textureValue != null)
                {
                    materialEditor.ShaderProperty(_dissolveAmount, UI.dissolveAmount);
                    if(_dissolve.floatValue == (int)Dissolve.BorderRamp)
                    {
                        materialEditor.TexturePropertySingleLine(UI.dissolveBorderRamp, _dissolveBorderRamp, _dissolveBorderSize, _dissolveBorderColor);
                    }
                    else if(_dissolve.floatValue == (int)Dissolve.BorderColor)
                    {
                        materialEditor.ShaderProperty(_dissolveBorderColor, UI.dissolveBorderColor);
                        materialEditor.ShaderProperty(_dissolveBorderSize, UI.dissolveBorderSize);
                    }
                }
            }
        }

        protected void DrawVertexAnimationHeader()
        {
            EditorGUILayout.LabelField("Vertex Animation:", UnityEditor.EditorStyles.boldLabel);
        }

        protected virtual void DrawVertexAnimation(MaterialEditor materialEditor)
        {
            //DrawVertexAnimationHeader();
            EditorGUI.BeginChangeCheck();
            SetBoldFontStyle(true);
            materialEditor.ShaderProperty(_vertexAnimation, UI.vertexAnimation);
            SetBoldFontStyle(false);
            if (EditorGUI.EndChangeCheck())
            {
                ManageKeywordsVertexAnimation();
            }

            if((VertexAnimation) _vertexAnimation.floatValue != VertexAnimation.Off)
            {
                EditorGUI.BeginChangeCheck();
                materialEditor.TexturePropertySingleLine(UI.vertexAnimationMap, _vertexAnimationMap, _vertexAnimationIntensity);
                if (EditorGUI.EndChangeCheck())
                {
                    ManageKeywordsVertexAnimationMap();
                }
                EditorGUI.BeginChangeCheck();
                materialEditor.ShaderProperty(_vertexAnimationStutter, UI.vertexAnimationStutter);
                if (EditorGUI.EndChangeCheck())
                {
                    ManageKeywordsVertexAnimationStutter();
                }
                materialEditor.ShaderProperty(_vertexAnimationFrequency, UI.vertexAnimationFrequency);
            }
        }

        /// <summary>
        /// Draw Stylize Content
        /// </summary>
        /// <param name="materialEditor"></param>
        protected virtual void DrawStylizeContent(MaterialEditor materialEditor)
        {
            DrawColorGrading(materialEditor);

            EditorHelper.VerticalSpace();

            DrawDissolve(materialEditor);

            EditorHelper.VerticalSpace();
            DrawVertexAnimation(materialEditor);
        }

        private void DrawStylize(MaterialEditor materialEditor)
        {
            if(StylizeBehavior(materialEditor))
            {
                DrawStylizeContent(materialEditor);
            }
        }

        /////////////////
        // Advanced    //
        /////////////////
        protected bool AdvancedBehavior(MaterialEditor materialEditor)
        {
            return EditorHelper.HandleBehavior("Advanced", "", _advancedTab, null, materialEditor, true);
        }

        //Stencil Builtin
        private void SetBuiltinStencilSettings(MaterialEditor materialEditor)
        {
            EditorGUI.BeginChangeCheck();
            foreach(Material mat in _stencil.targets)
            {
                Properties.stencil.SetValue(mat, Stencil.Builtin);
            }
            if(EditorGUI.EndChangeCheck())
            {
                materialEditor.RegisterPropertyChangeUndo("Stencil Mode");
            }
        }

        protected void DrawStencilHeader()
        {
            EditorGUILayout.LabelField("Stencil:", UnityEditor.EditorStyles.boldLabel);
        }

        protected void DrawPipelineHeader()
        {
            EditorGUILayout.LabelField("Pipeline:", UnityEditor.EditorStyles.boldLabel);
        }

        protected void DrawRenderPriority(MaterialEditor materialEditor)
        {
            EditorGUI.BeginChangeCheck();
            materialEditor.ShaderProperty(_renderPriority, UI.renderPriority);
            if(EditorGUI.EndChangeCheck())
            {
                UpdateRenderPriority();
            }
        }

        protected virtual void DrawPipeline(MaterialEditor materialEditor)
        {
            DrawPipelineHeader();

            materialEditor.EnableInstancingField();
            DrawRenderPriority(materialEditor);
        }

        protected virtual void DrawStencil(MaterialEditor materialEditor)
        {
            DrawStencilHeader();

            materialEditor.ShaderProperty(_stencil, UI.stencil);
            if(_stencil.floatValue == (int)Stencil.Custom)
            {
                materialEditor.ShaderProperty(_stencilRef, UI.stencilRef);
                materialEditor.ShaderProperty(_stencilReadMask, UI.stencilReadMask);
                materialEditor.ShaderProperty(_stencilWriteMask, UI.stencilWriteMask);
                materialEditor.ShaderProperty(_stencilComp, UI.stencilComp);
                materialEditor.ShaderProperty(_stencilPass, UI.stencilPass);
                materialEditor.ShaderProperty(_stencilFail, UI.stencilFail);
                materialEditor.ShaderProperty(_stencilZFail, UI.stencilZFail);
            }
            else// if(_stencil.floatValue == (int)Stencil.Builtin)
            {
                SetBuiltinStencilSettings(materialEditor);
            }
        }

        /// <summary>
        /// Draw Advanced Content
        /// </summary>
        /// <param name="materialEditor"></param>
        protected virtual void DrawAdvancedContent(MaterialEditor materialEditor)
        {            
            DrawPipeline(materialEditor);
            EditorHelper.Divider();
            DrawStencil(materialEditor);
        }

        private void DrawAdvanced(MaterialEditor materialEditor)
        {
            if(AdvancedBehavior(materialEditor))
            {
                DrawAdvancedContent(materialEditor);
            }
            EditorHelper.DrawSplitter();
        }

        /// <summary>
        /// Draw the complete editor based on the tabs
        /// </summary>
        /// <param name="materialEditor"></param>
        /// <param name="properties"></param>
        protected virtual void DrawInspector(MaterialEditor materialEditor, MaterialProperty[] properties)
        {
            //get properties
            FindProperties(properties);

            //EditorGUI.BeginChangeCheck();
            DrawOptions(materialEditor);
            DrawInput(materialEditor);
            DrawStylize(materialEditor);
            DrawAdvanced(materialEditor);
            _particles.DrawParticles(materialEditor, properties, _surface, _shaderTemplate);
            _refraction.DrawRefraction(materialEditor, properties);
            _outline.DrawOutline(materialEditor, properties);

            //EditorGUI.EndChangeCheck();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
		// Variants Setup                                                                          //
		/////////////////////////////////////////////////////////////////////////////////////////////
        
        private void ManageKeywordsBlend()
        {
            //Colorsource
            foreach (Material mat in _blend.targets)
            {
                Blend bm = Properties.blend.GetValue(mat);
                EditorHelper.SetKeyword(Properties.blend.GetValue(mat) == Blend.Premultiply, Keywords.blend[1], mat);
                EditorHelper.SetKeyword(Properties.blend.GetValue(mat) == Blend.Additive, Keywords.blend[2], mat);
                EditorHelper.SetKeyword(Properties.blend.GetValue(mat) == Blend.Multiply, Keywords.blend[3], mat);
                EditorHelper.SetKeyword(Properties.blend.GetValue(mat) == Blend.Custom, Keywords.blend[4], mat);
                //No Keyword == Alpha

                Properties.blend.SetValue(mat, bm);
            }
        }

        private void ManageKeywordsAlbedoMap()
        {
            //Colorsource
            foreach (Material mat in _albedoMap.targets)
            {
                EditorHelper.SetKeyword(Properties.albedoMap.GetValue(mat), Keywords.albedoMap, mat);
                //No Keyword == Vertex Colors
            }
        }

        private void ManageKeywordsSurface()
        {
            //Surface Type
            foreach (Material mat in _surface.targets)
            {
                Properties.surface.SetValue(mat, Properties.surface.GetValue(mat), Properties.alphaClipping.GetValue(mat));
                //No Keyword == Opaque
            }
        }

        private void ManageKeywordsAlphaClipping()
        {
            //Alpha Clipping
            foreach (Material mat in _alphaClipping.targets)
            {
                Properties.alphaClipping.SetValue(mat, Properties.alphaClipping.GetValue(mat));
                //No Keyword == No Alpha Clipping
            }
        }

        private void ManageKeywordsColorGrading()
        {
            //ColorGrading
            foreach (Material mat in _colorGrading.targets)
            {
                EditorHelper.SetKeyword(Properties.colorGrading.GetValue(mat) == ColorGrading.Albedo, Keywords.colorGrading[1], mat);
                EditorHelper.SetKeyword(Properties.colorGrading.GetValue(mat) == ColorGrading.FinalOutput, Keywords.colorGrading[2], mat);
                //No keyword = No ColorGrading
            }
        }

        private void ManageKeywordsDissolve()
        {
            //Dissolve
            foreach (Material mat in _dissolve.targets)
            {
                EditorHelper.SetKeyword(Properties.dissolve.GetValue(mat) == Dissolve.Default, Keywords.dissolve[1], mat);
                EditorHelper.SetKeyword(Properties.dissolve.GetValue(mat) == Dissolve.BorderColor, Keywords.dissolve[2], mat);
                EditorHelper.SetKeyword(Properties.dissolve.GetValue(mat) == Dissolve.BorderRamp, Keywords.dissolve[3], mat);
                //No Keyword = Dissolve Off
            }
        }

        private void ManageKeywordsVertexAnimation()
        {
            //Vertex animation
            foreach (Material mat in _vertexAnimation.targets)
            {
                EditorHelper.SetKeyword(Properties.vertexAnimation.GetValue(mat) == VertexAnimation.Sine, Keywords.vertexAnimation[1], mat);
                EditorHelper.SetKeyword(Properties.vertexAnimation.GetValue(mat) == VertexAnimation.Pulse, Keywords.vertexAnimation[2], mat);
                EditorHelper.SetKeyword(Properties.vertexAnimation.GetValue(mat) == VertexAnimation.Noise, Keywords.vertexAnimation[3], mat);
                //No Keyword = Vertex Animation Off
            }
        }

        private void ManageKeywordsVertexAnimationStutter()
        {
            foreach (Material mat in _vertexAnimationStutter.targets)
            {
                EditorHelper.SetKeyword(Properties.vertexAnimationStutter.GetValue(mat), Keywords.vertexAnimationStutter, mat);
                //No Keyword = Vertex Animation Stutter Off
            }
        }

        private void ManageKeywordsVertexAnimationMap()
        {
            //Vertex animation map
            foreach (Material mat in _vertexAnimationMap.targets)
            {
                EditorHelper.SetKeyword(Properties.vertexAnimationMap.GetValue(mat) != null, Keywords.vertexAnimationMap, mat);
                //No Keyword = Vertex Animation Map Off
            }
        }

        private void UpdateRenderPriority()
        {
            foreach (Material mat in _renderPriority.targets)
            {
                Properties.renderPriority.SetValue(mat, Properties.renderPriority.GetValue(mat));
            }
        }

        private void UpdateSystemProperties()
        {
            foreach (Material mat in _albedoMap.targets)
            {
                Properties.UpdateSystemProperties(mat);
            }
        }

        protected virtual void UpdateKeywords()
        {
            ManageKeywordsBlend();
            ManageKeywordsAlbedoMap();
            ManageKeywordsDissolve();
            ManageKeywordsAlphaClipping();
            ManageKeywordsSurface();
            UpdateRenderPriority();
            UpdateSystemProperties();
            ManageKeywordsColorGrading();
            ManageKeywordsVertexAnimation();
            ManageKeywordsVertexAnimationMap();
            _particles.UpdateKeywords();
            _outline.ManageKeywordsOutline();
            _outline.ManageKeywordsOutlineNoise();
            _outline.ManageKeywordsOutlineMap();
            _refraction.ManageKeywordsRefractionMap();
            _refraction.ManageKeywordsIndexOfRefraction();
        }
    }
}
#endif