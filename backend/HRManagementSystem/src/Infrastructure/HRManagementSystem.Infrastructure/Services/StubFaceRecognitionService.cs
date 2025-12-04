//using HRManagementSystem.Application.Interfaces;

//namespace HRManagementSystem.Infrastructure.Services;

///// <summary>
///// Stub implementation of IFaceRecognitionService for development/environments without native Dlib libraries.
///// Face recognition features will not work with this implementation.
///// </summary>
//public class StubFaceRecognitionService : IFaceRecognitionService
//{
//    public double[] ExtractEmbedding(byte[] imageBytes)
//    {
//        throw new NotSupportedException(
//            "Face recognition is not available. Native Dlib libraries are required. " +
//            "On Linux, install: sudo apt-get install libdlib-dev libopenblas-dev liblapack-dev");
//    }

//    public bool CompareEmbeddings(double[] registeredEmbedding, double[] newEmbedding, double tolerance = 0.6)
//    {
//        throw new NotSupportedException(
//            "Face recognition is not available. Native Dlib libraries are required. " +
//            "On Linux, install: sudo apt-get install libdlib-dev libopenblas-dev liblapack-dev");
//    }
//}
