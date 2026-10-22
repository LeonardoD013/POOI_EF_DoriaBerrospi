using POOI_EF_DoriaBerrospi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POOI_EF_DoriaBerrospi.Controllers
{
    public class ConsumidorController : Controller
    {
        private string cadenaConexion = ConfigurationManager.ConnectionStrings["conexionADatos"].ConnectionString;

        IEnumerable<Consumidor> listarConsumidor()
        {
            List<Consumidor> listaConsumidor = new List<Consumidor>();
            SqlConnection sqlConnection = new SqlConnection(cadenaConexion);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("usp_listar_consumidor", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                listaConsumidor.Add(
                    new Consumidor
                    {
                        dnicont = sqlDataReader.GetString(0),
                        nomcont = sqlDataReader.GetString(1),
                        apecont = sqlDataReader.GetString(2),
                        emailcont = sqlDataReader.GetString(3),
                        idpais = sqlDataReader.GetString(4),
                    });
            }
            return listaConsumidor;
        }

        string agregarConsumidor(Consumidor consumidor)
        {
            SqlConnection sqlConnection = new SqlConnection(cadenaConexion);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("usp_agrega_consumidor", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@dnicont", consumidor.dnicont);
            sqlCommand.Parameters.AddWithValue("@nomcont", consumidor.nomcont);
            sqlCommand.Parameters.AddWithValue("@apecont", consumidor.apecont);
            sqlCommand.Parameters.AddWithValue("@emailcont", consumidor.emailcont);
            sqlCommand.Parameters.AddWithValue("@idpais", consumidor.idpais);
            int cantidadFilas = sqlCommand.ExecuteNonQuery();

            return cantidadFilas > 0 ? "Consumidor Guardado" : "Error";
        }

        string actualizarConsumidor(Consumidor consumidor)
        {
            SqlConnection sqlConnection = new SqlConnection(cadenaConexion);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("usp_actualizar_consumidor", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@dnicont", consumidor.dnicont);
            sqlCommand.Parameters.AddWithValue("@nomcont", consumidor.nomcont);
            sqlCommand.Parameters.AddWithValue("@apecont", consumidor.apecont);
            sqlCommand.Parameters.AddWithValue("@emailcont", consumidor.emailcont);
            sqlCommand.Parameters.AddWithValue("@idpais", consumidor.idpais);
            int cantidadFilas = sqlCommand.ExecuteNonQuery();

            return cantidadFilas > 0 ? "Consumidor Actualizado" : "Error";
        }

        Consumidor ObtenerConsumidor(string dni)
        {
            SqlConnection sqlConnection = new SqlConnection(cadenaConexion);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("usp_obtener_consumidor", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@dnicont", dni);
            
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                return new Consumidor
                {
                    dnicont = sqlDataReader.GetString(0),
                    nomcont = sqlDataReader.GetString(1),
                    apecont = sqlDataReader.GetString(2),
                    emailcont = sqlDataReader.GetString(3),
                    idpais = sqlDataReader.GetString(4),
                };
            };

            return null;
        }

        int EliminarConsumidor(string dni)
        {
            SqlConnection sqlConnection = new SqlConnection(cadenaConexion);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("usp_eliminar_consumidor", sqlConnection);
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@dnicont", dni);

            return sqlCommand.ExecuteNonQuery();
        }

        public ActionResult Index()
        {
            return View(listarConsumidor());
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Consumidor consumidor)
        {
            ViewBag.Mensaje = agregarConsumidor(consumidor);
            return View(consumidor);
        }

        public ActionResult Details(string dni)
        {
            Consumidor consumidorEncontrado = ObtenerConsumidor(dni);
            return View(consumidorEncontrado);
        }

        public ActionResult Edit(string dni)
        {
            Consumidor consumidorEncontrado = ObtenerConsumidor(dni);
            return View(consumidorEncontrado);
        }

        [HttpPost]
        public ActionResult Edit(Consumidor consumidor)
        {
            ViewBag.Mensaje = actualizarConsumidor(consumidor);
            return View(consumidor);
        }

        public ActionResult Delete(string dni)
        {
            Consumidor consumidorEncontrado = ObtenerConsumidor(dni);
            return View(consumidorEncontrado);
        }

        [HttpPost]
        public ActionResult Delete(Consumidor consumidor)
        {
            int cantidadCambio = (EliminarConsumidor(consumidor.dnicont));
            if (cantidadCambio > 0)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");

        }
    }
}