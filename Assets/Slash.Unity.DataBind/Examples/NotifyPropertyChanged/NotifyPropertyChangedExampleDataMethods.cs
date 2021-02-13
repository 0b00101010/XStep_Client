// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotifyPropertyChangedExampleDataMethods.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Slash.Unity.DataBind.Examples.NotifyPropertyChangedExample
{
    public partial class NotifyPropertyChangedExampleData
    {
        public void ChangeTitle()
        {
            this.Title = "A brand new title!";
        }

        public void DecreaseNumber()
        {
            this.Integer -= 1;
        }

        public void IncreaseNumber()
        {
            this.Integer += 1;
        }
    }
}