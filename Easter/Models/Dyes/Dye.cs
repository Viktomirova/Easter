﻿using System;
using System.Collections.Generic;
using System.Text;
using Easter.Models.Dyes.Contracts;

namespace Easter.Models.Dyes
{
    public class Dye : IDye
    {
        private int power;
        public Dye(int power)
        {
            this.Power = power;
        }

        public int Power
        {
            get
            {
                return this.power;
            }
            private set
            {
                if (value < 0)
                {
                    this.power = 0;
                }

                this.power = value;
            }
        }

        public bool IsFinished()
        {
            if (this.power > 0)
            {
                return false;
            }

            return true;
        }

        public void Use()
        {
            this.power -= 10;
            if (this.power < 0)
            {
                this.power = 0;
            }
        }
    }
}