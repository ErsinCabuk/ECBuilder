using System;
using System.Drawing;
using System.IO;

namespace ECBuilder.Helpers
{
    /// <summary>
    /// Helpers for assets (images, documents)
    /// </summary>
    public class AssetsHelper
    {
        /// <summary>
        /// Gets an image of the file path.
        /// </summary>
        /// <param name="imagePath">Image path</param>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        /// <param name="showErrorMessage">If the image cannot be get, show an error message.</param>
        /// <param name="getTransparentImage">If the image cannot be get, replace it with a transparent image.</param>
        /// <returns>Image of the file path.</returns>
        public static Image GetImage(string imagePath, bool showErrorMessage = false, bool getTransparentImage = false)
        {
            Image image = null;

            try
            {
                if (string.IsNullOrEmpty(imagePath) && getTransparentImage)
                {
                    image = new Bitmap(50, 50);
                }
                else if (!File.Exists(imagePath) && !string.IsNullOrEmpty(imagePath))
                {
                    if (showErrorMessage) MessageBoxes.Error($"{imagePath} yolundaki resim bulunamadı.");
                    else image = ECBuilderSettings.AssetsHelperErrorImage;
                }
                else if (File.Exists(imagePath))
                {
                    image = Image.FromFile(imagePath);
                    image.Tag = imagePath;
                }
            }
            catch (Exception exception)
            {
                MessageBoxes.Error("Resim getirilirken bir hata oluştu. Hata: \n" + exception);
            }

            return image;
        }
    }
}
