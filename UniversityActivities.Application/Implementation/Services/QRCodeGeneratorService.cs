using QRCoder;
using System;
using System.Collections.Generic;
using System.Text;
using UniversityActivities.Application.Interfaces.QRCode;

namespace UniversityActivities.Application.Implementation.Services
{
    public class QRCodeGeneratorService:IQRCodeGeneratorService
    {
        public byte[] GenerateQRCode(string content)
        {
            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new PngByteQRCode(qrCodeData);
            return qrCode.GetGraphic(20);
        }
    }
}
