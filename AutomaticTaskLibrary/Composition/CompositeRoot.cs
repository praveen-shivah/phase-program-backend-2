﻿namespace AutomaticTaskLibrary
{
    using ApplicationLifeCycle;

    public class CompositeRoot : CompositeRootBase
    {
        protected override bool registerBindings()
        {
            return true;
        }
    }
}
