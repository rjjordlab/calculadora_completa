using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CalculadoraCompleta.Controllers
{
    public class HomeController : Controller
    {

        // GET: Home
        [HttpGet] //anotacao opcional, por defeito e GET:Home
        public ActionResult Index()
        {
            //inicializar o valor do visor
            ViewBag.Visor = 0;
            return View();
        }

        int operandoaux = 0;
        int operandoactual = 0;
        string operador = "";
        // POST: Home
        [HttpPost]
        public ActionResult Index(string bt, string visor)
        {
            int resultado = 0;
            //identificar o valor da variavel "bt"
            switch (bt)
            {
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    if (visor.Length <= 12)
                    {
                        if (visor.Equals("0")) visor = bt;
                        else visor += bt;
                    }
                    break;
                case "0":
                    if (visor.Length <= 12)
                    {
                        if (visor.IndexOf(",") == (1)) //se tiver virgula...
                        {
                            visor += bt;  //verificar se contem virgula(IndexOf se nao existir ',' tem '-1' como valor, e '1' se tiver) 
                        }
                        else
                        {
                            if (visor.Equals("0")) //se o display estiver a zero...
                            {
                                visor = "0";
                            }
                            else visor += bt;           //escrever um zero no display...
                        }
                    }
                    break;
                //-------------------------------------------------------------
                case "/":
                case "*":
                case "-":
                case "+":
                    operandoaux = Convert.ToInt32(visor);      //ao carregar num operador, guarda o valor do display numa variavel, metendo o display a zero
                    operador = bt;                              //guardar o operador
                    visor = "0";
                    break;
                case "=":
                    operandoactual = Convert.ToInt32(visor);
                    switch (operador)
                    {
                        case "+": resultado = operandoaux + operandoactual; break; //soma
                        case "-": resultado = operandoaux - operandoactual; break; //sub
                        case "x": resultado = operandoaux * operandoactual; break; //mult
                        case ":": resultado = operandoaux / operandoactual; break; //div
                    }
                    visor = resultado.ToString();
                    break;
                //--------------------------------------------------------------
                case "C":
                    visor = "0";
                    break;
                case "+/-":
                    if (visor.Length <= 12)
                    {
                        if (visor != ("0"))
                        {
                            if (visor[0].ToString() != ("-")) visor = "-" + visor;
                            else visor = visor.Substring(1);
                            break;
                        }
                    }
                    break;
                case ",":
                    if (visor.IndexOf(",") == (-1))  //verificar se contem virgula(IndexOf se nao existir ',' tem '-1' como valor, e '1' se tiver)
                        visor += ",";
                    break;
            }
            //enviar resposta para cliente
            ViewBag.Visor = visor;



            return View();
        }
    }
}