using System;

namespace Jeylabs_puzzle.Models
{
    public class Command
    {
        public static Shape RawHandler(string raw)
        {
            raw = raw.Trim();
            raw = raw.ToLower();
            // Split the user input by key-words
            string[] keyArray = raw.Split( new string[]{"draw a ","draw an ", " with a", " with an", " and a "," and an "," of "}, StringSplitOptions.RemoveEmptyEntries); 
            // Validation for input
            if(keyArray.Length==3||keyArray.Length == 5)
            {
                /** Handle raw shape name and valid it
                keyArray: 0[raw name] 1[measurement_1] 2[raw value_1] 3[measurement_2] 4[raw value_2]
                **/
                string shapeName = keyArray[0];
                // Remove space
                shapeName = shapeName.Replace(" ", "");
                if(Enum.IsDefined(typeof(ShapeEnumeration), shapeName))
                {
                    // validation for raw value_1
                    int dim_1 = 0,temp;
                    if(Int32.TryParse(keyArray[2],out temp))
                    {
                        dim_1 = temp;
                    }else
                    {
                        return null;
                    }
                    int? dim_2 = null;
                    // 5 keys in the array
                    if (keyArray.Length == 5)
                    {
                        // Height should always be dim_1
                        if(keyArray[3].ToLower().Contains("height"))
                        {
                            int tmp = dim_1;
                            dim_1 = int.Parse(keyArray[4]);
                            dim_2 = tmp;
                        }else
                        {
                            dim_2 = int.Parse(keyArray[4]);
                        }
                    }
                    return new Shape((ShapeEnumeration)Enum.Parse(typeof(ShapeEnumeration), shapeName), dim_1, dim_2);
                }
            }
            return null;
        }
    }
}
