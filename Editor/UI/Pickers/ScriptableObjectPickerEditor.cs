﻿using UnityEngine;

namespace Devdog.General.Editors
{
    [CustomObjectPicker(typeof(ScriptableObject), -10)]
    public class ScriptableObjectPickerEditor : ObjectPickerBaseEditor
    {
        public override bool IsSearchMatch(Object asset, string searchQuery)
        {
            searchQuery = searchQuery.ToLower();

            return GetObjectName(asset).Contains(searchQuery) ||
                   asset.GetType().Name.ToLower().Contains(searchQuery);
        }

        protected override string GetObjectName(Object asset)
        {
            return asset.name;
        }
    }
}