using System;
public static class NilsCounter
{
    public static string SplitStrings(string mainString, NilsEnum keyWord, string number)
    {
        string a1   = "William";
        string a2   = "Edmalm";
        string temp = keyWord.ToString();
        if (mainString.Contains(temp))
        {
            string[] parts = mainString.Split(new[] { temp }, 2, System.StringSplitOptions.None);
            a1 = parts[0].Trim();
            a2 = parts[1].Trim();
        }
        else return mainString;
        string result = a1 + " " + number + " " + a2;
        return result;
    }
    [Serializable] public enum NilsEnum { @damage, @block, @heal }
}