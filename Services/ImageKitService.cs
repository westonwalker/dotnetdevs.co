using dotnetdevs.Models;
using Imagekit.Sdk;
using Microsoft.AspNetCore.Components.Forms;

namespace dotnetdevs.Services
{
	public static class ImageKitService
	{
		public static bool ValidateImageSize(IBrowserFile file)
		{
			// decimal fileSize = Math.Round((file.Size / (decimal)1024), 2);
			float fileSize = (file.Size / 1024f) / 1024f;
			if (fileSize > 1)
			{
				return false;
			}
			return true;
		}

		public static bool ValidateImageType(IBrowserFile file)
		{
			if (file.ContentType != "image/jpeg" && file.ContentType != "image/png")
			{
				return false;
			}
			return true;
		}

		public static async Task<string> UploadImage(IBrowserFile file)
		{
			var imageKit = new ImagekitClient(
				Environment.GetEnvironmentVariable("IMAGE_KIT_KEY"),
				Environment.GetEnvironmentVariable("IMAGE_KIT_SECRET"),
				Environment.GetEnvironmentVariable("IMAGE_KIT_URL")
			);
			var extension = System.IO.Path.GetExtension(file.Name);
			var filename = $"{Guid.NewGuid().ToString()}{extension}";
			var resizedImage = await file.RequestImageFileAsync(file.ContentType, 250, 250);
			var buffer = new byte[resizedImage.Size];
			await resizedImage.OpenReadStream().ReadAsync(buffer);
			FileCreateRequest imagekitRequest = new FileCreateRequest
			{
				file = buffer,
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
