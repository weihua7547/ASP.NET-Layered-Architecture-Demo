using System.ComponentModel.DataAnnotations;
using Badminton.Model.Global;
using Microsoft.AspNetCore.Http;

namespace Badminton.Model.Common;

public class ImportExcel
{
    [Required(ErrorMessage = "請選擇檔案")]
    public required IFormFile Excel { get; set; }

    [FileExtensions(Extensions = "xls,xlsx")]
    public string ExcelName => Excel?.FileName;
}