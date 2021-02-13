// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestEnumGroupActiveSetter.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Examples.ActiveGroup
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Slash.Unity.DataBind.Examples.EnumEqualityCheck;
    using Slash.Unity.DataBind.Foundation.Setters;
    using UnityEngine;

    public class TestEnumGroupActiveSetter : EnumGroupActiveSetter<TestEnum>
    {
        public List<EnumGameObjectPair> EnumGameObjectMapping;

        /// <inheritdoc />
        protected override GameObject GetGameObject(TestEnum enumValue)
        {
            var enumGameObjectPair = this.EnumGameObjectMapping.FirstOrDefault(pair => pair.EnumValue == enumValue);
            return enumGameObjectPair != null ? enumGameObjectPair.GameObject : null;
        }

        /// <inheritdoc />
        protected override IEnumerable<GameObject> GetGameObjects()
        {
            return this.EnumGameObjectMapping.Select(pair => pair.GameObject).ToList();
        }

        [Serializable]
        public class EnumGameObjectPair
        {
            public TestEnum EnumValue;

            public GameObject GameObject;
        }
    }
}