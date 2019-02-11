using AppKit;
using System.IO;
using System;
using System.Collections.Generic;

namespace MyLittleIRAF
{
    //@/Users/uskov/Desktop/MyNewTextFile.txt
    static class MainClass
    {
        public static List<Spectrum> listOfSpectra = new List<Spectrum>();
        static void Main(string[] args)
        {
            NSApplication.Init();
            NSApplication.Main(args);
            
            //string path = @"/Users/uskov/Downloads/lm0003_05700_0450_0020_78_on.dat";
            //string path2 = @"/Users/uskov/Downloads/ERVul.dat";
            //List<Spectrum> lspec = new List<Spectrum>();
            //Console.WriteLine(lspec.Count);
            //SpectrumMod spectrumMod = new SpectrumMod(path);

            //lspec.Add(spectrumMod.Spec);

            //Console.WriteLine(lspec[lspec.Count].Name);
            //lspec.Add(new Spectrum(path));
            //lspec.Add(new Spectrum(path2));

            //int k = 1;
            //double[][] tmpArray = new double[lspec[k].XX.Length][];

            

            //Console.WriteLine(Convert.ToString(tmpArray.GetLength(0)));



            //for (int i = 0; i < tmpArray.GetLength(0); i++)
            //{
            //    tmpArray[i] = new double[2];
            //    tmpArray[i][0] = lspec[k].XX[i];
            //    tmpArray[i][1] = lspec[k].YY[i];
            //}



        }
    }
}
