using System.Data;
using C1.Framework.Drawing.Gdi;
using HatFClient.Repository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using HatFClient.CustomControls;
using System.Globalization;
using System.Diagnostics;
using HatFClient.ValueObject;

namespace HatFClient.Common
{
    internal class HatFComParts
    {
        #region << 入力文字制限 >>
        public static bool BoolChkCharOnKeyPressNumAlphabet(char charProc)
        {
            bool boolRet = false;
            if ((charProc < '0' || '9' < charProc) && (charProc < 'a' || 'z' < charProc) && (charProc < 'A' || 'Z' < charProc) && charProc != '\b')
            {
                boolRet = true;
            }
            return boolRet;
        }
        public static bool BoolChkCharOnKeyPressNumHyphen(char charProc)
        {
            bool boolRet = false;
            if ((charProc < '0' || '9' < charProc) && charProc != '-' && charProc != '\b')
            {
                boolRet = true;
            }
            return boolRet;
        }
        public static bool BoolChkCharOnKeyPressNumOnly(char charProc)
        {
            bool boolRet = false;
            if ((charProc < '0' || '9' < charProc) && charProc != '\b')
            {
                boolRet = true;
            }
            return boolRet;
        }
        #endregion

        #region << 入力チェック >>
        public static string GetErrMsgFocusOut(HatF_ErrorMessageFocusOutRepo repo, string strCd)
        {
            string strRet = @"";
            var varRet = repo.Entities.Find(opt => opt.Id.Equals(strCd));
            if (varRet != null)
            {
                strRet = varRet.Message;
            }
            return strRet;
        }
        public static string GetErrMsgButton(HatF_ErrorMessageButtonRepo repo, string strCd)
        {
            string strRet = @"";
            var varRet = repo.Entities.Find(opt => opt.Id.Equals(strCd));
            if (varRet != null)
            {
                strRet = varRet.Message;
            }
            return strRet;
        }
        public static bool BoolIsHalfByRegex(string strDat)
        {
            return (Regex.IsMatch(strDat, @"^[\u0020-\u007E\uFF66-\uFF9F]+$"));
        }
        public static bool BoolIsAlphabetNumByRegex(string strDat)
        {
            return (Regex.IsMatch(strDat, @"^[0-9A-Z]+$"));
        }
        public static bool BoolIsHyphenNumByRegex(string strDat)
        {
            return (Regex.IsMatch(strDat, @"^[0-9-]+$"));
        }
        public static bool BoolIsNumOnlyByRegex(string strDat)
        {
            return (Regex.IsMatch(strDat, @"^[0-9]+$"));
        }
        public static bool BoolIsNum1to9ByRegex(string strDat)
        {
            return (Regex.IsMatch(strDat, @"^[1-9]+$"));
        }
        public static bool BoolIsChar4Or7(string strDat)
        {
            bool boolFlg = true;
            switch (strDat.Length)
            {
                case 0:
                case 4:
                case 7:
                    break;
                default:
                    boolFlg = false;
                    break;
            }
            return boolFlg;
        }
        public static bool BoolIsInputDateOver(object objDat, int intOver)
        {
            bool boolFlg = true;
            DateTime dateTime = DateTime.Now;
            if (!string.IsNullOrEmpty(objDat.ToString()))
            {
                DateTime dateDat = DateTime.Parse(objDat.ToString());
                TimeSpan span = dateDat - dateTime;
                if (span.Days >= intOver)
                {
                    boolFlg = false;
                }
            }
            return boolFlg;
        }
        public static bool BoolIsInputDateUnder(object objDat, int intUnder)
        {
            bool boolFlg = true;
            DateTime dateTime = DateTime.Now;
            if (!string.IsNullOrEmpty(objDat.ToString()))
            {
                DateTime dateDat = DateTime.Parse(objDat.ToString());
                TimeSpan span = dateTime - dateDat;
                if (span.Days >= intUnder)
                {
                    boolFlg = false;
                }
            }
            return boolFlg;
        }
        public static bool IsZipCode(string strInput)
        {
            return Regex.IsMatch(strInput, @"^\d\d\d-\d\d\d\d$", RegexOptions.ECMAScript);
        }
        public static bool BoolIsComboboxValidation(ComboBoxEx objCombo)
        {
            bool boolFlg = true;
            if (!string.IsNullOrEmpty(objCombo.Text))
            {
                List<string> items = objCombo.Items.Cast<string>().ToList();
                var varRet = items.Find(opt => opt.Equals(objCombo.Text));
                if (varRet == null) { boolFlg = false; }
            }
            return boolFlg;
        }
        public static void SetColorOnErrorControl(Control conBox)
        {
            conBox.Font = new System.Drawing.Font(conBox.Font, FontStyle.Bold);
            conBox.BackColor = Color.Red;
            conBox.ForeColor = Color.White;
        }
        #endregion

        #region << メッセージエリア制御 >>
        public static void InitMessageArea(Control conName)
        {
            conName.Font = new System.Drawing.Font(conName.Font, FontStyle.Regular);
            conName.ForeColor = SystemColors.WindowText;
            conName.Text = string.Empty;
        }
        public static void ShowMessageAreaError(Control conName, string strMsg)
        {
            conName.Font = new System.Drawing.Font(conName.Font, FontStyle.Bold);
            conName.ForeColor = Color.Red;
            conName.Text = strMsg;
        }
        #endregion

