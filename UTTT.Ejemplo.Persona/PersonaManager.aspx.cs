﻿#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UTTT.Ejemplo.Linq.Data.Entity;
using System.Data.Linq;
using System.Linq.Expressions;
using System.Collections;
using UTTT.Ejemplo.Persona.Control;
using UTTT.Ejemplo.Persona.Control.Ctrl;
using System.Net.Mail;
using System.Text.RegularExpressions;

#endregion

namespace UTTT.Ejemplo.Persona
{
    public partial class PersonaManager : System.Web.UI.Page
    {
        #region Variables

        private SessionManager session = new SessionManager();
        private int idPersona = 0;
        private UTTT.Ejemplo.Linq.Data.Entity.Persona baseEntity;
        private DataContext dcGlobal = new DcGeneralDataContext();
        private int tipoAccion = 0;
        private static string eMessage = string.Empty;

        #endregion

        #region Eventos

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
                    this.baseEntity = new Linq.Data.Entity.Persona();
                    this.tipoAccion = 1;
                }
                else
                {
                    this.baseEntity = dcGlobal.GetTable<Linq.Data.Entity.Persona>().First(c => c.id == this.idPersona);
                    this.tipoAccion = 2;
                }

                if (!this.IsPostBack)
                {
                    if (this.session.Parametros["baseEntity"] == null)
                    {
                        this.session.Parametros.Add("baseEntity", this.baseEntity);
                    }
                    List<CatSexo> lista = dcGlobal.GetTable<CatSexo>().ToList();
                    CatSexo catTemp = new CatSexo();
                    catTemp.id = -1;
                    catTemp.strValor = "Seleccionar";
                    lista.Insert(0, catTemp);
                    this.ddlSexo.DataTextField = "strValor";
                    this.ddlSexo.DataValueField = "id";
                    this.ddlSexo.DataSource = lista;
                    this.ddlSexo.DataBind();

                    this.ddlSexo.SelectedIndexChanged += new EventHandler(ddlSexo_SelectedIndexChanged);
                    this.ddlSexo.AutoPostBack = true;
                    if (this.idPersona == 0)
                    {
                        this.lblAccion.Text = "Agregar";
                    }
                    else
                    {
                        this.lblAccion.Text = "Editar";
                        this.txtNombre.Text = this.baseEntity.strNombre;
                        this.txtAPaterno.Text = this.baseEntity.strAPaterno;
                        this.txtAMaterno.Text = this.baseEntity.strAMaterno;
                        this.txtCURP.Text = this.baseEntity.strCURP;
                        this.txtClaveUnica.Text = this.baseEntity.strClaveUnica;
                        this.setItem(ref this.ddlSexo, baseEntity.CatSexo.strValor);

                    }
                }
                

            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al cargar la página");
                this.Response.Redirect("~/PersonaPrincipal.aspx", false);
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
                if (this.txtClaveUnica.Text.Trim() == String.Empty && this.txtNombre.Text.Trim() == String.Empty &&
                    this.txtAPaterno.Text.Trim() == String.Empty && this.txtAMaterno.Text.Trim() == String.Empty && int.Parse(this.ddlSexo.Text) == -1)
                {
                    this.Response.Redirect("~/PersonaPrincipal.aspx", false);
                }

                DataContext dcGuardar = new DcGeneralDataContext();
                UTTT.Ejemplo.Linq.Data.Entity.Persona persona = new Linq.Data.Entity.Persona();
                if (this.idPersona == 0)
                {
                    persona.strClaveUnica = this.txtClaveUnica.Text.Trim();
                    persona.strNombre = this.txtNombre.Text.Trim();
                    persona.strAMaterno = this.txtAMaterno.Text.Trim();
                    persona.strAPaterno = this.txtAPaterno.Text.Trim();
                    persona.strCURP = this.txtCURP.Text.Trim();
                    persona.idCatSexo = int.Parse(this.ddlSexo.Text);

                    string mensaje = string.Empty;
                    //validacion datos en el codigo
                    if(!this.validacion(persona, ref mensaje))
                    {
                        this.lblMensaje.Text = mensaje;
                        this.lblMensaje.Visible = true;
                        return;
                    }


                    dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Persona>().InsertOnSubmit(persona);
                    dcGuardar.SubmitChanges();
                    this.showMessage("El registro se agrego correctamente.");
                    this.Response.Redirect("~/PersonaPrincipal.aspx", false);
                    
                }
                if (this.idPersona > 0)
                {
                    persona = dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Persona>().First(c => c.id == idPersona);
                    persona.strClaveUnica = this.txtClaveUnica.Text.Trim();
                    persona.strNombre = this.txtNombre.Text.Trim();
                    persona.strAMaterno = this.txtAMaterno.Text.Trim();
                    persona.strAPaterno = this.txtAPaterno.Text.Trim();
                    persona.strCURP = this.txtCURP.Text.Trim();
                    persona.idCatSexo = int.Parse(this.ddlSexo.Text);

                    string mensaje = string.Empty;
                    //validacion datos en el codigo
                    if (!this.validacion(persona, ref mensaje))
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
                this.Response.Redirect("~/PersonaPrincipal.aspx", false);
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }

