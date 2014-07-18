using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Text;
using System.Collections.Generic;

public partial class Grid : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static string LoadTexts(int Skip, int Take)
    {
        var lstText = new List<string>();
        var lstTextReturn = new StringBuilder();

        System.Threading.Thread.Sleep(1000);

        //Simulate a collection of data with a list<string>
        for (int i = 0; i < 1000; i++)
        {
            lstText.Add("Text " + i);
        }

        var lstSelectedText = (from text in lstText select text).Skip(Skip).Take(Take);
        foreach (var text in lstSelectedText)
        {
            lstTextReturn.AppendFormat("<li>");
            lstTextReturn.AppendFormat(string.Format("{0}", text));
            lstTextReturn.AppendFormat("</li>");

        }
        return lstTextReturn.ToString();
    }
}