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
    private FastIn In;
    private FastOut Out;
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

        In = new FastIn(_isLocal, true, INPUT_FILE);
        Out = new FastOut(_isLocal, OUTPUT_FILE);

        //_Run();
        _Solve();

        if (_isLocal)
        {
            _stopwatch!.Stop();
            Out.WriteLine(StringUtils.ToString("\n\n", "Time taken: ", _stopwatch!.ElapsedMilliseconds, "ms"));
        }

        Out.Flush();
    }

    private void _Run()
    {
        int _t = In.Next<int>();
        while (_t-- > 0)
        {
            _Solve();
        }
    }

    private void _Solve()
    {
        int queueSize = In.Next<int>();
        int timeSpan = In.Next<int>();
        char[] queue = In.GetArray<char>(queueSize);
        while (timeSpan-- > 0)
        {
            for (int i = queueSize - 2; i >= 0; i--)
            {
                if (queue[i] == 'B' && queue[i + 1] == 'G')
                {
                    queue[i] = 'G';
                    queue[i + 1] = 'B';
                    i--;
                }
            }
        }
        Out.WriteLine(StringUtils.ToString(queue));
    }
}

public static class StringUtils
{
    public static string ToString(params object[] paramsArr)
    {
        StringBuilder sb = new StringBuilder();
        foreach (object param in paramsArr)
        {
            sb.Append(param);
        }
        return sb.ToString();
    }

    public static string ToString(char[] arr)
    {
        StringBuilder sb = new StringBuilder();
        foreach (object param in arr)
        {
            sb.Append(param);
        }
        return sb.ToString();
    }

    public static string ToString(string[] arr)
    {
        StringBuilder sb = new StringBuilder();
        foreach (object param in arr)
        {
            sb.Append(param);
        }
        return sb.ToString();
    }

    public static string ToString(int[] arr)
    {
        StringBuilder sb = new StringBuilder();
        foreach (object param in arr)
        {
            sb.Append(param);
        }
        return sb.ToString();
    }

    public static string ToString(long[] arr)
    {
        StringBuilder sb = new StringBuilder();
        foreach (object param in arr)
        {
            sb.Append(param);
        }
        return sb.ToString();
    }

    public static string ToString(double[] arr)
    {
        StringBuilder sb = new StringBuilder();
        foreach (object param in arr)
        {
            sb.Append(param);
        }
        return sb.ToString();
    }
}

public class FastIn : IDisposable
{
    private readonly Stream _stream;
    private readonly byte[] _buffer = new byte[1 << 16];
    private int _ptr = 0;
    private int _len = 0;
    private bool _useCfInputFormat = false;

    public FastIn(bool useLocal = false, bool useCfInputFormat = false, string inputFile = "")
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

    private int NextInt()
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

    private long NextLong()
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

    private double NextDouble()
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

    private char NextChar()
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

    private string NextLine()
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

    private StringBuilder NextLine(bool useStringBuilder = true)
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

    private T GetValueFromAscii<T>(int c)
    {
        if (typeof(T) == typeof(int))
        {
            return (T)(object)(c - '0');
        }

        if (typeof(T) == typeof(long))
        {
            return (T)(object)(c - '0');
        }

        if (typeof(T) == typeof(double))
        {
            return (T)(object)(c - '0');
        }

        if (typeof(T) == typeof(char))
        {
            return (T)(object)((char)c);
        }

        if (typeof(T) == typeof(string))
        {
            return (T)(object)((char)c);
        }

        throw new NotSupportedException(typeof(T).ToString());
    }

    public T[] GetArray<T>(int size)
    {
        T[] arr = new T[size];
        if (size == 0)
        {
            return arr;
        }

        int index = 0;
        while (size-- > 0)
        {
            arr[index++] = Next<T>();
        }

        return arr;
    }

    ~FastIn()
    {
        _stream.Dispose();
    }

    public void Dispose()
    {
        _stream.Dispose();
    }
}

public class FastOut : IDisposable
{
    private readonly Stream _stream;
    private readonly byte[] _buffer = new byte[1 << 16];
    private int _ptr = 0;

    public FastOut(bool useLocal = false, string outputFile = "")
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

    public void Flush()
    {
        FlushBuffer();
    }

    ~FastOut()
    {
        Flush();
    }

    public void Dispose()
    {
        Flush();
        _stream.Dispose();
    }
}
