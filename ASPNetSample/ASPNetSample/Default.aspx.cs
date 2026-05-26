using ASPNetSample.BusinessLogic;
using ASPNetSample.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPNetSample
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            pnlMessage.Visible = false;
            if (!Page.IsPostBack)
            {
                LoadPersons();
            }
        }

        public void LoadPersons()
        {
            try
            { 
                PersonaService service = new PersonaService();
                List<Persona> personas = service.GetAllPersonas().ToList();
                gvMain.DataSource = personas;
                gvMain.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Ha ocurrido un error: " + ex.Message);
            }
        }

        void MostrarMensaje(string message) 
        {
            pnlMessage.Visible = true;
            lblMessage.Text = message;
        }
    }
}
