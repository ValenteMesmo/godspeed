using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text.RegularExpressions;

namespace Godspeed
{
    public class StateFile
    {
        private readonly string path;
        private const string fileName = "savefile.save";

        public StateFile(bool android = false)
        {
            if (android)
                path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), fileName);
            else
                path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
        }

        public void Save(Color[] pixels)
        {
            File.WriteAllText(path, string.Join("", pixels.Select(f => $"{f.R},{f.G},{f.B},{f.A};")));
        }

        public Color[] Load()
        {
            var result = new List<Color>();
            if (File.Exists(path) == false)
                return result.ToArray();

            var text = File.ReadAllText(path);

            Regex rgx = new Regex(@"(?<r>\d{1,3}),(?<g>\d{1,3}),(?<b>\d{1,3}),(?<a>\d{1,3});");
            MatchCollection matches = rgx.Matches(text);
            foreach (Match item in matches)
            {
                var r = int.Parse(item.Groups["r"].Value);
                var g = int.Parse(item.Groups["g"].Value);
                var b = int.Parse(item.Groups["b"].Value);
                var a = int.Parse(item.Groups["a"].Value);
                result.Add(new Color(r, g, b, a));
            }

            return result.ToArray();

        }
    }
}
