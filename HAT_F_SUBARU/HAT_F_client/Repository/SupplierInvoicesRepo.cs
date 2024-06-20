using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using HAT_F_api.CustomModels;
using HatFClient.Constants;

namespace HatFClient.Repository
{
    internal class SupplierInvoicesRepo
    {
        public static  SupplierInvoicesRepo instance = null;

        private const int DEFAULT_ROWS = 200;


        private SupplierInvoicesRepo() { }
        [System.Runtime.CompilerServices.MethodImpl(MethodImplOptions.Synchronized)]
        public static SupplierInvoicesRepo GetInstance() {
            if (instance == null) {
                instance = new SupplierInvoicesRepo();
            }
            return instance;
        }
        public async Task<List<SupplierInvoices>> getAll() {
            return (await Program.HatFApiClient.GetAsync<List<SupplierInvoices>>(ApiResources.HatF.Client.SupplierInvoices)).Data;
        }

    }
}
