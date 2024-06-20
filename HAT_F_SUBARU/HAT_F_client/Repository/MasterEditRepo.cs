using HAT_F_api.CustomModels;
using HatFClient.Constants;
using System;
using System.Collections.Generic;

namespace HatFClient.Repository
{
    [Obsolete]
    internal class MasterEditRepo {

        private static MasterEditRepo instance = null;
        private List<MasterTable> entities = new List<MasterTable>();

        private MasterEditRepo() {
            var response = Program.HatFApiClient.Get<List<MasterTable>>(ApiResources.HatF.MasterEditor.MasterTableList);
            this.entities = response.Data;
        }

        public static MasterEditRepo GetInstance() {
            if (instance == null) {
                instance = new MasterEditRepo();
            }
            return instance;
        }

        public List<MasterTable> Entities { get {  return entities; } }
    }
}
