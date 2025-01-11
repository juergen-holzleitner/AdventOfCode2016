using System.Text;

namespace _05_How_About_a_Nice_Game_of_Chess
{
  internal class Logic
  {
    internal record PasswordChar(char Char5, char Char6);

    internal static IEnumerable<PasswordChar> EnumeratePassword(string doorId)
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
          yield return new(str[5], str[6]);
        }
        n++;
      }
    }

    internal static string GetPassword(string doorId)
    {
      return string.Concat(EnumeratePassword(doorId).Take(8).Select(c => c.Char5));
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

    internal static string GetPassword2(string doorId)
    {
      var password = new char?[8];

      int numCorrect = 0;
      var passwordEnumerator = EnumeratePassword(doorId).GetEnumerator();
      while (numCorrect < 8)
      {
        if (!passwordEnumerator.MoveNext())
          throw new ApplicationException("Not enough passwords");

        var passwordChar = passwordEnumerator.Current;
        if (passwordChar.Char5 >= '0' && passwordChar.Char5 <= '7')
        {
          var index = passwordChar.Char5 - '0';
          if (password[index] == null)
          {
            password[index] = passwordChar.Char6;
            numCorrect++;
          }
        }
      }

      return string.Concat(password);
    }
  }
}
