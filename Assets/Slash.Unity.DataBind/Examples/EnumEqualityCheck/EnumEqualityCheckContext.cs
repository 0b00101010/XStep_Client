// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumEqualityCheckContext.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Examples.EnumEqualityCheck
{
    using Slash.Unity.DataBind.Core.Data;

    public class EnumEqualityCheckContext : Context
    {
        #region Fields

        private readonly Property<TestEnum> enumProperty = new Property<TestEnum>();

        #endregion

        #region Constructors and Destructors

        public EnumEqualityCheckContext()
        {
            this.Enum = TestEnum.First;
        }

        #endregion

        #region Properties

        public TestEnum Enum
        {
            get
            {
                return this.enumProperty.Value;
            }
            set
            {
                this.enumProperty.Value = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        public void SetEnum(TestEnum newEnum)
        {
            this.Enum = newEnum;
        }

        #endregion
    }
}