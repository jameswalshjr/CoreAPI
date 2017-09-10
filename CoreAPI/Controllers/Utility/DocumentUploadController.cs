using CoreAPI.Attributes;
using CoreAPI.Domain.Dto;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CoreAPI.Controllers.Utility
{
    [Route("api/[controller]")]
    public class DocumentUploadController : Controller
    {
        private static readonly FormOptions _defaultFormOptions = new FormOptions();

        [HttpPost("Usage")]
        //[ValidateAntiForgeryToken]
        [DisableFormValueModelBinding]
        //[EnableCors("LocalHostPolicy")]
        public async Task<IActionResult> Usage()
        {
            try
            {

                if (!MultiPartRequest.IsMultipartContentType(Request.ContentType))
                {
                    return BadRequest($"Expected a multipart request, but got {Request.ContentType}");
                }

                var from = new KeyValueAccumulator();
                string targetFilePath = Path.GetTempFileName(); ;

                var boundary = MultiPartRequest.GetBoundary(
                    MediaTypeHeaderValue.Parse(Request.ContentType),
                    _defaultFormOptions.MultipartBoundaryLengthLimit);
                var reader = new MultipartReader(boundary, HttpContext.Request.Body);

                var section = await reader.ReadNextSectionAsync();
                while (section != null)
                {
                    var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out ContentDispositionHeaderValue contentDisposition);

                    if (hasContentDispositionHeader)
                    {
                        if (MultiPartRequest.HasFileContentDisposition(contentDisposition))
                        {

                            using (var targetStream = System.IO.File.Create(targetFilePath))
                            {
                                await section.Body.CopyToAsync(targetStream);


                            }
                        }
                        else if (MultiPartRequest.HasFormDataContentDisposition(contentDisposition))
                        {
                            // Content-Disposition: form-data; name="key"
                            //
                            // value

                            // Do not limit the key name length here because the 
                            // multipart headers length limit is already in effect.
                            var key = HeaderUtilities.RemoveQuotes(contentDisposition.Name);
                            var encoding = GetEncoding(section);
                            using (var streamReader = new StreamReader(
                                section.Body,
                                encoding,
                                detectEncodingFromByteOrderMarks: true,
                                bufferSize: 1024,
                                leaveOpen: true))
                            {
                                // The value length limit is enforced by MultipartBodyLengthLimit
                                var value = await streamReader.ReadToEndAsync();
                                if (String.Equals(value, "undefined", StringComparison.OrdinalIgnoreCase))
                                {
                                    value = String.Empty;
                                }
                                from.Append(key, value);

                                if (from.ValueCount > _defaultFormOptions.ValueCountLimit)
                                {
                                    throw new InvalidDataException($"Form key count limit {_defaultFormOptions.ValueCountLimit} exceeded.");
                                }
                            }
                        }
                    }

                    // Drains any remaining section body that has not been consumed and
                    // reads the headers for the next section.
                    section = await reader.ReadNextSectionAsync();
                }

                List<UsageForm> ul = new List<UsageForm>();

                string[] alldata = System.IO.File.ReadAllLines(targetFilePath);
                var query = from line in alldata.Skip(1)
                            select line;
                foreach(var item in query)
                {
                    var itemData = item.Split(',');
                    ul.Add(new UsageForm
                    {
                        Client = Convert.ToInt32(itemData[0]),
                        Org = Convert.ToInt32(itemData[1]),
                        Qty = Convert.ToInt32(itemData[2]),
                        LIC = itemData[3],
                        Seq = Convert.ToInt32(itemData[4])
                    });

                }

                System.IO.File.Delete(targetFilePath);
                return Ok(Json(ul));
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message.ToString());
                return Content(HttpStatusCode.InternalServerError.ToString() + ex.InnerException.Message);
            }
        }

        [HttpGet("Token")]
        [GenerateAntiForgeryCookie]
        [EnableCors("LocalHostPolicy")]
        public IActionResult GetToken()
        {
            return Ok();
        }

        private static Encoding GetEncoding(MultipartSection section)
        {
            var hasMediaTypeHeader = MediaTypeHeaderValue.TryParse(section.ContentType, out MediaTypeHeaderValue mediaType);
            // UTF-7 is insecure and should not be honored. UTF-8 will succeed in 
            // most cases.
            if (!hasMediaTypeHeader || Encoding.UTF7.Equals(mediaType.Encoding))
            {
                return Encoding.UTF8;
            }
            return mediaType.Encoding;
        }

    }
}