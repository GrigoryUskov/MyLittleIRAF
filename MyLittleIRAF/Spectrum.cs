using System;
using System.IO;
using Interp;

namespace MyLittleIRAF
{
    public class Spectrum
    {
        private double[] xx;
        private double[] yy;
        private string name;

        public Spectrum(double[] xx, double[] yy, string name)
        {
            this.xx = xx;
            this.yy = yy;
            this.name = name;
        }
        public Spectrum(string path)
        {
            double[][] tmp = TableReader.LoadTable(path);

            xx = new double[tmp.Length];
            yy = new double[tmp.Length];
            //Console.WriteLine(tmp.Length);
            for (int i = 0; i < tmp.Length; i++)
            {
                xx[i] = tmp[i][0];
                yy[i] = tmp[i][1];
            }
            string[] mas = path.Split('/');
            this.name = mas[mas.Length-1];
        }

        public virtual Spectrum Trimmed(double lambda1, double lambda2)
        {
            int l1 = 0;
            int l2 = 0;
            for (int i = 0; i < xx.Length; i++)
            {
                if (xx[i] >= lambda1)
                {
                    l1 = i;
                    break;
                }
            }
            for (int i = l1; i < xx.Length; i++)
            {
                l2 = i;
                if (xx[i] >= lambda2)
                {
                    break;
                }
            }

            double[] tmpXx = new double[l2 - l1];
            double[] tmpYy = new double[l2 - l1];

            for (int i = 0; i < tmpXx.Length; i++)
            {
                tmpXx[i] = xx[l1 + i];
                tmpYy[i] = yy[l1 + i];
            }

            return new Spectrum(tmpXx, tmpYy, this.name + "_trimmed");

        }

        public static Spectrum operator + (Spectrum sp1, Spectrum sp2){

            double leftLambda = 0;
            double rightLambda = 0;

            if (sp1.xx[0] < sp2.xx[0])
            {
                leftLambda = sp2.xx[0];
            }
            else leftLambda = sp1.xx[0];

            if (sp1.xx[sp1.xx.Length - 1] < sp2.xx[sp2.xx.Length - 1])
            {
                rightLambda = sp1.xx[sp1.xx.Length - 1];
            }
            else rightLambda = sp2.xx[sp2.xx.Length - 1];


            sp1 = sp1.Trimmed(leftLambda, rightLambda);

            Interp.LinInterpolator li = new LinInterpolator(sp2.xx, sp2.yy);

            for(int i=0; i < sp1.xx.Length; i++)
            {
                sp1.yy[i] = li.Interp(sp1.xx[i]) + sp1.yy[i];
            }

            sp1.Name = "Sum.dat";
            return sp1;
        }
        public static Spectrum operator - (Spectrum sp1, Spectrum sp2)
        {
            double leftLambda = 0;
            double rightLambda = 0;

            if (sp1.xx[0] < sp2.xx[0])
            {
                leftLambda = sp2.xx[0];
            }
            else leftLambda = sp1.xx[0];

            if (sp1.xx[sp1.xx.Length - 1] < sp2.xx[sp2.xx.Length - 1])
            {
                rightLambda = sp1.xx[sp1.xx.Length - 1];
            }
            else rightLambda = sp2.xx[sp2.xx.Length - 1];


            sp1 = sp1.Trimmed(leftLambda, rightLambda);

            Interp.LinInterpolator li = new LinInterpolator(sp2.xx, sp2.yy);

            for (int i = 0; i < sp1.xx.Length; i++)
            {
                sp1.yy[i] = li.Interp(sp1.xx[i]) - sp1.yy[i];
            }
            sp1.Name = "Difference.dat";
            return sp1;

        }
        public static Spectrum operator * (Spectrum sp1, Spectrum sp2)
        {
            double leftLambda = 0;
            double rightLambda = 0;

            if (sp1.xx[0] < sp2.xx[0])
            {
                leftLambda = sp2.xx[0];
            }
            else leftLambda = sp1.xx[0];

            if (sp1.xx[sp1.xx.Length - 1] < sp2.xx[sp2.xx.Length - 1])
            {
                rightLambda = sp1.xx[sp1.xx.Length - 1];
            }
            else rightLambda = sp2.xx[sp2.xx.Length - 1];


            sp1 = sp1.Trimmed(leftLambda, rightLambda);

            Interp.LinInterpolator li = new LinInterpolator(sp2.xx, sp2.yy);

            for (int i = 0; i < sp1.xx.Length; i++)
            {
                sp1.yy[i] = li.Interp(sp1.xx[i]) * sp1.yy[i];
            }
            sp1.Name = "Multiply.dat";
            return sp1;

        }
        public static Spectrum operator / (Spectrum sp1, Spectrum sp2)
        {
            double leftLambda = 0;
            double rightLambda = 0;

            if (sp1.xx[0] < sp2.xx[0])
            {
                leftLambda = sp2.xx[0];
            }
            else leftLambda = sp1.xx[0];

            if (sp1.xx[sp1.xx.Length - 1] < sp2.xx[sp2.xx.Length - 1])
            {
                rightLambda = sp1.xx[sp1.xx.Length - 1];
            }
            else rightLambda = sp2.xx[sp2.xx.Length - 1];


            sp1 = sp1.Trimmed(leftLambda, rightLambda);

            Interp.LinInterpolator li = new LinInterpolator(sp2.xx, sp2.yy);

            for (int i = 0; i < sp1.xx.Length; i++)
            {
                sp1.yy[i] = li.Interp(sp1.xx[i]) / sp1.yy[i];
            }
            sp1.Name = "Division.dat";
            return sp1;

        }



        public double[] XX { get { return xx; } }
        public double[] YY { get { return yy; } }
        public string Name { get { return name; } set { name = value; } }


    }
}
