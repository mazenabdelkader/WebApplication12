using System;
using Microsoft.AspNetCore.Mvc;
using WebApplication12.Data;

namespace WebApplication12.Controllers
{
    public class ImageController : Controller
    {
        [ApiController]
        [Route("api/image")]
      
            private readonly AppDbContext _context;
            private readonly ImageService _imageService;
            private readonly AiService _aiService;

            public ImageController(AppDbContext context, ImageService imageService, AiService aiService)
            {
                _context = context;
                _imageService = imageService;
                _aiService = aiService;
            }

            [HttpPost("upload")]
            public async Task<IActionResult> Upload(IFormFile image, int userId)
            {
                if (image == null)
                    return BadRequest(new { message = "No image uploaded" });

                try
                {
                    var path = _imageService.SaveImage(image);

                    var skinImage = new SkinImage
                    {
                        UserId = userId,
                        ImagePath = path,
                        UploadDate = DateTime.Now
                    };

                    _context.SkinImages.Add(skinImage);
                    _context.SaveChanges();

                    var aiResult = await _aiService.PredictAsync(path);

                    var report = new Report
                    {
                        UserId = userId,
                        ImageId = skinImage.ImageId,
                        DiagnosisResult = aiResult.result,
                        ConfidenceScore = aiResult.confidence
                    };

                    _context.Reports.Add(report);
                    _context.SaveChanges();

                    return Ok(report);
                }
                catch
                {
                    return StatusCode(500, new { message = "Internal Server Error" });
                }
            }
        }
    }

