using System;
using System.Drawing;

namespace Jeylabs_puzzle.Models
{
    public class Drawing
    {
        public Graphics Graph { get; set; }
        public Bitmap BM { get; set; }
        public Pen BlackPen { get; set; }
        public Point Central { get; set; }
        public Drawing()
        {
            BM = new Bitmap("../Jeylabs_puzzle/wwwroot/images/bg.png");
            BlackPen = new Pen(Color.Black, 2);
            Central = new Point(400, 400);
        }


        public bool Draw(Shape shape)
        {
            if(shape!=null)
            {
                
                using (Graph = Graphics.FromImage(BM))
                {
                    switch (shape.Name)
                    {
                        case ShapeEnumeration.square:
                            if(shape.Dimension_2!=null)
                            {
                                return false;
                            }
                            Rectangle square = new Rectangle(Convert.ToInt32(Central.X-shape.Dimension_1/2), Convert.ToInt32(Central.Y - shape.Dimension_1 / 2), shape.Dimension_1, shape.Dimension_1);
                            Graph.DrawRectangle(BlackPen, square);
                            break;
                        case ShapeEnumeration.rectangle:
                            if (shape.Dimension_2 == null)
                            {
                                shape.Dimension_2 = shape.Dimension_1;
                            }
                            Rectangle rectangle = new Rectangle(Convert.ToInt32(Central.X - shape.Dimension_2 / 2), Convert.ToInt32(Central.Y - shape.Dimension_1 / 2), shape.Dimension_2 ?? default(int),shape.Dimension_1);
                            Graph.DrawRectangle(BlackPen, rectangle);
                            break;
                        case ShapeEnumeration.scalenetriangle:
                            if (shape.Dimension_2 == null)
                            {
                                shape.Dimension_2 = shape.Dimension_1;
                            }
                            Graph.DrawPolygon(BlackPen, PointsForScaleneTriangle(shape));
                            break;
                        case ShapeEnumeration.isoscelestriangle:
                            if (shape.Dimension_2 == null)
                            {
                                shape.Dimension_2 = shape.Dimension_1;
                            }
                            Graph.DrawPolygon(BlackPen, PointsForIsoscelesTriangle(shape));
                            break;
                        case ShapeEnumeration.equilateraltriangle:
                            if (shape.Dimension_2 != null)
                            {
                                return false;
                            }
                            Graph.DrawPolygon(BlackPen, PointsForPolygon(3, Convert.ToInt32(GenarateRadius(3,shape.Dimension_1))/2));
                            break;
                        case ShapeEnumeration.parallelogram:
                            if (shape.Dimension_2 == null)
                            {
                                shape.Dimension_2 = shape.Dimension_1;
                            }
                            Graph.DrawPolygon(BlackPen,PointsForParallelogram(shape));
                            break;
                        case ShapeEnumeration.pentagon:
                            if (shape.Dimension_2 != null)
                            {
                                return false;
                            }
                            Graph.DrawPolygon(BlackPen, PointsForPolygon(5, Convert.ToInt32(GenarateRadius(5, shape.Dimension_1)) / 2));
                            break;
                        case ShapeEnumeration.hexagon:
                            if (shape.Dimension_2 != null)
                            {
                                return false;
                            }
                            Graph.DrawPolygon(BlackPen, PointsForPolygon(6, Convert.ToInt32(GenarateRadius(6, shape.Dimension_1)) / 2));
                            break;
                        case ShapeEnumeration.heptagon:
                            if (shape.Dimension_2 != null)
                            {
                                return false;
                            }
                            Graph.DrawPolygon(BlackPen, PointsForPolygon(7, Convert.ToInt32(GenarateRadius(7, shape.Dimension_1)) / 2));
                            break;
                        case ShapeEnumeration.octagon:
                            if (shape.Dimension_2 != null)
                            {
                                return false;
                            }
                            Graph.DrawPolygon(BlackPen, PointsForPolygon(8, Convert.ToInt32(GenarateRadius(8, shape.Dimension_1)) / 2));
                            break;
                        case ShapeEnumeration.circle:
                            if (shape.Dimension_2 != null)
                            {
                                return false;
                            }
                            Graph.DrawEllipse(BlackPen,Central.X-shape.Dimension_1,Central.Y-shape.Dimension_1,shape.Dimension_1*2,shape.Dimension_1*2);
                            break;
                        case ShapeEnumeration.oval:
                            if (shape.Dimension_2 == null)
                            {
                                shape.Dimension_2 = shape.Dimension_1;
                            }
                            Graph.DrawEllipse(BlackPen, Convert.ToInt32(Central.X - 0.5 * shape.Dimension_2), Convert.ToInt32(Central.Y - 0.5*shape.Dimension_1), shape.Dimension_2 ?? default(int), shape.Dimension_1 );
                            break;
                    }
                }
                BM.Save("../Jeylabs_puzzle/wwwroot/images/shape.JPG");
                return true;
            }
            return false;
        }

