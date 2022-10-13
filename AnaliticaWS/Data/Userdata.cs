using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Reflection.Emit;
using System.Runtime.InteropServices.ComTypes;
using System.Web.Services.Description;
using AnaliticaWS.Models;
using MySql.Data.MySqlClient;

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
                            Establecimiento = dr["INSTITUCION"].ToString(),
                            Alumno = dr["FULLNAME"].ToString(),
                            Nivel = dr["NIVEL"].ToString(),
                            Anio = dr["ANIO"].ToString(),
                            Materia = dr["MATERIA"].ToString()
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

        public static PromedioByAnio getAllPromedioAlumnoByAnio(string legajo, string anio)
        {
            PromedioByAnio PromedioAnio = new PromedioByAnio();

            List<Materia> mat = new List<Materia>();

            MySqlConnection connect = new MySqlConnection();
            connect.ConnectionString = Connection.getConnection();
            connect.Open();

            MySqlCommand cmd = new MySqlCommand("getAlumnoByAnio", connect);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("LEGAJOALUMNO", legajo);
            cmd.Parameters.AddWithValue("ANIO", anio);
            try
            {

                cmd.ExecuteNonQuery();
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())

                    {

                        mat.Add(new Materia
                        {
                            Nombre = dr["MATERIA"].ToString(),
                            Nota = dr["NOTA"].ToString()
                        });

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

        public static List<PromedioWithParameters> getPromedioWithInsitucionMateriaNivel(string idInstitucion, string idMateria, string idNivel)
        {

            List<PromedioWithParameters> prom = new List<PromedioWithParameters>();


            MySqlConnection connect = new MySqlConnection();
            connect.ConnectionString = Connection.getConnection();
            connect.Open();

            MySqlCommand cmd = new MySqlCommand("getPromedioWithInstitucionMateriaNivel", connect);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("idInstitucion", idInstitucion);
            cmd.Parameters.AddWithValue("idMateria", idMateria);
            cmd.Parameters.AddWithValue("idNivel", idNivel);
            try
            {

                cmd.ExecuteNonQuery();
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        prom.Add(new PromedioWithParameters()
                        {
                            Instituto = dr["INSTITUTO"].ToString(),
                            Materia = dr["MATERIA"].ToString(),
                            Nivel = dr["NIVEL"].ToString(),
                            Promedio = dr["PROMEDIO"].ToString()
                        });
                    }
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
                return prom;
            }
            catch (Exception ex)
            {
                return prom;
            }
        }
    }
}
