namespace HRManagementSystem.Application.Interfaces;
public interface IFaceRecognitionService
{
    float[] ExtractEmbedding(string base64Image);
    bool CompareEmbeddings(float[] storedEmbedding, float[] newEmbedding, float threshold = 0.6f);

}
