using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppHangFireMVC.Controllers.Slack
{
    public static class Utils
    {
        public static string converteStringWebService(string Ds_Texto)
        {

            // Recomendação do site do Slack
            var retorno = Ds_Texto.Replace("<", "&lt;").Replace("&", "&amp;").Replace(">", "&gt;");

            // Quebra de linha
            retorno = retorno.Replace("\\n", "\n");

            // Tratamento do caractere "\"    
            retorno = retorno.Replace(@"\", @"\\");

            // Tratamento de Aspas duplas
            retorno = retorno.Replace(@"""", @"\""");

            // Resultado final
            return Uri.EscapeDataString(retorno);
        }
    }
}