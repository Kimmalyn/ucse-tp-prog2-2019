[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WebApp.MVCGridConfig), "RegisterGrids")]

namespace WebApp
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Linq;
    using System.Collections.Generic;

    using MVCGrid.Models;
    using MVCGrid.Web;
    using WebApp.Grid;

    public static class MVCGridConfig 
    {
        public static void RegisterGrids()
        {
            DirectoresGrid.Register();
            DocentesGrid.Register();
            PadresGrid.Register();
            AlumnosGrid.Register();
        }
    }
}