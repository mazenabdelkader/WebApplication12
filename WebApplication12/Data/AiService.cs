namespace WebApplication12.Data
{
    public class AiService
    {
     
            public async Task<(string result, double confidence)> PredictAsync(string imagePath)
            {
                await Task.Delay(1000); // Simulate AI processing
                return ("Benign", 0.92); // Dummy result
            }
        
    }
}
