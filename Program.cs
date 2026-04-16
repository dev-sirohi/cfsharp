using System.Diagnostics;
using System.Text;

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
    private const string INPUT_FILE = "../../../LocalFiles/input_dvsrh.txt";
    private const string OUTPUT_FILE = "../../../LocalFiles/output_dvsrh.txt";
    private FastIN IN;
    private FastOUT OUT;
    private Stopwatch? _stopwatch;

    public Runner(string[] args)
    {
        if (args.Length > 0 && string.Equals(args[0], LOCAL_ARG))
        {
            _isLocal = true;
        }

        if (_isLocal)
        {
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        IN = new FastIN(_isLocal, true, INPUT_FILE);
        OUT = new FastOUT(_isLocal, OUTPUT_FILE);

        //_Run();
        _Solve();

        if (_isLocal)
        {
            _stopwatch!.Stop();
            OUT.WriteLine(Utils.ConvertToString("\n\n", "Time taken: ", _stopwatch!.ElapsedMilliseconds, "ms"));
        }

        OUT.Flush();
    }

    private void _Run()
    {
        int _t = IN.Next<int>();
        while (_t-- > 0)
        {
            _Solve();
        }
    }

    private void _Solve()
    {
        int solvableProblemsCount = 0;
        int n = IN.Next<int>();
        for (int i = 0; i < n; i++)
        {
            int opinionsCount = 0;
            for (int j = 0; j < 3; j++)
            {
                int opinion = IN.Next<int>();
                opinionsCount += opinion;
            }
            if (opinionsCount > 1)
            {
                solvableProblemsCount++;
            }
        }
        OUT.WriteLine(Utils.ConvertToString(solvableProblemsCount));
    }
}

public static class Utils
{
    public static string ConvertToString(params object[] paramsArr)
    {
        StringBuilder sb = new StringBuilder();
        foreach (object param in paramsArr)
        {
            sb.Append(param);
        }
        return sb.ToString();
    }
}

public class FastIN : IDisposable
{
    private readonly Stream _stream;
    private readonly byte[] _buffer = new byte[1 << 16];
    private int _ptr = 0;
    private int _len = 0;
    private bool _useCfInputFormat = false;

    public FastIN(bool useLocal = false, bool useCfInputFormat = false, string inputFile = "")
    {
        _useCfInputFormat = useCfInputFormat;

        if (useLocal && !string.IsNullOrWhiteSpace(inputFile))
        {
            _stream = new FileStream(inputFile, FileMode.OpenOrCreate, FileAccess.Read);
        }
        else
        {
            _stream = Console.OpenStandardInput();
        }
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

    ~FastIN()
    {
        _stream.Dispose();
    }

    public void Dispose()
    {
        _stream.Dispose();
    }
}

public class FastOUT : IDisposable
{
    private readonly Stream _stream;
    private readonly byte[] _buffer = new byte[1 << 16];
    private int _ptr = 0;

    public FastOUT(bool useLocal = false, string outputFile = "")
    {
        if (useLocal && !string.IsNullOrWhiteSpace(outputFile))
        {
            _stream = new FileStream(outputFile, FileMode.Create, FileAccess.Write);
        }
        else
        {
            _stream = Console.OpenStandardOutput();
        }
    }

    private void FlushBuffer()
    {
        _stream.Write(_buffer, 0, _ptr);
        _stream.Flush();
        _ptr = 0;
    }

    public void WriteChar(char c)
    {
        if (_ptr == _buffer.Length)
        {
            FlushBuffer();
        }
        _buffer[_ptr++] = (byte)c;
    }

    public void WriteLine(string line = "")
    {
        int n = line.Length;
        for (int i = 0; i < n; i++)
        {
            WriteChar(line[i]);
        }
        WriteChar('\n');
    }

    public void WriteInt(int x)
    {
        if (x == 0)
        {
            WriteChar('0');
            return;
        }

        if (x == int.MinValue)
        {
            WriteLine("-2147483648");
            return;
        }

        if (x < 0)
        {
            WriteChar('-');
            x = -x;
        }

        int start = _ptr;

        while (x > 0)
        {
            if (_ptr == _buffer.Length) FlushBuffer();
            _buffer[_ptr++] = (byte)('0' + (x % 10));
            x /= 10;
        }

        Array.Reverse(_buffer, start, _ptr - start);
    }

    public void WriteLineInt(int x)
    {
        WriteInt(x);
        WriteChar('\n');
    }

    public void Flush()
    {
        FlushBuffer();
    }

    ~FastOUT()
    {
        Flush();
    }

    public void Dispose()
    {
        Flush();
        _stream.Dispose();
    }
}