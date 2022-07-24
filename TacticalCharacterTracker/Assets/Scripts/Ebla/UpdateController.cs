using System;
using System.Collections.Generic;
using Ebla.Utils;
using MolkExtras;

namespace Ebla
{
    public class UpdateController : Singleton<UpdateController>
    {
        private readonly List<IUpdatable> updatables = new();

        private int updatableCount;

        private void Update()
        {
            for (int i = 0; i < updatableCount; i++)
            {
                updatables[i].ExecuteUpdate();
            }
        }

        public void Subscribe(IUpdatable updatable)
        {
            if (updatables.Contains(updatable))
            {
                return;
            }

            updatables.Add(updatable);
            updatableCount++;
        }

        public void Unsubscribe(IUpdatable updatable)
        {
            if (updatables.Remove(updatable))
            {
                updatableCount--;
            }
        }
    }
}
