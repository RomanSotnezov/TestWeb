using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.UI.WebControls;
using TestAspWebApp.DAO;
using TestAspWebApp.Model;

namespace TestAspWebApp
{
    public partial class WebForm : System.Web.UI.Page
    {
        #region Data Providers
        /// <summary>
        /// Источник данных - файл.
        /// </summary>
        private readonly IDataProvider fileProvider = new FileDataProvider();

        /// <summary>
        /// Источник данных - база данных.
        /// </summary>
        private readonly IDataProvider sqlProvider = new DatabaseDataProvider();

        /// <summary>
        /// Провайдер для чтения данных.
        /// </summary>
        private IDataProvider ViewProvider 
        { 
            get 
            {
                return GetProvider(SourceTypesExtensions.ParseFromGuiString(DropDownViewSource.SelectedValue));
            }
        }

        /// <summary>
        /// Провайдер для обновления данных.
        /// </summary>
        private IDataProvider UpdateProvider
        {
            get
            {
                return GetProvider(SourceTypesExtensions.ParseFromGuiString(DropDownUpdateSource.SelectedValue));
            }
        }

        private IDataProvider GetProvider(SourceTypes type)
        {
            switch (type)
            {
                case SourceTypes.File:
                    return fileProvider;
                case SourceTypes.DataBase:
                    return sqlProvider;
                default:
                    throw new InvalidEnumArgumentException();
            }
        }

        #endregion

        /// <summary>
        /// Список отображаемых элементов.
        /// </summary>
        private IList<OrderItem> CurrenItems
        {
            get { return Session["currentItems"] as IList<OrderItem>; }
            set { Session["currentItems"] = value; }
        }    

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var arr = Enum.GetValues(typeof(SourceTypes)).Cast<SourceTypes>().Select(t => new ListItem(t.ToGUIString())).ToArray();
                DropDownViewSource.Items.AddRange(arr);
                DropDownUpdateSource.Items.AddRange(arr);
            }
        }

        #region Data and event handlers

        protected void ButtonViewOnClick(object sender, EventArgs e)
        {
            CurrenItems = ViewProvider.Read().ToList();
            UpdateGridView();
        }

        /// <summary>
        /// Обновить контрол GridView.
        /// </summary>
        private void UpdateGridView()
        {
            GridViewSource.DataSource = CurrenItems;
            GridViewSource.DataBind();
            UpdatePanel1.Update();
        }

        protected void ButtonSaveOnClick(object sender, EventArgs e)
        {            
            UpdateProvider.Save(CurrenItems);
        }

        protected void ButtonAddOnClick(object sender, EventArgs e)
        {
            UpdateProvider.Add(CurrenItems);
        }

        protected void GridViewSampleRowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewSource.EditIndex = e.NewEditIndex;
            UpdateGridView();
        }

        protected void GridViewSampleRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            CurrenItems[GridViewSource.EditIndex].SetValues(GetOrderItem(GridViewSource.Rows[GridViewSource.EditIndex], false));            
            GridViewSource.EditIndex = -1;
            UpdateGridView();
        }

        protected void GridViewSampleRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            CurrenItems.RemoveAt(e.RowIndex);
            UpdateGridView();
        }

        protected void GridViewSampleRowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewSource.EditIndex = -1;
            UpdateGridView();
        }

        protected void GridViewSampleRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("Insert"))
            {
                CurrenItems.Add(GetOrderItem(GridViewSource.FooterRow, true));
                UpdateGridView();
            }            
        }

        #endregion

        /// <summary>
        /// Получить данные о строке заказа по строке таблицы.
        /// </summary>
        private OrderItem GetOrderItem(GridViewRow row, bool isFoot)
        {
            var code = (TextBox)row.FindControl("txt"+ (isFoot ? "Add" : "") + "Code");
            var desc = (TextBox)row.FindControl("txt" + (isFoot ? "Add" : "") + "Desc");
            var quantity = (TextBox)row.FindControl("txt" + (isFoot ? "Add" : "") + "Quantity");
            var price = (TextBox)row.FindControl("txt" + (isFoot ? "Add" : "") + "Price");
            return new OrderItem
            {
                Code = int.Parse(code.Text),
                Description = desc.Text,
                Quantity = float.Parse(quantity.Text),
                Price = float.Parse(price.Text)
            };
        }

    }
}