using dotnetdevs.Models;
using Imagekit.Sdk;

namespace dotnetdevs.Services
{
	public static class ImageKitService
	{
		public static bool ValidateImageSize(IFormFile file)
		{
			decimal fileSize = Math.Round(((decimal)file.Length / (decimal)1024), 2);
			if (fileSize > 1024)
			{
				return false;
			}
			return true;
		}

		public static bool ValidateImageType(IFormFile file)
		{
			if (file.ContentType != "image/jpeg" && file.ContentType != "image/png")
			{
				return false;
			}
			return true;
		}

		public static async Task<string> UploadImage(IFormFile file)
		{
			var imageKit = new ImagekitClient(
				Environment.GetEnvironmentVariable("IMAGE_KIT_KEY"),
				Environment.GetEnvironmentVariable("IMAGE_KIT_SECRET"),
				Environment.GetEnvironmentVariable("IMAGE_KIT_URL")
			);
			var extension = System.IO.Path.GetExtension(file.FileName);
			var filename = $"{Guid.NewGuid().ToString()}{extension}";
			await using var memoryStream = new MemoryStream();
			await file.CopyToAsync(memoryStream);
			var bytes = memoryStream.ToArray();
			FileCreateRequest imagekitRequest = new FileCreateRequest
			{
				file = bytes,
				fileName = filename,
			};
			imagekitRequest.tags = new List<string>()
				{
					"dotnetdevs"
				};
			Result result = await imageKit.UploadAsync(imagekitRequest);
			return result.url;
		}
	}
}
