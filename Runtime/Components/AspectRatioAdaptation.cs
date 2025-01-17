﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace E7.NotchSolution
{
    /// <summary>
    /// Like <see cref="SafeAreaAdaptation"> but use the current screen aspect ratio number instead.
    /// The ratio is width/height ratio when on **landscape** orientation (e.g. 4/3, 16/9, 2/1) regardless of your game's orientation.
    /// 
    /// By default, the curve is setup so that lower aspect ratio (wider screen) is "normal" and higher number (narrower screen) is "adapted".
    /// So the leftmost node represents a device like an iPad.
    /// </summary>
    [HelpURL("https://github.com/5argon/NotchSolution/blob/master/.Documentation/Components/AdaptationComponents.md#-aspectratioadaptation")]
    public class AspectRatioAdaptation : AdaptationBase
    {
        public override void Adapt() => base.Adapt(valueForAdaptationCurve: AspectRatio);

        void Reset()
        {
            portraitOrDefaultAdaptation = new BlendedClipsAdaptor(GenDefaultCurve());
            landscapeAdaptation = new BlendedClipsAdaptor(GenDefaultCurve());

            AnimationCurve GenDefaultCurve() =>
             new AnimationCurve(new Keyframe[]
            {
                new Keyframe( 4f/3f, 0,1.2f,1.2f), // iPad
                new Keyframe( 16f/9f, Mathf.InverseLerp(4/3f, 19.5f/9f, 16/9f), 1.2f, 1.2f), // A 16:9 breakpoint 
                new Keyframe( 19.5f/9f, 1, 1.2f,1.2f), // I think LG G7 is the longest phone in the world now?
            });
        }

        public float AspectRatio
        {
            get
            {
                bool landscape = Screen.width > Screen.height;
                return landscape ? Screen.width / (float)Screen.height : Screen.height / (float)Screen.width;
            }
        }

    }
}