        #region << 発注状態 >>
        public static JHOrderState GetHattyuJyoutai(string strDEL_FLG, string strUKESHO_FLG, string strDEN_STATE)
        {
            if (CngNull2Str(strDEL_FLG).Equals(@"1"))
            {
                return JHOrderState.Deleted;
            }
            else
            {
                //if ((CngNull2Str(strICHU_FLG).Equals(@"1")))
                //{
                //    strRet = @"5";   // 逸注
                //}
                //else
                //{
                if ((CngNull2Str(strUKESHO_FLG).Equals(@"1")))
                {
                    return JHOrderState.Ukesyo;
                }
                else
                {
                    switch (CngNull2Str(strDEN_STATE))
                    {
                        case @"5":
                            return JHOrderState.Completed;
                        case @"4":
                            return JHOrderState.Acos;
                        case @"1":
                        case @"2":
                        case @"3":
                            return JHOrderState.Ordered;
                        default:
                            return JHOrderState.PreOrder;
                    }
                }

                //}
            }
        }
        #endregion

        #region << 整形等 >>
        public static string DoFormatN0(object objData)
        {
            string strRet = @"";
            decimal? dt = objData is decimal ? (decimal)objData : DoParseDecimal(objData);
            if (dt != null)
            {
                strRet = string.Format("{0:#,#}", (decimal)dt);
            }
            return strRet;
        }
        public static string DoFormatN1(object objData)
        {
            string strRet = @"";
            //decimal? dt = DoParseDecimal(objData);
            decimal? dt = objData is decimal ? (decimal)objData : DoParseDecimal(objData);
            if (dt != null)
            {
                strRet = string.Format("{0:#,0.#}", (decimal)dt);
            }
            return strRet;
        }
        public static string DoFormatN2(object objData)
        {
            string strRet = @"";
            // decimal? dt = DoParseDecimal(objData);
            decimal? dt = objData is decimal ? (decimal)objData : DoParseDecimal(objData);
            if (dt != null)
            {
                strRet = string.Format("{0:#,0.##}", (decimal)dt);
            }
            return strRet;
        }
        public static decimal? DoParseDecimal(object objData)
        {
            if (objData != null && decimal.TryParse(objData.ToString()?.Replace(",", ""), out decimal tmpData))
            {
                return tmpData;
            }
            else
            {
                return null;
            }
        }
        public static int? DoParseInt(object objData)
        {
            if (objData != null && int.TryParse(objData.ToString(), out int tmpData))
            {
                return tmpData;
            }
            else
            {
                return null;
            }
        }
        public static short? DoParseShort(object objData)
        {
            if (objData != null && short.TryParse(objData.ToString(), out short tmpData))
            {
                return tmpData;
            }
            else
            {
                return null;
            }
        }
        public static long? DoParseLong(object objData)
        {
            if (objData != null && long.TryParse(objData.ToString(), out long tmpData))
            {
                return tmpData;
            }
            else
            {
                return null;
            }
        }
        public static DateTime? DoParseDateTime(object objData)
        {
            if (objData != null && DateTime.TryParse(objData.ToString(), out DateTime tmpData))
            {
                return tmpData;
            }
            else
            {
                return null;
            }
        }
        public static DateTime DoParseDateTime2(object objData)
        {
            if (objData != null && DateTime.TryParse(objData.ToString(), out DateTime tmpData))
            {
                return tmpData;
            }
            else
            {
                return DateTime.Now;
            }
        }
        public static string DoFormatYYMMDD(object objData)
        {
            string strRet = @"";
            DateTime? dt = DoParseDateTime(objData);
            if (dt != null)
            {
                strRet = ((DateTime)dt).ToString("yy/MM/dd");
            }
            return strRet;
        }
        public static string DoZeroFill(string strInDat, int intKeta)
        {
            string strRet = @"";
            string strZero = @"";
            if (strInDat == null || strInDat.Length == 0) { return @""; }

            if (strInDat.Length < intKeta)
            {
                for (int i = 0; i < (intKeta - strInDat.Length); i++)
                {
                    strZero += @"0";
                }
                strRet = strZero + strInDat;
            }
            else
            {
                strRet = strInDat;
            }
            return strRet;
        }
        public static DateTime? GetDateAddYear(DateTime dtDat, int intAdd)
        {
            if (dtDat == null) { return null; }
            DateTime retDate = dtDat.AddYears(intAdd);
            return retDate;
        }
        public static DateTime? GetDateAddMonth(DateTime dtDat, int intAdd)
        {
            if (dtDat == null) { return null; }
            DateTime retDate = dtDat.AddMonths(intAdd);
            return retDate;
        }
        public static DateTime? GetDateAddDay(DateTime dtDat, int intAdd)
        {
            if (dtDat == null) { return null; }
            DateTime retDate = dtDat.AddDays(intAdd);
            return retDate;
        }
        public static string CngNull2Str(string strVal)
        {
            string strRet = @"";
            if (!string.IsNullOrEmpty(strVal))
            {
                strRet = strVal;
            }
            return strRet;
        }

        /// <summary>文字列が空白でなければ加工し、空白なら初期値を返却する</summary>
        /// <param name="value">文字列</param>
        /// <param name="formatter">加工処理</param>
        /// <param name="defaultValue">初期値</param>
        /// <returns>加工後の文字列または初期値</returns>
        public static string FormatOrDefault(string value, Func<string, string> formatter, string defaultValue)
        {
            return string.IsNullOrEmpty(value) ? defaultValue : formatter(value);
        }

        /// <summary>郵便番号形式(XXX-XXXX)に変換する。7桁以外の場合はそのまま返却する</summary>
        /// <param name="value">変換元の文字列</param>
        /// <returns>郵便番号</returns>
        public static string ToPostCode(string value)
        {
            if (value?.Length != 7)
            {
                return value;
            }
            return $"{value.Substring(0, 3)}-{value.Substring(3, 4)}";
        }
        #endregion
    }
}
