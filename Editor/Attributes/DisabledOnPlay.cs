using System;
using UnityEngine;
using UnityEditor;

using CinderUtils.Attributes;


namespace CinderUtils.Editor {

    [CustomPropertyDrawer(typeof(DisabledOnPlayAttribute))]
    public class DisabledOnPlayDrawer : DisabledDrawer {
        public override bool IsEnabled {
            get => !Application.isPlaying && !EditorApplication.isPlayingOrWillChangePlaymode;
        }
    }

}

