using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Helper
{
	
	class Line
	{
		private float a; 
		private float b;

        public Line()
        {
			this.a = 0;
			this.b = 0;
        }
		public Line(float a, float b)
		{
			this.a = a;
			this.b = b;
		}

		public float f(float x)
		{
			return a * x + b;
		}
	};

	class Point
	{
		private float x; 
		private float y;
		public Point(float x, float y)
        {
			this.x = x;
			this.y = y;
        }

		public float getX() { return this.x; }
		public float getY() { return this.y; }
	};

	class Calculation
    {

		public static float avg(float[] x, int size)
        {
			float sum = 0;
			for (int i = 0; i < size; sum += x[i], i++) ;
			return sum / size;
		}

		// returns the variance of X and Y
		public static float var(float[] x, int size)
        {
			float av = avg(x, size);
			float sum = 0;
			for (int i = 0; i < size; i++)
			{
				sum += x[i] * x[i];
			}
			return sum / size - av * av;
		}

		// returns the covariance of X and Y
		public static float cov(float[] x, float[] y, int size)
        {
			float sum = 0;
			for (int i = 0; i < size; i++)
			{
				sum += x[i] * y[i];
			}
			sum /= size;

			return sum - avg(x, size) * avg(y, size);
		}


		// returns the Pearson correlation coefficient of X and Y
		public static float pearson(float[] x, float[] y, int size)
        {
			return cov(x, y, size) / ((float)Math.Sqrt(var(x, size)) * (float)Math.Sqrt(var(y, size)));
		}

		// performs a linear regression and returns the line equation
		public static Line linear_reg(Point[] points, int size)
        {
			float[] x = new float[size];
			float[] y = new float[size];
			for (int i = 0; i < size; i++)
			{
				x[i] = points[i].getX();
				y[i] = points[i].getY();
			}
			float a = cov(x, y, size) / var(x, size);
			float b = avg(y, size) - a * (avg(x, size));

			
			return new Line(a, b);
		}

		// returns the deviation between point p and the line equation of the points
		public static float dev(Point p, Point[] points, int size)
        {
			Line l = linear_reg(points, size);
			return dev(p, l);
		}

		// returns the deviation between point p and the line
		public static float dev(Point p, Line l)
        {
			return Math.Abs(p.getY() - l.f(p.getX())) ;
		}

		public static float findThreshold(Point[] ps, int len, Line rl)
		{
			float max = 0;
			for (int i = 0; i < len; i++)
			{
				float d = Math.Abs(ps[i].getY() - rl.f(ps[i].getX()));
				if (d > max)
					max = d;
			}
			return max;
		}
	}
}
