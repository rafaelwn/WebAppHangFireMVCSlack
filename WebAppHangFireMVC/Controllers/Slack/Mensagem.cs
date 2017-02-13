using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace WebAppHangFireMVC.Controllers.Slack
{
    public class Mensagem
    {
        public static void stpEnvia_Mensagem_Slack(String Ds_Canal, String Ds_Mensagem)
        {

            const string slackToken = "xxxxx";
            const string usuario = "usuario";

            try
            {

                var mensagem = Utils.converteStringWebService(Ds_Mensagem + " - " + DateTime.Now.ToShortTimeString());

                if (mensagem.Length > 4000)
                    throw new ArgumentException("O tamanho máximo da mensagem deve ser inferior a 4.000 caracteres");

                var canal = Ds_Canal;
                var canais = canal.Split(';');

                foreach (var nomeCanal in canais)
                {

                    var request = (HttpWebRequest)WebRequest.Create("https://slack.com/api/chat.postMessage");

                    request.Method = "POST";

                    request.UserAgent = "curl/7.45.0";
                    request.ContentType = "application/x-www-form-urlencoded";

                    var parametros = $"token={slackToken}&channel={nomeCanal.Trim()}&text={mensagem}&username={usuario}&as_user=false";

                    var buffer = Encoding.GetEncoding("UTF-8").GetBytes(parametros);
                    using (var reqstr = request.GetRequestStream())
                    {

                        reqstr.Write(buffer, 0, buffer.Length);

                        using (var response = request.GetResponse())
                        {

                            using (var dataStream = response.GetResponseStream())
                            {

                                if (dataStream == null) return;

                                using (var reader = new StreamReader(dataStream))
                                {
                                    var responseFromServer = reader.ReadToEnd();

                                    if (responseFromServer.Contains("\"ok\":false"))
                                        throw new ArgumentException(responseFromServer);
                                    else
                                        throw new ArgumentException(responseFromServer);

                                }
                            }

                        }

                    }

                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Erro : " + e.Message);
            }
        }
    }
}
