using System.ComponentModel.DataAnnotations;
using Badminton.Model.Global;
using Microsoft.AspNetCore.Http;

namespace Badminton.Model.Common;

public class UploadDocument
{
    [Required(ErrorMessage = "請選擇圖片")]
    public required IFormFile Photo { get; set; }

    [FileExtensions(Extensions = "jpg,jpeg,png")]
    public string PhotoName => Photo?.FileName;
}