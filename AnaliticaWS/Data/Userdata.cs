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
using System.Runtime.Remoting.Contexts;

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
                connect.Close();
                return prom;
            }
            catch (Exception ex)
            {
                connect.Close();
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
                connect.Close();
                return prom;
            }
            catch (Exception ex)
            {
                connect.Close();
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
                connect.Close();
                return prom;
            }
            catch (Exception ex)
            {
                connect.Close();
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
                connect.Close();
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
                connect.Close();
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


                    connect.Close();


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
                connect.Close();
                return PromedioAnio;
            }

        }


        public static Errors deleteSensor(int id)
        {
            Errors err = new Errors();

            MySqlConnection connect = new MySqlConnection();
            connect.ConnectionString = Connection.getConnection();
            connect.Open();

            MySqlCommand cmd = new MySqlCommand("deleteSensor", connect);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("idSensor", id);
            try
            {
                cmd.ExecuteNonQuery();
                err.statusCode = 200;
                err.message = "Sensor eliminado";
                err.hasError = false;
                connect.Close();
                return err;
            }
            catch (Exception ex)
            {
                err.statusCode = 404;
                err.message = ex.Message;
                err.hasError = true;
                connect.Close();
                return err;
            }


        }
        public static SensorABM insertOrUpdateSensors(string idSensor, string tipoMedicion, string idArea) {

            SensorABM sensor = new SensorABM();


            if (idSensor == null || idSensor == "") {
                sensor.statusCode = 404;
                sensor.message = "El idSensor debe tener un valor";
                sensor.hasError = true;
                return sensor;
            }
            if (tipoMedicion == null || tipoMedicion == "")
            {
                sensor.statusCode = 404;
                sensor.message = "El tipoMedicion debe tener un valor";
                sensor.hasError = true;
                return sensor;
            }
            if (idArea == null || idArea == "")
            {
                sensor.statusCode = 404;
                sensor.message = "El idArea debe tener un valor";
                sensor.hasError = true;
                return sensor;
            }

            MySqlConnection connect = new MySqlConnection();
            connect.ConnectionString = Connection.getConnection();
            connect.Open();

            MySqlCommand cmd = new MySqlCommand("insertOrUpdateSensors", connect);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("idSensor", idSensor);
            cmd.Parameters.AddWithValue("tipoMedicion", tipoMedicion);
            cmd.Parameters.AddWithValue("idArea", idArea);
            try
            {

                cmd.ExecuteNonQuery();
                sensor.statusCode = 200;
                sensor.message = "Sensor insertada/actualizada";
                sensor.hasError = false;
                connect.Close();
                return sensor;
            }
            catch (Exception ex)
            {

                sensor.statusCode = 404;
                sensor.message = ex.Message;
                sensor.hasError = true;
                connect.Close();
                return sensor;
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
                connect.Close();
                return message;
            }
            catch (Exception ex)
            {
                
                message.StatusCode = 404;
                message.Message = ex.Message;
                message.HasError = true;
                connect.Close();
                return message;
            }
        }


        public static SensorABM insertOrUpdateDevice(string idDispositivo, string tipoDispositivo, string nombreDispositivo, string idArea)
        {

            SensorABM sensor = new SensorABM();


            if (idDispositivo == null || idDispositivo == "")
            {
                sensor.statusCode = 404;
                sensor.message = "El idDispositivo debe tener un valor";
                sensor.hasError = true;
                return sensor;
            }
            if (tipoDispositivo == null || tipoDispositivo == "")
            {
                sensor.statusCode = 404;
                sensor.message = "El tipoDispositivo debe tener un valor";
                sensor.hasError = true;
                return sensor;
            }
            if (nombreDispositivo == null || nombreDispositivo == "")
            {
                sensor.statusCode = 404;
                sensor.message = "El nombreDispositivo debe tener un valor";
                sensor.hasError = true;
                return sensor;
            }
            if (idArea == null || idArea == "")
            {
                sensor.statusCode = 404;
                sensor.message = "El idArea debe tener un valor";
                sensor.hasError = true;
                return sensor;
            }

            MySqlConnection connect = new MySqlConnection();
            connect.ConnectionString = Connection.getConnection();
            connect.Open();

            MySqlCommand cmd = new MySqlCommand("insertOrUpdateDevices", connect);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("idDispositivo", idDispositivo);
            cmd.Parameters.AddWithValue("tipoDispositivo", tipoDispositivo);
            cmd.Parameters.AddWithValue("nombreDispositivo", nombreDispositivo);
            cmd.Parameters.AddWithValue("idArea", idArea);
            try
            {

                cmd.ExecuteNonQuery();
                sensor.statusCode = 200;
                sensor.message = "Dispositivo insertado/actualizado";
                sensor.hasError = false;
                connect.Close();
                return sensor;
            }
            catch (Exception ex)
            {

                sensor.statusCode = 404;
                sensor.message = ex.Message;
                sensor.hasError = true;
                connect.Close();
                return sensor;
            }

        }


        public static Errors deleteDevice(int id)
        {
            Errors err = new Errors();

            MySqlConnection connect = new MySqlConnection();
            connect.ConnectionString = Connection.getConnection();
            connect.Open();

            MySqlCommand cmd = new MySqlCommand("deleteDevice", connect);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("idDevice", id);
            try
            {
                cmd.ExecuteNonQuery();
                err.statusCode = 200;
                err.message = "Dispositivo eliminado";
                err.hasError = false;
                connect.Close();
                return err;
            }
            catch (Exception ex)
            {
                err.statusCode = 404;
                err.message = ex.Message;
                err.hasError = true;
                connect.Close();
                return err;
            }


        }


        public static List<promediosSensores> getMedicionesPorDia()
        {

            List<promediosSensores> promediosSensores = new List<promediosSensores>();
            MySqlConnection connect = new MySqlConnection();
            connect.ConnectionString = Connection.getConnection();
            connect.Open();

            MySqlCommand cmd = new MySqlCommand("promedioMedicionPorDia", connect);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {

                cmd.ExecuteNonQuery();
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        promediosSensores.Add(new promediosSensores()
                        {

                            IdSensor = dr["Sensor"].ToString(),
                            NombreSensor = dr["Nombre"].ToString(),
                            IdArea = dr["Area"].ToString(),
                            NombreArea = dr["NombreArea"].ToString(),
                            Institucion = dr["NombreInstitucion"].ToString(),
                            Jurisdiccion = dr["Jurisdiccion"].ToString(),
                            TipoMedicion = dr["TipoMedicion"].ToString(),
                            UnidadDeMedida = dr["UnidadMedida"].ToString(),
                            FechaYHoraEnvio = DateTime.Now.ToString("yyyy-MM-dd")

                    }) ;


                    }


                }

                connect.Close();
                return promediosSensores;
            }
            catch (Exception ex)
            {
                connect.Close();
                return promediosSensores;
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
                connect.Close();
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
                connect.Close();
                prom.Add(new PromedioWithParameters()
                {
                    Mensaje = "No se encontro notas para los parametros indicados",

                });
                return prom[0];
            }
        }

        public static MedicionPortal getConsumoPortal(string institucion, string tipo, string tiempo) {

            MedicionPortal mp = new MedicionPortal();


            MySqlConnection connect = new MySqlConnection();
            connect.ConnectionString = Connection.getConnection();
            connect.Open();

            MySqlCommand cmd = new MySqlCommand("getMediciones", connect);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("INSTITUCION", institucion);
            cmd.Parameters.AddWithValue("TIPO", tipo);
            cmd.Parameters.AddWithValue("TIEMPO", tiempo);
            try
            {

                cmd.ExecuteNonQuery();
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read()) { 
                        mp.TipoMedida = dr["TipoMedida"].ToString();
                        mp.UnidadMedida = dr["UnidadMedida"].ToString();
                        mp.Promedio = dr["Promedio"].ToString();
                    }
                }

                mp.statusCode = 200;
                mp.message = "Ok";
                mp.hasError = false;
                connect.Close();
                return mp;

            }
            catch (Exception ex)
            {

                mp.statusCode = 404;
                mp.message = ex.Message;
                mp.hasError = true;
                connect.Close();
                return mp;
            }

        }

        public static ConsumoBoletinError getConsumosMensaules(string institucion, string mes, string anio) {

            ConsumoBoletinError consumoData = new ConsumoBoletinError();
            consumoData.consumos = new List<ConsumoBoletin>();


            if (institucion == null || institucion == "")
            {
                consumoData.statusCode = 404;
                consumoData.message = "La institucion debe tener un valor";
                consumoData.hasError = true;
                return consumoData;
            }
            if (mes == null || mes == "")
            {
                consumoData.statusCode = 404;
                consumoData.message = "El mes debe tener un valor";
                consumoData.hasError = true;
                return consumoData;
            }
            if (anio == null || anio == "")
            {
                consumoData.statusCode = 404;
                consumoData.message = "El año debe tener un valor";
                consumoData.hasError = true;
                return consumoData;
            }
            
            MySqlConnection connect = new MySqlConnection();
            connect.ConnectionString = Connection.getConnection();
            connect.Open();

            MySqlCommand cmd = new MySqlCommand("getConsumoEnergeticoMensual", connect);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("INSTITUCION", institucion);
            cmd.Parameters.AddWithValue("MES", mes);
            cmd.Parameters.AddWithValue("ANIO", anio);
            try
            {

                cmd.ExecuteNonQuery();
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        consumoData.consumos.Add(new ConsumoBoletin()
                        {
                            IdDispositivo = dr["IDDispositivo"].ToString(),
                            nombreDispositivo = dr["Nombre"].ToString(),
                            promedio = dr["Promedio"].ToString(),
                            nombreArea = dr["NombreArea"].ToString(),
                            nombreInstitucion = dr["NombreInstitucion"].ToString(),
                            tipoDispositivo = dr["TipoDispositivo"].ToString()
                        });

                    }
                }

                consumoData.statusCode = 200;
                consumoData.message = "Ok";
                consumoData.hasError = false;
                connect.Close();
                return consumoData;

            }
            catch (Exception ex)
            {

                consumoData.statusCode = 404;
                consumoData.message = ex.Message;
                consumoData.hasError = true;
                connect.Close();
                return consumoData;
            }

        }
        public static void insertLog(Log alog) {
            MySqlConnection connect = new MySqlConnection();
            connect.ConnectionString = Connection.getConnection();

            try
            {
               
                StringBuilder sCommand = new StringBuilder("INSERT IGNORE INTO log (PROCESO, ESTADO, FECHAHORA) VALUES ");
                using (MySqlConnection mConnection = new MySqlConnection(connect.ConnectionString))


                {
                    List<string> Rows = new List<string>();
                    Rows.Add(string.Format("('{0}','{1}','{2}')", MySqlHelper.EscapeString(alog.proceso.ToString()), MySqlHelper.EscapeString(alog.estado.ToString()), MySqlHelper.EscapeString(alog.fecha.ToString("yyyy-MM-dd HH:mm:ss"))));
                    sCommand.Append(string.Join(",", Rows));
                    sCommand.Append(";");
                    mConnection.Open();
                    using (MySqlCommand myCmd = new MySqlCommand(sCommand.ToString(), mConnection))
                    {

                        myCmd.CommandType = CommandType.Text;
                        myCmd.ExecuteNonQuery();
                    }

                }
                connect.Close();
            }
            catch (Exception e)
            {
                connect.Close();
            }

        }
        public static void insertarConsumos(List<Consumo> consumos) {
            MySqlConnection connect = new MySqlConnection();
            connect.ConnectionString = Connection.getConnection();
            try
            {
               

                StringBuilder sCommand = new StringBuilder("INSERT IGNORE INTO consumo (ID_DISPOSITIVO, VALOR, FECHA) VALUES ");
                using (MySqlConnection mConnection = new MySqlConnection(connect.ConnectionString))


                {
                    List<string> Rows = new List<string>();

                    foreach (Consumo cons in consumos)
                    {
                        Rows.Add(string.Format("('{0}','{1}','{2}')", MySqlHelper.EscapeString(cons.id.ToString()), MySqlHelper.EscapeString(cons.consumo.ToString()), MySqlHelper.EscapeString(cons.fecha.ToString("yyyy-MM-dd H:mm:ss"))));
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
                connect.Close();
            }
            catch (Exception e)
            {
                connect.Close();
            }
        }
        public static Boolean BulkToUpdateAsistenciaMySQL(List<Asistencia> asistencias) {


            MySqlConnection connect = new MySqlConnection();
            connect.ConnectionString = Connection.getConnection();

            try
            {
               
                
                StringBuilder sCommand = new StringBuilder("INSERT IGNORE INTO cursada_alumno (ID, LEGAJO_ALUMNO, ID_CURSADA, ASISTENCIA) VALUES ");
                using (MySqlConnection mConnection = new MySqlConnection(connect.ConnectionString))


                {
                    List<string> Rows = new List<string>();
                    foreach (Asistencia asis in asistencias)
                    {
                        Rows.Add(string.Format("('{0}','{1}','{2}','{3}')", MySqlHelper.EscapeString(asis.id), MySqlHelper.EscapeString(asis.legajoAlumno), MySqlHelper.EscapeString(asis.idCursada), MySqlHelper.EscapeString(asis.valor)));
                    }
                    sCommand.Append(string.Join(",", Rows));
                    sCommand.Append(" ON DUPLICATE KEY UPDATE ASISTENCIA = VALUES(ASISTENCIA)");
                    sCommand.Append(";");
                    var hol = sCommand;
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

                connect.Close();
                return false;

            }
            catch (Exception e)
            {
                connect.Close();
                return true;
            }

        }

        public static Boolean BulkToMySQL(List<NotasDTO> notas)
        {
            MySqlConnection connect = new MySqlConnection();
            connect.ConnectionString = Connection.getConnection();

            try {
               
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

                   
                }

                connect.Close();
                return false;

            } catch (Exception e){
                connect.Close();
                return true;
            }
         
        }


        public class Errors
        {
            public int statusCode { get; set; }
            public string message { get; set; }
            public Boolean hasError { get; set; }

        }

    }

   
}
