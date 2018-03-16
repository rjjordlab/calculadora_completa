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
            //inicializar variaveis de formatacao da calculadora
            Session["primeirooperador"] = true;
            //marcar o visor para limpeza
            Session["limpezaVisor"] = true;
            return View();
        }

        // POST: Home
        [HttpPost]
        public ActionResult Index(string bt, string visor)
        {
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
                case "0":
                    if (visor.Length <= 12)
                    {
                        if ((bool)Session["limpezaVisor"] || visor.Equals("0")) visor = bt;
                        else visor += bt;
                    }
                    //impedir limpeza do visor/display
                    Session["limpezaVisor"] = false;
                    break;
                //-------------------------------------------------------------
                case ":":
                case "x":
                case "-":
                case "+":
                    //executo este codigo, porque e a primeira vez da escolha do operador
                    if (!(bool)Session["primeirooperador"]) // se nao for a primeira vez que escolho um operador...
                    {
                        //variaveis auxiliares
                        double operando1 = Convert.ToDouble((string)Session["operando"]);
                        double operando2 = Convert.ToDouble(visor);

                        switch (Session["operadoranterior"]) //se der bug... usar cast-> (string)Session["operadoranterior"]
                        {
                            case "+":
                                visor = operando1 + operando2 + "";
                                break;
                            case "-":
                                visor = operando1 - operando2 + "";
                                break;
                            case "x":
                                visor = operando1 * operando2 + "";
                                break;
                            case ":":
                                visor = operando1 / operando2 + "";
                                break;
                        }

                        //guardar dados para a operacao seguinte
                        Session["operadoranterior"] = bt;
                        Session["primeirooperador"] = false;
                        Session["operando"] = visor;
                        Session["limpezaVisor"] = true;        //marcar o visor para limpeza

                        //tratar da situacao do igual("=")
                        if (bt.Equals("="))
                        {//se o botao for o igual...
                            Session["primeirooperador"] = true;
                        }
                        break;
                    }
                    break;
                //--------------------------------------------------------------
                case "C":
                    visor = "0"; //limpar visor
                    //reiniciar variaveis
                    //inicializar variaveis de formatacao da calculadora
                    Session["primeirooperador"] = true;
                    //marcar o visor para limpeza
                    Session["limpezaVisor"] = true;
                    break;
                case "+/-":
                    visor = Convert.ToDouble(visor) * -1 + "";    //converte para double e depois converte para string no final
                    break;
                    
                case ",":
                    if(!visor.Contains(","))  //se nao tiver virgula...
                      visor += ";";
                    break;
            }
            //enviar resposta para cliente
            ViewBag.Visor = visor;
            return View();
        }
    }
}