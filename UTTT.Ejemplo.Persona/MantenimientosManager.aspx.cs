using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UTTT.Ejemplo.Linq.Data.Entity;
using UTTT.Ejemplo.Persona.Control;
using UTTT.Ejemplo.Persona.Control.Ctrl;

namespace UTTT.Ejemplo.Persona
{
    public partial class MantenimientosManager : System.Web.UI.Page
    {

        private SessionManager session = new SessionManager();
        private int idPersona = 0;
        private UTTT.Ejemplo.Linq.Data.Entity.Mantenimientos baseEntity;
        private DataContext dcGlobal = new DcGeneralDataContext();
        private int tipoAccion = 0;
        private int idMantenimiento = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.Response.Buffer = true;
                this.session = (SessionManager)this.Session["SessionManager"];
                this.idPersona = this.session.Parametros["idPersona"] != null ?
                    int.Parse(this.session.Parametros["idPersona"].ToString()) : 0;

                this.idMantenimiento = this.session.Parametros["idMantenimiento"] != null ?
                    int.Parse(this.session.Parametros["idMantenimiento"].ToString()) : 0;

                if (this.idMantenimiento == 0)
                {
                    this.baseEntity = new Linq.Data.Entity.Mantenimientos();
                    this.tipoAccion = 1;
                }
                else
                {
                    this.baseEntity = dcGlobal.GetTable<Linq.Data.Entity.Mantenimientos>().First(c => c.id == this.idMantenimiento);
                    this.tipoAccion = 2;
                }

                if (!this.IsPostBack)
                {
                    if (this.session.Parametros["baseEntity"] == null)
                    {
                        this.session.Parametros.Add("baseEntity", this.baseEntity);
                    }
                    if (this.idMantenimiento == 0)
                    {
                        this.lblAccion.Text = "Agregar";
                    }
                    else
                    {
                        this.lblAccion.Text = "Editar";
                        CalendarExtender1.SelectedDate = this.baseEntity.dteFechaMantenimiento.Value.Date;
                        this.txtObservaciones.Text = this.baseEntity.strObservaciones;
                        
                    }
                }
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al cargar la página");
                this.Response.Redirect("~/Mantenimientos.aspx", false);
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsValid)
                {
                    return;
                }
                if (this.txtFechaMantenimiento.Text.Trim() == String.Empty && this.txtObservaciones.Text.Trim() == String.Empty)
                {
                    this.Response.Redirect("~/Mantenimientos.aspx", false);
                }
                else
                {
                    btnAceptar.ValidationGroup = "vgMant";
                    Page.Validate("vgMant");
                }

                //Se obtiene la fecha de mant
                string date = Request.Form[this.txtFechaMantenimiento.UniqueID];
                DateTime dt;
                bool isValid = DateTime.TryParseExact(date, "dd/MM/yyyy", new CultureInfo("es-MX"), DateTimeStyles.None, out dt);
                if (!isValid)
                {
                    this.lblMensaje.Text = "La fecha no tiene el formato valido";
                    this.lblMensaje.Visible = true;
                    return;
                }
                DateTime fechaMantenimiento = DateTime.Parse(date, CultureInfo.CreateSpecificCulture("es-MX"));

                DataContext dcGuardar = new DcGeneralDataContext();
                UTTT.Ejemplo.Linq.Data.Entity.Mantenimientos mantenimientos = new Linq.Data.Entity.Mantenimientos();
                if (this.idMantenimiento == 0)
                {
                    mantenimientos.idEquipo = this.idPersona;
                    mantenimientos.dteFechaMantenimiento = fechaMantenimiento;
                    mantenimientos.strObservaciones = this.txtObservaciones.Text.Trim();

                    string mensaje = string.Empty;
                    //validacion datos en el codigo
                    if (!this.validacion(mantenimientos, ref mensaje))
                    {
                        this.lblMensaje.Text = mensaje;
                        this.lblMensaje.Visible = true;
                        return;
                    }

                    dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Mantenimientos>().InsertOnSubmit(mantenimientos);
                    dcGuardar.SubmitChanges();
                    this.showMessage("El registro se agrego correctamente.");
                    this.Response.Redirect("~/Mantenimientos.aspx");
                }
                if (this.idMantenimiento > 0)
                {
                    mantenimientos = dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Mantenimientos>().First(c => c.id == this.idMantenimiento);
                    mantenimientos.strObservaciones = this.txtObservaciones.Text.Trim();
                    mantenimientos.dteFechaMantenimiento = fechaMantenimiento;

                    string mensaje = string.Empty;
                    //validacion datos en el codigo
                    if (!this.validacion(mantenimientos, ref mensaje))
                    {
                        this.lblMensaje.Text = mensaje;
                        this.lblMensaje.Visible = true;
                        return;
                    }

                    dcGuardar.SubmitChanges();
                    this.showMessage("El registro se edito correctamente.");
                    this.Server.Transfer("~/Mantenimientos.aspx");

                }
            }
            catch (Exception _e)
            {
                this.showMessageException(_e.Message);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Response.Redirect("~/Mantenimientos.aspx", false);
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }

        public bool validacion(UTTT.Ejemplo.Linq.Data.Entity.Mantenimientos _mant, ref String _mensaje)
        {


            if (_mant.strObservaciones.Equals(String.Empty))
            {
                _mensaje = "Observaciones no puede estar vacio";
                return false;
            }

            if (_mant.strObservaciones.Length < 3)
            {
                _mensaje = "Observaciones debe contener al menos 3 caracteres";
                return false;
            }
            //Valida Nombre solo contenga letras 
            if (!Regex.IsMatch(_mant.strObservaciones, @"^[a-zA-Z0-9\-À-ÿ\u00f1\u00d1]+(\s*[a-zA-Z0-9\-À-ÿ\u00f1\u00d1]*)*[a-zA-Z0-9\-À-ÿ\u00f1\u00d1]+$"))
            {
                _mensaje = "observaciones solo puede contener letras";
                return false;
            }

            if (_mant.strObservaciones.Length > 50)
            {
                _mensaje = "Los caracteres permitidos para observaciones rebasan lo establecido de 50";
                return false;
            }

            //valida fecha
            if (_mant.dteFechaMantenimiento < DateTime.Now)
            {
                _mensaje = "Ingresa una fecha de mantenimiento valida";
                return false;
            }
            if (_mant.dteFechaMantenimiento == null)
            {
                _mensaje = "Ingresa una fecha de mantenimiento";
                return false;
            }
            DateTime date = new DateTime(1900, 12, 31);
            if (_mant.dteFechaMantenimiento < date)
            {
                _mensaje = "Ingresa una fecha de mantenimiento valida";
                return false;
            }
            if (!Regex.IsMatch(_mant.dteFechaMantenimiento.ToString(), @"^([0 - 2][0 - 9] | 3[0 - 1])(\/| -)(0[1 - 9] | 1[0 - 2])\2(\d{ 4})$"))
            {
                _mensaje = "Formato de fecha invalido";
            }
            return true;
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