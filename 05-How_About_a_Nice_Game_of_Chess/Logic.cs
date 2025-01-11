using System.Text;

namespace _05_How_About_a_Nice_Game_of_Chess
{
  internal class Logic
  {
    internal static IEnumerable<char> EnumeratePassword(string doorId)
    {

      long n = 0;
      while (true)
      {
        var inputBytes = Encoding.ASCII.GetBytes(doorId + n.ToString());
        var hash = System.Security.Cryptography.MD5.HashData(inputBytes);
        var sb = new StringBuilder();
        for (int i = 0; i < hash.Length; i++)
        {
          sb.Append(hash[i].ToString("x2"));
        }
        var str = sb.ToString();
        if (str.StartsWith("00000"))
        {
          yield return str[5];
        }
        n++;
      }
    }

    internal static string GetPassword(string doorId)
    {
      return string.Concat(EnumeratePassword(doorId).Take(8));
    }

    internal static string HashDoorId(string doorId, long n)
    {
      var val = doorId + n.ToString();
      var inputBytes = Encoding.ASCII.GetBytes(val);
      var hash = System.Security.Cryptography.MD5.HashData(inputBytes);
      var sb = new StringBuilder();
      for (int i = 0; i < hash.Length; i++)
      {
        sb.Append(hash[i].ToString("x2"));
      }
      return sb.ToString();
    }
  }
}
