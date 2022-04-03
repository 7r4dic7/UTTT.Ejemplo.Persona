using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UTTT.Ejemplo.Linq.Data.Entity;
using UTTT.Ejemplo.Persona.Control;
using UTTT.Ejemplo.Persona.Control.Ctrl;

namespace UTTT.Ejemplo.Persona
{
    public partial class EquipoManager : System.Web.UI.Page
    {
        #region Variables

        private SessionManager session = new SessionManager();
        private int idPersona = 0;
        private UTTT.Ejemplo.Linq.Data.Entity.Equipo baseEntity;
        private DataContext dcGlobal = new DcGeneralDataContext();
        private int tipoAccion = 0;
        private static string eMessage = string.Empty;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.Response.Buffer = true;
                this.session = (SessionManager)this.Session["SessionManager"];
                this.idPersona = this.session.Parametros["idPersona"] != null ?
                    int.Parse(this.session.Parametros["idPersona"].ToString()) : 0;
                if (this.idPersona == 0)
                {
                    this.baseEntity = new Linq.Data.Entity.Equipo();
                    this.tipoAccion = 1;
                }
                else
                {
                    this.baseEntity = dcGlobal.GetTable<Linq.Data.Entity.Equipo>().First(c => c.id == this.idPersona);
                    this.tipoAccion = 2;
                }

                if (!this.IsPostBack)
                {
                    if (this.session.Parametros["baseEntity"] == null)
                    {
                        this.session.Parametros.Add("baseEntity", this.baseEntity);
                    }
                    List<catDepartamento> lista = dcGlobal.GetTable<catDepartamento>().ToList();
                    this.ddlDepartamneto.DataTextField = "strValor";
                    this.ddlDepartamneto.DataValueField = "id";


                    //this.ddlSexo.SelectedIndexChanged += new EventHandler(ddlSexo_SelectedIndexChanged);
                    //this.ddlSexo.AutoPostBack = false;
                    if (this.idPersona == 0)
                    {
                        this.lblAccion.Text = "Agregar";
                        CalendarExtender1.SelectedDate = DateTime.Now;

                        catDepartamento catTemp = new catDepartamento();
                        catTemp.id = -1;
                        catTemp.strValor = "Seleccionar";
                        lista.Insert(0, catTemp);
                        this.ddlDepartamneto.DataSource = lista;
                        this.ddlDepartamneto.DataBind();
                    }
                    else
                    {
                        this.lblAccion.Text = "Editar";
                        this.txtNombre.Text = this.baseEntity.strNombre;
                        this.txtDescripcion.Text = this.baseEntity.strDescripcion;
                        CalendarExtender1.SelectedDate = this.baseEntity.dteFechaCompra.Value.Date;

                        this.ddlDepartamneto.DataSource = lista;
                        this.ddlDepartamneto.DataBind();
                        this.setItem(ref this.ddlDepartamneto, baseEntity.catDepartamento.strValor);

                    }
                    this.ddlDepartamneto.SelectedIndexChanged += new EventHandler(ddlDepartamento_SelectedIndexChanged);
                    this.ddlDepartamneto.AutoPostBack = true;
                }


            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al cargar la página");
                this.Response.Redirect("~/EquipoPrincipal.aspx", false);
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
                if (this.txtFechaCompra.Text.Trim() == String.Empty && this.txtNombre.Text.Trim() == String.Empty &&
                    this.txtDescripcion.Text.Trim() == String.Empty  && int.Parse(this.ddlDepartamneto.Text) == -1)
                {
                    this.Response.Redirect("~/EquipoPrincipal.aspx", false);
                }
                else
                {
                    btnAceptar.ValidationGroup = "vgDepartamento";
                    Page.Validate("vgDepartamento");
                }

                //Se obtiene la fecha de nacimiento
                string date = Request.Form[this.txtFechaCompra.UniqueID];
                DateTime fechaCompra = DateTime.ParseExact(date, "dd/MM/yyyy", null);

                DataContext dcGuardar = new DcGeneralDataContext();
                UTTT.Ejemplo.Linq.Data.Entity.Equipo equipo = new Linq.Data.Entity.Equipo();
                if (this.idPersona == 0)
                {
                    equipo.strNombre = this.txtNombre.Text.Trim();
                    equipo.strDescripcion = this.txtDescripcion.Text.Trim();
                   equipo.idDepartamento = int.Parse(this.ddlDepartamneto.Text);
                    equipo.dteFechaCompra = fechaCompra;

                    string mensaje = string.Empty;
                    //validacion datos en el codigo
                    if (!this.validacion(equipo, ref mensaje))
                    {
                        this.lblMensaje.Text = mensaje;
                        this.lblMensaje.Visible = true;
                        return;
                    }


                    dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Equipo>().InsertOnSubmit(equipo);
                    dcGuardar.SubmitChanges();
                    this.showMessage("El registro se agrego correctamente.");
                    this.Response.Redirect("~/EquipoPrincipal.aspx", false);

                }
                if (this.idPersona > 0)
                {
                    equipo = dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Equipo>().First(c => c.id == idPersona);
                    equipo.strNombre = this.txtNombre.Text.Trim();
                    equipo.strDescripcion = this.txtDescripcion.Text.Trim();
                    equipo.idDepartamento = int.Parse(this.ddlDepartamneto.Text);

                    //asigna fecha nacimiento
                    equipo.dteFechaCompra = fechaCompra;

                    string mensaje = string.Empty;
                    this.lblMensaje.Visible = true;
                    this.lblMensaje.Text = this.ddlDepartamneto.Text;
                    if (int.Parse(this.ddlDepartamneto.Text) == -1)
                    {
                        mensaje = "Seleciona un departamento";
                        this.lblMensaje.Text = mensaje;
                        return;
                    }

                    //validacion datos en el codigo
                    if (!this.validacion(equipo, ref mensaje))
                    {
                        this.lblMensaje.Text = mensaje;
                        this.lblMensaje.Visible = true;
                        return;
                    }

                    dcGuardar.SubmitChanges();
                    this.showMessage("El registro se edito correctamente.");
                    this.Response.Redirect("~/EquipoPrincipal.aspx", false);
                }
            }
            catch (Exception ex)
            {
                ExceptionMessage(ex);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Response.Redirect("~/EquipoPrincipal.aspx", false);
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }

