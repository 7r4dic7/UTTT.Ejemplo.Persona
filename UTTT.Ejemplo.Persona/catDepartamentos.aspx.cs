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
    public partial class catDepartamentos : System.Web.UI.Page
    {

        private SessionManager session = new SessionManager();
        private int idPersona = 0;
        private UTTT.Ejemplo.Linq.Data.Entity.catDepartamento baseEntity;
        private DataContext dcGlobal = new DcGeneralDataContext();
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
                this.session.Pantalla = "~/DepartamentosManager.aspx";
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
                if (!this.txtValor.Text.Equals(String.Empty))
                {
                    nombreBool = true;
                }
                Expression<Func<UTTT.Ejemplo.Linq.Data.Entity.catDepartamento, bool>>
                    predicate =
                    (c =>
                    
                    ((nombreBool) ? (((nombreBool) ? c.strValor.Contains(this.txtValor.Text.Trim()) : false)) : true)
                    );

                predicate.Compile();

                List<UTTT.Ejemplo.Linq.Data.Entity.catDepartamento> listaDepartamento =
                    dcConsulta.GetTable<UTTT.Ejemplo.Linq.Data.Entity.catDepartamento>().Where(predicate).ToList();
                e.Result = listaDepartamento;
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        protected void dgvDepartamentos_RowCommand(object sender, GridViewCommandEventArgs e)
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
                this.session.Pantalla = "~/DepartamentosManager.aspx";
                this.Response.Redirect(this.session.Pantalla, false);

            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        private void eliminar(int _idDepartamento)
        {
            try
            {
                DataContext dcDelete = new DcGeneralDataContext();
                UTTT.Ejemplo.Linq.Data.Entity.catDepartamento departamento = dcDelete.GetTable<UTTT.Ejemplo.Linq.Data.Entity.catDepartamento>().First(
                    c => c.id == _idDepartamento);
                dcDelete.GetTable<UTTT.Ejemplo.Linq.Data.Entity.catDepartamento>().DeleteOnSubmit(departamento);
                dcDelete.SubmitChanges();
                this.showMessage("El registro se elimino correctamente.");
                this.DataSourcePersona.RaiseViewChanged();
            }
            catch (Exception _e)
            {
                throw _e;
            }
        }

        protected void buscarTextBox(object sender, EventArgs e)
        {
            DataSourcePersona.RaiseViewChanged();
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