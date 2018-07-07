using System;
namespace Jeylabs_puzzle.Models
{
    public class Shape
    {
        public ShapeEnumeration Name { get; set; }
        public int Dimension_1  { get; set; }
        public int? Dimension_2 { get; set; }

        public Shape(ShapeEnumeration name, int dim_1, int? dim_2)
        {
            Name = name;
            Dimension_1 = dim_1;
            Dimension_2 = dim_2;
        }
    }
}
