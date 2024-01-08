using System.Drawing;
using System.Net.Http;

namespace ECBuilder
{
    /// <summary>
    /// ECBuilder settings
    /// </summary>
    public class ECBuilderSettings
    {

        /// <summary>
        /// The error image to get if an error occurs while <see cref="Helpers.AssetsHelper.GetImage(string, bool, bool)">GetImage</see> get the image.
        /// </summary>
        public static Image AssetsHelperErrorImage { get; set; }

        /// <summary>
        /// <see cref="HttpClient"/> that will be used to perform operations in the <see cref="DataAccess.API">API</see>
        /// </summary>
        public static HttpClient APIClient { get; set; }

        /// <summary>
        /// <see cref="Components.Buttons.EditButton">EditButton</see> edit mode color.
        /// </summary>
        public static Color EditButtonEditColor { get; set; }

        /// <summary>
        /// <see cref="Components.Buttons.EditButton">EditButton</see> save mode color.
        /// </summary>
        public static Color EditButtonSaveColor { get; }
    }
}
