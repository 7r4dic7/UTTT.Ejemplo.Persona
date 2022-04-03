using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
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
    public partial class Departamentomanager : System.Web.UI.Page
    {

        private SessionManager session = new SessionManager();
        private int idPersona = 0;
        private UTTT.Ejemplo.Linq.Data.Entity.catDepartamento baseEntity;
        private DataContext dcGlobal = new DcGeneralDataContext();
        private int tipoAccion = 0;
        private static string eMessage = string.Empty;
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
                this.Response.Buffer = true;
                this.session = (SessionManager)this.Session["SessionManager"];
                this.idPersona = this.session.Parametros["idPersona"] != null ?
                    int.Parse(this.session.Parametros["idPersona"].ToString()) : 0;
                if (this.idPersona == 0)
                {
                    this.baseEntity = new Linq.Data.Entity.catDepartamento();
                    this.tipoAccion = 1;
                }
                else
                {
                    this.baseEntity = dcGlobal.GetTable<Linq.Data.Entity.catDepartamento>().First(c => c.id == this.idPersona);
                    this.tipoAccion = 2;
                }

                if (!this.IsPostBack)
                {
                    if (this.session.Parametros["baseEntity"] == null)
                    {
                        this.session.Parametros.Add("baseEntity", this.baseEntity);
                    }
                    if (this.idPersona == 0)
                    {
                        this.lblAccion.Text = "Agregar";
                    }
                    else
                    {
                        this.lblAccion.Text = "Editar";
                        this.txtValor.Text = this.baseEntity.strValor;
                        this.txtDescripcion.Text = this.baseEntity.strDescripcion;
                     

                    }
                }
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al cargar la página");
                this.Response.Redirect("~/catDepartamentos.aspx", false);
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
                if (this.txtValor.Text.Trim() == String.Empty && this.txtDescripcion.Text.Trim() == String.Empty)
                {
                    this.Response.Redirect("~/catDepartamentos.aspx", false);
                }
                else
                {
                    btnAceptar.ValidationGroup = "vgCatdepartamento";
                    Page.Validate("vgCatdepartamento");
                }


                DataContext dcGuardar = new DcGeneralDataContext();
                UTTT.Ejemplo.Linq.Data.Entity.catDepartamento departamento = new Linq.Data.Entity.catDepartamento();
                if (this.idPersona == 0)
                {
                    departamento.strValor = this.txtValor.Text.Trim();
                    departamento.strDescripcion = this.txtDescripcion.Text.Trim();
                    string mensaje = string.Empty;
                    //validacion datos en el codigo
                    if (!this.validacion(departamento, ref mensaje))
                    {
                        this.lblMensaje.Text = mensaje;
                        this.lblMensaje.Visible = true;
                        return;
                    }


                    dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.catDepartamento>().InsertOnSubmit(departamento);
                    dcGuardar.SubmitChanges();
                    this.showMessage("El registro se agrego correctamente.");
                    this.Response.Redirect("~/catDepartamentos.aspx", false);

                }
                if (this.idPersona > 0)
                {
                    departamento = dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.catDepartamento>().First(c => c.id == idPersona);
                    departamento.strValor = this.txtValor.Text.Trim();
                    departamento.strDescripcion = this.txtDescripcion.Text.Trim();

                    string mensaje = string.Empty;
                    //validacion datos en el codigo
                    if (!this.validacion(departamento, ref mensaje))
                    {
                        this.lblMensaje.Text = mensaje;
                        this.lblMensaje.Visible = true;
                        return;
                    }

                    dcGuardar.SubmitChanges();
                    this.showMessage("El registro se edito correctamente.");
                    this.Response.Redirect("~/PersonaPrincipal.aspx", false);
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
                this.Response.Redirect("~/catDepartamentos.aspx", false);
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }


        public bool validacion(UTTT.Ejemplo.Linq.Data.Entity.catDepartamento _departamento, ref String _mensaje)
        {
            if (_departamento.strValor.Equals(String.Empty))
            {
                _mensaje = "Valor esta vacio";
                return false;
            }

            if (_departamento.strDescripcion.Equals(String.Empty))
            {
                _mensaje = "Descripcion esta vacio";
                return false;
            }

            if (_departamento.strValor.Length < 3)
            {
                _mensaje = "Valor debe contener al menos 3 caracteres";
                return false;
            }

            if (_departamento.strDescripcion.Length < 3)
            {
                _mensaje = "Descripcion debe contener al menos 3 caracteres";
                return false;
            }
            if (_departamento.strValor.Length > 50)
            {
                _mensaje = "Valor debe contener menos de 50 caracteres";
                return false;
            }

            if (_departamento.strDescripcion.Length > 50)
            {
                _mensaje = "Descripcion debe contener menos de 50 caracteres";
                return false;
            }
            //Valida Nombre solo contenga letras 
            if (!Regex.IsMatch(_departamento.strValor, @"^[a-zA-ZÀ-ÿ\u00f1\u00d1]+(\s*[a-zA-ZÀ-ÿ\u00f1\u00d1]*)*[a-zA-ZÀ-ÿ\u00f1\u00d1]+$"))
            {
                _mensaje = "Formato incorrecto en Nombre";
                return false;
            }

            //Valida APaterno solo contenga letras 
            if (!Regex.IsMatch(_departamento.strDescripcion, @"^[a-zA-ZÀ-ÿ\u00f1\u00d1]+(\s*[a-zA-ZÀ-ÿ\u00f1\u00d1]*)*[a-zA-ZÀ-ÿ\u00f1\u00d1]+$"))
            {
                _mensaje = "Formato incorrecto en APaterno";
                return false;
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