using System.Windows.Forms;

namespace ECBuilder.Helpers
{
    /// <summary>
    /// More useful methods inherited from the <see cref="MessageBox"/>
    /// </summary>
    public class MessageBoxes
    {
        /// <summary>
        /// Shows error iconed message
        /// </summary>
        /// <param name="message">MessagBox message</param>
        /// <param name="title">MessageBox title</param>
        /// <returns><see cref="DialogResult"/></returns>
        public static DialogResult Error(string message, string title = "Hata")
        {
            return MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Shows question iconed message
        /// </summary>
        /// <param name="message">MessagBox message</param>
        /// <param name="title">MessageBox title</param>
        /// <returns><see cref="DialogResult"/></returns>
        public static DialogResult Question(string message, string title = "Onay")
        {
            return MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        /// <summary>
        /// Shows success iconed message
        /// </summary>
        /// <param name="message">MessagBox message</param>
        /// <param name="title">MessageBox title</param>
        /// <returns><see cref="DialogResult"/></returns>
        public static DialogResult Success(string message, string title = "Başarılı")
        {
            return MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Shows info iconed message
        /// </summary>
        /// <param name="message">MessagBox message</param>
        /// <param name="title">MessageBox title</param>
        /// <returns><see cref="DialogResult"/></returns>
        public static DialogResult Information(string message, string title = "Bilgi")
        {
            return MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
