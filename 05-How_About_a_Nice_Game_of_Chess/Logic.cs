using System.Text;

namespace _05_How_About_a_Nice_Game_of_Chess
{
  internal class Logic
  {
    internal record PasswordChar(int Char5, int Char6);

    internal static IEnumerable<PasswordChar> EnumeratePassword(string doorId)
    {
      using var md5 = System.Security.Cryptography.MD5.Create();

      long n = 0;
      while (true)
      {
        var inputBytes = Encoding.ASCII.GetBytes(doorId + n.ToString());
#pragma warning disable CA1850 // Prefer static 'HashData' method over 'ComputeHash'
        var hash = md5.ComputeHash(inputBytes); // because it is faster than HashData
#pragma warning restore CA1850 // Prefer static 'HashData' method over 'ComputeHash'
        if (hash[0] == 0 && hash[1] == 0 && (hash[2]&0xf0) == 0)
        {
          var char5 = hash[2] & 0x0f;
          var char6 = hash[3] >> 4;

          yield return new(char5, char6);
        }

        n++;
      }
    }

    internal static string GetPassword(string doorId)
    {
      return string.Concat(EnumeratePassword(doorId).Take(8).Select(c => c.Char5.ToString("x")));
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
        if (passwordChar.Char5 >= 0 && passwordChar.Char5 <= 7)
        {
          var index = passwordChar.Char5;
          if (password[index] == null)
          {
            password[index] = passwordChar.Char6.ToString("x").First();
            numCorrect++;
          }
        }
      }

      return string.Concat(password);
    }
  }
}
