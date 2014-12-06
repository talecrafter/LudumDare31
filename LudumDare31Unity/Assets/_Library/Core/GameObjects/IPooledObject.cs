﻿using System;

namespace CraftingLegends.Core
{
    public interface IPooledObject
    {
        void ToggleOn();
        void ToggleOff();
        event Action<IPooledObject> isDisabled;
        bool isInactiveInObjectPool { get; set; }
    }
}