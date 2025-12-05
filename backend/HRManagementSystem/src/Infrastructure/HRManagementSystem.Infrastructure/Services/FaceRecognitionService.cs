/* using System.Drawing;
using FaceRecognitionDotNet;
namespace HRManagementSystem.Infrastructure.Services;
public class FaceRecognitionService : IFaceRecognitionService
{
    private readonly FaceRecognition _faceRecognition;
    public FaceRecognitionService(FaceRecognition faceRecognition)
    {
        _faceRecognition = faceRecognition;
    }

    public double[] ExtractEmbedding(byte[] imageBytes)
    {
        using var ms = new MemoryStream(imageBytes);
        using var bmp = new Bitmap(ms);
        using var img = FaceRecognition.LoadImage(bmp);

        var encodings = _faceRecognition.FaceEncodings(img).ToList();
        if (!encodings.Any())
            throw new Exception("No face detected");

        return encodings[0].GetRawEncoding();
    }

    public bool CompareEmbeddings(double[] registeredEmbedding, double[] newEmbedding, double tolerance = 0.6)
    {
        if (registeredEmbedding.Length != newEmbedding.Length)
            return false;

        double sum = 0;
        for (int i = 0; i < registeredEmbedding.Length; i++)
        {
            double diff = registeredEmbedding[i] - newEmbedding[i];
            sum += diff * diff;
        }

        double distance = Math.Sqrt(sum);
        return distance <= tolerance;
    }
}
 */