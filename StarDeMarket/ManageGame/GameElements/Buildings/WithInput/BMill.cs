﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace StarDeMarket
{
    class BMill : BuildingWithInput
    {
        Storage storage;

        EItem[] input = { EItem.Getreide};
        int[] inputCount = { 2};

        EItem[] output = { EItem.Mehl };
        int[] outputCount = { 1 };
        public BMill()
        {
            storage = new Storage();
        }

        public override void Update()
        {
            Production();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public override void Workerwork()
        {
            throw new NotImplementedException();
        }

        public override void Production()
        {
            if (CheckRessourcen())
            {
                for (int i = 0; i < input.Length; ++i)
                {
                    storage.Get(input[i], inputCount[i]);
                }

                for (int i = 0; i < output.Length; ++i)
                {
                    storage.Add(output[i], outputCount[i]);
                }
            }
        }

        public override bool CheckRessourcen()
        {
            for (int i = 0; i < input.Length; ++i)
            {
                if (!storage.Check(input[i], inputCount[i]))
                    return false;
            }
            return true;
        }
    }
}