<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm.aspx.cs" Inherits="TestAspWebApp.WebForm" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">        
        <asp:DropDownList ID="DropDownViewSource" runat="server"/>        
        <asp:Button ID="ButtonView" runat="server" Text="Показать" OnClick="ButtonViewOnClick"/>        
        <asp:ScriptManager ID="ScriptManager1" runat="server"/>
        <asp:UpdatePanel ID = "UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
            <asp:GridView 
                ID="GridViewSource" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" 
                Width="75%" ShowFooter="True" AutoGenerateColumns="False"
                OnRowEditing="GridViewSampleRowEditing" OnRowUpdating="GridViewSampleRowUpdating" 
                OnRowDeleting="GridViewSampleRowDeleting" OnRowCommand="GridViewSampleRowCommand"
                OnRowCancelingEdit="GridViewSampleRowCancelingEdit">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="Код товара (ключ)" HeaderStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label ID="lblCode" runat="server" Text='<%#Eval("Code") %>'/>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCode" runat="server" Text='<%#Eval("Code") %>'/>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddCode" runat="server"/>
                            <asp:RequiredFieldValidator ID="reqCode" ValidationGroup="ValgrpCust" ControlToValidate="txtAddCode" runat="server" ErrorMessage="*"/>
                        </FooterTemplate>                        
                        <HeaderStyle Width="15%"/>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Описание" HeaderStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="lblDEsc" runat="server" Text='<%#Eval("Description") %>'/>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDesc" runat="server" Text='<%#Eval("Description") %>'/>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddDesc" runat="server"/>
                        </FooterTemplate>
                        <HeaderStyle Width="15%"/>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Количество" HeaderStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label ID="lblQuantity" runat="server" Text='<%#Eval("Quantity") %>'/>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtQuantity" runat="server" Text='<%#Eval("Quantity") %>'/>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddQuantity" runat="server"/>
                            <asp:RequiredFieldValidator ID="reqQuantity" ValidationGroup="ValgrpCust" ControlToValidate="txtAddQuantity" runat="server" ErrorMessage="*"/>
                        </FooterTemplate>
                        <HeaderStyle Width="15%"></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Цена" HeaderStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label ID="lblPrice" runat="server" Text='<%#Eval("Price") %>'/>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPrice" runat="server" Text='<%#Eval("Price") %>'/>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtAddPrice" runat="server"/>
                            <asp:RequiredFieldValidator ID="reqPrice" ValidationGroup="ValgrpCust" ControlToValidate="txtAddPrice" runat="server" ErrorMessage="*"/>
                        </FooterTemplate>
                        <HeaderStyle Width="15%"></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Редактировать/Удалить" HeaderStyle-Width="15%">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEdit" Text="Edit" runat="server" CommandName="Edit" />
                            <span onclick="return confirm('Are you sure want to delete?')">
                                <asp:LinkButton ID="btnDelete" Text="Delete" runat="server" CommandName="Delete" />
                            </span>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="btnUpdate" Text="Update" runat="server" CommandName="Update" />
                            <asp:LinkButton ID="btnCancel" Text="Cancel" runat="server" CommandName="Cancel" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:Button ID="btnInsertRecord" runat="server" Text="Add" ValidationGroup="ValgrpCust" CommandName="Insert" />
                        </FooterTemplate>
                        <HeaderStyle Width="15%"/>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:DropDownList ID="DropDownUpdateSource" runat="server"/>        
        <asp:Button ID="ButtonSave" runat="server" Text="Сохранить" OnClick="ButtonSaveOnClick" />
        <asp:Button ID="ButtonAdd" runat="server" Text="Добавить" OnClick="ButtonAddOnClick" />
    </form>
</body>
</html>
