using System;

namespace MyLittleIRAF
{
    public class SpectraCollection
    {

        private Spectrum[] specs;

       

        public SpectraCollection()
        {
            specs = new Spectrum[1];
        }

        public void Add(Spectrum spec)
        {
            Spectrum[] tmpSpecs = new Spectrum[specs.Length+1];

            for (int i = 0; i < specs.Length; i++)
            {
                tmpSpecs[i] = specs[i];
            }
            tmpSpecs[specs.Length] = spec;

            specs = tmpSpecs;
        }

        public void Del(int num)
        {
            Spectrum[] tmpSpecs = new Spectrum[specs.Length - 1];

            for (int i = 0; i < specs.Length-1; i++)
            {
                if (i >= num) tmpSpecs[i] = specs[i++];

                else tmpSpecs[i] = specs[i];
             
            }

            specs = tmpSpecs;
        }

        public Spectrum[] Spectra { get { return specs; } }
    }
}
