﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MohjongWinTest
{
    class HandTiles
    {
        int[] tiletypes = new int[38];

        public HandTiles()
        {
            for (int i = 0; i < tiletypes.Length; i++)
                tiletypes[i] = 0;
        }

        public HandTiles(int[] hands)
        {
            for (int i = 0; i < hands.Length; i++)
                tiletypes[hands[i]]++;
        }
    
        public HandTiles(HandTiles t)
        {
            for (int i = 0; i < t.tiletypes.Length; i++)
                tiletypes[i] = t[i];
        }

        public int this[int index]
        {
            get
            {
                return tiletypes[index];
            }

            set
            {
                tiletypes[index] = value;
            }
        }

        public bool IsWinningHand()
        {
            HandTiles tmp_tiletypes = new HandTiles(this);

            if (IsZeroAllTiles())
                return false;

            // 七対子判定
            for(int i = 1;i < 38;i++)
            {
                if(tmp_tiletypes[i] >= 2)
                {
                    tmp_tiletypes[i] -= 2;
                    i--;
                }
            }
            if (tmp_tiletypes.IsZeroAllTiles())
                return true;

            // 国士無双判定
            tmp_tiletypes = new HandTiles(this);
            if(tmp_tiletypes.IsKokushi())
                return true;

            for (int i = 1; i < 38; i++)
            {
                // 初期化
                tmp_tiletypes = new HandTiles(this);

                // 頭雀を含むか
                if (tmp_tiletypes[i] >= 2)
                {
                    tmp_tiletypes[i] -= 2;

                    for (int j = 1; j < 38; j++)
                    {
                        // 刻子を含むか
                        if (tmp_tiletypes[j] >= 3)
                        {
                            tmp_tiletypes[j] -= 3;

                            j = 1;
                        }
                    }

                    for (int j = 1; j < 38; j++)
                    {
                        // 順子を含むか
                        if (tmp_tiletypes.ContainsChow(j))
                        {
                            tmp_tiletypes[j]--;
                            tmp_tiletypes[j + 1]--;
                            tmp_tiletypes[j + 2]--;

                            j = 1;
                        }
                    }

                    if (tmp_tiletypes.IsZeroAllTiles())
                        return true;
                }
            }

            return false;
        }


        bool ContainsChow(int index)
        {
            // 数牌の種類を超えての順子判定は、番号を種類ごとに一つ飛ばして割り振ってあるため起こらない
            return
                (index <= 27 && 
                (tiletypes[index] >= 1 && tiletypes[index + 1] >= 1 && tiletypes[index + 2] >= 1));
        }

        bool IsKokushi()
        {
            int[] yaotiles = { 1, 9, 11, 19, 21, 29, 31, 32, 33, 34, 35, 36, 37 };
            HandTiles tmp_tiletypes = new HandTiles(this);

            for(int i = 0;i < yaotiles.Length;i++)
            {
                if (tmp_tiletypes[yaotiles[i]] >= 1)
                    tmp_tiletypes[yaotiles[i]]--;
                else
                    return false;
            }

            for(int i = 0;i < yaotiles.Length;i++)
            {
                if (tmp_tiletypes[yaotiles[i]] >= 1)
                    return true;
            }

            return false;
        }

        bool IsZeroAllTiles()
        {
            for(int i = 0;i < tiletypes.Length;i++)
            {
                if (tiletypes[i] != 0)
                    return false;
            }

            return true;
        }

        int Length
        {
            get { return tiletypes.Length; }
        }
    }
}
