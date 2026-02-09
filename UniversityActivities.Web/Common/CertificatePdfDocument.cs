using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using UniversityActivities.Application.DTOs.Activities.Student;
namespace UniversityActivities.Web.Common
{
    public class CertificatePdfDocument : IDocument
    {
        private readonly AttendanceCertificateDto _data;

        public CertificatePdfDocument(AttendanceCertificateDto data)
        {
            _data = data;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(40);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(16));

                page.Content().Border(4).BorderColor(Colors.Blue.Medium).Padding(40).Column(col =>
                {
                    col.Spacing(20);

                    col.Item().AlignCenter().Text("Certificate of Attendance")
                        .FontSize(32)
                        .Bold()
                        .FontColor(Colors.Blue.Darken2);

                    col.Item().AlignRight().Text(text =>
                    {
                        text.Span("Certificate No: ").SemiBold();
                        text.Span(_data.CertificateNumber.ToString().ToUpper());
                    });

                    col.Item().AlignCenter().Text("This certificate is proudly presented to");

                    col.Item().AlignCenter().Text(_data.StudentName)
                        .FontSize(28)
                        .Bold();

                    col.Item().AlignCenter().Text("For attending the activity");

                    col.Item().AlignCenter().Text(_data.ActivityTitle)
                        .FontSize(20)
                        .Bold();

                    col.Item().AlignCenter().Text($"Held on {_data.ActivityDate}");

                    col.Item().PaddingTop(40).Row(row =>
                    {
                        row.RelativeItem().Column(c =>
                        {
                            c.Item().Text("_____________________");
                            c.Item().Text(_data.ManagementName).Bold();
                        });

                        row.RelativeItem().AlignRight().Column(c =>
                        {
                            c.Item().Text("_____________________");
                            c.Item().Text("Date").Bold();
                        });
                    });
                });
            });
        }
    }
}
