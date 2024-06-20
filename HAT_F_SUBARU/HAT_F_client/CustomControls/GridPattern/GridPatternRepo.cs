using HatFClient.Common;
using HatFClient.CustomModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HatFClient.Repository
{
    public class GridPatternRepo
    {
        private const string FILE_NAME = "{0}-pattern-{1}.json";
        private static readonly string SavePath = HatFAppDataPath.GetLocalDataSavePath("ViewPattern");

        private string UserId { get; set; }
        public string ModelName { get; set; }

        internal GridPatternRepo(string userId, string modelName)
        {
            UserId = userId;
            ModelName = modelName;
        }

        private void MakeSaveDirectory()
        {
            if (!Directory.Exists(SavePath))
            {
                Directory.CreateDirectory(SavePath);
            }
        }

        private string getJsonFilePath()
        {
            return Path.Combine(SavePath, string.Format(FILE_NAME, UserId, ModelName));
        }

        public List<PatternInfo> LoadPatterns()
        {
            var fName = getJsonFilePath();
            // ファイルが存在しないときは全フィールドをセットしたデフォルトパターンを一つ生成
            if (!File.Exists(fName))
            {
                var list = new List<PatternInfo>();
                PatternInfo patternInfo = PatternInfo.createFullPattern(ModelName, "デフォルト");
                list.Add(patternInfo);

                MakeSaveDirectory();
                File.WriteAllText(fName, JsonConvert.SerializeObject(list));

                return list;
            }
            return JsonConvert.DeserializeObject<List<PatternInfo>>(File.ReadAllText(fName));
        }

        public List<PatternInfo> SavePattern(PatternInfo patternInfo)
        {
            var list = LoadPatterns();
            list = list.Where(p => p.UUID != patternInfo.UUID).ToList();
            list.Insert(0, patternInfo);

            var fName = getJsonFilePath();
            MakeSaveDirectory();
            File.WriteAllText(fName, JsonConvert.SerializeObject(list));

            return list;
        }

        public List<PatternInfo> DeletePattern(PatternInfo patternInfo)
        {
            var list = LoadPatterns();
            list = list.Where(p => p.UUID != patternInfo.UUID).ToList();

            var fName = getJsonFilePath();
            File.WriteAllText(fName, JsonConvert.SerializeObject(list));

            return list;
        }

        public PatternInfo FindByName(string name)
        {
            var list = LoadPatterns();
            return list.FirstOrDefault(p => p.Pattern == name);
        }
    }

}
