using C1.Win.C1FlexGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace HatFClient.CustomModels
{
    public class ColumnInfo
    {
        public int Index { get; set; }
        public string VarName { get; set; }
        public string Label { get; set; }
        public int Width { get; set; }
        public int TextAlignment { get; set; }
        public Type Type { get; set; }
    }

    public class PatternInfo
    {
        //private static Assembly asm = Assembly.LoadFrom("HatFApiModels.dll");
        private static readonly Assembly asm = typeof(HAT_F_api.Models.Announcement).Assembly;

        public const int DEFAULT_WIDTH = 100;
        public const int DEFAULT_ALIGNMENT = (int)TextAlignEnum.LeftCenter;

        public string UUID { get; set; } = Guid.NewGuid().ToString();
        public string Pattern { get; set; }
        public string ClassName { get; set; }
        public BindingList<ColumnInfo> Columns { get; set; } = new BindingList<ColumnInfo>();

        private Type ClassType { get; set; }

        public List<string> getUnusedVarNames()
        {
            var usedNames = Columns.Select(c => c.VarName).ToList();
            ClassType = Type.GetType(ClassName);
            if (ClassType == null)
            {
                ClassType = asm.GetType(ClassName);
            }
            PropertyInfo[] props = ClassType.GetProperties();
            return props.Where(p => !usedNames.Contains(p.Name)).Select(p => p.Name).ToList();
        }

        public void addColumnByName(string varName)
        {
            PropertyInfo prop = ClassType.GetProperty(varName);

            var nnType = Nullable.GetUnderlyingType(prop.PropertyType);
            var type = nnType != null ? nnType : prop.PropertyType;

            ColumnInfo columnInfo = new ColumnInfo
            {
                VarName = varName,
                Label = prop.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? varName,
                Width = DEFAULT_WIDTH,
                TextAlignment = DEFAULT_ALIGNMENT,
                Type = type,
            };
            Columns.Add(columnInfo);
        }

        public void removeColumnByName(string varName)
        {
            var target = Columns.FirstOrDefault(c => c.VarName == varName);
            Columns.Remove(target);
        }

        public void swapColumnPos(int m, int n)
        {
            if (m < 0 || n < 0 || m > Columns.Count - 1 || n > Columns.Count - 1) { return; }

            var temp = Columns[m];
            Columns[m] = Columns[n];
            Columns[n] = temp;
        }

        public void moveColumnPos(int from, int to)
        {
            if (from < 0 || from > Columns.Count - 1 || to < 0 || to > Columns.Count - 1) { return; }

            var temp = Columns[from];
            Columns.RemoveAt(from);
            Columns.Insert(to, temp);
        }

        public object GetValue(object instance, string varName)
        {
            return instance.GetType().GetProperty(varName).GetValue(instance, null);
        }

        public static PatternInfo copy(PatternInfo src)
        {
            var copy = new PatternInfo
            {
                Pattern = $"{src.Pattern}のコピー",
                ClassName = src.ClassName,
            };
            foreach (var item in src.Columns)
            {
                copy.Columns.Add(item);
            }
            return copy;
        }

        public static PatternInfo createFullPattern(string className, string pattern)
        {
            var patternInfo = new PatternInfo()
            {
                Pattern = pattern,
                ClassName = className,
            };
            // SET ALL AVAILABLE FIELDS
            var fields = patternInfo.getUnusedVarNames();
            foreach (var field in fields)
            {
                patternInfo.addColumnByName(field);
            }
            return patternInfo;
        }
    }
}
