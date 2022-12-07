using System.Text.RegularExpressions;

namespace Nhom24.Models.Process
{
    public class StringProcess
    {
        public string AutoGenerateCode(string strInput)
        {
            string strResults = "", numPart = "", strPart = "";
            //tach phan so tu strInput
            //VD: strInput = "STD001" => numPart = "001"
            numPart = Regex.Match(strInput, @"\d+").Value;
            //tach phan chu tu strInput
            strPart = Regex.Match(strInput, @"\D+").Value;
            //tang phan so len 1 don vi
            int intPart = (Convert.ToInt32(numPart) + 1);
            //bo sung cac ky tu 0 con thieu
            for (int i = 0; i < numPart.Length - intPart.ToString().Length; i++)
            {
                strPart += "0";
            }
            strResults = strPart + intPart;
            return strResults;
        }
    }
}