        protected void ddlSexo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idSexo = int.Parse(this.ddlSexo.Text);
                Expression<Func<CatSexo, bool>> predicateSexo = c => c.id == idSexo;
                predicateSexo.Compile();
                List<CatSexo> lista = dcGlobal.GetTable<CatSexo>().Where(predicateSexo).ToList();
                CatSexo catTemp = new CatSexo();            
                this.ddlSexo.DataTextField = "strValor";
                this.ddlSexo.DataValueField = "id";
                this.ddlSexo.DataSource = lista;
                this.ddlSexo.DataBind();
            }
            catch (Exception)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }

        #endregion

        #region Metodos

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

        #endregion

        #region Validación código
        ///<summary>
        /// Valida datos básicos 
        /// </summary>
        /// <param name="_persona"></param>
        /// <param name="_mensaje"></param>
        /// <returns></returns>
        
        public bool validacion(UTTT.Ejemplo.Linq.Data.Entity.Persona _persona, ref String _mensaje)
        {
            
            if(_persona.idCatSexo == -1)
            {
                _mensaje = "Seleccione Masculino o Femenino";
                return false;
            }

            int i = 0;
            //verifica si un texto es un numero
            if(int.TryParse(_persona.strClaveUnica, out i)== false)
            {
                _mensaje = "La clave unica debe de ser un numero";
                return false;
            }

            /// Validamos un numero
            /// string. saber que es un numero
            /// 99 y 1000
            
            if(int.Parse(_persona.strClaveUnica)<100 || int.Parse(_persona.strClaveUnica) > 999)
            {
                _mensaje = "La clave unica esta fuera de rango";
                return false;
            }

            if (_persona.strNombre.Equals(String.Empty))
            {
                _mensaje = "Nombre esta vacio";
                return false;
            }

            if(_persona.strNombre.Length < 3)
            {
                _mensaje = "Nombre debe contener al menos 3 caracteres";
                return false;
            }

            if (_persona.strAPaterno.Length < 3)
            {
                _mensaje = "APaterno debe contener al menos 3 caracteres";
                return false;
            }

            if (_persona.strAMaterno.Length < 3)
            {
                _mensaje = "AMaterno debe contener al menos 3 caracteres";
                return false;
            }

            //Valida Nombre solo contenga letras 
            if (!Regex.IsMatch(_persona.strNombre, @"^[a-zA-Z]+$"))
            {
                _mensaje = "Formato incorrecto en Nombre";
                return false;
            }

            //Valida APaterno solo contenga letras 
            if (!Regex.IsMatch(_persona.strAPaterno, @"^[a-zA-Z]+$"))
            {
                _mensaje = "Formato incorrecto en APaterno";
                return false;
            }

            //Valida AMaterno solo contenga letras 
            if (!Regex.IsMatch(_persona.strAMaterno, @"^[a-zA-Z]+$"))
            {
                _mensaje = "Formato incorrecto en AMaterno";
                return false;
            }

            //Valida CURP 
            if (!Regex.IsMatch(_persona.strCURP, @"^[A-Z]{1}[AEIOU]{1}[A-Z]{2}[0-9]{2}(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1])[HM]{1}(AS|BC|BS|CC|CS|CH|CL|CM|DF|DG|GT|GR|HG|JC|MC|MN|MS|NT|NL|OC|PL|QT|QR|SP|SL|SR|TC|TS|TL|VZ|YN|ZS|NE)[B-DF-HJ-NP-TV-Z]{3}[0-9A-Z]{1}[0-9]{1}$"))
            {
                _mensaje = "Formato incorrecto en CURP";
                return false;
            }

            if (_persona.strNombre.Length > 50)
            {
                _mensaje = "Los caracteres permitidos para nombre rebasan lo establecido de 50";
                return false;
            }

            if (_persona.strAPaterno.Equals(String.Empty))
            {
                _mensaje = "APaterno esta vacio";
                return false;
            }

            if (_persona.strAPaterno.Length > 50)
            {
                _mensaje = "Los caracteres permitidos para APaterno rebasan lo establecido de 50";
                return false;
            }

            return true;
        }
        #endregion
        protected void ExceptionMessage(Exception e)
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
                mail.To.Add("edel.meza@uttt.edu.mx");
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
    }
}