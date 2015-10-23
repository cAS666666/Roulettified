using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Roulettified.Utilities
{
    public class ArrayUtilities
    {
        public String arrayListToString(ArrayList arr, String delimiter)
        {
            StringBuilder builder = new StringBuilder();
            int arrLength = arr.Count;

            builder.Append(arr[0]); // Add first since the order of the comma is significant 
            for (int i = 1; i < arrLength ; i++)
            {
                builder.Append(delimiter);
                builder.Append(arr[i]);
            }

            return builder.ToString();
        }
    }
}