using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityActivities.Application.Interfaces.QRCode
{
    public interface IQRCodeGeneratorService
    {
        /// <summary>
        /// Generates a QR code image as a byte array based on the provided content.
        /// </summary>
        /// <param name="content">The content to encode in the QR code.</param>
        /// <returns>A byte array representing the generated QR code image.</returns>
        byte[] GenerateQRCode(string content);  
    }
}
