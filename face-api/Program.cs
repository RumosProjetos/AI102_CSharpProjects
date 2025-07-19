using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Azure;
using SkiaSharp;

// Import namespaces
using Azure.AI.Vision.Face;


namespace analyze_faces
{
    class Program
    {

        static void Main(string[] args)
        {
            // Clear the console
            Console.Clear();

            try
            {
                // Get config settings from AppSettings
                IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
                IConfigurationRoot configuration = builder.Build();
                string cogSvcEndpoint = configuration["AIServicesEndpoint"];
                string cogSvcKey = configuration["AIServiceKey"];

                // Get image
                string imageFile = "images/face1.jpg";
                if (args.Length > 0)
                {
                    imageFile = args[0];
                }

                // Authenticate Face client
                AzureKeyCredential credential = new AzureKeyCredential(cogSvcKey);
                var client = new FaceClient(new Uri(cogSvcEndpoint), credential);

                var requiredFaceAttributes = new FaceAttributeType[] {
                    FaceAttributeType.Detection01.Occlusion,
                    FaceAttributeType.Detection01.Accessories,
                    FaceAttributeType.Detection01.HeadPose,
                    FaceAttributeType.Detection01.Noise
                };

                using FileStream stream = new FileStream(imageFile, FileMode.Open);

                var response2 = client.Detect(BinaryData.FromStream(stream), 
                    FaceDetectionModel.Detection01, 
                    FaceRecognitionModel.Recognition01, 
                    returnFaceId: false, 
                    returnFaceLandmarks: true, 
                    returnFaceAttributes: requiredFaceAttributes
                    );

                IReadOnlyList<FaceDetectionResult> faces2 = response2.Value;

                if (faces2.Count >= 1)
                {
                    Console.WriteLine($"Detected Faces: {faces2.Count}");
                    var face_count = 0;
                    foreach (var face in faces2)
                    {
                        face_count += 1;
                        //print('\nFace number {}'.format(face_count))
                        Console.WriteLine($"Face number {face_count}");
                        Console.WriteLine($" Head Pose (Yaw) {face.FaceAttributes.HeadPose.Yaw}");                        
                    }

                    AnnotateFaces(imageFile, faces2);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void AnnotateFaces(string imageFile, IReadOnlyList<FaceDetectionResult> detectedFaces)
        {
            Console.WriteLine($"\nAnnotating faces in image...");

            // Load the image using SkiaSharp
            using SKBitmap bitmap = SKBitmap.Decode(imageFile);
            using SKCanvas canvas = new SKCanvas(bitmap);

            SKPaint rectPaint = new SKPaint
            {
                Color = SKColors.LightGreen,
                StrokeWidth = 3,
                Style = SKPaintStyle.Stroke,
                IsAntialias = true
            };

            SKPaint textPaint = new SKPaint
            {
                Color = SKColors.White,
                IsAntialias = true
            };

            SKFont textFont = new SKFont(SKTypeface.Default, 24, 1, 0);

            // Annotate each face in the image
            int faceCount = 0;
            foreach (var face in detectedFaces)
            {
                faceCount++;
                var r = face.FaceRectangle;
                SKRect rect = new SKRect(r.Left, r.Top, r.Left + r.Width, r.Top + r.Height);
                canvas.DrawRect(rect, rectPaint);

                string annotation = $"Face number {faceCount}";
                canvas.DrawText(annotation, r.Left, r.Top, SKTextAlign.Left, textFont, textPaint);
            }

            // Save annotated image
            var outputFile = "detected_faces.jpg";
            using (SKFileWStream output = new SKFileWStream(outputFile))
            {
                bitmap.Encode(output, SKEncodedImageFormat.Jpeg, 100);
            }

            Console.WriteLine($"  Results saved in {outputFile}\n");
        }
    }
}
