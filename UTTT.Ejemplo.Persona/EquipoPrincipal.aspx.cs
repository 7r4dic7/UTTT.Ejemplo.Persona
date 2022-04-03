using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UTTT.Ejemplo.Linq.Data.Entity;
using UTTT.Ejemplo.Persona.Control;
using UTTT.Ejemplo.Persona.Control.Ctrl;

namespace UTTT.Ejemplo.Persona
{
    public partial class EquipoPrincipal : System.Web.UI.Page
    {

        private SessionManager session = new SessionManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.session = (SessionManager)this.Session["SessionManager"];
            if (this.session == null)
            {
                this.Response.Redirect("~/Login.aspx");
                return;
            }
            try
            {
                Response.Buffer = true;
                DataContext dcTemp = new DcGeneralDataContext();
            }
            catch (Exception _e)
            {
                var ex = new PersonaManager();
                ex.ExceptionMessage(_e);
                this.showMessage("Ha ocurrido un problema al cargar la página");
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                this.DataSourcePersona.RaiseViewChanged();
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al buscar");
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                this.session.Pantalla = "~/EquipoManager.aspx";
                Hashtable parametrosRagion = new Hashtable();
                parametrosRagion.Add("idPersona", "0");
                this.session.Parametros = parametrosRagion;
                this.Session["SessionManager"] = this.session;
                this.Response.Redirect(this.session.Pantalla, false);
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al agregar");
            }
        }


        protected void DataSourcePersona_Selecting(object sender, LinqDataSourceSelectEventArgs e)
        {
            try
            {
                DataContext dcConsulta = new DcGeneralDataContext();
                bool nombreBool = false;
                if (!this.txtEquipo.Text.Equals(String.Empty))
                {
                    nombreBool = true;
                }

                Expression<Func<UTTT.Ejemplo.Linq.Data.Entity.Equipo, bool>>
                    predicate =
                    (c =>
                    ((nombreBool ? (((nombreBool) ? c.strNombre.Contains(this.txtEquipo.Text.Trim()) : false)) : true)
                    ));

                predicate.Compile();

                List<UTTT.Ejemplo.Linq.Data.Entity.Equipo> listaEquipo =
                    dcConsulta.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Equipo>().Where(predicate).ToList();
                e.Result = listaEquipo;
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        protected void dgvEquipo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int idPersona = int.Parse(e.CommandArgument.ToString());
                switch (e.CommandName)
                {
                    case "Editar":
                        this.editar(idPersona);
                        break;
                    case "Eliminar":
                        this.eliminar(idPersona);
                        break;
                    case "Mantenimientos":
                        this.Mantenimientos(idPersona);
                        break;
                }
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al seleccionar");
            }
        }

        private void editar(int _idPersona)
        {
            try
            {
                Hashtable parametrosRagion = new Hashtable();
                parametrosRagion.Add("idPersona", _idPersona.ToString());
                this.session.Parametros = parametrosRagion;
                this.Session["SessionManager"] = this.session;
                this.session.Pantalla = String.Empty;
                this.session.Pantalla = "~/EquipoManager.aspx";
                this.Response.Redirect(this.session.Pantalla, false);

            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        private void eliminar(int _idPersona)
        {
            try
            {
                DataContext dcDelete = new DcGeneralDataContext();
                UTTT.Ejemplo.Linq.Data.Entity.Equipo equipo = dcDelete.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Equipo>().First(
                    c => c.id == _idPersona);
                dcDelete.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Equipo>().DeleteOnSubmit(equipo);
                dcDelete.SubmitChanges();
                this.showMessage("El registro se elimino correctamente.");
                this.DataSourcePersona.RaiseViewChanged();
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        private void Mantenimientos(int _idPersona)
        {
            try
            {
                Hashtable parametrosRagion = new Hashtable();
                parametrosRagion.Add("idPersona", _idPersona.ToString());
                this.session.Parametros = parametrosRagion;
                this.Session["SessionManager"] = this.session;
                this.session.Pantalla = String.Empty;
                this.session.Pantalla = "~/Mantenimientos.aspx";
                this.Response.Redirect(this.session.Pantalla, false);
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        public void onTxtNombreTextChange(object sender, EventArgs e)
        {
            try
            {
                this.DataSourcePersona.RaiseViewChanged();
            }
            catch (Exception _e)
            {

                this.showMessage("Ha ocurrido un problema al buscar");
            }
        }

        protected void buscarTextBox(object sender, EventArgs e)
        {
            this.DataSourcePersona.RaiseViewChanged();
        }
        protected void btnEmpleado_Click(object sender, EventArgs e)
        {
            try
            {
                this.session.Pantalla = "~/PersonaPrincipal.aspx";
                this.Session["SessionManager"] = this.session;
                this.Response.Redirect(this.session.Pantalla, false);
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un error");
            }
        }

        protected void btnCatDepartamento_Click(object sender, EventArgs e)
        {
            try
            {
                this.session.Pantalla = "~/catDepartamentos.aspx";
                this.Session["SessionManager"] = this.session;
                this.Response.Redirect(this.session.Pantalla, false);
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un error");
            }
        }

        protected void btnEquipo_Click(object sender, EventArgs e)
        {
            try
            {
                this.session.Pantalla = "~/EquipoPrincipal.aspx";
                this.Session["SessionManager"] = this.session;
                this.Response.Redirect(this.session.Pantalla, false);
            }
            catch (Exception _e)
            {

                this.showMessage("Ha ocurrido un error");
            }
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            try
            {
                this.session.Pantalla = "~/Login.aspx";
                this.Session["SessionManager"] = null;
                this.Response.Redirect(this.session.Pantalla, false);
            }
            catch (Exception)
            {
                this.showMessage("Ha ocurrido un fallo");
            }
        }
    }
}