using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GmapsProject.Model
{
    class Ciudad
    {
        private String ciudad;
        private String departamento;
        private String pais;

        public Ciudad(String ciudad, String departamento, String pais)
        {
            this.ciudad = ciudad;
            this.departamento = departamento;
            this.pais = pais;
        }

        public String getCiudadNombre()
        {
            return ciudad;
        }

        public String getDepartamento()
        {
            return departamento;
        }

        public String getPais()
        {
            return pais;
        }
    }
}
