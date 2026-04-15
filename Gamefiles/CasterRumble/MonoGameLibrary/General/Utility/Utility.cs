using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.General.Utility
{
    public static class Utility
    {
        public static Color FromHex(string hex)
        {
            hex = hex.TrimStart('#');
            return new Color(
                Convert.ToInt32(hex.Substring(0, 2), 16),
                Convert.ToInt32(hex.Substring(2, 2), 16),
                Convert.ToInt32(hex.Substring(4, 2), 16),
                hex.Length == 8 ? Convert.ToInt32(hex.Substring(6, 2), 16) : 255
            );
        }

        public static string KeepOnlyNumbers(string input)
        {
            return new string(input.Where(char.IsDigit).ToArray());
        }
    }
}
