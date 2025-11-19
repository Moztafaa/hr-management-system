using System.Drawing;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

namespace HRManagementSystem.Infrastructure.Services;
public class FaceRecognitionService : IFaceRecognitionService
{
    private readonly InferenceSession _session;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
   public FaceRecognitionService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        var modelPath = Path.Combine(AppContext.BaseDirectory, "Models", "arcface.onnx");
        _session = new InferenceSession(modelPath);
    }

    public float[] ExtractEmbedding(string base64Image)
    {
        byte[] bytes = Convert.FromBase64String(base64Image);

        using var ms = new MemoryStream(bytes);
        using var img = new Bitmap(ms);

        // 1) preprocess image (112x112)
        var input = ImageToTensor(img);

        // 2) run inference
        var inputs = new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("input_1", input)
            };

        using IDisposableReadOnlyCollection<DisposableNamedOnnxValue> results = _session.Run(inputs);
        Tensor<float> embeddings = results.First().AsTensor<float>();

        return embeddings.ToArray();
    }

    public bool CompareEmbeddings(float[] stored, float[] current, float threshold = 0.6f)
    {
        if (stored == null || current == null)
            return false;

        double sum = 0;
        for (int i = 0; i < stored.Length; i++)
            sum += Math.Pow(stored[i] - current[i], 2);

        double distance = Math.Sqrt(sum);

        return distance < threshold;
    }
    private DenseTensor<float> ImageToTensor(Bitmap bitmap)
    {
        int width = 112;
        int height = 112;
        var resized = new Bitmap(bitmap, new Size(width, height));

        var tensor = new DenseTensor<float>(new[] { 1, height, width, 3 }); // [1,112,112,3]

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var pixel = resized.GetPixel(x, y);
                // normalize to [0,1] float
                tensor[0, y, x, 0] = pixel.R / 255f;
                tensor[0, y, x, 1] = pixel.G / 255f;
                tensor[0, y, x, 2] = pixel.B / 255f;
            }
        }

        return tensor;
    }
}
