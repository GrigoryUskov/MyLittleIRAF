using System;
using Interp;

namespace MyLittleIRAF
{
    public class SpectrumMod : Spectrum
    {
        private double[] cont;

        public SpectrumMod(double[] xx, double[] yy, double[] cont, string name):base(xx, yy, name)
        {
            this.cont = cont;
        }

        public SpectrumMod(string path):base(path)
        {
            double[][] tmp = TableReader.LoadTable(path);

            cont = new double[tmp.Length];

            if (tmp[0].Length == 3)
            {
                for (int i = 0; i < tmp.Length; i++)
                {
                    cont[i] = tmp[i][2];
                }
            }
            else
            {
                for (int i = 0; i < tmp.GetLength(0); i++)
                {
                    cont[i] = 0;
                }
            }


        }

        public SpectrumMod Shift(double rv)
        {
            double[] shiftedXX = new double[XX.Length];

            for (int i = 0; i < shiftedXX.Length; i++)
            {
                shiftedXX[i] = XX[i] * (1 + rv / 300000.0);

            }

            return new SpectrumMod(shiftedXX, YY, cont, Name + "_shifted");
        }

        public new SpectrumMod Trimmed(double lambda1, double lambda2)
        {


            int l1 = 0;
            int l2 = 0;
            for (int i = 0; i < XX.Length; i++)
            {
                if (XX[i] >= lambda1)
                {
                    l1 = i;
                    break;
                }
            }
            for (int i = l1; i < XX.Length; i++)
            {
                l2 = i;
                if (XX[i] >= lambda2)
                {
                    break;
                }
            }

            double[] tmpXx = new double[l2 - l1];
            double[] tmpYy = new double[l2 - l1];
            double[] tmpCont = new double[l2 - l1];

            for (int i = 0; i < tmpXx.Length; i++)
            {
                tmpXx[i] = XX[l1 + i];
                tmpYy[i] = YY[l1 + i];
                tmpCont[i] = cont[l1 + i];
            }

            return new SpectrumMod(tmpXx, tmpYy, cont, Name + "_trimmed");
        }

        public static SpectrumMod operator +(SpectrumMod op1, Spectrum op2)
        {

            double leftLambda = 0;
            double rightLambda = 0;

            if (op1.XX[0] < op2.XX[0])
            {
                leftLambda = op2.XX[0];
            }
            else leftLambda = op1.XX[0];

            if (op1.XX[op1.XX.Length - 1] < op2.XX[op2.XX.Length - 1])
            {
                rightLambda = op1.XX[op1.XX.Length - 1];
            }
            else rightLambda = op2.XX[op2.XX.Length - 1];


            op1 = op1.Trimmed(leftLambda, rightLambda);

            Interp.LinInterpolator li = new LinInterpolator(op2.XX, op2.YY);

            for (int i = 0; i < op1.XX.Length; i++)
            {
                op1.YY[i] = li.Interp(op1.XX[i]) + op1.YY[i];
            }

            return op1;
        }

        public Spectrum Spec{ get{ return new Spectrum(XX,YY,Name); } }
        public double[] Cont { get { return cont; } }

    }
}
