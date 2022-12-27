namespace dotnetdevs.Helpers
{
    public class UIHelpers
    {
        public static string GetAvatarPlaceholder()
        {
            return "https://ik.imagekit.io/nearfal/anonymouse.png";

		}

        public static string truncateText(string text)
        {
			return text.Length <= 25 ? text : text.Substring(0, 25) + "...";
		}
    }
}
