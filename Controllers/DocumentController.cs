using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using doCloud.Models;

namespace doCloud.Controllers
{
    [Route("api/[controller]")]
    public class DocumentController : Controller
    {
        string connString = Startup.connectionString ;

        // Obtiene una lista de documentos por documento tipo (carpeta)
        [HttpGet("{id:int}")]
        public List<Document> GetById(int id)
        {
            List<Document> DocLst = new List<Document>();


            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Retrieve all rows
                using (var cmd = new NpgsqlCommand("SELECT * from dar_documento Where ns_documento_tipo = :ns_documento_tipo", conn))
                {
                    cmd.Parameters.Add(new NpgsqlParameter("ns_documento_tipo", id));

                    using (var dr = cmd.ExecuteReader())
                        while (dr.Read())
                        {
                            Document doc = new Document();

                            doc.idns_documento = dr.IsDBNull(dr.GetOrdinal("IDNS_DOCUMENTO")) ? 0 : dr.GetInt32(dr.GetOrdinal("IDNS_DOCUMENTO"));
                            doc.h_fecha = dr.IsDBNull(dr.GetOrdinal("H_FECHA")) ? DateTime.Now : dr.GetDateTime(dr.GetOrdinal("H_FECHA"));
                            doc.ns_documento_fs = dr.IsDBNull(dr.GetOrdinal("NS_DOCUMENTO_TIPO")) ? 0 : dr.GetInt32(dr.GetOrdinal("NS_DOCUMENTO_TIPO"));
                            doc.ns_flow = dr.IsDBNull(dr.GetOrdinal("NS_FLOW")) ? 0 : dr.GetInt32(dr.GetOrdinal("NS_FLOW"));
                            doc.ns_usuario_carga = dr.IsDBNull(dr.GetOrdinal("NS_USUARIO_CARGA")) ? 0 : dr.GetInt32(dr.GetOrdinal("NS_USUARIO_CARGA"));
                            doc.sd_metadata = dr.IsDBNull(dr.GetOrdinal("SD_METADATA")) ? "" : dr.GetString(dr.GetOrdinal("SD_METADATA"));
                            doc.ns_documento_fs = dr.IsDBNull(dr.GetOrdinal("NS_DOCUMENTO_FS")) ? 0 : dr.GetInt32(dr.GetOrdinal("NS_DOCUMENTO_FS"));
                            doc.ns_documento_subtipo = dr.IsDBNull(dr.GetOrdinal("NS_DOCUMENTO_SUBTIPO")) ? 0 : dr.GetInt32(dr.GetOrdinal("NS_DOCUMENTO_SUBTIPO"));
                            doc.sd_nulo = dr.IsDBNull(dr.GetOrdinal("SD_NULO")) ? "" : dr.GetString(dr.GetOrdinal("SD_NULO"));

                            DocLst.Add(doc);
                        }
                }           
            }

            return DocLst;
        }
    }
}