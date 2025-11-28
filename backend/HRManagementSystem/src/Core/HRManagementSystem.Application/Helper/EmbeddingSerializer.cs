namespace HRManagementSystem.Application.Helper;
public static class EmbeddingSerializer
{
    public static byte[] DoubleArrayToBytes(double[] array)
    {
        var bytes = new byte[array.Length * sizeof(double)];
        Buffer.BlockCopy(array, 0, bytes, 0, bytes.Length);
        return bytes;
    }

    public static double[] BytesToDoubleArray(byte[] bytes)
    {
        var array = new double[bytes.Length / sizeof(double)];
        Buffer.BlockCopy(bytes, 0, array, 0, bytes.Length);
        return array;
    }

}