        protected void ddlDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idSexo = int.Parse(this.ddlDepartamneto.Text);
                Expression<Func<catDepartamento, bool>> predicateDep = c => c.id == idSexo;
                predicateDep.Compile();
                List<catDepartamento> lista = dcGlobal.GetTable<catDepartamento>().Where(predicateDep).ToList();
                catDepartamento catTemp = new catDepartamento();
                this.ddlDepartamneto.DataTextField = "strValor";
                this.ddlDepartamneto.DataValueField = "id";
                this.ddlDepartamneto.DataSource = lista;
                this.ddlDepartamneto.DataBind();
            }
            catch (Exception)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }

        public void setItem(ref DropDownList _control, String _value)
        {
            foreach (ListItem item in _control.Items)
            {
                if (item.Value == _value)
                {
                    item.Selected = true;
                    break;
                }
            }
            _control.Items.FindByText(_value).Selected = true;
        }


        public bool validacion(UTTT.Ejemplo.Linq.Data.Entity.Equipo _equipo, ref String _mensaje)
        {

            if (_equipo.idDepartamento == -1)
            {
                _mensaje = "Seleccione un departamento";
                return false;
            }

            if (_equipo.strNombre.Equals(String.Empty))
            {
                _mensaje = "Nombre no puede estar vacio";
                return false;
            }

            if (_equipo.strNombre.Length < 3)
            {
                _mensaje = "Nombre debe contener al menos 3 caracteres";
                return false;
            }

            if (_equipo.strDescripcion.Length < 3)
            {
                _mensaje = "Descripcion debe contener al menos 3 caracteres";
                return false;
            }
            //Valida Nombre solo contenga letras 
            if (!Regex.IsMatch(_equipo.strNombre, @"^[a-zA-Z0-9\-À-ÿ\u00f1\u00d1]+(\s*[a-zA-Z0-9\-À-ÿ\u00f1\u00d1]*)*[a-zA-Z0-9\-À-ÿ\u00f1\u00d1]+$"))
            {
                _mensaje = "Formato incorrecto en Nombre";
                return false;
            }

            //Valida APaterno solo contenga letras 
            if (!Regex.IsMatch(_equipo.strDescripcion, @"^[a-zA-Z0-9\-À-ÿ\u00f1\u00d1]+(\s*[a-zA-Z0-9\-À-ÿ\u00f1\u00d1]*)*[a-zA-Z0-9\-À-ÿ\u00f1\u00d1]+$"))
            {
                _mensaje = "Formato incorrecto en Descripcion";
                return false;
            }

            if (_equipo.strNombre.Length > 50)
            {
                _mensaje = "Los caracteres permitidos para nombre rebasan lo establecido de 50";
                return false;
            }

            if (_equipo.strDescripcion.Equals(String.Empty))
            {
                _mensaje = "Descripcion no puede estar vacio";
                return false;
            }

            if (_equipo.strDescripcion.Length > 50)
            {
                _mensaje = "Los caracteres permitidos para descripcion rebasan lo establecido de 50";
                return false;
            }

            //valida fecha
            if (_equipo.dteFechaCompra > DateTime.Now)
            {
                _mensaje = "Ingresa una fecha de compra valida";
                return false;
            }
            if (_equipo.dteFechaCompra == null)
            {
                _mensaje = "Ingresa una fecha de compra";
                return false;
            }
            DateTime date = new DateTime(1900,12,31);
            if (_equipo.dteFechaCompra < date)
            {
                _mensaje = "Ingresa una fecha de compra valida";
                return false;
            }
            if (!Regex.IsMatch(_equipo.dteFechaCompra.ToString(), @"^([0 - 2][0 - 9] | 3[0 - 1])(\/| -)(0[1 - 9] | 1[0 - 2])\2(\d{ 4})$"))
            {
                _mensaje = "Formato de fecha invalido";
            }
            return true;
        }

        public void ExceptionMessage(Exception e)
        {
            System.Text.StringBuilder msg = new System.Text.StringBuilder();
            msg.AppendLine(e.GetType().FullName);
            msg.AppendLine(e.Message);
            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace();
            msg.AppendLine(st.ToString());
            msg.AppendLine();
            eMessage = msg.ToString();


            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("19301522@uttt.edu.mx");
                mail.To.Add("hack.ta66@gmail.com");
                //mail.To.Add("edel.meza@uttt.edu.mx");
                mail.Subject = "Exception stack";
                mail.Body = eMessage;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new System.Net.NetworkCredential("19301522@uttt.edu.mx", "OAR7550O");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
            this.Response.Redirect("~/errorPage.aspx");
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