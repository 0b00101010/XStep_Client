﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DropDownUnity.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Examples.EnumEqualityCheck
{
    using UnityEngine;

    public class DropDownUnity : MonoBehaviour
    {
        #region Fields

        public bool IsExpanded;

        public RectTransform List;

        #endregion

        #region Public Methods and Operators

        public void SetExpand(bool isExpanded)
        {
            this.IsExpanded = isExpanded;
            this.UpdateList();
        }

        public void ToggleExpand()
        {
            this.IsExpanded = !this.IsExpanded;
            this.UpdateList();
        }

        #endregion

        #region Methods

        protected void Start()
        {
            this.UpdateList();
        }

        private void UpdateList()
        {
            if (this.List != null)
            {
                this.List.gameObject.SetActive(this.IsExpanded);
            }
        }

        #endregion
    }
}