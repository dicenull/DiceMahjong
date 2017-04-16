﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DxLibDLL;

namespace DiceMohjong.Phases
{
    abstract class Phase
    {
        public Phase Update()
        {
            key.Update();
            Phase res = update();

            DX.ScreenFlip();

            return res;
        }

        protected abstract Phase update();

        protected static Key key = new Key();
    }
}
