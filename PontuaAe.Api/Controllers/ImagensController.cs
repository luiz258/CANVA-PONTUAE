using System;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PontuaAe.Api.Controllers
{


    [ApiController]
    [Route("v1/empresass")]
    [AllowAnonymous]
    public class ImagensController : Controller
    {
        [HttpPost, DisableRequestSizeLimit]
        [Route("v1/imagem")]
        [AllowAnonymous]
        public JsonResult UploudImagem()
        {
            try
            {
              

                var file = Request.Form.Files[0];

                var folderName = Path.Combine("Dados/imgEmpresa");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var splitedName = fileName.Split(".");
                    string completeName = splitedName[0] + DateTime.Now.Ticks + "." + splitedName[1];
                    var fullPath = Path.Combine(pathToSave, completeName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                
                    }
                    return Json(new { data = new { name = completeName }, sucesso = true });
                }
                else
                {
                    return Json(new { sucesso = false });
                }
            }

            catch (Exception e)
            {
                return Json(new { sucesso = false });


            }
        }

        [HttpGet, DisableRequestSizeLimit]
        [Route("v1/imagem/{Logo}")]
        [AllowAnonymous]
        public FileResult BuscarImagem(string Logo)
        {
            string arquivo = Logo.Remove(0, 12);

            var folderName = Path.Combine("Dados/imgEmpresa", arquivo);
            var caminhoDaImagem = Logo;
            byte[] dadosArquivo = System.IO.File.ReadAllBytes(folderName);
            return File(dadosArquivo, System.Net.Mime.MediaTypeNames.Image.Jpeg, "nomedoarquivo.jpg");
        }
       
    }
}