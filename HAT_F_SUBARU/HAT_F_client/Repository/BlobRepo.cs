using HAT_F_api.CustomModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Net.Sockets;
using HatFClient.Common;

namespace HatFClient.Repository {
    public class BlobRepo {
        private readonly static string API_URL = HatFConfigReader.GetAppSetting("Api:HatF:Uri");
        private readonly static string BLOB_API = $"{API_URL}/api/Blob"; 
        private const string FILE_REPLACE_PATTERN = "^.*\\/";

        private static BlobRepo _instance = new BlobRepo();
        private static HttpClient _httpClient = new HttpClient();

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static BlobRepo GetInstance() {
            if (_instance == null) {
                _instance = new BlobRepo();
            }
            return _instance;
        }

        private string _containerId = null;
        private BindingList<BlobFileInfo> _blobFileInfos = new BindingList<BlobFileInfo>();

        public BindingList<BlobFileInfo> BlobFileInfos { get { return _blobFileInfos; } }

        private BlobRepo() { }

        public async Task Init(string id) {
            _containerId = id;
            _blobFileInfos.Clear();
            
            var blobFiles = await GetFileList(id);
            blobFiles.ForEach(blobFile => {
                blobFile.Name = Regex.Replace(blobFile.Name, FILE_REPLACE_PATTERN,"");
                _blobFileInfos.Add(blobFile);
            });
        }

        public BlobFileInfo Find(string fName) {
            return _blobFileInfos.FirstOrDefault(b => b.Name == fName);
        }

        public async Task Upload(string filePath) {
            try {
                await UploadFile(filePath, _containerId);
                await SetMetaData(filePath, _containerId);
                FileInfo fileInfo = new FileInfo(filePath);
                BlobFileInfo blobFileInfo = Find(fileInfo.Name); 
                if (blobFileInfo != null) {
                    _blobFileInfos.Remove(blobFileInfo);
                }

                _blobFileInfos.Add(FromFilePath(filePath));
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
        }
        public async Task Download(string saveDir, BlobFileInfo blobFileInfo) {
            try {
                var savePath = Path.Combine(saveDir, blobFileInfo.Name);
                await Download(savePath, _containerId, blobFileInfo.Name);
                // RESTORE META DATA (I SHOULD GET THIS FROM API)
                var createdOn = DateTime.Parse(blobFileInfo.CreatedOn);
                var lastModified = DateTime.Parse(blobFileInfo.LastModified);

                File.SetCreationTime(savePath, createdOn);
                File.SetLastWriteTime(savePath, lastModified);

            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
        }
        public async Task Delete(BlobFileInfo blobFileInfo) {
            try {
                await DeleteFile( _containerId, blobFileInfo.Name);

                _blobFileInfos.Remove(blobFileInfo);

            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
        }

        public static BlobFileInfo FromFilePath(string filePath) {
            FileInfo fileInfo = new FileInfo(filePath);
            return new BlobFileInfo {
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
        public static async Task SetMetaData(string filePath, string id) {

            //FileInfo fileInfo = new FileInfo(filePath);
            BlobFileInfo blobFileInfo = FromFilePath(filePath);
            var metaData = new Dictionary<string, string>();
            metaData["CreatedOn"] = blobFileInfo.CreatedOn;
            metaData["LastModified"] = blobFileInfo.LastModified;

            var json = JsonConvert.SerializeObject(metaData);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(BLOB_API + $"/{id}/" + blobFileInfo.Name, content);

            if (response.IsSuccessStatusCode) {
                Debug.WriteLine("OK");

            } else {
                Debug.WriteLine("ERROR:" + response.StatusCode.ToString());
                throw (new Exception($"ERROR:{response.StatusCode}:{response.ReasonPhrase}"));
            }
        }

        public static async Task UploadFile(string filePath, string id) {

            var fileStream = File.OpenRead(filePath);
            var streamContent = new StreamContent(fileStream);
            FileInfo fileInfo = new FileInfo(filePath);

            var response = await _httpClient.PostAsync(BLOB_API + $"/{id}/" + fileInfo.Name, streamContent);

            if (response.IsSuccessStatusCode) {
                Debug.WriteLine("OK");
            } else {
                Debug.WriteLine("ERROR:" + response.StatusCode.ToString());
                throw (new Exception($"ERROR:{response.StatusCode}:{response.ReasonPhrase}"));
            }
        }
        public static async Task Download(string savePath, string id, string fileName) {
            var response = await _httpClient.GetAsync(BLOB_API + $"/{id}/" + fileName);
            if (response.IsSuccessStatusCode) {
                var fileStream = await response.Content.ReadAsStreamAsync();

                using (var file = new FileStream(savePath, FileMode.Create, FileAccess.Write)) {
                    await fileStream.CopyToAsync(file);
                }
            }
        }
        public static async Task DeleteFile(string id, string fileName) {

            var response = await _httpClient.DeleteAsync(BLOB_API + $"/{id}/{fileName}");

            if (response.IsSuccessStatusCode) {
                Debug.WriteLine("OK");
            } else {
                Debug.WriteLine("ERROR:" + response.StatusCode.ToString());
                throw (new Exception($"ERROR:{response.StatusCode}:{response.ReasonPhrase}"));
            }
        }
        public static async Task<List<BlobFileInfo>> GetFileList(string id) {

            Debug.WriteLine(BLOB_API + $"{id}");
            var response = await _httpClient.GetAsync(BLOB_API + $"/{id}");
            if (response.IsSuccessStatusCode) {
                string body = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<List<BlobFileInfo>>(body);
            }
            return null;
        }

    }
}
