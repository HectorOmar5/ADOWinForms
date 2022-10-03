using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADOWinForms.Entidades;
using System.Threading.Tasks;

namespace ADOWinForms.ADO
{
    internal interface ICRUD
    {
        void Eliminar(int idEstatus);
        void Actualizar(int idEstatus, string nombreEstatus, String Clave);
        void Agregar(string Nombre, string Clave);
        EstatusAlumno ConsultarUno(int idEstatus);
        List<EstatusAlumno> ObtenerTodos();
    }
}
