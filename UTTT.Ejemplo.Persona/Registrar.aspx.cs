using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UTTT.Ejemplo.Linq.Data.Entity;
using UTTT.Ejemplo.Persona.Control;
using UTTT.Ejemplo.Persona.Control.Ctrl;

namespace UTTT.Ejemplo.Persona
{
    public partial class Registrar : System.Web.UI.Page
    {
        private SessionManager session = new SessionManager();
        private int idPersona = 0;
        private UTTT.Ejemplo.Linq.Data.Entity.Usuario baseEntity;
        private DataContext dcGlobal = new DcGeneralDataContext();
        private int tipoAccion = 0;
        private static string eMessage = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.Response.Buffer = true;
                this.session = (SessionManager)this.Session["SessionManager"];


                if (!this.IsPostBack)
                {
                    
                    List<catEstado> lista = dcGlobal.GetTable<catEstado>().ToList();
                    this.ddlEstado.DataTextField = "strValor";
                    this.ddlEstado.DataValueField = "id";


                    //this.ddlSexo.SelectedIndexChanged += new EventHandler(ddlSexo_SelectedIndexChanged);
                    //this.ddlSexo.AutoPostBack = false;
                    if (this.idPersona == 0)
                    {
                        catEstado catTemp = new catEstado();
                        catTemp.id = -1;
                        catTemp.strValor = "Seleccionar";
                        lista.Insert(0, catTemp);
                        this.ddlEstado.DataSource = lista;
                        this.ddlEstado.DataBind();
                    }
                    //else
                    //{
                    //    this.lblAccion.Text = "Editar";
                    //    this.txtNombre.Text = this.baseEntity.strNombre;
                    //    this.txtAPaterno.Text = this.baseEntity.strAPaterno;
                    //    this.txtAMaterno.Text = this.baseEntity.strAMaterno;
                    //    this.txtClaveUnica.Text = this.baseEntity.strClaveUnica;
                    //    this.txtCURP.Text = this.baseEntity.strCURP;

                    //    CalendarExtender1.SelectedDate = this.baseEntity.dteFechaNacimiento.Value.Date;

                    //    this.ddlSexo.DataSource = lista;
                    //    this.ddlSexo.DataBind();
                    //    this.setItem(ref this.ddlSexo, baseEntity.CatSexo.strValor);

                    //}
                    this.ddlEstado.SelectedIndexChanged += new EventHandler(ddlEstado_SelectedIndexChanged);
                    this.ddlEstado.AutoPostBack = true;
                }


            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al cargar la página");
                this.Response.Redirect("~/Login.aspx", false);
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsValid)
                {
                    return;
                }
                if (this.txtIdEmpleado.Text.Trim() == String.Empty && this.txtUsuario.Text.Trim() == String.Empty && this.txtPassword.Value.ToString()
                    .Trim() == String.Empty && this.txtConfPassword.Value.ToString().Trim() == String.Empty && int.Parse(this.ddlEstado.Text) == -1)
                {
                    this.Response.Redirect("~/Login.aspx", false);
                }
                else
                {
                    btnConfirmar.ValidationGroup = "vgReg";
                    Page.Validate("vgReg");
                }

