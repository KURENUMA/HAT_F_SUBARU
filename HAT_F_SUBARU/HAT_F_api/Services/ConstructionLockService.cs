using HAT_F_api.Models;
using HAT_F_api.Utils;
using Microsoft.EntityFrameworkCore;

namespace HAT_F_api.Services
{
    public class ConstructionLockService
    {
        private HatFContext _hatFContext;
        private HatFApiExecutionContext _hatFApiExecutionContext;
        private UpdateInfoSetter _updateInfoSetter;

        public ConstructionLockService(HatFContext hatFContext, HatFApiExecutionContext hatFApiExecutionContext, UpdateInfoSetter updateInfoSetter)
        {
            _hatFContext = hatFContext;
            _hatFApiExecutionContext = hatFApiExecutionContext;
            _updateInfoSetter = updateInfoSetter;
        }

        /// <summary>
        /// 物件ロック登録
        /// </summary>
        /// <param name="constructionCode"></param>
        /// <param name="emp_id"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, string>> LockAsync(string constructionCode, int emp_id)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            //ロックユーザー取得
            var constructionLockResult = await _hatFContext.ConstructionLocks
                .Where(e => e.ConstructionCode == constructionCode).FirstOrDefaultAsync();
            //ロックユーザーが存在しない場合
            if (constructionLockResult == null)
            {
                //ロック処理
                _hatFContext.ConstructionLocks.Add(new ConstructionLock()
                {
                    ConstructionCode = constructionCode,
                    EditorEmpId = emp_id,
                    EditStartDatetime = DateTime.Now,
                });
                await _hatFContext.SaveChangesAsync();
            }
            else
            {
                var employeesResult = await _hatFContext.Employees
                    .Where(e => e.EmpCode == constructionLockResult.EditorEmpId.ToString()).FirstOrDefaultAsync();
                result.Add("emp_name", employeesResult.EmpName);
                result.Add("edit_start_datetime", constructionLockResult.EditStartDatetime.ToString());
            }

            return result;
            
        }

        /// <summary>
        /// 物件ロック削除
        /// </summary>
        /// <param name="constructionCode"></param>
        /// <returns></returns>
        public async Task<bool> UnLockAsync(string constructionCode)
        {
            var unlock = _hatFContext.ConstructionLocks.Single(x => x.ConstructionCode == constructionCode);
            _hatFContext.ConstructionLocks.Remove(unlock);

            if (await _hatFContext.SaveChangesAsync() == 0)
            {
                return false;
            }
            return true;
        }
    }
}
