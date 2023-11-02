namespace TH.Core.TMP
{
    public static class TMPUtil
    {
        /// <summary>
        /// TMP에 표시할 줄바꿈을 포함하는 텍스트 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ToTMP(this string text)
        {
            return text.Replace("\n", "\n\n");
        }
    }
}