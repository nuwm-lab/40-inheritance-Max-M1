using System;
using System.Globalization;
namespace LabWork
{

    class EquilateralTriangle
    {
        public double SideLength { get; private set; }
        public double Angle { get; private set; }

        public EquilateralTriangle(double sideLength)
        {
            SetSideLength(sideLength);
        }

        public void SetSideLength(double sideLength)
        {
            if (sideLength <= 0)
            {
                throw new ArgumentException("Invalid side or angle values.");
            }
            SideLength = sideLength;
        }

        public void SetSideLength(int sideLength)
        {
            SetSideLength((double)sideLength);
        }

        public void SetAngle(double angle)
        {
            if (angle != 60)
            {
                throw new ArgumentException("Invalid side or angle values.");
            }
            Angle = angle;
        }

        public void SetAngle(string angle)
        {
            if (!double.TryParse(angle, NumberStyles.Float, CultureInfo.InvariantCulture, out double parsedAngle))
            {
                throw new ArgumentException("Invalid angle format.");
            }
            SetAngle(parsedAngle);
        }

        public double GetPerimeter()
        {
            return 3 * SideLength;
        }

        public override string ToString()
        {
            return $"Equilateral Triangle: Side Length = {SideLength}, Angle = {Angle}, Perimeter = {GetPerimeter()}";
        }
    }

    class Triangle : EquilateralTriangle
    {
        public double AngleB { get; private set; }
        public double AngleC { get; private set; }

        public Triangle(double sideA, double angleB, double angleC) : base(sideA)
        {
            SetTriangle(sideA, angleB, angleC);
        }

        public void SetTriangle(double sideA, double angleB, double angleC)
        {
            if (sideA <= 0 || angleB <= 0 || angleC <= 0 || angleB + angleC >= 180)
            {
                throw new ArgumentException("Invalid side or angle values.");
            }
            SetSideLength(sideA);
            AngleB = angleB;
            AngleC = angleC;
        }

        public double GetSideB()
        {
            return SideLength * Math.Sin(DegreesToRadians(AngleB)) / Math.Sin(DegreesToRadians(180 - AngleB - AngleC));
        }

        public double GetSideC()
        {
            return SideLength * Math.Sin(DegreesToRadians(AngleC)) / Math.Sin(DegreesToRadians(180 - AngleB - AngleC));
        }

        public double GetThirdAngle()
        {
            return 180 - AngleB - AngleC;
        }

        public double GetPerimeter()
        {
            return SideLength + GetSideB() + GetSideC();
        }

        private double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }

        public override string ToString()
        {
            return $"Triangle: Side A = {SideLength}, Side B = {GetSideB():F2}, Side C = {GetSideC():F2}, Perimeter = {GetPerimeter():F2}, Angle B = {AngleB}, Angle C = {AngleC}, Angle A = {GetThirdAngle():F2}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var culture = CultureInfo.InvariantCulture;

                Console.WriteLine("Enter side length for an equilateral triangle:");
                double eqSideLength = double.Parse(Console.ReadLine().Replace(',', '.'), culture);

                Console.WriteLine("Enter angle for the equilateral triangle:");
                string eqAngle = Console.ReadLine();

                EquilateralTriangle eqTriangle = new EquilateralTriangle(eqSideLength);
                eqTriangle.SetAngle(eqAngle);
                Console.WriteLine(eqTriangle);

                Console.WriteLine("\nEnter side length for a triangle:");
                double sideA = double.Parse(Console.ReadLine().Replace(',', '.'), culture);

                Console.WriteLine("Enter angle B (in degrees):");
                double angleB = double.Parse(Console.ReadLine().Replace(',', '.'), culture);

                Console.WriteLine("Enter angle C (in degrees):");
                double angleC = double.Parse(Console.ReadLine().Replace(',', '.'), culture);

                Triangle triangle = new Triangle(sideA, angleB, angleC);
                Console.WriteLine(triangle);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

}
