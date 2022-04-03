using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UTTT.Ejemplo.Linq.Data.Entity;
using UTTT.Ejemplo.Persona.Control;
using UTTT.Ejemplo.Persona.Control.Ctrl;

namespace UTTT.Ejemplo.Persona
{
    public partial class Login : System.Web.UI.Page
    {
        private UTTT.Ejemplo.Linq.Data.Entity.Persona baseEntity;
        private DataContext dcGlobal = new DcGeneralDataContext();
        private SessionManager session = new SessionManager();
        private int idPersona = 0;
        private int tipoAccion = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender,EventArgs e)
        {
            try
            {
                if (!Page.IsValid)
                {
                    return;
                }
                var auth = dcGlobal.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Usuario>().
                    FirstOrDefault(p => p.strNombreUsuario.Trim().Replace(" ","").Equals(this.txtUsuario.Text.Trim().Replace(" ","")));
                string mensaje = string.Empty;
                if(!this.validacion(auth, ref mensaje))
                {
                    this.lblMensaje.Text = mensaje;
                    this.lblMensaje.Visible = true;
                    return;
                }
                this.session.Pantalla = "~/Index.aspx";
                Hashtable parametrosRagion = new Hashtable();
                parametrosRagion.Add("strNombreUsuario", auth.strNombreUsuario);
                this.session.Parametros = parametrosRagion;
                this.Session["SessionManager"] = this.session;
                this.Response.Redirect(this.session.Pantalla,false);
            }
            catch (Exception _e)
            {
                var ex = new PersonaManager();
                ex.ExceptionMessage(_e);
                this.showMessage("Ha ocurrido un error");
            }
        }

        protected void btnRecuperar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Response.Redirect("~/RecuperarPass.aspx", false);
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Response.Redirect("~/Registrar.aspx", false);
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }

        public bool validacion(UTTT.Ejemplo.Linq.Data.Entity.Usuario _usuario, ref String _mensaje)
        {
            if (_usuario == null)
            {
                _mensaje = "No Existe ningun usario con ese nombre";
                return false;
            }
            String contra = this.DesEncriptar(_usuario.strPassword).Trim();
            String contraPass = this.txtPassword.Value.Trim();
            if (!contra.Equals(contraPass))
            {
                _mensaje = "Contraseña Incorrecta";
                return false;
            }
            if (_usuario.idEstado == 2)
            {
                _mensaje = "Usuario inactivo";
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

    }
}