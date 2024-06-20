using HAT_F_api.CustomModels;
using HatFClient.Common;
using HatFClient.Constants;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace HatFClient.Repository
{
    internal class BlobStrageRepo
    {
        private const string FILE_REPLACE_PATTERN = "^.*\\/";

        private static BlobStrageRepo _instance = null;

        private string _containerId = null;
        private BindingList<BlobFileInfo> _blobFileInfos = new BindingList<BlobFileInfo>();

        public static BlobStrageRepo GetInstance()
        {
            if (_instance == null)
            {
                _instance = new BlobStrageRepo();
            }
            return _instance;
        }

        public BindingList<BlobFileInfo> BlobFileInfos { get { return _blobFileInfos; } }

        public async Task Init(string id)
        {
            _containerId = id;
            _blobFileInfos.Clear();

            var blobFiles = await GetFileList(id);
            blobFiles.ForEach(blobFile => {
                blobFile.Name = Regex.Replace(blobFile.Name, FILE_REPLACE_PATTERN, "");
                _blobFileInfos.Add(blobFile);
            });
        }

        public BlobFileInfo Find(string fName)
        {
            return _blobFileInfos.FirstOrDefault(b => b.Name == fName);
        }

        public async Task Upload(string filePath)
        {
            try
            {
                await UploadFile(filePath, _containerId);
                await SetMetaData(filePath, _containerId);
                FileInfo fileInfo = new FileInfo(filePath);
                BlobFileInfo blobFileInfo = Find(fileInfo.Name);
                if (blobFileInfo != null)
                {
                    _blobFileInfos.Remove(blobFileInfo);
                }

                _blobFileInfos.Add(FromFilePath(filePath));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public async Task Download(string saveDir, BlobFileInfo blobFileInfo)
        {
            try
            {
                var savePath = Path.Combine(saveDir, blobFileInfo.Name);
                await Download(savePath, _containerId, blobFileInfo.Name);
                // RESTORE META DATA (I SHOULD GET THIS FROM API)
                var createdOn = DateTime.Parse(blobFileInfo.CreatedOn);
                var lastModified = DateTime.Parse(blobFileInfo.LastModified);

                File.SetCreationTime(savePath, createdOn);
                File.SetLastWriteTime(savePath, lastModified);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public async Task Delete(BlobFileInfo blobFileInfo)
        {
            try
            {
                await DeleteFile(_containerId, blobFileInfo.Name);

                _blobFileInfos.Remove(blobFileInfo);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public static BlobFileInfo FromFilePath(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            return new BlobFileInfo
            {
                Checked = false,
                Name = fileInfo.Name,
                ContentLength = fileInfo.Length,
                CreatedOn = fileInfo.CreationTime.ToString(),
                LastModified = fileInfo.LastWriteTime.ToString(),
            };
        }

        /// <summary>
        /// ファイルのメタデータをセット
        /// AzureBlobではファイル本来の更新日、作成日が失われるため
        /// メタデータにセットする
        /// 作成日: CreatedOn
        /// 更新日: LastModified
        /// ※Azure Blobの作成日、更新日に名称を合わせて設定する
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task SetMetaData(string filePath, string id)
        {
            BlobFileInfo blobFileInfo = FromFilePath(filePath);
            var metaData = new Dictionary<string, string>();
            metaData["CreatedOn"] = blobFileInfo.CreatedOn;
            metaData["LastModified"] = blobFileInfo.LastModified;

            var json = JsonConvert.SerializeObject(metaData);
            //var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            string url = ApiResources.HatF.BlobStrage + $"/{id}/" + blobFileInfo.Name;
            await Program.HatFApiClient.PutAsync(url, json);
        }


        public static async Task UploadFile(string filePath, string id)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            string url = ApiResources.HatF.BlobStrage + $"/{id}/" + fileInfo.Name;
            await Program.HatFApiClient.PostStreamAsync(url, filePath);
        }

        public static async Task Download(string savePath, string id, string fileName)
        {
            string blobApi = ApiResources.HatF.BlobStrage + $"/{id}/{fileName}";

            var fileStream = await Program.HatFApiClient.GetStreamAsync(blobApi);
            using (var file = new FileStream(savePath, FileMode.Create, FileAccess.Write))
            {
                await fileStream.CopyToAsync(file);
            }
        }

        public static async Task DeleteFile(string id, string fileName)
        {
            string blobApi = ApiResources.HatF.BlobStrage + $"/{id}/{fileName}";
            await Program.HatFApiClient.DeleteAsync(blobApi);
        }

        public static async Task<List<BlobFileInfo>> GetFileList(string id)
        {
            string url = ApiResources.HatF.BlobStrage + $"/{id}";
            var response = await Program.HatFApiClient.GetAsync<List<BlobFileInfo>>(url);
            if (ApiHelper.IsPositiveResponse(response))
            {
                return response.Data;
            }
            return null;
        }
    }
}
