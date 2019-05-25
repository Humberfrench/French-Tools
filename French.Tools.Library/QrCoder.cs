using System.Drawing;

namespace French.Tools.Library
{
    public class QrCoder
    {
        public byte[] GerarQrCode(int width, int height, string texto)
        {
            var bw = new ZXing.BarcodeWriter
            {
                Options = new ZXing.Common.EncodingOptions
                {
                    Width = width,
                    Height = height,
                    Margin = 0
                },
                Format = ZXing.BarcodeFormat.QR_CODE
            };
            var converter = new ImageConverter();
            var resultado = (byte[])converter.ConvertTo(bw.Write(texto), typeof(byte[]));
            return resultado;
        }
        public byte[] GerarBarCode(int width, int height, string texto)
        {
            var bw = new ZXing.BarcodeWriter
            {
                Options = new ZXing.Common.EncodingOptions
                {
                    Width = width,
                    Height = height,
                    Margin = 0,
                },
                Format = ZXing.BarcodeFormat.CODE_39
            };
            var converter = new ImageConverter();

            

            var resultado = (byte[])converter.ConvertTo(bw.Write(texto), typeof(byte[]));
            return resultado;
        }
        public byte[] GerarBarCode(string texto)
        {
            var bw = new ZXing.BarcodeWriter
            {
                Format = ZXing.BarcodeFormat.CODE_39,
                Options = new ZXing.QrCode.QrCodeEncodingOptions
                {
                ErrorCorrection = ZXing.QrCode.Internal.ErrorCorrectionLevel.H,
                //Width = settings.BarCodeMaxWidth,
                //Height = settings.BarCodeHeight,
                PureBarcode = true,
            }
            };
            var converter = new ImageConverter();

            var bitmap = bw.Write(texto);

            var resultado = (byte[])converter.ConvertTo(bitmap, typeof(byte[]));
            return resultado;
        }

    }
}