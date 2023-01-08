using Microsoft.EntityFrameworkCore;
using Flashcard.Models;
using System.Text.Json;

namespace Flashcard.Chart
{
    /// <summary>
    /// Chart.js の Chart() 関数に渡す JSONデータを表すクラス
    /// </summary>
    public class ChartJson
    {
        public string type { get; set; }
        public Datas data { get; set; }
        public Options options { get; set; }

        public ChartJson RoundData()
        {
            if (data != null) data.RoundData();
            return this;
        }
    }

    public class Datas
    {
        public string[] labels { get; set; }
        public Dataset[] datasets { get; set; }

        public void RoundData()
        {
            if (datasets != null)
            {
                foreach (var dataset in datasets)
                {
                    dataset.RoundData();
                }
            }
        }
    }

    public class Dataset
    {
        public string label { get; set; }
        /// <summary> double に解釈されるべきデータ </summary>
        public double?[] data { get; set; }
        public string backgroundColor { get; set; }
        public string borderColor { get; set; }
        public int order { get; set; } = 1;
        public string type { get; set; } = "bar";
        public bool fill { get; set; } = false;
        public int radius { get; set; } = 3;
        public double[] borderDash { get; set; }
        public string yAxisID { get; set; } = "y-1";

        public static Dataset CreateBar(string label, double?[] data, string color)
        {
            return new Dataset { label = label, data = data, borderColor = color, backgroundColor = color, order = 3 };
        }

        public static Dataset CreateLine(string label, double?[] data, string bdColor, string bgColor, string yAxis = "y-1")
        {
            return new Dataset { label = label, data = data, borderColor = bdColor, backgroundColor = bgColor, order = 2, type = "line", yAxisID = yAxis };
        }

        public static Dataset CreateLine2(string label, double?[] data, string bdColor, string bgColor)
        {
            return CreateLine(label, data, bdColor, bgColor, "y-2");
        }

        private static double[] _dashParam = new double[] { 10, 3 };

        public static Dataset CreateDashLine(string label, double?[] data, string color)
        {
            return new Dataset { label = label, data = data, borderColor = color, backgroundColor = color, order = 2, type = "line", radius = 0, borderDash = _dashParam };
        }

        public static Dataset CreateDataset(string type, string label, double?[] data, string color, string bgColor, int order)
        {
            return new Dataset { type = type, label = label, data = data, borderColor = color, backgroundColor = bgColor, order = order };
        }

        public void RoundData()
        {
            if (data != null)
            {
                data = data.Select(x => x._round(3)).ToArray();
            }
        }
    }

    public class Options
    {
        public int AnimationDuration
        {
            get { return animation.duration; }
            set { animation.duration = value; }
        }

        public bool maintainAspectRatio { get; set; } = false;

        public Animation animation { get; set; } = new Animation();

        public Scales scales { get; set; }

        public static Options Plain()
        {
            return new Options
            {
                scales = new Scales
                {
                    yAxes = new[] {
                        new Yaxis{
                            ticks = new Ticks()
                        }
                    }
                }
            };
        }

        public static Options CreateTwoAxes(Ticks leftTicks = null, Ticks rightTicks = null)
        {
            return new Options
            {
                scales = new Scales
                {
                    yAxes = new[] {
                            new Yaxis{ id = "y-1", position = "left", ticks = leftTicks ?? new Ticks() },
                            new Yaxis{ id = "y-2", position = "right", ticks = rightTicks ?? new Ticks() },
                    }
                }
            };
        }
    }

    public class Animation
    {
        public int duration { get; set; } = 1000;
    }

    public class Scales
    {
        public Yaxis[] yAxes { get; set; }
    }

    public class Yaxis
    {
        public string id { get; set; } = "y-1";
        public string position { get; set; } = "left";
        public Ticks ticks { get; set; }
    }

    public class Ticks
    {
        public bool beginAtZero { get; set; } = true;
        public double? min { get; set; }
        public double? max { get; set; }
        public double? stepSize { get; set; }

        public Ticks(double? maxVal = null, double? stepVal = null, double? minVal = null)
        {
            if (maxVal.HasValue)
            {
                max = maxVal.Value;
                min = minVal.HasValue ? minVal.Value : 0;
                stepSize = stepVal.HasValue ? stepVal.Value : maxVal.Value / 10;
            }
        }
    }

    /// <summary>
    /// ヘルパー拡張メソッド
    /// </summary>
    public static class Extensions
    {
        public static double? _round(this double? d, int digits = 0)
        {
            return d.HasValue ? Math.Round(d.Value, digits) : d;
        }

        private static JsonSerializerOptions SerializerOpt = new JsonSerializerOptions { IgnoreNullValues = true };

        public static string _toString(this ChartJson json)
        {
            return json.RoundData()._jsonSerialize();
        }

        public static string _jsonSerialize<T>(this T json)
        {
            return json == null ? "" : JsonSerializer.Serialize(json, SerializerOpt);
        }

        public static T _jsonDeserialize<T>(this string jsonStr)
        {
            return JsonSerializer.Deserialize<T>(jsonStr);
        }
    }
}
