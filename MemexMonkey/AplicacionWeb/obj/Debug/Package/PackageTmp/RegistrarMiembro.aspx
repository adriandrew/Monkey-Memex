<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegistrarMiembro.aspx.cs" Inherits="AplicacionWeb.Miembros.RegistrarMiembro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:CreateUserWizard ID="CreateUserWizard" runat="server" OnCreatedUser="CreateUserWizard_CreatedUser">
    <WizardSteps>
        <asp:CreateUserWizardStep runat="server">
        </asp:CreateUserWizardStep>
        <asp:CompleteWizardStep runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td align="center">Completar</td>
                    </tr>
                    <tr>
                        <td>La cuenta se ha creado correctamente.</td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Button ID="ContinueButton" runat="server" BackColor="Black" CausesValidation="False" CommandName="Continue" ForeColor="White" Text="Continuar" ValidationGroup="CreateUserWizard" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:CompleteWizardStep>
    </WizardSteps>
</asp:CreateUserWizard>
</asp:Content>
