using HAT_F_api.Models;
using HAT_F_api.Utils;
using Microsoft.EntityFrameworkCore;

namespace HAT_F_api.Services
{
    public class AmountCheckLockService
    {
        private HatFContext _hatFContext;
        private HatFApiExecutionContext _hatFApiExecutionContext;
        private UpdateInfoSetter _updateInfoSetter;

        public AmountCheckLockService(HatFContext hatFContext, HatFApiExecutionContext hatFApiExecutionContext, UpdateInfoSetter updateInfoSetter)
        {
            _hatFContext = hatFContext;
            _hatFApiExecutionContext = hatFApiExecutionContext;
            _updateInfoSetter = updateInfoSetter;
        }

        /// <summary>
        /// 仕入金額照合ロック登録
        /// </summary>
        /// <param name="hatOrderNo"></param>
        /// <param name="emp_id"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, string>> LockAsync(string hatOrderNo, int emp_id)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            //ロックユーザー取得
            var amountCheckLockResult = await _hatFContext.AmounCheckLocks
                .Where(e => e.HatOrderNo == hatOrderNo).FirstOrDefaultAsync();
            //ロックユーザーが存在しない場合
            if (amountCheckLockResult == null)
            {
                //ロック処理
                _hatFContext.AmounCheckLocks.Add(new AmounCheckLock()
                {
                    HatOrderNo = hatOrderNo,
                    EditorEmpId = emp_id,
                    EditStartDatetime = DateTime.Now,
                });
                await _hatFContext.SaveChangesAsync();
            }
            else
            {
                var employeesResult = await _hatFContext.Employees
                    .Where(e => e.EmpCode == amountCheckLockResult.EditorEmpId.ToString()).FirstOrDefaultAsync();
                result.Add("emp_name", employeesResult.EmpName);
                result.Add("edit_start_datetime", amountCheckLockResult.EditStartDatetime.ToString());
            }

            return result;
        }

        /// <summary>
        /// 仕入金額照合ロック削除
        /// </summary>
        /// <param name="hatOrderNo"></param>
        /// <returns></returns>
        public async Task<bool> UnLockAsync(string hatOrderNo)
        {
            var unlock = _hatFContext.AmounCheckLocks.Single(x => x.HatOrderNo == hatOrderNo);
            _hatFContext.AmounCheckLocks.Remove(unlock);

            if (await _hatFContext.SaveChangesAsync() == 0)
            {
                return false;
            }
            return true;
        }
    }
}
