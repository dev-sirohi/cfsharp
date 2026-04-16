using System.Diagnostics;
using System.Text;

namespace Codeforces;

public class App
{
    public static void Main(string[] args)
    {
        _ = new Runner(args);
    }
}

public class Runner
{
    private static bool _isLocal = false;
    private const string LOCAL_ARG = "local";
    private const string INPUT_FILE = "input_dvsrh.txt";
    private const string OUTPUT_FILE = "output_dvsrh.txt";
    private FastIO io;

    public Runner(string[] args)
    {
        if (args.Length > 0 && string.Equals(args[0], LOCAL_ARG))
        {
            _isLocal = true;
        }

        if (_isLocal)
        {
            if (!File.Exists(INPUT_FILE))
            {
                throw new Exception("Could not find input file");
            }
            if (!File.Exists(OUTPUT_FILE))
            {
                throw new Exception("Could not find output file");
            }
            Console.SetIn(new StreamReader(INPUT_FILE));
            Console.SetOut(new StreamWriter(OUTPUT_FILE));
        }

        io = new FastIO(true);

        _Run();

        if (_isLocal)
        {
            Console.Out.Flush();
        }
    }

    private void _Run()
    {
        int _t = io.Next<int>();
        while (_t-- > 0)
        {
            _Solve();
        }
    }

    private void _Solve()
    {
        int n = io.Next<int>();

    }
}

public class FastIO
{
    private readonly Stream _stream = Console.OpenStandardInput();
    private readonly byte[] _buffer = new byte[1 << 16];
    private int _ptr = 0;
    private int _len = 0;
    private bool _useCfInputFormat = false;

    public FastIO(bool useCfInputFormat = false)
    {
        _useCfInputFormat = useCfInputFormat;
    }

    private byte Read()
    {
        if (_ptr >= _len)
        {
            _len = _stream.Read(_buffer, 0, _buffer.Length);
            _ptr = 0;
            if (_len == 0)
            {
                return 0;
            }
        }
        return _buffer[_ptr++];
    }

    public int NextInt()
    {
        int c = Read();
        while (c <= ' ')
        {
            if (c == 0)
            {
                return 0;
            }
            c = Read();
        }

        int sign = 1;
        if (c == '-')
        {
            sign = -1;
            c = Read();
        }

        int val = 0;
        while (c >= '0' && c <= '9')
        {
            val = val * 10 + (c - '0');
            c = Read();
        }

        return sign * val;
    }

    public long NextLong()
    {
        int c = Read();
        while (c <= ' ')
        {
            if (c == 0)
            {
                return 0;
            }
            c = Read();
        }

        int sign = 1;
        if (c == '-')
        {
            sign = -1;
            c = Read();
        }

        long val = 0;
        while (c >= '0' && c <= '9')
        {
            val = val * 10 + (c - '0');
            c = Read();
        }

        return sign * val;
    }

    public double NextDouble()
    {
        int c = Read();

        while (c <= ' ')
        {
            if (c == 0)
            {
                return 0;
            }
            c = Read();
        }

        int sign = 1;
        if (c == '-')
        {
            sign = -1;
            c = Read();
        }

        double result = 0;
        while (c >= '0' && c <= '9')
        {
            result = result * 10 + (c - '0');
            c = Read();
        }

        if (c == '.')
        {
            c = Read();
            double fraction = 0;
            double divisor = 1;

            while (c >= '0' && c <= '9')
            {
                fraction = fraction * 10 + (c - '0');
                divisor *= 10;
                c = Read();
            }

            result += fraction / divisor;
        }

        return sign * result;
    }

    public char NextChar()
    {
        int c = Read();
        while (c <= ' ')
        {
            if (c == 0)
            {
                return '\0';
            }
            c = Read();
        }

        if (c > ' ')
        {
            return (char)c;
        }

        return '\0';
    }

    public string NextLine()
    {
        int c = Read();
        while (c < ' ')
        {
            if (c == 0)
            {
                return "";
            }
            c = Read();
        }

        StringBuilder sb = new StringBuilder();

        while (c != '\n' && c != '\r' && c != 0)
        {
            sb.Append((char)c);
            c = Read();
        }

        return sb.ToString();
    }

    public StringBuilder NextLine(bool useStringBuilder = true)
    {
        StringBuilder sb = new StringBuilder();

        int c = Read();
        while (c < ' ')
        {
            if (c == 0)
            {
                return sb;
            }
            c = Read();
        }

        while (c != '\n' && c != '\r' && c != 0)
        {
            sb.Append((char)c);
            c = Read();
        }

        return sb;
    }

    public T Next<T>()
    {
        if (typeof(T) == typeof(int))
        {
            return (T)(object)NextInt();
        }

        if (typeof(T) == typeof(long))
        {
            return (T)(object)NextLong();
        }

        if (typeof(T) == typeof(double))
        {
            return (T)(object)NextDouble();
        }

        if (typeof(T) == typeof(char))
        {
            return (T)(object)NextChar();
        }

        if (typeof(T) == typeof(string))
        {
            return (T)(object)NextLine();
        }

        throw new NotSupportedException(typeof(T).ToString());
    }

    public T[] GetArray<T>(int size = 0, char sep = ',', bool skipBraceCheck = false)
    {
        if (_useCfInputFormat)
        {
            sep = ' ';
            skipBraceCheck = true;
        }

        if (size <= 0)
        {
            return new T[0];
        }

        T[] res = new T[size];

        int c = Read();

        // skip whitespace
        while (c <= ' ')
        {
            if (c == 0)
            {
                return res;
            }
            c = Read();
        }

        bool isBracketed = (c == '[');

        if (!isBracketed && !skipBraceCheck)
        {
            for (int i = 0; i < size; i++)
            {
                res[i] = Next<T>();
            }
            return res;
        }

        for (int i = 0; i < size; i++)
        {
            while (c != 0 && c != '-' && (c < '0' || c > '9'))
            {
                c = Read();
            }

            res[i] = Next<T>();

            c = Read();

            while (c <= ' ')
            {
                c = Read();
            }

            if (c == sep)
            {
                continue;
            }

            if (c == ']')
            {
                break;
            }
        }

        return res;
    }
}