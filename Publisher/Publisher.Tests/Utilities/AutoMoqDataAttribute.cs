﻿using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace Publisher.Tests.Utilities
{
    /// <summary>
    /// This is an helper class to support AutoMoq - 
    /// This should be moved to common framework (common assembly) so that it can be shared among various other applications
    /// </summary>
    public class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute()
            : this(new Fixture())
        {
        }

        public AutoMoqDataAttribute(IFixture fixture)
            : base(fixture.Customize(new AutoMoqCustomization()))
        {
        }
    }

    public class InlineAutoMoqDataAttribute : InlineAutoDataAttribute
    {
        public InlineAutoMoqDataAttribute(params object[] objects) : base(new AutoMoqDataAttribute(), objects) { }
    }
}