                DataContext dcGuardar = new DcGeneralDataContext();
                UTTT.Ejemplo.Linq.Data.Entity.Usuario persona = new Linq.Data.Entity.Usuario();
                if (this.idPersona == 0)
                {
                    persona.idPersona = int.Parse(this.txtIdEmpleado.Text);
                    persona.strNombreUsuario = this.txtUsuario.Text.Trim();
                    persona.strPassword = this.txtPassword.Value.ToString().Trim();
                    persona.idPerfil = 1;
                    persona.idEstado = int.Parse(this.ddlEstado.Text);

                    string mensaje = string.Empty;
                    //validacion datos en el codigo
                    if (!this.validacion(persona, ref mensaje))
                    {
                        this.lblMensaje.Text = mensaje;
                        this.lblMensaje.Visible = true;
                        return;
                    }
                    
                    if (this.txtPassword.Value.ToString().Trim() != this.txtConfPassword.Value.ToString().Trim())
                    {
                        this.lblMensaje.Visible = true;
                        this.lblMensaje.Text = "Las contraseñas no coinciden";
                        return;
                    }
                    else
                    {
                       var pass = Encriptar(txtPassword.Value.ToString().Trim());
                        persona.strPassword = pass;
                    }
                    var c = dcGlobal.GetTable<Linq.Data.Entity.Persona>().FirstOrDefault(x => x.id == persona.idPersona);
                    if (c == null)
                    {
                        mensaje = "No existe ningun empleado con ese id";
                        this.lblMensaje.Visible = true;
                        return;
                    }
                    dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Usuario>().InsertOnSubmit(persona);
                    dcGuardar.SubmitChanges();
                    this.showMessage("El registro se agrego correctamente.");
                    this.Response.Redirect("~/Login.aspx", false);

                }
                if (this.idPersona > 0)
                {
                    persona = dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Usuario>().First(c => c.id == idPersona);
                    persona.idPersona = int.Parse(this.txtIdEmpleado.Text);
                    persona.strNombreUsuario = this.txtUsuario.Text.Trim();
                    persona.strPassword = this.txtPassword.Value.ToString().Trim();
                    persona.idPerfil = 1;
                    persona.idEstado = int.Parse(this.ddlEstado.Text);

                    string mensaje = string.Empty;
                    this.lblMensaje.Visible = true;
                    this.lblMensaje.Text = this.ddlEstado.Text;
                    if (int.Parse(this.ddlEstado.Text) == -1)
                    {
                        mensaje = "Seleciona un estado";
                        this.lblMensaje.Text = mensaje;
                        return;
                    }

                    //validacion datos en el codigo
                    if (!this.validacion(persona, ref mensaje))
                    {
                        this.lblMensaje.Text = mensaje;
                        this.lblMensaje.Visible = true;
                        return;
                    }
                    if (!this.txtPassword.Equals(this.txtConfPassword.Value.ToString()))
                    {
                        this.lblMensaje.Visible = true;
                        this.lblMensaje.Text = "Las contraseñas no coinciden";
                        return;
                    }
                   
                    dcGuardar.SubmitChanges();
                    this.showMessage("El registro se edito correctamente.");
                    this.Response.Redirect("~/Login.aspx", false);
                }
            }
            catch (Exception ex)
            {
                PersonaManager _e = new PersonaManager();
                _e.ExceptionMessage(ex);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Response.Redirect("~/Login.aspx", false);
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }

        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idEstado = int.Parse(this.ddlEstado.Text);
                Expression<Func<catEstado, bool>> predicate = c => c.id == idEstado;
                predicate.Compile();
                List<catEstado> lista = dcGlobal.GetTable<catEstado>().Where(predicate).ToList();
                catEstado catTemp = new catEstado();
                this.ddlEstado.DataTextField = "strValor";
                this.ddlEstado.DataValueField = "id";
                this.ddlEstado.DataSource = lista;
                this.ddlEstado.DataBind();
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

        public bool validacion(UTTT.Ejemplo.Linq.Data.Entity.Usuario _usuario, ref String _mensaje)
        {
             
            var co = dcGlobal.GetTable<Linq.Data.Entity.Usuario>().Where(x => x.strNombreUsuario == _usuario.strNombreUsuario);
            if(co.Equals(_usuario.strNombreUsuario))
            {
                _mensaje = "El nombre de usuario ya esta en uso";
                this.lblMensaje.Visible=true;
                return false;
            }
            var com = dcGlobal.GetTable<Linq.Data.Entity.Usuario>().Where(x => x.idPersona == _usuario.idPersona);
            if (com.Equals(_usuario.idPersona))
            {
                _mensaje = "El empleado ya cuenta con un usuario";
                this.lblMensaje.Visible = true;
                return false;
            }
            if (_usuario.strPassword.Trim().Length < 8)
            {
                _mensaje = "La contraseña necesita al menos 8 caracteres";
                this.lblMensaje.Visible = true;
                return false;
            }
            if (_usuario.strPassword.Trim().Length > 24)
            {
                _mensaje = "contraseña no puede contener mas de 24 caracteres";
                this.lblMensaje.Visible = true;
                return false;
            }
            if (!Regex.IsMatch(_usuario.strPassword, @"^[a-zA-Z]\w{8,24}$"))
            {
                _mensaje = "La contraseña debe tener al entre 8 y 24 caracteres y solo puede contener letras y numeros";
                this.lblMensaje.Visible = true;
                return false;
            }
            return true;
        }

        public string Encriptar(string _cadenaAencriptar)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(_cadenaAencriptar);
            result = Convert.ToBase64String(encryted);
            return result;
        }
        public string DesEncriptar(string _cadenaAdesencriptar)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(_cadenaAdesencriptar);
            //result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }
        public bool ComprobarContraseña()
        {
            if (string.IsNullOrEmpty(this.txtPassword.Value.ToString().Trim()) && string.IsNullOrEmpty(this.txtConfPassword.Value.ToString().Trim()))
            {
                return false;
            }
            return true;
        }
    }
}