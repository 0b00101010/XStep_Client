// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MasterPathExampleContext.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Examples.MasterPath
{
    using Slash.Unity.DataBind.Core.Data;

    public class MasterPathExampleContext : Context
    {
        private readonly Property<MasterPathExampleChildContext> childAProperty =
            new Property<MasterPathExampleChildContext>(new MasterPathExampleChildContext
            {
                Header = "Prefab Reuse",
                Description =
                    "By having relative paths, you don't have to adjust the paths of the data bindings when using prefabs, but only the master path on the root game object"
            });

        private readonly Property<MasterPathExampleChildContext> childBProperty =
            new Property<MasterPathExampleChildContext>(new MasterPathExampleChildContext
            {
                Header = "Convenience",
                Description = "You just have to specify the relative path from the main path for the data bindings"
            });

        private readonly Property<string> textProperty =
            new Property<string>("Usage of MasterPath");

        public MasterPathExampleChildContext ChildA
        {
            get
            {
                return this.childAProperty.Value;
            }
            set
            {
                this.childAProperty.Value = value;
            }
        }

        public MasterPathExampleChildContext ChildB
        {
            get
            {
                return this.childBProperty.Value;
            }
            set
            {
                this.childBProperty.Value = value;
            }
        }

        public string Text
        {
            get
            {
                return this.textProperty.Value;
            }
            set
            {
                this.textProperty.Value = value;
            }
        }
    }

    public class MasterPathExampleChildContext : Context
    {
        private readonly Property<string> descriptionProperty =
            new Property<string>();

        private readonly Property<string> headerProperty =
            new Property<string>();

        public string Description
        {
            get
            {
                return this.descriptionProperty.Value;
            }
            set
            {
                this.descriptionProperty.Value = value;
            }
        }

        public string Header
        {
            get
            {
                return this.headerProperty.Value;
            }
            set
            {
                this.headerProperty.Value = value;
            }
        }
    }
}