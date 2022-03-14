<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonaManager.aspx.cs" Inherits="UTTT.Ejemplo.Persona.PersonaManager" debug=false%>
<%@ Register Assembly ="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.3.0/font/bootstrap-icons.css">
    <script type="text/javascript">
        function validaNumeros(evt) {
            //Valida qque solo se ingresen numeros a la caja de texto
            var code = (evt.which) ? evt.which : evt.keycode;
            if (code == 8) {
                return true;
            }
            else if (code >= 48 && code <= 57) {
                return true;
            }
            else {
                return false;
            }
        }
        function validaLetras(e) {
            //Valida que solo se ingresen letras y algunos caracteres especiales
            key = e.keycode || e.which;
            tecla = String.fromCharCode(key).toLowerCase();
            letras = " áéíóúabcdefghijklmnñopqrstuvwxyz";
            especiales = "8-37-39-46";
            tecla_especial = false;
            for (var i in especiales) {
                if (key == especiales[i]) {
                    tecla_especial = true;
                    break;
                }
            }
            if (letras.indexOf(tecla) == -1 && !tecla_especial) {
                return false;
            }
        }
        function validaCurp(ev) {
            //Valida que solo se ingresen letras y numeros
            var code = (ev.which) ? ev.which : ev.keycode;
            if (code == 8) {
                return true;
            }
            else if (code >= 48 && code <= 57) {
                return true;
            }
            else if (code >= 65 && code <= 90) {
                return true;
            }
            else if (code >= 97 && code <= 122) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
</head>
<body>

    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
        <div class="container-fluid justify-content-center" style="font-family: Arial; font-size: medium; font-weight: bold">
            <div class="row">
                <div class="col align-content-center">
                <h1><strong>Persona</strong></h1> 
                </div>
            </div>
        </div>
        <div class="container-fluid">
            <div class="row">
                <div class="col align-content-center">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <asp:Label ID="lblAccion" runat="server" Text="Accion" Font-Bold="True"></asp:Label>
                </div>
            </div>
        </div>
        <div>
        </div>



            
               
                    <div class="container-fluid">
                        <div class="row">
                                        <div class="col-md-7 ms-12 m-auto text-center">
                                            <label class="col-form-label">Sexo:</label>
                                            <br />
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">

                                                <ContentTemplate>
                                                    <asp:DropDownList Cssclass="form-select w-100" ID="ddlSexo" runat="server"
                                                         Width="250px">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvSexo" runat="server" ControlToValidate="ddlSexo" ErrorMessage="*Selecciona un sexo " InitialValue="-1" ValidationGroup="vgPersona"></asp:RequiredFieldValidator>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlSexo" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>

                                        </div>
                                        
                                        <div class="col-md-7 ms-12 m-auto text-center">
                                            <div class="input-group">
                                            <label class="input-group-text">Clave Unica:</label>
                                            <asp:TextBox CssClass="form-control w-100" ID="txtClaveUnica" runat="server"
                                                Width="250px" ViewStateMode="Disabled" onkeypress="return validaNumeros(event);" pattern=".{1,3}"
                                                title="1 a 3 es la longitud que se permite ingresar"></asp:TextBox>

                                            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtClaveUnica" ErrorMessage="*La clave unica debe de estar entre 100 y 999" MaximumValue="999" MinimumValue="100" Type="Integer" ValidationGroup="vgPersona"></asp:RangeValidator>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtClaveUnica" ErrorMessage="*Campo obligatorio" InitialValue="vgPersona"></asp:RequiredFieldValidator>
                                             
                                        </div>
</div>
                                    </div>
                               
                                        <div class="col-md-7 ms-12 m-auto text-center">
                                            <div class="input-group">
                                            <label class="input-group-text">Nombre:</label>
                                            <asp:TextBox CssClass="form-control w-100" ID="txtNombre" runat="server" Width="250px" ViewStateMode="Disabled"
                                                onkeypress="return validaLetras(event);"></asp:TextBox>

                                            <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre" ErrorMessage="*Nombre es obligatorio" ValidationGroup="vgPersona"></asp:RequiredFieldValidator>

                                            <asp:RegularExpressionValidator ID="revNombre" runat="server" ControlToValidate="txtNombre" ErrorMessage="*Formato invalido" ValidationExpression="^[a-zA-ZÀ-ÿ\u00f1\u00d1]+(\s*[a-zA-ZÀ-ÿ\u00f1\u00d1]*)*[a-zA-ZÀ-ÿ\u00f1\u00d1]+$" ValidationGroup="vgPersona"></asp:RegularExpressionValidator>
</div>
                                        </div>
                                        
                                        <div class="col-md-7 ms-12 m-auto text-center">
                                            <div class="input-group">
                                            <label class="input-group-text">A Paterno:</label>
                                            <asp:TextBox CssClass="form-control w-100" ID="txtAPaterno" runat="server" Width="250px" ViewStateMode="Disabled"
                                                onkeypress="return validaLetras(event);"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvAPaterno" runat="server" ControlToValidate="txtAPaterno" ErrorMessage="*Campo obligatorio" ValidationGroup="vgPersona"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="revAPaterno" runat="server" ControlToValidate="txtAPaterno" ErrorMessage="*Formato incorrecto" ValidationExpression="^[a-zA-ZÀ-ÿ\u00f1\u00d1]+(\s*[a-zA-ZÀ-ÿ\u00f1\u00d1]*)*[a-zA-ZÀ-ÿ\u00f1\u00d1]+$" ValidationGroup="vgPersona"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                    <div class="col-md-7 ms-12 m-auto text-center">
                                        <div class="input-group">
                                        <label class="input-group-text">A Materno:</label>
                                        <asp:TextBox class="form-control w-100" ID="txtAMaterno" runat="server" Width="250px"
                                            ViewStateMode="Disabled" onkeypress="return validaLetras(event);"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revAMaterno" runat="server" ControlToValidate="txtAMaterno" ErrorMessage="*Formato incorrecto" ValidationGroup="vgPersona" ValidationExpression="^[a-zA-ZÀ-ÿ\u00f1\u00d1]+(\s*[a-zA-ZÀ-ÿ\u00f1\u00d1]*)*[a-zA-ZÀ-ÿ\u00f1\u00d1]+$"></asp:RegularExpressionValidator>
                                    </div>
                                        </div>
                                    
                                    <div class="col-md-7 ms-12 m-auto text-center">
                                        <div class="input-group">
                                        <label class="input-group-text">CURP:</label>
                                        <asp:TextBox CssClass="form-control w-100" ID="txtCURP" runat="server" Width="250px" ViewStateMode="Disabled"
                                            onkeypress="return validaCurp(event);"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvCURP" runat="server" ControlToValidate="txtCURP" ErrorMessage="*Campo obligatorio" ValidationGroup="vgPersona"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revCURP" runat="server" ControlToValidate="txtCURP" ErrorMessage="*La CURP es incorrecta" ValidationExpression="^[A-Z]{1}[AEIOU]{1}[A-Z]{2}[0-9]{2}(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1])[HM]{1}(AS|BC|BS|CC|CS|CH|CL|CM|DF|DG|GT|GR|HG|JC|MC|MN|MS|NT|NL|OC|PL|QT|QR|SP|SL|SR|TC|TS|TL|VZ|YN|ZS|NE)[B-DF-HJ-NP-TV-Z]{3}[0-9A-Z]{1}[0-9]{1}$" ValidationGroup="vgPersona"></asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                        </div>

                                        <div class="col-md-7 ms-12 m-auto text-center">
                                            <div class="input-group">
                                            <label class="input-group-text">Fecha de nacimiento:</label>
                                            
                                            <asp:TextBox CssClass="form-control" Width="90%" ID="txtFechaNacimiento" runat="server"></asp:TextBox>
                                            <asp:ImageButton CssClass="mt-1" ID="imgPopup" ImageUrl="~/Images/images.jpg" ImageAlign="Bottom" runat="server" CausesValidation="false" Height="33px" Width="42px" />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup" runat="server" TargetControlID="txtFechaNacimiento" Format="dd/MM/yyyy" />
                                            <asp:RequiredFieldValidator ID="rfvFechaNacimiento" runat="server" ControlToValidate="txtFechaNacimiento" ErrorMessage="*Campo obligatorio" ValidationGroup="vgPersona"></asp:RequiredFieldValidator>
                                       </div>
                                                
                                            </div>
                        <br />
                        <br />
                            <div class="col-md-7 ms-12 m-auto text-center">
                                <asp:Button class="btn btn-info w-100 p-1" ID="btnAceptar" runat="server" Text="Aceptar"
                                    OnClick="btnAceptar_Click" ViewStateMode="Disabled" />
                                <asp:Button class="btn btn-danger w-100 p-1" ID="btnCancelar" runat="server" Text="Cancelar"
                                    OnClick="btnCancelar_Click" ViewStateMode="Disabled" />
                            </div>
                         
                    <br />
                <div class="container-fluid">
                    <div class="row">
                            <div class="col">
                                <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                            </div>
                        </div>
                </div>
            
        
    </form>
    <scripts>
        <scripts src="Scripts/bootstrap.min.js"></scripts>
    </scripts>
</body>
</html>
