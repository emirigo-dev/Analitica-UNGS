using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Net.Http;
using System.Net;
using System.Reflection.Emit;
using System.Runtime.InteropServices.ComTypes;
using System.Web.Services.Description;
using AnaliticaWS.Models;
using MySql.Data.MySqlClient;
using System.Web.UI.WebControls;
using System.Web.Http;
using System.Text;
using System.Web.UI;
using System.Xml.Linq;

namespace AnaliticaWS.Data
{
    public class Userdata
    {

        public static List<PromedioInstitucion> getAllPromedioInstitucion()
        {

            List<PromedioInstitucion> prom = new List<PromedioInstitucion>();

            MySqlConnection connect = new MySqlConnection();
            connect.ConnectionString = Connection.getConnection();
            connect.Open();

            MySqlCommand cmd = new MySqlCommand("getPromedioInstitucion", connect);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {

                cmd.ExecuteNonQuery();
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        prom.Add(new PromedioInstitucion()
                        {
                            name = dr["NOMBRE"].ToString(),
                            average = dr["PROMEDIO"].ToString()
                        });
                    }
                }
                return prom;
            }
            catch (Exception ex)
            {
                return prom;
            }

        }

        public static List<PromedioMateria> getAllPromediosMateria()
        {

            List<PromedioMateria> prom = new List<PromedioMateria>();

            MySqlConnection connect = new MySqlConnection();
            connect.ConnectionString = Connection.getConnection();
            connect.Open();

            MySqlCommand cmd = new MySqlCommand("getPromedioMateria", connect);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {

                cmd.ExecuteNonQuery();
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        prom.Add(new PromedioMateria()
                        {
                            name = dr["NOMBRE"].ToString(),
                            average = dr["PROMEDIO"].ToString()
                        });
                    }
                }
                return prom;
            }
            catch (Exception ex)
            {
                return prom;
            }

        }


        public static List<PromedioNivel> getAllPromedioNivel()
        {

            List<PromedioNivel> prom = new List<PromedioNivel>();

            MySqlConnection connect = new MySqlConnection();
            connect.ConnectionString = Connection.getConnection();
            connect.Open();

            MySqlCommand cmd = new MySqlCommand("getPromedioNivel", connect);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {

                cmd.ExecuteNonQuery();
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        prom.Add(new PromedioNivel()
                        {
                            name = dr["NOMBRE"].ToString(),
                            average = dr["PROMEDIO"].ToString()
                        });
                    }
                }
                return prom;
            }
            catch (Exception ex)
            {
                return prom;
            }

        }

        public static List<PromedioAnualAlumno> getAllPromedioAnual(String anio) {

            List<PromedioAnualAlumno> prom = new List<PromedioAnualAlumno>();


            MySqlConnection connect = new MySqlConnection();
            connect.ConnectionString = Connection.getConnection();
            connect.Open();

            MySqlCommand cmd = new MySqlCommand("getAlumnoWithYear", connect);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("ANIO", anio);
            try
            {

        cmd.ExecuteNonQuery();
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {

                        prom.Add(new PromedioAnualAlumno()
                        {   
                            Mensaje = "OK.",
                            Jurisdiccion = dr["JURISDICCION"].ToString(),
                            Grado = dr["GRADO"].ToString(),
                            Legajo = dr["LEGAJO"].ToString(),
                            Establecimiento = dr["INSTITUCION"].ToString(),
                            Alumno = dr["FULLNAME"].ToString(),
                            Nivel = dr["NIVEL"].ToString(),
                            Anio = dr["ANIO"].ToString(),
                            Nota = dr["NOTA"].ToString(),
                            Materia = dr["MATERIA"].ToString()
                        });
                    }
                        
                    
                }
                if (prom.Count > 0) {
                    return prom;
                }
                prom.Add(new PromedioAnualAlumno(){ 
                    Mensaje = "No se encontraron datos para este año"
                });
                return prom;
            }
            catch (Exception ex)
            {
                return prom;
            }

        }

        public static PromedioPorAlumno getAllPromedioAlumnoPorLegajo(string legajo)
        {
            PromedioPorAlumno PromedioAnio = new PromedioPorAlumno();

            List<Materia> mat = new List<Materia>();

            MySqlConnection connect = new MySqlConnection();
            connect.ConnectionString = Connection.getConnection();
            connect.Open();

            MySqlCommand cmd = new MySqlCommand("getAlumnoByLegajo", connect);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("LEGAJOALUMNO", legajo);
            try
            {

                cmd.ExecuteNonQuery();
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())

                    {

                        mat.Add(new Materia
                        {
                            Anio = dr["ANIO"].ToString(),
                            Grado = dr["GRADO"].ToString(),
                            Nivel = dr["NIVEL"].ToString(),
                            Nombre = dr["MATERIA"].ToString(),
                            Nota = dr["NOTA"].ToString()
                        });
                        PromedioAnio.Legajo = dr["LEGAJO"].ToString();
                        PromedioAnio.Jurisdiccion = dr["JURISDICCION"].ToString();
                        PromedioAnio.Institucion = dr["INSTITUCION"].ToString();
                        PromedioAnio.Nombre = dr["FULLNAME"].ToString();
                    }
                    PromedioAnio.Materias = mat;
                    



                }
                if (mat.Count > 0)
                {
                    PromedioAnio.Mensaje = "OK.";
                }
                else
                {
                    PromedioAnio.Mensaje = "No se encontraron notas para los parametros solicitados";
                }
                return PromedioAnio;
            }
            catch (Exception ex)
            {
                return PromedioAnio;
            }

        }


        public static MedicionMessage insertarMedicionesDeSensores(string idSensor, string valorMedicion, DateTime horario)
        {


            MedicionMessage message = new MedicionMessage();

            string format = "yyyy-MM-dd HH:mm:ss";
            string insert = horario.ToString(format);

            if (idSensor == null || idSensor == "")
            {
                message.StatusCode = 404;
                message.Message = "El idSensor debe tener un valor";
                message.HasError = true;
                return message;
            }

            if (valorMedicion == null || valorMedicion == "")
            {
                message.StatusCode = 404;
                message.Message = "El valor de medicion debe tener un valor";
                message.HasError = true;
                return message;
            }

            if (insert == "0001-01-01 00:00:00" || valorMedicion == "")
            {
                message.StatusCode = 404;
                message.Message = "El horario debe tener un valor";
                message.HasError = true;
                return message;
            }

            MySqlConnection connect = new MySqlConnection();
            connect.ConnectionString = Connection.getConnection();
            connect.Open();

            MySqlCommand cmd = new MySqlCommand("insertarMedicionesSensores", connect);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("idSensor", idSensor);
            cmd.Parameters.AddWithValue("horario", insert);
            cmd.Parameters.AddWithValue("valorMedicion", valorMedicion);
            try
            {

                cmd.ExecuteNonQuery();
                message.StatusCode = 200;
                message.Message = "Medicion insertada";
                message.HasError = false;
                return message;
            }
            catch (Exception ex)
            {
                
                message.StatusCode = 404;
                message.Message = ex.Message;
                message.HasError = true;
                return message;
            }
        }



        public static PromedioWithParameters getPromedioWithInsitucionMateriaNivel(string idInstitucion, string idMateria, string idNivel, string idGrado, string anio)
        {

            List<PromedioWithParameters> prom = new List<PromedioWithParameters>();
            NotasConceptuales nota = new NotasConceptuales();


            MySqlConnection connect = new MySqlConnection();
            connect.ConnectionString = Connection.getConnection();
            connect.Open();

            MySqlCommand cmd = new MySqlCommand("getPromedioWithInstitucionMateriaNivel", connect);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("idInstitucion", idInstitucion);
            cmd.Parameters.AddWithValue("idMateria", idMateria);
            cmd.Parameters.AddWithValue("idNivel", idNivel);
            cmd.Parameters.AddWithValue("idGrado", idGrado);
            cmd.Parameters.AddWithValue("anio", anio);


            try
            {

                cmd.ExecuteNonQuery();
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        if (dr["CONCEPTO"].ToString() == "Supera")
                        {
                            nota.Supera = dr["Porcentaje"].ToString();
                        }
                        else if (dr["CONCEPTO"].ToString() == "MUY BIEN")
                        {
                            nota.MBien = dr["Porcentaje"].ToString();
                        }
                        else if (dr["CONCEPTO"].ToString() == "BIEN")
                        {
                            nota.Bien = dr["Porcentaje"].ToString();
                        }
                        else if (dr["CONCEPTO"].ToString() == "INSUFICIENTE") {
                            nota.Insuficiente = dr["Porcentaje"].ToString();

                        }


                    }

                    prom.Add(new PromedioWithParameters()
                    {

                        Conceptual = dr["CONCEPTUAL"].ToString() == "0" ? false : true,
                        Instituto = dr["INSTITUTO"].ToString(),
                        Materia = dr["MATERIA"].ToString(),
                        Nivel = dr["NIVEL"].ToString(),
                        Grado = dr["GRADO"].ToString(),
                        Anio = dr["ANIO_CURSADO"].ToString(),
                        Jurisdiccion = dr["JURISDICCION"].ToString(),
                        Promedio = dr["PROMEDIO"].ToString(),
                        Notas = nota,
                       
                    });
                }
                if (prom.Count != 0)
                {
                    prom[0].Mensaje = "Ok.";
                }
                else {
                    prom.Add(new PromedioWithParameters()
                    {
                        Mensaje = "No se encontro notas para los parametros indicados",

                    });
                }
                return prom[0];
            }
            catch (Exception ex)
            {
                prom.Add(new PromedioWithParameters()
                {
                    Mensaje = "No se encontro notas para los parametros indicados",

                });
                return prom[0];
            }
        }

        public static Boolean BulkToUpdateAsistenciaMySQL(List<Asistencia> asistencias) {

            try
            {
                MySqlConnection connect = new MySqlConnection();
                connect.ConnectionString = Connection.getConnection();
                StringBuilder sCommand = new StringBuilder("REPLACE INTO cursada_alumno(ID, LEGAJO_ALUMNO, ID_CURSADA, ASISTENCIA) VALUES ");
                using (MySqlConnection mConnection = new MySqlConnection(connect.ConnectionString))
                {
                    List<string> Rows = new List<string>();
                    foreach (Asistencia asis in asistencias)
                    {
                        Rows.Add(string.Format("('{0}','{1}','{2}','{3}')", MySqlHelper.EscapeString(asis.id), MySqlHelper.EscapeString(asis.legajoAlumno), MySqlHelper.EscapeString(asis.idCursada), MySqlHelper.EscapeString(asis.valor)));
                    }
                    sCommand.Append(string.Join(",", Rows));
                    sCommand.Append(";");
                    mConnection.Open();
                    using (MySqlCommand myCmd = new MySqlCommand(sCommand.ToString(), mConnection))
                    {

                        myCmd.CommandType = CommandType.Text;
                        myCmd.ExecuteNonQuery();
                    }

                }

                return false;

            }
            catch (Exception e)
            {
                return true;
            }

        }

        public static Boolean BulkToMySQL(List<NotasDTO> notas)
        {
            try {
                MySqlConnection connect = new MySqlConnection();
                connect.ConnectionString = Connection.getConnection();
                StringBuilder sCommand = new StringBuilder("INSERT IGNORE INTO nota (LEGAJO_ALUMNO, ID_CURSADA, VALOR) VALUES ");
                using (MySqlConnection mConnection = new MySqlConnection(connect.ConnectionString))
                {
                    List<string> Rows = new List<string>();
                    foreach (NotasDTO nota in notas)
                    {
                        Rows.Add(string.Format("('{0}','{1}', '{2}')", MySqlHelper.EscapeString(nota.legajoAlumno), MySqlHelper.EscapeString(nota.idCursada), MySqlHelper.EscapeString(nota.nota)));
                    }
                    sCommand.Append(string.Join(",", Rows));
                    sCommand.Append(";");
                    mConnection.Open();
                    using (MySqlCommand myCmd = new MySqlCommand(sCommand.ToString(), mConnection))
                    {
                         
                        myCmd.CommandType = CommandType.Text;
                        myCmd.ExecuteNonQuery();
                    }

                    /*MySqlCommand command2 = new MySqlCommand("SHOW WARNINGS", mConnection);
                    using (MySqlDataReader reader = command2.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            String level = reader["Level"].ToString();
                            String code = reader["Code"].ToString();
                            String message = reader["Message"].ToString();
                        }
                    }
                    */
                }

                return false;

            } catch (Exception e){
                return true;
            }
         
        }


    }
}