        // Get N points for a N-polygon
        private Point[] PointsForPolygon(int num, int radius)
        {
            Point[] points = new Point[num];
            for (int i = 0; i < num; i++)
            {
                points[i] = new Point(Convert.ToInt32(Central.X+radius*Math.Cos(2*Math.PI*i/num)),Convert.ToInt32(Central.Y+radius*Math.Sin(2*Math.PI*i/num)));
            }
            return points;
        }

        // Get 3 points for scalene triangle
        private Point[] PointsForScaleneTriangle(Shape shape)
        {
            // random x-dim for top point
            Random random = new Random();
            int x_dim = 0;
            while (x_dim == 0)
            {
                x_dim = 400 - random.Next(800);
            }
            // Generate 3 points 
            Point[] points = new Point[3];
            points[0] = new Point(Convert.ToInt32(Central.X + x_dim), Convert.ToInt32(Central.Y - 2 * shape.Dimension_1 / 3));
            points[1] = new Point(Convert.ToInt32(Central.X - shape.Dimension_2 / 2), Convert.ToInt32(Central.Y + shape.Dimension_1 / 3));
            points[2] = new Point(Convert.ToInt32(Central.X + shape.Dimension_2 / 2), Convert.ToInt32(Central.Y + shape.Dimension_1 / 3));
            return points;
        }

        // Get 3 points for isosceles triangle
        private Point[] PointsForIsoscelesTriangle(Shape shape)
        {
            // Generate 3 points 
            Point[] points = new Point[3];
            points[0] = new Point(Convert.ToInt32(Central.X), Convert.ToInt32(Central.Y - 2 * shape.Dimension_1 / 3));
            points[1] = new Point(Convert.ToInt32(Central.X - shape.Dimension_2 / 2), Convert.ToInt32(Central.Y + shape.Dimension_1 / 3));
            points[2] = new Point(Convert.ToInt32(Central.X + shape.Dimension_2 / 2), Convert.ToInt32(Central.Y + shape.Dimension_1 / 3));
            return points;
        }


        // Get 4 points for Parallelogram
        private Point[] PointsForParallelogram(Shape shape)
        {
            //2 Random x_dim 
            Random random = new Random();
            int x1_dim = 0,x2_dim = 0;
            while (x1_dim==x2_dim)
            {
                x1_dim = 200 - random.Next(400);
                x2_dim = 200 - random.Next(400);
            }
            Point[] points = new Point[4];
            points[0] = new Point(Convert.ToInt32(x1_dim+Central.X), Convert.ToInt32(Central.Y-shape.Dimension_1/2));
            points[1] = new Point(Convert.ToInt32(x1_dim+Central.X+shape.Dimension_2), Convert.ToInt32(Central.Y-shape.Dimension_1/2));
            points[2] = new Point(Convert.ToInt32(x2_dim + Central.X + shape.Dimension_2), Convert.ToInt32(Central.Y + shape.Dimension_1 / 2));
            points[3] = new Point(Convert.ToInt32(x2_dim+Central.X), Convert.ToInt32(Central.Y+shape.Dimension_1/2));
            return points;
        }

        // Get radius for a N-polygon
        private double  GenarateRadius(int num, int edge)
        {
            double radius = edge/Math.Sin(Math.PI/num);
            return radius;
        }

    }
}
