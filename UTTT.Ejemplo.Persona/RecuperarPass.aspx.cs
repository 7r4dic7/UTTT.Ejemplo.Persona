using System;
using System.Collections.Generic;
using System.Data.Linq;
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
    public partial class RecuperarPass : System.Web.UI.Page
    {
        private SessionManager session = new SessionManager();
        private int idPersona = 0;
        private UTTT.Ejemplo.Linq.Data.Entity.Usuario baseEntity;
        private DataContext dcGlobal = new DcGeneralDataContext();
        private int tipoAccion = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Response.Buffer = true;
            this.session = (SessionManager)this.Session["SessionManager"];
        }

        protected void btnVerificar_Click(object sender, EventArgs e)
        {
            DataContext dcGuardar = new DcGeneralDataContext();
            if (!Page.IsValid)
            {
                return;
            }
            try
            {
                var userEA = dcGlobal.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Usuario>().FirstOrDefault(p => p.strNombreUsuario.Trim().Replace(" ", "").Equals(this.txtUsuario.Text.Trim().Replace(" ", "")));
                if(btnVerificar.Text == "Cambiar Contraseña")
                {
                    txtUsuario.ReadOnly = true;
                }
                if (!this.txtUsuario.ReadOnly)
                {

                    String mensaje = String.Empty;
                    if (!this.validacion(userEA, ref mensaje))
                    {
                        this.lblMensaje.Text = mensaje;
                        this.lblMensaje.Visible = true;
                        return;
                    }
                    activarElementos();
                    this.lblMensaje.Text = "";
                    this.txtUsuario.ReadOnly = true;

                }
                else 
                {

                    if (!ComprobarContraseña())
                    {
                        this.Response.Redirect("~/Login.aspx", false);
                    }
                    this.rfvPassword.Enabled = true;
                    this.rfvPassword1.Enabled = true;
                    this.comContras.Enabled = true;
                    String mensaje = String.Empty;
                    userEA.strPassword = this.txtPassword.Text.Trim();
                    if (!this.validacion2(userEA, ref mensaje))
                    {
                        this.lblMensaje.Text = mensaje;
                        this.lblMensaje.Visible = true;
                        return;
                    }
                    if (!userEA.strPassword.Equals(this.txtConfPassword.Text.Trim()))
                    {
                        this.lblMensaje.Text = "Las contraseñas no coinciden";
                        this.lblMensaje.Visible = true;
                        activarElementos();
                        return;
                    }


                    //Encriptar(this.txt_Password.Text.Trim());


                    userEA.strPassword = Encriptar(this.txtPassword.Text.Trim());
                    dcGlobal.SubmitChanges();
                    this.showMessage("Contraseña Actualizada Correctamente");
                    this.Response.Redirect("~/Login.aspx", false);

                }


            }
            catch (Exception ex)
            {

            }
        }

        public bool validacion(UTTT.Ejemplo.Linq.Data.Entity.Usuario _persona, ref String _mensaje)
        {
            if (_persona == null)
            {
                _mensaje = "No Existe ningun usario con ese nombre";
                return false;
            }
            return true;
        }
        public bool validacion2(UTTT.Ejemplo.Linq.Data.Entity.Usuario _persona, ref String _mensaje)
        {
            if (_persona == null)
            {
                _mensaje = "No Existe ningun usario con ese nombre";
                this.lblMensaje.Visible = true;
                activarElementos(); ;
                return false;
            }
            if (_persona.strPassword.Trim().Length < 8)
            {
                _mensaje = "La contraseña necesita al menos 8 caracteres";
                this.lblMensaje.Visible = true;
                activarElementos();
                return false;
            }
            if (_persona.strPassword.Trim().Length > 16)
            {
                _mensaje = "La contraseña no puede contener mas de 24 caracteres";
                this.lblMensaje.Visible = true;
                activarElementos();
                return false;
            }
            if (!Regex.IsMatch(_persona.strPassword, @"^[a-zA-Z]\w{8,24}$"))
            {
                _mensaje = "La contraseña debe tener al entre 8 y 24 caracteres y solo puede contener letras y numeros";
                this.lblMensaje.Visible = true;
                activarElementos();
                return false;
            }


            return true;
        }

        //Metodos
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
            if (String.IsNullOrEmpty(this.txtPassword.Text.Trim()) && String.IsNullOrEmpty(this.txtConfPassword.Text.Trim()))
            {
                return false;
            }
            return true;
        }

        public void activarElementos()
        {
            this.txtUsuario.ReadOnly = true;
            //this.lblPass.Visible = true;
            this.txtPassword.Visible = true;
            //this.lblPass2.Visible = true;
            this.txtConfPassword.Visible = true;
            this.btnVerificar.Text = "Cambiar Contraseña";
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

    }
}