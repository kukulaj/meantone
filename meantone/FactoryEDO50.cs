using System;

namespace meantone
{ 
public class FactoryEDO50 : FactoryEDO5
{
	
        private bool[,] dichotomy;
    public FactoryEDO50(Type_Map map, bool[] pinc) : base(50, map, pinc)
    {
        commas = new Comma[2];
        pumps = new Pump[2];
         
        commas[1] = new Comma(this, new int[] { 4, -1, 0 });
        pumps[1] = new Pump(commas[1],
            new int[] {37, 16, 45, 24, 3 });

        pumpStructure = new PumpStructureRandom(this);

            //scale = new bool[25];
            
        for (int i = 0; i < edo; i++)
        {
            scale[i] = false;
        }
            

        for (int i = 0; i < 19; i++)
        {
            scale[(29 * i) % scale.Length] = true;
        }

        dichotomy = new bool[2, edo];
        dichotomy[0, 0] = true;
        dichotomy[0, 29] = true;
        dichotomy[0, 21] = true;
        dichotomy[0, 16] = true;
        dichotomy[0, 34] = true;
        dichotomy[0, 37] = true;
        dichotomy[0, 13] = true;

        dichotomy[1, 0] = true;
        dichotomy[1, 29] = true;
        dichotomy[1, 21] = true;

    }

    public override double vertical_interval_cost(int dp, int loc)
    {
            return interval_cost(dp);
        if (dp < 0)
        {
            dp = -dp;
        }

        int phase = 0;
        if (loc % 12 == 11)
        {
            phase = 1;
        }
        if (dichotomy[phase, dp % edo])
            return 0.0;
        return 5000.0;
    }
        private bool inAScale(bool[] aScale, int pitch)
        {
            int period = aScale.Length;
            int rem = pitch % period;
            if (rem < 0)
            {
                rem = rem + period;
            }
            return aScale[rem];
        }

        public override bool inScale(int pitch, int loc)
        {

            bool result = true;


            result = inAScale(scale, pitch);

            return result;
        }
    }
}